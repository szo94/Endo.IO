using NUnit.Framework;
using System;
using System.IO;

namespace Endo.IO.Testing
{
    [TestFixture]
    public class ClarityExportReaderTests
    {
        [Test]
        public void ReadFileOnProperlyFormattedFileReturnsListOfEvents()
        {
            ClarityExportReader reader = new ClarityExportReader(Path.Combine(
                Environment.CurrentDirectory,
                @"Endo.IO\Resources\",
                "SampleClarityExport_Cleaned.csv"));
            Assert.IsNotEmpty(reader.ReadFile());
        }

        [Test]
        public void ReadFileOnImproperlyFormattedFileThrowsCsvHelperReaderException()
        {
            ClarityExportReader reader = new ClarityExportReader(Path.Combine(
                Environment.CurrentDirectory,
                @"Endo.IO\Resources\",
                "SampleClarityExport_Uncleaned.csv"));
            Assert.Throws<CsvHelper.ReaderException>(() => reader.ReadFile());
        }
    }
}
