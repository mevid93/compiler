﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            Assert.AreEqual("// hard coded function to allocate string array", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** allocateArrayOfStrings(int * arr_size)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_a = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_b = *arr_size * tmp_a;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** x = malloc(tmp_b);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("while_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (var_i >= *arr_size) goto while_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_c = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_d = 256 * tmp_c;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("x[var_i] = malloc(tmp_d);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = var_i + 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto while_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("while_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return x;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// hard coded function to deallocate string array", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("void deallocateArrayOfStrings(char ** arr, int * arr_size)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("while_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (var_i >= *arr_size) goto while_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(arr[var_i]);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = var_i + 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto while_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("while_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(arr);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here are forward declarations for functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool function_is_even(int * var_n);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool function_is_odd(int * var_n);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here are the definitions of functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool function_is_even(int * var_n)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_0 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_1 = *var_n == tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_1) goto label_else_0_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return true;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_if_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_else_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_2 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_3 = *var_n - tmp_2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_4 = function_is_odd(&tmp_3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return tmp_4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_0_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("bool function_is_odd(int * var_n)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_1_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_5 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_6 = *var_n == tmp_5;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_6) goto label_else_1_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return false;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_if_1_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_else_1_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_7 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_8 = *var_n - tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_9 = function_is_even(&tmp_8);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return tmp_9;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_1_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_10 = \"Give integer:\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", tmp_10);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("scanf(\"%d\", &var_i);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_even;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_11 = function_is_even(&var_i);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_even = tmp_11;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_2_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!var_even) goto label_else_2_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_12 = \" is EVEN number!\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%d%s\\n\", var_i, tmp_12);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_if_2_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_else_2_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_13 = \" is ODD number!\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%d%s\\n\", var_i, tmp_13);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_2_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);

        }
    }
}
