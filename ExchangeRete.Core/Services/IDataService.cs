using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRete.Core
{
	public interface IDataService
	{
		Task<List<CityModel>> GetCities();

		Task<List<BankModel>> GetBanks(int cityId);

		event EventHandler OnSaveCityChanged;
		void RaiseSaveCityChanged();
	}
}
