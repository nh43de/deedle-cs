// Decompiled with JetBrains decompiler
// Type: Deedle.DelayedSeries
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using \u003CStartupCodeDeedle\u003E;
using Deedle.Delayed;
using Deedle.Indices;
using Deedle.Vectors;
using Microsoft.FSharp.Control;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deedle
{
  
  [Serializable]
  public class DelayedSeries
  {
    public static Series<K, V> FromValueLoader<K, V>(K min, K max, Func<K, BoundaryBehavior, K, BoundaryBehavior, Task<IEnumerable<KeyValuePair<K, V>>>> loader)
    {
      return DelayedSeries.FromValueLoader<K, V>(min, max, (FSharpFunc<Tuple<K, BoundaryBehavior>, FSharpFunc<Tuple<K, BoundaryBehavior>, FSharpAsync<IEnumerable<KeyValuePair<K, V>>>>>) new DelayedSeries.FromValueLoader<K, V>(loader));
    }

    public static Series<K, V> FromValueLoader<K, V>(K min, K max, FSharpFunc<Tuple<K, BoundaryBehavior>, FSharpFunc<Tuple<K, BoundaryBehavior>, FSharpAsync<IEnumerable<KeyValuePair<K, V>>>>> loader)
    {
      IVectorBuilder instance1 = FVectorBuilderimplementation.VectorBuilder.Instance;
      IIndexBuilder instance2 = FIndexBuilderimplementation.IndexBuilder.Instance;
      Ranges.Ranges<K> ranges = Ranges.Ranges<K>.NewRange(new Tuple<Tuple<K, BoundaryBehavior>, Tuple<K, BoundaryBehavior>>(new Tuple<K, BoundaryBehavior>(min, BoundaryBehavior.get_Inclusive()), new Tuple<K, BoundaryBehavior>(max, BoundaryBehavior.get_Inclusive())));
      DelayedSource<K, V> source = new DelayedSource<K, V>(Addressing.LinearAddressingScheme.Instance, min, max, ranges, instance2, instance1, (FSharpFunc<Tuple<Tuple<K, BoundaryBehavior>, Tuple<K, BoundaryBehavior>>[], FSharpAsync<Tuple<IIndex<K>, IVector<V>>>[]>) new DelayedSeries.source<K, V>(loader, instance1, instance2));
      return new Series<K, V>((IIndex<K>) new DelayedIndex<K, V>(source), (IVector<V>) new DelayedVector<K, V>(source), instance1, (IIndexBuilder) new DelayedIndexBuilder());
    }

    public static Series<K, V> FromIndexVectorLoader<K, V>(Addressing.IAddressingScheme scheme, IVectorBuilder vectorBuilder, IIndexBuilder indexBuilder, K min, K max, Func<K, BoundaryBehavior, K, BoundaryBehavior, Task<Tuple<IIndex<K>, IVector<V>>>> loader)
    {
      return DelayedSeries.FromIndexVectorLoader<K, V>(scheme, vectorBuilder, indexBuilder, min, max, (FSharpFunc<Tuple<K, BoundaryBehavior>, FSharpFunc<Tuple<K, BoundaryBehavior>, FSharpAsync<Tuple<IIndex<K>, IVector<V>>>>>) new DelayedSeries.FromIndexVectorLoader<K, V>(loader));
    }

    public static Series<K, V> FromIndexVectorLoader<K, V>(Addressing.IAddressingScheme scheme, IVectorBuilder vectorBuilder, IIndexBuilder indexBuilder, K min, K max, FSharpFunc<Tuple<K, BoundaryBehavior>, FSharpFunc<Tuple<K, BoundaryBehavior>, FSharpAsync<Tuple<IIndex<K>, IVector<V>>>>> loader)
    {
      Ranges.Ranges<K> ranges = Ranges.Ranges<K>.NewRange(new Tuple<Tuple<K, BoundaryBehavior>, Tuple<K, BoundaryBehavior>>(new Tuple<K, BoundaryBehavior>(min, BoundaryBehavior.get_Inclusive()), new Tuple<K, BoundaryBehavior>(max, BoundaryBehavior.get_Inclusive())));
      DelayedSource<K, V> source = new DelayedSource<K, V>(scheme, min, max, ranges, indexBuilder, vectorBuilder, (FSharpFunc<Tuple<Tuple<K, BoundaryBehavior>, Tuple<K, BoundaryBehavior>>[], FSharpAsync<Tuple<IIndex<K>, IVector<V>>>[]>) new DelayedSeries.source<K, V>(loader));
      return new Series<K, V>((IIndex<K>) new DelayedIndex<K, V>(source), (IVector<V>) new DelayedVector<K, V>(source), vectorBuilder, (IIndexBuilder) new DelayedIndexBuilder());
    }

    [Obsolete("The DelayedSeries.Create method has been renamed to DelayedSeries.FromValueLoader")]
    public static Series<a, b> Create<a, b>(a min, a max, FSharpFunc<Tuple<a, BoundaryBehavior>, FSharpFunc<Tuple<a, BoundaryBehavior>, FSharpAsync<IEnumerable<KeyValuePair<a, b>>>>> loader)
    {
      return DelayedSeries.FromValueLoader<a, b>(min, max, loader);
    }

    [Obsolete("The DelayedSeries.Create method has been renamed to DelayedSeries.FromValueLoader")]
    public static Series<a, b> Create<a, b>(a min, a max, Func<a, BoundaryBehavior, a, BoundaryBehavior, Task<IEnumerable<KeyValuePair<a, b>>>> loader)
    {
      return DelayedSeries.FromValueLoader<a, b>(min, max, loader);
    }
  }
}
