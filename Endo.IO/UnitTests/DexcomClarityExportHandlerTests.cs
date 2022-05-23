using Endo.IO.Data;
using NUnit.Framework;
using System;
using System.IO;

namespace Endo.IO.Testing
{
    [TestFixture]
    public class DexcomClarityExportHandlerTests
    {
        [Test]
        public void ReadFileOnProperlyFormattedFileReturnsListOfEvents()
        {
            ILogHandler logHandler = new DexcomClarityExportHandler(Path.Combine(
                Environment.CurrentDirectory,
                @"Endo.IO\Resources\",
                "SampleClarityExport_Cleaned.csv"));
            Assert.IsNotEmpty(logHandler.GetLog().Events);
        }

        [Test]
        public void ReadFileOnImproperlyFormattedFileThrowsCsvHelperReaderException()
        {
            ILogHandler logHandler = new DexcomClarityExportHandler(Path.Combine(
                Environment.CurrentDirectory,
                @"Endo.IO\Resources\",
                "SampleClarityExport_Uncleaned.csv"));
            Assert.Throws<CsvHelper.ReaderException>(() => logHandler.GetLog());
        }
    }
}
