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
        /// <summary>
        /// Static method <c>ConvertTypeToTargetLanguage</c> converts Mini-Pascal type
        /// to C-language type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>C-language type</returns>
        public static string ConvertReturnTypeToTargetLanguage(string type)
        {
            switch (type)
            {
                case "string":
                    return "const char *";
                case "real":
                    return "double";
                case "integer":
                    return "int";
                case "boolean":
                    return "bool";
                case "array[] of string":
                    return "char **";
                case "array[] of real":
                    return "double *";
                case "array[] of integer":
                    return "int *";
                case "array[] of boolean":
                    return "bool *";
                default:
                    throw new Exception("Unexpected error... Invalid return type!");
            }
        }

        /// <summary>
        /// Static method <c>ConvertParameterTypeToTargetLanguage</c> converts parameter
        /// type to C-language.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>parameter type in C-language</returns>
        public static string ConvertParameterTypeToTargetLanguage(string type)
        {
            switch (type)
            {
                case "string":
                    return "const char *";
                case "real":
                    return "double *";
                case "integer":
                    return "int *";
                case "boolean":
                    return "bool";
                case "array[] of string":
                    return "char **";
                case "array[] of real":
                    return "double *";
                case "array[] of integer":
                    return "int *";
                case "array[] of boolean":
                    return "bool *";
                default:
                    throw new Exception("Unexpected error... Invalid parameter type!");
            }
        }

        /// <summary>
        /// Static method <c>ConvertSimpleTypeToTargetLanguage</c> converts simple type to 
        /// C-language equivalent.
        /// </summary>
        /// <param name="simpleType">simple type in Mini-Pascal</param>
        /// <returns>simple type</returns>
        public static string ConvertSimpleTypeToTargetLanguage(string simpleType)
        {
            switch (simpleType)
            {
                case "string":
                    return "char";
                case "real":
                    return "double";
                case "integer":
                    return "int";
                case "boolean":
                    return "bool";
                default:
                    throw new Exception($"Unexpected error... Invalid simple type {simpleType}!");
            }
        }

        /// <summary>
        /// Static method <c>GetPrefixForArgumentByType</c> returns prefix for given
        /// argument type.
        /// </summary>
        /// <param name="evaluatedType">argument type</param>
        /// <param name="isParameter">is argument also a parameter</param>
        /// <returns>argument prefix</returns>
        public static string GetPrefixForArgumentByType(string evaluatedType, bool isParameter)
        {
            if (isParameter) return "";

            switch (evaluatedType)
            {
                case "boolean":
                case "real":
                case "integer":
                    return "&";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Static method <c>GetPrefixForVariable</c> returns prefix for variable. 
        /// </summary>
        /// <param name="type">variable type</param>
        /// <param name="isParameter">is variable a parameter</param>
        /// <returns>prefix</returns>
        public static string GetPrefixForVariable(string variableType, bool isParameter)
        {
            switch (variableType)
            {
                case "integer":
                case "boolean":
                case "string":
                case "real":
                    if (isParameter) return "*";
                    return "";
                default:
                    return "";
            }
        }

    }
}
