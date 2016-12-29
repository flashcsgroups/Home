using System;
using Newtonsoft.Json;

namespace ExchangeRete.Core
{
	public class BankModel //: ICustomCloneable<BankModel>, ISelectableUnit
	{	
		[JsonProperty("BankId")]
		public string BankId { get; set; }
		[JsonProperty("Name")]
		public string Name { get; set; }
		[JsonProperty("EURBuy")]
		public double EURBuy { get; set; }
		[JsonProperty("EURSell")]
		public double EURSell { get; set; }
		[JsonProperty("USDBuy")]
		public double USDBuy { get; set; }
		[JsonProperty("USDSell")]
		public double USDSell { get; set; }
		[JsonProperty("Message")]
		public string Message { get; set; }
		[JsonProperty("TranslitId")]
		public string TranslitId { get; set; }

		//[JsonProperty("Url")]
		//public string Url { get; set; }
		//[JsonProperty("UrlName")]
		//public string UrlName { get; set; }
		//[JsonProperty("IsOutdated")]
		//public string IsOutdated { get; set; }
		//[JsonProperty("LastCheck")]
		//public string LastCheck { get; set; }
		//[JsonProperty("LastUpdate")]
		//public string LastUpdate { get; set; }	//sorting{}
	}
}