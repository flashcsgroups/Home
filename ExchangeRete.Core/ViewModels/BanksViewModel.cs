using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using System.Linq;
using System.Collections.ObjectModel;

namespace ExchangeRete.Core                    
{
	public enum EExchangeAction : int
	{	
		EURBuy	=	0,
		EURSell	=	1,
		USDBuy	=	3,
		USDSell = 	2

	}

	public class BanksViewModel : BaseViewModel
	{
		private List<BankModel> _banks = new List<BankModel>();

		public List<BankModel> Banks
		{
			get
			{
				return _banks;
			}
			set
			{
				_banks = value;
				RaisePropertyChanged(() => Banks);
				CurrencyBank = SortBank.SortBanksByExchangeAction(Banks, Settings.SaveParamSortBanks);
			}
		}
		private List<BankViewModel> _currencyBank = new List<BankViewModel>();
		public List<BankViewModel> CurrencyBank
		{
			get
			{
				return _currencyBank;
			}
			set
			{
				_currencyBank = value;
				RaisePropertyChanged(() => CurrencyBank);
			}
		}

		public override async void Start()
		{
			LoadSettingsBanks();
			await DownloadData();
		}

		public BanksViewModel(IDataService service)
		{
			_dataService = service;

			// подписка на событие/ будет вызыватся когда сохранится город
			_dataService.OnSaveCityChanged += async (object sender, EventArgs e) =>
			{
				Banks =await _dataService.GetBanks(Convert.ToInt32(Settings.SaveSettingsCitySelect.Id));
			};
		}

		public void LoadSettingsBanks() {
			if (!ReferenceEquals(null, Settings.SaveSettingsBanks))
				CurrencyBank = SortBank.SortBanksByExchangeAction
									   (Settings.SaveSettingsBanks, EExchangeAction.EURBuy);
		}

		public async Task DownloadData()
		{
			if (ReferenceEquals(null, Settings.SaveSettingsBanks) &
			    ReferenceEquals(null, Settings.SaveSettingsCitySelect	))
			{
				CurrencyBank = SortBank.SortBanksByExchangeAction
				                       (Banks = await _dataService.GetBanks(4212), EExchangeAction.EURBuy);
			}
			else if (ReferenceEquals(null, Settings.SaveSettingsBanks)) 
			{
				CurrencyBank = SortBank.SortBanksByExchangeAction
									   (Banks = await _dataService.GetBanks(Settings.SaveSettingsCitySelect.Id), Settings.SaveParamSortBanks);
			}
			else
			{
				CurrencyBank = SortBank.SortBanksByExchangeAction
				                       (Banks = Settings.SaveSettingsBanks,	Settings.SaveParamSortBanks);	
			}
		}

		private IMvxCommand _selectBuySellEURUSD;
		public IMvxCommand SelectBuySellEURUSD
		{
			get
			{
				if (_selectBuySellEURUSD == null) 
				{
					_selectBuySellEURUSD = new MvxCommand<EExchangeAction>(mode => {
						//Сортируем и возвращаем CurrencyBank для отображения
						CurrencyBank = SortBank.SortBanksByExchangeAction( Banks , mode);
					});
				}
				return _selectBuySellEURUSD;
			}
		}
	}
}
