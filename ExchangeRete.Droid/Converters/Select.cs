using System;
using MvvmCross.Plugins.Visibility;
using MvvmCross.Platform.UI;

namespace ExchangeRete.Droid
{
	public class SelectValueConverter : MvxBaseVisibilityValueConverter
	{
		protected override MvxVisibility Convert(object value, object parameter, System.Globalization.CultureInfo culture)
		{
			if ((bool)(value))
				return MvxVisibility.Visible;
			else
				return MvxVisibility.Collapsed;
		}
	}
}
