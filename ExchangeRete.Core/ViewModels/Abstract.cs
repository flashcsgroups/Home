using System;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace ExchangeRete.Core
{
	public class BaseViewModel : MvxViewModel
	{
		protected IDataService _dataService;
	}
}
