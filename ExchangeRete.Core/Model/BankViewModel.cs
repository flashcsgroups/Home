using System;
namespace ExchangeRete.Core
{
	
	public class BankViewModel : BankModel
	{
		//BankModel bank = new BankModel();
		public BankViewModel(BankModel Bank,EExchangeAction mode){
			
			this.Name = Bank.Name;
			this.EURBuy = Bank.EURBuy;
			this.EURSell = Bank.EURSell;
			this.USDBuy = Bank.USDBuy;
			this.USDSell = Bank.USDSell;
			 
			switch (mode)
			{
				case EExchangeAction.EURBuy:
					ValueCurrency = this.EURBuy;
					break;
				case EExchangeAction.EURSell:
					ValueCurrency = this.EURSell;
					break;
				case EExchangeAction.USDBuy:
					ValueCurrency = this.USDBuy;
					break;
				case EExchangeAction.USDSell:
					ValueCurrency = this.USDSell;
					break;
			}
		}
		public double ValueCurrency { get; set; }
	}
}
