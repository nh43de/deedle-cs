// Decompiled with JetBrains decompiler
// Type: Deedle.Indices.IIndexBuilder
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Vectors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

namespace Deedle.Indices
{
    /// <summary>
    /// A builder represents various ways of constructing index, either from keys or from
    /// other indices. The operations that build a new index from an existing index also 
    /// build `VectorConstruction` which specifies how to transform vectors aligned with the
    /// previous index to match the new index. The methods generally take `VectorConstruction`
    /// as an input, apply necessary transformations to it and return a new `VectorConstruction`.
    ///
    /// ## Example
    ///
    /// For example, given `index`, we can say:
    ///
    ///     // Create an index that excludes the value 42
    ///     let newIndex, vectorCmd = indexBuilder.DropItem(index, 42, VectorConstruction.Return(0))
    ///
    ///     // Now we can transform multiple vectors (e.g. all frame columns) using 'vectorCmd'
    ///     // (the integer '0' in `Return` is an offset in the array of vector arguments)
    ///     let newVector = vectorBuilder.Build(vectorCmd, [| vectorToTransform |])
    ///
    /// </summary>
    public interface IIndexBuilder
    {
        ///<summary>   
        /// Create a new index using the specified keys. Optionally, the caller can specify
        /// if the index keys are ordered or not. When the value is not set, the construction
        /// should check and infer this from the data.
        ///</summary> 
        IIndex<K> Create<K>([In] IEnumerable<K> sequence, bool? ordered);

        ///<summary>   
        /// Create a new index using the specified keys. This overload takes data as ReadOnlyCollection
        /// and so it is more efficient if the caller already has the keys in an allocated collection.
        /// Optionally, the caller can specify if the index keys are ordered or not. When the value 
        /// is not set, the construction should check and infer this from the data.
        ///</summary> 
        IIndex<K> Create<K>([In] ReadOnlyCollection<K> sequence, bool ordered);

        ///<summary> 
        /// When we perform some projection on the vector (`Select` or `Convert`), then we may also
        /// need to perform some transformation on the index (because it may turn delayed index 
        /// into an evaluated index). If the vector operation does that, then `Project` should do the 
        /// same (e.g. evaluate) on the index.
        ///</summary> 
        IIndex<K> Project<K>([In] IIndex<K> obj0);

        ///<summary> 
        /// When we create a new vector (`IVectorBuilder.Create`), then we may get a materialized
        /// vector and we may need to perform the same transformation on the index. This is similar
        /// to `Project`, but used in different scenarios.
        ///</summary> 
        IIndex<K> Recreate<K>([In] IIndex<K> obj0);

