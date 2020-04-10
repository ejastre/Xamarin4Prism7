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
using XamarinPrism7.Interfaces;
using XamarinPrism7.Models;
using XamarinPrism7.Services;

namespace XamarinPrism7.ViewModels
{
    public class TestViewModel : BaseViewModel
    {
        private Person _person;
        public Person Person
        {
            get { return _person; }
            set { SetProperty(ref _person, value); }
        }

        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { SetProperty(ref _persons, value); }
        }

        private ObservableCollection<Person> _selectedPersons;
        public ObservableCollection<Person> SelectedPersons
        {
            get { return _selectedPersons; }
            set { SetProperty(ref _selectedPersons, value); }
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

        private readonly IDataStore firebase = new FirebaseDataStore();

        public TestViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
            Title = "Test Page";

            //Task.Run(async () => await InitializePage());            
            //InitializePage();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        private async Task InitializePage()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Loading..."))
                {
                    Person = new Person();
                    Persons = new ObservableCollection<Person>();
                    
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
            //await NavigationService.NavigateAsync("Page1");
        }

        private async Task RefreshDataAsync()
        {
            using (UserDialogs.Instance.Loading("Loading..."))
            {
                IsRefreshing = false;

                var list = await firebase.GetItemsAsync<Person>();

                if (list == null)
                    UserDialogs.Instance.Toast(new ToastConfig("Failed to load data!"));
                else
                    Persons = new ObservableCollection<Person>(list.OrderBy(x => x.Name));

                //var list = await firebase.GetAll<Person>();

                //var persons = list.Select(item => new Person
                //                      {
                //    Key = item.Key,
                //                          Name = item.Object.Name
                //                      }).ToList();
                //if (persons == null)
                //    UserDialogs.Instance.Toast(new ToastConfig("Failed to load data!"));
                //else
                //    Persons = new ObservableCollection<Person>(persons.OrderBy(x => x.Name));                
            }
        }

        private async Task SalvarPedido()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Adding..."))
                {
                    var result = await firebase.AddItemAsync(Person);

                    if (!result)
                        UserDialogs.Instance.Toast("Person exists!");
                    else
                    {
                        UserDialogs.Instance.Toast("Person Saved Successful!");
                        await RefreshDataAsync();
                    }                               
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
                //using (UserDialogs.Instance.Loading("Updating..."))
                //{
                //    await firebase.UpdateObject(Person, Person.Key);
                //    await RefreshDataAsync();
                //}
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
                    //var result = await firebaseHelper.DeletePerson(Person.PersonId);

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
