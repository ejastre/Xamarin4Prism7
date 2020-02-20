using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinPrism7.Models;

namespace XamarinPrism7.Helper
{
    public class FirebaseHelper
    {
        private readonly FirebaseClient firebase = new FirebaseClient("https://xamarin4prism7.firebaseio.com/");

        public async Task<List<Person>> GetAllPersons()
        {
            try
            {
                var persons = (await firebase
                                      .Child("Persons")
                                      .OnceAsync<Person>()).Select(item => new Person
                                      {
                                          Name = item.Object.Name,
                                          PersonId = item.Object.PersonId
                                      }).ToList();
                return persons;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public async Task<FirebaseObject<Person>> AddPerson(int personId, string name)
        {
            var result = (await firebase.Child("Persons")
                                        .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId)
                                                             .FirstOrDefault();

            return string.IsNullOrEmpty(result?.Key)
                ? await firebase
                  .Child("Persons")
                  .PostAsync(new Person() { PersonId = personId, Name = name })
                : null;
        }

        public async Task<Person> GetPerson(int personId)
        {
            var allPersons = await GetAllPersons();
            await firebase
              .Child("Persons")
              .OnceAsync<Person>();
            return allPersons.Where(a => a.PersonId == personId).FirstOrDefault();
        }

        public async Task UpdatePerson(int personId, string name)
        {
            var toUpdatePerson = (await firebase
              .Child("Persons")
              .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId).FirstOrDefault();

            await firebase
              .Child("Persons")
              .Child(toUpdatePerson.Key)
              .PutAsync(new Person() { PersonId = personId, Name = name });
        }

        public async Task<bool> DeletePerson(int personId)
        {
            try
            {
                var result = (await firebase
                    .Child("Persons")
                    .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId).FirstOrDefault();

                if (result == null) return false;

                await firebase
                        .Child("Persons")
                        .Child(result.Key)
                        .DeleteAsync();

                return true;
            }
            catch 
            {
                return false;
            }            
        }
    }
}
