using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System.Collections.Generic;
using System.Linq;

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

        [TestMethod]
        [DeploymentItem("SampleFiles\\program2.txt")]
        public void CheckConstraintsWorksWithValidProgram2()
        {
            string filename = "program2.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsFalse(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 0);
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program3.txt")]
        public void CheckConstraintsWorksWithValidProgram3()
        {
            string filename = "program3.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsFalse(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 0);
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program4.txt")]
        public void CheckConstraintsWorksWithValidProgram4()
        {
            string filename = "program4.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsFalse(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 0);
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program5.txt")]
        public void CheckConstraintsWorksWithValidProgram5()
        {
            string filename = "program5.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsFalse(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 0);
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program6.txt")]
        public void CheckConstraintsWorksWithValidProgram6()
        {
            string filename = "program6.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsFalse(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 0);
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\types1.txt")]
        public void CheckConstraintsWorksWithValidTypes1()
        {
            string filename = "types1.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsFalse(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 0);
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\InvalidSemantics\\duplicates.txt")]
        public void CheckConstraintsWorksWithDuplicateFunctions()
        {
            string filename = "duplicates.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsTrue(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count != 0);

            List<string> errors = analyzer.GetDetectedErrors();

            Assert.IsTrue(errors.Any(s => s.Contains("SemanticError::Row 10::Column 1")));
            Assert.IsTrue(errors.Any(s => s.Contains("SemanticError::Row 34::Column 1")));
            Assert.IsTrue(errors.Any(s => s.Contains("SemanticError::Row 50::Column 1")));
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\ValidSemantics\\assertion.txt")]
        public void CheckConstraintsWorksWithValidAssert()
        {
            string filename = "assertion.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsFalse(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 0);
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\InvalidSemantics\\assertionFail.txt")]
        public void CheckConstraintsWorksWithInvalidAssert()
        {
            string filename = "assertionFail.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsTrue(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count != 0);

            List<string> errors = analyzer.GetDetectedErrors();

            Assert.IsTrue(errors.Any(s => s.Contains("SemanticError::Row 3::Column 26")));
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\InvalidSemantics\\variableDcl.txt")]
        public void CheckConstraintsWorksWithInvalidVariableDcl()
        {
            string filename = "variableDcl.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsTrue(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 2);

            List<string> errors = analyzer.GetDetectedErrors();

            Assert.IsTrue(errors.Any(s => s.Contains("SemanticError::Row 4::Column 5")));
            Assert.IsTrue(errors.Any(s => s.Contains("SemanticError::Row 4::Column 5")));
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\InvalidSemantics\\arraySize.txt")]
        public void CheckConstraintsWorksWithInvalidArraySizeDcl()
        {
            string filename = "arraySize.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsTrue(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 1);

            List<string> errors = analyzer.GetDetectedErrors();

            Assert.IsTrue(errors.Any(s => s.Contains("SemanticError::Row 3::Column 13")));
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\InvalidSemantics\\returnType.txt")]
        public void CheckConstraintsWorksWithInvalidReturnType()
        {
            string filename = "returnType.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsTrue(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 1);

            List<string> errors = analyzer.GetDetectedErrors();

            Assert.IsTrue(errors.Any(s => s.Contains("SemanticError::Row 10::Column 12")));
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\ValidSemantics\\expressionWithCall.txt")]
        public void CheckConstraintsWorksWithFunctionCallInsideExpression()
        {
            string filename = "expressionWithCall.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            //ast.PrettyPrint();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsFalse(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 0);
        }
    }
}
