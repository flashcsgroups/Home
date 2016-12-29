using System;
namespace ExchangeRete.Core
{
	public class IPlatformDependency
	{
		bool ReleaseBuild { get; }

		string UserAgent { get; }

		string Version { get; }

		string AppId { get; }

		string AppInstallerId { get; }

		string Tracker { get; }

		//Platform Platform { get; }

		System.Windows.Input.ICommand ReviewAppCommand { get; }

		System.Windows.Input.ICommand DialBranchCommand { get; }

		double Height { get; }

		double Width { get; }

		bool IsLocationServiceEnabled { get; set; }

		//void ResolveGeoLocation();

		//GeoPositionModel CurrentPosition { get; set; }

		//void ShowGeoWarning();

		//void ShowCBFailure();

		//void ShowConnectionFailure();

		bool IsConnected { get; }

		//void ShowFailedSendReport();

		//System.Threading.Tasks.Task MigrateFromNative();

		//void GoToMarket();

		//Task ShowInternalAd(InternalAdModel internalAd);

		//void OpenUrlWithBrowser(String url);

		//#region Google analytics

		//void SendCity(string cityId);

		//void SendMainPage();

		//void SendBank(string bankId);

		//void SendSharingVK();

		//void SendSharingFacebook();

		//void SendReviewing();

		//void SendCB();

		//void SendNews();

		//void SendInAppPurchase();

		//void SendInternalAdView();

		//#endregion

		//#region iap and sharing

		//Task<bool> ShareVK(string vkAppId);

		//Task<bool> ShareFB(string fbClientId);

		//Task<int> RestoreIAP();

		//Task<bool> BuyIAP();

		//#endregion
	}
}
