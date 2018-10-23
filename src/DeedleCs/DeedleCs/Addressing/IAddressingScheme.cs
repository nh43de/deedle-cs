// Decompiled with JetBrains decompiler
// Type: Deedle.Addressing
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


namespace Deedle
{
    /// <summary>
    /// An `Address` value is used as an interface between vectors and indices. The index maps
    /// keys of various types to address, which is then used to get a value from the vector.
    ///
    /// Here is a brief summary of what we assume (and don't assume) about addresses:
    ///
    ///  - Address is `int64` (although we might need to generalize this in the future)
    ///  - Different data sources can use different addressing schemes
    ///    (as long as both index and vector use the same scheme)
    ///  - Addresses don't have to be continuous (e.g. if the source is partitioned, it
    ///    can use 32bit partition index + 32bit offset in the partition)
    ///  - In the in-memory representation, address is just index into an array
    ///  - In the BigDeedle representation, address is abstracted and comes with
    ///    `AddressOperations` that specifies how to use it (tests use linear
    ///    offset and partitioned representation)
    ///
    /// [category:Vectors and indices]
    /// </summary>
    public static partial class Addressing
    {
        /// <summary>
        /// An empty interface that is used as an marker for "addressing schemes". As discussed
        /// above, Deedle can use different addressing schemes. We need to make sure that the index
        /// and vector share the scheme - this is done by attaching `IAddressingScheme` to each
        /// index or vector and checking that they match. Implementations must support equality!
        /// </summary>
        public interface IAddressingScheme
        {

        }

        public static class AddressModule
        {
            /// <summary>
            /// Represents an invalid address (which is returned from 
            /// optimized lookup functions when they fail)
            /// </summary>
            public static long invalid
            {
                get
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Address operations that are used by the standard in-memory Deedle structures
        /// (LinearIndex and ArrayVector). Here, address is a positive array offset.
        /// </summary>
        public static class LinearAddress
        {
            /// <summary>
            /// Represents an invalid address (which is returned from 
            /// optimized lookup functions when they fail)
            /// </summary>
            public static long invalid
            {
                get
                {
                    return Addressing.AddressModule.invalid;
                }
            }

            public static long asInt64(long x)
            {
                return x;
            }

            public static int asInt(long x)
            {
                return (int)x;
            }

            public static long ofInt64(long x)
            {
                return x;
            }

            public static long ofInt(int x)
            {
                return (long)x;
            }

            public static long increment(long x)
            {
                return x + 1L;
            }

            public static long decrement(long x)
            {
                return x - 1L;
            }
        }

        /// <summary>
        /// Represents a linear addressing scheme where the addresses are `0 .. <size>-1`.
        /// </summary>
        public class LinearAddressingScheme : IAddressingScheme
        {

        }
    }
}
