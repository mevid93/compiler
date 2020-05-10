using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program6Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program6.txt")]
        public void CodeGenerationWorksForProgram4()
        {
            string filename = "program6.txt";
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
            Assert.AreEqual("bool function_opposite(bool * var_value);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("void procedure_opposite2(bool * var_value);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            comment = "// here are the definitions of functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool function_opposite(bool * var_value)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_0 = !*var_value;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("void procedure_opposite2(bool * var_value)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_1 = !*var_value;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("*var_value = tmp_1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_myboolean1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_boolean;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myboolean1 = false;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_boolean = true;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myboolean2 = var_boolean;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_2 = !var_myboolean1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myboolean1 = tmp_2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_3 = var_boolean && var_myboolean1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_boolean = tmp_3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_4 = var_myboolean1 || var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myboolean2 = tmp_4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_5 = function_opposite(&var_myboolean1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_boolean = tmp_5;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_opposite2(&var_boolean);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_comparison;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_6 = var_myboolean1 == var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_7 = var_myboolean1 != var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_8 = var_myboolean1 < var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_8;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_9 = var_myboolean1 > var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_9;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_10 = var_myboolean1 <= var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_10;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_11 = var_myboolean1 >= var_myboolean2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_11;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            
        }
    }
}
