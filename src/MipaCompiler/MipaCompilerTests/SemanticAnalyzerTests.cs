using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;

namespace MipaCompilerTests
{
    [TestClass]
    public class SemanticAnalyzerTests
    {

        [TestMethod]
        [DeploymentItem("SampleFiles\\program1.txt")]
        public void CheckConstraintsWorksWithValidProgram1()
        {
            string filename = "program1.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsFalse(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 0);
        }

    }
}
