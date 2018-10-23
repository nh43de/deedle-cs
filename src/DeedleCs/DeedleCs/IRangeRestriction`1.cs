// Decompiled with JetBrains decompiler
// Type: Deedle.IRangeRestriction`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Collections.Generic;

namespace Deedle
{
    /// <summary>
    /// A sequence of indicies together with the total number. Use `RangeRestriction.ofSeq` to
    /// create one from a sequence. This can be implemented by concrete vector/index 
    /// builders to allow further optimizations (e.g. when the underlying source directly
    /// supports range operations). 
    ///
    /// For example, if your source has an optimised way for getting every 10th address, you 
    /// can create your own `IRangeRestriction` and then check for it in `LookupRange` and 
    /// use optimised implementation rather than actually iterating over the sequence of indices.
    /// </summary>
    /// <typeparam name="TAddress"></typeparam>
    public interface IRangeRestriction<TAddress> : IEnumerable<TAddress>
    {
        long Count { get; }
    }
}