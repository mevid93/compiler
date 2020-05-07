﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program5Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program5.txt")]
        public void CodeGenerationWorksForProgram4()
        {
            string filename = "program5.txt";
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
            Assert.AreEqual("double function_square(double * var_value);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("void procedure_increase(double * var_value);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            comment = "// here are the definitions of functions and procedures (if any exists)";
            Assert.AreEqual(comment, visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double function_square(double * var_value)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double tmp_0 = *var_value * *var_value;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("void procedure_increase(double * var_value)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_1 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double tmp_2 = *var_value + tmp_1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("*var_value = tmp_2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double var_myreal1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double var_myreal2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double tmp_3 = 100.0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myreal1 = tmp_3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("scanf(\"%lf\", &var_myreal2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double var_sum;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double var_sub;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double var_mul;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double var_div;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double tmp_4 = var_myreal1 + var_myreal2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_sum = tmp_4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double tmp_5 = var_myreal1 - var_myreal2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_sub = tmp_5;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double tmp_6 = var_myreal1 * var_myreal2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_mul = tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("double tmp_7 = var_myreal1 / var_myreal2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_div = tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool var_comparison;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_8 = var_myreal1 == var_myreal2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_8;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_9 = var_myreal1 != var_myreal2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_9;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_10 = var_myreal1 < var_myreal2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_10;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_11 = var_myreal1 > var_myreal2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_11;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_12 = var_myreal1 <= var_myreal2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_12;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_13 = var_myreal1 >= var_myreal2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_comparison = tmp_13;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%lf\\n\", var_sum);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
        }
    }
}