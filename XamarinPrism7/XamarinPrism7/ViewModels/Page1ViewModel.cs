using System.Threading.Tasks;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace XamarinPrism7.ViewModels
{
    public class Page1ViewModel : BaseViewModel
    {
        public DelegateCommand BackCommand { get; }

        public Page1ViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Page 1";

            BackCommand = new DelegateCommand(BackCommandExecuted);

            LoadPage();
        }

        private async Task LoadPage()
        {
            //UserDialogs.Instance.Progress(new ProgressDialogConfig { AutoShow = true, CancelText = "Cancel", Title = "Progress", MaskType = MaskType.Black });

            

            //Task.Delay(5000);

            //UserDialogs.Instance.HideLoading();


            //UserDialogs.Instance.ShowLoading("sdsa");

            //Task y = Task.Run(() =>
            //{
            //    for (int x = 0; x < 10000; x++) Debug.WriteLine("int x = " + x);
            //});
            //y.Wait();
            //UserDialogs.Instance.HideLoading();

            using (UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
            {
                await Task.Delay(2000);
            }
        }

        private async void BackCommandExecuted()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