        ///<summary> 
        /// Create a new index that represents sub-range of an existing index.
        /// The range is specified as a pair of addresses, which means that it can be 
        /// used by operations such as "series.Take(5)" (which do not rely on keys)
        ///</summary> 
        Tuple<IIndex<K>, VectorConstruction> GetAddressRange<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] RangeRestriction<long> obj1);

        ///<summary> 
        /// Create a new index that represents sub-range of an existing index. The range is specified
        /// as a pair of options (when `None`, the original left/right boundary should be used) 
        /// that contain boundary behavior and the boundary key.
        ///</summary> 
        Tuple<IIndex<K>, VectorConstruction> GetRange<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] Tuple<Tuple<K, BoundaryBehavior>, Tuple<K, BoundaryBehavior>> obj1);

        ///<summary> 
        /// Creates a union of two indices and builds corresponding vector transformations
        /// for both vectors that match the left and the right index.
        ///</summary> 
        Tuple<IIndex<K>, VectorConstruction, VectorConstruction> Union<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] Tuple<IIndex<K>, VectorConstruction> obj1);

        ///<summary> 
        /// Creates an interesection of two indices and builds corresponding vector transformations
        /// for both vectors that match the left and the right index.
        ///</summary> 
        Tuple<IIndex<K>, VectorConstruction, VectorConstruction> Intersect<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] Tuple<IIndex<K>, VectorConstruction> obj1);

        ///<summary> 
        /// Append two indices and builds corresponding vector transformations
        /// for both vectors that match the left and the right index. If the indices
        /// are ordered, the ordering should be preserved (the keys should be aligned).
        /// The specified `VectorListTransform` defines how to deal with the case when
        /// a key is defined in both indices (i.e. which value should be in the new vector).
        ///</summary> 
        Tuple<IIndex<K>, VectorConstruction> Merge<K>([In] IList<Tuple<IIndex<K>, VectorConstruction>> obj0, [In] VectorListTransform obj1);

        ///<summary>
        /// Given an old index and a new index, build a vector transformation that reorders
        /// elements from a vector associated with the old index so that they match the new
        /// index. When finding element location in the new index, the provided `Lookup` strategy
        /// is used. This is used, for example, when doing left/right join (to align the new data
        /// with another index) or when selecting multiple keys (`Series.lookupAll`).
        ///
        /// The proivded `condition` is used when searching for a value in the old index
        /// (when lookup is not exact). It is called to check that the address contains an
        /// appropriate value (e.g. when we need to skip over missing values).
        ///</summary> 
        VectorConstruction Reindex<K>([In] IIndex<K> obj0, [In] IIndex<K> obj1, [In] Lookup obj2, [In] VectorConstruction obj3, [In] Func<long, bool> obj4);

        ///<summary>   
        /// Create a new index by picking a new key value for each key in the original index
        /// (used e.g. when we have a frame and want to use specified column as a new index).
        ///</summary> 
        Tuple<IIndex<TNewKey>, VectorConstruction> WithIndex<K, TNewKey>([In] IIndex<K> obj0, [In] IVector<TNewKey> obj1, [In] VectorConstruction obj2);

        ///<summary> 
        /// Drop an item associated with the specified key from the index. 
        ///</summary> 
        Tuple<IIndex<K>, VectorConstruction> DropItem<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] K obj1);

        ///<summary> 
        /// Get a series construction that restricts the range of the input to only 
        /// locations where the specified vector contains the specified value.
        /// (used to filter frame rows according to a column value)
        ///</summary> 
        Tuple<IIndex<K>, VectorConstruction> Search<K, V>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] IVector<V> obj1, [In] V obj2);

        ///<summary> 
        /// Get items associated with the specified key from the index. This method takes
        /// `ICustomLookup<K>` which provides an implementation of `ICustomKey<K>`. This 
        /// is used for custom equality testing (for example, when getting a level of a hierarchical index)
        ///</summary> 
        Tuple<IIndex<K>, VectorConstruction> LookupLevel<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] ICustomLookup<K> obj1);

        ///<summary> 
        /// Order (possibly unordered) index and return transformation that reorders vector
        ///</summary> 
        Tuple<IIndex<K>, VectorConstruction> OrderIndex<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0);
        
        ///<summary> 
        /// Shift the values in the series by a specified offset, in a specified direction.
        /// The resulting series should be shorter by abs(offset); key for which there is no
        /// value should be dropped. For example:
        /// 
        ///     (original)  (shift 1) (shift -1)
        ///     a b c       _ b c     a b _
        ///     1 2 3         1 2     1 2
        /// 
        ///</summary> 
        Tuple<IIndex<K>, VectorConstruction> Shift<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] int obj1);

        ///<summary> 
        /// Aggregate an ordered index into floating windows or chunks. 
        ///
        /// ## Parameters
        ///
        ///  - `index` - Specifies the index to be aggregated
        ///  - `aggregation` - Defines the kind of aggregation to apply (the type 
        ///    is a discriminated union with a couple of cases)
        ///  - `source` - Source vector construction to be transformed 
        ///  - `selector` - Given information about window/chunk (including 
        ///    vector construction that can be used to build the data chunk), return
        ///    a new key, together with a new value for the returned vector.
        ///</summary> 
        Tuple<IIndex<TNewKey>, IVector<R>> Aggregate<K, TNewKey, R>(IIndex<K> index, Aggregation<K> aggregation, VectorConstruction source, Func<Tuple<DataSegmentKind, Tuple<IIndex<K>, VectorConstruction>>, Tuple<TNewKey, R>> selector);

        ///<summary> 
        /// Group a (possibly unordered) index according to a provided sequence of labels.
        /// The operation results in a sequence of unique labels along with corresponding 
        /// series construction objects which can be applied to arbitrary vectors/columns.
        ///</summary> 
        ReadOnlyCollection<Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>> GroupBy<K, TNewKey>(IIndex<K> index, Func<K, TNewKey> keySelector, [In] VectorConstruction obj2);

        ///<summary> 
        /// Aggregate data into non-overlapping chunks by aligning them to the
        /// specified keys. The second parameter specifies the direction. If it is
        /// `Direction.Forward` than the key is the first element of a chunk; for 
        /// `Direction.Backward`, the key is the last element (note that this does not 
        /// hold at the boundaries where values before/after the key may also be included)
        ///</summary> 
        Tuple<IIndex<TNewKey>, IVector<R>> Resample<K, TNewKey, R>([In] IIndexBuilder obj0, [In] IIndex<K> obj1, [In] IEnumerable<K> obj2, [In] Deedle.Direction obj3, VectorConstruction source, Func<Tuple<K, Tuple<IIndex<K>, VectorConstruction>>, Tuple<TNewKey, R>> selector);

        ///<summary> 
        /// Given an index and vector construction, return a new index asynchronously
        /// to allow composing evaluation of lazy series. The command to be applied to
        /// vectors can be applied asynchronously using `vectorBuilder.AsyncBuild`
        ///</summary> 
        Tuple<IIndex<K>, VectorConstruction> AsyncMaterialize<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0);
    }
}