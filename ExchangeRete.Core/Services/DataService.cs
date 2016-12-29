using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRete.Core
{
	public class DataService : IDataService
	{
		private static string METHOD_Cities = "Cities";
		private static string METHOD_RatesForCity = "RatesForCity";

		public async Task<List<CityModel>> GetCities()
		{
			var cities = await WebDataService.getData<List<CityModel>>(METHOD_Cities);
			Settings.SaveSettingsCities = cities;

			// делает город выбранным из настроек
			if (!ReferenceEquals(null, Settings.SaveSettingsCitySelect)) { 
				for (int i = 0; i < cities.Count; i++)
				{
					if (cities[i].Id == Settings.SaveSettingsCitySelect.Id)
						cities[i].Selected = Settings.SaveSettingsCitySelect.Selected;
				}
			}

			return cities;
		}

		public async Task<List<BankModel>> GetBanks(int cityId)
		{
			var banks = new List<BankModel>();
			if (cityId.Equals(null))
			{
				banks = await WebDataService.getData<List<BankModel>>(METHOD_RatesForCity, "?cityId=", "4212");
			}
			else
			{
				banks = await WebDataService.getData<List<BankModel>>(METHOD_RatesForCity, "?cityId=", cityId.ToString());
			}
			Settings.SaveSettingsBanks = banks;

			return banks;
		}

		public event EventHandler OnSaveCityChanged;

		public void RaiseSaveCityChanged()
		{
			OnSaveCityChanged(this, null);
		}
	}
}
