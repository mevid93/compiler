using System;
using System.Collections.Generic;

namespace MipaCompiler
{
    /// <summary>
    /// Class <c>MemoryMap</c> contains functionality to keep record of different arrays
    /// and where they point. Class is used for array memory free operations, so that
    /// arrays are freed when needed.
    /// </summary>
    public class MemoryMap
    {
        private List<MemoryAddress> addresses;      // list of memory addresses that are not freed

        /// <summary>
        /// Constructor <c>MemoryMap</c> creates new MemoryMap-object.
        /// </summary>
        public MemoryMap()
        {
            addresses = new List<MemoryAddress>();
        }

        /// <summary>
        /// Method <c>AddNewAddress</c> adds new memory address to list of addresses.
        /// </summary>
        /// <param name="newAddress">new memory address</param>
        public void AddNewAddress(MemoryAddress newAddress)
        {
            addresses.Add(newAddress);
        }

        /// <summary>
        /// Method <c>MoveArrayToPointOtherAddress</c> switches array to point other memory address.
        /// If old address where array becomes loose (no other arrays pointing to it), array should
        /// be freed before assignment. If array should be freed, the name of the array is returned.
        /// Otherwise null is returned.
        /// </summary>
        public string MoveArrayToPointOtherAddress(string varArr, int varScope, string valArr, int valScope)
        {
            ArrayInfo varArray = new ArrayInfo(varArr, varScope); // array which is assigned a new value
            ArrayInfo valArray = new ArrayInfo(valArr, valScope); // value of asignment

            MemoryAddress varMem = null;    // address of var before assignment
            MemoryAddress valMem = null;    // address of assigned array
            int oldAddressIndex = 0;

            foreach (MemoryAddress memAddr in addresses)
            {
                for (int i = 0; i < memAddr.GetArrayInfos().Count; i++)
                {
                    ArrayInfo info = memAddr.GetArrayInfos()[i];

                    if (info.AreSame(varArray))
                    {
                        varMem = memAddr;
                        oldAddressIndex = i;
                        break;
                    }
                    if (info.AreSame(valArray))
                    {
                        valMem = memAddr;
                        break;
                    }
                }
            }

            // remove var array from old address
            for (int i = 0; i < varMem.GetArrayInfos().Count; i++)
            {
                ArrayInfo info = varMem.GetArrayInfos()[i];
                if (info.AreSame(varArray))
                {
                    varMem.GetArrayInfos().RemoveAt(i);
                    break;
                }
            }

            // insert to new address
            valMem.GetArrayInfos().Add(varArray);

            // check if needs to be freed
            if (varMem.GetArrayInfos().Count == 0)
            {
                addresses.RemoveAt(oldAddressIndex);
                return varArr;
            }
            return null;
        }

        /// <summary>
        /// Returns list of array names that need to be freed when exiting to scope
        /// with level given as parameter. If multiple arrays are pointing to same
        /// location, then only one of them is returned. However, if an array
        /// with scope less than or equal to newscope points to address, then the memory
        /// is not freed.
        /// </summary>
        /// <param name="newScope">threshold scope</param>
        /// <returns>list of arrays that need to be freed</returns>
        public List<string> GetArraysThatNeedToBeFreedWhenExitingScope(int newScope)
        {
            List<string> arrays = new List<string>();

            // check all memory addresses
            for (int j = addresses.Count - 1; j >= 0; j--)
            {
                MemoryAddress addr = addresses[j];

                bool remove = true;

                // check all arrays pointing to the address
                for (int i = 0; i < addr.GetArrayInfos().Count; i++)
                {
                    int scope = addr.GetArrayInfos()[i].GetScope();

                    if (scope <= newScope) remove = false;
                }

                // remove if flag is true
                if (remove)
                {
                    if(addr.GetArrayInfos().Count != 0)
                    {
                        // add to list of arrays that need to be freed
                        arrays.Add(addr.GetArrayInfos()[0].GetName());
                    }

                    // remove from addresses
                    addresses.RemoveAt(j);
                }
            }

            return arrays;
        }

        /// <summary>
        /// Returns list of array names that need to be freed before return statement
        /// If multiple arrays are pointing to same
        /// location, then only one of them is returned. If skipArray is given, it means
        /// that that arays is returned and should not be freed.
        /// </summary>
        /// <param name="skipArray">name of returned array</param>
        /// <param name="scopeAfterReturn">scope of returned array</param>
        /// <returns>list of arrays that need to be freed</returns>
        public List<string> GetArraysThatNeedToBeFreedBeforeReturn(string skipArray = null)
        {
            List<string> arrays = new List<string>();

            // check all memory addresses
            for (int j = addresses.Count - 1; j >= 0; j--)
            {
                MemoryAddress addr = addresses[j];

                bool remove = false;
                bool free = false;

                // check all arrays pointing to the address
                for (int i = 0; i < addr.GetArrayInfos().Count; i++)
                {
                    string name = addr.GetArrayInfos()[i].GetName();
                    int scope = addr.GetArrayInfos()[i].GetScope();

                    if (scope != -1)
                    {
                        remove = true;
                        free = true;
                    }

                    if (skipArray != null && skipArray.Equals(name))
                    {
                        remove = false;
                        free = false;
                    }
                }

                // free if flag is true
                if (free)
                {
                    arrays.Add(addr.GetArrayInfos()[0].GetName());

                    // remove if flag is true
                    if (remove) addresses.RemoveAt(j);
                }
            }

            return arrays;
        }


        public List<MemoryAddress> GetAddresses()
        {
            return addresses;
        }
    }

    /// <summary>
    /// Class <c>MemoryAddress</c> represents single allocated array memory address.
    /// </summary>
    public class MemoryAddress
    {
        private int address;                // address where the arrays are pointing
        private List<ArrayInfo> arrays;     // list of arrays pointing to address

        /// <summary>
        /// Constructor <c>MemoryAddress</c> creates new MemoryAddress-object.
        /// </summary>
        /// <param name="address">new address</param>
        /// <param name="array">array pointing to new address</param>
        /// <param name="arrayScope">scope of the array pointing to the address</param>
        public MemoryAddress(int address, string array, int arrayScope)
        {
            this.address = address;
            arrays = new List<ArrayInfo>();
            arrays.Add(new ArrayInfo(array, arrayScope));
        }

        /// <summary>
        /// Method <c>GetAddress</c> returns memory address.
        /// </summary>
        /// <returns>memory address</returns>
        public int GetAddress()
        {
            return address;
        }

        public List<ArrayInfo> GetArrayInfos()
        {
            return arrays;
        }
        
    }

    public class ArrayInfo
    {
        private string arrayName;
        private int arrayScope;

        public ArrayInfo(string arrayName, int arrayScope)
        {
            this.arrayName = arrayName;
            this.arrayScope = arrayScope;
        }

        public string GetName()
        {
            return arrayName;
        }

        public int GetScope()
        {
            return arrayScope;
        }

        public bool AreSame(ArrayInfo compared)
        {
            if (compared.GetScope() == arrayScope && compared.GetName() == arrayName) return true;
            return false;
        }
    }
}