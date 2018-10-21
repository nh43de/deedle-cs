// Decompiled with JetBrains decompiler
// Type: Deedle.IVector
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using static Deedle.Addressing;

namespace Deedle
{
    public interface IVector
    {
        /// <summary>
        /// Returns all values of the vector as a sequence of optional objects
        /// </summary>
        IEnumerable<object> ObjectSequence { get; }

        /// <summary>
        /// Returns the type of elements stored in the current vector as `System.Type`.
        /// This member is mainly used for internal purposes (to invoke a generic function
        /// represented by `VectorCallSite1<R>` with the typed version of the current 
        /// vector as an argument.
        /// </summary>
        Type ElementType { get; }

        [Obsolete]
        /// <summary>
        /// When `true`, the formatter in F# Interactive will not attempt to evaluate the
        /// vector to print it. This is useful when the vector contains lazily loaded data.
        /// </summary>
        bool SuppressPrinting { get; }

        /// <summary>
        /// Return value stored in the vector at a specified address. This is simply an
        /// untyped version of `GetValue` method on a typed vector.
        /// </summary>
        /// <param name="obj0"></param>
        /// <returns></returns>
        object GetObject([In] long obj0);

        /// <summary>
        /// Represents a generic function `\forall.'T.(IVector<'T> -> 'R)`. The function can be 
        /// generically invoked on an argument of type `IVector` using `IVector.Invoke`
        ///
        /// [category:Vectors and indices]
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="obj0"></param>
        /// <returns></returns>
        R Invoke<R>([In] VectorCallSite<R> obj0);

        /// <summary>
        /// Returns the number of elements in the vector
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Returns the addressing scheme of the index. When creating a series or a frame
        /// this is compared for equality with the addressing scheme of the vector(s).
        /// </summary>
        IAddressingScheme AddressingScheme { get; }
    }
}
