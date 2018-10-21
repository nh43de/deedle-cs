// Decompiled with JetBrains decompiler
// Type: Deedle.Vectors.Virtual.IVirtualVectorSource
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Runtime.InteropServices;

namespace Deedle.Vectors.Virtual
{
  
  [Serializable]
  public interface IVirtualVectorSource
  {
    string AddressingSchemeID { get; }

    Type ElementType { get; }

    long Length { get; }

    Addressing.IAddressOperations AddressOperations { get; }

    R Invoke<R>([In] IVirtualVectorSourceOperation<R> obj0);
  }
}
