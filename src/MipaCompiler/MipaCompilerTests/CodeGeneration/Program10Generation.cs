using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;
using MipaCompiler.Node;
using System;

namespace MipaCompilerTests.CodeGeneration
{
    [TestClass]
    public class Program10Generation
    {
        [TestMethod]
        [DeploymentItem("SampleFiles\\program10.txt")]
        public void CodeGenerationWorksForProgram10()
        {
            string filename = "program10.txt";
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

            Assert.AreEqual("// here are forward declarations for functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * function_reversearray(int * ret_arr_size, bool * var_arr, int * size_arr);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("void procedure_printarray(bool * var_arr, int * size_arr);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here are the definitions of functions and procedures (if any exists)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * function_reversearray(int * ret_arr_size, bool * var_arr, int * size_arr)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * var_reversed = malloc(*size_arr * sizeof(bool));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_reversed = *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_j;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_0 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_1 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_2 = *size_arr - tmp_1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_j = tmp_2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_3 = var_i < *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_3) goto label_while_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * tmp_4 = &var_arr[var_i];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_reversed[var_j] = *tmp_4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_5 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_6 = var_j - tmp_5;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_j = tmp_6;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_7 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_8 = var_i + tmp_7;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_8;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_while_0_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_0_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("*ret_arr_size = size_reversed;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return var_reversed;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_reversed);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("void procedure_printarray(bool * var_arr, int * size_arr)", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int var_i;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_9 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_9;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_1_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_10 = var_i < *size_arr;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!tmp_10) goto label_while_1_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * tmp_11 = &var_arr[var_i];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("if (!*tmp_11) goto label_else_0_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_12 = \"TRUE\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", tmp_12);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_if_0_exit;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_else_0_entry: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("char * tmp_13 = \"FALSE\";", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("printf(\"%s\\n\", tmp_13);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_if_0_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_14 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_15 = var_i + tmp_14;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_i = tmp_15;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("goto label_while_1_entry;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("label_while_1_exit: ;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("", visitor.GetCodeLines()[i++]);

            Assert.AreEqual("// here is the main function", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int main()", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("{", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_16 = 4;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * var_myarray1 = malloc(tmp_16 * sizeof(bool));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * var_myarray2 = malloc(tmp_16 * sizeof(bool));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_myarray1 = tmp_16;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_myarray2 = tmp_16;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * var_myarray3 = malloc(0 * sizeof(bool));", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_myarray3 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_17 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_18 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_19 = tmp_17 > tmp_18;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_20 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_20] = tmp_19;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_21 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_22 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_23 = tmp_21 == tmp_22;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_24 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_24] = tmp_23;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_25 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_25] = true;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_26 = true > false;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_27 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_27] = tmp_26;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("size_myarray2 = size_myarray1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray2 = var_myarray1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray2, &size_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int size_tmp_28;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * tmp_28 = function_reversearray(&size_tmp_28, var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("size_myarray3 = size_tmp_28;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray3 = tmp_28;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray3, &size_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_29 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * tmp_30 = &var_myarray1[tmp_29];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_31 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * tmp_32 = &var_myarray1[tmp_31];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_33 = *tmp_30 == *tmp_32;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_34 = 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_34] = tmp_33;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_35 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * tmp_36 = &var_myarray1[tmp_35];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_37 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * tmp_38 = &var_myarray1[tmp_37];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_39 = *tmp_36 >= *tmp_38;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_40 = 1;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_40] = tmp_39;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_41 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * tmp_42 = &var_myarray1[tmp_41];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_43 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * tmp_44 = &var_myarray1[tmp_43];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_45 = *tmp_42 <= *tmp_44;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_46 = 2;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_46] = tmp_45;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_47 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * tmp_48 = &var_myarray1[tmp_47];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_49 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool * tmp_50 = &var_myarray1[tmp_49];", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("bool tmp_51 = *tmp_48 != *tmp_50;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("int tmp_52 = 3;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("var_myarray1[tmp_52] = tmp_51;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray1, &size_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray2, &size_myarray2);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("procedure_printarray(var_myarray3, &size_myarray3);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(tmp_28);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("free(var_myarray1);", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("return 0;", visitor.GetCodeLines()[i++]);
            Assert.AreEqual("}", visitor.GetCodeLines()[i++]);

        }
    }
}
