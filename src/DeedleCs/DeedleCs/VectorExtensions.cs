// Decompiled with JetBrains decompiler
// Type: Deedle.Frame`2
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System.Collections.Generic;
using System.Linq;

namespace Deedle
{
    public static class VectorExtensions
    {
        /// <summary>
        /// Creates a vector that stores the specified data in an array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static IVector ofValues<T>(IEnumerable<T> data)
        {
            return FVectorBuilderimplementation.VectorBuilder.Instance.Create<T>(data.ToArray());
        }
    }
}
}
