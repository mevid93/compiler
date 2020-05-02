using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program1Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program1.txt")]
        public void CodeGenerationWorksForProgram1()
        {
            string filename = "program1.txt";
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

            // check that code lines exists
            Assert.AreEqual("#include <stdio.h>", visitor.GetCodeLines()[0]);
            Assert.AreEqual("", visitor.GetCodeLines()[1]);
            Assert.AreEqual("typedef int bool;", visitor.GetCodeLines()[2]);
            Assert.AreEqual("#define true 1", visitor.GetCodeLines()[3]);
            Assert.AreEqual("#define false 0", visitor.GetCodeLines()[4]);
            Assert.AreEqual("", visitor.GetCodeLines()[5]);
            string comment = "// here are forward declarations for functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[6]);
            Assert.AreEqual("", visitor.GetCodeLines()[7]);
            comment = "// here are the definitions of functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[8]);
            Assert.AreEqual("", visitor.GetCodeLines()[9]);
            comment = "// here is the main function";
            Assert.AreEqual(comment, visitor.GetCodeLines()[10]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[11]);
            Assert.AreEqual("{", visitor.GetCodeLines()[12]);
            Assert.AreEqual("int var_i, var_j;", visitor.GetCodeLines()[13]);
            Assert.AreEqual("scanf(\"%d %d\", &var_i, &var_j);", visitor.GetCodeLines()[14]);
            Assert.AreEqual("label_while_0_entry: ;", visitor.GetCodeLines()[15]);
            Assert.AreEqual("bool tmp_0 = var_i != var_j;", visitor.GetCodeLines()[16]);
            Assert.AreEqual("if (!tmp_0) goto label_while_0_exit;", visitor.GetCodeLines()[17]);
            Assert.AreEqual("label_if_0_entry: ;", visitor.GetCodeLines()[18]);
            Assert.AreEqual("bool tmp_1 = var_i > var_j;", visitor.GetCodeLines()[19]);
            Assert.AreEqual("if (!tmp_1) goto label_else_0_entry;", visitor.GetCodeLines()[20]);
            Assert.AreEqual("int tmp_2 = var_i - var_j;", visitor.GetCodeLines()[21]);
            Assert.AreEqual("var_i = tmp_2;", visitor.GetCodeLines()[22]);
            Assert.AreEqual("goto label_if_0_exit;", visitor.GetCodeLines()[23]);
            Assert.AreEqual("label_else_0_entry: ;", visitor.GetCodeLines()[24]);
            Assert.AreEqual("int tmp_3 = var_j - var_i;", visitor.GetCodeLines()[25]);
            Assert.AreEqual("var_j = tmp_3;", visitor.GetCodeLines()[26]);
            Assert.AreEqual("label_if_0_exit: ;", visitor.GetCodeLines()[27]);
            Assert.AreEqual("goto label_while_0_entry;", visitor.GetCodeLines()[28]);
            Assert.AreEqual("label_while_0_exit: ;", visitor.GetCodeLines()[29]);
            Assert.AreEqual("printf(\"%d\", var_i);", visitor.GetCodeLines()[30]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[31]);
            
        }
    }
}
