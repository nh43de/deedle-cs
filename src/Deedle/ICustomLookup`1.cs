// Decompiled with JetBrains decompiler
// Type: Deedle.ICustomLookup`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Runtime.InteropServices;

namespace Deedle
{
  
  [Serializable]
  public interface ICustomLookup<K>
  {
    bool Matches([In] K obj0);
  }
}
