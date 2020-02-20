using System;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;

namespace XamarinPrism7.ViewModels
{    
    public class MainPageViewModel : BaseViewModel
    {
        public DelegateCommand BackCommand { get; }
        public DelegateCommand ToastCommand { get; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page 2";

            BackCommand = new DelegateCommand(BackCommandExecuted);
            ToastCommand = new DelegateCommand(ToastCommandExecuted);
        }        

        private async void BackCommandExecuted()
        {
            await NavigationService.NavigateAsync("Test");
        }

        private async void ToastCommandExecuted()
        {
            await NavigationService.NavigateAsync("Page1");
        }
    }
}
