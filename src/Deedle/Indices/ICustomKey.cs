// Decompiled with JetBrains decompiler
// Type: Deedle.Keys.ICustomKey
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Runtime.InteropServices;

namespace Deedle.Keys
{
  
  [Serializable]
  public interface ICustomKey
  {
    int Levels { get; }

    object GetLevel([In] int obj0);

    object[] GetLevels();
  }
}
