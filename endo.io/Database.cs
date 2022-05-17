using System;
using System.Collections.Generic;

namespace endo.io.Data
{
    internal abstract class Database<T> where T : Database<T>
    {
        private static readonly Lazy<T> instance = new Lazy<T>(Activator.CreateInstance<T>);
        public static T Instance => instance.Value;

        public abstract bool VerifyLoginCredentials(string userName, string password);

        public abstract UserProfile GetUserProfile(string userName);

        public abstract void AddUser(string userName, string firstName, List<decimal?> basalRates,
            int? targetBg, int? highB, int? lowBg);
    }
}