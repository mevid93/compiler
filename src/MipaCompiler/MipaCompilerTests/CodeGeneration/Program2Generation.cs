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
            Assert.AreEqual("#include <stdio.h>", visitor.GetCodeLines()[0]);
            Assert.AreEqual("", visitor.GetCodeLines()[1]);
            Assert.AreEqual("typedef int bool;", visitor.GetCodeLines()[2]);
            Assert.AreEqual("#define true 1", visitor.GetCodeLines()[3]);
            Assert.AreEqual("#define false 0", visitor.GetCodeLines()[4]);
            Assert.AreEqual("", visitor.GetCodeLines()[5]);

            string comment = "// here are forward declarations for functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[6]);
            Assert.AreEqual("int function_f(int * var_n);", visitor.GetCodeLines()[7]);
            Assert.AreEqual("int function_m(int * var_n);", visitor.GetCodeLines()[8]);
            Assert.AreEqual("", visitor.GetCodeLines()[9]);

            comment = "// here are the definitions of functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[10]);
            Assert.AreEqual("int function_f(int * var_n)", visitor.GetCodeLines()[11]);
            Assert.AreEqual("{", visitor.GetCodeLines()[12]);
            Assert.AreEqual("label_if_0_entry: ;", visitor.GetCodeLines()[13]);
            Assert.AreEqual("bool tmp_0 = *var_n == 0;", visitor.GetCodeLines()[14]);
            Assert.AreEqual("if (!tmp_0) goto label_else_0_entry;", visitor.GetCodeLines()[15]);
            Assert.AreEqual("return 1;", visitor.GetCodeLines()[16]);
            Assert.AreEqual("goto label_if_0_exit;", visitor.GetCodeLines()[17]);
            Assert.AreEqual("label_else_0_entry: ;", visitor.GetCodeLines()[18]);
            Assert.AreEqual("int tmp_1 = *var_n - 1;", visitor.GetCodeLines()[19]);
            Assert.AreEqual("int tmp_2 = function_f(&tmp_1);", visitor.GetCodeLines()[20]);
            Assert.AreEqual("int tmp_3 = function_m(&tmp_2);", visitor.GetCodeLines()[21]);
            Assert.AreEqual("int tmp_4 = *var_n - tmp_3;", visitor.GetCodeLines()[22]);
            Assert.AreEqual("return tmp_4;", visitor.GetCodeLines()[23]);
            Assert.AreEqual("label_if_0_exit: ;", visitor.GetCodeLines()[24]);
            Assert.AreEqual("}", visitor.GetCodeLines()[25]);
            Assert.AreEqual("", visitor.GetCodeLines()[26]);

            Assert.AreEqual("int function_m(int * var_n)", visitor.GetCodeLines()[27]);
            Assert.AreEqual("{", visitor.GetCodeLines()[28]);
            Assert.AreEqual("label_if_1_entry: ;", visitor.GetCodeLines()[29]);
            Assert.AreEqual("bool tmp_5 = *var_n == 0;", visitor.GetCodeLines()[30]);
            Assert.AreEqual("if (!tmp_5) goto label_else_1_entry;", visitor.GetCodeLines()[31]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[32]);
            Assert.AreEqual("goto label_if_1_exit;", visitor.GetCodeLines()[33]);
            Assert.AreEqual("label_else_1_entry: ;", visitor.GetCodeLines()[34]);
            Assert.AreEqual("int tmp_6 = *var_n - 1;", visitor.GetCodeLines()[35]);
            Assert.AreEqual("int tmp_7 = function_m(&tmp_6);", visitor.GetCodeLines()[36]);
            Assert.AreEqual("int tmp_8 = function_f(&tmp_7);", visitor.GetCodeLines()[37]);
            Assert.AreEqual("int tmp_9 = *var_n - tmp_8;", visitor.GetCodeLines()[38]);
            Assert.AreEqual("return tmp_9;", visitor.GetCodeLines()[39]);
            Assert.AreEqual("label_if_1_exit: ;", visitor.GetCodeLines()[40]);
            Assert.AreEqual("}", visitor.GetCodeLines()[41]);
            Assert.AreEqual("", visitor.GetCodeLines()[42]);
            Assert.AreEqual("", visitor.GetCodeLines()[43]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[44]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[45]);
            Assert.AreEqual("{", visitor.GetCodeLines()[46]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[47]);
            Assert.AreEqual("var_i = 0;", visitor.GetCodeLines()[48]);
            Assert.AreEqual("label_while_2_entry: ;", visitor.GetCodeLines()[49]);
            Assert.AreEqual("bool tmp_10 = var_i <= 19;", visitor.GetCodeLines()[50]);
            Assert.AreEqual("if (!tmp_10) goto label_while_2_exit;", visitor.GetCodeLines()[51]);
            Assert.AreEqual("int tmp_11 = function_f(&var_i);", visitor.GetCodeLines()[52]);
            Assert.AreEqual("printf(\"%d\\n\", tmp_11);", visitor.GetCodeLines()[53]);
            Assert.AreEqual("goto label_while_2_entry;", visitor.GetCodeLines()[54]);
            Assert.AreEqual("label_while_2_exit: ;", visitor.GetCodeLines()[55]);
            Assert.AreEqual("var_i = 0;", visitor.GetCodeLines()[56]);
            Assert.AreEqual("label_while_3_entry: ;", visitor.GetCodeLines()[57]);
            Assert.AreEqual("bool tmp_12 = var_i <= 19;", visitor.GetCodeLines()[58]);
            Assert.AreEqual("if (!tmp_12) goto label_while_3_exit;", visitor.GetCodeLines()[59]);
            Assert.AreEqual("int tmp_13 = function_m(&var_i);", visitor.GetCodeLines()[60]);
            Assert.AreEqual("printf(\"%d\\n\", tmp_13);", visitor.GetCodeLines()[61]);
            Assert.AreEqual("goto label_while_3_entry;", visitor.GetCodeLines()[62]);
            Assert.AreEqual("label_while_3_exit: ;", visitor.GetCodeLines()[63]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[64]);
            Assert.AreEqual("}", visitor.GetCodeLines()[65]);

        }
    }
}
