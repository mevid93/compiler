﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program12Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program12.txt")]
        public void CodeGenerationWorksForProgram12()
        {
            string filename = "program12.txt";
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
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here are the definitions of functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * var_password = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_0 = \"Insert password:\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", tmp_0);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("scanf(\"%s\", var_password);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_assert_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_1 = \"YOLO123\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_2 = 0 == strcmp(var_password, tmp_1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (tmp_2) goto label_assert_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\", \"Assertion error at row 54... \");", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", \"Expected tmp_2 to be true!\");", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("exit(1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_assert_0_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_3 = \"ACCESS GRANTED!\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", tmp_3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_password);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);


        }
    }
}
