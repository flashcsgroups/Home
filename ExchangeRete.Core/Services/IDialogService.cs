using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExchangeRete.Core
{
	public interface IDialogService
	{
		Task<string> CreateReport(CancellationToken cancelToken, BankModel bank);

		void ShowCBError();

		void ShowServiceError();

		void ShowMessage(string message);

		void ShowTitledMessage(string title, string body);

		Task<bool> Confirm(string title, string question, string yesString, string noString, CancellationToken cancelToken);

		Task<bool> Confirm(string title, string question, string yesString, string noString, bool landscape, CancellationToken cancelToken);

		//Task<NewVersionDialogResult> NewVersionDialog(string changelogHtmlContent);

		Task<bool> WhatsNewDialog(string changelogHtmlContent);

		//Task<bool> ShowInternalAd(InternalAdModel internalAd);

	}
}
