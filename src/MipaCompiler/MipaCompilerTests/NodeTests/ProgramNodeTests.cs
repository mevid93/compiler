using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            List<string> codeLines = new List<string>();

            progNode.GenerateCode(codeLines);

            Assert.IsTrue(codeLines.Count != 0);

            // print to output for manual view
            foreach(string s in codeLines)
            {
                Console.WriteLine(s);
            }

            Assert.AreEqual("#include <stdio.h>\n", codeLines[0]);
        }
    }
}
