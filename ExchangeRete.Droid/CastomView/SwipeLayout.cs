using System;

using Android.Content;
using Android.Views;
//using ExchangeRete.Droid;
using Android.Util;
using Android.Animation;
using Android.Support.V4.View;
using Android.Content.Res;
using Java.Lang;
using Android.Support.V4.Widget;
using Android.Views.Animations;
using System.Collections.Generic;
//using Java.Util;

//import android.animation.ObjectAnimator;
//import android.content.Context;
//import android.content.res.TypedArray;
//import android.support.v4.view.NestedScrollingParent;
//import android.support.v4.view.ViewCompat;
//import android.support.v4.widget.ViewDragHelper;
//import android.util.AttributeSet;
//import android.util.Log;
//import android.util.TypedValue;
//import android.view.MotionEvent;
//import android.view.View;
//import android.view.ViewConfiguration;
//import android.view.ViewGroup;
//import android.view.animation.AccelerateInterpolator;

//import com.rds.swipelayout.R;

//import java.lang.ref.WeakReference;
//import java.util.Map;
//using Java.Util.WeakHashMap;
using System.Runtime.CompilerServices;

namespace ExchangeRete.Droid
{
	public class SwipeLayout : ViewGroup
	{
		private static readonly String TAG = SwipeLayout.class.getSimpleName();
		private static readonly float VELOCITY_THRESHOLD = 1500f;
		private ViewDragHelper dragHelper;
		private View leftView;
		private View rightView;
		private View centerView;
		private float velocityThreshold;
		private float touchSlop;
		private OnSwipeListener swipeListener;
		private WeakReference<ObjectAnimator> resetAnimator;

		//ConditionalWeakTable https://msdn.microsoft.com/en-us/library/dd287757.aspx
		private readonly Map<View, Boolean> hackedParents = new WeakHashMap<>();
		private bool leftSwipeEnabled = true;
		private bool rightSwipeEnabled = true;

		private static readonly int TOUCH_STATE_WAIT = 0;
		private static readonly int TOUCH_STATE_SWIPE = 1;
		private static readonly int TOUCH_STATE_SKIP = 2;

		private int touchState = TOUCH_STATE_WAIT;
		private float touchX;
		private float touchY;

		public SwipeLayout(Context context)
		{
			//super(context);
			base.SwipeLayout(context);
			init(context, null);
		}

		public SwipeLayout(Context context, IAttributeSet attrs)
		{
			base.SwipeLayout(context, attrs);
			//super(context, attrs);
			init(context, attrs);
		}

		public SwipeLayout(Context context, IAttributeSet attrs, int defStyleAttr)
		{
			base.SwipeLayout(context, attrs, defStyleAttr);
			//super(context, attrs, defStyleAttr);
			init(context, attrs);
		}

		private void init(Context context, IAttributeSet attrs)
		{
			dragHelper = ViewDragHelper.Create(this, 1f, dragCallback);
			velocityThreshold = TypedValue.ApplyDimension(TypedValue.COMPLEX_UNIT_DIP, VELOCITY_THRESHOLD, getResources().getDisplayMetrics());
			touchSlop = ViewConfiguration.Get(Context).ScaledEdgeSlop;
			//touchSlop = ViewConfiguration.Get(GetContext()).getScaledTouchSlop();

			if (attrs != null)
			{
				TypedArray a = context.ObtainStyledAttributes(attrs, Resource.Styleable.SwipeLayout);
				if (a.HasValue(Resource.Styleable.SwipeLayout_swipe_enabled))
				{
					leftSwipeEnabled = a.GetBoolean(Resource.Styleable.SwipeLayout_swipe_enabled, true);
					rightSwipeEnabled = a.GetBoolean(Resource.Styleable.SwipeLayout_swipe_enabled, true);
				}
				if (a.HasValue(Resource.Styleable.SwipeLayout_left_swipe_enabled))
				{
					leftSwipeEnabled = a.GetBoolean(Resource.Styleable.SwipeLayout_left_swipe_enabled, true);
				}
				if (a.HasValue(Resource.Styleable.SwipeLayout_right_swipe_enabled))
				{
					rightSwipeEnabled = a.GetBoolean(Resource.Styleable.SwipeLayout_right_swipe_enabled, true);
				}
				a.Recycle();
			}
		}

