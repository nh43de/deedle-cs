// Decompiled with JetBrains decompiler
// Type: Deedle.IVectorLocation
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using System;

namespace Deedle
{
    /// <summary>
    /// Represents a location in a vector. In general, we always know the address, but 
    /// sometimes (BigDeedle) it is hard to get the offset (requires some data lookups),
    /// so we use this interface to delay the calculation of the Offset (which is mainly
    /// needed in one of the `series.Select` overloads)
    ///
    /// [category:Vectors and indices]
    /// </summary>
    public interface IVectorLocation
    {
        /// <summary>
        /// Returns the address of the location (this should be immediate) 
        /// </summary>
        long Address { get; }

        /// <summary>
        /// Returns the offset of the location (this may involve some calculation)
        /// </summary>
        long Offset { get; }
    }
}