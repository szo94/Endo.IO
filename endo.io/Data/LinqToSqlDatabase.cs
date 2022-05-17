using System.Collections.Generic;
using System.Linq;

namespace endo.io.Data
{
    internal class LinqToSqlDatabase : Database<LinqToSqlDatabase>
    {
        private DataClasses1DataContext db = new DataClasses1DataContext();

        public override bool UserExists(string userName)
        {
            return db.Users.Any(u => u.UserName.ToLower() == userName.ToLower());
        }

        public override bool VerifyLoginCredentials(string userName, string password)
        {
            return db.Users.Any(u => u.UserName.ToLower() == userName.ToLower() &&
                                     u.Password == password);
        }

        public override UserProfile GetUserProfile(string userName)
        {
            // fetch user
            var user = db.Users
                .Where(u => u.UserName == userName)
                .Select(u => new { u.UserName, u.FirstName, u.TargetBg, u.HighBg, u.LowBg })
                .First();

            // fetch basal rates
            var basalRates = db.BasalRates
                .Where(u => u.UserName == userName)
                .OrderBy(u => u.Hour)
                .Select(u => u.Rate)
                .ToList();

            return new UserProfile(user.UserName, user.FirstName, basalRates, user.TargetBg,
                user.HighBg, user.LowBg);
        }

        public override void AddUser(string userName, string firstName, List<decimal?> basalRates,
            int? targetBg, int? highB, int? lowBg)
        {
            // queue new User
            db.Users.InsertOnSubmit(new User()
            {
                UserName = userName,
                FirstName = firstName,
                TargetBg = targetBg,
                HighBg = highB,
                LowBg = lowBg
            });

            // queue new BasalRates
            for (int i = 0; i < 24; i++)
            {
                db.BasalRates.InsertOnSubmit(new BasalRate()
                {
                    UserName = userName,
                    Hour = i,
                    Rate = basalRates[i]
                });

            }

            // submit
            db.SubmitChanges();
        }
    }
}