		public void setOnSwipeListener(OnSwipeListener swipeListener)
		{
			this.swipeListener = swipeListener;
		}

		/**
	     * reset swipe-layout state to initial position
	     */
		public void reset()
		{
			if (centerView == null) return;

			finishResetAnimator();
			dragHelper.Abort();

			offsetChildren(null, -centerView.Left);
		}

		/**
	     * reset swipe-layout state to initial position with animation (200ms)
	     */
		public void animateReset()
		{
			if (centerView == null) return;

			finishResetAnimator();
			dragHelper.Abort();

			ObjectAnimator animator = new ObjectAnimator();
			animator.SetTarget(this);
			//animator.SetPropertyName("offset");
			animator.PropertyName("offset");
			//animator.SetPropertyName("offset");AccelerateInterpolator()
			//animator.PropertyName("offset");
			animator.SetInterpolator(new AccelerateInterpolator());
			animator.SetIntValues(centerView.Left, 0);
			animator.SetDuration(200);
			animator.Start();
			resetAnimator = new WeakReference<ObjectAnimator>(animator);//---------ТУТ 
		}

		private void finishResetAnimator()
		{
			if (resetAnimator == null) return;

			ObjectAnimator animator = resetAnimator.get();

			if (animator != null)
			{
				resetAnimator.clear();
				if (animator.IsRunning)//isRunning()
				{
					animator.End();
				}
			}
		}

		/**
	     * get horizontal offset from initial position
	     */
		public int getOffset()
		{
			return centerView == null ? 0 : centerView.Left;
		}

		/**
	     * set horizontal offset from initial position
	     */
		public void setOffset(int offset)
		{
			if (centerView != null)
			{
				offsetChildren(null, offset - centerView.Left);
			}
		}

		public bool isSwipeEnabled()
		{
			return leftSwipeEnabled || rightSwipeEnabled;
		}

		public bool isLeftSwipeEnabled()
		{
			return leftSwipeEnabled;
		}

		public bool isRightSwipeEnabled()
		{
			return rightSwipeEnabled;
		}

		/**
	     * enable or disable swipe gesture handling
	     *
	     * @param enabled
	     */
		public void setSwipeEnabled(bool enabled)
		{
			this.leftSwipeEnabled = enabled;
			this.rightSwipeEnabled = enabled;
		}

		/**
	     * Enable or disable swipe gesture from left side
	     *
	     * @param leftSwipeEnabled
	     */

		public void setLeftSwipeEnabled(bool leftSwipeEnabled)
		{
			this.leftSwipeEnabled = leftSwipeEnabled;
		}

		/**
	     * Enable or disable swipe gesture from right side
	     *
	     * @param rightSwipeEnabled
	     */

		public void setRightSwipeEnabled(bool rightSwipeEnabled)
		{
			this.rightSwipeEnabled = rightSwipeEnabled;
		}


		protected void onMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			int count = ChildCount;

			int maxHeight = 0;

			// Find out how big everyone wants to be
			if (MeasureSpec.GetMode(heightMeasureSpec) == MeasureSpec.EXACTLY)
			{
				MeasureChildren(widthMeasureSpec, heightMeasureSpec);
			}
			else {
				//find a child with biggest height
				for (int i = 0; i < count; i++)
				{
					View child = GetChildAt(i);
					MeasureChild(child, widthMeasureSpec, heightMeasureSpec);
					maxHeight = System.Math.Max(maxHeight, child.MeasuredHeight);
				}

				if (maxHeight > 0)
				{
					heightMeasureSpec = MeasureSpec.MakeMeasureSpec(maxHeight, MeasureSpec.EXACTLY);
					MeasureChildren(widthMeasureSpec, heightMeasureSpec);
				}
			}

			// Find rightmost and bottom-most child
			for (int i = 0; i < count; i++)
			{
				View child = GetChildAt(i);
				if (child.Visibility != GONE)
				{
					int childBottom;

					childBottom = child.MeasuredHeight;
					maxHeight = System.Math.Max(maxHeight, childBottom);
				}
			}

