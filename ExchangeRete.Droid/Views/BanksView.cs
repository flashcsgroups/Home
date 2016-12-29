using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using ExchangeRete.Core;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views;

namespace ExchangeRete.Droid
{
	//[Activity(Label = "View for BanksViewModel")]
	[Register("exchangerete.droid.BanksView")]
	public class BanksView : MvxFragment
	{



		//public new BanksViewModel ViewModel
		//{
		//	get
		//	{
		//		return (BanksViewModel)base.ViewModel;
		//	}
		//}


		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle saveInstanceState)
		{
			var ignore = base.OnCreateView(inflater, container, saveInstanceState);
			return this.BindingInflate(Resource.Layout.BanksView, null);
		}

	}
}
