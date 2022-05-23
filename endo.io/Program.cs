using System;
using System.Collections.Generic;
using Endo.IO.Data;
using static Endo.IO.Constants.Constants;

namespace Endo.IO
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            // get reference to database connection
            LinqToSqlDatabaseConnection db = LinqToSqlDatabaseConnection.Instance;

            // prompt user for username
            Console.WriteLine("Welcome to Endo.IO!\n");
            Console.Write("Enter username: ");
            string userName = Console.ReadLine();
            while (userName == "" || !db.UserExists(userName))
            {
                Console.Write("User not found.\nPlease enter a valid username: ");
                userName = Console.ReadLine();
            }

            // prompt user for password
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            while (!db.VerifyLoginCredentials(userName, password))
            {
                Console.Write("Password incorrect.\nPlease try again: ");
                password = Console.ReadLine();
            }
            Console.Clear();

            // fetch user profile from database
            UserProfile profile = db.GetUserProfile(userName);

            // attempt to read and process file
            IEventLogGetter logHandler = new DexcomClarityExportReader();
            try
            {
                // get log
                IEventLog eventLog = logHandler.GetLog();

                // analyze event data
                LogAnalyzer analyzer = new LogAnalyzer(profile, eventLog);

                // print results
                PrintResultsGraph(profile, analyzer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to get log: {ex.GetType()}");
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();        
        }
        
        // print results in the form of a graph to console
        private static void PrintResultsGraph(UserProfile profile, LogAnalyzer analyzer)
        {
            PrintRow("", HEADERS, v => $"{v,COL_WIDTH}");
            Console.WriteLine(new string('-', 24 * COL_WIDTH + 13));
            PrintRow("Average", analyzer.AverageByHour, v => $"{v,COL_WIDTH:F1}");
            PrintRow("Variance", analyzer.VarianceByHour, v => $"{v,COL_WIDTH:F1}");
            PrintRow("Basal Rates", profile.BasalRates, v => $"{v,COL_WIDTH:F1}");
            PrintRow("Suggestions", analyzer.BasalSuggestions, v => $"{v,COL_WIDTH:+#.#;-#.#;0}");
            Console.WriteLine();
            Console.WriteLine($"Number of Readings:{analyzer.EventLog.Count,8}");
            Console.WriteLine($"Average BG:{analyzer.AverageBG,16:F}");
            Console.WriteLine($"Time In Range:{analyzer.TimeInRange,13:P1}");
        }

        // print graph row to console specifying row name, data, and cell format
        private static void PrintRow<T>(string rowTitle, IEnumerable<T> rowData, Func<T, string> format)
        {
            Console.Write($"{rowTitle,-13}");
            foreach (var v in rowData)
                Console.Write(format(v));
            Console.WriteLine();
        }
    }
}