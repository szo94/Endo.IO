using System;
using System.Collections.Generic;

namespace Endo.IO.Data
{
    internal abstract class DatabaseConnection<T> where T : DatabaseConnection<T>
    {
        // generic singleton
        private static readonly Lazy<T> instance = new Lazy<T>(Activator.CreateInstance<T>);
        public static T Instance => instance.Value;

        // check that username exists in database
        public abstract bool UserExists(string userName);

        // check that username/password combo exists in database
        public abstract bool VerifyLoginCredentials(string userName, string password);

        // fetch User and BasalRates objects from database and return UserProfile object
        public abstract UserProfile GetUserProfile(string userName);

        // instantiate User and BasalRates objects and write to database
        public abstract void AddUser(string userName, string firstName, List<decimal?> basalRates,
            int? targetBg, int? highB, int? lowBg);
    }
}