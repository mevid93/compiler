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
        [DeploymentItem("SampleFiles\\program7.txt")]
        public void CheckConstraintsWorksWithValidProgram7()
        {
            string filename = "program7.txt";
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
        [DeploymentItem("SampleFiles\\program8.txt")]
        public void CheckConstraintsWorksWithValidProgram8()
        {
            string filename = "program8.txt";
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
        [DeploymentItem("SampleFiles\\program9.txt")]
        public void CheckConstraintsWorksWithValidProgram9()
        {
            string filename = "program9.txt";
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
        [DeploymentItem("SampleFiles\\program10.txt")]
        public void CheckConstraintsWorksWithValidProgram10()
        {
            string filename = "program10.txt";
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
        [DeploymentItem("SampleFiles\\program11.txt")]
        public void CheckConstraintsWorksWithValidProgram11()
        {
            string filename = "program11.txt";
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
        [DeploymentItem("SampleFiles\\program12.txt")]
        public void CheckConstraintsWorksWithValidProgram12()
        {
            string filename = "program12.txt";
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
        [DeploymentItem("SampleFiles\\program13.txt")]
        public void CheckConstraintsWorksWithValidProgram13()
        {
            string filename = "program13.txt";
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
        [DeploymentItem("SampleFiles\\InvalidSemantics\\error1.txt")]
        public void CheckConstraintsWorksWithError1()
        {
            string filename = "error1.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsTrue(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsTrue(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count != 0);

            List<string> errors = analyzer.GetDetectedErrors();
            
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\InvalidSemantics\\error2.txt")]
        public void CheckConstraintsWorksWithError2()
        {
            string filename = "error2.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsTrue(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count != 0);

            List<string> errors = analyzer.GetDetectedErrors();

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
        [DeploymentItem("SampleFiles\\InvalidSemantics\\returnMissing.txt")]
        public void CheckConstraintsWorksWithReturnMissing()
        {
            string filename = "returnMissing.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsTrue(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 2);

            List<string> errors = analyzer.GetDetectedErrors();
            
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\InvalidSemantics\\deadCode.txt")]
        public void CheckConstraintsWorksWithDeadCode()
        {
            string filename = "deadCode.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.IsFalse(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.IsTrue(analyzer.ErrosDetected());
            Assert.IsTrue(analyzer.GetDetectedErrors().Count == 2);

            List<string> errors = analyzer.GetDetectedErrors();

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

        [TestMethod]
        [DeploymentItem("SampleFiles\\ValidSemantics\\readwriteln.txt")]
        public void CheckConstraintsWorksWithReadWriteln()
        {
            string filename = "readwriteln.txt";
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
