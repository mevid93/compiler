using Xunit;
using MipaCompiler;
using MipaCompiler.Node;
using System.Collections.Generic;

namespace MipaCompilerTests
{
    public class ParserTests
    {
        [Fact]
        public void ParsingWorksWithValidProgram1()
        {
            string filename = "../../../SampleFiles/program1.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);

            INode ast = parser.Parse();
            //ast.PrettyPrint();
            Assert.False(parser.ErrorsDetected());
            Assert.NotNull(ast);
            Assert.Equal(NodeType.PROGRAM, ast.GetNodeType());
            ProgramNode pdn = (ProgramNode)ast;
            Assert.Equal("GCD".ToLower(), pdn.GetProgramName());
            Assert.True(pdn.GetFunctions().Count == 0);
            Assert.True(pdn.GetProcedures().Count == 0);
            Assert.False(pdn.GetMainBlock() == null);

            Assert.Equal(NodeType.BLOCK, pdn.GetMainBlock().GetNodeType());
            BlockNode bn = (BlockNode)pdn.GetMainBlock();
            List<INode> statements = bn.GetStatements();
            Assert.Equal(NodeType.VARIABLE_DCL, statements[0].GetNodeType());
            Assert.Equal(NodeType.CALL, statements[1].GetNodeType());
            Assert.Equal(NodeType.WHILE, statements[2].GetNodeType());
            Assert.Equal(NodeType.CALL, statements[3].GetNodeType());
        }

        [Fact]
        public void ParsingWorksWithValidProgram2()
        {
            string filename = "../../../SampleFiles/program2.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);

            INode ast = parser.Parse();
            //ast.PrettyPrint();
            Assert.False(parser.ErrorsDetected());
            Assert.NotNull(ast);
            Assert.Equal(NodeType.PROGRAM, ast.GetNodeType());
            ProgramNode pdn = (ProgramNode)ast;
            Assert.Equal("EvenOrOdd".ToLower(), pdn.GetProgramName());
            Assert.True(pdn.GetFunctions().Count == 2);
            Assert.True(pdn.GetProcedures().Count == 0);
            Assert.False(pdn.GetMainBlock() == null);

            
            Assert.Equal(NodeType.BLOCK, pdn.GetMainBlock().GetNodeType());
            BlockNode bn = (BlockNode)pdn.GetMainBlock();
            List<INode> statements = bn.GetStatements();
            Assert.Equal(NodeType.VARIABLE_DCL, statements[0].GetNodeType());
            Assert.Equal(NodeType.CALL, statements[1].GetNodeType());
            Assert.Equal(NodeType.CALL, statements[2].GetNodeType());
            Assert.Equal(NodeType.VARIABLE_DCL, statements[3].GetNodeType());
            Assert.Equal(NodeType.CALL, statements[2].GetNodeType());

            Assert.Equal(NodeType.FUNCTION, pdn.GetFunctions()[0].GetNodeType());
            FunctionNode fn = (FunctionNode)pdn.GetFunctions()[0];
            SimpleTypeNode stn = (SimpleTypeNode)fn.GetReturnType();
            Assert.Equal("boolean", stn.GetTypeValue());
            Assert.True(fn.GetParameters().Count == 1);
            BlockNode fbn = (BlockNode)fn.GetBlock();
            Assert.Equal(NodeType.IF_ELSE, fbn.GetStatements()[0].GetNodeType());
        }

        [Fact]
        public void ParsingWorksWithValidProgram3()
        {
            string filename = "../../../SampleFiles/program3.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);

            INode ast = parser.Parse();
            //ast.PrettyPrint();
            Assert.False(parser.ErrorsDetected());
            Assert.NotNull(ast);
            Assert.Equal(NodeType.PROGRAM, ast.GetNodeType());
            ProgramNode pdn = (ProgramNode)ast;
            Assert.Equal("SwapAndSumThem".ToLower(), pdn.GetProgramName());
            Assert.True(pdn.GetFunctions().Count == 1);
            Assert.True(pdn.GetProcedures().Count == 1);
            Assert.False(pdn.GetMainBlock() == null);

            Assert.Equal(NodeType.PROCEDURE, pdn.GetProcedures()[0].GetNodeType());
            ProcedureNode pro = (ProcedureNode)pdn.GetProcedures()[0];
            Assert.Equal("Swap".ToLower(), pro.GetName());
            List<INode> pars = pro.GetParameters();
            Assert.Equal(2, pars.Count);

        }

        [Fact]
        public void ParsingWorksWithValidTypes1()
        {
            string filename = "../../../SampleFiles/types1.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);

            INode ast = parser.Parse();
            //ast.PrettyPrint();
            Assert.False(parser.ErrorsDetected());
            Assert.NotNull(ast);
            Assert.Equal(NodeType.PROGRAM, ast.GetNodeType());
            ProgramNode pdn = (ProgramNode)ast;
            Assert.Equal("Types".ToLower(), pdn.GetProgramName());
            Assert.True(pdn.GetFunctions().Count == 0);
            Assert.True(pdn.GetProcedures().Count == 0);
            Assert.False(pdn.GetMainBlock() == null);

        }
    }
}
