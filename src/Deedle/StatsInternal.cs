// Decompiled with JetBrains decompiler
// Type: Deedle.StatsInternal
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Internal;


using Microsoft.FSharp.Core.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deedle
{
  
  public static class StatsInternal
  {
    
    public static Series<K, double> applySeriesProj<K>(FSharpFunc<IEnumerable<OptionalValue<double>>, double[]> proj, Series<K, double> series)
    {
      double[] numArray = proj.Invoke(FVectorextensionscore.IVector`1get_DataSequence<double>(series.Vector));
      IVector<double> vector = series.VectorBuilder.Create<double>(numArray);
      return new Series<K, double>(series.Index, vector, series.VectorBuilder, series.IndexBuilder);
    }

    
    public static IEnumerable<double> movingWindowFn<a, b>(int winSize, FSharpFunc<a[], b> finit, FSharpFunc<b, FSharpFunc<a, FSharpFunc<a, b>>> fupdate, FSharpFunc<b, double> ftransf, IEnumerable<a> source)
    {
      return (IEnumerable<double>) new StatsInternal.movingWindowFn<a, b>(winSize, finit, fupdate, ftransf, source, (a[]) null, (FSharpRef<int>) null, (FSharpRef<int>) null, (FSharpRef<bool>) null, (FSharpRef<b>) null, (IEnumerator<a>) null, default (a), default (a), 0, 0.0);
    }

    
    public static StatsInternal.Sums initSumsDense(int moment, double[] init)
    {
      double nobs = (double) ArrayModule.Length<double>((M0[]) init);
      double num1;
      if (moment < 1)
      {
        num1 = 0.0;
      }
      else
      {
        double[] numArray = init;
        if ((object) numArray == null)
          throw new ArgumentNullException("array");
        double num2 = 0.0;
        for (int index = 0; index < numArray.Length; ++index)
          num2 += numArray[index];
        num1 = num2;
      }
      double sum = num1;
      double num3;
      if (moment < 2)
      {
        num3 = 0.0;
      }
      else
      {
        double[] numArray1 = init;
        FSharpFunc<double, double> fsharpFunc = (FSharpFunc<double, double>) new StatsInternal.sump2();
        double[] numArray2 = numArray1;
        if ((object) numArray2 == null)
          throw new ArgumentNullException("array");
        double num2 = 0.0;
        for (int index = 0; index < numArray2.Length; ++index)
          num2 += fsharpFunc.Invoke(numArray2[index]);
        num3 = num2;
      }
      double sump2 = num3;
      double num4;
      if (moment < 3)
      {
        num4 = 0.0;
      }
      else
      {
        double[] numArray1 = init;
        FSharpFunc<double, double> fsharpFunc = (FSharpFunc<double, double>) new StatsInternal.sump3();
        double[] numArray2 = numArray1;
        if ((object) numArray2 == null)
          throw new ArgumentNullException("array");
        double num2 = 0.0;
        for (int index = 0; index < numArray2.Length; ++index)
          num2 += fsharpFunc.Invoke(numArray2[index]);
        num4 = num2;
      }
      double sump3 = num4;
      double num5;
      if (moment < 4)
      {
        num5 = 0.0;
      }
      else
      {
        double[] numArray1 = init;
        FSharpFunc<double, double> fsharpFunc = (FSharpFunc<double, double>) new StatsInternal.sump4();
        double[] numArray2 = numArray1;
        if ((object) numArray2 == null)
          throw new ArgumentNullException("array");
        double num2 = 0.0;
        for (int index = 0; index < numArray2.Length; ++index)
          num2 += fsharpFunc.Invoke(numArray2[index]);
        num5 = num2;
      }
      double sump4 = num5;
      return new StatsInternal.Sums(nobs, sum, sump2, sump3, sump4);
    }

    
    public static StatsInternal.Sums updateSumsDense(int moment, StatsInternal.Sums state, double curr, double outg)
    {
      double sum = moment >= 1 ? state.sum + curr - outg : 0.0;
      double num1;
      if (moment < 2)
      {
        num1 = 0.0;
      }
      else
      {
        double sump2 = state.sump2;
        double num2 = curr;
        int num3 = 2;
        double num4 = num2;
        double num5 = num3 < 0 ? 1.0 / Operators.OperatorIntrinsics.PowDouble(num4, num3) : Operators.OperatorIntrinsics.PowDouble(num4, num3);
        double num6 = sump2 + num5;
        double num7 = outg;
        int num8 = 2;
        double num9 = num7;
        double num10 = num8 < 0 ? 1.0 / Operators.OperatorIntrinsics.PowDouble(num9, num8) : Operators.OperatorIntrinsics.PowDouble(num9, num8);
        num1 = num6 - num10;
      }
      double sump2_1 = num1;
      double num11;
      if (moment < 3)
      {
        num11 = 0.0;
      }
      else
      {
        double sump3 = state.sump3;
        double num2 = curr;
        int num3 = 3;
        double num4 = num2;
        double num5 = num3 < 0 ? 1.0 / Operators.OperatorIntrinsics.PowDouble(num4, num3) : Operators.OperatorIntrinsics.PowDouble(num4, num3);
        double num6 = sump3 + num5;
        double num7 = outg;
        int num8 = 3;
        double num9 = num7;
        double num10 = num8 < 0 ? 1.0 / Operators.OperatorIntrinsics.PowDouble(num9, num8) : Operators.OperatorIntrinsics.PowDouble(num9, num8);
        num11 = num6 - num10;
      }
      double sump3_1 = num11;
      double num12;
      if (moment < 4)
      {
        num12 = 0.0;
      }
      else
      {
        double sump4 = state.sump4;
        double num2 = curr;
        int num3 = 4;
        double num4 = num2;
        double num5 = num3 < 0 ? 1.0 / Operators.OperatorIntrinsics.PowDouble(num4, num3) : Operators.OperatorIntrinsics.PowDouble(num4, num3);
        double num6 = sump4 + num5;
        double num7 = outg;
        int num8 = 4;
        double num9 = num7;
        double num10 = num8 < 0 ? 1.0 / Operators.OperatorIntrinsics.PowDouble(num9, num8) : Operators.OperatorIntrinsics.PowDouble(num9, num8);
        num12 = num6 - num10;
      }
      double sump4_1 = num12;
      return new StatsInternal.Sums(state.nobs, sum, sump2_1, sump3_1, sump4_1);
    }

    
    public static StatsInternal.Sums initSumsSparse<V>(int moment, OptionalValue<V>[] init)
    {
      int num = moment;
      V[] vArray1 = (V[]) ArrayModule.Choose<OptionalValue<V>, V>((FSharpFunc<M0, FSharpOption<M1>>) new StatsInternal.initSumsSparse<V>(), (M0[]) init);
      FSharpFunc<V, double> fsharpFunc = (FSharpFunc<V, double>) new StatsInternal.initSumsSparse<V>();
      V[] vArray2 = vArray1;
      if ((object) vArray2 == null)
        throw new ArgumentNullException("array");
      double[] init1 = new double[vArray2.Length];
      int moment1 = num;
      for (int index = 0; index < init1.Length; ++index)
        init1[index] = fsharpFunc.Invoke(vArray2[index]);
      return StatsInternal.initSumsDense(moment1, init1);
    }

    
    public static StatsInternal.Sums updateSumsSparse(int moment, StatsInternal.Sums state, OptionalValue<double> curr, OptionalValue<double> outg)
    {
      Tuple<OptionalValue<double>, OptionalValue<double>> tuple = new Tuple<OptionalValue<double>, OptionalValue<double>>(curr, outg);
      FSharpChoice<Unit, double> fsharpChoice1 = OptionalValueModule.MissingPresent<double>(tuple.Item1);
      if (fsharpChoice1 is FSharpChoice<Unit, double>.Choice1Of2)
      {
        FSharpChoice<Unit, double> fsharpChoice2 = OptionalValueModule.MissingPresent<double>(tuple.Item2);
        if (fsharpChoice2 is FSharpChoice<Unit, double>.Choice1Of2)
          return state;
        double outg1 = ((FSharpChoice<Unit, double>.Choice2Of2) fsharpChoice2).get_Item();
        StatsInternal.Sums sums = StatsInternal.updateSumsDense(moment, state, 0.0, outg1);
        return new StatsInternal.Sums(state.nobs - 1.0, sums.sum, sums.sump2, sums.sump3, sums.sump4);
      }
      FSharpChoice<Unit, double> fsharpChoice3 = OptionalValueModule.MissingPresent<double>(tuple.Item2);
      if (!(fsharpChoice3 is FSharpChoice<Unit, double>.Choice1Of2))
      {
        double outg1 = ((FSharpChoice<Unit, double>.Choice2Of2) fsharpChoice3).get_Item();
        double curr1 = ((FSharpChoice<Unit, double>.Choice2Of2) fsharpChoice1).get_Item();
        return StatsInternal.updateSumsDense(moment, state, curr1, outg1);
      }
      double curr2 = ((FSharpChoice<Unit, double>.Choice2Of2) fsharpChoice1).get_Item();
      StatsInternal.Sums sums1 = StatsInternal.updateSumsDense(moment, state, curr2, 0.0);
      return new StatsInternal.Sums(state.nobs + 1.0, sums1.sum, sums1.sump2, sums1.sump3, sums1.sump4);
    }

    
    public static Series<K, double> applyMovingSumsTransform<K, V>(int moment, int winSize, FSharpFunc<StatsInternal.Sums, double> proj, Series<K, V> series)
    {
      if (winSize <= 0)
        throw new ArgumentException("Window must be positive", "windowSize");
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new StatsInternal.calcSparse((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new StatsInternal.calcSparse(winSize, (FSharpFunc<OptionalValue<double>[], StatsInternal.Sums>) new StatsInternal.calcSparse(moment), (FSharpFunc<StatsInternal.Sums, FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, StatsInternal.Sums>>>) new StatsInternal.calcSparse(moment), proj), (FSharpFunc<IEnumerable<double>, double[]>) new StatsInternal.calcSparse()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new StatsInternal.applyMovingSumsTransform<V>(), series));
    }

    public static double varianceSums(StatsInternal.Sums s)
    {
      double num = (s.nobs * s.sump2 - s.sum * s.sum) / (s.nobs * s.nobs - s.nobs);
      if (num < 0.0)
        return Operators.get_NaN();
      return num;
    }

    public static double skewSums(StatsInternal.Sums s)
    {
      double num1 = s.sum / s.nobs;
      double d = s.sump2 / s.nobs - num1 * num1;
      double num2 = s.sump3 / s.nobs - num1 * num1 * num1 - 3.0 * num1 * d;
      double num3 = Math.Sqrt(d);
      if ((d != 0.0 ? (s.nobs < 3.0 ? 1 : 0) : 1) != 0)
        return Operators.get_NaN();
      double num4 = Math.Sqrt(s.nobs * (s.nobs - 1.0)) * num2;
      double num5 = s.nobs - 2.0;
      double num6 = num3;
      int num7 = 3;
      double num8 = num6;
      double num9 = num7 < 0 ? 1.0 / Operators.OperatorIntrinsics.PowDouble(num8, num7) : Operators.OperatorIntrinsics.PowDouble(num8, num7);
      double num10 = num5 * num9;
      return num4 / num10;
    }

    public static double kurtSums(StatsInternal.Sums s)
    {
      double num1 = s.sum / s.nobs;
      double num2 = num1 * num1;
      double num3 = s.sump2 / s.nobs - num2;
      double num4 = num2 * num1;
      double num5 = s.sump3 / s.nobs - num4 - 3.0 * num1 * num3;
      double num6 = num4 * num1;
      double num7 = s.sump4 / s.nobs - num6 - 6.0 * num3 * num1 * num1 - 4.0 * num5 * num1;
      if ((num3 != 0.0 ? (s.nobs < 4.0 ? 1 : 0) : 1) != 0)
        return Operators.get_NaN();
      double num8 = (s.nobs * s.nobs - 1.0) * num7 / (num3 * num3);
      double num9 = 3.0;
      double num10 = s.nobs - 1.0;
      int num11 = 2;
      double num12 = num10;
      double num13 = num11 < 0 ? 1.0 / Operators.OperatorIntrinsics.PowDouble(num12, num11) : Operators.OperatorIntrinsics.PowDouble(num12, num11);
      double num14 = num9 * num13;
      return (num8 - num14) / ((s.nobs - 2.0) * (s.nobs - 3.0));
    }

    
    public static double[] movingMinMaxHelper(int winSize, FSharpFunc<double, FSharpFunc<double, bool>> cmp, IEnumerable<OptionalValue<double>> s)
    {
      List<double> doubleList = new List<double>();
      FSharpRef<int> fsharpRef = (FSharpRef<int>) Operators.Ref<int>((M0) 0);
      Deque<Tuple<int, double>> deque = new Deque<Tuple<int, double>>();
      IEnumerator<OptionalValue<double>> enumerator = s.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          OptionalValue<double> current = enumerator.Current;
          Operators.op_ColonEquals<int>((FSharpRef<M0>) fsharpRef, (M0) (Operators.op_Dereference<int>((FSharpRef<M0>) fsharpRef) + 1));
          while ((deque.Count <= 0 ? 0 : (Operators.op_Dereference<int>((FSharpRef<M0>) fsharpRef) >= deque.First.Item1 ? 1 : 0)) != 0)
            deque.RemoveFirst();
          if (current.HasValue)
          {
            while ((deque.Count <= 0 ? 0 : (int) FSharpFunc<double, double>.InvokeFast<bool>((FSharpFunc<double, FSharpFunc<double, M0>>) cmp, deque.Last.Item2, current.Value)) != 0)
              deque.RemoveLast();
            deque.Add(new Tuple<int, double>(Operators.op_Dereference<int>((FSharpRef<M0>) fsharpRef) + winSize, current.Value));
            doubleList.Add(deque.First.Item2);
          }
          else
            doubleList.Add(!deque.IsEmpty ? deque.First.Item2 : Operators.get_NaN());
        }
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
      return doubleList.ToArray();
    }

    
    public static IEnumerable<c> expandingWindowFn<a, b, c>(a initState, FSharpFunc<a, FSharpFunc<b, a>> fupdate, FSharpFunc<a, c> ftransf, IEnumerable<b> source)
    {
      return (IEnumerable<c>) SeqModule.Map<a, c>((FSharpFunc<M0, M1>) ftransf, SeqModule.Skip<a>(1, (IEnumerable<M0>) SeqModule.Scan<b, a>((FSharpFunc<M1, FSharpFunc<M0, M1>>) fupdate, (M1) initState, (IEnumerable<M0>) source)));
    }

    
    public static StatsInternal.Moments updateMoments(StatsInternal.Moments state, double x)
    {
      StatsInternal.Moments moments = state;
      double sum1 = moments.sum;
      double nobs1 = moments.nobs;
      double m4_1 = moments.M4;
      double m3_1 = moments.M3;
      double m2_1 = moments.M2;
      double m1_1 = moments.M1;
      double num1 = nobs1;
      double nobs2 = nobs1 + 1.0;
      double num2 = x - m1_1;
      double num3 = num2 / nobs2;
      double num4 = num3 * num3;
      double num5 = num2 * num3 * num1;
      double m1_2 = m1_1 + num3;
      double m4_2 = m4_1 + num5 * num4 * (nobs2 * nobs2 - 3.0 * nobs2 + 3.0) + 6.0 * num4 * m2_1 - 4.0 * num3 * m3_1;
      double m3_2 = m3_1 + num5 * num3 * (nobs2 - 2.0) - 3.0 * num3 * m2_1;
      double m2_2 = m2_1 + num5;
      double sum2 = sum1 + x;
      return new StatsInternal.Moments(nobs2, sum2, m1_2, m2_2, m3_2, m4_2);
    }

    
    public static StatsInternal.Moments updateMomentsSparse(StatsInternal.Moments state, OptionalValue<double> curr)
    {
      FSharpChoice<Unit, double> fsharpChoice = OptionalValueModule.MissingPresent<double>(curr);
      if (fsharpChoice is FSharpChoice<Unit, double>.Choice1Of2)
        return state;
      double x = ((FSharpChoice<Unit, double>.Choice2Of2) fsharpChoice).get_Item();
      return StatsInternal.updateMoments(state, x);
    }

    
    public static Series<K, double> applyExpandingMomentsTransform<K, V>(FSharpFunc<StatsInternal.Moments, double> proj, Series<K, V> series)
    {
      return StatsInternal.applySeriesProj<K>((FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>) new StatsInternal.calcSparse0((FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>) new StatsInternal.calcSparse(new StatsInternal.Moments(0.0, 0.0, 0.0, 0.0, 0.0, 0.0), (FSharpFunc<StatsInternal.Moments, FSharpFunc<OptionalValue<double>, StatsInternal.Moments>>) new StatsInternal.calcSparse(), proj), (FSharpFunc<IEnumerable<double>, double[]>) new StatsInternal.calcSparse()), SeriesModule.MapValues<V, double, K>((FSharpFunc<V, double>) new StatsInternal.applyExpandingMomentsTransform<V>(), series));
    }

    
    internal static IEnumerable<double> expandingMinMaxHelper(FSharpFunc<double, FSharpFunc<double, bool>> cmp, IEnumerable<FSharpOption<double>> s)
    {
      return (IEnumerable<double>) new StatsInternal.expandingMinMaxHelper(cmp, s, (FSharpRef<double>) null, (FSharpOption<double>) null, (IEnumerator<FSharpOption<double>>) null, 0, 0.0);
    }

    public static OptionalValue<b>[] valuesAllOpt<a, b>(Series<a, b> series)
    {
      return (OptionalValue<b>[]) ArrayModule.OfSeq<OptionalValue<b>>((IEnumerable<M0>) FVectorextensionscore.IVector`1get_DataSequence<b>(series.Vector));
    }

    
    public static FSharpOption<V> trySeriesExtreme<V, K>(FSharpFunc<V, FSharpFunc<V, V>> f, Series<K, V> series)
    {
      V v = default (V);
      bool flag = false;
      IEnumerator<OptionalValue<V>> enumerator = FVectorextensionscore.IVector`1get_DataSequence<V>(series.Vector).GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          OptionalValue<V> current = enumerator.Current;
          if (current.HasValue)
          {
            v = !flag ? current.Value : FSharpFunc<V, V>.InvokeFast<V>(f, v, current.Value);
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

    
    public static double quickSelectInplace(int n, double[] arr)
    {
      FSharpFunc<int, FSharpFunc<int, Unit>> fsharpFunc = (FSharpFunc<int, FSharpFunc<int, Unit>>) new StatsInternal.swap(arr);
      FSharpFunc<int, FSharpFunc<int, FSharpFunc<int, int>>> partition = (FSharpFunc<int, FSharpFunc<int, FSharpFunc<int, int>>>) new StatsInternal.partition(arr);
      return (double) FSharpFunc<int, int>.InvokeFast<double>((FSharpFunc<int, FSharpFunc<int, M0>>) new StatsInternal.select(n, arr, partition), 0, arr.Length - 1);
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class movingWindowFn<a, b> : GeneratedSequenceBase<double>
    {
      public int winSize;
      public FSharpFunc<a[], b> finit;
      public FSharpFunc<b, FSharpFunc<a, FSharpFunc<a, b>>> fupdate;
      public FSharpFunc<b, double> ftransf;
      public IEnumerable<a> source;
      public a[] arr;
      public FSharpRef<int> r;
      public FSharpRef<int> i;
      public FSharpRef<bool> isInit;
      public FSharpRef<b> state;
      public IEnumerator<a> e;
      public a curr;
      public a outg;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public double current;

      public movingWindowFn(int winSize, FSharpFunc<a[], b> finit, FSharpFunc<b, FSharpFunc<a, FSharpFunc<a, b>>> fupdate, FSharpFunc<b, double> ftransf, IEnumerable<a> source, a[] arr, FSharpRef<int> r, FSharpRef<int> i, FSharpRef<bool> isInit, FSharpRef<b> state, IEnumerator<a> e, a curr, a outg, int pc, double current)
      {
        this.winSize = winSize;
        this.finit = finit;
        this.fupdate = fupdate;
        this.ftransf = ftransf;
        this.source = source;
        this.arr = arr;
        this.r = r;
        this.i = i;
        this.isInit = isInit;
        this.state = state;
        this.e = e;
        this.curr = curr;
        this.outg = outg;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<double> next)
      {
        switch (this.pc)
        {
          case 1:
label_10:
            this.pc = 4;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<a>>((M0) this.e);
            this.e = (IEnumerator<a>) null;
            this.state = (FSharpRef<b>) null;
            this.isInit = (FSharpRef<bool>) null;
            this.i = (FSharpRef<int>) null;
            this.r = (FSharpRef<int>) null;
            this.arr = (a[]) null;
            this.pc = 4;
            goto case 4;
          case 2:
          case 3:
            this.outg = default (a);
            this.curr = default (a);
            break;
          case 4:
            this.current = 0.0;
            return 0;
          default:
            this.arr = (a[]) ArrayModule.ZeroCreate<a>(this.winSize);
            this.r = (FSharpRef<int>) Operators.Ref<int>((M0) (this.winSize - 1));
            this.i = (FSharpRef<int>) Operators.Ref<int>((M0) 0);
            this.isInit = (FSharpRef<bool>) Operators.Ref<bool>((M0) 0);
            this.state = (FSharpRef<b>) Operators.Ref<b>((M0) default (b));
            this.e = this.source.GetEnumerator();
            this.pc = 1;
            break;
        }
        if (this.e.MoveNext())
        {
          this.curr = this.e.Current;
          this.outg = this.arr[Operators.op_Dereference<int>((FSharpRef<M0>) this.i)];
          this.arr[Operators.op_Dereference<int>((FSharpRef<M0>) this.i)] = this.curr;
          Operators.op_ColonEquals<int>((FSharpRef<M0>) this.i, (M0) ((Operators.op_Dereference<int>((FSharpRef<M0>) this.i) + 1) % this.winSize));
          if (Operators.op_Dereference<int>((FSharpRef<M0>) this.r) == null)
          {
            if (Operators.op_Dereference<bool>((FSharpRef<M0>) this.isInit) == null)
            {
              Operators.op_ColonEquals<b>((FSharpRef<M0>) this.state, (M0) this.finit.Invoke(this.arr));
              Operators.op_ColonEquals<bool>((FSharpRef<M0>) this.isInit, (M0) 1);
            }
            else
              Operators.op_ColonEquals<b>((FSharpRef<M0>) this.state, (M0) FSharpFunc<b, a>.InvokeFast<a, b>((FSharpFunc<b, FSharpFunc<a, FSharpFunc<M0, M1>>>) this.fupdate, (b) Operators.op_Dereference<b>((FSharpRef<M0>) this.state), this.curr, (M0) this.outg));
            this.pc = 2;
            this.current = this.ftransf.Invoke((b) Operators.op_Dereference<b>((FSharpRef<M0>) this.state));
            return 1;
          }
          Operators.op_ColonEquals<int>((FSharpRef<M0>) this.r, (M0) (Operators.op_Dereference<int>((FSharpRef<M0>) this.r) - 1));
          this.pc = 3;
          this.current = Operators.get_NaN();
          return 1;
        }
        goto label_10;
      }

      public virtual void Close()
      {
        Exception exception = (Exception) null;
        while (true)
        {
          switch (this.pc)
          {
            case 4:
              goto label_7;
            default:
              Unit unit;
              try
              {
                switch (this.pc)
                {
                  case 0:
                  case 4:
                    this.pc = 4;
                    this.current = 0.0;
                    unit = (Unit) null;
                    break;
                  default:
                    this.pc = 4;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<a>>((M0) this.e);
                    goto case 0;
                }
              }
              catch (object ex)
              {
                exception = (Exception) ex;
                unit = (Unit) null;
              }
              continue;
          }
        }
label_7:
        if (exception != null)
          throw exception;
      }

      public virtual bool get_CheckClose()
      {
        switch (this.pc)
        {
          case 0:
          case 4:
            return false;
          case 1:
            return true;
          case 3:
            return true;
          default:
            return true;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual double get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<double> GetFreshEnumerator()
      {
        return (IEnumerator<double>) new StatsInternal.movingWindowFn<a, b>(this.winSize, this.finit, this.fupdate, this.ftransf, this.source, (a[]) null, (FSharpRef<int>) null, (FSharpRef<int>) null, (FSharpRef<bool>) null, (FSharpRef<b>) null, (IEnumerator<a>) null, default (a), default (a), 0, 0.0);
      }
    }

    
    [Serializable]
    public sealed class Sums : IEquatable<StatsInternal.Sums>, IStructuralEquatable, IComparable<StatsInternal.Sums>, IComparable, IStructuralComparable
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      internal double nobs;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      internal double sum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      internal double sump2;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      internal double sump3;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      internal double sump4;

      
      public double nobs
      {
        get
        {
          return this.nobs;
        }
      }

      
      public double sum
      {
        get
        {
          return this.sum;
        }
      }

      
      public double sump2
      {
        get
        {
          return this.sump2;
        }
      }

      
      public double sump3
      {
        get
        {
          return this.sump3;
        }
      }

      
      public double sump4
      {
        get
        {
          return this.sump4;
        }
      }

      public Sums(double nobs, double sum, double sump2, double sump3, double sump4)
      {
        this.nobs = nobs;
        this.sum = sum;
        this.sump2 = sump2;
        this.sump3 = sump3;
        this.sump4 = sump4;
      }

      [CompilerGenerated]
      public override string ToString()
      {
        return ((FSharpFunc<StatsInternal.Sums, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<StatsInternal.Sums, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<StatsInternal.Sums, string>, Unit, string, string, StatsInternal.Sums>("%+A"))).Invoke(this);
      }

      [CompilerGenerated]
      public virtual int CompareTo(StatsInternal.Sums obj)
      {
        if (this != null)
        {
          if (obj == null)
            return 1;
          IComparer genericComparer1 = LanguagePrimitives.get_GenericComparer();
          double nobs1 = this.nobs;
          double nobs2 = obj.nobs;
          int num1 = nobs1 >= nobs2 ? (nobs1 <= nobs2 ? (nobs1 != nobs2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(genericComparer1, (M0) nobs1, (M0) nobs2) : 0) : 1) : -1;
          if (num1 < 0 || num1 > 0)
            return num1;
          IComparer genericComparer2 = LanguagePrimitives.get_GenericComparer();
          double sum1 = this.sum;
          double sum2 = obj.sum;
          int num2 = sum1 >= sum2 ? (sum1 <= sum2 ? (sum1 != sum2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(genericComparer2, (M0) sum1, (M0) sum2) : 0) : 1) : -1;
          if (num2 < 0 || num2 > 0)
            return num2;
          IComparer genericComparer3 = LanguagePrimitives.get_GenericComparer();
          double sump2_1 = this.sump2;
          double sump2_2 = obj.sump2;
          int num3 = sump2_1 >= sump2_2 ? (sump2_1 <= sump2_2 ? (sump2_1 != sump2_2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(genericComparer3, (M0) sump2_1, (M0) sump2_2) : 0) : 1) : -1;
          if (num3 < 0 || num3 > 0)
            return num3;
          IComparer genericComparer4 = LanguagePrimitives.get_GenericComparer();
          double sump3_1 = this.sump3;
          double sump3_2 = obj.sump3;
          int num4 = sump3_1 >= sump3_2 ? (sump3_1 <= sump3_2 ? (sump3_1 != sump3_2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(genericComparer4, (M0) sump3_1, (M0) sump3_2) : 0) : 1) : -1;
          if (num4 < 0 || num4 > 0)
            return num4;
          IComparer genericComparer5 = LanguagePrimitives.get_GenericComparer();
          double sump4_1 = this.sump4;
          double sump4_2 = obj.sump4;
          if (sump4_1 < sump4_2)
            return -1;
          if (sump4_1 > sump4_2)
            return 1;
          if (sump4_1 == sump4_2)
            return 0;
          return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(genericComparer5, (M0) sump4_1, (M0) sump4_2);
        }
        return obj != null ? -1 : 0;
      }

      [CompilerGenerated]
      public virtual int CompareTo(object obj)
      {
        return this.CompareTo((StatsInternal.Sums) obj);
      }

      [CompilerGenerated]
      public virtual int CompareTo(object obj, IComparer comp)
      {
        StatsInternal.Sums sums = (StatsInternal.Sums) obj;
        if (this != null)
        {
          if ((StatsInternal.Sums) obj == null)
            return 1;
          IComparer comparer1 = comp;
          double nobs1 = this.nobs;
          double nobs2 = sums.nobs;
          int num1 = nobs1 >= nobs2 ? (nobs1 <= nobs2 ? (nobs1 != nobs2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(comparer1, (M0) nobs1, (M0) nobs2) : 0) : 1) : -1;
          if (num1 < 0 || num1 > 0)
            return num1;
          IComparer comparer2 = comp;
          double sum1 = this.sum;
          double sum2 = sums.sum;
          int num2 = sum1 >= sum2 ? (sum1 <= sum2 ? (sum1 != sum2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(comparer2, (M0) sum1, (M0) sum2) : 0) : 1) : -1;
          if (num2 < 0 || num2 > 0)
            return num2;
          IComparer comparer3 = comp;
          double sump2_1 = this.sump2;
          double sump2_2 = sums.sump2;
          int num3 = sump2_1 >= sump2_2 ? (sump2_1 <= sump2_2 ? (sump2_1 != sump2_2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(comparer3, (M0) sump2_1, (M0) sump2_2) : 0) : 1) : -1;
          if (num3 < 0 || num3 > 0)
            return num3;
          IComparer comparer4 = comp;
          double sump3_1 = this.sump3;
          double sump3_2 = sums.sump3;
          int num4 = sump3_1 >= sump3_2 ? (sump3_1 <= sump3_2 ? (sump3_1 != sump3_2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(comparer4, (M0) sump3_1, (M0) sump3_2) : 0) : 1) : -1;
          if (num4 < 0 || num4 > 0)
            return num4;
          IComparer comparer5 = comp;
          double sump4_1 = this.sump4;
          double sump4_2 = sums.sump4;
          if (sump4_1 < sump4_2)
            return -1;
          if (sump4_1 > sump4_2)
            return 1;
          if (sump4_1 == sump4_2)
            return 0;
          return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(comparer5, (M0) sump4_1, (M0) sump4_2);
        }
        return (StatsInternal.Sums) obj != null ? -1 : 0;
      }

      [CompilerGenerated]
      public virtual int GetHashCode(IEqualityComparer comp)
      {
        if (this == null)
          return 0;
        int num1 = 0;
        int num2 = LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<double>(comp, (M0) this.sump4) + ((num1 << 6) + (num1 >> 2)) - 1640531527;
        int num3 = LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<double>(comp, (M0) this.sump3) + ((num2 << 6) + (num2 >> 2)) - 1640531527;
        int num4 = LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<double>(comp, (M0) this.sump2) + ((num3 << 6) + (num3 >> 2)) - 1640531527;
        int num5 = LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<double>(comp, (M0) this.sum) + ((num4 << 6) + (num4 >> 2)) - 1640531527;
        return LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<double>(comp, (M0) this.nobs) + ((num5 << 6) + (num5 >> 2)) - 1640531527;
      }

      [CompilerGenerated]
      public override sealed int GetHashCode()
      {
        return this.GetHashCode(LanguagePrimitives.get_GenericEqualityComparer());
      }

      [CompilerGenerated]
      public virtual bool Equals(object obj, IEqualityComparer comp)
      {
        if (this == null)
          return obj == null;
        StatsInternal.Sums sums1 = obj as StatsInternal.Sums;
        if (sums1 == null)
          return false;
        StatsInternal.Sums sums2 = sums1;
        if (this.nobs != sums2.nobs)
          return false;
        if (this.sum != sums2.sum)
          return false;
        if (this.sump2 != sums2.sump2)
          return false;
        if (this.sump3 != sums2.sump3)
          return false;
        return this.sump4 == sums2.sump4;
      }

      [CompilerGenerated]
      public virtual bool Equals(StatsInternal.Sums obj)
      {
        if (this == null)
          return obj == null;
        if (obj == null)
          return false;
        double nobs1 = this.nobs;
        double nobs2 = obj.nobs;
        if (((nobs1 == nobs1 ? 0 : (nobs2 != nobs2 ? 1 : 0)) == 0 ? (nobs1 == nobs2 ? 1 : 0) : 1) == 0)
          return false;
        double sum1 = this.sum;
        double sum2 = obj.sum;
        if (((sum1 == sum1 ? 0 : (sum2 != sum2 ? 1 : 0)) == 0 ? (sum1 == sum2 ? 1 : 0) : 1) == 0)
          return false;
        double sump2_1 = this.sump2;
        double sump2_2 = obj.sump2;
        if (((sump2_1 == sump2_1 ? 0 : (sump2_2 != sump2_2 ? 1 : 0)) == 0 ? (sump2_1 == sump2_2 ? 1 : 0) : 1) == 0)
          return false;
        double sump3_1 = this.sump3;
        double sump3_2 = obj.sump3;
        if (((sump3_1 == sump3_1 ? 0 : (sump3_2 != sump3_2 ? 1 : 0)) == 0 ? (sump3_1 == sump3_2 ? 1 : 0) : 1) == 0)
          return false;
        double sump4_1 = this.sump4;
        double sump4_2 = obj.sump4;
        if ((sump4_1 == sump4_1 ? 0 : (sump4_2 != sump4_2 ? 1 : 0)) != 0)
          return true;
        return sump4_1 == sump4_2;
      }

      [CompilerGenerated]
      public override sealed bool Equals(object obj)
      {
        StatsInternal.Sums sums = obj as StatsInternal.Sums;
        if (sums != null)
          return this.Equals(sums);
        return false;
      }
    }

    [Serializable]
    internal sealed class sump2 : FSharpFunc<double, double>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sump2()
      {
        base.ctor();
      }

      public virtual double Invoke(double x)
      {
        double num1 = x;
        int num2 = 2;
        double num3 = num1;
        if (num2 >= 0)
          return Operators.OperatorIntrinsics.PowDouble(num3, num2);
        return 1.0 / Operators.OperatorIntrinsics.PowDouble(num3, num2);
      }
    }

    [Serializable]
    internal sealed class sump3 : FSharpFunc<double, double>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sump3()
      {
        base.ctor();
      }

      public virtual double Invoke(double x)
      {
        double num1 = x;
        int num2 = 3;
        double num3 = num1;
        if (num2 >= 0)
          return Operators.OperatorIntrinsics.PowDouble(num3, num2);
        return 1.0 / Operators.OperatorIntrinsics.PowDouble(num3, num2);
      }
    }

    [Serializable]
    internal sealed class sump4 : FSharpFunc<double, double>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sump4()
      {
        base.ctor();
      }

      public virtual double Invoke(double x)
      {
        double num1 = x;
        int num2 = 4;
        double num3 = num1;
        if (num2 >= 0)
          return Operators.OperatorIntrinsics.PowDouble(num3, num2);
        return 1.0 / Operators.OperatorIntrinsics.PowDouble(num3, num2);
      }
    }

    [Serializable]
    internal sealed class initSumsSparse<V> : FSharpFunc<OptionalValue<V>, FSharpOption<V>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal initSumsSparse()
      {
        base.ctor();
      }

      public virtual FSharpOption<V> Invoke(OptionalValue<V> value)
      {
        if (value.HasValue)
          return FSharpOption<V>.Some(value.Value);
        return (FSharpOption<V>) null;
      }
    }

    [Serializable]
    internal sealed class initSumsSparse<V> : FSharpFunc<V, double>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal initSumsSparse()
      {
        base.ctor();
      }

      public virtual double Invoke(V x)
      {
        throw new NotSupportedException("Dynamic invocation of op_Explicit is not supported");
      }
    }

    [Serializable]
    internal sealed class calcSparse : FSharpFunc<OptionalValue<double>, FSharpOption<double>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal calcSparse()
      {
        base.ctor();
      }

      public virtual FSharpOption<double> Invoke(OptionalValue<double> value)
      {
        if (value.HasValue)
          return FSharpOption<double>.Some(value.Value);
        return (FSharpOption<double>) null;
      }
    }

    [Serializable]
    internal sealed class calcSparse : FSharpFunc<double, double>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal calcSparse()
      {
        base.ctor();
      }

      public virtual double Invoke(double x)
      {
        return x;
      }
    }

    [Serializable]
    internal sealed class calcSparse : FSharpFunc<OptionalValue<double>[], StatsInternal.Sums>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int moment;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal calcSparse(int moment)
      {
        this.ctor();
        this.moment = moment;
      }

      public virtual StatsInternal.Sums Invoke(OptionalValue<double>[] init)
      {
        int moment1 = this.moment;
        double[] numArray1 = (double[]) ArrayModule.Choose<OptionalValue<double>, double>((FSharpFunc<M0, FSharpOption<M1>>) new StatsInternal.calcSparse(), (M0[]) init);
        FSharpFunc<double, double> fsharpFunc = (FSharpFunc<double, double>) new StatsInternal.calcSparse();
        double[] numArray2 = numArray1;
        if ((object) numArray2 == null)
          throw new ArgumentNullException("array");
        double[] init1 = new double[numArray2.Length];
        int moment2 = moment1;
        for (int index = 0; index < init1.Length; ++index)
          init1[index] = fsharpFunc.Invoke(numArray2[index]);
        return StatsInternal.initSumsDense(moment2, init1);
      }
    }

    [Serializable]
    internal sealed class calcSparse : OptimizedClosures.FSharpFunc<StatsInternal.Sums, OptionalValue<double>, OptionalValue<double>, StatsInternal.Sums>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int moment;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal calcSparse(int moment)
      {
        this.ctor();
        this.moment = moment;
      }

      public virtual StatsInternal.Sums Invoke(StatsInternal.Sums state, OptionalValue<double> curr, OptionalValue<double> outg)
      {
        return StatsInternal.updateSumsSparse(this.moment, state, curr, outg);
      }
    }

    [Serializable]
    internal sealed class calcSparse : FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int winSize;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<OptionalValue<double>[], StatsInternal.Sums> finit;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<StatsInternal.Sums, FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, StatsInternal.Sums>>> fupdate;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<StatsInternal.Sums, double> ftransf;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal calcSparse(int winSize, FSharpFunc<OptionalValue<double>[], StatsInternal.Sums> finit, FSharpFunc<StatsInternal.Sums, FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, StatsInternal.Sums>>> fupdate, FSharpFunc<StatsInternal.Sums, double> ftransf)
      {
        this.ctor();
        this.winSize = winSize;
        this.finit = finit;
        this.fupdate = fupdate;
        this.ftransf = ftransf;
      }

      public virtual IEnumerable<double> Invoke(IEnumerable<OptionalValue<double>> source)
      {
        return StatsInternal.movingWindowFn<OptionalValue<double>, StatsInternal.Sums>(this.winSize, this.finit, this.fupdate, this.ftransf, source);
      }
    }

    [Serializable]
    internal sealed class calcSparse : FSharpFunc<IEnumerable<double>, double[]>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal calcSparse()
      {
        base.ctor();
      }

      public virtual double[] Invoke(IEnumerable<double> source)
      {
        return (double[]) ArrayModule.OfSeq<double>((IEnumerable<M0>) source);
      }
    }

    [Serializable]
    internal sealed class calcSparse : FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<IEnumerable<double>, double[]> g;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal calcSparse(FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>> f, FSharpFunc<IEnumerable<double>, double[]> g)
      {
        this.ctor();
        this.f = f;
        this.g = g;
      }

      public virtual double[] Invoke(IEnumerable<OptionalValue<double>> x)
      {
        return this.g.Invoke(this.f.Invoke(x));
      }
    }

    [Serializable]
    internal sealed class applyMovingSumsTransform<V> : FSharpFunc<V, double>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal applyMovingSumsTransform()
      {
        base.ctor();
      }

      public virtual double Invoke(V x)
      {
        throw new NotSupportedException("Dynamic invocation of op_Explicit is not supported");
      }
    }

    
    [Serializable]
    public sealed class Moments : IEquatable<StatsInternal.Moments>, IStructuralEquatable, IComparable<StatsInternal.Moments>, IComparable, IStructuralComparable
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      internal double nobs;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      internal double sum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      internal double M1;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      internal double M2;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      internal double M3;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      internal double M4;

      
      public double nobs
      {
        get
        {
          return this.nobs;
        }
      }

      
      public double sum
      {
        get
        {
          return this.sum;
        }
      }

      
      public double M1
      {
        get
        {
          return this.M1;
        }
      }

      
      public double M2
      {
        get
        {
          return this.M2;
        }
      }

      
      public double M3
      {
        get
        {
          return this.M3;
        }
      }

      
      public double M4
      {
        get
        {
          return this.M4;
        }
      }

      public Moments(double nobs, double sum, double m1, double m2, double m3, double m4)
      {
        this.nobs = nobs;
        this.sum = sum;
        this.M1 = m1;
        this.M2 = m2;
        this.M3 = m3;
        this.M4 = m4;
      }

      [CompilerGenerated]
      public override string ToString()
      {
        return ((FSharpFunc<StatsInternal.Moments, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<StatsInternal.Moments, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<StatsInternal.Moments, string>, Unit, string, string, StatsInternal.Moments>("%+A"))).Invoke(this);
      }

      [CompilerGenerated]
      public virtual int CompareTo(StatsInternal.Moments obj)
      {
        if (this != null)
        {
          if (obj == null)
            return 1;
          IComparer genericComparer1 = LanguagePrimitives.get_GenericComparer();
          double nobs1 = this.nobs;
          double nobs2 = obj.nobs;
          int num1 = nobs1 >= nobs2 ? (nobs1 <= nobs2 ? (nobs1 != nobs2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(genericComparer1, (M0) nobs1, (M0) nobs2) : 0) : 1) : -1;
          if (num1 < 0 || num1 > 0)
            return num1;
          IComparer genericComparer2 = LanguagePrimitives.get_GenericComparer();
          double sum1 = this.sum;
          double sum2 = obj.sum;
          int num2 = sum1 >= sum2 ? (sum1 <= sum2 ? (sum1 != sum2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(genericComparer2, (M0) sum1, (M0) sum2) : 0) : 1) : -1;
          if (num2 < 0 || num2 > 0)
            return num2;
          IComparer genericComparer3 = LanguagePrimitives.get_GenericComparer();
          double m1_1 = this.M1;
          double m1_2 = obj.M1;
          int num3 = m1_1 >= m1_2 ? (m1_1 <= m1_2 ? (m1_1 != m1_2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(genericComparer3, (M0) m1_1, (M0) m1_2) : 0) : 1) : -1;
          if (num3 < 0 || num3 > 0)
            return num3;
          IComparer genericComparer4 = LanguagePrimitives.get_GenericComparer();
          double m2_1 = this.M2;
          double m2_2 = obj.M2;
          int num4 = m2_1 >= m2_2 ? (m2_1 <= m2_2 ? (m2_1 != m2_2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(genericComparer4, (M0) m2_1, (M0) m2_2) : 0) : 1) : -1;
          if (num4 < 0 || num4 > 0)
            return num4;
          IComparer genericComparer5 = LanguagePrimitives.get_GenericComparer();
          double m3_1 = this.M3;
          double m3_2 = obj.M3;
          int num5 = m3_1 >= m3_2 ? (m3_1 <= m3_2 ? (m3_1 != m3_2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(genericComparer5, (M0) m3_1, (M0) m3_2) : 0) : 1) : -1;
          if (num5 < 0 || num5 > 0)
            return num5;
          IComparer genericComparer6 = LanguagePrimitives.get_GenericComparer();
          double m4_1 = this.M4;
          double m4_2 = obj.M4;
          if (m4_1 < m4_2)
            return -1;
          if (m4_1 > m4_2)
            return 1;
          if (m4_1 == m4_2)
            return 0;
          return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(genericComparer6, (M0) m4_1, (M0) m4_2);
        }
        return obj != null ? -1 : 0;
      }

      [CompilerGenerated]
      public virtual int CompareTo(object obj)
      {
        return this.CompareTo((StatsInternal.Moments) obj);
      }

      [CompilerGenerated]
      public virtual int CompareTo(object obj, IComparer comp)
      {
        StatsInternal.Moments moments = (StatsInternal.Moments) obj;
        if (this != null)
        {
          if ((StatsInternal.Moments) obj == null)
            return 1;
          IComparer comparer1 = comp;
          double nobs1 = this.nobs;
          double nobs2 = moments.nobs;
          int num1 = nobs1 >= nobs2 ? (nobs1 <= nobs2 ? (nobs1 != nobs2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(comparer1, (M0) nobs1, (M0) nobs2) : 0) : 1) : -1;
          if (num1 < 0 || num1 > 0)
            return num1;
          IComparer comparer2 = comp;
          double sum1 = this.sum;
          double sum2 = moments.sum;
          int num2 = sum1 >= sum2 ? (sum1 <= sum2 ? (sum1 != sum2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(comparer2, (M0) sum1, (M0) sum2) : 0) : 1) : -1;
          if (num2 < 0 || num2 > 0)
            return num2;
          IComparer comparer3 = comp;
          double m1_1 = this.M1;
          double m1_2 = moments.M1;
          int num3 = m1_1 >= m1_2 ? (m1_1 <= m1_2 ? (m1_1 != m1_2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(comparer3, (M0) m1_1, (M0) m1_2) : 0) : 1) : -1;
          if (num3 < 0 || num3 > 0)
            return num3;
          IComparer comparer4 = comp;
          double m2_1 = this.M2;
          double m2_2 = moments.M2;
          int num4 = m2_1 >= m2_2 ? (m2_1 <= m2_2 ? (m2_1 != m2_2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(comparer4, (M0) m2_1, (M0) m2_2) : 0) : 1) : -1;
          if (num4 < 0 || num4 > 0)
            return num4;
          IComparer comparer5 = comp;
          double m3_1 = this.M3;
          double m3_2 = moments.M3;
          int num5 = m3_1 >= m3_2 ? (m3_1 <= m3_2 ? (m3_1 != m3_2 ? LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(comparer5, (M0) m3_1, (M0) m3_2) : 0) : 1) : -1;
          if (num5 < 0 || num5 > 0)
            return num5;
          IComparer comparer6 = comp;
          double m4_1 = this.M4;
          double m4_2 = moments.M4;
          if (m4_1 < m4_2)
            return -1;
          if (m4_1 > m4_2)
            return 1;
          if (m4_1 == m4_2)
            return 0;
          return LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<double>(comparer6, (M0) m4_1, (M0) m4_2);
        }
        return (StatsInternal.Moments) obj != null ? -1 : 0;
      }

      [CompilerGenerated]
      public virtual int GetHashCode(IEqualityComparer comp)
      {
        if (this == null)
          return 0;
        int num1 = 0;
        int num2 = LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<double>(comp, (M0) this.M4) + ((num1 << 6) + (num1 >> 2)) - 1640531527;
        int num3 = LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<double>(comp, (M0) this.M3) + ((num2 << 6) + (num2 >> 2)) - 1640531527;
        int num4 = LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<double>(comp, (M0) this.M2) + ((num3 << 6) + (num3 >> 2)) - 1640531527;
        int num5 = LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<double>(comp, (M0) this.M1) + ((num4 << 6) + (num4 >> 2)) - 1640531527;
        int num6 = LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<double>(comp, (M0) this.sum) + ((num5 << 6) + (num5 >> 2)) - 1640531527;
        return LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<double>(comp, (M0) this.nobs) + ((num6 << 6) + (num6 >> 2)) - 1640531527;
      }

      [CompilerGenerated]
      public override sealed int GetHashCode()
      {
        return this.GetHashCode(LanguagePrimitives.get_GenericEqualityComparer());
      }

      [CompilerGenerated]
      public virtual bool Equals(object obj, IEqualityComparer comp)
      {
        if (this == null)
          return obj == null;
        StatsInternal.Moments moments1 = obj as StatsInternal.Moments;
        if (moments1 == null)
          return false;
        StatsInternal.Moments moments2 = moments1;
        if (this.nobs != moments2.nobs)
          return false;
        if (this.sum != moments2.sum)
          return false;
        if (this.M1 != moments2.M1)
          return false;
        if (this.M2 != moments2.M2)
          return false;
        if (this.M3 != moments2.M3)
          return false;
        return this.M4 == moments2.M4;
      }

      [CompilerGenerated]
      public virtual bool Equals(StatsInternal.Moments obj)
      {
        if (this == null)
          return obj == null;
        if (obj == null)
          return false;
        double nobs1 = this.nobs;
        double nobs2 = obj.nobs;
        if (((nobs1 == nobs1 ? 0 : (nobs2 != nobs2 ? 1 : 0)) == 0 ? (nobs1 == nobs2 ? 1 : 0) : 1) == 0)
          return false;
        double sum1 = this.sum;
        double sum2 = obj.sum;
        if (((sum1 == sum1 ? 0 : (sum2 != sum2 ? 1 : 0)) == 0 ? (sum1 == sum2 ? 1 : 0) : 1) == 0)
          return false;
        double m1_1 = this.M1;
        double m1_2 = obj.M1;
        if (((m1_1 == m1_1 ? 0 : (m1_2 != m1_2 ? 1 : 0)) == 0 ? (m1_1 == m1_2 ? 1 : 0) : 1) == 0)
          return false;
        double m2_1 = this.M2;
        double m2_2 = obj.M2;
        if (((m2_1 == m2_1 ? 0 : (m2_2 != m2_2 ? 1 : 0)) == 0 ? (m2_1 == m2_2 ? 1 : 0) : 1) == 0)
          return false;
        double m3_1 = this.M3;
        double m3_2 = obj.M3;
        if (((m3_1 == m3_1 ? 0 : (m3_2 != m3_2 ? 1 : 0)) == 0 ? (m3_1 == m3_2 ? 1 : 0) : 1) == 0)
          return false;
        double m4_1 = this.M4;
        double m4_2 = obj.M4;
        if ((m4_1 == m4_1 ? 0 : (m4_2 != m4_2 ? 1 : 0)) != 0)
          return true;
        return m4_1 == m4_2;
      }

      [CompilerGenerated]
      public override sealed bool Equals(object obj)
      {
        StatsInternal.Moments moments = obj as StatsInternal.Moments;
        if (moments != null)
          return this.Equals(moments);
        return false;
      }
    }

    [Serializable]
    internal sealed class calcSparse : OptimizedClosures.FSharpFunc<StatsInternal.Moments, OptionalValue<double>, StatsInternal.Moments>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal calcSparse()
      {
        base.ctor();
      }

      public virtual StatsInternal.Moments Invoke(StatsInternal.Moments state, OptionalValue<double> curr)
      {
        return StatsInternal.updateMomentsSparse(state, curr);
      }
    }

    [Serializable]
    internal sealed class calcSparse : FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public StatsInternal.Moments initState;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<StatsInternal.Moments, FSharpFunc<OptionalValue<double>, StatsInternal.Moments>> fupdate;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<StatsInternal.Moments, double> ftransf;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal calcSparse(StatsInternal.Moments initState, FSharpFunc<StatsInternal.Moments, FSharpFunc<OptionalValue<double>, StatsInternal.Moments>> fupdate, FSharpFunc<StatsInternal.Moments, double> ftransf)
      {
        this.ctor();
        this.initState = initState;
        this.fupdate = fupdate;
        this.ftransf = ftransf;
      }

      public virtual IEnumerable<double> Invoke(IEnumerable<OptionalValue<double>> source)
      {
        return StatsInternal.expandingWindowFn<StatsInternal.Moments, OptionalValue<double>, double>(this.initState, this.fupdate, this.ftransf, source);
      }
    }

    [Serializable]
    internal sealed class calcSparse : FSharpFunc<IEnumerable<double>, double[]>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal calcSparse()
      {
        base.ctor();
      }

      public virtual double[] Invoke(IEnumerable<double> source)
      {
        return (double[]) ArrayModule.OfSeq<double>((IEnumerable<M0>) source);
      }
    }

    [Serializable]
    internal sealed class calcSparse0 : FSharpFunc<IEnumerable<OptionalValue<double>>, double[]>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<IEnumerable<double>, double[]> g;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal calcSparse0(FSharpFunc<IEnumerable<OptionalValue<double>>, IEnumerable<double>> f, FSharpFunc<IEnumerable<double>, double[]> g)
      {
        this.ctor();
        this.f = f;
        this.g = g;
      }

      public virtual double[] Invoke(IEnumerable<OptionalValue<double>> x)
      {
        return this.g.Invoke(this.f.Invoke(x));
      }
    }

    [Serializable]
    internal sealed class applyExpandingMomentsTransform<V> : FSharpFunc<V, double>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal applyExpandingMomentsTransform()
      {
        base.ctor();
      }

      public virtual double Invoke(V x)
      {
        throw new NotSupportedException("Dynamic invocation of op_Explicit is not supported");
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class expandingMinMaxHelper : GeneratedSequenceBase<double>
    {
      public FSharpFunc<double, FSharpFunc<double, bool>> cmp;
      public IEnumerable<FSharpOption<double>> s;
      public FSharpRef<double> m;
      public FSharpOption<double> v;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<FSharpOption<double>> @enum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public double current;

      public expandingMinMaxHelper(FSharpFunc<double, FSharpFunc<double, bool>> cmp, IEnumerable<FSharpOption<double>> s, FSharpRef<double> m, FSharpOption<double> v, IEnumerator<FSharpOption<double>> @enum, int pc, double current)
      {
        this.cmp = cmp;
        this.s = s;
        this.m = m;
        this.v = v;
        this.@enum = @enum;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<double> next)
      {
        switch (this.pc)
        {
          case 1:
label_10:
            this.pc = 3;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<FSharpOption<double>>>((M0) this.@enum);
            this.@enum = (IEnumerator<FSharpOption<double>>) null;
            this.m = (FSharpRef<double>) null;
            this.pc = 3;
            goto case 3;
          case 2:
            this.v = (FSharpOption<double>) null;
            break;
          case 3:
            this.current = 0.0;
            return 0;
          default:
            this.m = (FSharpRef<double>) Operators.Ref<double>((M0) Operators.get_NaN());
            this.@enum = this.s.GetEnumerator();
            this.pc = 1;
            break;
        }
        if (((IEnumerator) this.@enum).MoveNext())
        {
          this.v = this.@enum.Current;
          this.pc = 2;
          FSharpOption<double> v = this.v;
          double num1;
          if (v != null)
          {
            double num2 = v.get_Value();
            double d = (double) Operators.op_Dereference<double>((FSharpRef<M0>) this.m);
            if ((!double.IsNaN(d) ? FSharpFunc<double, double>.InvokeFast<bool>((FSharpFunc<double, FSharpFunc<double, M0>>) this.cmp, d, num2) : (M0) 1) != null)
            {
              Operators.op_ColonEquals<double>((FSharpRef<M0>) this.m, (M0) num2);
              num1 = num2;
            }
            else
              num1 = d;
          }
          else
            num1 = (double) Operators.op_Dereference<double>((FSharpRef<M0>) this.m);
          this.current = num1;
          return 1;
        }
        goto label_10;
      }

      public virtual void Close()
      {
        Exception exception = (Exception) null;
        while (true)
        {
          switch (this.pc)
          {
            case 3:
              goto label_8;
            default:
              Unit unit;
              try
              {
                switch (this.pc)
                {
                  case 0:
                  case 3:
                    this.pc = 3;
                    this.current = 0.0;
                    unit = (Unit) null;
                    break;
                  case 1:
                    this.pc = 3;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<FSharpOption<double>>>((M0) this.@enum);
                    goto case 0;
                  default:
                    goto case 1;
                }
              }
              catch (object ex)
              {
                exception = (Exception) ex;
                unit = (Unit) null;
              }
              continue;
          }
        }
label_8:
        if (exception != null)
          throw exception;
      }

      public virtual bool get_CheckClose()
      {
        switch (this.pc)
        {
          case 0:
          case 3:
            return false;
          case 1:
            return true;
          default:
            return true;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual double get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<double> GetFreshEnumerator()
      {
        return (IEnumerator<double>) new StatsInternal.expandingMinMaxHelper(this.cmp, this.s, (FSharpRef<double>) null, (FSharpOption<double>) null, (IEnumerator<FSharpOption<double>>) null, 0, 0.0);
      }
    }

    [Serializable]
    internal sealed class swap : OptimizedClosures.FSharpFunc<int, int, Unit>
    {
      public double[] arr;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal swap(double[] arr)
      {
        this.ctor();
        this.arr = arr;
      }

      public virtual Unit Invoke(int a, int b)
      {
        double num = this.arr[b];
        this.arr[b] = this.arr[a];
        this.arr[a] = num;
        return (Unit) null;
      }
    }

    [Serializable]
    internal sealed class partition : OptimizedClosures.FSharpFunc<int, int, int, int>
    {
      public double[] arr;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal partition(double[] arr)
      {
        this.ctor();
        this.arr = arr;
      }

      public virtual int Invoke(int left, int right, int pivotIndex)
      {
        double num1 = this.arr[pivotIndex];
        int index1 = pivotIndex;
        int index2 = right;
        double num2 = this.arr[index2];
        this.arr[index2] = this.arr[index1];
        this.arr[index1] = num2;
        int num3 = left;
        int index3 = left;
        int num4 = right - 1;
        if (num4 >= index3)
        {
          do
          {
            if (this.arr[index3] < num1)
            {
              int index4 = num3;
              int index5 = index3;
              double num5 = this.arr[index5];
              this.arr[index5] = this.arr[index4];
              this.arr[index4] = num5;
              ++num3;
            }
            ++index3;
          }
          while (index3 != num4 + 1);
        }
        int index6 = right;
        int index7 = num3;
        double num6 = this.arr[index7];
        this.arr[index7] = this.arr[index6];
        this.arr[index6] = num6;
        return num3;
      }
    }

    [Serializable]
    internal sealed class select : OptimizedClosures.FSharpFunc<int, int, double>
    {
      public int n;
      public double[] arr;
      public FSharpFunc<int, FSharpFunc<int, FSharpFunc<int, int>>> partition;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal select(int n, double[] arr, FSharpFunc<int, FSharpFunc<int, FSharpFunc<int, int>>> partition)
      {
        this.ctor();
        this.n = n;
        this.arr = arr;
        this.partition = partition;
      }

      public virtual double Invoke(int left, int right)
      {
        while (left != right)
        {
          int num1 = (left + right) / 2;
          int num2 = (int) FSharpFunc<int, int>.InvokeFast<int, int>((FSharpFunc<int, FSharpFunc<int, FSharpFunc<M0, M1>>>) this.partition, left, right, (M0) num1);
          if (this.n == num2)
            return this.arr[this.n];
          if (this.n < num2)
          {
            int num3 = left;
            right = num2 - 1;
            left = num3;
          }
          else
          {
            int num3 = num2 + 1;
            right = right;
            left = num3;
          }
        }
        return this.arr[left];
      }
    }
  }
}
