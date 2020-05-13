using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program11Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program11.txt")]
        public void CodeGenerationWorksForProgram11()
        {
            string filename = "program11.txt";
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
            Assert.AreEqual("char ** function_reversearray(int * ret_arr_size, char ** var_arr, int * size_arr);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("void procedure_printarray(char ** var_arr, int * size_arr);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here are the definitions of functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** function_reversearray(int * ret_arr_size, char ** var_arr, int * size_arr)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_0 = sizeof(char *);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_1 = *size_arr * tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** var_reversed = allocateArrayOfStrings(size_arr);", visitor.GetCodeLines()[i++]);
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
            Assert.AreEqual("char * tmp_6 = var_arr[var_i];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_reversed[var_j] = tmp_6;", visitor.GetCodeLines()[i++]);
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

            Assert.AreEqual("void procedure_printarray(char ** var_arr, int * size_arr)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_11 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_11;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_1_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_12 = var_i < *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_12) goto label_while_1_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_13 = var_arr[var_i];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", tmp_13);", visitor.GetCodeLines()[i++]);
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
            Assert.AreEqual("int tmp_17 = sizeof(char *);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_18 = tmp_16 * tmp_17;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** var_myarray1 = allocateArrayOfStrings(&tmp_16);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_19 = sizeof(char *);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_20 = tmp_16 * tmp_19;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** var_myarray2 = allocateArrayOfStrings(&tmp_16);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_myarray1 = tmp_16;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_myarray2 = tmp_16;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_21 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** var_myarray3 = allocateArrayOfStrings(&tmp_21);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_myarray3 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_22 = \"one\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_23 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_23] = tmp_22;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_24 = \"two\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_25 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_25] = tmp_24;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_26 = \"three\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_27 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_27] = tmp_26;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_28 = \"four\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_29 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_29] = tmp_28;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("size_myarray2 = size_myarray1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray2 = var_myarray1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray2, &size_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_tmp_30;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** tmp_30 = function_reversearray(&size_tmp_30, var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("size_myarray3 = size_tmp_30;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray3 = tmp_30;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray3, &size_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_eq;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_lteq;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_gt;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_neq;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_31 = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_32 = 256 * tmp_31;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * var_concat = malloc(tmp_32);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_33 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_34 = var_myarray1[tmp_33];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_35 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_36 = var_myarray1[tmp_35];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_37 = 0 == strcmp(tmp_34, tmp_36);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_eq = tmp_37;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_38 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_39 = var_myarray1[tmp_38];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_40 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_41 = var_myarray1[tmp_40];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_42 = 0 != strcmp(tmp_39, tmp_41);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_neq = tmp_42;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_43 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_44 = var_myarray1[tmp_43];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_45 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_46 = var_myarray1[tmp_45];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_47 = 0 >= strcmp(tmp_44, tmp_46);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_lteq = tmp_47;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_48 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_49 = var_myarray1[tmp_48];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_50 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_51 = var_myarray1[tmp_50];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_52 = 0 < strcmp(tmp_49, tmp_51);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_gt = tmp_52;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_53 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_54 = var_myarray1[tmp_53];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_55 = \" \";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_57 = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_58 = 256 * tmp_57;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_56 = malloc(tmp_58);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(tmp_56, tmp_54, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_59 = strlen(tmp_56);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_60 = 255 - tmp_59;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncat(tmp_56, tmp_55, tmp_60);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_61 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_62 = var_myarray1[tmp_61];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_64 = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_65 = 256 * tmp_64;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_63 = malloc(tmp_65);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(tmp_63, tmp_56, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_66 = strlen(tmp_63);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_67 = 255 - tmp_66;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncat(tmp_63, tmp_62, tmp_67);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(var_concat, tmp_63, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_concat);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray2, &size_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray3, &size_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(tmp_63);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(tmp_56);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(tmp_30);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);

        }
    }
}
