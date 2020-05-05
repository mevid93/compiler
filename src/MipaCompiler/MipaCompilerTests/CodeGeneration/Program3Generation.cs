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
            Assert.AreEqual("#include <stdio.h>", visitor.GetCodeLines()[0]);
            Assert.AreEqual("", visitor.GetCodeLines()[1]);
            Assert.AreEqual("typedef int bool;", visitor.GetCodeLines()[2]);
            Assert.AreEqual("#define true 1", visitor.GetCodeLines()[3]);
            Assert.AreEqual("#define false 0", visitor.GetCodeLines()[4]);
            Assert.AreEqual("", visitor.GetCodeLines()[5]);

            string comment = "// here are forward declarations for functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[6]);
            Assert.AreEqual("int function_sum(int * var_data, int * size_data);", visitor.GetCodeLines()[7]);
            Assert.AreEqual("void procedure_swap(int * var_i, int * var_j);", visitor.GetCodeLines()[8]);
            Assert.AreEqual("", visitor.GetCodeLines()[9]);

            comment = "// here are the definitions of functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[10]);
            Assert.AreEqual("int function_sum(int * var_data, int * size_data)", visitor.GetCodeLines()[11]);
            Assert.AreEqual("{", visitor.GetCodeLines()[12]);
            Assert.AreEqual("int var_i, var_sum;", visitor.GetCodeLines()[13]);
            Assert.AreEqual("var_i = 0;", visitor.GetCodeLines()[14]);
            Assert.AreEqual("var_sum = 0;", visitor.GetCodeLines()[15]);
            Assert.AreEqual("label_while_0_entry: ;", visitor.GetCodeLines()[16]);
            Assert.AreEqual("bool tmp_0 = var_i < *size_data;", visitor.GetCodeLines()[17]);
            Assert.AreEqual("if (!tmp_0) goto label_while_0_exit;", visitor.GetCodeLines()[18]);
            Assert.AreEqual("{", visitor.GetCodeLines()[19]);
            Assert.AreEqual("int tmp_1 = var_data[var_i];", visitor.GetCodeLines()[20]);
            Assert.AreEqual("int tmp_2 = var_sum + tmp_1;", visitor.GetCodeLines()[21]);
            Assert.AreEqual("var_sum = tmp_2;", visitor.GetCodeLines()[22]);
            Assert.AreEqual("int tmp_3 = var_i + 1;", visitor.GetCodeLines()[23]);
            Assert.AreEqual("var_i = tmp_3;", visitor.GetCodeLines()[24]);
            Assert.AreEqual("}", visitor.GetCodeLines()[25]);
            Assert.AreEqual("goto label_while_0_entry;", visitor.GetCodeLines()[26]);
            Assert.AreEqual("label_while_0_exit: ;", visitor.GetCodeLines()[27]);
            Assert.AreEqual("return var_sum;", visitor.GetCodeLines()[28]);
            Assert.AreEqual("}", visitor.GetCodeLines()[29]);
            Assert.AreEqual("", visitor.GetCodeLines()[30]);

            Assert.AreEqual("void procedure_swap(int * var_i, int * var_j)", visitor.GetCodeLines()[31]);
            Assert.AreEqual("{", visitor.GetCodeLines()[32]);
            Assert.AreEqual("int var_tmp;", visitor.GetCodeLines()[33]);
            Assert.AreEqual("var_tmp = *var_i;", visitor.GetCodeLines()[34]);
            Assert.AreEqual("*var_i = *var_j;", visitor.GetCodeLines()[35]);
            Assert.AreEqual("*var_j = var_tmp;", visitor.GetCodeLines()[36]);
            Assert.AreEqual("}", visitor.GetCodeLines()[37]);
            Assert.AreEqual("", visitor.GetCodeLines()[38]);
            Assert.AreEqual("", visitor.GetCodeLines()[39]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[40]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[41]);
            Assert.AreEqual("{", visitor.GetCodeLines()[42]);
            Assert.AreEqual("int var_a[2];", visitor.GetCodeLines()[43]);
            Assert.AreEqual("int size_a = 2;", visitor.GetCodeLines()[44]);
            Assert.AreEqual("scanf(\"%d %d\", &var_a[0], &var_a[1]);", visitor.GetCodeLines()[45]);
            Assert.AreEqual("int tmp_4 = var_a[0];", visitor.GetCodeLines()[46]);
            Assert.AreEqual("int tmp_5 = var_a[1];", visitor.GetCodeLines()[47]);
            Assert.AreEqual("procedure_swap(&tmp_4, &tmp_5);", visitor.GetCodeLines()[48]);
            Assert.AreEqual("int tmp_7 = var_a[0];", visitor.GetCodeLines()[49]);
            Assert.AreEqual("int tmp_8 = var_a[1];", visitor.GetCodeLines()[50]);
            Assert.AreEqual("printf(\"%d%d\\n\", tmp_7, tmp_8);", visitor.GetCodeLines()[51]);
            Assert.AreEqual("int tmp_9 = function_sum(var_a, &size_a);", visitor.GetCodeLines()[52]);
            Assert.AreEqual("printf(\"%s%d\\n\", \"Sum is \", tmp_9);", visitor.GetCodeLines()[53]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[54]);
            Assert.AreEqual("}", visitor.GetCodeLines()[55]);
            
        }
    }
}
