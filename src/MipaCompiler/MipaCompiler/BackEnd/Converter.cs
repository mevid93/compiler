using System;

namespace MipaCompiler.BackEnd
{
    /// <summary>
    /// Class <c>Converter</c> contains static methods that can be used to convert some of the
    /// simple values, types or expressions to equivalent C-code. Its purpose is to reduce the
    /// amount of duplicate code in different AST nodes.
    /// </summary>
    public class Converter
    {
        // Mini-Pascal type defnitions
        const string STR_BOOLEAN = "boolean";
        const string STR_INTEGER = "integer";
        const string STR_REAL = "real";
        const string STR_STRING = "string";
        const string STR_ARRAY_BOOLEAN = "array[] of boolean";
        const string STR_ARRAY_INTEGER = "array[] of integer";
        const string STR_ARRAY_REAL = "array[] of real";
        const string STR_ARRAY_STRING = "array[] of string";

        /// <summary>
        /// Static method <c>ConvertReturnTypeToC</c> converts Mini-Pascal function return
        /// type to equivalent type in C-language.
        /// </summary>
        /// <param name="type">type in Mini-Pascal</param>
        /// <returns>C-language type</returns>
        public static string ConvertReturnTypeToC(string type)
        {
            switch (type)
            {
                case STR_STRING:
                    return "char *";
                case STR_REAL:
                    return "double";
                case STR_INTEGER:
                    return "int";
                case STR_BOOLEAN:
                    return "bool";
                case STR_ARRAY_STRING:
                    return "char **";
                case STR_ARRAY_REAL:
                    return "double *";
                case STR_ARRAY_INTEGER:
                    return "int *";
                case STR_ARRAY_BOOLEAN:
                    return "bool *";
                default:
                    throw new Exception("Unexpected error: Invalid Mini-Pascal return type!");
            }
        }

        /// <summary>
        /// Static method <c>ConvertParameterTypeToC</c> converts Mini-Pascal function
        /// parameter te equivalent C-language type.
        /// type to C-language.
        /// </summary>
        /// <param name="type">parameter type in Mini-Pascal</param>
        /// <returns>parameter type in C-language</returns>
        public static string ConvertParameterTypeToC(string type)
        {
            switch (type)
            {
                case STR_STRING:
                    return "char *";
                case STR_REAL:
                    return "double *";
                case STR_INTEGER:
                    return "int *";
                case STR_BOOLEAN:
                    return "bool *";
                case STR_ARRAY_STRING:
                    return "char **";
                case STR_ARRAY_REAL:
                    return "double *";
                case STR_ARRAY_INTEGER:
                    return "int *";
                case STR_ARRAY_BOOLEAN:
                    return "bool *";
                default:
                    throw new Exception("Unexpected error: Invalid Mini-Pascal parameter type!");
            }
        }

        /// <summary>
        /// Static method <c>ConvertSimpleTypeToC</c> converts Mini-Pascal simple type to 
        /// C-language equivalent.
        /// </summary>
        /// <param name="simpleType">simple type in Mini-Pascal</param>
        /// <returns>simple type in C-language</returns>
        public static string ConvertSimpleTypeToC(string simpleType)
        {
            switch (simpleType)
            {
                case STR_STRING:
                    return "char";
                case STR_REAL:
                    return "double";
                case STR_INTEGER:
                    return "int";
                case STR_BOOLEAN:
                    return "bool";
                default:
                    throw new Exception($"Unexpected error: Invalid Mini-Pascal simple type!");
            }
        }

