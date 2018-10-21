// Decompiled with JetBrains decompiler
// Type: Deedle.ISeries`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Indices;
using Deedle.Vectors;

using System;
using System.Runtime.InteropServices;

namespace Deedle
{
  
  [Serializable]
  public interface ISeries<K>
  {
    IVector Vector { get; }

    IIndex<K> Index { get; }

    OptionalValue<object> TryGetObject([In] K obj0);

    IVectorBuilder VectorBuilder { get; }
  }
}
