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

		private const string CitySelectIDKey = "CitySelectID";
		public static int SaveSettingsCitySelectID
		{
			get
			{
				return AppSettings.GetValueOrDefault(CitySelectIDKey, 0);
			}
			set
			{
				AppSettings.AddOrUpdateValue(CitySelectIDKey, value);
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
		private const string SelectedCityKey = "SelectedCity";
		public static bool SaveSelectedCity
		{
			get
			{
				return AppSettings.GetValueOrDefault(SelectedCityKey, false);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SelectedCityKey, value);
			}
		}
		//public class CacheObject<T>
		//{
		//	//public T Object{ get; set; } = default(T);
		//	private T _object = default(T);

		//	public T Object
		//	{
		//		get { return _object; }
		//		set { _object = value; }
		//	}

		//	public DateTime DateAdded { get; set; }

		//	public TimeSpan ValidPeriod { get; set; }

		//	public bool IsValid { get { return DateAdded.Add(ValidPeriod) >= DateTime.Now; } }
		//}

		//public class DataCache<T>
		//{
		//	//private string CachePefix{ get; set; } = "cache{0}";
		//	private string _cachePrefix = "cache{0}";

		//	public string CachePefix
		//	{
		//		get { return _cachePrefix; }
		//		set { _cachePrefix = value; }
		//	}
		//	private const string ExchangeActionKey = "SelectedAction";

			//public static EExchangeAction SelectedExchangeAction
			//{
			//	get
			//	{
			//		return AppSettings.GetValueOrDefault(ExchangeActionKey, EExchangeAction.Buy);
			//	}
			//	set
			//	{
			//		AppSettings.AddOrUpdateValue(ExchangeActionKey, value);
			//	}
			//}

			//private TimeSpan ValidPeriod{ get; set; } = TimeSpan.FromHours(1);
		//	private TimeSpan _validPeriod = TimeSpan.FromHours(1);

		//	public TimeSpan ValidPeriod
		//	{
		//		get { return _validPeriod; }
		//		set { _validPeriod = value; }
		//	}

		//	public DataCache(string cachePrefix, TimeSpan validPeriod)
		//	{
		//		CachePefix = cachePrefix;
		//		ValidPeriod = validPeriod;
		//	}

		//	public CacheObject<T> this[string Id]
		//	{
		//		get
		//		{
		//			try
		//			{
		//				var cacheStr = AppSettings.GetValueOrDefault<string>(string.Format(CachePefix, Id));

		//				if (String.IsNullOrWhiteSpace(cacheStr))
		//					return new CacheObject<T>()
		//					{
		//						DateAdded = DateTime.Now.AddDays(-10),
		//						ValidPeriod = TimeSpan.FromSeconds(0)
		//					};

		//				var res = JsonConvert.DeserializeObject<CacheObject<T>>(cacheStr);
		//				return res;
		//			}
		//			catch (JsonException ex)
		//			{
		//				return null;
		//			}
		//			catch (Exception ex)
		//			{
		//				throw;
		//			}
		//		}
		//		set
		//		{
		//			value.DateAdded = DateTime.Now;
		//			value.ValidPeriod = ValidPeriod;
		//			AppSettings.AddOrUpdateValue(string.Format(CachePefix, Id), JsonConvert.SerializeObject(value));
		//		}
		//	}
		//}

		//private static DataCache<List<CityModel>> citiesCache = new DataCache<List<CityModel>>("cache_cities_{0}", TimeSpan.FromDays(1));

		//public static DataCache<List<CityModel>> CitiesCache
		//{
		//	get
		//	{
		//		return citiesCache;
		//	}
		//}

		//private const string SelectedCityKey = "SelectedCity";

		//public static CityModel SelectedCity
		//{
		//	get
		//	{
		//		var serialized = AppSettings.GetValueOrDefault(SelectedCityKey, "");
		//		if (string.IsNullOrWhiteSpace(serialized))
		//		{
		//			return new CityModel() { Id = 7701, Name = "Москва" };
		//		}
		//		//var deserialized = JsonConvert.DeserializeObject<CityModel>(serialized);
		//		return JsonConvert.DeserializeObject<CityModel>(serialized);
		//	}
		//	set
		//	{
		//		AppSettings.AddOrUpdateValue(SelectedCityKey, JsonConvert.SerializeObject(value));
		//	}
		//}

	}
}
