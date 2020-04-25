using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;
using System.Collections.Generic;

namespace MipaCompilerTests.NodeTests
{
    [TestClass]
    public class ProgramNodeTests
    {
        [TestMethod]
        public void CodeGenerationWorks1()
        {
            List<INode> procedures = new List<INode>();
            List<INode> functions = new List<INode>();

            List<INode> statements = new List<INode>();
            statements.Add(new ReturnNode(1, 1, null));
            BlockNode blockNode = new BlockNode(1, 1, statements);

            ProgramNode progNode = new ProgramNode(1, 1, "TestProgram", procedures, functions, blockNode);

            Visitor visitor = new Visitor();

            progNode.GenerateCode(visitor);

            Assert.IsTrue(visitor.GetCodeLines().Count != 0);

            // print to output for manual view
            foreach(string s in visitor.GetCodeLines())
            {
                Console.WriteLine(s);
            }

            Assert.AreEqual("#include <stdio.h>\n", visitor.GetCodeLines()[0]);
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program2.txt")]
        public void CodeGenerationForwardDeclarationWorks()
        {
            string filename = "program2.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Visitor visitor = new Visitor();

            ast.GenerateCode(visitor);

            Assert.IsTrue(visitor.GetCodeLines().Count != 0);

            // print to output for manual view
            foreach (string s in visitor.GetCodeLines())
            {
                Console.WriteLine(s);
            }

            Assert.AreEqual("#include <stdio.h>\n", visitor.GetCodeLines()[0]);
        }

        [TestMethod]
        [DeploymentItem("SampleFiles\\program3.txt")]
        public void CodeGenerationForwardDeclarationWorks2()
        {
            string filename = "program3.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Visitor visitor = new Visitor();

            ast.GenerateCode(visitor);

            Assert.IsTrue(visitor.GetCodeLines().Count != 0);

            // print to output for manual view
            foreach (string s in visitor.GetCodeLines())
            {
                Console.WriteLine(s);
            }

            Assert.AreEqual("#include <stdio.h>\n", visitor.GetCodeLines()[0]);
        }
    }
}
