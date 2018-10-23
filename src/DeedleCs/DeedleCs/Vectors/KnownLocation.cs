// Decompiled with JetBrains decompiler
// Type: Deedle.Vectors.KnownLocation
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using System;
using System.Runtime.CompilerServices;

namespace Deedle.Vectors
{
    [Serializable]
    public class KnownLocation : IVectorLocation
    {
        internal long offset;
        internal long addr;

        public KnownLocation(long addr, long offset)
        {
            KnownLocation knownLocation = this;
            this.addr = addr;
            this.offset = offset;
        }

        public long Address => this.addr;

        public long Offset => this.offset;
    }
}