			maxHeight += PaddingTop + PaddingBottom;
			maxHeight = System.Math.Max(maxHeight, SuggestedMinimumHeight);

			SetMeasuredDimension(ResolveSize(SuggestedMinimumWidth, widthMeasureSpec),
					ResolveSize(maxHeight, heightMeasureSpec));
		}

		protected void onLayout(bool changed, int left, int top, int right, int bottom)
		{
			layoutChildren(left, top, right, bottom);
		}

		private void layoutChildren(int left, int top, int right, int bottom)
		{
			readonly int count = ChildCount;

			readonly int parentTop = PaddingTop;

			for (int i = 0; i < count; i++)
			{
				View child = GetChildAt(i);
				LayoutParams lp = (LayoutParams)child.LayoutParameters;
				switch (lp.gravity)
				{
					case LayoutParams.CENTER:
						centerView = child;
						break;

					case LayoutParams.LEFT:
						leftView = child;
						break;

					case LayoutParams.RIGHT:
						rightView = child;
						break;
				}
			}

			if (centerView == null) throw new RuntimeException("Center view must be added");

			for (int i = 0; i < count; i++)
			{
				readonly View child = GetChildAt(i);
				if (child.Visibility != GONE)
				{
					readonly LayoutParams lp = (LayoutParams)child.LayoutParameters;

					readonly int width = child.MeasuredWidth;
					readonly int height = child.MeasuredHeight;

					int childLeft;
					int childTop;

					int orientation = lp.gravity;

					switch (orientation)
					{
						case LayoutParams.LEFT:
							childLeft = centerView.Left - width;
							break;

						case LayoutParams.RIGHT:
							childLeft = centerView.Right;
							break;

						case LayoutParams.CENTER:
						default:
							childLeft = child.Left;
							break;
					}
					childTop = parentTop;

					child.Layout(childLeft, childTop, childLeft + width, childTop + height);
				}
			}
		}
		//private readonly ViewDragHelper.Callback dragCallback = new ViewDragHelper.Callback()
		//{  		//	private int initLeft;
		//	public bool tryCaptureView(View child, int pointerId)
		//	{
		//		initLeft = child.Left;
		//		return true;
		//	}


		//	public int clampViewPositionHorizontal(View child, int left, int dx)
		//	{
		//		if (dx > 0)
		//		{
		//			return clampMoveRight(child, left);
		//		}
		//		else {
		//			return clampMoveLeft(child, left);
		//		}
		//	}


		//	public int getViewHorizontalDragRange(View child)
		//	{
		//		return Width;
		//	}


		//	public void onViewReleased(View releasedChild, float xvel, float yvel)
		//	{
		//		Log.d(TAG, "VELOCITY " + xvel + "; THRESHOLD " + velocityThreshold);

		//		int dx = releasedChild.Left - initLeft;
		//		if (dx == 0) return;


		//		bool handled = false;
		//		if (dx > 0)
		//		{

		//			handled = xvel >= 0 ? onMoveRightReleased(releasedChild, dx, xvel) : onMoveLeftReleased(releasedChild, dx, xvel);

		//		}
		//		else if (dx < 0)
		//		{

		//			handled = xvel <= 0 ? onMoveLeftReleased(releasedChild, dx, xvel) : onMoveRightReleased(releasedChild, dx, xvel);
		//		}

		//		if (!handled)
		//		{
		//			startScrollAnimation(releasedChild, releasedChild.Left - centerView.Left, false, dx > 0);
		//		}
		//	}

		//	private bool leftViewClampReached(LayoutParams leftViewLP)
		//	{
		//		if (leftView == null) return false;

		//		switch (leftViewLP.clamp)
		//		{
		//			case LayoutParams.CLAMP_PARENT:
		//				return leftView.Right >= Width;

		//			case LayoutParams.CLAMP_SELF:
		//				return leftView.Right >= leftView.Width;

		//			default:
		//				return leftView.Right >= leftViewLP.clamp;
		//		}
		//	}

		//	private bool rightViewClampReached(LayoutParams lp)
		//	{
		//		if (rightView == null) return false;

		//		switch (lp.clamp)
		//		{
		//			case LayoutParams.CLAMP_PARENT:
		//				return rightView.Right <= Width;

