using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program7Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program7.txt")]
        public void CodeGenerationWorksForProgram7()
        {
            string filename = "program7.txt";
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
            Assert.AreEqual("char * function_doublestring(char * var_value);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("void procedure_replacewithyolo(char * var_value);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            comment = "// here are the definitions of functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * function_doublestring(char * var_value)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_0 = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(tmp_0, var_value, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_1 = strlen(tmp_0);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_2 = 255 - tmp_1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncat(tmp_0, var_value, tmp_2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("void procedure_replacewithyolo(char * var_value)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_3 = \"Yolo\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(var_value, tmp_3, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * var_mystring1 = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * var_mystring2 = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * var_mystring3 = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_4 = \"Hello\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(var_mystring1, tmp_4, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_5 = \"World\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(var_mystring2, tmp_5, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("scanf(\"%s\", var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_6 = function_doublestring(var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(var_mystring1, tmp_6, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_replacewithyolo(var_mystring2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring3);", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("char * tmp_7 = \"xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(var_mystring1, tmp_7, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_8 = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(tmp_8, var_mystring1, 255);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_9 = strlen(tmp_8);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_10 = 255 - tmp_9;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncat(tmp_8, var_mystring1, tmp_10);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strncpy(var_mystring1, tmp_8, 255);", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("free(tmp_8);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_mystring2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_mystring1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);

        }
    }
}
