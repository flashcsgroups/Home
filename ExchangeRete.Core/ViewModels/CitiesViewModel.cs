using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;

namespace ExchangeRete.Core
{
	public class CitiesViewModel : BaseViewModel
	{
		private List<CityModel> _cities = new List<CityModel>();

		public List<CityModel> Cities
		{
			get
			{
				return _cities;
			}
			set
			{
				_cities = value;
				RaisePropertyChanged(() => Cities);
			}
		}
		public override async void Start()
		{
			CitiesLoadSettings();
			await DownloadDataCities();
		}

		public void CitiesLoadSettings()
		{
			if (!ReferenceEquals(null,Settings.SaveSettingsCities))
			Cities = Settings.SaveSettingsCities;
		}
		public CitiesViewModel(IDataService service)
		{
			_dataService = service;
		}


		public async Task DownloadDataCities()
		{
			Cities = new List<CityModel>(await _dataService.GetCities());
		}

		private IMvxCommand _selectCities;
		public IMvxCommand SelectCities
		{
			get
			{
				return new MvxCommand<CityModel>(city =>
				{
					if (!ReferenceEquals(null, Settings.SaveSettingsCitySelect))
					{   //Ищим старый выделенный город и делаем его фолс
						int selectId = Settings.SaveSettingsCitySelect.Id;
						for (int i = 0; i < _cities.Count; i++)
						{
							if (_cities[i].Id == selectId)
							{
								_cities[i].Selected = false;
							}
						}
					}
					//новый выделенный сохраняем
					city.Selected = true;
					Settings.SaveSettingsCitySelect = city;
				});
			}
		}
	}
}
