// Decompiled with JetBrains decompiler
// Type: Deedle.Vectors.IVectorBuilder
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Microsoft.FSharp.Control;

using System;
using System.Runtime.InteropServices;

namespace Deedle.Vectors
{
  
  [Serializable]
  public interface IVectorBuilder
  {
    IVector<T> Create<T>([In] T[] obj0);

    IVector<T> CreateMissing<T>([In] OptionalValue<T>[] obj0);

    IVector<T> Build<T>([In] Addressing.IAddressingScheme obj0, [In] VectorConstruction obj1, [In] IVector<T>[] obj2);

    FSharpAsync<IVector<T>> AsyncBuild<T>([In] Addressing.IAddressingScheme obj0, [In] VectorConstruction obj1, [In] IVector<T>[] obj2);
  }
}
