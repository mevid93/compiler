using Microsoft.VisualStudio.TestTools.UnitTesting;
using MipaCompiler;

namespace MipaCompilerTests
{
    [TestClass]
    public class MemoryMapTests
    {
        [TestMethod]
        public void ConstructorWorks()
        {
            MemoryMap memo = new MemoryMap();

            // when array is declared --> it is pointing to new address
            memo.AddNewAddress(new MemoryAddress(0, "var_array1", 0));
            memo.AddNewAddress(new MemoryAddress(1, "var_array2", 1));

            Assert.AreEqual(2, memo.GetAddresses().Count);
        }

        [TestMethod]
        public void MoveArrayToPointOtherAddressWorks()
        {
            MemoryMap memo = new MemoryMap();

            // when array is declared --> it is pointing to new address
            memo.AddNewAddress(new MemoryAddress(0, "var_array1", 0));
            memo.AddNewAddress(new MemoryAddress(1, "var_array2", 1));

            Assert.AreEqual(2, memo.GetAddresses().Count);

            string free = memo.MoveArrayToPointOtherAddress("var_array1", 0, "var_array2", 1);

            Assert.AreEqual("var_array1", free);

            
        }
    }
}
