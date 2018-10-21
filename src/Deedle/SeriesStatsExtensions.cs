// Decompiled with JetBrains decompiler
// Type: Deedle.SeriesStatsExtensions
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using \u003CStartupCodeDeedle\u003E;


using System;
using System.Collections.Generic;

namespace Deedle
{
  
  [Serializable]
  public class SeriesStatsExtensions
  {
    public static double Sum<K, V>(this Series<K, V> series)
    {
      return (double) SeqModule.Fold<V, double>((FSharpFunc<M1, FSharpFunc<M0, M1>>) new SeriesStatsExtensions.Sum<V>(), (M1) Operators.get_NaN(), (IEnumerable<M0>) series.Values);
    }

    public static V NumSum<K, V>(this Series<K, V> series)
    {
      IEnumerator<V> enumerator = series.Values.GetEnumerator();
      try
      {
        V v = (V) LanguagePrimitives.GenericZeroDynamic<V>();
        while (enumerator.MoveNext())
          v = (V) LanguagePrimitives.CheckedAdditionDynamic<V, V, V>((M0) v, enumerator.Current);
        return v;
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
    }

    public static V Min<K, V>(this Series<K, V> series)
    {
      Series<K, V> series1 = series;
      FSharpFunc<V, FSharpFunc<V, V>> fsharpFunc = (FSharpFunc<V, FSharpFunc<V, V>>) new SeriesStatsExtensions.Min<V>();
      Series<K, V> series2 = series1;
      V v = default (V);
      bool flag = false;
      IEnumerator<OptionalValue<V>> enumerator = FVectorextensionscore.IVector`1get_DataSequence<V>(series2.Vector).GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          OptionalValue<V> current = enumerator.Current;
          if (current.HasValue)
          {
            v = !flag ? current.Value : (V) FSharpFunc<V, V>.InvokeFast<V>((FSharpFunc<V, FSharpFunc<V, M0>>) fsharpFunc, v, current.Value);
            flag = true;
          }
        }
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
      return (V) OptionModule.GetValue<V>(!flag ? (FSharpOption<M0>) null : (FSharpOption<M0>) FSharpOption<V>.Some(v));
    }

    public static V Max<K, V>(this Series<K, V> series)
    {
      Series<K, V> series1 = series;
      FSharpFunc<V, FSharpFunc<V, V>> fsharpFunc = (FSharpFunc<V, FSharpFunc<V, V>>) new SeriesStatsExtensions.Max<V>();
      Series<K, V> series2 = series1;
      V v = default (V);
      bool flag = false;
      IEnumerator<OptionalValue<V>> enumerator = FVectorextensionscore.IVector`1get_DataSequence<V>(series2.Vector).GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          OptionalValue<V> current = enumerator.Current;
          if (current.HasValue)
          {
            v = !flag ? current.Value : (V) FSharpFunc<V, V>.InvokeFast<V>((FSharpFunc<V, FSharpFunc<V, M0>>) fsharpFunc, v, current.Value);
            flag = true;
          }
        }
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
      return (V) OptionModule.GetValue<V>(!flag ? (FSharpOption<M0>) null : (FSharpOption<M0>) FSharpOption<V>.Some(v));
    }

    public static double Mean<K, V>(this Series<K, V> series)
    {
      Series<K, V> series1 = series;
      int num1 = 1;
      OptionalValue<V>[] optionalValueArray = StatsInternal.valuesAllOpt<K, V>(series1);
      int num2 = num1;
      V[] vArray1 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new SeriesStatsExtensions.Mean<V>(), (M0[]) optionalValueArray);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new SeriesStatsExtensions.Mean<V>();
      V[] vArray2 = vArray1;
      if ((object) vArray2 == null)
        throw new ArgumentNullException("array");
      double[] init = new double[vArray2.Length];
      int moment = num2;
      for (int index = 0; index < init.Length; ++index)
        init[index] = fsharpFunc.Invoke(vArray2[index]);
      StatsInternal.Sums sums = StatsInternal.initSumsDense(moment, init);
      return sums.sum / sums.nobs;
    }

