// Decompiled with JetBrains decompiler
// Type: Deedle.IVector
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Deedle
{
  
  [Serializable]
  public interface IVector
  {
    IEnumerable<OptionalValue<object>> ObjectSequence { get; }

    Type ElementType { get; }

    bool SuppressPrinting { get; }

    OptionalValue<object> GetObject([In] long obj0);

    R Invoke<R>([In] VectorCallSite<R> obj0);

    long Length { get; }

    Addressing.IAddressingScheme AddressingScheme { get; }
  }
}
