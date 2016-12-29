using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
//зависимость пробрасывать в сити и в банк модел
namespace ExchangeRete.Core
{
	public class TabViewModel : MvxViewModel
	{
		
		public TabViewModel(IDataService service) 
		{
			Banks = (BanksViewModel) Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel
			                            (MvxViewModelRequest<BanksViewModel>.GetDefaultRequest(), null);
			Cities = (CitiesViewModel)Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel
			                             (MvxViewModelRequest<CitiesViewModel>.GetDefaultRequest(), null);
		}

		private BanksViewModel _banks;
		public BanksViewModel Banks
		{
			get { return _banks; }
			set { _banks = value; RaisePropertyChanged(() => Banks); }
		}

		private CitiesViewModel _cities;
		public CitiesViewModel Cities
		{
			get { return _cities; }
			set { _cities = value; RaisePropertyChanged(() => Cities); }
		}
	}
}
