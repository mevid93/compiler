using Xunit;
using MipaCompiler;
using MipaCompiler.Node;
using System.Collections.Generic;
using System.Linq;

namespace MipaCompilerTests
{
    public class SemanticAnalyzerTests
    {

        [Fact]
        public void CheckConstraintsWorksWithValidProgram1()
        {
            string filename = "../../../SampleFiles/program1.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram2()
        {
            string filename = "../../../SampleFiles/program2.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram3()
        {
            string filename = "../../../SampleFiles/program3.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram4()
        {
            string filename = "../../../SampleFiles/program4.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram5()
        {
            string filename = "../../../SampleFiles/program5.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram6()
        {
            string filename = "../../../SampleFiles/program6.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram7()
        {
            string filename = "../../../SampleFiles/program7.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram8()
        {
            string filename = "../../../SampleFiles/program8.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram9()
        {
            string filename = "../../../SampleFiles/program9.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram10()
        {
            string filename = "../../../SampleFiles/program10.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram11()
        {
            string filename = "../../../SampleFiles/program11.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram12()
        {
            string filename = "../../../SampleFiles/program12.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidProgram13()
        {
            string filename = "../../../SampleFiles/program13.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithValidTypes1()
        {
            string filename = "../../../SampleFiles/types1.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithDuplicateFunctions()
        {
            string filename = "../../../SampleFiles/InvalidSemantics/duplicates.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.True(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count != 0);

            List<string> errors = analyzer.GetDetectedErrors();

            Assert.Contains(errors, s => s.Contains("SemanticError::Row 10::Column 1"));
            Assert.Contains(errors, s => s.Contains("SemanticError::Row 34::Column 1"));
            Assert.Contains(errors, s => s.Contains("SemanticError::Row 50::Column 1"));
        }

        [Fact]
        public void CheckConstraintsWorksWithValidAssert()
        {
            string filename = "../../../SampleFiles/ValidSemantics/assertion.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithInvalidAssert()
        {
            string filename = "../../../SampleFiles/InvalidSemantics/assertionFail.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.True(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count != 0);

            List<string> errors = analyzer.GetDetectedErrors();

            Assert.Contains(errors, s => s.Contains("SemanticError::Row 3::Column 26"));
        }

        [Fact]
        public void CheckConstraintsWorksWithError1()
        {
            string filename = "../../../SampleFiles/InvalidSemantics/error1.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.True(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.True(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count != 0);

            List<string> errors = analyzer.GetDetectedErrors();
            
        }

        [Fact]
        public void CheckConstraintsWorksWithError2()
        {
            string filename = "../../../SampleFiles/InvalidSemantics/error2.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.True(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count != 0);

            List<string> errors = analyzer.GetDetectedErrors();

        }

        [Fact]
        public void CheckConstraintsWorksWithInvalidVariableDcl()
        {
            string filename = "../../../SampleFiles/InvalidSemantics/variableDcl.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.True(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 2);

            List<string> errors = analyzer.GetDetectedErrors();

            Assert.Contains(errors, s => s.Contains("SemanticError::Row 4::Column 5"));
        }

        [Fact]
        public void CheckConstraintsWorksWithInvalidReturnType()
        {
            string filename = "../../../SampleFiles/InvalidSemantics/returnType.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.True(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 2);

            List<string> errors = analyzer.GetDetectedErrors();

            Assert.Contains(errors, s => s.Contains("SemanticError::Row 10::Column 12"));
        }

        [Fact]
        public void CheckConstraintsWorksWithReturnMissing()
        {
            string filename = "../../../SampleFiles/InvalidSemantics/returnMissing.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.True(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 2);

            List<string> errors = analyzer.GetDetectedErrors();
            
        }

        [Fact]
        public void CheckConstraintsWorksWithDeadCode()
        {
            string filename = "../../../SampleFiles/InvalidSemantics/deadCode.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.True(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 2);

            List<string> errors = analyzer.GetDetectedErrors();

        }

        [Fact]
        public void CheckConstraintsWorksWithFunctionCallInsideExpression()
        {
            string filename = "../../../SampleFiles/ValidSemantics/expressionWithCall.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            //ast.PrettyPrint();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }

        [Fact]
        public void CheckConstraintsWorksWithReadWriteln()
        {
            string filename = "../../../SampleFiles/ValidSemantics/readwriteln.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            //ast.PrettyPrint();

            Assert.False(parser.ErrorsDetected());

            SemanticAnalyzer analyzer = new SemanticAnalyzer(ast);
            analyzer.CheckConstraints();

            Assert.False(analyzer.ErrosDetected());
            Assert.True(analyzer.GetDetectedErrors().Count == 0);
        }
    }
}
