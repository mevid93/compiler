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
            Assert.AreEqual("int tmp_0 = 0;", visitor.GetCodeLines()[14]);
            Assert.AreEqual("bool tmp_1 = *var_n == tmp_0;", visitor.GetCodeLines()[15]);
            Assert.AreEqual("if (!tmp_1) goto label_else_0_entry;", visitor.GetCodeLines()[16]);
            Assert.AreEqual("int tmp_2 = 1;", visitor.GetCodeLines()[17]);
            Assert.AreEqual("return tmp_2;", visitor.GetCodeLines()[18]);
            Assert.AreEqual("goto label_if_0_exit;", visitor.GetCodeLines()[19]);
            Assert.AreEqual("label_else_0_entry: ;", visitor.GetCodeLines()[20]);
            Assert.AreEqual("int tmp_3 = 1;", visitor.GetCodeLines()[21]);
            Assert.AreEqual("int tmp_4 = *var_n - tmp_3;", visitor.GetCodeLines()[22]);
            Assert.AreEqual("int tmp_5 = function_f(&tmp_4);", visitor.GetCodeLines()[23]);
            Assert.AreEqual("int tmp_6 = function_m(&tmp_5);", visitor.GetCodeLines()[24]);
            Assert.AreEqual("int tmp_7 = *var_n - tmp_6;", visitor.GetCodeLines()[25]);
            Assert.AreEqual("return tmp_7;", visitor.GetCodeLines()[26]);
            Assert.AreEqual("label_if_0_exit: ;", visitor.GetCodeLines()[27]);
            Assert.AreEqual("}", visitor.GetCodeLines()[28]);
            Assert.AreEqual("", visitor.GetCodeLines()[29]);

            Assert.AreEqual("int function_m(int * var_n)", visitor.GetCodeLines()[30]);
            Assert.AreEqual("{", visitor.GetCodeLines()[31]);
            Assert.AreEqual("label_if_1_entry: ;", visitor.GetCodeLines()[32]);
            Assert.AreEqual("int tmp_8 = 0;", visitor.GetCodeLines()[33]);
            Assert.AreEqual("bool tmp_9 = *var_n == tmp_8;", visitor.GetCodeLines()[34]);
            Assert.AreEqual("if (!tmp_9) goto label_else_1_entry;", visitor.GetCodeLines()[35]);
            Assert.AreEqual("int tmp_10 = 0;", visitor.GetCodeLines()[36]);
            Assert.AreEqual("return tmp_10;", visitor.GetCodeLines()[37]);
            Assert.AreEqual("goto label_if_1_exit;", visitor.GetCodeLines()[38]);
            Assert.AreEqual("label_else_1_entry: ;", visitor.GetCodeLines()[39]);
            Assert.AreEqual("int tmp_11 = 1;", visitor.GetCodeLines()[40]);
            Assert.AreEqual("int tmp_12 = *var_n - tmp_11;", visitor.GetCodeLines()[41]);
            Assert.AreEqual("int tmp_13 = function_m(&tmp_12);", visitor.GetCodeLines()[42]);
            Assert.AreEqual("int tmp_14 = function_f(&tmp_13);", visitor.GetCodeLines()[43]);
            Assert.AreEqual("int tmp_15 = *var_n - tmp_14;", visitor.GetCodeLines()[44]);
            Assert.AreEqual("return tmp_15;", visitor.GetCodeLines()[45]);
            Assert.AreEqual("label_if_1_exit: ;", visitor.GetCodeLines()[46]);
            Assert.AreEqual("}", visitor.GetCodeLines()[47]);
            Assert.AreEqual("", visitor.GetCodeLines()[48]);
            Assert.AreEqual("", visitor.GetCodeLines()[49]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[50]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[51]);
            Assert.AreEqual("{", visitor.GetCodeLines()[52]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[53]);
            Assert.AreEqual("int tmp_16 = 0;", visitor.GetCodeLines()[54]);
            Assert.AreEqual("var_i = tmp_16;", visitor.GetCodeLines()[55]);
            Assert.AreEqual("label_while_2_entry: ;", visitor.GetCodeLines()[56]);
            Assert.AreEqual("int tmp_17 = 19;", visitor.GetCodeLines()[57]);
            Assert.AreEqual("bool tmp_18 = var_i <= tmp_17;", visitor.GetCodeLines()[58]);
            Assert.AreEqual("if (!tmp_18) goto label_while_2_exit;", visitor.GetCodeLines()[59]);
            Assert.AreEqual("int tmp_19 = function_f(&var_i);", visitor.GetCodeLines()[60]);
            Assert.AreEqual("printf(\"%d\\n\", tmp_19);", visitor.GetCodeLines()[61]);
            Assert.AreEqual("goto label_while_2_entry;", visitor.GetCodeLines()[62]);
            Assert.AreEqual("label_while_2_exit: ;", visitor.GetCodeLines()[63]);
            Assert.AreEqual("int tmp_20 = 0;", visitor.GetCodeLines()[64]);
            Assert.AreEqual("var_i = tmp_20;", visitor.GetCodeLines()[65]);
            Assert.AreEqual("label_while_3_entry: ;", visitor.GetCodeLines()[66]);
            Assert.AreEqual("int tmp_21 = 19;", visitor.GetCodeLines()[67]);
            Assert.AreEqual("bool tmp_22 = var_i <= tmp_21;", visitor.GetCodeLines()[68]);
            Assert.AreEqual("if (!tmp_22) goto label_while_3_exit;", visitor.GetCodeLines()[69]);
            Assert.AreEqual("int tmp_23 = function_m(&var_i);", visitor.GetCodeLines()[70]);
            Assert.AreEqual("printf(\"%d\\n\", tmp_23);", visitor.GetCodeLines()[71]);
            Assert.AreEqual("goto label_while_3_entry;", visitor.GetCodeLines()[72]);
            Assert.AreEqual("label_while_3_exit: ;", visitor.GetCodeLines()[73]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[74]);
            Assert.AreEqual("}", visitor.GetCodeLines()[75]);

        }
    }
}
