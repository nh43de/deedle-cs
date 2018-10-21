// Decompiled with JetBrains decompiler
// Type: Deedle.Addressing
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Deedle
{
    public static partial class Addressing
    {
        [Serializable]
        public interface IAddressOperations
        {
            long FirstElement { get; }

            long LastElement { get; }

            IEnumerable<long> Range { get; }

            long OffsetOf([In] long obj0);

            long AddressOf([In] long obj0);

            long AdjustBy([In] long obj0, [In] long obj1);
        }
    }
}
