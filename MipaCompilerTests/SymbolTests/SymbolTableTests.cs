using MipaCompiler.Symbol;
using Xunit;

namespace MipaCompilerTests.SymbolTests
{
    public class SymbolTableTests
    {
        [Fact]
        public void SymbolTableWorks()
        {
            SymbolTable symbolTable = new SymbolTable();
            Assert.True(symbolTable.GetCurrentScope() == 0);

            symbolTable.AddScope();
            Assert.Equal(1, symbolTable.GetCurrentScope());
            symbolTable.RemoveScope();
            Assert.Equal(0, symbolTable.GetCurrentScope());
        }

        [Fact]
        public void SymbolTableWorksForVariableSymbols()
        {
            SymbolTable symbolTable = new SymbolTable();

            symbolTable.DeclareVariableSymbol(new VariableSymbol("symbol1", "integer", "26", 0));
            Assert.True(symbolTable.IsVariableSymbolInTable("symbol1"));
            Assert.False(symbolTable.IsVariableSymbolInTable("symbol2"));

            Assert.Equal("symbol1", symbolTable.GetVariableSymbolByIdentifier("symbol1").GetIdentifier());
            symbolTable.AddScope();

            symbolTable.ReDeclareVariableSymbol(new VariableSymbol("symbol1", "string", "hello world", symbolTable.GetCurrentScope()));
            Assert.True(symbolTable.IsVariableSymbolInTable("symbol1"));
            string type = symbolTable.GetVariableSymbolByIdentifier("symbol1").GetSymbolType();
            Assert.Equal("string", type);

            symbolTable.RemoveScope();
            Assert.True(symbolTable.IsVariableSymbolInTable("symbol1"));
            type = symbolTable.GetVariableSymbolByIdentifier("symbol1").GetSymbolType();
            Assert.Equal("integer", type);
        }

        [Fact]
        public void SymbolTableWorksForFunctionSymbols()
        {
            SymbolTable symbolTable = new SymbolTable();

            string[] args = { "real", "integer" };
            symbolTable.DeclareFunctionSymbol(new FunctionSymbol("function1", args, "integer"));
            Assert.True(symbolTable.IsFunctionSymbolInTable(new FunctionSymbol("function1", args, "integer")));
            Assert.False(symbolTable.IsFunctionSymbolInTable(new FunctionSymbol("function2", args, "integer")));

            FunctionSymbol f = symbolTable.GetFunctionSymbolByIdentifierAndArguments("function1", args);
            Assert.True(f.GetIdentifier().Equals("function1"));
            Assert.True(f.GetReturnType().Equals("integer"));
        }

        [Fact]
        public void SymbolTableWorksForProcedureSymbols()
        {
            SymbolTable symbolTable = new SymbolTable();

            string[] args = { "real", "integer" };
            symbolTable.DeclareProcedureSymbol(new ProcedureSymbol("procedure1", args));
            Assert.True(symbolTable.IsProcedureSymbolInTable(new ProcedureSymbol("procedure1", args)));
            Assert.False(symbolTable.IsProcedureSymbolInTable(new ProcedureSymbol("procedure2", args)));

            ProcedureSymbol p = symbolTable.GetProcedureSymbolByIdentifierAndArguments("procedure1", args);
            Assert.True(p.GetIdentifier().Equals("procedure1"));
        }

        [Fact]
        public void CheckingIfVariableExistsInCurrentScopeWorks()
        {
            SymbolTable symbolTable = new SymbolTable();

            symbolTable.DeclareVariableSymbol(new VariableSymbol("var1", "integer", "23", 1));

            Assert.False(symbolTable.IsVariableSymbolInTableWithCurrentScope("var1"));
            symbolTable.AddScope();
            Assert.True(symbolTable.IsVariableSymbolInTableWithCurrentScope("var1"));
        }
    }
}
