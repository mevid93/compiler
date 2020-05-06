using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program4Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program4.txt")]
        public void CodeGenerationWorksForProgram4()
        {
            string filename = "program4.txt";
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
            Assert.AreEqual("", visitor.GetCodeLines()[7]);

            comment = "// here are the definitions of functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[8]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[9]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[10]);
            Assert.AreEqual("{", visitor.GetCodeLines()[11]);
            Assert.AreEqual("int tmp_7 = 2;", visitor.GetCodeLines()[46]);
            Assert.AreEqual("int var_a[tmp_7];", visitor.GetCodeLines()[47]);
            Assert.AreEqual("int size_a = tmp_7;", visitor.GetCodeLines()[48]);
            Assert.AreEqual("int tmp_8 = 0;", visitor.GetCodeLines()[49]);
            Assert.AreEqual("int tmp_9 = 1;", visitor.GetCodeLines()[50]);
            Assert.AreEqual("scanf(\"%d %d\", &var_a[tmp_8], &var_a[tmp_9]);", visitor.GetCodeLines()[51]);
            Assert.AreEqual("int tmp_10 = 0;", visitor.GetCodeLines()[52]);
            Assert.AreEqual("int *tmp_11 = (int *) &var_a + tmp_10;", visitor.GetCodeLines()[53]);
            Assert.AreEqual("int tmp_12 = 1;", visitor.GetCodeLines()[54]);
            Assert.AreEqual("int *tmp_13 = (int *) &var_a + tmp_12;", visitor.GetCodeLines()[55]);
            Assert.AreEqual("procedure_swap(tmp_11, tmp_13);", visitor.GetCodeLines()[56]);
            Assert.AreEqual("int tmp_15 = 0;", visitor.GetCodeLines()[57]);
            Assert.AreEqual("int *tmp_16 = (int *) &var_a + tmp_15;", visitor.GetCodeLines()[58]);
            Assert.AreEqual("int tmp_17 = 1;", visitor.GetCodeLines()[59]);
            Assert.AreEqual("int *tmp_18 = (int *) &var_a + tmp_17;", visitor.GetCodeLines()[60]);
            Assert.AreEqual("printf(\"%d%d\\n\", *tmp_16, *tmp_18);", visitor.GetCodeLines()[61]);
            Assert.AreEqual("char tmp_19[256] = \"Sum is \";", visitor.GetCodeLines()[62]);
            Assert.AreEqual("int tmp_20 = function_sum(var_a, &size_a);", visitor.GetCodeLines()[63]);
            Assert.AreEqual("printf(\"%s%d\\n\", tmp_19, tmp_20);", visitor.GetCodeLines()[64]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[65]);
            Assert.AreEqual("}", visitor.GetCodeLines()[66]);
            
        }
    }
}
