using NUnit.Framework;
using Endo.IO;
using System;
using System.IO;

namespace endo.io.Testing
{
    [TestFixture]
    public class ClarityExportReaderTests
    {
        [Test]
        public void ReadFileOnProperlyFormattedFileReturnsListOfEvents()
        {
            ClarityExportReader reader = new ClarityExportReader(Path.Combine(
                Environment.CurrentDirectory,
                @"endo.io\UnitTests\",
                "SampleClarityExport_Cleaned.csv"));
            Assert.IsNotEmpty(reader.ReadFile());
        }

        [Test]
        public void ReadFileOnImproperlyFormattedFileThrowsCsvHelperReaderException()
        {
            ClarityExportReader reader = new ClarityExportReader(Path.Combine(
                Environment.CurrentDirectory,
                @"endo.io\UnitTests\",
                "SampleClarityExport_Uncleaned.csv"));
            Assert.Throws<CsvHelper.ReaderException>(() => reader.ReadFile());
        }
    }
}
