// Decompiled with JetBrains decompiler
// Type: Deedle.Indices.IIndex`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using static Deedle.Addressing;

namespace Deedle.Indices
{
    public interface IIndex<K>
    {
        ///<summary> 
        /// Returns the addressing scheme of the index. When creating a series or a frame
        /// this is compared for equality with the addressing scheme of the vector(s).
        ///</summary> 
        IAddressingScheme AddressingScheme { get; }

        ///<summary> 
        /// Returns the address operations associated with this index. The addresses of the
        /// index are not necesarilly continuous integers from 0. This provides some operations
        /// that can be used for implementing generic operations over any kind of indices.
        ///</summary> 
        IAddressOperations AddressOperations { get; }

        ///<summary> 
        /// Returns a (fully evaluated) collection with all keys in the index
        ///</summary> 
        ReadOnlyCollection<K> Keys { get; }

        /// <summary>
        /// Returns a lazy sequence that iterates over all keys in the index
        ///</summary> 
        IEnumerable<K> KeySequence { get; }

        ///<summary> 
        /// Performs reverse lookup - and returns key for a specified address
        ///</summary> 
        K KeyAt([In] long obj0);


        ///<summary> 
        /// Return an address that represents the specified offset
        ///</summary> 
        long AddressAt([In] long obj0);

        ///<summary> 
        /// Returns the number of keys in the index
        ///</summary> 
        long KeyCount { get; }

        ///<summary> 
        /// Returns whether the specified index is empty. This is equivalent to 
        /// testing if `Keys` are empty, but it does not have to evaluate delayed index.
        ///</summary> 
        bool IsEmpty { get; }

        ///<summary> 
        // A fast lookup that returns the address, or an invalid address sentinel if
        // the key is not found (eg, a negative offset)
        ///</summary> 
        long Locate(K key);

        ///<summary> 
        /// Find the address associated with the specified key, or with the nearest
        /// key as specified by the `lookup` argument. The `condition` function is called
        /// when searching for keys to ask the caller whether the address should be returned
        /// (or whether to continue searching). This is used when searching for previous element
        /// in a series (where we need to check if a value at the address is available)
        ///</summary> 
        Tuple<K, long> Lookup(K key, Lookup lookup, Func<long, bool> condition);

        ///<summary> 
        /// Returns all key-address mappings in the index
        ///</summary> 
        IEnumerable<KeyValuePair<K, long>> Mappings { get; }

        ///<summary> 
        /// Returns the minimal and maximal key associated with the index.
        /// (the operation may fail for unordered indices)
        ///</summary> 
        Tuple<K, K> KeyRange { get; }

        ///<summary> 
        /// Returns `true` if the index is ordered and `false` otherwise
        ///</summary> 
        bool IsOrdered { get; }

        ///<summary> 
        /// Returns a comparer associated with the values used by the current index.
        ///</summary> 
        Comparer<K> Comparer { get; }

        ///<summary> 
        /// Returns an index builder that can be used for constructing new indices of the
        /// same kind as the current index (e.g. a lazy index returns a lazy index builder)
        /// </summary>
        IIndexBuilder Builder { get; }
    }
}