using Prism;
using Prism.Ioc;
using XamarinPrism7.ViewModels;
using XamarinPrism7.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinPrism7
{
    public partial class App
    {
        /* 
         * DONE ***
         * XF 4.4.0.99
         * Prism 7
         * Floating Label
         * Toast Message e Loading
         * Firebase Cloud API
         * Prism TwoWay Data Binding
         * Add Delete Button
         * Make CRUD Generic and Test with other Table
         * 
         * MISSING ***
         * Add AppCenter and Log Class
         * 
         * Splash Screen
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/Test");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<Page1, Page1ViewModel>();
            containerRegistry.RegisterForNavigation<Test, TestViewModel>();            
        }
    }
}
