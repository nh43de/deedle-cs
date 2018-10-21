// Decompiled with JetBrains decompiler
// Type: Deedle.Addressing
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Deedle
{
    public interface IAddressOperations
    {
        ///<summary> 
        /// Returns the first address of the range
        ///</summary> 
        long FirstElement { get; }

        ///<summary> 
        /// Returns the last address of the range
        ///</summary> 
        long LastElement { get; }

        ///<summary> 
        /// Returns a sequence that iterates over `FirstElement .. LastElement`
        ///</summary> 
        IEnumerable<long> Range { get; }

        ///<summary> 
        /// Given an address, return the absolute offset of the address in the range
        /// This might be tricky for partitioned ranges. For example if you have two 
        /// partitions with 10 values addressed by (0,0)..(0,9); (1,0)..(1,9), the the
        /// offset of address (1, 5) is 15.
        ///</summary> 
        long OffsetOf([In] long obj0);

        ///<summary> 
        /// Return the address of a value at the specified absolute offset.
        /// (See the comment for `OffsetOf` for more info about partitioning)
        ///</summary> 
        long AddressOf([In] long obj0);

        ///<summary> 
        /// Increment or decrement the specified address by a given number
        ///<summary> 
        long AdjustBy([In] long obj0, [In] long obj1);
    }
}
