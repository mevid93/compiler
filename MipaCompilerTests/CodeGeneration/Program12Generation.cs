using System;
using Xunit;
using MipaCompiler;
using MipaCompiler.Node;

namespace MipaCompilerTests.CodeGeneration
{
    public class Program12Generation
    {
        [Fact]
        public void CodeGenerationWorksForProgram12()
        {
            string filename = "../../../SampleFiles/program12.txt";
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
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// here are the definitions of functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.Equal("", visitor.GetCodeLines()[i++]);

            Assert.Equal("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.Equal("int main()", visitor.GetCodeLines()[i++]);
            Assert.Equal("{", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_0 = sizeof(char);", visitor.GetCodeLines()[i++]);
            Assert.Equal("int tmp_1 = 256 * tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * var_password = malloc(tmp_1);", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_2 = \"Insert password:\";", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\\n\", tmp_2);", visitor.GetCodeLines()[i++]);
            Assert.Equal("scanf(\"%s\", var_password);", visitor.GetCodeLines()[i++]);
            Assert.Equal("label_assert_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_3 = \"YOLO123\";", visitor.GetCodeLines()[i++]);
            Assert.Equal("bool tmp_4 = 0 == strcmp(var_password, tmp_3);", visitor.GetCodeLines()[i++]);
            Assert.Equal("if (tmp_4) goto label_assert_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\", \"Assertion error at row 60... \");", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\\n\", \"Expected tmp_4 to be true!\");", visitor.GetCodeLines()[i++]);
            Assert.Equal("exit(1);", visitor.GetCodeLines()[i++]);
            Assert.Equal("label_assert_0_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.Equal("char * tmp_5 = \"ACCESS GRANTED!\";", visitor.GetCodeLines()[i++]);
            Assert.Equal("printf(\"%s\\n\", tmp_5);", visitor.GetCodeLines()[i++]);
            Assert.Equal("return 0;", visitor.GetCodeLines()[i++]);
            Assert.Equal("}", visitor.GetCodeLines()[i++]);

        }
    }
}
