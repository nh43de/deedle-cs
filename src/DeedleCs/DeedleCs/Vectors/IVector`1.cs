// Decompiled with JetBrains decompiler
// Type: Deedle.IVector`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Vectors;

using System;
using System.Runtime.InteropServices;

namespace Deedle
{
    public interface IVector<T> : IVector
    {
        /// <summary>
        /// Returns value stored in the vector at a specified address. 
        /// </summary>
        /// <param name="obj0"></param>
        /// <returns></returns>
        T GetValue([In] long obj0);

        /// <summary>
        /// Returns value stored in the vector at a specified location. 
        /// This can typically just call 'GetValue(loc.Address)', but it can do something
        /// more clever using the fact that the caller provided us with the address & offset.
        /// </summary>
        /// <param name="obj0"></param>
        /// <returns></returns>
        T GetValueAtLocation([In] IVectorLocation obj0);

        /// <summary>
        /// Returns all data of the vector in one of the supported formats. Depending
        /// on the vector, data may be returned as a continuous block of memory using
        /// `ReadOnlyCollection<T>` or as a lazy sequence `seq<T>`.
        /// </summary>
        VectorData<T> Data { get; }

        /// <summary>
        /// Apply the specified function to all values stored in the vector and return
        /// a new vector (not necessarily of the same representation) with the results.
        /// The function handles missing values - it is called with optional values and
        /// may return a missing value as a result of the transformation.
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="obj0"></param>
        /// <returns></returns>
        IVector<TNew> Select<TNew>([In] FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<T>, OptionalValue<TNew>>> obj0);

        /// <summary>
        /// Create a vector whose values are converted using the specified function, but
        /// can be converted back using another specified function. For virtualized vectors,
        /// this enables e.g. efficient lookup on the returned vectors (by delegating the
        /// lookup to the original source).
        /// </summary>
        /// <typeparam name="TNew"></typeparam>
        /// <param name="obj0"></param>
        /// <param name="obj1"></param>
        /// <returns></returns>
        IVector<TNew> Convert<TNew>([In] FSharpFunc<T, TNew> obj0, [In] FSharpFunc<TNew, T> obj1);
    }
}
