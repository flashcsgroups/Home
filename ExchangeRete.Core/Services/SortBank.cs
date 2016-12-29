using System;
using System.Collections.Generic;
using System.Linq;

namespace ExchangeRete.Core
{
	public static class SortBank
	{
		public static List<BankViewModel> SortBanksByExchangeAction(List<BankModel> banks, EExchangeAction mode)
		{//делаем фильтрацию и сохраняем в настройки
			var bybanks = new List<BankModel>();

				switch (mode)
				{
					case EExchangeAction.EURBuy:
						bybanks = banks.OrderBy(bank => 	bank.EURBuy).ToList<BankModel>();	//фильтрация
						Settings.SaveParamSortBanks = EExchangeAction.EURBuy;						//настройки
						break;
					
					case EExchangeAction.EURSell:
						bybanks = banks.OrderBy(bank => 	bank.EURSell).ToList<BankModel>();
						Settings.SaveParamSortBanks = EExchangeAction.EURSell;
						break;
					
					case EExchangeAction.USDBuy:
						bybanks = banks.OrderBy(bank => 	bank.USDBuy).ToList<BankModel>();
						Settings.SaveParamSortBanks = EExchangeAction.USDBuy;
						break;
					
					case EExchangeAction.USDSell:
						bybanks = banks.OrderBy(bank => 	bank.USDSell).ToList<BankModel>();
						Settings.SaveParamSortBanks = EExchangeAction.USDSell;
						break;
				}
			// banksBVM = new BanksViewModel();
			var banksVM = new List<BankViewModel>();

			foreach (var bank in bybanks)
			{
				banksVM.Add(new BankViewModel(bank, mode));

			}
			return banksVM; 
			//for (int j = 0; j < banks.Count; j++)
			//{
			//	for (int i = 0; i < banks.Count; i++)
			//	{
			//		switch (mode)
			//		{
			//			case EExchangeAction.EURBuy:
			//				if (Convert.ToDouble(banks[i].USDBuy) > Convert.ToDouble(banks[j].USDBuy))
			//				{
			//					bank = banks[i];
			//					banks[i] = banks[j];
			//					banks[j] = bank;
			//				}
			//				break;
			//			case EExchangeAction.EURSell:
			//				if (Convert.ToDouble(banks[i].EURSell) > Convert.ToDouble(banks[j].EURSell))
			//				{
			//					bank = banks[i];
			//					banks[i] = banks[j];
			//					banks[j] = bank;
			//				}
			//				break;
			//			case EExchangeAction.USDBuy:
			//				if (Convert.ToDouble(banks[i].USDBuy) > Convert.ToDouble(banks[j].USDBuy))
			//				{
			//					bank = banks[i];
			//					banks[i] = banks[j];
			//					banks[j] = bank;
			//				}
			//				break;
			//			case EExchangeAction.USDSell:
			//				if (Convert.ToDouble(banks[i].USDSell) > Convert.ToDouble(banks[j].USDSell))
			//				{
			//					bank = banks[i];
			//					banks[i] = banks[j];
			//					banks[j] = bank;
			//				}
			//				break;
			//			default:
			//				break;
			//		}
			//	}
			//}
			//return banks;
			
		}
	}
}
