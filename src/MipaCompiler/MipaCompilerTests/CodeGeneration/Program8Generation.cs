using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program8Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program8.txt")]
        public void CodeGenerationWorksForProgram8()
        {
            string filename = "program8.txt";
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
            Assert.AreEqual("int * function_reversearray(int * ret_arr_size, int * var_arr, int * size_arr);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("void procedure_printarray(int * var_arr, int * size_arr);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here are the definitions of functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * function_reversearray(int * ret_arr_size, int * var_arr, int * size_arr)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_0 = sizeof(int);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_1 = *size_arr * tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * var_reversed = malloc(tmp_1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_reversed = *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_j;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_2 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_3 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_4 = *size_arr - tmp_3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_j = tmp_4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_5 = var_i < *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_5) goto label_while_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_6 = &var_arr[var_i];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_reversed[var_j] = *tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_7 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_8 = var_j - tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_j = tmp_8;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_9 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_10 = var_i + tmp_9;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_10;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_while_0_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_0_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("*ret_arr_size = size_reversed;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return var_reversed;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_reversed);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("void procedure_printarray(int * var_arr, int * size_arr)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_11 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_11;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_1_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_12 = var_i < *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_12) goto label_while_1_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_13 = &var_arr[var_i];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%d\\n\", *tmp_13);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_14 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_15 = var_i + tmp_14;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_15;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_while_1_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_1_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_16 = 4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_17 = sizeof(int);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_18 = tmp_16 * tmp_17;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * var_myarray1 = malloc(tmp_18);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_19 = sizeof(int);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_20 = tmp_16 * tmp_19;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * var_myarray2 = malloc(tmp_20);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_myarray1 = tmp_16;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_myarray2 = tmp_16;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_21 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_22 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_22] = tmp_21;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_23 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_24 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_24] = tmp_23;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_25 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_26 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_26] = tmp_25;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_27 = 4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_28 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_28] = tmp_27;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("size_myarray2 = size_myarray1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray2 = var_myarray1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray2, &size_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_tmp_29;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_29 = function_reversearray(&size_tmp_29, var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("size_myarray2 = size_tmp_29;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray2 = tmp_29;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray2, &size_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_30 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_31 = &var_myarray1[tmp_30];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_32 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_33 = &var_myarray1[tmp_32];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_34 = *tmp_31 + *tmp_33;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_35 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_35] = tmp_34;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_36 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_37 = &var_myarray1[tmp_36];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_38 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_39 = &var_myarray1[tmp_38];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_40 = *tmp_37 - *tmp_39;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_41 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_41] = tmp_40;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_42 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_43 = &var_myarray1[tmp_42];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_44 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_45 = &var_myarray1[tmp_44];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_46 = *tmp_43 * *tmp_45;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_47 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_47] = tmp_46;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_48 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_49 = &var_myarray1[tmp_48];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_50 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_51 = &var_myarray1[tmp_50];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_52 = *tmp_49 / *tmp_51;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_53 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_53] = tmp_52;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_54 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_55 = &var_myarray2[tmp_54];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_56 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * tmp_57 = &var_myarray2[tmp_56];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_58 = *tmp_55 % *tmp_57;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_59 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray2[tmp_59] = tmp_58;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray2, &size_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(tmp_29);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);


        }
    }
}
