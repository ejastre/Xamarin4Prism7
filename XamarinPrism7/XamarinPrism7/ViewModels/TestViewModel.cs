using Acr.UserDialogs;
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
    public class TestViewModel : ViewModelBase
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

        public ICommand SelectedCommand => new Command(Selected);

        private void Selected()
        {
            UserDialogs.Instance.Toast(Person.Name);            
        }

        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public TestViewModel()
        {
            Person = new Person
            {
                PersonId = 5,
                Name = "Juvenal Antena"
            };
            Task.Run(async () => await RefreshDataAsync()).Wait();
        }

        private async Task RefreshDataAsync()
        {
            IsRefreshing = true;

            var persons = await firebaseHelper.GetAllPersons();

            Persons = new ObservableCollection<Person>(persons.OrderBy(x => x.PersonId));

            IsRefreshing = false;
        }

        private async Task SalvarPedido()
        {
            try
            {
                using (UserDialogs.Instance.Loading("Saving...", null, null, true, MaskType.Black))
                {
                    var person = Person;
                    var result = await firebaseHelper.AddPerson(person.PersonId, person.Name);

                    if (string.IsNullOrEmpty(result?.Key))
                        UserDialogs.Instance.Toast("User exists!");
                    else
                    {
                        UserDialogs.Instance.Toast("Save Successful!");
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
                using (UserDialogs.Instance.Loading("Updating...", null, null, true, MaskType.Black))
                {
                    await firebaseHelper.UpdatePerson(Person.PersonId, Person.Name);

                    await RefreshDataAsync();
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast("Failed to Save!");
            }
        }

    }
}