        /// <summary>
        /// Static method <c>GetPrefixWhenPointerNeeded</c> returns C-language prefix for variable
        /// when pointer is needed. Method checks the variable type and pointer information,
        /// and returns a prefix, so that the prefix + variable name is a pointer.
        /// </summary>
        /// <param name="variableType">type of the variable</param>
        /// <param name="isPointer">is pointer</param>
        /// <returns>prefix for variable name</returns>
        public static string GetPrefixWhenPointerNeeded(string variableType, bool isPointer)
        {
            if (isPointer) return "";

            switch (variableType)
            {
                case STR_STRING:
                    return "&";
                case STR_REAL:
                    return "&";
                case STR_INTEGER:
                    return "&";
                case STR_BOOLEAN:
                    return "&";
                case STR_ARRAY_STRING:
                    return "";
                case STR_ARRAY_REAL:
                    return "";
                case STR_ARRAY_INTEGER:
                    return "";
                case STR_ARRAY_BOOLEAN:
                    return "";
                default:
                    throw new Exception("Unexpected error: Invalid Mini-Pascal return type!");
            }
        }

        /// <summary>
        /// Static method <c>GetPrefixWhenPointerNotNeeded</c> returns C-language prefix for variable
        /// when pointer is not needed. Method checks the variable type and pointer information,
        /// and returns a prefix, so that the prefix + variable name is not a pointer.
        /// </summary>
        /// <param name="variableType">type of the variable</param>
        /// <param name="isPointer">is pointer</param>
        /// <returns>prefix for variable name</returns>
        public static string GetPrefixWhenPointerNotNeeded(string variableType, bool isPointer)
        {
            if (isPointer) return "*";

            switch (variableType)
            {
                case STR_STRING:
                    return "";
                case STR_REAL:
                    return "";
                case STR_INTEGER:
                    return "";
                case STR_BOOLEAN:
                    return "";
                case STR_ARRAY_STRING:
                    return "";
                case STR_ARRAY_REAL:
                    return "";
                case STR_ARRAY_INTEGER:
                    return "";
                case STR_ARRAY_BOOLEAN:
                    return "";
                default:
                    throw new Exception("Unexpected error: Invalid Mini-Pascal type!");
            }
        }

        /// <summary>
        /// Method <c>ConvertDeclarationTypeToC</c> converts Mini-Pascal variable declaration
        /// type into equivalent C-language type.
        /// </summary>
        /// <param name="type">variable declaration type</param>
        /// <returns>declaration type</returns>
        public static string ConvertDeclarationTypeToC(string type)
        {
            switch (type)
            {
                case STR_STRING:
                    return "char *";
                case STR_REAL:
                    return "double";
                case STR_INTEGER:
                    return "int";
                case STR_BOOLEAN:
                    return "bool";
                case STR_ARRAY_STRING:
                    return "char **";
                case STR_ARRAY_REAL:
                    return "double *";
                case STR_ARRAY_INTEGER:
                    return "int *";
                case STR_ARRAY_BOOLEAN:
                    return "bool *";
                default:
                    throw new Exception("Unexpected error: Invalid Mini-Pascal declaration type!");
            }
        }

        /// <summary>
        /// Method <c>GetStringBufferSize</c> returns string buffer size in C-language.
        /// </summary>
        /// <returns></returns>
        public static string GetStringBufferSize()
        {
            return "[256]";
        }

        /// <summary>
        /// Method <c>ConvertTypeToMallocTypeInC</c> converts Mini-Pascal type into
        /// C-language type that is used to allocate memoery.
        /// </summary>
        /// <param name="type">Mini-Pascal type</param>
        /// <returns>type of malloc</returns>
        public static string ConvertTypeToMallocTypeInC(string type)
        {
            switch (type)
            {
                case STR_STRING:
                    return "char *";
                case STR_REAL:
                    return "double";
                case STR_INTEGER:
                    return "int";
                case STR_BOOLEAN:
                    return "bool";
                case STR_ARRAY_STRING:
                    return "char *";
                case STR_ARRAY_REAL:
                    return "double";
                case STR_ARRAY_INTEGER:
                    return "int";
                case STR_ARRAY_BOOLEAN:
                    return "bool";
                default:
                    throw new Exception("Unexpected error: Invalid Mini-Pascal malloc type!");
            }
        }
    }
}
