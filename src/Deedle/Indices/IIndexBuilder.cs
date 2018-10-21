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
  
  public interface IIndexBuilder
  {
    IIndex<K> Create<K>([In] IEnumerable<K> obj0, [In] FSharpOption<bool> obj1);

    IIndex<K> Create<K>([In] ReadOnlyCollection<K> obj0, [In] FSharpOption<bool> obj1);

    IIndex<K> Project<K>([In] IIndex<K> obj0);

    IIndex<K> Recreate<K>([In] IIndex<K> obj0);

    Tuple<IIndex<K>, VectorConstruction> GetAddressRange<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] RangeRestriction<long> obj1);

    Tuple<IIndex<K>, VectorConstruction> GetRange<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] Tuple<FSharpOption<Tuple<K, BoundaryBehavior>>, FSharpOption<Tuple<K, BoundaryBehavior>>> obj1);

    Tuple<IIndex<K>, VectorConstruction, VectorConstruction> Union<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] Tuple<IIndex<K>, VectorConstruction> obj1);

    Tuple<IIndex<K>, VectorConstruction, VectorConstruction> Intersect<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] Tuple<IIndex<K>, VectorConstruction> obj1);

    Tuple<IIndex<K>, VectorConstruction> Merge<K>([In] FSharpList<Tuple<IIndex<K>, VectorConstruction>> obj0, [In] VectorListTransform obj1);

    VectorConstruction Reindex<K>([In] IIndex<K> obj0, [In] IIndex<K> obj1, [In] Lookup obj2, [In] VectorConstruction obj3, [In] FSharpFunc<long, bool> obj4);

    Tuple<IIndex<TNewKey>, VectorConstruction> WithIndex<K, TNewKey>([In] IIndex<K> obj0, [In] IVector<TNewKey> obj1, [In] VectorConstruction obj2);

    Tuple<IIndex<K>, VectorConstruction> DropItem<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] K obj1);

    Tuple<IIndex<K>, VectorConstruction> Search<K, V>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] IVector<V> obj1, [In] V obj2);

    Tuple<IIndex<K>, VectorConstruction> LookupLevel<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] ICustomLookup<K> obj1);

    Tuple<IIndex<K>, VectorConstruction> OrderIndex<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0);

    Tuple<IIndex<K>, VectorConstruction> Shift<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0, [In] int obj1);

    Tuple<IIndex<TNewKey>, IVector<R>> Aggregate<K, TNewKey, R>(IIndex<K> index, Aggregation<K> aggregation, VectorConstruction source, FSharpFunc<Tuple<DataSegmentKind, Tuple<IIndex<K>, VectorConstruction>>, Tuple<TNewKey, OptionalValue<R>>> selector);

    ReadOnlyCollection<Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>> GroupBy<K, TNewKey>(IIndex<K> index, FSharpFunc<K, OptionalValue<TNewKey>> keySelector, [In] VectorConstruction obj2);

    Tuple<IIndex<TNewKey>, IVector<R>> Resample<K, TNewKey, R>([In] IIndexBuilder obj0, [In] IIndex<K> obj1, [In] IEnumerable<K> obj2, [In] Deedle.Direction obj3, VectorConstruction source, FSharpFunc<Tuple<K, Tuple<IIndex<K>, VectorConstruction>>, Tuple<TNewKey, OptionalValue<R>>> selector);

    Tuple<FSharpAsync<IIndex<K>>, VectorConstruction> AsyncMaterialize<K>([In] Tuple<IIndex<K>, VectorConstruction> obj0);
  }
}
