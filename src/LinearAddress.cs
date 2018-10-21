// Decompiled with JetBrains decompiler
// Type: Deedle.Addressing
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using System.Diagnostics;

namespace Deedle
{
    public static partial class Addressing
    {
        public static class LinearAddress
        {
            public static long invalid
            {
                [DebuggerNonUserCode]
                get
                {
                    return Addressing.AddressModule.invalid;
                }
            }

            public static long asInt64(long x)
            {
                return x;
            }

            public static int asInt(long x)
            {
                return (int)x;
            }

            public static long ofInt64(long x)
            {
                return x;
            }

            public static long ofInt(int x)
            {
                return (long)x;
            }

            public static long increment(long x)
            {
                return x + 1L;
            }

            public static long decrement(long x)
            {
                return x - 1L;
            }
        }
    }
}
