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
            Assert.AreEqual("strcpy(tmp_0, var_value);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strcat(tmp_0, var_value);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("void procedure_replacewithyolo(char * var_value)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_1 = \"Yolo\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strcpy(var_value, tmp_1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * var_mystring1 = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * var_mystring2 = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * var_mystring3 = malloc(256 * sizeof(char));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_2 = \"Hello\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strcpy(var_mystring1, tmp_2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_3 = \"World\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strcpy(var_mystring2, tmp_3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("scanf(\"%s\", var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_4 = function_doublestring(var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("strcpy(var_mystring1, tmp_4);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_replacewithyolo(var_mystring2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_mystring3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_mystring2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_mystring1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);

        }
    }
}
