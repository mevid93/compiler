using System;
using Xunit;
using MipaCompiler;
using MipaCompiler.Node;

namespace MipaCompilerTests.CodeGeneration
{
    public class Program10Generation
    {
        [Fact]
        public void CodeGenerationWorksForProgram10()
        {
            string filename = "../../../SampleFiles/program10.txt";
            Scanner scanner = new Scanner(filename);
            Parser parser = new Parser(scanner);
            INode ast = parser.Parse();

            Visitor visitor = new Visitor();

            ast.GenerateCode(visitor);

            Assert.True(visitor.GetCodeLines().Count != 0);

            // print to output for manual view
            foreach (string s in visitor.GetCodeLines())
            {
                //Console.WriteLine(s);
            }

            // check that code lines exists
            int i = 0;

            Assert.Equal("#include <stdio.h>", visitor.GetCodeLines()[i++]);
            Assert.Equal("#include <string.h>", visitor.GetCodeLines()[i++]);
            Assert.Equal("#include <stdlib.h>", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("typedef int bool;", visitor.GetCodeLines()[i++]);
            Assert.Equal("#define true 1", visitor.GetCodeLines()[i++]);
            Assert.Equal("#define false 0", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// hard coded function to allocate string array", visitor.GetCodeLines()[i++]);
            Assert.Equal("char ** allocateArrayOfStrings(int * arr_size)", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_a = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_b = *arr_size * tmp_a;", visitor.GetCodeLines()[i++]);
            Assert.Equal("char ** x = malloc(tmp_b);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int var_i = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("while_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("if (var_i >= *arr_size) goto while_exit;", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_c = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_d = 256 * tmp_c;", visitor.GetCodeLines()[i++]);
            Assert.Equal("x[var_i] = malloc(tmp_d);", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_i = var_i + 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("goto while_entry;", visitor.GetCodeLines()[i++]);
            Assert.Equal("while_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("return x;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// hard coded function to deallocate string array", visitor.GetCodeLines()[i++]);
            Assert.Equal("void deallocateArrayOfStrings(char ** arr, int * arr_size)", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int var_i = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("while_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("if (var_i >= *arr_size) goto while_exit;", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("free(arr[var_i]);", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_i = var_i + 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("goto while_entry;", visitor.GetCodeLines()[i++]);
            Assert.Equal("while_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("free(arr);", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// here are forward declarations for functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * function_reversearray(int * ret_arr_size, bool * var_arr, int * size_arr);", visitor.GetCodeLines()[i++]);
            Assert.Equal("void procedure_printarray(bool * var_arr, int * size_arr);", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// here are the definitions of functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * function_reversearray(int * ret_arr_size, bool * var_arr, int * size_arr)", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_0 = sizeof(bool);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_1 = *size_arr * tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * var_reversed = malloc(tmp_1);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int size_reversed = *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int var_j;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_2 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_i = tmp_2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_3 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_4 = *size_arr - tmp_3;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_j = tmp_4;", visitor.GetCodeLines()[i++]);
            Assert.Equal("label_while_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_5 = var_i < *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.Equal("if (!tmp_5) goto label_while_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * tmp_6 = &var_arr[var_i];", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_reversed[var_j] = *tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_7 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_8 = var_j - tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_j = tmp_8;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_9 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_10 = var_i + tmp_9;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_i = tmp_10;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("goto label_while_0_entry;", visitor.GetCodeLines()[i++]);
            Assert.Equal("label_while_0_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("*ret_arr_size = size_reversed;", visitor.GetCodeLines()[i++]);
            Assert.Equal("return var_reversed;", visitor.GetCodeLines()[i++]);
            Assert.Equal("free(var_reversed);", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("void procedure_printarray(bool * var_arr, int * size_arr)", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_11 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_i = tmp_11;", visitor.GetCodeLines()[i++]);
            Assert.Equal("label_while_1_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_12 = var_i < *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.Equal("if (!tmp_12) goto label_while_1_exit;", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("label_if_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * tmp_13 = &var_arr[var_i];", visitor.GetCodeLines()[i++]);
            Assert.Equal("if (!*tmp_13) goto label_else_0_entry;", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_14 = \"TRUE\";", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\\n\", tmp_14);", visitor.GetCodeLines()[i++]);
            Assert.Equal("goto label_if_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.Equal("label_else_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_15 = \"FALSE\";", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\\n\", tmp_15);", visitor.GetCodeLines()[i++]);
            Assert.Equal("label_if_0_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_16 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_17 = var_i + tmp_16;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_i = tmp_17;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("goto label_while_1_entry;", visitor.GetCodeLines()[i++]);
            Assert.Equal("label_while_1_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.Equal("int main()", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_18 = 4;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_19 = sizeof(bool);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_20 = tmp_18 * tmp_19;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * var_myarray1 = malloc(tmp_20);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_21 = sizeof(bool);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_22 = tmp_18 * tmp_21;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * var_myarray2 = malloc(tmp_22);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int size_myarray1 = tmp_18;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int size_myarray2 = tmp_18;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_23 = sizeof(bool);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_24 = 0 * tmp_23;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * var_myarray3 = malloc(tmp_24);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int size_myarray3 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_25 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_26 = 2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_27 = tmp_25 > tmp_26;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_28 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myarray1[tmp_28] = tmp_27;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_29 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_30 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_31 = tmp_29 == tmp_30;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_32 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myarray1[tmp_32] = tmp_31;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_33 = 2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myarray1[tmp_33] = true;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_34 = true > false;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_35 = 3;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myarray1[tmp_35] = tmp_34;", visitor.GetCodeLines()[i++]);
            Assert.Equal("free(var_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.Equal("size_myarray2 = size_myarray1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myarray2 = var_myarray1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("procedure_printarray(var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.Equal("procedure_printarray(var_myarray2, &size_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int size_tmp_36;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * tmp_36 = function_reversearray(&size_tmp_36, var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.Equal("free(var_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.Equal("size_myarray3 = size_tmp_36;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myarray3 = tmp_36;", visitor.GetCodeLines()[i++]);
            Assert.Equal("procedure_printarray(var_myarray3, &size_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_37 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * tmp_38 = &var_myarray1[tmp_37];", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_39 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * tmp_40 = &var_myarray1[tmp_39];", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_41 = *tmp_38 == *tmp_40;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_42 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myarray1[tmp_42] = tmp_41;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_43 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * tmp_44 = &var_myarray1[tmp_43];", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_45 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * tmp_46 = &var_myarray1[tmp_45];", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_47 = *tmp_44 >= *tmp_46;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_48 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myarray1[tmp_48] = tmp_47;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_49 = 2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * tmp_50 = &var_myarray1[tmp_49];", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_51 = 2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * tmp_52 = &var_myarray1[tmp_51];", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_53 = *tmp_50 <= *tmp_52;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_54 = 2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myarray1[tmp_54] = tmp_53;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_55 = 3;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * tmp_56 = &var_myarray1[tmp_55];", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_57 = 3;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool * tmp_58 = &var_myarray1[tmp_57];", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_59 = *tmp_56 != *tmp_58;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_60 = 3;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myarray1[tmp_60] = tmp_59;", visitor.GetCodeLines()[i++]);
            Assert.Equal("procedure_printarray(var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.Equal("procedure_printarray(var_myarray2, &size_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.Equal("procedure_printarray(var_myarray3, &size_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.Equal("free(tmp_36);", visitor.GetCodeLines()[i++]);
            Assert.Equal("free(var_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.Equal("return 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);

        }
    }
}
