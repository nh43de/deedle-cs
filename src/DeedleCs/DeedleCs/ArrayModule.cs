﻿// Decompiled with JetBrains decompiler
// Type: Deedle.Frame`2
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System.Collections.Generic;
using System.Linq;

namespace Deedle
{
    public static class ArrayModule
    {
        public static T[] OfSeq<T>(IEnumerable<T> items)
        {
            return items.ToArray();
        }
    }
}
}