		//			case LayoutParams.CLAMP_SELF:
		//				return rightView.Right <= Width;

		//			default:
		//				return rightView.Left + lp.clamp <= Width;
		//		}
		//	}


		//	public void onViewPositionChanged(View changedView, int left, int top, int dx, int dy)
		//	{
		//		offsetChildren(changedView, dx);

		//		if (swipeListener == null) return;

		//		int stickyBound;
		//		if (dx > 0)
		//		{
		//			//move to right

		//			if (leftView != null)
		//			{
		//				stickyBound = getStickyBound(leftView);
		//				if (stickyBound != LayoutParams.STICKY_NONE)
		//				{
		//					if (leftView.Right - stickyBound > 0 && leftView.Right - stickyBound - dx <= 0)
		//						swipeListener.onLeftStickyEdge(SwipeLayout.this, true);
		//				}
		//			}

		//			if (rightView != null)
		//			{
		//				stickyBound = getStickyBound(rightView);
		//				if (stickyBound != LayoutParams.STICKY_NONE)
		//				{
		//					if (rightView.Left + stickyBound > Width && rightView.Left + stickyBound - dx <= Width)
		//						swipeListener.onRightStickyEdge(SwipeLayout.this, true);
		//				}
		//			}
		//		}
		//		else if (dx < 0)
		//		{
		//			//move to left

		//			if (leftView != null)
		//			{
		//				stickyBound = getStickyBound(leftView);
		//				if (stickyBound != LayoutParams.STICKY_NONE)
		//				{
		//					if (leftView.Right - stickyBound <= 0 && leftView.Right - stickyBound - dx > 0)
		//						swipeListener.onLeftStickyEdge(SwipeLayout.this, false);
		//				}
		//			}

		//			if (rightView != null)
		//			{
		//				stickyBound = getStickyBound(rightView);
		//				if (stickyBound != LayoutParams.STICKY_NONE)
		//				{
		//					if (rightView.Left + stickyBound <= Width && rightView.Left + stickyBound - dx > Width)
		//						swipeListener.onRightStickyEdge(SwipeLayout.this, false);
		//				}
		//			}
		//		}
		//	}

		//	private int getStickyBound(View view)
		//	{
		//		LayoutParams lp = getLayoutParams(view);
		//		if (lp.sticky == LayoutParams.STICKY_NONE) return LayoutParams.STICKY_NONE;

		//		return lp.sticky == LayoutParams.STICKY_SELF ? view.Width : lp.sticky;
		//	}

		//	private int clampMoveRight(View child, int left)
		//	{
		//		if (leftView == null)
		//		{
		//			return child == centerView ? System.Math.Min(left, 0) : System.Math.Min(left, Width);
		//		}

		//		LayoutParams lp = getLayoutParams(leftView);
		//		switch (lp.clamp)
		//		{
		//			case LayoutParams.CLAMP_PARENT:
		//				return System.Math.Min(left, Width + child.Left - leftView.Right);

		//			case LayoutParams.CLAMP_SELF:
		//				return System.Math.Min(left, child.Left - leftView.Left);

		//			default:
		//				return System.Math.Min(left, child.Left - leftView.Right + lp.clamp);
		//		}
		//	}

		//	private int clampMoveLeft(View child, int left)
		//	{
		//		if (rightView == null)
		//		{
		//			return child == centerView ? System.Math.Max(left, 0) : System.Math.Max(left, -child.Width);
		//		}

		//		LayoutParams lp = getLayoutParams(rightView);
		//		switch (lp.clamp)
		//		{
		//			case LayoutParams.CLAMP_PARENT:

		//				return System.Math.Max(child.Left - rightView.Left, left);

		//			case LayoutParams.CLAMP_SELF:
		//				return System.Math.Max(left, Width - rightView.Left + child.Left - rightView.Width);

		//			default:
		//				return System.Math.Max(left, Width - rightView.Left + child.Left - lp.clamp);
		//		}
		//	}

		//	private bool onMoveRightReleased(View child, int dx, float xvel)
		//	{

