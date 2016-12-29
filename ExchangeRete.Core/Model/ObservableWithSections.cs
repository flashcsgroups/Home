using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ExchangeRete.Core
{
	public static class SectionedListExtensions
	{
		//	public static List<BankModel> SortBanksByExchangeAction(this List<BankModel> banks, EExchangeAction mode)
		//	{

		//		// 			Тут сортировка
		//		//for (int j = 0; j < banks.Count; j++)
		//		//{
		//		//	for (int i = 0; i < banks.Count; i++)
		//		//	{
		//		//		if (Convert.ToDouble(banks[i].EURBuy) > Convert.ToDouble(banks[j].EURBuy))
		//		//		{
		//		//			bank = _banks[i];
		//		//			banks[i] = banks[j];
		//		//			banks[j] = bank;
		//		//		}

		//		//	}
		//		//}

		//		BankModel bank = new BankModel();
		//		switch (mode)
		//		{
		//			case 0:
		//				for (int j = 0; j < banks.Count; j++)
		//				{
		//					for (int i = 0; i < banks.Count; i++)
		//					{
		//						if (Convert.ToDouble(banks[i].USDBuy) > Convert.ToDouble(banks[j].USDBuy))
		//						{
		//							bank = banks[i];
		//							banks[i] = banks[j];
		//							banks[j] = bank;
		//						}

		//					}
		//				}
		//				break;
		//			case EExchangeAction.EURSell:
		//				for (int j = 0; j < banks.Count; j++)
		//				{
		//					for (int i = 0; i < banks.Count; i++)
		//					{
		//						if (Convert.ToDouble(banks[i].EURSell) > Convert.ToDouble(banks[j].EURSell))
		//						{
		//							bank = banks[i];
		//							banks[i] = banks[j];
		//							banks[j] = bank;
		//						}

		//					}
		//				}
		//				break;
		//			case EExchangeAction.USDBuy:
		//				for (int j = 0; j < banks.Count; j++)
		//				{
		//					for (int i = 0; i < banks.Count; i++)
		//					{
		//						if (Convert.ToDouble(banks[i].USDBuy) > Convert.ToDouble(banks[j].USDBuy))
		//						{
		//							bank = banks[i];
		//							banks[i] = banks[j];
		//							banks[j] = bank;
		//						}

		//					}
		//				}
		//				break;
		//			case EExchangeAction.USDSell:
		//				for (int j = 0; j < banks.Count; j++)
		//				{
		//					for (int i = 0; i < banks.Count; i++)
		//					{
		//						if (Convert.ToDouble(banks[i].USDSell) > Convert.ToDouble(banks[j].USDSell))
		//						{
		//							bank = banks[i];
		//							banks[i] = banks[j];
		//							banks[j] = bank;
		//						}

		//					}
		//				}
		//				break;
		//			default:
		//				break;
		//		}
		//		return banks;
		//	}
		//}
		//public class SectionedListExtensions
		//{ 
		//		public static SectionedList<BankModel> SortBanksByExchangeAction(this SectionedList<BankModel> banks, EExchangeAction mode)
		//		{
		//			if (banks == null || banks.Sections.Count == 0)
		//			{
		//				return banks;
		//			}
		//			var tempBanks = new SectionedList<BankModel>();
		//			if (mode == EExchangeAction.Sell)
		//			{
		//				if (Settings.SelectedCurrency.CharCode == "USD")
		//				{
		//					foreach (var section in banks.Sections)
		//					{
		//						var orderedBanks = section.Items.OrderBy(x => x.USDSell).ToList<BankModel>();
		//						var newSection = new SectionedListSection<BankModel>()
		//						{
		//							Name = section.Name,
		//							Items = orderedBanks
		//						};
		//						tempBanks.Sections.Add(newSection);
		//					}
		//				}
		//				else {
		//					foreach (var section in banks.Sections)
		//					{
		//						var orderedBanks = section.Items.OrderBy(x => x.EURSell).ToList<BankModel>();
		//						var newSection = new SectionedListSection<BankModel>()
		//						{
		//							Name = section.Name,
		//							Items = orderedBanks
		//						};
		//						tempBanks.Sections.Add(newSection);
		//					}
		//				}
		//				Settings.SelectedExchangeAction = EExchangeAction.Sell;
		//			}
		//			else {
		//				if (Settings.SelectedCurrency.CharCode == "USD")
		//				{
		//					foreach (var section in banks.Sections)
		//					{
		//						var orderedBanks = section.Items.OrderByDescending(x => x.USDBuy).ToList<BankModel>();
		//						var newSection = new SectionedListSection<BankModel>()
		//						{
		//							Name = section.Name,
		//							Items = orderedBanks
		//						};
		//						tempBanks.Sections.Add(newSection);
		//					}
		//				}
		//				else {
		//					foreach (var section in banks.Sections)
		//					{
		//						var orderedBanks = section.Items.OrderByDescending(x => x.EURBuy).ToList<BankModel>();
		//						var newSection = new SectionedListSection<BankModel>()
		//						{
		//							Name = section.Name,
		//							Items = orderedBanks
		//						};
		//						tempBanks.Sections.Add(newSection);
		//					}
		//				}
		//				Settings.SelectedExchangeAction = EExchangeAction.Buy;
		//			}
		//			Settings.Bests.Clear();
		//			foreach (var item in tempBanks.Sections[0].Items.Take(5))
		//			{
		//				Settings.Bests.Add(item.BankId);
		//			}
		//			Settings.BanksCache[Settings.SelectedCity.Id.ToString()].Object = tempBanks;
		//			return tempBanks;
		//		}
		//	}
		//}
		public interface ICustomCloneable<T>
		{
			T Clone();
		}

		public class SectionedListSection<T1> : ICustomCloneable<SectionedListSection<T1>>
		{
			public SectionedListSection<T1> Clone()
			{
				SectionedListSection<T1> clone = new SectionedListSection<T1>();
				clone.Name = this.Name;
				foreach (var item in this.Items)
				{
					clone.Items.Add(item);
				}
				return clone;
			}

			public string Name { get; set; }

			public List<T1> Items { get; set; }

			public SectionedListSection()
			{
				Items = new List<T1>();
			}
		}

		[JsonObject("SectionedList<T>")]
		public class SectionedList<T> : List<T>, ICustomCloneable<SectionedList<T>> where T : ICustomCloneable<T>
		{

			public SectionedList<T> Clone()
			{
				var clone = new SectionedList<T>();
				clone.Sections = this.Sections;
				return clone;
			}


			public List<SectionedListSection<T>> Sections { get; set; }

			public SectionedList()
			{
				Sections = new List<SectionedListSection<T>>();
			}

			public SectionedList(List<T> origList, Func<List<T>, List<SectionedListSection<T>>> buildSections) : this()
			{
				Sections.AddRange(buildSections(origList));
				this.AddRange(origList);
			}
		}
	}
}