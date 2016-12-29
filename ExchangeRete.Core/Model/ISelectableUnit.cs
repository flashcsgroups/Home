using System;
using MvvmCross.Core.ViewModels;

namespace ExchangeRete.Core
{
	public interface ISelectableUnit
	{
		IMvxCommand SelectCommand { get; set; }
	}
}
