using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExchangeRete.Core;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Views;

namespace ExchangeRete.Droid
{
	//[Activity(Label = "View for CitiesViewModel")]
	[Register("exchangerete.droid.CitiesView")]
	public class CitiesView : MvxFragment
	{



		//public new CitiesViewModel ViewModel
		//{
		//	get
		//	{
		//		return (CitiesViewModel)base.ViewModel;
		//	}
		//}


		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle saveInstanceState)
		{
			var ignore = base.OnCreateView(inflater, container, saveInstanceState);
			return this.BindingInflate(Resource.Layout.CitiesView, null);
		}

	}
}
