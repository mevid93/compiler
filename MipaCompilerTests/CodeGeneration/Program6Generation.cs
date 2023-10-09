using System;
using Xunit;
using MipaCompiler;
using MipaCompiler.Node;

namespace MipaCompilerTests.CodeGeneration
{
    public class Program6Generation
    {
        [Fact]
        public void CodeGenerationWorksForProgram6()
        {
            string filename = "../../../SampleFiles/program6.txt";
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
            Assert.Equal("bool function_opposite(bool * var_value);", visitor.GetCodeLines()[i++]);
            Assert.Equal("void procedure_opposite2(bool * var_value);", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// here are the definitions of functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool function_opposite(bool * var_value)", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_0 = !*var_value;", visitor.GetCodeLines()[i++]);
            Assert.Equal("return tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("void procedure_opposite2(bool * var_value)", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_1 = !*var_value;", visitor.GetCodeLines()[i++]);
            Assert.Equal("*var_value = tmp_1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);


            Assert.Equal("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.Equal("int main()", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool var_myboolean1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool var_boolean;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myboolean1 = false;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_boolean = true;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myboolean2 = var_boolean;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_2 = !var_myboolean1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myboolean1 = tmp_2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_3 = var_boolean && var_myboolean1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_boolean = tmp_3;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_4 = var_myboolean1 || var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_myboolean2 = tmp_4;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_5 = function_opposite(&var_myboolean1);", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_boolean = tmp_5;", visitor.GetCodeLines()[i++]);
            Assert.Equal("procedure_opposite2(&var_boolean);", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool var_comparison;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_6 = var_myboolean1 == var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_comparison = tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_7 = var_myboolean1 != var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_comparison = tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_8 = var_myboolean1 < var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_comparison = tmp_8;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_9 = var_myboolean1 > var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_comparison = tmp_9;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_10 = var_myboolean1 <= var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_comparison = tmp_10;", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_11 = var_myboolean1 >= var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.Equal("var_comparison = tmp_11;", visitor.GetCodeLines()[i++]);
            Assert.Equal("return 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);


        }
    }
}
