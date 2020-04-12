using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler.Symbol;

namespace MipaCompilerTests.SymbolTests
{
    [TestClass]
    public class ProcedureSymbolTests
    {
        [TestMethod]
        public void ProcedureSymbolWorks()
        {
            string[] args = { "integer", "string", "boolean", "real" };

            ProcedureSymbol functionSymbol = new ProcedureSymbol("CustomProcedure", args);

            Assert.AreEqual("CustomProcedure", functionSymbol.GetIdentifier());
            Assert.AreEqual("integer", functionSymbol.GetArgumentTypes()[0]);
            Assert.AreEqual("real", functionSymbol.GetArgumentTypes()[3]);
        }

        [TestMethod]
        public void HasSameDefinitionWorks()
        {
            string[] args = { "integer", "string", "boolean", "real" };
            string[] args1 = { "integer", "string", "boolean", "integer" };
            string[] args2 = { };

            ProcedureSymbol procedureSymbol = new ProcedureSymbol("CustomFunction", args);
            ProcedureSymbol procedureSymbol2 = new ProcedureSymbol("CustomFunction", args);
            ProcedureSymbol procedureSymbol3 = new ProcedureSymbol("CustomFunction3", args);
            ProcedureSymbol procedureSymbol4 = new ProcedureSymbol("CustomFunction4", args1);
            ProcedureSymbol procedureSymbol5 = new ProcedureSymbol("CustomFunction5", args2);

            Assert.IsTrue(procedureSymbol.HasSameDefinition(procedureSymbol2));
            Assert.IsFalse(procedureSymbol.HasSameDefinition(procedureSymbol3));
            Assert.IsFalse(procedureSymbol.HasSameDefinition(procedureSymbol4));
            Assert.IsFalse(procedureSymbol.HasSameDefinition(procedureSymbol5));
        }
    }
}
