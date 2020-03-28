using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;

namespace MipaCompilerTests
{
    [TestClass]
    public class ScannerTests
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program1.txt")]
        public void ScannerConstructorWorks()
        {
            string filename = "program1.txt";
            Scanner scanner = new Scanner(filename);
            Assert.IsNotNull(scanner);
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program1.txt")]
        public void ScanNextTokenWorks1()
        {
            string filename = "program1.txt";
            Scanner scanner = new Scanner(filename);
            // define array that contains list of tokens that should be extracted

            // perform the scan and checking
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program2.txt")]
        public void ScanNextTokenWorks2()
        {
            string filename = "program1.txt";
            Scanner scanner = new Scanner(filename);
            // define array that contains list of tokens that should be extracted

            // perform the scan and checking
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program3.txt")]
        public void ScanNextTokenWorks3()
        {
            string filename = "program1.txt";
            Scanner scanner = new Scanner(filename);
            // define array that contains list of tokens that should be extracted

            // perform the scan and checking
        }

    }
}
