using System.Collections.Generic;
using System.Linq;

namespace Endo.IO.Data
{
    internal class LinqToSqlDatabaseConnection : DatabaseConnection<LinqToSqlDatabaseConnection>
    {
        private readonly DataClassesDataContext db = new DataClassesDataContext();

        // check that username exists in database
        public override bool UserExists(string userName)
        {
            return db.Users.Any(u => u.UserName.ToLower() == userName.ToLower());
        }

        // check that username/password combo exists in database
        public override bool VerifyLoginCredentials(string userName, string password)
        {
            return db.Users.Any(u => u.UserName.ToLower() == userName.ToLower() &&
                                     u.Password == password);
        }

        // fetch User and BasalRates objects from database and return UserProfile object
        public override UserProfile GetUserProfile(string userName)
        {
            // fetch User object
            var user = db.Users
                .Where(u => u.UserName == userName)
                .Select(u => new { u.UserName, u.FirstName, u.TargetBg, u.HighBg, u.LowBg })
                .First();

            // fetch BasalRates object
            var basalRates = db.BasalRates
                .Where(u => u.UserName == userName)
                .OrderBy(u => u.Hour)
                .Select(u => u.Rate)
                .ToList();

            // return new UserProfile object
            return new UserProfile(user.UserName, user.FirstName, basalRates, user.TargetBg,
                user.HighBg, user.LowBg);
        }

        // instantiate User and BasalRates objects and write to database
        public override void AddUser(string userName, string firstName, List<decimal?> basalRates,
            int? targetBg, int? highB, int? lowBg)
        {
            // queue new User object for submission
            db.Users.InsertOnSubmit(new User()
            {
                UserName = userName,
                FirstName = firstName,
                TargetBg = targetBg,
                HighBg = highB,
                LowBg = lowBg
            });

            // queue new BasalRates object for submission
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