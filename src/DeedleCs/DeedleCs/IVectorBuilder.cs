// Decompiled with JetBrains decompiler
// Type: Deedle.Vectors.IVectorBuilder
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using System;
using System.Runtime.InteropServices;

namespace Deedle.Vectors
{
    /// <summary>
    /// Represents an object that can construct vector values by processing 
    /// the "mini-DSL" representation `VectorConstruction`.
    /// </summary>
    public interface IVectorBuilder
    {
        ///<summary> 
        /// Create a vector from an array containing values. The values may 
        /// still represent missing values and the vector should handle this.
        /// For example `Double.NaN` or `null` should be turned into a missing
        /// value in the returned vector.
        ///</summary> 
        IVector<T> Create<T>([In] T[] obj0);

        ///<summary> 
        /// Create a vector from an array containing values that may be missing. 
        /// Even if a value is passed, it may be a missing value such as `Double.NaN`
        /// or `null`. The vector builder should hanlde this.
        ///</summary> 
        IVector<T> CreateMissing<T>([In] T[] obj0);

        ///<summary> 
        /// Apply a vector construction to a given vector. The second parameter
        /// is an array of arguments ("variables") that may be referenced from the
        /// `VectorConstruction` using the `Return 0` construct.
        ///</summary> 
        IVector<T> Build<T>([In] Addressing.IAddressingScheme obj0, [In] VectorConstruction obj1, [In] IVector<T>[] obj2);

        ///<summary> 
        /// Asynchronous version of `Build` operation. This is mainly used for 
        /// `AsyncMaterialize` and it does not handle fully general vector constructions (yet)
        ///</summary> 
        IVector<T> AsyncBuild<T>([In] Addressing.IAddressingScheme obj0, [In] VectorConstruction obj1, [In] IVector<T>[] obj2);
    }
}