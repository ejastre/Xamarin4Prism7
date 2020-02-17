using Acr.UserDialogs;
using System;
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

        public ICommand SalvarPedidoCmd { get; set; }

        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public TestViewModel()
        {
            SalvarPedidoCmd = new Command(async () => await SalvarPedido());

            Person = new Person
            {
                PersonId = 5,
                Name = "Juvenal Antena"
            };
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
                        UserDialogs.Instance.Toast("Save Successful!");
                }                
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast("Failed to Save!");
            }
        }

    }
}
