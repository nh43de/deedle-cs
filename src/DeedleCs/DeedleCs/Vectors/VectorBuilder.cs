// Decompiled with JetBrains decompiler
// Type: Deedle.F# VectorBuilder implementation
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Vectors;
using Deedle.Vectors.ArrayVector;
using Microsoft.FSharp.Core;
using System;

namespace Deedle
{
    public static class FVectorBuilderimplementation
    {
        public class VectorBuilder
        {
            public static IVectorBuilder Instance
            {
                get
                {
                    return ArrayVectorBuilder.Instance;
                }
            }
        }

    }

}
