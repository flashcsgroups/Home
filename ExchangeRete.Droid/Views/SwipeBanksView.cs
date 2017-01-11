using System;

using Android.App;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Linq;
using Android.Support.V4.App;
using Android.Graphics.Drawables;
using FortySevenDeg.SwipeListView;

namespace ExchangeRete.Droid
{
	[Activity(Label = "Swipe ListView", MainLauncher = true)]
	public class swipebanks_list_item : FragmentActivity
	{
		SwipeListView _swipeListView;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.swipebanks_list_item);

			_swipeListView = FindViewById<SwipeListView>(Resource.Id.example_lv_list);

			_swipeListView = (Resource.Layout.banks_list_item);
			_swipeListView.FrontViewClicked += HandleFrontViewClicked;
			_swipeListView.BackViewClicked += HandleBackViewClicked;
		}

		void HandleFrontViewClicked(object sender, SwipeListViewClickedEventArgs e)
		{
			RunOnUiThread(() => _swipeListView.OpenAnimate(e.Position));
		}

		void HandleBackViewClicked(object sender, SwipeListViewClickedEventArgs e)
		{
			RunOnUiThread(() => _swipeListView.CloseAnimate(e.Position));
		}

	}
}
