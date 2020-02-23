using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinPrism7.Helper;
using XamarinPrism7.Interfaces;

namespace XamarinPrism7.Services
{
    public class FirebaseDataStore : IDataStore        
    {
        private readonly FirebaseClient _firebase;

        public FirebaseDataStore()
        {
            FirebaseOptions options = new FirebaseOptions()
            {
                AuthTokenAsyncFactory = async () => await new FirebaseAuthService().GetFirebaseAuthToken()
            };
            _firebase = new FirebaseClient(Config.FirebaseUrl, options);
        }

        public async Task<IEnumerable<T>> GetItemsAsync<T>()
        {
            try
            {
                var firebaseObjects = await _firebase
                                                .Child(typeof(T).Name)
                                                .OnceAsync<T>();

                return firebaseObjects
                    .Select(x => x.Object);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> AddItemAsync<T>(T item)
        {
            try
            {
                await _firebase
                    .Child(typeof(T).Name)
                    .PostAsync(item);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //public async Task<bool> UpdateItemAsync<T>(string id, T item)
        //{
        //    try
        //    {
        //        await _query
        //            .Child(id)
        //            .PutAsync(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //public async Task<bool> DeleteItemAsync<T>(string id)
        //{
        //    try
        //    {
        //        await _query
        //            .Child(id)
        //            .DeleteAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //public async Task<T> GetItemAsync<T>(string id)
        //{
        //    try
        //    {
        //        return await _query
        //            .Child(id)
        //            .OnceSingleAsync<T>();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        
    }
}
