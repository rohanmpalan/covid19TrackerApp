using Covid19Stats.Interface;
using Covid19Stats.Repository;
using Covid19Stats.Services.Data;
using Covid19Stats.Services.General;
using Covid19Stats.View;
using Covid19Stats.ViewModel;
using LightInject;

namespace Covid19Stats
{
    class Bootstrapper
    {
        public static IServiceContainer Container { get; private set; }
        public static void Init()
        {
            Container = new ServiceContainer();
            RegisterRepositories();
            RegisterViewModes();
            RegisterServices();
            ConfigureNavigationService();
        }

        private static void RegisterRepositories()
        {
            Container.Register(typeof(IRepository<>), typeof(Repository<>));
        }

        private static void RegisterServices()
        {
            Container.Register<IWorldDataService, WorldDataService>(new PerRequestLifeTime());
            Container.Register<INavigationService, NavigationService>(new PerContainerLifetime());
            Container.Register<IDialogService, DialogService>(new PerContainerLifetime());
        }

        private static void RegisterViewModes()
        {
            Container.Register<BaseViewModel>(new PerRequestLifeTime());
            Container.Register<HomeViewModel>(new PerRequestLifeTime());
            Container.Register<CountryDetailsViewModel>(new PerRequestLifeTime());
        }

        private static void ConfigureNavigationService()
        {
            INavigationService navigationService = Container.GetInstance<INavigationService>();
            navigationService.Configure(PageKeys.Home, typeof(HomePage));
            navigationService.Configure(PageKeys.CountryDetails, typeof(CountryDetailsPage));
        }
    }

    public class PageKeys
    {
        public static readonly string Home = "HomePage";
        public static readonly string CountryDetails = "CountryDetailsPage";
    }
}
