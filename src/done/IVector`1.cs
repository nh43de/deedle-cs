// Decompiled with JetBrains decompiler
// Type: Deedle.IVector`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Vectors;

using System;
using System.Runtime.InteropServices;

namespace Deedle
{
  
  [Serializable]
  public interface IVector<T> : IVector
  {
    OptionalValue<T> GetValue([In] long obj0);

    OptionalValue<T> GetValueAtLocation([In] IVectorLocation obj0);

    VectorData<T> Data { get; }

    IVector<TNew> Select<TNew>([In] FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<T>, OptionalValue<TNew>>> obj0);

    IVector<TNew> Convert<TNew>([In] FSharpFunc<T, TNew> obj0, [In] FSharpFunc<TNew, T> obj1);
  }
}
