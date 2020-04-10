using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System.Collections.Generic;

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

            Assert.AreEqual(pdn.GetMainBlock().GetNodeType(),  NodeType.BLOCK);
            BlockNode bn = (BlockNode)pdn.GetMainBlock();
            List<INode> statements = bn.GetStatements();
            Assert.AreEqual(NodeType.VARIABLE_DCL, statements[0].GetNodeType());
            Assert.AreEqual(NodeType.CALL, statements[1].GetNodeType());
            Assert.AreEqual(NodeType.WHILE, statements[2].GetNodeType());
            Assert.AreEqual(NodeType.CALL, statements[3].GetNodeType());
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program2.txt")]
        public void ParsingWorksWithValidProgram2()
        {
            string filename = "program2.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);

            INode ast = parser.Parse();
            ast.PrettyPrint();
            Assert.IsFalse(parser.ErrorsDetected());
            Assert.IsNotNull(ast);
            Assert.AreEqual(NodeType.PROGRAM, ast.GetNodeType());
            ProgramNode pdn = (ProgramNode)ast;
            Assert.AreEqual("MutualRecursion", pdn.GetProgramName());
            Assert.IsTrue(pdn.GetFunctions().Count == 2);
            Assert.IsTrue(pdn.GetProcedures().Count == 0);
            Assert.IsFalse(pdn.GetMainBlock() == null);

            
            Assert.AreEqual(pdn.GetMainBlock().GetNodeType(), NodeType.BLOCK);
            BlockNode bn = (BlockNode)pdn.GetMainBlock();
            List<INode> statements = bn.GetStatements();
            Assert.AreEqual(NodeType.VARIABLE_DCL, statements[0].GetNodeType());
            Assert.AreEqual(NodeType.ASSIGNMENT, statements[1].GetNodeType());
            Assert.AreEqual(NodeType.WHILE, statements[2].GetNodeType());
            Assert.AreEqual(NodeType.ASSIGNMENT, statements[3].GetNodeType());
            Assert.AreEqual(NodeType.WHILE, statements[2].GetNodeType());

            Assert.AreEqual(pdn.GetFunctions()[0].GetNodeType(), NodeType.FUNCTION);
            FunctionNode fn = (FunctionNode)pdn.GetFunctions()[0];
            Assert.AreEqual("integer", fn.GetReturnType());
            Assert.IsTrue(fn.GetParameters().Count == 1);
            BlockNode fbn = (BlockNode)fn.GetBlock();
            Assert.AreEqual(NodeType.IF_ELSE, fbn.GetStatements()[0].GetNodeType());
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program3.txt")]
        public void ParsingWorksWithValidProgram3()
        {
            string filename = "program3.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);

            INode ast = parser.Parse();
            ast.PrettyPrint();
            Assert.IsFalse(parser.ErrorsDetected());
            Assert.IsNotNull(ast);
            Assert.AreEqual(NodeType.PROGRAM, ast.GetNodeType());
            ProgramNode pdn = (ProgramNode)ast;
            Assert.AreEqual("SwapAndSumThem", pdn.GetProgramName());
            Assert.IsTrue(pdn.GetFunctions().Count == 2);
            Assert.IsTrue(pdn.GetProcedures().Count == 0);
            Assert.IsFalse(pdn.GetMainBlock() == null);

        }
    }
}
