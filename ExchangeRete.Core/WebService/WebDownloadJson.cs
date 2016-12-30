using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Platform;

namespace ExchangeRete.Core
{
	
	public static class WebDataService
	{									//http://metanit.com/sharp/xamarin/6.1.php
		private static string url = "http://wsv3.cash2cash.ru/ExRatesJson.svc/";

		public static async Task<T> getData<T>(string METHOD, string parametrs = null, string value = null)
		{
			try
			{
				string urlmod = url + METHOD + parametrs + value;
				HttpClient client = new HttpClient();
				var json = await client.GetStringAsync(urlmod);

				return JsonConvert.DeserializeObject<T>(json);
			}
			catch (Exception e)
			{	
				Mvx.Error("An error occured while getting data from internet: {0}", e);
				return default(T);
			}
		}
	}

}
