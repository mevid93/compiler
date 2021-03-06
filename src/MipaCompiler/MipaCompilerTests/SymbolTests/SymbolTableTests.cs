﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler.Symbol;

namespace MipaCompilerTests.SymbolTests
{
    [TestClass]
    public class SymbolTableTests
    {
        [TestMethod]
        public void SymbolTableWorks()
        {
            SymbolTable symbolTable = new SymbolTable();
            Assert.IsTrue(symbolTable.GetCurrentScope() == 0);

            symbolTable.AddScope();
            Assert.AreEqual(1, symbolTable.GetCurrentScope());
            symbolTable.RemoveScope();
            Assert.AreEqual(0, symbolTable.GetCurrentScope());
        }

        [TestMethod]
        public void SymbolTableWorksForVariableSymbols()
        {
            SymbolTable symbolTable = new SymbolTable();

            symbolTable.DeclareVariableSymbol(new VariableSymbol("symbol1", "integer", "26", 0));
            Assert.IsTrue(symbolTable.IsVariableSymbolInTable("symbol1"));
            Assert.IsFalse(symbolTable.IsVariableSymbolInTable("symbol2"));

            Assert.AreEqual("symbol1", symbolTable.GetVariableSymbolByIdentifier("symbol1").GetIdentifier());
            symbolTable.AddScope();

            symbolTable.ReDeclareVariableSymbol(new VariableSymbol("symbol1", "string", "hello world", symbolTable.GetCurrentScope()));
            Assert.IsTrue(symbolTable.IsVariableSymbolInTable("symbol1"));
            string type = symbolTable.GetVariableSymbolByIdentifier("symbol1").GetSymbolType();
            Assert.AreEqual("string", type);

            symbolTable.RemoveScope();
            Assert.IsTrue(symbolTable.IsVariableSymbolInTable("symbol1"));
            type = symbolTable.GetVariableSymbolByIdentifier("symbol1").GetSymbolType();
            Assert.AreEqual("integer", type);
        }

        [TestMethod]
        public void SymbolTableWorksForFunctionSymbols()
        {
            SymbolTable symbolTable = new SymbolTable();

            string[] args = { "real", "integer" };
            symbolTable.DeclareFunctionSymbol(new FunctionSymbol("function1", args, "integer"));
            Assert.IsTrue(symbolTable.IsFunctionSymbolInTable(new FunctionSymbol("function1", args, "integer")));
            Assert.IsFalse(symbolTable.IsFunctionSymbolInTable(new FunctionSymbol("function2", args, "integer")));

            FunctionSymbol f = symbolTable.GetFunctionSymbolByIdentifierAndArguments("function1", args);
            Assert.IsTrue(f.GetIdentifier().Equals("function1"));
            Assert.IsTrue(f.GetReturnType().Equals("integer"));
        }

        [TestMethod]
        public void SymbolTableWorksForProcedureSymbols()
        {
            SymbolTable symbolTable = new SymbolTable();

            string[] args = { "real", "integer" };
            symbolTable.DeclareProcedureSymbol(new ProcedureSymbol("procedure1", args));
            Assert.IsTrue(symbolTable.IsProcedureSymbolInTable(new ProcedureSymbol("procedure1", args)));
            Assert.IsFalse(symbolTable.IsProcedureSymbolInTable(new ProcedureSymbol("procedure2", args)));

            ProcedureSymbol p = symbolTable.GetProcedureSymbolByIdentifierAndArguments("procedure1", args);
            Assert.IsTrue(p.GetIdentifier().Equals("procedure1"));
        }

        [TestMethod]
        public void CheckingIfVariableExistsInCurrentScopeWorks()
        {
            SymbolTable symbolTable = new SymbolTable();

            symbolTable.DeclareVariableSymbol(new VariableSymbol("var1", "integer", "23", 1));

            Assert.IsFalse(symbolTable.IsVariableSymbolInTableWithCurrentScope("var1"));
            symbolTable.AddScope();
            Assert.IsTrue(symbolTable.IsVariableSymbolInTableWithCurrentScope("var1"));
        }
    }
}
