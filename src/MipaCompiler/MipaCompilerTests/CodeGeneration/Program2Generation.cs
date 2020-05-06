using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program2Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program2.txt")]
        public void CodeGenerationWorksForProgram2()
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

            // check that code lines exists
            int i = 0;
            Assert.AreEqual("#include <stdio.h>", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("#include <string.h>", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("#include <stdlib.h>", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("typedef int bool;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("#define true 1", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("#define false 0", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            string comment = "// here are forward declarations for functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int function_f(int * var_n);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int function_m(int * var_n);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            comment = "// here are the definitions of functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int function_f(int * var_n)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_0 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_1 = *var_n == tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_1) goto label_else_0_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_2 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return tmp_2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_if_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_else_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_3 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_4 = *var_n - tmp_3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_5 = function_f(&tmp_4);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_6 = function_m(&tmp_5);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_7 = *var_n - tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_0_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("int function_m(int * var_n)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_1_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_8 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_9 = *var_n == tmp_8;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_9) goto label_else_1_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_10 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return tmp_10;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_if_1_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_else_1_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_11 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_12 = *var_n - tmp_11;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_13 = function_m(&tmp_12);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_14 = function_f(&tmp_13);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_15 = *var_n - tmp_14;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return tmp_15;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_1_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_16 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_16;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_2_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_17 = 19;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_18 = var_i <= tmp_17;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_18) goto label_while_2_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_19 = function_f(&var_i);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%d\\n\", tmp_19);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_while_2_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_2_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_20 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_20;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_3_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_21 = 19;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_22 = var_i <= tmp_21;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_22) goto label_while_3_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_23 = function_m(&var_i);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%d\\n\", tmp_23);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_while_3_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_3_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);

        }
    }
}
