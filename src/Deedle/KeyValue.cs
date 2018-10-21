// Decompiled with JetBrains decompiler
// Type: Deedle.KeyValue
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Collections.Generic;

namespace Deedle
{
  
  [Serializable]
  public class KeyValue
  {
    public static KeyValuePair<K, V> Create<K, V>(K key, V value)
    {
      return new KeyValuePair<K, V>(key, value);
    }
  }
}
