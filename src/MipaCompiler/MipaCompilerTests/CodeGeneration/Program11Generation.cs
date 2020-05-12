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

            Assert.AreEqual("// here are forward declarations for functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** function_reversearray(int * ret_arr_size, char ** var_arr, int * size_arr);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("void procedure_printarray(char ** var_arr, int * size_arr);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here are the definitions of functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** function_reversearray(int * ret_arr_size, char ** var_arr, int * size_arr)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** var_reversed = allocateArrayOfStrings(size_arr);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_reversed = *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_j;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_0 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_1 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_2 = *size_arr - tmp_1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_j = tmp_2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_3 = var_i < *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_3) goto label_while_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_4 = var_arr[var_i];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_reversed[var_j] = tmp_4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_5 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_6 = var_j - tmp_5;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_j = tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_7 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_8 = var_i + tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_8;", visitor.GetCodeLines()[i++]);
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
            Assert.AreEqual("int tmp_9 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_9;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_1_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_10 = var_i < *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_10) goto label_while_1_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_11 = var_arr[var_i];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", tmp_11);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_12 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_13 = var_i + tmp_12;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_13;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_while_1_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_1_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);


            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_14 = 4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** var_myarray1 = allocateArrayOfStrings(&tmp_14);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** var_myarray2 = allocateArrayOfStrings(&tmp_14);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_myarray1 = tmp_14;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_myarray2 = tmp_14;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_15 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** var_myarray3 = allocateArrayOfStrings(&tmp_15);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_myarray3 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_16 = \"one\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_17 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_17] = tmp_16;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_18 = \"two\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_19 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_19] = tmp_18;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_20 = \"three\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_21 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_21] = tmp_20;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_22 = \"four\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_23 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_23] = tmp_22;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("size_myarray2 = size_myarray1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray2 = var_myarray1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray2, &size_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_tmp_24;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char ** tmp_24 = function_reversearray(&size_tmp_24, var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("size_myarray3 = size_tmp_24;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray3 = tmp_24;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray3, &size_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_eq;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_lteq;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_gt;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_neq;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * var_concat = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_25 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_26 = var_myarray1[tmp_25];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_27 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_28 = var_myarray1[tmp_27];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_29 = 0 == strcmp(tmp_26, tmp_28);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_eq = tmp_29;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_30 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_31 = var_myarray1[tmp_30];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_32 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_33 = var_myarray1[tmp_32];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_34 = 0 != strcmp(tmp_31, tmp_33);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_neq = tmp_34;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_35 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_36 = var_myarray1[tmp_35];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_37 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_38 = var_myarray1[tmp_37];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_39 = 0 >= strcmp(tmp_36, tmp_38);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_lteq = tmp_39;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_40 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_41 = var_myarray1[tmp_40];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_42 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_43 = var_myarray1[tmp_42];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_44 = 0 < strcmp(tmp_41, tmp_43);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_gt = tmp_44;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_45 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_46 = var_myarray1[tmp_45];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_47 = \" \";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_48 = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(tmp_48, tmp_46, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_49 = strlen(tmp_48);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_50 = 255 - tmp_49;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncat(tmp_48, tmp_47, tmp_50);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_51 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_52 = var_myarray1[tmp_51];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_53 = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(tmp_53, tmp_48, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_54 = strlen(tmp_53);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_55 = 255 - tmp_54;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncat(tmp_53, tmp_52, tmp_55);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(var_concat, tmp_53, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_concat);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray2, &size_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray3, &size_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(tmp_53);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(tmp_48);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_concat);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(tmp_24);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);


        }
    }
}
