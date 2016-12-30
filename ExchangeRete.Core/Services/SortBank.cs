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
			var banksVM = new List<BankViewModel>();

			foreach (var bank in bybanks)
			{
				banksVM.Add(new BankViewModel(bank, mode));
			}
			return banksVM; 
		}
	}
}
