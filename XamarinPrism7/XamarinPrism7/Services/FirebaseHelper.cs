﻿using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XamarinPrism7.Models;
using System;
using XamarinPrism7.Interfaces;
using XamarinPrism7.Services;

namespace XamarinPrism7.Helper
{
    public class FirebaseHelper
    {
        // https://github.com/step-up-labs/firebase-database-dotnet

        private readonly IDataStore dataStore;

        private readonly FirebaseClient firebase = 
            new FirebaseClient("https://xamarin4prism7.firebaseio.com/");

        

        public async Task<List<FirebaseObject<T>>> GetAll<T>()
        {
            try
            {
                var list = (await firebase.Child(typeof(T).Name)
                                          .OnceAsync<T>()).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FirebaseObject<T>> InsertObject<T>(T obj, string id, string key)
        {
            try
            {
                // Check if exists
                var result = (await firebase
                                    .Child(typeof(T).Name)
                                    .OrderBy("Name")
                                    .StartAt(key)
                                    .LimitToFirst(1)
                                    .OnceAsync<T>()).FirstOrDefault();

                return result == null
                    ? await firebase
                                  .Child(typeof(T).Name)
                                  .PostAsync(obj)
                    : null;
            }
            catch (Exception ex)
            {
                return null;
            }            
        }

        public async Task<bool> InsertObject2<T>(T item)
        {
            try
            {
                var data = new FirebaseDataStore();
                var result = await data.AddItemAsync<T>(item);
                return result;
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        //public async Task<Person> GetPerson(int personId)
        //{
        //    var allPerson = await GetAllPerson();
        //    await firebase
        //      .Child("Person")
        //      .OnceAsync<Person>();
        //    return allPerson.Where(a => a.PersonId == personId).FirstOrDefault();
        //}

        public async Task UpdateObject<T>(T obj, string key)
        {
            try
            {
                await firebase
                  .Child(typeof(T).Name)
                  .Child(key)
                  .PutAsync(obj);
            }
            catch (Exception ex)
            {
                                
            }            
        }

        public async Task<bool> DeletePerson(int personId)
        {
            try
            {
                //var result = (await firebase
                //    .Child("Person")
                //    .OnceAsync<Person>()).Where(a => a.Object.PersonId == personId).FirstOrDefault();

                //if (result == null) return false;

                //await firebase
                //        .Child("Person")
                //        .Child(result.Key)
                //        .DeleteAsync();

                return true;
            }
            catch 
            {
                return false;
            }            
        }
    }
}