    public static double StdDev<K, V>(this Series<K, V> series)
    {
      Series<K, V> series1 = series;
      int num1 = 2;
      OptionalValue<V>[] optionalValueArray = StatsInternal.valuesAllOpt<K, V>(series1);
      int num2 = num1;
      V[] vArray1 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new SeriesStatsExtensions.StdDev<V>(), (M0[]) optionalValueArray);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new SeriesStatsExtensions.StdDev<V>();
      V[] vArray2 = vArray1;
      if ((object) vArray2 == null)
        throw new ArgumentNullException("array");
      double[] init = new double[vArray2.Length];
      int moment = num2;
      for (int index = 0; index < init.Length; ++index)
        init[index] = fsharpFunc.Invoke(vArray2[index]);
      return Math.Sqrt(StatsInternal.varianceSums(StatsInternal.initSumsDense(moment, init)));
    }

    public static double Skewness<K, V>(this Series<K, V> series)
    {
      Series<K, V> series1 = series;
      int num1 = 3;
      OptionalValue<V>[] optionalValueArray = StatsInternal.valuesAllOpt<K, V>(series1);
      int num2 = num1;
      V[] vArray1 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new SeriesStatsExtensions.Skewness<V>(), (M0[]) optionalValueArray);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new SeriesStatsExtensions.Skewness<V>();
      V[] vArray2 = vArray1;
      if ((object) vArray2 == null)
        throw new ArgumentNullException("array");
      double[] init = new double[vArray2.Length];
      int moment = num2;
      for (int index = 0; index < init.Length; ++index)
        init[index] = fsharpFunc.Invoke(vArray2[index]);
      return StatsInternal.skewSums(StatsInternal.initSumsDense(moment, init));
    }

    public static double Kurtosis<K, V>(this Series<K, V> series)
    {
      Series<K, V> series1 = series;
      int num1 = 4;
      OptionalValue<V>[] optionalValueArray = StatsInternal.valuesAllOpt<K, V>(series1);
      int num2 = num1;
      V[] vArray1 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new SeriesStatsExtensions.Kurtosis<V>(), (M0[]) optionalValueArray);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new SeriesStatsExtensions.Kurtosis<V>();
      V[] vArray2 = vArray1;
      if ((object) vArray2 == null)
        throw new ArgumentNullException("array");
      double[] init = new double[vArray2.Length];
      int moment = num2;
      for (int index = 0; index < init.Length; ++index)
        init[index] = fsharpFunc.Invoke(vArray2[index]);
      return StatsInternal.kurtSums(StatsInternal.initSumsDense(moment, init));
    }

    [Obsolete("Use StdDev instead")]
    public static double StandardDeviation<K, V>(this Series<K, V> series)
    {
      Series<K, V> series1 = series;
      int num1 = 2;
      OptionalValue<V>[] optionalValueArray = StatsInternal.valuesAllOpt<K, V>(series1);
      int num2 = num1;
      V[] vArray1 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new SeriesStatsExtensions.StandardDeviation<V>(), (M0[]) optionalValueArray);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new SeriesStatsExtensions.StandardDeviation<V>();
      V[] vArray2 = vArray1;
      if ((object) vArray2 == null)
        throw new ArgumentNullException("array");
      double[] init = new double[vArray2.Length];
      int moment = num2;
      for (int index = 0; index < init.Length; ++index)
        init[index] = fsharpFunc.Invoke(vArray2[index]);
      return Math.Sqrt(StatsInternal.varianceSums(StatsInternal.initSumsDense(moment, init)));
    }

    public static double Median<K, V>(this Series<K, V> series)
    {
      V[] vArray1 = (V[]) ArrayModule.OfSeq<V>((IEnumerable<M0>) series.Values);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new SeriesStatsExtensions.Median<V>();
      V[] vArray2 = vArray1;
      if ((object) vArray2 == null)
        throw new ArgumentNullException("array");
      double[] numArray = new double[vArray2.Length];
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] = fsharpFunc.Invoke(vArray2[index]);
      double[] arr = numArray;
      int n = arr.Length / 2;
      if (arr.Length == 0)
        return Operators.get_NaN();
      if (arr.Length % 2 == 1)
        return StatsInternal.quickSelectInplace(n, arr);
      return (StatsInternal.quickSelectInplace(n, arr) + StatsInternal.quickSelectInplace(n - 1, arr)) / 2.0;
    }

    public static Series<K2, double> MeanLevel<K1, V, K2>(this Series<K1, V> series, Func<K1, K2> groupSelector)
    {
      FSharpFunc<K1, K2> level = (FSharpFunc<K1, K2>) new SeriesStatsExtensions.MeanLevel<K1, K2>(groupSelector);
      return SeriesModule.MapValues<Series<K1, V>, double, K2>((FSharpFunc<Series<K1, V>, double>) new SeriesStatsExtensions.MeanLevel<K1, V>(), series.GroupBy<K2>(new Func<KeyValuePair<K1, V>, K2>(new SeriesStatsExtensions.MeanLevel<K1, V, K2>(level).Invoke)));
    }

    public static Series<K2, double> StdDevLevel<K1, V, K2>(Series<K1, V> series, Func<K1, K2> groupSelector)
    {
      FSharpFunc<K1, K2> level = (FSharpFunc<K1, K2>) new SeriesStatsExtensions.StdDevLevel<K1, K2>(groupSelector);
      return SeriesModule.MapValues<Series<K1, V>, double, K2>((FSharpFunc<Series<K1, V>, double>) new SeriesStatsExtensions.StdDevLevel<K1, V>(), series.GroupBy<K2>(new Func<KeyValuePair<K1, V>, K2>(new SeriesStatsExtensions.StdDevLevel<K1, V, K2>(level).Invoke)));
    }

    public static Series<K2, double> MedianLevel<K1, V, K2>(this Series<K1, V> series, Func<K1, K2> groupSelector)
    {
      FSharpFunc<K1, K2> level = (FSharpFunc<K1, K2>) new SeriesStatsExtensions.MedianLevel<K1, K2>(groupSelector);
      return SeriesModule.MapValues<Series<K1, V>, double, K2>((FSharpFunc<Series<K1, V>, double>) new SeriesStatsExtensions.MedianLevel<K1, V>(), series.GroupBy<K2>(new Func<KeyValuePair<K1, V>, K2>(new SeriesStatsExtensions.MedianLevel<K1, V, K2>(level).Invoke)));
    }

    public static Series<K2, double> SumLevel<K1, V, K2>(this Series<K1, V> series, Func<K1, K2> groupSelector)
    {
      FSharpFunc<K1, K2> level = (FSharpFunc<K1, K2>) new SeriesStatsExtensions.SumLevel<K1, K2>(groupSelector);
      return SeriesModule.MapValues<Series<K1, V>, double, K2>((FSharpFunc<Series<K1, V>, double>) new SeriesStatsExtensions.SumLevel<K1, V>(), series.GroupBy<K2>(new Func<KeyValuePair<K1, V>, K2>(new SeriesStatsExtensions.SumLevel<K1, V, K2>(level).Invoke)));
    }

    public static Series<K2, V> MinLevel<K1, V, K2>(this Series<K1, V> series, Func<K1, K2> groupSelector)
    {
      FSharpFunc<K1, K2> level = (FSharpFunc<K1, K2>) new SeriesStatsExtensions.MinLevel<K1, K2>(groupSelector);
      return SeriesModule.Flatten<K2, V>(SeriesModule.MapValues<Series<K1, V>, FSharpOption<V>, K2>((FSharpFunc<Series<K1, V>, FSharpOption<V>>) new SeriesStatsExtensions.MinLevel<K1, V>(), series.GroupBy<K2>(new Func<KeyValuePair<K1, V>, K2>(new SeriesStatsExtensions.MinLevel<K1, V, K2>(level).Invoke))));
    }

    public static Series<K2, V> MaxLevel<K1, V, K2>(this Series<K1, V> series, Func<K1, K2> groupSelector)
    {
      FSharpFunc<K1, K2> level = (FSharpFunc<K1, K2>) new SeriesStatsExtensions.MaxLevel<K1, K2>(groupSelector);
      return SeriesModule.Flatten<K2, V>(SeriesModule.MapValues<Series<K1, V>, FSharpOption<V>, K2>((FSharpFunc<Series<K1, V>, FSharpOption<V>>) new SeriesStatsExtensions.MaxLevel<K1, V>(), series.GroupBy<K2>(new Func<KeyValuePair<K1, V>, K2>(new SeriesStatsExtensions.MaxLevel<K1, V, K2>(level).Invoke))));
    }

    public static Series<K, double> InterpolateLinear<K, V>(this Series<K, V> series, IEnumerable<K> keys, Func<K, K, double> keyDiff)
    {
      Series<K, V> series1 = series;
      return Stats.interpolate<K, double>(keys, (FSharpFunc<K, FSharpFunc<FSharpOption<Tuple<K, double>>, FSharpFunc<FSharpOption<Tuple<K, double>>, double>>>) new SeriesStatsExtensions.InterpolateLinear<K>((FSharpFunc<K, FSharpFunc<K, double>>) new SeriesStatsExtensions.InterpolateLinear<K>(keyDiff)), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new SeriesStatsExtensions.InterpolateLinear<V>(), series1));
    }
  }
}
