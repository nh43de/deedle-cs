// Decompiled with JetBrains decompiler
// Type: Deedle.Ranges.IRangeKeyOperations`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Deedle.Ranges
{
  
  [Serializable]
  public interface IRangeKeyOperations<TKey>
  {
    int Compare([In] TKey obj0, [In] TKey obj1);

    TKey IncrementBy([In] TKey obj0, [In] long obj1);

    long Distance([In] TKey obj0, [In] TKey obj1);

    IEnumerable<TKey> Range([In] TKey obj0, [In] TKey obj1);

    OptionalValue<TKey> ValidateKey([In] TKey obj0, [In] Lookup obj1);
  }
}
