using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Endo.IO.Data;
using static Endo.IO.Constants.Constants;

namespace Endo.IO
{
    internal class Program
    {
        // get reference to database connection
        static readonly LinqToSqlDatabaseConnection db = LinqToSqlDatabaseConnection.Instance;

        [STAThread]
        private static void Main()
        {
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

            // get file path for input file
            string filePath = GetFilePath();

            // attempt to read and process file
            ClarityExportReader reader = new ClarityExportReader(filePath);
            try
            {
                // read file
                List<ClarityEvent> eventLog = reader.ReadFile();

                // analyze event data
                LogAnalyzer analyzer = new LogAnalyzer(profile, eventLog);

                // print results
                PrintResultsGraph(profile, analyzer);
            }
            catch (Exception ex)
            {
                Console.WriteLine((ex is IOException ? "Failed to read file " : "Failed to copy events ")
                                  + ex.GetType());
            }

            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();        
        }

        // open file explore for user to select input file, return file path
        private static string GetFilePath()
        {
            string filePath = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Clarity Export";
            ofd.InitialDirectory =
                Path.Combine(Assembly.GetExecutingAssembly().Location, @"..\..\..\Resources");
            ofd.Filter = "CSV Files |*.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
            }
            return filePath;
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