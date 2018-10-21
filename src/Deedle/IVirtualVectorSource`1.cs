// Decompiled with JetBrains decompiler
// Type: Deedle.Vectors.Virtual.IVirtualVectorSource`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Deedle.Vectors.Virtual
{
  
  [Serializable]
  public interface IVirtualVectorSource<V> : IVirtualVectorSource
  {
    OptionalValue<V> ValueAt([In] IVectorLocation obj0);

    RangeRestriction<long> LookupRange([In] V obj0);

    OptionalValue<Tuple<V, long>> LookupValue([In] V obj0, [In] Lookup obj1, [In] Func<long, bool> obj2);

    IVirtualVectorSource<V> GetSubVector([In] RangeRestriction<long> obj0);

    IVirtualVectorSource<V> MergeWith([In] IEnumerable<IVirtualVectorSource<V>> obj0);
  }
}
