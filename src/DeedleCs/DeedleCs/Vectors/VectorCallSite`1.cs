// Decompiled with JetBrains decompiler
// Type: Deedle.VectorCallSite`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Runtime.InteropServices;

namespace Deedle
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public interface VectorCallSite<R>
    {
        /// <summary>
        /// Represents a generic function `\forall.'T.(IVector<'T> -> 'R)`. The function can be 
        /// generically invoked on an argument of type `IVector` using `IVector.Invoke`
        ///
        /// [category:Vectors and indices]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj0"></param>
        /// <returns></returns>
        R Invoke<T>([In] IVector<T> obj0);
    }
}