		//		if (xvel > velocityThreshold)
		//		{
		//			int left = centerView.Left < 0 ? child.Left - centerView.Left : Width;
		//			bool moveToOriginal = centerView.Left < 0;
		//			startScrollAnimation(child, clampMoveRight(child, left), !moveToOriginal, true);
		//			return true;
		//		}

		//		if (leftView == null)
		//		{
		//			startScrollAnimation(child, child.Left - centerView.Left, false, true);
		//			return true;
		//		}

		//		LayoutParams lp = getLayoutParams(leftView);

		//		if (dx > 0 && xvel >= 0 && leftViewClampReached(lp))
		//		{
		//			if (swipeListener != null)
		//			{
		//				swipeListener.onSwipeClampReached(SwipeLayout.this, true);
		//			}
		//			return true;
		//		}

		//		if (dx > 0 && xvel >= 0 && lp.bringToClamp != LayoutParams.BRING_TO_CLAMP_NO && leftView.Right > lp.bringToClamp)
		//		{
		//			int left = centerView.Left < 0 ? child.Left - centerView.Left : Width;
		//			startScrollAnimation(child, clampMoveRight(child, left), true, true);
		//			return true;
		//		}

		//		if (lp.sticky != LayoutParams.STICKY_NONE)
		//		{
		//			int stickyBound = lp.sticky == LayoutParams.STICKY_SELF ? leftView.Width : lp.sticky;
		//			float amplitude = stickyBound * lp.stickySensitivity;

		//			if (isBetween(-amplitude, amplitude, centerView.Left - stickyBound))
		//			{
		//				bool toClamp = (lp.clamp == LayoutParams.CLAMP_SELF && stickyBound == leftView.Width) ||
		//						lp.clamp == stickyBound ||
		//						(lp.clamp == LayoutParams.CLAMP_PARENT && stickyBound == Width);
		//				startScrollAnimation(child, child.Left - centerView.Left + stickyBound, toClamp, true);
		//				return true;
		//			}
		//		}
		//		return false;
		//	}

		//	private bool onMoveLeftReleased(View child, int dx, float xvel)
		//	{
		//		if (-xvel > velocityThreshold)
		//		{
		//			int left = centerView.Left > 0 ? child.Left - centerView.Left : -Width;
		//			bool moveToOriginal = centerView.Left > 0;
		//			startScrollAnimation(child, clampMoveLeft(child, left), !moveToOriginal, false);
		//			return true;
		//		}

		//		if (rightView == null)
		//		{
		//			startScrollAnimation(child, child.Left - centerView.Left, false, false);
		//			return true;
		//		}


		//		LayoutParams lp = getLayoutParams(rightView);

		//		if (dx < 0 && xvel <= 0 && rightViewClampReached(lp))
		//		{
		//			if (swipeListener != null)
		//			{
		//				swipeListener.onSwipeClampReached(SwipeLayout.this, false);
		//			}
		//			return true;
		//		}

		//		if (dx < 0 && xvel <= 0 && lp.bringToClamp != LayoutParams.BRING_TO_CLAMP_NO && rightView.Left + lp.bringToClamp < Width)
		//		{
		//			int left = centerView.Left > 0 ? child.Left - centerView.Left : -Width;
		//			startScrollAnimation(child, clampMoveLeft(child, left), true, false);
		//			return true;
		//		}

		//		if (lp.sticky != LayoutParams.STICKY_NONE)
		//		{
		//			int stickyBound = lp.sticky == LayoutParams.STICKY_SELF ? rightView.Width : lp.sticky;
		//			float amplitude = stickyBound * lp.stickySensitivity;

		//			if (isBetween(-amplitude, amplitude, centerView.Right + stickyBound - Width))
		//			{
		//				bool toClamp = (lp.clamp == LayoutParams.CLAMP_SELF && stickyBound == rightView.Width) ||
		//						lp.clamp == stickyBound ||
		//						(lp.clamp == LayoutParams.CLAMP_PARENT && stickyBound == Width);
		//				startScrollAnimation(child, child.Left - rightView.Left + Width - stickyBound, toClamp, false);
		//				return true;
		//			}
		//		}

		//		return false;
		//	}

		//	private bool isBetween(float left, float right, float check)
		//	{
		//		return check >= left && check <= right;
		//	}
		//};

