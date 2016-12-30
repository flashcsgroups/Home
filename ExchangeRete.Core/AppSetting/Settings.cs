using System;
using System.Threading.Tasks;
using Plugin.Settings.Abstractions;
using Plugin.Settings;
using MvvmCross.Platform;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExchangeRete.Core
{
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		private const string CitiesKey = "Cities";
		public static List<CityModel> SaveSettingsCities
		{
			get
			{
				var citymodel = AppSettings.GetValueOrDefault(CitiesKey, "");
				if (citymodel.Equals(null)) return null;
				return JsonConvert.DeserializeObject<List<CityModel>>(citymodel);
			}
			set
			{
				AppSettings.AddOrUpdateValue(CitiesKey, JsonConvert.SerializeObject(value));
			}
		}

		private const string CitySelectKey = "CitySelect";
		public static CityModel SaveSettingsCitySelect
		{
			get
			{
				var citymodel = AppSettings.GetValueOrDefault(CitySelectKey, "");
				if (citymodel.Equals(null)) return null;
				return JsonConvert.DeserializeObject<CityModel>(citymodel);
			}
			set
			{
				AppSettings.AddOrUpdateValue(CitySelectKey, JsonConvert.SerializeObject(value));
				Mvx.Resolve<IDataService>().RaiseSaveCityChanged();
			}
		}

		private const string SettingsBanksKey = "SettingsBanks";
		public static List<BankModel> SaveSettingsBanks
		{
			get
			{
				string json = AppSettings.GetValueOrDefault(SettingsBanksKey, "");
				if (json.Equals(null)) return null;
				return JsonConvert.DeserializeObject<List<BankModel>>(json);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsBanksKey, JsonConvert.SerializeObject(value));
			}
		}

		private const string SortParamBanksKey = "SortParamBanks";
		public static EExchangeAction SaveParamSortBanks
		{
			get
			{
				var SortParam = AppSettings.GetValueOrDefault<EExchangeAction>(SortParamBanksKey, EExchangeAction.EURBuy);
				if (SortParam.Equals(null)) return EExchangeAction.EURBuy;
				return SortParam;
			}
			set
			{
				AppSettings.AddOrUpdateValue(SortParamBanksKey, value);
			}
		}
	}
}
