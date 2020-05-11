using MipaCompiler.Symbol;
using System;
using System.Collections.Generic;

namespace MipaCompiler.BackEnd
{
    /// <summary>
    /// Class <c>Helper</c> contains static methods that can be used to convert some of the
    /// simple values, types or expressions to equivalent C-code. Its purpose is to reduce the
    /// amount of duplicate code in different AST nodes.
    /// </summary>
    public class Helper
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
                    return "";
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
        /// Static method <c>ConvertDeclarationTypeToC</c> converts Mini-Pascal variable declaration
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
        /// Static method <c>GetStringBufferSize</c> returns string buffer size in C-language.
        /// </summary>
        /// <returns></returns>
        public static string GetStringBufferSize()
        {
            return "[256]";
        }

        /// <summary>
        /// Static method <c>ConvertTypeToMallocTypeInC</c> converts Mini-Pascal type into
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

        /// <summary>
        /// Static method <c>GetElementTypeFromArrayTypeInC</c> converts Mini-Pascal array type
        /// to C-language array element type.
        /// </summary>
        /// <param name="miniPascalArrayType">array declaration type</param>
        public static string GetElementTypeFromArrayTypeInC(String miniPascalArrayType)
        {
            switch (miniPascalArrayType)
            {
                case STR_ARRAY_STRING:
                    return "char";
                case STR_ARRAY_REAL:
                    return "double";
                case STR_ARRAY_INTEGER:
                    return "int";
                case STR_ARRAY_BOOLEAN:
                    return "bool";
                default:
                    throw new Exception("Unexpected error: Invalid Mini-Pascal array type!");
            }
        }

        /// <summary>
        /// Static method <c>FreeStrings</c> frees all strings before return statement.
        /// Second parameter is optional and meant for strings that are returned
        /// and should not be freed. String that is skipped, is removed from the
        /// list of allocated strings.
        /// </summary>
        /// <param name="visitor">visitor object</param>
        /// <param name="skipString">string that should not be freed (optional)</param>
        public static void FreeAllocatedStrings(Visitor visitor, bool freeBeforeReturn, string skipString = null)
        {
            // get list of allocated strings
            List<string> allocatedStrings = visitor.GetAllocatedStrings();

            // get symbol table
            SymbolTable symTable = visitor.GetSymbolTable();

            // get scope level that is one below current level
            int thresholdScope = symTable.GetCurrentScope() - 1;

            // free and remove all strings from list that are above threshold scope.
            // if freeBeforeReturn is true --> then free also other strings,
            // but do not remove those from the list of allocated strings
            for (int i = allocatedStrings.Count - 1; i >= 0; i--)
            {
                VariableSymbol varSymbol = symTable.GetVariableSymbolByIdentifier(allocatedStrings[i]);

                // remove skipped string from list of allocated strings
                if (skipString != null && skipString.Equals(allocatedStrings[i]))
                {
                    allocatedStrings.Remove(allocatedStrings[i]);
                    continue;
                }

                // free and remove strings from list that are above threshold
                if (varSymbol.GetCurrentScope() > thresholdScope)
                {
                    visitor.AddCodeLine($"free({allocatedStrings[i]});");
                    allocatedStrings.Remove(allocatedStrings[i]);
                    continue;
                }

                // free strings below threshold but do not remove them (only is freeBeforeReturn)
                if (freeBeforeReturn)
                {
                    visitor.AddCodeLine($"free({allocatedStrings[i]});");
                }
            }
        }

        /// <summary>
        /// Static method <c>FreeArraysBeforeExitingScope</c> frees arrays stored in visitor
        /// before exiting scope.
        /// </summary>
        /// <param name="newscope">new scope level after exiting scope</param>
        /// <param name="visitor">code generator visitor</param>
        public static void FreeArraysBeforeExitingScope(int newscope, Visitor visitor)
        {
            MemoryMap memoryMap = visitor.GetMemoryMap();

            List<string> arrays = memoryMap.GetArraysThatNeedToBeFreedWhenExitingScope(newscope);

            foreach (string arr in arrays) visitor.AddCodeLine($"free({arr});");
        }

        /// <summary>
        /// Static method <c>FreeArraysBeforeReturnStatement</c> frees arrays stored in visitor
        /// before return statement.
        /// </summary>
        /// <param name="skipArray">array which should not be freed</param>
        /// <param name="visitor">code generator visitor</param>
        public static void FreeArraysBeforeReturnStatement(Visitor visitor, string skipArray = null)
        {
            MemoryMap memoryMap = visitor.GetMemoryMap();

            List<string> arrays = memoryMap.GetArraysThatNeedToBeFreedBeforeReturn(skipArray);

            foreach (string arr in arrays) visitor.AddCodeLine($"free({arr});");
        }

        /// <summary>
        /// Static method <c>FreeArraysBeforeArrayAssignment</c> frees arrays stored in visitor
        /// before array assignment operation.
        /// </summary>
        /// <param name="nameOfArr">array variable that is assigned new address</param>
        /// <param name="scopeOfArr">scope of array tha tis assigned a new address</param>
        /// <param name="nameOfTargetArr">name of the array which address is assigned</param>
        /// <param name="scopeOfTargetArr">scope of the array which address is assigned</param>
        /// <param name="visitor">code generator visitor</param>
        public static void FreeArraysBeforeArrayAssignment(string nameOfArr, int scopeOfArr, string nameOfTargetArr, int scopeOfTargetArr, Visitor visitor)
        {
            MemoryMap memoryMap = visitor.GetMemoryMap();

            string array = memoryMap.MoveArrayToPointOtherAddress(nameOfArr, scopeOfArr, nameOfTargetArr, scopeOfTargetArr);

            if (array != null) visitor.AddCodeLine($"free({array});");
        }

    }
}
