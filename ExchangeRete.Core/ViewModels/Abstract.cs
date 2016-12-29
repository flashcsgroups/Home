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
	//public class BaseNavigationViewModel: BaseViewModel
	//{
	//	private const string ParameterName = "parameter";
	//	protected virtual void ShowViewModel<TViewModel>(object parameter)
	//		where TViewModel : IMvxViewModel
	//	{
	//		var text = JsonConvert.SerializeObject(parameter);
	//		base.ShowViewModel<TViewModel>(new Dictionary<string, string>() {
	//		{ ParameterName, text }
	//	});
	//	}
	//}
}