		private void startScrollAnimation(View view, int targetX, bool moveToClamp, bool toRight)
		{
			if (dragHelper.SettleCapturedViewAt(targetX, view.Top))//view.GetTop()
			{
				ViewCompat.PostOnAnimation(view, new SettleRunnable(view, moveToClamp, toRight));
			}
			else {
				if (moveToClamp && swipeListener != null)
				{
					swipeListener.onSwipeClampReached(SwipeLayout.this, toRight);
				}
			}
		}

		private LayoutParams getLayoutParams(View view)
		{
			return view.LayoutParameters;//спорный момент
		}

		private void offsetChildren(View skip, int dx)
		{
			if (dx == 0) return;

			int count = ChildCount;
			for (int i = 0; i < count; i++)
			{
				View child = GetChildAt(i);
				if (child == skip) continue;
				child.OffsetLeftAndRight(dx);
				Invalidate(child.Left, child.Top, child.Right, child.Bottom);
			}
		}

		private void hackParents()
		{
			IViewParent parent = Parent;
			while (parent != null)
			{
				if (parent.GetType().Equals(typeof(INestedScrollingParent)))
				{
					View view = (View)parent;
					hackedParents.put(view, view.Enabled);
				}
				parent = parent.Parent;
			}
		}

		private void unHackParents()
		{
			for (Map.Entry<View, Boolean> entry : hackedParents.entrySet())
			{
				View view = entry.getKey();
				if (view != null)
				{
					view.getEnabled(entry.getValue());

				}
			}
			hackedParents.clear();
		}

		public bool onInterceptTouchEvent(MotionEvent eevent)
		{
			return isSwipeEnabled()
					? dragHelper.ShouldInterceptTouchEvent(eevent)
					                : base.OnInterceptTouchEvent(eevent);
		}


		public bool onTouchEvent(MotionEvent eevent)
		{
			bool defaultResult = base.OnTouchEvent(eevent);
			if (!isSwipeEnabled())
			{
				return defaultResult;
			}

			switch (eevent.ActionMasked)
			{
				//case MotionEvent.ACTION_DOWN:
				case MotionEventActions.PointerDown:
					touchState = TOUCH_STATE_WAIT;
					touchX = eevent.GetX();
					touchY = eevent.GetY();
					break;
					
				case MotionEventActions.Move:
					if (touchState == TOUCH_STATE_WAIT)
					{
						float dx = System.Math.Abs(eevent.GetX() - touchX);
						float dy = System.Math.Abs(eevent.GetY() - touchY);

						bool isLeftToRight = (eevent.GetX() - touchX) > 0;

						if ((isLeftToRight && !leftSwipeEnabled) || (!isLeftToRight && !rightSwipeEnabled))
						{
							return defaultResult;
						}

						if (dx >= touchSlop || dy >= touchSlop)
						{
							touchState = dy == 0 || dx / dy > 1f ? TOUCH_STATE_SWIPE : TOUCH_STATE_SKIP;
							if (touchState == TOUCH_STATE_SWIPE)
							{
								RequestDisallowInterceptTouchEvent(true);

								hackParents();

								if (swipeListener != null)
									swipeListener.onBeginSwipe(this, eevent.GetX() > touchX);
							}
						}
					}
					break;

				case MotionEventActions.Cancel:
				//case MotionEvent.ACTION_UP:
				case MotionEventActions.PointerUp:
					if (touchState == TOUCH_STATE_SWIPE)
					{
						unHackParents();
						RequestDisallowInterceptTouchEvent(false);
					}
					touchState = TOUCH_STATE_WAIT;
					break;
			}

			if (eevent.ActionMasked != MotionEventActions.Move || touchState == TOUCH_STATE_SWIPE)
			{
				dragHelper.ProcessTouchEvent(eevent);

			}

			return true;
		}


		protected LayoutParams generateDefaultLayoutParams()
		{
			return new LayoutParams(LayoutParams.MATCH_PARENT, LayoutParams.WRAP_CONTENT);
		}


		public LayoutParams generateLayoutParams(IAttributeSet attrs)
		{
			return new LayoutParams(getContext(), attrs);
		}


