// Decompiled with JetBrains decompiler
// Type: Deedle.Indices.IIndex`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Deedle.Indices
{
  
  [Serializable]
  public interface IIndex<K>
  {
    Addressing.IAddressingScheme AddressingScheme { get; }

    Addressing.IAddressOperations AddressOperations { get; }

    ReadOnlyCollection<K> Keys { get; }

    IEnumerable<K> KeySequence { get; }

    K KeyAt([In] long obj0);

    long AddressAt([In] long obj0);

    long KeyCount { get; }

    bool IsEmpty { get; }

    long Locate(K key);

    OptionalValue<Tuple<K, long>> Lookup(K key, Lookup lookup, FSharpFunc<long, bool> condition);

    IEnumerable<KeyValuePair<K, long>> Mappings { get; }

    Tuple<K, K> KeyRange { get; }

    bool IsOrdered { get; }

    System.Collections.Generic.Comparer<K> Comparer { get; }

    IIndexBuilder Builder { get; }
  }
}
