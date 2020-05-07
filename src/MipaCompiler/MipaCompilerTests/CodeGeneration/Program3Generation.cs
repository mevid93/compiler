using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program3Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program3.txt")]
        public void CodeGenerationWorksForProgram3()
        {
            string filename = "program3.txt";
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

            string comment = "// here are forward declarations for functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int function_sum(int * var_data, int * size_data);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("void procedure_swap(int * var_i, int * var_j);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            comment = "// here are the definitions of functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int function_sum(int * var_data, int * size_data)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_sum;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_0 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_1 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_sum = tmp_1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_2 = var_i < *size_data;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_2) goto label_while_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int *tmp_3 = &var_data[var_i];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_4 = var_sum + *tmp_3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_sum = tmp_4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_5 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_6 = var_i + tmp_5;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_while_0_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_0_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return var_sum;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("void procedure_swap(int * var_i, int * var_j)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_tmp;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_tmp = *var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("*var_i = *var_j;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("*var_j = var_tmp;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_7 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int * var_a = malloc(tmp_7 * sizeof(int));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_a = tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_8 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_9 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("scanf(\"%d %d\", &var_a[tmp_8], &var_a[tmp_9]);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_10 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int *tmp_11 = &var_a[tmp_10];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_12 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int *tmp_13 = &var_a[tmp_12];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_swap(tmp_11, tmp_13);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_15 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int *tmp_16 = &var_a[tmp_15];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_17 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int *tmp_18 = &var_a[tmp_17];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%d%d\\n\", *tmp_16, *tmp_18);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char tmp_19[256] = \"Sum is \";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_20 = function_sum(var_a, &size_a);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s%d\\n\", tmp_19, tmp_20);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_a);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            
        }
    }
}
