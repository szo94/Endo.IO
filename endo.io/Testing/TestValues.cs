using System.IO;
using System.Reflection;

namespace Endo.IO.Testing
{
    internal class TestValues
    {
        public static string UserName = "SOchs";

        public static string FilePath = Path.Combine(Assembly.GetExecutingAssembly().Location,
            @"..\..\..\TestFiles\SampleClarityExport_Cleaned.csv");
    }
}
