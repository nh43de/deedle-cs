// Decompiled with JetBrains decompiler
// Type: Deedle.ISeries`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Indices;
using Deedle.Vectors;

using System;
using System.Runtime.InteropServices;

namespace Deedle
{
    /// <summary>
    /// Represents an untyped series with keys of type `K` and values of some unknown type
    /// (This type should not generally be used directly, but it can be used when you need
    /// to write code that works on a sequence of series of heterogeneous types).
    ///
    /// [category:Core frame and series types]
    /// </summary>
    /// <typeparam name="K"></typeparam>
    public interface ISeries<K>
    {
        /// <summary>
        /// Returns the vector containing data of the series (as an untyped vector)
        /// </summary>
        IVector Vector { get; }

        /// <summary>
        /// Returns the index containing keys of the series 
        /// </summary>
        IIndex<K> Index { get; }

        /// <summary>
        /// Attempts to get the value at a specified key and return it as `obj`
        /// </summary>
        /// <param name="obj0"></param>
        /// <returns></returns>
        object TryGetObject([In] K obj0);

        /// <summary>
        /// Returns the vector builder associated with this series
        /// </summary>
        IVectorBuilder VectorBuilder { get; }
    }
}