using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program4Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program4.txt")]
        public void CodeGenerationWorksForProgram4()
        {
            string filename = "program4.txt";
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
            Assert.AreEqual("char ** x = malloc(*arr_size * sizeof(char *));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("while_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (var_i >= *arr_size) goto while_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("x[var_i] = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
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

            string comment = "// here are forward declarations for functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int function_square(int * var_value);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("void procedure_increase(int * var_value);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            comment = "// here are the definitions of functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int function_square(int * var_value)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_0 = *var_value * *var_value;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("void procedure_increase(int * var_value)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_1 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_2 = *var_value + tmp_1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("*var_value = tmp_2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_myinteger1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_3 = 100;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myinteger1 = tmp_3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("scanf(\"%d\", &var_myinteger2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_sum;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_sub;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_mul;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_mod;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_div;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_4 = var_myinteger1 + var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_sum = tmp_4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_5 = var_myinteger1 - var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_sub = tmp_5;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_6 = var_myinteger1 * var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_mul = tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_7 = var_myinteger1 / var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_div = tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_8 = var_myinteger1 % var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_mod = tmp_8;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_9 = function_square(&var_sum);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_sum = tmp_9;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_increase(&var_sum);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_comparison;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_10 = var_myinteger1 == var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_10;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_11 = var_myinteger1 != var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_11;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_12 = var_myinteger1 < var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_12;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_13 = var_myinteger1 > var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_13;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_14 = var_myinteger1 <= var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_14;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_15 = var_myinteger1 >= var_myinteger2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_15;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%d\\n\", var_sum);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
        }
    }
}
