// Decompiled with JetBrains decompiler
// Type: Deedle.Delayed.IDelayedIndex`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Indices;

using System;
using System.Runtime.InteropServices;

namespace Deedle.Delayed
{
  
  [Serializable]
  internal interface IDelayedIndex<K>
  {
    R Invoke<R>([In] DelayedIndexFunction<K, R> obj0);

    IIndex<K> SourceIndex { get; }
  }
}
