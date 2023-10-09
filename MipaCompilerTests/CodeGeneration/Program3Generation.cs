using System;
using Xunit;
using MipaCompiler;
using MipaCompiler.Node;

namespace MipaCompilerTests.CodeGeneration
{
    public class Program3Generation
    {
        [Fact]
        public void CodeGenerationWorksForProgram3()
        {
            string filename = "../../../SampleFiles/program3.txt";
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
            Assert.Equal("int function_sum(int * var_data, int * size_data);", visitor.GetCodeLines()[i++]);
            Assert.Equal("void procedure_swap(int * var_i, int * var_j);", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// here are the definitions of functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.Equal("int function_sum(int * var_data, int * size_data)", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int var_sum;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_0 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_i = tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_1 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_sum = tmp_1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("label_while_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_2 = var_i < *size_data;", visitor.GetCodeLines()[i++]);
            Assert.Equal("if (!tmp_2) goto label_while_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int * tmp_3 = &var_data[var_i];", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_4 = var_sum + *tmp_3;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_sum = tmp_4;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_5 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_6 = var_i + tmp_5;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_i = tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("goto label_while_0_entry;", visitor.GetCodeLines()[i++]);
            Assert.Equal("label_while_0_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("return var_sum;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("void procedure_swap(int * var_i, int * var_j)", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int var_tmp;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_tmp = *var_i;", visitor.GetCodeLines()[i++]);
            Assert.Equal("*var_i = *var_j;", visitor.GetCodeLines()[i++]);
            Assert.Equal("*var_j = var_tmp;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.Equal("int main()", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_7 = 2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_8 = sizeof(int);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_9 = tmp_7 * tmp_8;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int * var_a = malloc(tmp_9);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int size_a = tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_10 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_11 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("scanf(\"%d %d\", &var_a[tmp_10], &var_a[tmp_11]);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_12 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int * tmp_13 = &var_a[tmp_12];", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_14 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int * tmp_15 = &var_a[tmp_14];", visitor.GetCodeLines()[i++]);
            Assert.Equal("procedure_swap(tmp_13, tmp_15);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_16 = 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int * tmp_17 = &var_a[tmp_16];", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_18 = 1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("int * tmp_19 = &var_a[tmp_18];", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%d%d\\n\", *tmp_17, *tmp_19);", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_20 = \"Sum is \";", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_21 = function_sum(var_a, &size_a);", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s%d\\n\", tmp_20, tmp_21);", visitor.GetCodeLines()[i++]);
            Assert.Equal("free(var_a);", visitor.GetCodeLines()[i++]);
            Assert.Equal("return 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);

        }
    }
}
