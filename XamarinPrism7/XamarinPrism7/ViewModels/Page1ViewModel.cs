using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinPrism7.Helper;
using XamarinPrism7.Models;

namespace XamarinPrism7.ViewModels
{
    public class Page1ViewModel : BaseViewModel
    {
        private Car _Car;
        public Car Car
        {
            get { return _Car; }
            set { SetProperty(ref _Car, value); }
        }

        private ObservableCollection<Car> _Cars;
        public ObservableCollection<Car> Cars
        {
            get { return _Cars; }
            set { SetProperty(ref _Cars, value); }
        }

        private ObservableCollection<Car> _selectedCars;
        public ObservableCollection<Car> SelectedCars
        {
            get { return _selectedCars; }
            set { SetProperty(ref _selectedCars, value); }
        }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetProperty(ref isRefreshing, value); }
        }
        public ICommand RefreshCommand => new Command(async () => await RefreshDataAsync());
        public ICommand SalvarPedidoCmd => new Command(async () => await SalvarPedido());
        public ICommand UpdatePedidoCmd => new Command(async () => await UpdatePedido());
        public ICommand GoBackCmd => new Command(async () => await GoBack());
        public ICommand DeleteCmd => new Command(async () => await DeleteOrder());
        public ICommand SelectedCommand => new Command(async () => await SelecteItem());

        private readonly FirebaseHelper firebaseHelper = new FirebaseHelper();

        public Page1ViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Cars";

            //Task.Run(async () => await InitializePage());            
            InitializePage();
        }

        private async Task InitializePage()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Loading..."))
                {
                    Car = new Car
                    {
                        CarId = 5,
                        Name = "Juvenal Antena"
                    };
                    Cars = new ObservableCollection<Car>();

                    //TODO Qdo volta esta carregando?
                    await RefreshDataAsync();
                }
            }
            catch (Exception ex)
            {
                // LOG
            }
        }

        private async Task GoBack()
        {
            try
            {
                await NavigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast("Failed to Save!");
            }
        }

        private async Task SelecteItem()
        {
            await NavigationService.NavigateAsync("Page1");
        }

        private async Task RefreshDataAsync()
        {
            using (UserDialogs.Instance.Loading("Loading..."))
            {
                IsRefreshing = false;

                //var Cars = await firebaseHelper.GetAllCars();

                //if (Cars == null)
                //    UserDialogs.Instance.Toast(new ToastConfig("Failed to load data!"));
                //else
                //    Cars = new ObservableCollection<Car>(Cars.OrderBy(x => x.CarId));
            }


        }

        private async Task SalvarPedido()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Adding..."))
                {
                    //var Car = Car;
                    //var result = await firebaseHelper.AddCar(Car.CarId, Car.Name);

                    //if (string.IsNullOrEmpty(result?.Key))
                    //    UserDialogs.Instance.Toast("User exists!");
                    //else
                    //{
                    //    UserDialogs.Instance.Toast("Save Successful!");
                    //    await RefreshDataAsync();
                    //}
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast("Failed to Save!");
            }
        }

        private async Task UpdatePedido()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Updating..."))
                {
                    //await firebaseHelper.UpdateCar(Car.CarId, Car.Name);
                    await RefreshDataAsync();
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast("Failed to Save!");
            }
        }

        private async Task DeleteOrder()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Deleting..."))
                {
                    //var result = await firebaseHelper.DeleteCar(Car.CarId);

                    //if (!result)
                    //    UserDialogs.Instance.Toast("User not exists!");
                    //else
                    //{
                    //    UserDialogs.Instance.Toast("Delete Successful!");
                    //    await RefreshDataAsync();
                    //}
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast("Failed to Delete!");
            }
        }

    }
}
