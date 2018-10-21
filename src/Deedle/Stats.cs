// Decompiled with JetBrains decompiler
// Type: Deedle.Stats
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using \u003CStartupCodeDeedle\u003E;


using System;
using System.Collections.Generic;

namespace Deedle
{
  
  [Serializable]
  public class Stats
  {
    
    public static Series<K, double> movingCount<K, V>(int size, Series<K, V> series)
    {
      int moment = 0;
      int winSize = size;
      FSharpFunc<StatsInternal.Sums, double> ftransf = (FSharpFunc<StatsInternal.Sums, double>) new Stats.movingCount();
      Series<K, V> series1 = series;
      if (winSize <= 0)
        throw new ArgumentException("Window must be positive", "windowSize");
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.movingCount((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.movingCount(winSize, (FSharpFunc<OptionalValue<double>[], StatsInternal.Sums>) new Stats.movingCount(moment), (FSharpFunc<StatsInternal.Sums, FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, StatsInternal.Sums>>>) new Stats.movingCount(moment), ftransf), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.movingCount()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.movingCount<V>(), series1));
    }

    
    public static Series<K, double> movingSum<K, V>(int size, Series<K, V> series)
    {
      int moment = 1;
      int winSize = size;
      FSharpFunc<StatsInternal.Sums, double> ftransf = (FSharpFunc<StatsInternal.Sums, double>) new Stats.movingSum();
      Series<K, V> series1 = series;
      if (winSize <= 0)
        throw new ArgumentException("Window must be positive", "windowSize");
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.movingSum((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.movingSum(winSize, (FSharpFunc<OptionalValue<double>[], StatsInternal.Sums>) new Stats.movingSum(moment), (FSharpFunc<StatsInternal.Sums, FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, StatsInternal.Sums>>>) new Stats.movingSum(moment), ftransf), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.movingSum()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.movingSum<V>(), series1));
    }

    
    public static Series<K, double> movingMean<K, V>(int size, Series<K, V> series)
    {
      int moment = 1;
      int winSize = size;
      FSharpFunc<StatsInternal.Sums, double> ftransf = (FSharpFunc<StatsInternal.Sums, double>) new Stats.movingMean();
      Series<K, V> series1 = series;
      if (winSize <= 0)
        throw new ArgumentException("Window must be positive", "windowSize");
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.movingMean((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.movingMean(winSize, (FSharpFunc<OptionalValue<double>[], StatsInternal.Sums>) new Stats.movingMean(moment), (FSharpFunc<StatsInternal.Sums, FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, StatsInternal.Sums>>>) new Stats.movingMean(moment), ftransf), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.movingMean()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.movingMean<V>(), series1));
    }

    
    public static Series<K, double> movingVariance<K, V>(int size, Series<K, V> series)
    {
      int moment = 2;
      int winSize = size;
      FSharpFunc<StatsInternal.Sums, double> ftransf = (FSharpFunc<StatsInternal.Sums, double>) new Stats.movingVariance();
      Series<K, V> series1 = series;
      if (winSize <= 0)
        throw new ArgumentException("Window must be positive", "windowSize");
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.movingVariance((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.movingVariance(winSize, (FSharpFunc<OptionalValue<double>[], StatsInternal.Sums>) new Stats.movingVariance(moment), (FSharpFunc<StatsInternal.Sums, FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, StatsInternal.Sums>>>) new Stats.movingVariance(moment), ftransf), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.movingVariance()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.movingVariance<V>(), series1));
    }

    
    public static Series<K, double> movingStdDev<K, V>(int size, Series<K, V> series)
    {
      int moment = 2;
      int winSize = size;
      FSharpFunc<StatsInternal.Sums, double> ftransf = (FSharpFunc<StatsInternal.Sums, double>) new Stats.movingStdDev((FSharpFunc<StatsInternal.Sums, double>) new Stats.movingStdDev(), (FSharpFunc<double, double>) new Stats.movingStdDev());
      Series<K, V> series1 = series;
      if (winSize <= 0)
        throw new ArgumentException("Window must be positive", "windowSize");
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.movingStdDev((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.movingStdDev(winSize, (FSharpFunc<OptionalValue<double>[], StatsInternal.Sums>) new Stats.movingStdDev(moment), (FSharpFunc<StatsInternal.Sums, FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, StatsInternal.Sums>>>) new Stats.movingStdDev(moment), ftransf), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.movingStdDev()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.movingStdDev0<V>(), series1));
    }

    
    public static Series<K, double> movingSkew<K, V>(int size, Series<K, V> series)
    {
      int moment = 3;
      int winSize = size;
      FSharpFunc<StatsInternal.Sums, double> ftransf = (FSharpFunc<StatsInternal.Sums, double>) new Stats.movingSkew();
      Series<K, V> series1 = series;
      if (winSize <= 0)
        throw new ArgumentException("Window must be positive", "windowSize");
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.movingSkew((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.movingSkew(winSize, (FSharpFunc<OptionalValue<double>[], StatsInternal.Sums>) new Stats.movingSkew(moment), (FSharpFunc<StatsInternal.Sums, FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, StatsInternal.Sums>>>) new Stats.movingSkew(moment), ftransf), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.movingSkew()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.movingSkew<V>(), series1));
    }

    
    public static Series<K, double> movingKurt<K, V>(int size, Series<K, V> series)
    {
      int moment = 4;
      int winSize = size;
      FSharpFunc<StatsInternal.Sums, double> ftransf = (FSharpFunc<StatsInternal.Sums, double>) new Stats.movingKurt();
      Series<K, V> series1 = series;
      if (winSize <= 0)
        throw new ArgumentException("Window must be positive", "windowSize");
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.movingKurt((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.movingKurt(winSize, (FSharpFunc<OptionalValue<double>[], StatsInternal.Sums>) new Stats.movingKurt(moment), (FSharpFunc<StatsInternal.Sums, FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, StatsInternal.Sums>>>) new Stats.movingKurt(moment), ftransf), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.movingKurt()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.movingKurt<V>(), series1));
    }

    
    public static Series<K, double> movingMin<K, V>(int size, Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.movingMin(size, (FSharpFunc<double, FSharpFunc<double, bool>>) new Stats.movingMin()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.movingMin<V>(), series));
    }

    
    public static Series<K, double> movingMax<K, V>(int size, Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.movingMax(size, (FSharpFunc<double, FSharpFunc<double, bool>>) new Stats.movingMax()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.movingMax<V>(), series));
    }

    public static Series<K, double> expandingCount<K, V>(Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.expandingCount((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.expandingCount(new StatsInternal.Moments(0.0, 0.0, 0.0, 0.0, 0.0, 0.0), (FSharpFunc<StatsInternal.Moments, FSharpFunc<OptionalValue<double>, StatsInternal.Moments>>) new Stats.expandingCount(), (FSharpFunc<StatsInternal.Moments, double>) new Stats.expandingCount()), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.expandingCount()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.expandingCount<V>(), series));
    }

    public static Series<K, double> expandingSum<K, V>(Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.expandingSum((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.expandingSum(new StatsInternal.Moments(0.0, 0.0, 0.0, 0.0, 0.0, 0.0), (FSharpFunc<StatsInternal.Moments, FSharpFunc<OptionalValue<double>, StatsInternal.Moments>>) new Stats.expandingSum(), (FSharpFunc<StatsInternal.Moments, double>) new Stats.expandingSum()), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.expandingSum()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.expandingSum<V>(), series));
    }

    public static Series<K, double> expandingMean<K, V>(Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.expandingMean((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.expandingMean(new StatsInternal.Moments(0.0, 0.0, 0.0, 0.0, 0.0, 0.0), (FSharpFunc<StatsInternal.Moments, FSharpFunc<OptionalValue<double>, StatsInternal.Moments>>) new Stats.expandingMean(), (FSharpFunc<StatsInternal.Moments, double>) new Stats.expandingMean()), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.expandingMean()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.expandingMean<V>(), series));
    }

    public static Series<K, double> expandingVariance<K, V>(Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.expandingVariance((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.expandingVariance(new StatsInternal.Moments(0.0, 0.0, 0.0, 0.0, 0.0, 0.0), (FSharpFunc<StatsInternal.Moments, FSharpFunc<OptionalValue<double>, StatsInternal.Moments>>) new Stats.expandingVariance(), (FSharpFunc<StatsInternal.Moments, double>) new Stats.toVar()), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.expandingVariance()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.expandingVariance<V>(), series));
    }

    public static Series<K, double> expandingStdDev<K, V>(Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.expandingStdDev((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.expandingStdDev(new StatsInternal.Moments(0.0, 0.0, 0.0, 0.0, 0.0, 0.0), (FSharpFunc<StatsInternal.Moments, FSharpFunc<OptionalValue<double>, StatsInternal.Moments>>) new Stats.expandingStdDev(), (FSharpFunc<StatsInternal.Moments, double>) new Stats.toStdDev()), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.expandingStdDev()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.expandingStdDev<V>(), series));
    }

    public static Series<K, double> expandingSkew<K, V>(Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.expandingSkew((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.expandingSkew(new StatsInternal.Moments(0.0, 0.0, 0.0, 0.0, 0.0, 0.0), (FSharpFunc<StatsInternal.Moments, FSharpFunc<OptionalValue<double>, StatsInternal.Moments>>) new Stats.expandingSkew(), (FSharpFunc<StatsInternal.Moments, double>) new Stats.toEstSkew()), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.expandingSkew()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.expandingSkew<V>(), series));
    }

    public static Series<K, double> expandingKurt<K, V>(Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.expandingKurt((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.expandingKurt(new StatsInternal.Moments(0.0, 0.0, 0.0, 0.0, 0.0, 0.0), (FSharpFunc<StatsInternal.Moments, FSharpFunc<OptionalValue<double>, StatsInternal.Moments>>) new Stats.expandingKurt(), (FSharpFunc<StatsInternal.Moments, double>) new Stats.toEstKurt()), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.expandingKurt()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.expandingKurt<V>(), series));
    }

    public static Series<K, double> expandingMin<K, V>(Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.expandingMin((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.expandingMin((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.expandingMin((FSharpFunc<double, FSharpFunc<OptionalValue<double>, double>>) new Stats.minFn(), Operators.get_NaN()), (FSharpFunc<IEnumerable<double>, IEnumerable<double>>) new Stats.expandingMin(1)), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.expandingMin()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.expandingMin<V>(), series));
    }

    public static Series<K, double> expandingMax<K, V>(Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new Stats.expandingMax((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.expandingMax((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new Stats.expandingMax((FSharpFunc<double, FSharpFunc<OptionalValue<double>, double>>) new Stats.maxFn(), Operators.get_NaN()), (FSharpFunc<IEnumerable<double>, IEnumerable<double>>) new Stats.expandingMax(1)), (FSharpFunc<IEnumerable<double>, double[]>) new Stats.expandingMax()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.expandingMax<V>(), series));
    }

    public static int count<K, V>(Series<K, V> series)
    {
      return series.ValueCount;
    }

    public static double sum<K, V>(Series<K, V> series)
    {
      return (double) SeqModule.Fold<V, double>((FSharpFunc<M1, FSharpFunc<M0, M1>>) new Stats.sum<V>(), (M1) Operators.get_NaN(), (IEnumerable<M0>) series.Values);
    }

    public static V numSum<K, V>(Series<K, V> series)
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

    public static double mean<K, V>(Series<K, V> series)
    {
      int num1 = 1;
      OptionalValue<V>[] optionalValueArray = StatsInternal.valuesAllOpt<K, V>(series);
      int num2 = num1;
      V[] vArray1 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new Stats.sums<V>(), (M0[]) optionalValueArray);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new Stats.sums<V>();
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

    public static double variance<K, V>(Series<K, V> series)
    {
      int num1 = 2;
      OptionalValue<V>[] optionalValueArray = StatsInternal.valuesAllOpt<K, V>(series);
      int num2 = num1;
      V[] vArray1 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new Stats.variance<V>(), (M0[]) optionalValueArray);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new Stats.variance<V>();
      V[] vArray2 = vArray1;
      if ((object) vArray2 == null)
        throw new ArgumentNullException("array");
      double[] init = new double[vArray2.Length];
      int moment = num2;
      for (int index = 0; index < init.Length; ++index)
        init[index] = fsharpFunc.Invoke(vArray2[index]);
      return StatsInternal.varianceSums(StatsInternal.initSumsDense(moment, init));
    }

    public static double stdDev<K, V>(Series<K, V> series)
    {
      int num1 = 2;
      OptionalValue<V>[] optionalValueArray = StatsInternal.valuesAllOpt<K, V>(series);
      int num2 = num1;
      V[] vArray1 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new Stats.stdDev<V>(), (M0[]) optionalValueArray);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new Stats.stdDev<V>();
      V[] vArray2 = vArray1;
      if ((object) vArray2 == null)
        throw new ArgumentNullException("array");
      double[] init = new double[vArray2.Length];
      int moment = num2;
      for (int index = 0; index < init.Length; ++index)
        init[index] = fsharpFunc.Invoke(vArray2[index]);
      return Math.Sqrt(StatsInternal.varianceSums(StatsInternal.initSumsDense(moment, init)));
    }

    public static double skew<K, V>(Series<K, V> series)
    {
      int num1 = 3;
      OptionalValue<V>[] optionalValueArray = StatsInternal.valuesAllOpt<K, V>(series);
      int num2 = num1;
      V[] vArray1 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new Stats.skew<V>(), (M0[]) optionalValueArray);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new Stats.skew<V>();
      V[] vArray2 = vArray1;
      if ((object) vArray2 == null)
        throw new ArgumentNullException("array");
      double[] init = new double[vArray2.Length];
      int moment = num2;
      for (int index = 0; index < init.Length; ++index)
        init[index] = fsharpFunc.Invoke(vArray2[index]);
      return StatsInternal.skewSums(StatsInternal.initSumsDense(moment, init));
    }

    public static double kurt<K, V>(Series<K, V> series)
    {
      int num1 = 4;
      OptionalValue<V>[] optionalValueArray = StatsInternal.valuesAllOpt<K, V>(series);
      int num2 = num1;
      V[] vArray1 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new Stats.kurt<V>(), (M0[]) optionalValueArray);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new Stats.kurt<V>();
      V[] vArray2 = vArray1;
      if ((object) vArray2 == null)
        throw new ArgumentNullException("array");
      double[] init = new double[vArray2.Length];
      int moment = num2;
      for (int index = 0; index < init.Length; ++index)
        init[index] = fsharpFunc.Invoke(vArray2[index]);
      return StatsInternal.kurtSums(StatsInternal.initSumsDense(moment, init));
    }

    public static FSharpOption<V> tryMin<K, V>(Series<K, V> series)
    {
      FSharpFunc<V, FSharpFunc<V, V>> fsharpFunc = (FSharpFunc<V, FSharpFunc<V, V>>) new Stats.tryMin<V>();
      Series<K, V> series1 = series;
      V v = default (V);
      bool flag = false;
      IEnumerator<OptionalValue<V>> enumerator = FVectorextensionscore.IVector`1get_DataSequence<V>(series1.Vector).GetEnumerator();
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
      if (flag)
        return FSharpOption<V>.Some(v);
      return (FSharpOption<V>) null;
    }

    public static FSharpOption<V> tryMax<K, V>(Series<K, V> series)
    {
      FSharpFunc<V, FSharpFunc<V, V>> fsharpFunc = (FSharpFunc<V, FSharpFunc<V, V>>) new Stats.tryMax<V>();
      Series<K, V> series1 = series;
      V v = default (V);
      bool flag = false;
      IEnumerator<OptionalValue<V>> enumerator = FVectorextensionscore.IVector`1get_DataSequence<V>(series1.Vector).GetEnumerator();
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
      if (flag)
        return FSharpOption<V>.Some(v);
      return (FSharpOption<V>) null;
    }

    public static double min<K, V>(Series<K, V> series)
    {
      IEnumerable<V> values = series.Values;
      if (SeqModule.IsEmpty<V>((IEnumerable<M0>) values))
        return Operators.get_NaN();
      IEnumerable<double> doubles = (IEnumerable<double>) SeqModule.Map<V, double>((FSharpFunc<M0, M1>) new Stats.min<V>(), (IEnumerable<M0>) values);
      if ((object) doubles == null)
        throw new ArgumentNullException("source");
      IEnumerator<double> enumerator = doubles.GetEnumerator();
      try
      {
        if (!enumerator.MoveNext())
          throw new ArgumentException(LanguagePrimitives.ErrorStrings.get_InputSequenceEmptyString(), "source");
        double num = enumerator.Current;
        while (enumerator.MoveNext())
        {
          double current = enumerator.Current;
          if (current < num)
            num = current;
        }
        return num;
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
    }

    public static double max<K, V>(Series<K, V> series)
    {
      IEnumerable<V> values = series.Values;
      if (SeqModule.IsEmpty<V>((IEnumerable<M0>) values))
        return Operators.get_NaN();
      IEnumerable<double> doubles = (IEnumerable<double>) SeqModule.Map<V, double>((FSharpFunc<M0, M1>) new Stats.max<V>(), (IEnumerable<M0>) values);
      if ((object) doubles == null)
        throw new ArgumentNullException("source");
      IEnumerator<double> enumerator = doubles.GetEnumerator();
      try
      {
        if (!enumerator.MoveNext())
          throw new ArgumentException(LanguagePrimitives.ErrorStrings.get_InputSequenceEmptyString(), "source");
        double num = enumerator.Current;
        while (enumerator.MoveNext())
        {
          double current = enumerator.Current;
          if (current > num)
            num = current;
        }
        return num;
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
    }

    public static int uniqueCount<K, V>(Series<K, V> series)
    {
      return SeqModule.Length<V>(SeqModule.Distinct<V>((IEnumerable<M0>) series.Values));
    }

    
    public static FSharpOption<Tuple<K, T>> maxBy<T, a, K>(FSharpFunc<T, a> f, Series<K, T> series)
    {
      if (series.ValueCount == 0)
        return (FSharpOption<Tuple<K, T>>) null;
      IEnumerable<Tuple<K, T>> observations = SeriesModule.GetObservations<K, T>(series);
      FSharpFunc<Tuple<K, T>, a> fsharpFunc = (FSharpFunc<Tuple<K, T>, a>) new Stats.maxBy<K, T, a>((FSharpFunc<Tuple<K, T>, T>) new Stats.maxBy<K, T>(), f);
      IEnumerable<Tuple<K, T>> tuples = observations;
      if ((object) tuples == null)
        throw new ArgumentNullException("source");
      IEnumerator<Tuple<K, T>> enumerator = tuples.GetEnumerator();
      Tuple<K, T> tuple1;
      try
      {
        if (!enumerator.MoveNext())
          throw new ArgumentException(LanguagePrimitives.ErrorStrings.get_InputSequenceEmptyString(), "source");
        Tuple<K, T> current1 = enumerator.Current;
        a a1 = fsharpFunc.Invoke(current1);
        Tuple<K, T> tuple2 = current1;
        while (enumerator.MoveNext())
        {
          Tuple<K, T> current2 = enumerator.Current;
          a a2 = fsharpFunc.Invoke(current2);
          a a3 = a1;
          if (LanguagePrimitives.HashCompare.GenericGreaterThanIntrinsic<a>((M0) a2, (M0) a3))
          {
            a1 = a2;
            tuple2 = current2;
          }
        }
        tuple1 = tuple2;
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
      return FSharpOption<Tuple<K, T>>.Some(tuple1);
    }

    
    public static FSharpOption<Tuple<K, T>> minBy<T, a, K>(FSharpFunc<T, a> f, Series<K, T> series)
    {
      if (series.ValueCount == 0)
        return (FSharpOption<Tuple<K, T>>) null;
      IEnumerable<Tuple<K, T>> observations = SeriesModule.GetObservations<K, T>(series);
      FSharpFunc<Tuple<K, T>, a> fsharpFunc = (FSharpFunc<Tuple<K, T>, a>) new Stats.minBy<K, T, a>((FSharpFunc<Tuple<K, T>, T>) new Stats.minBy<K, T>(), f);
      IEnumerable<Tuple<K, T>> tuples = observations;
      if ((object) tuples == null)
        throw new ArgumentNullException("source");
      IEnumerator<Tuple<K, T>> enumerator = tuples.GetEnumerator();
      Tuple<K, T> tuple1;
      try
      {
        if (!enumerator.MoveNext())
          throw new ArgumentException(LanguagePrimitives.ErrorStrings.get_InputSequenceEmptyString(), "source");
        Tuple<K, T> current1 = enumerator.Current;
        a a1 = fsharpFunc.Invoke(current1);
        Tuple<K, T> tuple2 = current1;
        while (enumerator.MoveNext())
        {
          Tuple<K, T> current2 = enumerator.Current;
          a a2 = fsharpFunc.Invoke(current2);
          a a3 = a1;
          if (LanguagePrimitives.HashCompare.GenericLessThanIntrinsic<a>((M0) a2, (M0) a3))
          {
            a1 = a2;
            tuple2 = current2;
          }
        }
        tuple1 = tuple2;
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
      return FSharpOption<Tuple<K, T>>.Some(tuple1);
    }

    public static double median<K, V>(Series<K, V> series)
    {
      V[] vArray1 = (V[]) ArrayModule.OfSeq<V>((IEnumerable<M0>) series.Values);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new Stats.values<V>();
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

    public static Series<string, double> quantile<K, V>(double[] quantiles, Series<K, V> series)
    {
      V[] array = (V[]) SeqModule.ToArray<V>((IEnumerable<M0>) series.Values);
      FSharpFunc<V, double> fsharpFunc1 = (FSharpFunc<V, double>) new Stats.vals<V>();
      V[] vArray = array;
      if ((object) vArray == null)
        throw new ArgumentNullException("array");
      double[] numArray1 = new double[vArray.Length];
      for (int index = 0; index < numArray1.Length; ++index)
        numArray1[index] = fsharpFunc1.Invoke(vArray[index]);
      double[] vals = (double[]) ArrayModule.Sort<double>((M0[]) numArray1);
      int valsLength = ArrayModule.Length<double>((M0[]) vals);
      if (valsLength == 0)
      {
        double[] numArray2 = quantiles;
        FSharpFunc<double, Tuple<string, double>> fsharpFunc2 = (FSharpFunc<double, Tuple<string, double>>) new Stats.quantile();
        double[] numArray3 = numArray2;
        if ((object) numArray3 == null)
          throw new ArgumentNullException("array");
        Tuple<string, double>[] tupleArray = new Tuple<string, double>[numArray3.Length];
        for (int index = 0; index < tupleArray.Length; ++index)
          tupleArray[index] = fsharpFunc2.Invoke(numArray3[index]);
        return FSeriesextensions.Series.ofObservations<string, double>((IEnumerable<Tuple<string, double>>) tupleArray);
      }
      double[] numArray4 = quantiles;
      FSharpFunc<double, Tuple<string, double>> fsharpFunc3 = (FSharpFunc<double, Tuple<string, double>>) new Stats.quantile(vals, valsLength);
      double[] numArray5 = numArray4;
      if ((object) numArray5 == null)
        throw new ArgumentNullException("array");
      Tuple<string, double>[] tupleArray1 = new Tuple<string, double>[numArray5.Length];
      for (int index = 0; index < tupleArray1.Length; ++index)
        tupleArray1[index] = fsharpFunc3.Invoke(numArray5[index]);
      return FSeriesextensions.Series.ofObservations<string, double>((IEnumerable<Tuple<string, double>>) tupleArray1);
    }

    public static Series<string, double> describe<K, V>(Series<K, V> series)
    {
      double[] numArray1 = new double[3]{ 0.25, 0.5, 0.75 };
      V[] array = (V[]) SeqModule.ToArray<V>((IEnumerable<M0>) series.Values);
      FSharpFunc<V, double> fsharpFunc1 = (FSharpFunc<V, double>) new Stats.quantileResult<V>();
      V[] vArray1 = array;
      if ((object) vArray1 == null)
        throw new ArgumentNullException("array");
      double[] numArray2 = new double[vArray1.Length];
      for (int index = 0; index < numArray2.Length; ++index)
        numArray2[index] = fsharpFunc1.Invoke(vArray1[index]);
      double[] vals = (double[]) ArrayModule.Sort<double>((M0[]) numArray2);
      int valsLength = ArrayModule.Length<double>((M0[]) vals);
      Series<string, double> series1;
      if (valsLength == 0)
      {
        double[] numArray3 = numArray1;
        FSharpFunc<double, Tuple<string, double>> fsharpFunc2 = (FSharpFunc<double, Tuple<string, double>>) new Stats.quantileResult();
        double[] numArray4 = numArray3;
        if ((object) numArray4 == null)
          throw new ArgumentNullException("array");
        Tuple<string, double>[] tupleArray = new Tuple<string, double>[numArray4.Length];
        for (int index = 0; index < tupleArray.Length; ++index)
          tupleArray[index] = fsharpFunc2.Invoke(numArray4[index]);
        series1 = FSeriesextensions.Series.ofObservations<string, double>((IEnumerable<Tuple<string, double>>) tupleArray);
      }
      else
      {
        double[] numArray3 = numArray1;
        FSharpFunc<double, Tuple<string, double>> fsharpFunc2 = (FSharpFunc<double, Tuple<string, double>>) new Stats.quantileResult(vals, valsLength);
        double[] numArray4 = numArray3;
        if ((object) numArray4 == null)
          throw new ArgumentNullException("array");
        Tuple<string, double>[] tupleArray = new Tuple<string, double>[numArray4.Length];
        for (int index = 0; index < tupleArray.Length; ++index)
          tupleArray[index] = fsharpFunc2.Invoke(numArray4[index]);
        series1 = FSeriesextensions.Series.ofObservations<string, double>((IEnumerable<Tuple<string, double>>) tupleArray);
      }
      Series<string, double> series2 = series1;
      Tuple<string, double> tuple1 = new Tuple<string, double>("unique", (double) SeqModule.Length<V>(SeqModule.Distinct<V>((IEnumerable<M0>) series.Values)));
      string str1 = "mean";
      Series<K, V> series3 = series;
      int num1 = 1;
      OptionalValue<V>[] optionalValueArray1 = StatsInternal.valuesAllOpt<K, V>(series3);
      int num2 = num1;
      V[] vArray2 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new Stats.describe<V>(), (M0[]) optionalValueArray1);
      FSharpFunc<V, double> fsharpFunc3 = (FSharpFunc<V, double>) new Stats.describe<V>();
      V[] vArray3 = vArray2;
      if ((object) vArray3 == null)
        throw new ArgumentNullException("array");
      double[] init1 = new double[vArray3.Length];
      int moment1 = num2;
      string str2 = str1;
      Tuple<string, double> tuple2 = tuple1;
      for (int index = 0; index < init1.Length; ++index)
        init1[index] = fsharpFunc3.Invoke(vArray3[index]);
      Tuple<string, double> tuple3 = tuple2;
      string str3 = str2;
      StatsInternal.Sums sums = StatsInternal.initSumsDense(moment1, init1);
      double num3 = sums.sum / sums.nobs;
      Tuple<string, double> tuple4 = new Tuple<string, double>(str3, num3);
      string str4 = "std";
      Series<K, V> series4 = series;
      int num4 = 2;
      OptionalValue<V>[] optionalValueArray2 = StatsInternal.valuesAllOpt<K, V>(series4);
      int num5 = num4;
      V[] vArray4 = ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new Stats.describe<V>(), (M0[]) optionalValueArray2);
      FSharpFunc<V, double> fsharpFunc4 = (FSharpFunc<V, double>) new Stats.describe<V>();
      V[] vArray5 = vArray4;
      if ((object) vArray5 == null)
        throw new ArgumentNullException("array");
      double[] init2 = new double[vArray5.Length];
      int moment2 = num5;
      string str5 = str4;
      Tuple<string, double> tuple5 = tuple4;
      Tuple<string, double> tuple6 = tuple3;
      for (int index = 0; index < init2.Length; ++index)
        init2[index] = fsharpFunc4.Invoke(vArray5[index]);
      Tuple<string, double> tuple7 = tuple6;
      Tuple<string, double> tuple8 = tuple5;
      Tuple<string, double> tuple9 = new Tuple<string, double>(str5, Math.Sqrt(StatsInternal.varianceSums(StatsInternal.initSumsDense(moment2, init2))));
      string str6 = "min";
      IEnumerable<V> values1 = series.Values;
      Tuple<string, double> tuple10;
      Tuple<string, double> tuple11;
      Tuple<string, double> tuple12;
      string str7;
      double num6;
      if (SeqModule.IsEmpty<V>((IEnumerable<M0>) values1))
      {
        num6 = Operators.get_NaN();
        str7 = str6;
        tuple12 = tuple9;
        tuple11 = tuple8;
        tuple10 = tuple7;
      }
      else
      {
        IEnumerable<double> doubles = (IEnumerable<double>) SeqModule.Map<V, double>((FSharpFunc<M0, M1>) new Stats.describe<V>(), (IEnumerable<M0>) values1);
        if ((object) doubles == null)
          throw new ArgumentNullException("source");
        IEnumerator<double> enumerator = doubles.GetEnumerator();
        string str8 = str6;
        Tuple<string, double> tuple13 = tuple9;
        Tuple<string, double> tuple14 = tuple8;
        Tuple<string, double> tuple15 = tuple7;
        double num7;
        try
        {
          if (!enumerator.MoveNext())
            throw new ArgumentException(LanguagePrimitives.ErrorStrings.get_InputSequenceEmptyString(), "source");
          double num8 = enumerator.Current;
          while (enumerator.MoveNext())
          {
            double current = enumerator.Current;
            if (current < num8)
              num8 = current;
          }
          num7 = num8;
        }
        finally
        {
          (enumerator as IDisposable)?.Dispose();
        }
        tuple10 = tuple15;
        tuple11 = tuple14;
        tuple12 = tuple13;
        str7 = str8;
        num6 = num7;
      }
      FSharpList<Tuple<string, double>> fsharpList1 = FSharpList<Tuple<string, double>>.Cons(new Tuple<string, double>(str7, num6), FSharpList<Tuple<string, double>>.get_Empty());
      FSharpList<Tuple<string, double>> fsharpList2 = FSharpList<Tuple<string, double>>.Cons(tuple12, fsharpList1);
      FSharpList<Tuple<string, double>> fsharpList3 = FSharpList<Tuple<string, double>>.Cons(tuple11, fsharpList2);
      FSharpList<Tuple<string, double>> fsharpList4 = FSharpList<Tuple<string, double>>.Cons(tuple10, fsharpList3);
      FSharpList<M0> list = SeqModule.ToList<Tuple<string, double>>((IEnumerable<M0>) SeriesModule.GetObservations<string, double>(series2));
      string str9 = "max";
      IEnumerable<V> values2 = series.Values;
      FSharpList<Tuple<string, double>> fsharpList5;
      FSharpList<M0> fsharpList6;
      string str10;
      double num9;
      if (SeqModule.IsEmpty<V>((IEnumerable<M0>) values2))
      {
        num9 = Operators.get_NaN();
        str10 = str9;
        fsharpList6 = list;
        fsharpList5 = fsharpList4;
      }
      else
      {
        IEnumerable<double> doubles = (IEnumerable<double>) SeqModule.Map<V, double>((FSharpFunc<M0, M1>) new Stats.describe<V>(), (IEnumerable<M0>) values2);
        if ((object) doubles == null)
          throw new ArgumentNullException("source");
        IEnumerator<double> enumerator = doubles.GetEnumerator();
        string str8 = str9;
        FSharpList<Tuple<string, double>> fsharpList7 = (FSharpList<Tuple<string, double>>) list;
        FSharpList<Tuple<string, double>> fsharpList8 = fsharpList4;
        double num7;
        try
        {
          if (!enumerator.MoveNext())
            throw new ArgumentException(LanguagePrimitives.ErrorStrings.get_InputSequenceEmptyString(), "source");
          double num8 = enumerator.Current;
          while (enumerator.MoveNext())
          {
            double current = enumerator.Current;
            if (current > num8)
              num8 = current;
          }
          num7 = num8;
        }
        finally
        {
          (enumerator as IDisposable)?.Dispose();
        }
        fsharpList5 = fsharpList8;
        fsharpList6 = (FSharpList<M0>) fsharpList7;
        str10 = str8;
        num9 = num7;
      }
      FSharpList<FSharpList<Tuple<string, double>>> fsharpList9 = FSharpList<FSharpList<Tuple<string, double>>>.Cons(FSharpList<Tuple<string, double>>.Cons(new Tuple<string, double>(str10, num9), FSharpList<Tuple<string, double>>.get_Empty()), FSharpList<FSharpList<Tuple<string, double>>>.get_Empty());
      FSharpList<FSharpList<Tuple<string, double>>> fsharpList10 = FSharpList<FSharpList<Tuple<string, double>>>.Cons((FSharpList<Tuple<string, double>>) fsharpList6, fsharpList9);
      return FSeriesextensions.Series.ofObservations<string, double>((IEnumerable<Tuple<string, double>>) SeqModule.Concat<FSharpList<Tuple<string, double>>, Tuple<string, double>>((IEnumerable<M0>) FSharpList<FSharpList<Tuple<string, double>>>.Cons(fsharpList5, fsharpList10)));
    }

    
    public static Series<K, T> interpolate<K, T>(IEnumerable<K> keys, FSharpFunc<K, FSharpFunc<FSharpOption<Tuple<K, T>>, FSharpFunc<FSharpOption<Tuple<K, T>>, T>>> f, Series<K, T> series)
    {
      FSharpFunc<K, FSharpFunc<OptionalValue<KeyValuePair<K, T>>, FSharpFunc<OptionalValue<KeyValuePair<K, T>>, T>>> liftedf = (FSharpFunc<K, FSharpFunc<OptionalValue<KeyValuePair<K, T>>, FSharpFunc<OptionalValue<KeyValuePair<K, T>>, T>>>) new Stats.liftedf<K, T>(f);
      return series.Interpolate(keys, new Func<K, OptionalValue<KeyValuePair<K, T>>, OptionalValue<KeyValuePair<K, T>>, T>(new Stats.interpolate<K, T>(liftedf).Invoke));
    }

    
    public static Series<K, double> interpolateLinear<K, V>(IEnumerable<K> keys, FSharpFunc<K, FSharpFunc<K, double>> keyDiff, Series<K, V> series)
    {
      FSharpFunc<K, FSharpFunc<FSharpOption<Tuple<K, double>>, FSharpFunc<FSharpOption<Tuple<K, double>>, double>>> f = (FSharpFunc<K, FSharpFunc<FSharpOption<Tuple<K, double>>, FSharpFunc<FSharpOption<Tuple<K, double>>, double>>>) new Stats.linearF<K>(keyDiff);
      Series<K, double> series1 = SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new Stats.interpolateLinear5<V>(), series);
      return Stats.interpolate<K, double>(keys, f, series1);
    }

    public static Series<C, int> count<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, ObjectSeries<R>, int>((FSharpFunc<C, FSharpFunc<ObjectSeries<R>, int>>) new Stats.count<R, C>(), (Series<C, ObjectSeries<R>>) frame.Columns);
    }

    public static Series<C, double> sum<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, Series<R, double>, double>((FSharpFunc<C, FSharpFunc<Series<R, double>, double>>) new Stats.sum<R, C>(), frame.GetColumns<double>());
    }

    public static Series<C, double> mean<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, Series<R, double>, double>((FSharpFunc<C, FSharpFunc<Series<R, double>, double>>) new Stats.mean<R, C>(), frame.GetColumns<double>());
    }

    public static Series<C, double> median<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, Series<R, double>, double>((FSharpFunc<C, FSharpFunc<Series<R, double>, double>>) new Stats.median<R, C>(), frame.GetColumns<double>());
    }

    public static Series<C, double> stdDev<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, Series<R, double>, double>((FSharpFunc<C, FSharpFunc<Series<R, double>, double>>) new Stats.stdDev<R, C>(), frame.GetColumns<double>());
    }

    public static Series<C, double> variance<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, Series<R, double>, double>((FSharpFunc<C, FSharpFunc<Series<R, double>, double>>) new Stats.variance<R, C>(), frame.GetColumns<double>());
    }

    public static Series<C, double> skew<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, Series<R, double>, double>((FSharpFunc<C, FSharpFunc<Series<R, double>, double>>) new Stats.skew<R, C>(), frame.GetColumns<double>());
    }

    public static Series<C, double> kurt<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, Series<R, double>, double>((FSharpFunc<C, FSharpFunc<Series<R, double>, double>>) new Stats.kurt<R, C>(), frame.GetColumns<double>());
    }

    public static Series<C, double> min<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, Series<R, double>, double>((FSharpFunc<C, FSharpFunc<Series<R, double>, double>>) new Stats.min<R, C>(), frame.GetColumns<double>());
    }

    public static Series<C, double> max<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, Series<R, double>, double>((FSharpFunc<C, FSharpFunc<Series<R, double>, double>>) new Stats.max<R, C>(), frame.GetColumns<double>());
    }

    public static Series<C, int> uniqueCount<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, ObjectSeries<R>, int>((FSharpFunc<C, FSharpFunc<ObjectSeries<R>, int>>) new Stats.uniqueCount<R, C>(), (Series<C, ObjectSeries<R>>) frame.Columns);
    }

    
    public static Series<L, int> levelCount<K, L, V>(FSharpFunc<K, L> level, Series<K, V> series)
    {
      FSharpFunc<K, L> level1 = level;
      return SeriesModule.MapValues<Series<K, V>, int, L>((FSharpFunc<Series<K, V>, int>) new Stats.levelCount<K, V>(), series.GroupBy<L>(new Func<KeyValuePair<K, V>, L>(new Stats.levelCount<K, L, V>(level1).Invoke)));
    }

    
    public static Series<L, double> levelSum<K, L, V>(FSharpFunc<K, L> level, Series<K, V> series)
    {
      FSharpFunc<K, L> level1 = level;
      return SeriesModule.MapValues<Series<K, V>, double, L>((FSharpFunc<Series<K, V>, double>) new Stats.levelSum<K, V>(), series.GroupBy<L>(new Func<KeyValuePair<K, V>, L>(new Stats.levelSum<K, L, V>(level1).Invoke)));
    }

    
    public static Series<L, double> levelMean<K, L, V>(FSharpFunc<K, L> level, Series<K, V> series)
    {
      FSharpFunc<K, L> level1 = level;
      return SeriesModule.MapValues<Series<K, V>, double, L>((FSharpFunc<Series<K, V>, double>) new Stats.levelMean<K, V>(), series.GroupBy<L>(new Func<KeyValuePair<K, V>, L>(new Stats.levelMean<K, L, V>(level1).Invoke)));
    }

    
    public static Series<L, double> levelMedian<K, L, V>(FSharpFunc<K, L> level, Series<K, V> series)
    {
      FSharpFunc<K, L> level1 = level;
      return SeriesModule.MapValues<Series<K, V>, double, L>((FSharpFunc<Series<K, V>, double>) new Stats.levelMedian<K, V>(), series.GroupBy<L>(new Func<KeyValuePair<K, V>, L>(new Stats.levelMedian<K, L, V>(level1).Invoke)));
    }

    
    public static Series<L, double> levelStdDev<K, L, V>(FSharpFunc<K, L> level, Series<K, V> series)
    {
      FSharpFunc<K, L> level1 = level;
      return SeriesModule.MapValues<Series<K, V>, double, L>((FSharpFunc<Series<K, V>, double>) new Stats.levelStdDev<K, V>(), series.GroupBy<L>(new Func<KeyValuePair<K, V>, L>(new Stats.levelStdDev<K, L, V>(level1).Invoke)));
    }

    
    public static Series<L, double> levelVariance<K, L, V>(FSharpFunc<K, L> level, Series<K, V> series)
    {
      FSharpFunc<K, L> level1 = level;
      return SeriesModule.MapValues<Series<K, V>, double, L>((FSharpFunc<Series<K, V>, double>) new Stats.levelVariance<K, V>(), series.GroupBy<L>(new Func<KeyValuePair<K, V>, L>(new Stats.levelVariance<K, L, V>(level1).Invoke)));
    }

    
    public static Series<L, double> levelSkew<K, L, V>(FSharpFunc<K, L> level, Series<K, V> series)
    {
      FSharpFunc<K, L> level1 = level;
      return SeriesModule.MapValues<Series<K, V>, double, L>((FSharpFunc<Series<K, V>, double>) new Stats.levelSkew<K, V>(), series.GroupBy<L>(new Func<KeyValuePair<K, V>, L>(new Stats.levelSkew<K, L, V>(level1).Invoke)));
    }

    
    public static Series<L, double> levelKurt<K, L, V>(FSharpFunc<K, L> level, Series<K, V> series)
    {
      FSharpFunc<K, L> level1 = level;
      return SeriesModule.MapValues<Series<K, V>, double, L>((FSharpFunc<Series<K, V>, double>) new Stats.levelKurt<K, V>(), series.GroupBy<L>(new Func<KeyValuePair<K, V>, L>(new Stats.levelKurt<K, L, V>(level1).Invoke)));
    }
  }
}
