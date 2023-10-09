using System;
using Xunit;
using MipaCompiler;
using MipaCompiler.Node;

namespace MipaCompilerTests.CodeGeneration
{
    public class Program7Generation
    {
        [Fact]
        public void CodeGenerationWorksForProgram7()
        {
            string filename = "../../../SampleFiles/program7.txt";
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
            Assert.Equal("char * function_doublestring(char * var_value);", visitor.GetCodeLines()[i++]);
            Assert.Equal("void procedure_replacewithyolo(char * var_value);", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// here are the definitions of functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * function_doublestring(char * var_value)", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_1 = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_2 = 256 * tmp_1;", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_0 = malloc(tmp_2);", visitor.GetCodeLines()[i++]);
            Assert.Equal("strncpy(tmp_0, var_value, 255);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_3 = strlen(tmp_0);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_4 = 255 - tmp_3;", visitor.GetCodeLines()[i++]);
            Assert.Equal("strncat(tmp_0, var_value, tmp_4);", visitor.GetCodeLines()[i++]);
            Assert.Equal("return tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("void procedure_replacewithyolo(char * var_value)", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_5 = \"Yolo\";", visitor.GetCodeLines()[i++]);
            Assert.Equal("strncpy(var_value, tmp_5, 255);", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.Equal("int main()", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_6 = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_7 = 256 * tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * var_mystring1 = malloc(tmp_7);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_8 = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_9 = 256 * tmp_8;", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * var_mystring2 = malloc(tmp_9);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_10 = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_11 = 256 * tmp_10;", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * var_mystring3 = malloc(tmp_11);", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_12 = \"Hello\";", visitor.GetCodeLines()[i++]);
            Assert.Equal("strncpy(var_mystring1, tmp_12, 255);", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_13 = \"World\";", visitor.GetCodeLines()[i++]);
            Assert.Equal("strncpy(var_mystring2, tmp_13, 255);", visitor.GetCodeLines()[i++]);
            Assert.Equal("scanf(\"%s\", var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\\n\", var_mystring1);", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\\n\", var_mystring2);", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\\n\", var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_14 = function_doublestring(var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.Equal("strncpy(var_mystring1, tmp_14, 255);", visitor.GetCodeLines()[i++]);
            Assert.Equal("procedure_replacewithyolo(var_mystring2);", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\\n\", var_mystring1);", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\\n\", var_mystring2);", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\\n\", var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_15 = \"xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\";", visitor.GetCodeLines()[i++]);
            Assert.Equal("strncpy(var_mystring1, tmp_15, 255);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_17 = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_18 = 256 * tmp_17;", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_16 = malloc(tmp_18);", visitor.GetCodeLines()[i++]);
            Assert.Equal("strncpy(tmp_16, var_mystring1, 255);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_19 = strlen(tmp_16);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_20 = 255 - tmp_19;", visitor.GetCodeLines()[i++]);
            Assert.Equal("strncat(tmp_16, var_mystring1, tmp_20);", visitor.GetCodeLines()[i++]);
            Assert.Equal("strncpy(var_mystring1, tmp_16, 255);", visitor.GetCodeLines()[i++]);
            Assert.Equal("free(tmp_16);", visitor.GetCodeLines()[i++]);
            Assert.Equal("return 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);


        }
    }
}
