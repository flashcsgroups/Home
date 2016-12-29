using System;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;

namespace ExchangeRete.Core
{
	public class CityModel: MvxNotifyPropertyChanged //: ICustomCloneable<CityModel>
	{
		[JsonProperty("Id")]
		public int Id { get; set; }
		[JsonProperty("Name")]
		public string Name { get; set; }
		[JsonProperty("TranslitId")]
		public string TranslitId { get; set; }

		//[JsonProperty("Selected")]
		//public bool Selected { get{} set{ RaisePropertyChanged("Selected"); } }
			
		private bool _selected;
		[JsonProperty("Selected")]
		public bool Selected
		{
			get { return _selected; }
			set { _selected = value; RaisePropertyChanged("Selected"); }
		}
	}
}