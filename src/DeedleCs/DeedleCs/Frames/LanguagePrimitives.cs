// Decompiled with JetBrains decompiler
// Type: Deedle.Frame`2
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;

namespace Deedle
{
    public static class LanguagePrimitives
    {
        public static class HashCompare
        {
            public static bool GenericEqualityIntrinsic<T>(Type t1, Type t2)
            {
                return t1 == t2;
            }
        }
    }
}
}
