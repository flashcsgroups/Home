using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Renderscripts;
using Android.Support.V4.View;
using Android.Widget;
using ExchangeRete.Core;
using MvvmCross.Droid.Support.V4;

namespace ExchangeRete.Droid.Views
{
	[Activity(Label = "View for TabViewModel")]
	public class TabView : MvxFragmentActivity
	{
		private ViewPager _viewPager;
		//private TitlePageIndicator _pageIndicator;
		private MvxViewPagerFragmentAdapter _adapter;

		public new TabViewModel ViewModel
		{
			get { return (TabViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}

		protected override void OnCreate(Android.OS.Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.TabView);

			var fragments = new List<MvxViewPagerFragmentAdapter.FragmentInfo>
			  {
				new MvxViewPagerFragmentAdapter.FragmentInfo
				{
					FragmentType = typeof(CitiesView),
				  	Title = "Fragment1",
					ViewModel = ViewModel.Cities
				},
				new MvxViewPagerFragmentAdapter.FragmentInfo
				{
					FragmentType = typeof(BanksView),
				  	Title = "Fragment2",
					ViewModel = ViewModel.Banks
				}
			  };
			_viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
			_adapter = new MvxViewPagerFragmentAdapter(this, SupportFragmentManager, fragments);
			_viewPager.Adapter = _adapter;

			//_pageIndicator = FindViewById<titlepageindicator>(Resource.Id.viewPagerIndicator);

			//_pageIndicator.SetViewPager(_viewPager);
			//_pageIndicator.CurrentItem = 0;
		}

	}
}
