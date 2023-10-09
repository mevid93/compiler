using MipaCompiler;
using Xunit;

namespace MipaCompilerTests
{
    public class MemoryMapTests
    {
        [Fact]
        public void ConstructorWorks()
        {
            MemoryMap memo = new MemoryMap();

            // when array is declared --> it is pointing to new address
            memo.AddNewAddress(new MemoryAddress(0, "var_array1", 0));
            memo.AddNewAddress(new MemoryAddress(1, "var_array2", 1));

            Assert.Equal(2, memo.GetAddresses().Count);
        }

        [Fact]
        public void MoveArrayToPointOtherAddressWorks()
        {
            MemoryMap memo = new MemoryMap();

            // when array is declared --> it is pointing to new address
            memo.AddNewAddress(new MemoryAddress(0, "var_array1", 0));
            memo.AddNewAddress(new MemoryAddress(1, "var_array2", 1));

            Assert.Equal(2, memo.GetAddresses().Count);

            string free = memo.MoveArrayToPointOtherAddress("var_array1", 0, "var_array2", 1);

            Assert.Equal("var_array1", free);

            
        }
    }
}
