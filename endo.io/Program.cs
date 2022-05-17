using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using endo.io.Data;
using endo.io.Testing;
using static endo.io.Constants.Constants;

namespace endo.io
{
    internal class Program
    {
        // get reference to database object (singleton)
        static LinqToSqlDatabase db = LinqToSqlDatabase.Instance;

        [STAThread]
        private static void Main()
        {
            // get username
            Console.WriteLine("Welcome to Endo.IO!\n");
            Console.Write("Enter username: ");
            string userName = Console.ReadLine();
            while (!db.UserExists(userName))
            {
                Console.WriteLine("User not found.\nPlease enter a valid username: ");
                userName = Console.ReadLine();
            }

            // get password
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            while (!db.VerifyLoginCredentials(userName, password))
            {
                Console.WriteLine("Password incorrect.\nPlease try again: ");
                userName = Console.ReadLine();
            }

            // fetch user profile from database
            UserProfile profile = db.GetUserProfile(userName);

            // open file explorer for user to select input file
            string filePath = GetInputFilePath();

            // read clarity export
            ClarityExportReader reader = new ClarityExportReader(filePath);
            List<ClarityEvent> eventLog = null;
            try
            {
                eventLog = reader.ReadFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex is IOException ? "Failed to read file" : $"Failed to copy events: {ex.GetType()}");
            }

            if (eventLog != null)
            {
                // analyze event data
                LogAnalyzer analyzer = new LogAnalyzer(profile, eventLog);

                // print results to console
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

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();        
        }

        private static string GetInputFilePath()
        {
            string filePath = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Clarity Export";
            ofd.Filter = "CSV Files |*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
            }
            return filePath;
        }
        
        private static void PrintRow<T>(string rowTitle, IEnumerable<T> rowData, Func<T, string> format)
        {
            Console.Write($"{rowTitle,-13}");
            foreach (var v in rowData)
                Console.Write(format(v));
            Console.WriteLine();
        }
    }
}