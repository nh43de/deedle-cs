// Decompiled with JetBrains decompiler
// Type: Deedle.EnumerableExtensions
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using \u003CStartupCodeDeedle\u003E;


using System;
using System.Collections.Generic;

namespace Deedle
{
  
  [Serializable]
  public class EnumerableExtensions
  {
    public static Series<K, V> ToSeries<K, V>(this IEnumerable<KeyValuePair<K, V>> observations)
    {
      return FSeriesextensions.Series.ofObservations<K, V>((IEnumerable<Tuple<K, V>>) SeqModule.Map<KeyValuePair<K, V>, Tuple<K, V>>((FSharpFunc<M0, M1>) new SeriesExtensions.ToSeries<K, V>(), (IEnumerable<M0>) observations));
    }

    public static Series<K, V> ToSparseSeries<K, V>(this IEnumerable<KeyValuePair<K, OptionalValue<V>>> observations)
    {
      return FSeriesextensions.Series.ofOptionalObservations<K, V>((IEnumerable<Tuple<K, FSharpOption<V>>>) SeqModule.Map<KeyValuePair<K, OptionalValue<V>>, Tuple<K, FSharpOption<V>>>((FSharpFunc<M0, M1>) new SeriesExtensions.ToSparseSeries<K, V>(), (IEnumerable<M0>) observations));
    }

    public static Series<int, V> ToOrdinalSeries<V>(this IEnumerable<V> observations)
    {
      return FSeriesextensions.Series.ofValues<V>(observations);
    }
  }
}
