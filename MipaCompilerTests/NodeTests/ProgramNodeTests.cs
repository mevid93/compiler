using Xunit;
using MipaCompiler;
using MipaCompiler.Node;
using System;
using System.Collections.Generic;

namespace MipaCompilerTests.NodeTests
{
    public class ProgramNodeTests
    {
        [Fact]
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

            Assert.True(visitor.GetCodeLines().Count != 0);

            // print to output for manual view
            foreach(string s in visitor.GetCodeLines())
            {
                //Console.WriteLine(s);
            }

            Assert.Equal("#include <stdio.h>", visitor.GetCodeLines()[0]);
        }

    }
}
