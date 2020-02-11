using System;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;

namespace XamarinPrism7.ViewModels
{    
    public class MainPageViewModel : ViewModelBase
    {
        public DelegateCommand BackCommand { get; }
        public DelegateCommand ToastCommand { get; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";

            BackCommand = new DelegateCommand(BackCommandExecuted);
            ToastCommand = new DelegateCommand(ToastCommandExecuted);
        }        

        private async void BackCommandExecuted()
        {
            await NavigationService.NavigateAsync("Page1");
        }

        private void ToastCommandExecuted()
        {
            UserDialogs.Instance.Toast("Toast Test");
        }
    }
}