		protected LayoutParams generateLayoutParams(ViewGroup.LayoutParams p)
		{
			return new LayoutParams(p);
		}


		protected bool checkLayoutParams(ViewGroup.LayoutParams p)
		{
			//return obj.GetType().IsAssignableFrom(otherObj.GetType())
			return (p.GetType().Equals(typeof(LayoutParams)));

		}

		private  class SettleRunnable : IRunnable
		{
			
			private readonly View mView;
			private readonly bool moveToClamp;
			private readonly bool moveToRight;

			public IntPtr Handle
			{
				get
				{
					return (IntPtr)0;
				}
			}

			SettleRunnable(View view, bool moveToClamp, bool moveToRight)
			{
				this.mView = view;
				this.moveToClamp = moveToClamp;
				this.moveToRight = moveToRight;
			}

			public void Run()
			{
				if (dragHelper != null && dragHelper.continueSettling(true))
				{
					ViewCompat.PostOnAnimation(this.mView, this);
				}
				else
				{
					Log.d(TAG, "ONSWIPE clamp: " + moveToClamp + " ; moveToRight: " + moveToRight);
					if (moveToClamp && swipeListener != null)
					{
						swipeListener.onSwipeClampReached(SwipeLayout.this, moveToRight);
					}
				}
			}

			public void Dispose()
			{
				throw new NotImplementedException();
			}
		}

		public class LayoutParams : ViewGroup.LayoutParams
		{
			public static readonly int LEFT = -1;
			public static readonly int RIGHT = 1;
			public static readonly int CENTER = 0;

			public static readonly int CLAMP_PARENT = -1;
			public static readonly int CLAMP_SELF = -2;
			public static readonly int BRING_TO_CLAMP_NO = -1;

			public static readonly int STICKY_SELF = -1;
			public static readonly int STICKY_NONE = -2;
			private static readonly float DEFAULT_STICKY_SENSITIVITY = 0.9f;

			private int gravity = CENTER;
			private int sticky;
			private float stickySensitivity = DEFAULT_STICKY_SENSITIVITY;
			private int clamp = CLAMP_SELF;
			private int bringToClamp = BRING_TO_CLAMP_NO;

			public LayoutParams(Context c, IAttributeSet attrs)
			{
				base.LayoutParams(c, attrs);
				//super(c, attrs);

				TypedArray a = c.ObtainStyledAttributes(attrs, Resource.Styleable.SwipeLayout);

				readonly int N = a.IndexCount;//a.GetIndexCount()
				for (int i = 0; i < N; ++i)
				{
					int attr = a.GetIndex(i);
					if (attr == Resource.Styleable.SwipeLayout_gravity)
					{
						gravity = a.GetInt(attr, CENTER);
					}
					else if (attr == Resource.Styleable.SwipeLayout_sticky)
					{
						sticky = a.GetLayoutDimension(attr, STICKY_SELF);
					}
					else if (attr == Resource.Styleable.SwipeLayout_clamp)
					{
						clamp = a.GetLayoutDimension(attr, CLAMP_SELF);
					}
					else if (attr == Resource.Styleable.SwipeLayout_bring_to_clamp)
					{
						bringToClamp = a.GetLayoutDimension(attr, BRING_TO_CLAMP_NO);
					}
					else if (attr == Resource.Styleable.SwipeLayout_sticky_sensitivity)
					{
						stickySensitivity = a.GetFloat(attr, DEFAULT_STICKY_SENSITIVITY);
					}
				}
				a.Recycle();
			}

			public LayoutParams(ViewGroup.LayoutParams source)
			{
				base.LayoutParams(source);
				//super(source);
			}

			public LayoutParams(int width, int height)
			{
				base.LayoutParams(width, height);
				//super(width, height);
			}
		}

		public interface OnSwipeListener
		{
			void onBeginSwipe(SwipeLayout swipeLayout, bool moveToRight);

			void onSwipeClampReached(SwipeLayout swipeLayout, bool moveToRight);

			void onLeftStickyEdge(SwipeLayout swipeLayout, bool moveToRight);

			void onRightStickyEdge(SwipeLayout swipeLayout, bool moveToRight);
		}
	}
}
