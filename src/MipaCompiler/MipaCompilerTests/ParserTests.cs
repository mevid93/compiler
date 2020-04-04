using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;

namespace MipaCompilerTests
{
    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program1.txt")]
        public void ParsingWorksWithValidProgram1()
        {
            string filename = "program1.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);

            INode ast = parser.Parse();
            ast.PrettyPrint();
            Assert.IsFalse(parser.ErrorsDetected());
            Assert.IsNotNull(ast);
            Assert.AreEqual(NodeType.PROGRAM, ast.GetNodeType());
            ProgramNode pdn = (ProgramNode)ast;
            Assert.AreEqual("GCD", pdn.GetProgramName());
            Assert.IsTrue(pdn.GetFunctions().Count == 0);
            Assert.IsTrue(pdn.GetProcedures().Count == 0);
            Assert.IsFalse(pdn.GetMainBlock() == null);
        }
    }
}
