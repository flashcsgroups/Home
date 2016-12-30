using ExchangeRete.Core;
using MvvmCross.Platform.IoC;

namespace ExchangeRate.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
			
			RegisterAppStart<TabViewModel>();
        }
    }
}

