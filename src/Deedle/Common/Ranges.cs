// Decompiled with JetBrains decompiler
// Type: Deedle.Ranges.Ranges
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

namespace Deedle.Ranges
{
  
  public static class Ranges
  {
    
    public static Deedle.Ranges.Ranges<T> inlineCreate<T>(FSharpFunc<T, FSharpFunc<long, T>> succ, IEnumerable<Tuple<T, T>> ranges)
    {
      Tuple<T, T>[] tupleArray = (Tuple<T, T>[]) ArrayModule.OfSeq<Tuple<T, T>>((IEnumerable<M0>) ranges);
      foreach (Tuple<T, T> tuple in tupleArray)
      {
        if (LanguagePrimitives.HashCompare.GenericGreaterThanIntrinsic<T>(tuple.Item1, tuple.Item2))
          throw new ArgumentException("Invalid range (first offset is greater than second)", nameof (ranges));
      }
      T one = LanguagePrimitives.GenericOneDynamic<T>();
      IRangeKeyOperations<T> ops = (IRangeKeyOperations<T>) new Deedle.Ranges.Ranges.ops<T>(succ, one);
      return new Deedle.Ranges.Ranges<T>((IEnumerable<Tuple<T, T>>) tupleArray, ops);
    }

    
    public static Deedle.Ranges.Ranges<T> create<T>(IRangeKeyOperations<T> ops, IEnumerable<Tuple<T, T>> ranges)
    {
      return new Deedle.Ranges.Ranges<T>(ranges, ops);
    }

    public static Deedle.Ranges.Ranges<T> combine<T>(IEnumerable<Deedle.Ranges.Ranges<T>> ranges)
    {
      if (SeqModule.IsEmpty<Deedle.Ranges.Ranges<T>>((IEnumerable<M0>) ranges))
        throw new ArgumentException("Range cannot be empty", nameof (ranges));
      IRangeKeyOperations<T> operations = ((Deedle.Ranges.Ranges<T>) SeqModule.Head<Deedle.Ranges.Ranges<T>>((IEnumerable<M0>) ranges)).Operations;
      FSharpList<Tuple<T, T>> fsharpList = (FSharpList<Tuple<T, T>>) ListModule.SortWith<Tuple<T, T>>((FSharpFunc<M0, FSharpFunc<M0, int>>) new Deedle.Ranges.Ranges.blocks<T>(operations), SeqModule.ToList<Tuple<T, T>>((IEnumerable<M0>) new Deedle.Ranges.Ranges.blocks<T>(ranges, (Deedle.Ranges.Ranges<T>) null, (IEnumerator<Deedle.Ranges.Ranges<T>>) null, (Tuple<T, T>) null, (IEnumerator<Tuple<T, T>>) null, 0, (Tuple<T, T>) null)));
      FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, IEnumerable<Tuple<T, T>>>> fsharpFunc = (FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, IEnumerable<Tuple<T, T>>>>) new Deedle.Ranges.Ranges.loop<T>(operations);
      IRangeKeyOperations<T> ops = operations;
      return new Deedle.Ranges.Ranges<T>((IEnumerable<Tuple<T, T>>) FSharpFunc<Tuple<T, T>, FSharpList<Tuple<T, T>>>.InvokeFast<IEnumerable<Tuple<T, T>>>((FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, M0>>) fsharpFunc, (Tuple<T, T>) ListModule.Head<Tuple<T, T>>((FSharpList<M0>) fsharpList), (FSharpList<Tuple<T, T>>) ListModule.Tail<Tuple<T, T>>((FSharpList<M0>) fsharpList)), ops);
    }

    public static long invalid
    {
      [DebuggerNonUserCode] get
      {
        return -1;
      }
    }

    
    public static T keyAtOffset<T>(long offs, Deedle.Ranges.Ranges<T> ranges)
    {
      FSharpFunc<long, FSharpFunc<int, T>> fsharpFunc = (FSharpFunc<long, FSharpFunc<int, T>>) new Deedle.Ranges.Ranges.loop<T>(offs, ranges);
      if (offs < 0L)
        throw new IndexOutOfRangeException();
      return FSharpFunc<long, int>.InvokeFast<T>(fsharpFunc, 0L, 0);
    }

    
    public static long offsetOfKey<T>(T key, Deedle.Ranges.Ranges<T> ranges)
    {
      FSharpFunc<T, FSharpFunc<T, bool>> fsharpFunc1 = (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.op_LessDot<T>(ranges);
      FSharpFunc<T, FSharpFunc<T, bool>> fsharpFunc2 = (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.op_GreaterEqualsDot<T>(ranges);
      FSharpFunc<T, FSharpFunc<T, bool>> fsharpFunc3 = (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.op_LessEqualsDot<T>(ranges);
      FSharpFunc<long, FSharpFunc<int, long>> fsharpFunc4 = (FSharpFunc<long, FSharpFunc<int, long>>) new Deedle.Ranges.Ranges.loop<T>(key, ranges);
      if (ranges.Operations.ValidateKey(key, Lookup.Exact).HasValue)
        return (long) FSharpFunc<long, int>.InvokeFast<long>((FSharpFunc<long, FSharpFunc<int, M0>>) fsharpFunc4, 0L, 0);
      return Deedle.Ranges.Ranges.invalid;
    }

    
    public static Deedle.Ranges.Ranges<T> restrictRanges<T>(RangeRestriction<T> restriction, Deedle.Ranges.Ranges<T> ranges)
    {
      FSharpFunc<T, FSharpFunc<T, bool>> fsharpFunc1 = (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.op_LessDot<T>(ranges);
      FSharpFunc<T, FSharpFunc<T, bool>> fsharpFunc2 = (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.op_GreaterDot<T>(ranges);
      FSharpFunc<T, FSharpFunc<T, bool>> fsharpFunc3 = (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.op_GreaterEqualsDot<T>(ranges);
      FSharpFunc<T, FSharpFunc<T, bool>> fsharpFunc4 = (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.op_LessEqualsDot<T>(ranges);
      FSharpFunc<T, FSharpFunc<T, T>> fsharpFunc5 = (FSharpFunc<T, FSharpFunc<T, T>>) new Deedle.Ranges.Ranges.max<T>(ranges);
      FSharpFunc<T, FSharpFunc<T, T>> fsharpFunc6 = (FSharpFunc<T, FSharpFunc<T, T>>) new Deedle.Ranges.Ranges.min<T>(ranges);
      RangeRestriction<T> input = restriction;
      if (input.get_Tag() == 0)
      {
        RangeRestriction<T>.Fixed @fixed = (RangeRestriction<T>.Fixed) input;
        T loRestr = @fixed.item1;
        T hiRestr = @fixed.item2;
        return new Deedle.Ranges.Ranges<T>((IEnumerable<Tuple<T, T>>) SeqModule.ToArray<Tuple<T, T>>((IEnumerable<M0>) new Deedle.Ranges.Ranges.newRanges<T>(ranges, loRestr, hiRestr, default (T), default (T), default (T), default (T), (Tuple<T, T>) null, (Tuple<T, T>) null, (IEnumerator<Tuple<T, T>>) null, 0, (Tuple<T, T>) null)), ranges.Operations);
      }
      Tuple<Tuple<bool, int>, RangeRestriction<T>> tuple1 = MatchingHelpers.Let<Tuple<bool, int>, RangeRestriction<T>>(new Tuple<bool, int>(true, 1), input);
      long num1;
      bool isStart;
      int step;
      if (tuple1.Item2.get_Tag() == 1)
      {
        RangeRestriction<T>.Start start = (RangeRestriction<T>.Start) tuple1.Item2;
        step = tuple1.Item1.Item2;
        isStart = tuple1.Item1.Item1;
        num1 = start.item;
      }
      else
      {
        Tuple<Tuple<bool, int>, RangeRestriction<T>> tuple2 = MatchingHelpers.Let<Tuple<bool, int>, RangeRestriction<T>>(new Tuple<bool, int>(false, -1), input);
        if (tuple2.Item2.get_Tag() != 2)
          throw Operators.Failure("restrictRanges: Custom ranges are not supported");
        RangeRestriction<T>.End end = (RangeRestriction<T>.End) tuple2.Item2;
        int num2 = tuple2.Item1.Item2;
        int num3 = tuple2.Item1.Item1 ? 1 : 0;
        num1 = end.item;
        isStart = num3 != 0;
        step = num2;
      }
      FSharpFunc<int, FSharpFunc<int, IEnumerable<Tuple<T, T>>>> fsharpFunc7 = (FSharpFunc<int, FSharpFunc<int, IEnumerable<Tuple<T, T>>>>) new Deedle.Ranges.Ranges.loop<T>(ranges, step, isStart);
      if (isStart)
        return new Deedle.Ranges.Ranges<T>((IEnumerable<Tuple<T, T>>) ArrayModule.OfSeq<Tuple<T, T>>((IEnumerable<M0>) FSharpFunc<int, int>.InvokeFast<IEnumerable<Tuple<T, T>>>((FSharpFunc<int, FSharpFunc<int, M0>>) fsharpFunc7, 0, (int) num1)), ranges.Operations);
      return new Deedle.Ranges.Ranges<T>((IEnumerable<Tuple<T, T>>) ArrayModule.Reverse<Tuple<T, T>>(ArrayModule.OfSeq<Tuple<T, T>>((IEnumerable<M0>) FSharpFunc<int, int>.InvokeFast<IEnumerable<Tuple<T, T>>>((FSharpFunc<int, FSharpFunc<int, M0>>) fsharpFunc7, ranges.Ranges.Length - 1, (int) num1))), ranges.Operations);
    }

    public static Tuple<T, T> keyRange<T>(Deedle.Ranges.Ranges<T> ranges)
    {
      return new Tuple<T, T>(ranges.Ranges[0].Item1, ranges.Ranges[ranges.Ranges.Length - 1].Item2);
    }

    public static IEnumerable<T> keys<T>(Deedle.Ranges.Ranges<T> ranges)
    {
      return (IEnumerable<T>) new Deedle.Ranges.Ranges.keys<T>(ranges, default (T), default (T), (Tuple<T, T>) null, (IEnumerator<Tuple<T, T>>) null, default (T), (IEnumerator<T>) null, 0, default (T));
    }

    public static long length<T>(Deedle.Ranges.Ranges<T> ranges)
    {
      Tuple<T, T>[] ranges1 = ranges.Ranges;
      FSharpFunc<Tuple<T, T>, long> fsharpFunc = (FSharpFunc<Tuple<T, T>, long>) new Deedle.Ranges.Ranges.length<T>(ranges);
      Tuple<T, T>[] tupleArray = ranges1;
      if ((object) tupleArray == null)
        throw new ArgumentNullException("array");
      long num = 0;
      for (int index = 0; index < tupleArray.Length; ++index)
        checked { num += fsharpFunc.Invoke(tupleArray[index]); }
      return num;
    }

    
    public static OptionalValue<Tuple<T, long>> lookup<T>(T key, Lookup semantics, FSharpFunc<T, FSharpFunc<long, bool>> check, Deedle.Ranges.Ranges<T> ranges)
    {
      FSharpFunc<T, FSharpFunc<T, bool>> fsharpFunc1 = (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.op_LessDot<T>(ranges);
      FSharpFunc<T, FSharpFunc<T, bool>> fsharpFunc2 = (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.op_LessDot<T>(ranges);
      FSharpFunc<T, FSharpFunc<T, bool>> fsharpFunc3 = (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.op_GreaterEqualsDot<T>(ranges);
      FSharpFunc<T, FSharpFunc<T, bool>> fsharpFunc4 = (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.op_LessEqualsDot<T>(ranges);
      if (semantics == Lookup.Exact)
      {
        long num = Deedle.Ranges.Ranges.offsetOfKey<T>(key, ranges);
        if ((num == Deedle.Ranges.Ranges.invalid ? 0 : (int) FSharpFunc<T, long>.InvokeFast<bool>((FSharpFunc<T, FSharpFunc<long, M0>>) check, key, num)) != 0)
          return new OptionalValue<Tuple<T, long>>(new Tuple<T, long>(key, num));
        return OptionalValue<Tuple<T, long>>.Missing;
      }
      OptionalValue<T> optionalValue = ranges.Operations.ValidateKey(key, semantics);
      if (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<OptionalValue<T>>((M0) optionalValue, (M0) OptionalValue<T>.Missing))
        return OptionalValue<Tuple<T, long>>.Missing;
      T validKey = optionalValue.Value;
      FSharpFunc<long, long> fsharpFunc5;
      if ((semantics & Lookup.Greater) == Lookup.Greater)
      {
        fsharpFunc5 = (FSharpFunc<long, long>) new Deedle.Ranges.Ranges.step(1L);
      }
      else
      {
        if ((semantics & Lookup.Smaller) != Lookup.Smaller)
          throw new ArgumentException("Invalid lookup semantics (1)", nameof (semantics));
        fsharpFunc5 = (FSharpFunc<long, long>) new Deedle.Ranges.Ranges.step(-1L);
      }
      FSharpFunc<long, long> f = fsharpFunc5;
      Tuple<long, int> tuple = (Tuple<long, int>) FSharpFunc<long, int>.InvokeFast<Tuple<long, int>>((FSharpFunc<long, FSharpFunc<int, M0>>) new Deedle.Ranges.Ranges.loop<T>(semantics, ranges, validKey), 0L, 0);
      long num1 = tuple.Item1;
      int rangeIdx = tuple.Item2;
      if ((num1 != Deedle.Ranges.Ranges.invalid ? (rangeIdx == -1 ? 1 : 0) : 1) != 0)
        return OptionalValue<Tuple<T, long>>.Missing;
      T keyStart = Deedle.Ranges.Ranges.keyAtOffset<T>(num1, ranges);
      GeneratedSequenceBase<T> generatedSequenceBase;
      if (semantics == Lookup.Exact)
        generatedSequenceBase = (GeneratedSequenceBase<T>) new Deedle.Ranges.Ranges.keysToScan<T>(keyStart, 0, default (T));
      else if ((semantics & Lookup.Greater) == Lookup.Greater)
      {
        generatedSequenceBase = (GeneratedSequenceBase<T>) new Deedle.Ranges.Ranges.keysToScan<T>(ranges, rangeIdx, keyStart, default (T), default (T), (Tuple<T, T>) null, default (T), (IEnumerator<T>) null, (IEnumerator<Tuple<T, T>>) null, default (T), (IEnumerator<T>) null, 0, default (T));
      }
      else
      {
        if ((semantics & Lookup.Smaller) != Lookup.Smaller)
          throw new ArgumentException("Invalid lookup semantics", nameof (semantics));
        generatedSequenceBase = (GeneratedSequenceBase<T>) new Deedle.Ranges.Ranges.keysToScan<T>(ranges, rangeIdx, keyStart, default (T), default (T), (Tuple<T, T>) null, default (T), (IEnumerator<T>) null, (IEnumerator<Tuple<T, T>>) null, default (T), (IEnumerator<T>) null, 0, default (T));
      }
      IEnumerable<T> objs = (IEnumerable<T>) generatedSequenceBase;
      Deedle.Ranges.Ranges.lookup<T> lookup287 = new Deedle.Ranges.Ranges.lookup<T>(check);
      IEnumerable<Tuple<T, long>> tuples1 = (IEnumerable<Tuple<T, long>>) SeqModule.Zip<T, long>(objs, (IEnumerable<M1>) new Deedle.Ranges.Ranges.lookup(f, num1, (FSharpRef<long>) null, 0, 0L));
      IEnumerable<Tuple<T, long>> tuples2 = ((!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<T>(keyStart, key) ? 0 : (semantics != Lookup.Greater ? (semantics == Lookup.Smaller ? 1 : 0) : 1)) == 0 ? (FSharpFunc<IEnumerable<Tuple<T, long>>, IEnumerable<Tuple<T, long>>>) new Deedle.Ranges.Ranges.lookup<T>() : (FSharpFunc<IEnumerable<Tuple<T, long>>, IEnumerable<Tuple<T, long>>>) new Deedle.Ranges.Ranges.lookup<T>(1)).Invoke(tuples1);
      FSharpOption<Tuple<T, long>> fsharpOption = (FSharpOption<Tuple<T, long>>) SeqModule.TryFind<Tuple<T, long>>((FSharpFunc<M0, bool>) lookup287, (IEnumerable<M0>) tuples2);
      if (fsharpOption == null)
        return OptionalValue<Tuple<T, long>>.Missing;
      return new OptionalValue<Tuple<T, long>>(fsharpOption.get_Value());
    }

    [Serializable]
    internal sealed class ops<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ops()
      {
        base.ctor();
      }

      public virtual bool Invoke(T x, T y)
      {
        return LanguagePrimitives.HashCompare.GenericGreaterOrEqualIntrinsic<T>((M0) x, (M0) y);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ops<T> : IEnumerator, IDisposable, IEnumerator<T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, bool>> geq;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpRef<T> current;

      public ops(T lo, T hi, T step, FSharpFunc<T, FSharpFunc<T, bool>> geq, FSharpRef<T> current)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        this.current = current;
        // ISSUE: explicit constructor call
        base.ctor();
        Deedle.Ranges.Ranges.ops<T> ops933 = this;
      }

      T IEnumerator<T>.get_Current()
      {
        return this.current.get_Value();
      }

      object IEnumerator.get_Current()
      {
        return (object) this.current.get_Value();
      }

      bool IEnumerator.MoveNext()
      {
        if (FSharpFunc<T, T>.InvokeFast<bool>((FSharpFunc<T, FSharpFunc<T, M0>>) this.geq, this.current.get_Value(), this.hi) != null)
          return false;
        this.current.set_Value((T) LanguagePrimitives.AdditionDynamic<T, T, T>((M0) this.current.get_Value(), (M1) this.step));
        return true;
      }

      void IEnumerator.Reset()
      {
        FSharpRef<T> current = this.current;
        T lo = this.lo;
        T step = this.step;
        if (true)
          throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
        T obj = (T) null;
        current.set_Value(obj);
      }

      void IDisposable.Dispose()
      {
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ops<T> : IEnumerable, IEnumerable<T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, bool>> geq;

      public ops(T lo, T hi, T step, FSharpFunc<T, FSharpFunc<T, bool>> geq)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        // ISSUE: explicit constructor call
        base.ctor();
        Deedle.Ranges.Ranges.ops<T> ops932 = this;
      }

      IEnumerator<T> IEnumerable<T>.GetEnumerator()
      {
        T lo = this.lo;
        T step = this.step;
        if (!true)
          return (IEnumerator<T>) new Deedle.Ranges.Ranges.ops<T>(this.lo, this.hi, this.step, this.geq, (FSharpRef<T>) Operators.Ref<T>((M0) null));
        throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) ((IEnumerable<T>) this).GetEnumerator();
      }
    }

    [Serializable]
    internal sealed class ops<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ops()
      {
        base.ctor();
      }

      public virtual bool Invoke(T x, T y)
      {
        return LanguagePrimitives.HashCompare.GenericLessOrEqualIntrinsic<T>((M0) x, (M0) y);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ops<T> : IEnumerator, IDisposable, IEnumerator<T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, bool>> geq;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpRef<T> current;

      public ops(T lo, T hi, T step, FSharpFunc<T, FSharpFunc<T, bool>> geq, FSharpRef<T> current)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        this.current = current;
        // ISSUE: explicit constructor call
        base.ctor();
        Deedle.Ranges.Ranges.ops<T> ops936 = this;
      }

      T IEnumerator<T>.get_Current()
      {
        return this.current.get_Value();
      }

      object IEnumerator.get_Current()
      {
        return (object) this.current.get_Value();
      }

      bool IEnumerator.MoveNext()
      {
        if (FSharpFunc<T, T>.InvokeFast<bool>((FSharpFunc<T, FSharpFunc<T, M0>>) this.geq, this.current.get_Value(), this.hi) != null)
          return false;
        this.current.set_Value((T) LanguagePrimitives.AdditionDynamic<T, T, T>((M0) this.current.get_Value(), (M1) this.step));
        return true;
      }

      void IEnumerator.Reset()
      {
        FSharpRef<T> current = this.current;
        T lo = this.lo;
        T step = this.step;
        if (true)
          throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
        T obj = (T) null;
        current.set_Value(obj);
      }

      void IDisposable.Dispose()
      {
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ops<T> : IEnumerable, IEnumerable<T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, bool>> geq;

      public ops(T lo, T hi, T step, FSharpFunc<T, FSharpFunc<T, bool>> geq)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        // ISSUE: explicit constructor call
        base.ctor();
        Deedle.Ranges.Ranges.ops<T> ops935 = this;
      }

      IEnumerator<T> IEnumerable<T>.GetEnumerator()
      {
        T lo = this.lo;
        T step = this.step;
        if (!true)
          return (IEnumerator<T>) new Deedle.Ranges.Ranges.ops<T>(this.lo, this.hi, this.step, this.geq, (FSharpRef<T>) Operators.Ref<T>((M0) null));
        throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) ((IEnumerable<T>) this).GetEnumerator();
      }
    }

    [Serializable]
    internal sealed class ops<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ops()
      {
        base.ctor();
      }

      public virtual bool Invoke(T x, T y)
      {
        return LanguagePrimitives.HashCompare.GenericGreaterOrEqualIntrinsic<T>((M0) x, (M0) y);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ops<T> : IEnumerator, IDisposable, IEnumerator<T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, bool>> geq;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpRef<T> current;

      public ops(T lo, T hi, T step, FSharpFunc<T, FSharpFunc<T, bool>> geq, FSharpRef<T> current)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        this.current = current;
        // ISSUE: explicit constructor call
        base.ctor();
        Deedle.Ranges.Ranges.ops<T> ops939 = this;
      }

      T IEnumerator<T>.get_Current()
      {
        return this.current.get_Value();
      }

      object IEnumerator.get_Current()
      {
        return (object) this.current.get_Value();
      }

      bool IEnumerator.MoveNext()
      {
        if (FSharpFunc<T, T>.InvokeFast<bool>((FSharpFunc<T, FSharpFunc<T, M0>>) this.geq, this.current.get_Value(), this.hi) != null)
          return false;
        this.current.set_Value((T) LanguagePrimitives.AdditionDynamic<T, T, T>((M0) this.current.get_Value(), (M1) this.step));
        return true;
      }

      void IEnumerator.Reset()
      {
        FSharpRef<T> current = this.current;
        T lo = this.lo;
        T step = this.step;
        if (true)
          throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
        T obj = (T) null;
        current.set_Value(obj);
      }

      void IDisposable.Dispose()
      {
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ops<T> : IEnumerable, IEnumerable<T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, bool>> geq;

      public ops(T lo, T hi, T step, FSharpFunc<T, FSharpFunc<T, bool>> geq)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        // ISSUE: explicit constructor call
        base.ctor();
        Deedle.Ranges.Ranges.ops<T> ops938 = this;
      }

      IEnumerator<T> IEnumerable<T>.GetEnumerator()
      {
        T lo = this.lo;
        T step = this.step;
        if (!true)
          return (IEnumerator<T>) new Deedle.Ranges.Ranges.ops<T>(this.lo, this.hi, this.step, this.geq, (FSharpRef<T>) Operators.Ref<T>((M0) null));
        throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) ((IEnumerable<T>) this).GetEnumerator();
      }
    }

    [Serializable]
    internal sealed class ops0<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ops0()
      {
        base.ctor();
      }

      public virtual bool Invoke(T x, T y)
      {
        return LanguagePrimitives.HashCompare.GenericLessOrEqualIntrinsic<T>((M0) x, (M0) y);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ops2<T> : IEnumerator, IDisposable, IEnumerator<T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, bool>> geq;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpRef<T> current;

      public ops2(T lo, T hi, T step, FSharpFunc<T, FSharpFunc<T, bool>> geq, FSharpRef<T> current)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        this.current = current;
        // ISSUE: explicit constructor call
        base.ctor();
        Deedle.Ranges.Ranges.ops2<T> ops9312 = this;
      }

      T IEnumerator<T>.get_Current()
      {
        return this.current.get_Value();
      }

      object IEnumerator.get_Current()
      {
        return (object) this.current.get_Value();
      }

      bool IEnumerator.MoveNext()
      {
        if (FSharpFunc<T, T>.InvokeFast<bool>((FSharpFunc<T, FSharpFunc<T, M0>>) this.geq, this.current.get_Value(), this.hi) != null)
          return false;
        this.current.set_Value((T) LanguagePrimitives.AdditionDynamic<T, T, T>((M0) this.current.get_Value(), (M1) this.step));
        return true;
      }

      void IEnumerator.Reset()
      {
        FSharpRef<T> current = this.current;
        T lo = this.lo;
        T step = this.step;
        if (true)
          throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
        T obj = (T) null;
        current.set_Value(obj);
      }

      void IDisposable.Dispose()
      {
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ops1<T> : IEnumerable, IEnumerable<T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, bool>> geq;

      public ops1(T lo, T hi, T step, FSharpFunc<T, FSharpFunc<T, bool>> geq)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        // ISSUE: explicit constructor call
        base.ctor();
        Deedle.Ranges.Ranges.ops1<T> ops9311 = this;
      }

      IEnumerator<T> IEnumerable<T>.GetEnumerator()
      {
        T lo = this.lo;
        T step = this.step;
        if (!true)
          return (IEnumerator<T>) new Deedle.Ranges.Ranges.ops2<T>(this.lo, this.hi, this.step, this.geq, (FSharpRef<T>) Operators.Ref<T>((M0) null));
        throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) ((IEnumerable<T>) this).GetEnumerator();
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ops<T> : IRangeKeyOperations<T>
    {
      public FSharpFunc<T, FSharpFunc<long, T>> succ;
      public T one;

      public ops(FSharpFunc<T, FSharpFunc<long, T>> succ, T one)
      {
        this.succ = succ;
        this.one = one;
        // ISSUE: explicit constructor call
        base.ctor();
        Deedle.Ranges.Ranges.ops<T> ops89 = this;
      }

      int IRangeKeyOperations<T>.Compare(T a, T b)
      {
        return LanguagePrimitives.HashCompare.GenericComparisonIntrinsic<T>((M0) a, (M0) b);
      }

      T IRangeKeyOperations<T>.IncrementBy(T a, long i)
      {
        return (T) FSharpFunc<T, long>.InvokeFast<T>((FSharpFunc<T, FSharpFunc<long, M0>>) this.succ, a, i);
      }

      long IRangeKeyOperations<T>.Distance(T l, T h)
      {
        if (!true)
        {
          T obj = (T) null;
          throw new NotSupportedException("Dynamic invocation of op_Explicit is not supported");
        }
        throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
      }

      IEnumerable<T> IRangeKeyOperations<T>.Range(T a, T b)
      {
        if (LanguagePrimitives.HashCompare.GenericLessOrEqualIntrinsic<T>((M0) a, (M0) b))
        {
          T lo = a;
          T hi = b;
          if (LanguagePrimitives.HashCompare.GenericLessOrEqualIntrinsic<T>((M0) lo, (M0) hi))
            return (IEnumerable<T>) new Deedle.Ranges.Ranges.ops<T>(lo, hi, (T) LanguagePrimitives.GenericOneDynamic<T>(), (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.ops<T>());
          return (IEnumerable<T>) new Deedle.Ranges.Ranges.ops<T>(lo, hi, (T) LanguagePrimitives.GenericOneDynamic<T>(), (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.ops<T>());
        }
        T lo1 = a;
        T one = this.one;
        if (true)
          throw new NotSupportedException("Dynamic invocation of op_UnaryNegation is not supported");
        T step = (T) null;
        T hi1 = b;
        if (LanguagePrimitives.HashCompare.GenericLessOrEqualIntrinsic<T>((M0) lo1, (M0) hi1))
          return (IEnumerable<T>) new Deedle.Ranges.Ranges.ops<T>(lo1, hi1, step, (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.ops<T>());
        return (IEnumerable<T>) new Deedle.Ranges.Ranges.ops1<T>(lo1, hi1, step, (FSharpFunc<T, FSharpFunc<T, bool>>) new Deedle.Ranges.Ranges.ops0<T>());
      }

      OptionalValue<T> IRangeKeyOperations<T>.ValidateKey(T k, Lookup _arg1)
      {
        return new OptionalValue<T>(k);
      }
    }

    [Serializable]
    internal sealed class blocks<T> : FSharpFunc<Tuple<T, T>, int>
    {
      public IRangeKeyOperations<T> ops;
      public T l1;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T _arg2;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal blocks(IRangeKeyOperations<T> ops, T l1, T _arg2)
      {
        this.ctor();
        this.ops = ops;
        this.l1 = l1;
        this._arg2 = _arg2;
      }

      public virtual int Invoke(Tuple<T, T> tupledArg)
      {
        T obj1 = tupledArg.Item1;
        T obj2 = tupledArg.Item2;
        T obj3 = this._arg2;
        return this.ops.Compare(this.l1, obj1);
      }
    }

    [Serializable]
    internal sealed class blocks<T> : FSharpFunc<Tuple<T, T>, FSharpFunc<Tuple<T, T>, int>>
    {
      public IRangeKeyOperations<T> ops;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal blocks(IRangeKeyOperations<T> ops)
      {
        this.ctor();
        this.ops = ops;
      }

      public virtual FSharpFunc<Tuple<T, T>, int> Invoke(Tuple<T, T> tupledArg)
      {
        return (FSharpFunc<Tuple<T, T>, int>) new Deedle.Ranges.Ranges.blocks<T>(this.ops, tupledArg.Item1, tupledArg.Item2);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class blocks<T> : GeneratedSequenceBase<Tuple<T, T>>
    {
      public IEnumerable<Deedle.Ranges.Ranges<T>> ranges;
      public Deedle.Ranges.Ranges<T> r;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<Deedle.Ranges.Ranges<T>> @enum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, T> v;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<Tuple<T, T>> enum0;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, T> current;

      public blocks(IEnumerable<Deedle.Ranges.Ranges<T>> ranges, Deedle.Ranges.Ranges<T> r, IEnumerator<Deedle.Ranges.Ranges<T>> @enum, Tuple<T, T> v, IEnumerator<Tuple<T, T>> enum0, int pc, Tuple<T, T> current)
      {
        this.ranges = ranges;
        this.r = r;
        this.@enum = @enum;
        this.v = v;
        this.enum0 = enum0;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<Tuple<T, T>> next)
      {
        switch (this.pc)
        {
          case 1:
label_8:
            this.pc = 4;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Deedle.Ranges.Ranges<T>>>((M0) this.@enum);
            this.@enum = (IEnumerator<Deedle.Ranges.Ranges<T>>) null;
            this.pc = 4;
            goto case 4;
          case 2:
label_7:
            this.pc = 1;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<T, T>>>((M0) this.enum0);
            this.enum0 = (IEnumerator<Tuple<T, T>>) null;
            this.r = (Deedle.Ranges.Ranges<T>) null;
            break;
          case 3:
            this.v = (Tuple<T, T>) null;
            goto label_4;
          case 4:
            this.current = (Tuple<T, T>) null;
            return 0;
          default:
            this.@enum = this.ranges.GetEnumerator();
            this.pc = 1;
            break;
        }
        if (this.@enum.MoveNext())
        {
          this.r = this.@enum.Current;
          this.enum0 = ((IEnumerable<Tuple<T, T>>) this.r.Ranges).GetEnumerator();
          this.pc = 2;
        }
        else
          goto label_8;
label_4:
        if (this.enum0.MoveNext())
        {
          this.v = this.enum0.Current;
          this.pc = 3;
          this.current = this.v;
          return 1;
        }
        goto label_7;
      }

      public virtual void Close()
      {
        Exception exception = (Exception) null;
        while (true)
        {
          switch (this.pc)
          {
            case 4:
              goto label_9;
            default:
              Unit unit;
              try
              {
                switch (this.pc)
                {
                  case 0:
                  case 4:
                    this.pc = 4;
                    this.current = (Tuple<T, T>) null;
                    unit = (Unit) null;
                    break;
                  case 1:
                    this.pc = 4;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Deedle.Ranges.Ranges<T>>>((M0) this.@enum);
                    goto case 0;
                  case 2:
                    this.pc = 1;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<T, T>>>((M0) this.enum0);
                    goto case 1;
                  default:
                    goto case 2;
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
label_9:
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
          case 2:
            return true;
          default:
            return true;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual Tuple<T, T> get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<Tuple<T, T>> GetFreshEnumerator()
      {
        return (IEnumerator<Tuple<T, T>>) new Deedle.Ranges.Ranges.blocks<T>(this.ranges, (Deedle.Ranges.Ranges<T>) null, (IEnumerator<Deedle.Ranges.Ranges<T>>) null, (Tuple<T, T>) null, (IEnumerator<Tuple<T, T>>) null, 0, (Tuple<T, T>) null);
      }
    }

    [Serializable]
    internal sealed class loop<T> : FSharpFunc<Unit, IEnumerable<Tuple<T, T>>>
    {
      public FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, IEnumerable<Tuple<T, T>>>> loop;
      public T s;
      public T e;
      public FSharpList<Tuple<T, T>> blocks;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal loop(FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, IEnumerable<Tuple<T, T>>>> loop, T s, T e, FSharpList<Tuple<T, T>> blocks)
      {
        this.ctor();
        this.loop = loop;
        this.s = s;
        this.e = e;
        this.blocks = blocks;
      }

      public virtual IEnumerable<Tuple<T, T>> Invoke(Unit unitVar)
      {
        return (IEnumerable<Tuple<T, T>>) FSharpFunc<Tuple<T, T>, FSharpList<Tuple<T, T>>>.InvokeFast<IEnumerable<Tuple<T, T>>>((FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, M0>>) this.loop, new Tuple<T, T>(this.s, this.e), this.blocks);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class loop<T> : GeneratedSequenceBase<Tuple<T, T>>
    {
      public IRangeKeyOperations<T> ops;
      public FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, IEnumerable<Tuple<T, T>>>> loop;
      public T startl;
      public T endl;
      public FSharpList<Tuple<T, T>> blocks;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpList<Tuple<T, T>> matchValue;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, T> current;

      public loop(IRangeKeyOperations<T> ops, FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, IEnumerable<Tuple<T, T>>>> loop, T startl, T endl, FSharpList<Tuple<T, T>> blocks, FSharpList<Tuple<T, T>> matchValue, int pc, Tuple<T, T> current)
      {
        this.ops = ops;
        this.loop = loop;
        this.startl = startl;
        this.endl = endl;
        this.blocks = blocks;
        this.matchValue = matchValue;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<Tuple<T, T>> next)
      {
        switch (this.pc)
        {
          case 1:
            this.matchValue = (FSharpList<Tuple<T, T>>) null;
            this.pc = 2;
            goto case 2;
          case 2:
            this.current = (Tuple<T, T>) null;
            return 0;
          default:
            this.matchValue = this.blocks;
            this.pc = 1;
            ref IEnumerable<Tuple<T, T>> local = ref next;
            IEnumerable<M0> m0s;
            if (this.matchValue.get_TailOrNull() != null)
            {
              FSharpList<Tuple<T, T>> matchValue1 = this.matchValue;
              T obj1 = matchValue1.get_HeadOrDefault().Item1;
              T obj2 = matchValue1.get_HeadOrDefault().Item2;
              matchValue1.get_TailOrNull();
              if (this.ops.Compare(obj1, this.endl) <= 0)
              {
                T obj3 = matchValue1.get_HeadOrDefault().Item1;
                T obj4 = matchValue1.get_HeadOrDefault().Item2;
                matchValue1.get_TailOrNull();
                string message = "Cannot combine overlapping ranges";
                if (true)
                  throw new InvalidOperationException(message);
                Unit unit = (Unit) null;
                m0s = SeqModule.Empty<Tuple<T, T>>();
              }
              else
              {
                if (this.matchValue.get_TailOrNull() != null)
                {
                  FSharpList<Tuple<T, T>> matchValue2 = this.matchValue;
                  T obj3 = matchValue2.get_HeadOrDefault().Item1;
                  T obj4 = matchValue2.get_HeadOrDefault().Item2;
                  matchValue2.get_TailOrNull();
                  if (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<T>((M0) obj3, (M0) this.ops.IncrementBy(this.endl, 1L)))
                  {
                    T obj5 = matchValue2.get_HeadOrDefault().Item1;
                    m0s = (IEnumerable<M0>) FSharpFunc<Tuple<T, T>, FSharpList<Tuple<T, T>>>.InvokeFast<IEnumerable<Tuple<T, T>>>((FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, M0>>) this.loop, new Tuple<T, T>(this.startl, matchValue2.get_HeadOrDefault().Item2), matchValue2.get_TailOrNull());
                    goto label_13;
                  }
                }
                if (this.matchValue.get_TailOrNull() == null)
                  throw new MatchFailureException("C:\\code\\Github\\Deedle\\src\\Deedle\\Common\\Ranges.fs", 111, 12);
                FSharpList<Tuple<T, T>> matchValue3 = this.matchValue;
                m0s = SeqModule.Append<Tuple<T, T>>(SeqModule.Singleton<Tuple<T, T>>((M0) new Tuple<T, T>(this.startl, this.endl)), SeqModule.Delay<Tuple<T, T>>((FSharpFunc<Unit, IEnumerable<M0>>) new Deedle.Ranges.Ranges.loop<T>(this.loop, matchValue3.get_HeadOrDefault().Item1, matchValue3.get_HeadOrDefault().Item2, matchValue3.get_TailOrNull())));
              }
            }
            else
              m0s = SeqModule.Singleton<Tuple<T, T>>((M0) new Tuple<T, T>(this.startl, this.endl));
label_13:
            local = (IEnumerable<Tuple<T, T>>) m0s;
            return 2;
        }
      }

      public virtual void Close()
      {
        this.pc = 2;
      }

      public virtual bool get_CheckClose()
      {
        switch (this.pc)
        {
          case 0:
          case 2:
            return false;
          default:
            return false;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual Tuple<T, T> get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<Tuple<T, T>> GetFreshEnumerator()
      {
        return (IEnumerator<Tuple<T, T>>) new Deedle.Ranges.Ranges.loop<T>(this.ops, this.loop, this.startl, this.endl, this.blocks, (FSharpList<Tuple<T, T>>) null, 0, (Tuple<T, T>) null);
      }
    }

    [Serializable]
    internal sealed class loop<T> : OptimizedClosures.FSharpFunc<Tuple<T, T>, FSharpList<Tuple<T, T>>, IEnumerable<Tuple<T, T>>>
    {
      public IRangeKeyOperations<T> ops;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal loop(IRangeKeyOperations<T> ops)
      {
        this.ctor();
        this.ops = ops;
      }

      public virtual IEnumerable<Tuple<T, T>> Invoke(Tuple<T, T> tupledArg, FSharpList<Tuple<T, T>> blocks)
      {
        return (IEnumerable<Tuple<T, T>>) new Deedle.Ranges.Ranges.loop<T>(this.ops, (FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, IEnumerable<Tuple<T, T>>>>) this, tupledArg.Item1, tupledArg.Item2, blocks, (FSharpList<Tuple<T, T>>) null, 0, (Tuple<T, T>) null);
      }
    }

    [Serializable]
    internal sealed class loop<T> : OptimizedClosures.FSharpFunc<long, int, T>
    {
      public long offs;
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal loop(long offs, Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.offs = offs;
        this.ranges = ranges;
      }

      public virtual T Invoke(long sum, int idx)
      {
        while (idx < this.ranges.Ranges.Length)
        {
          Tuple<T, T> range = this.ranges.Ranges[idx];
          T obj1 = range.Item1;
          T obj2 = range.Item2;
          T obj3 = this.ranges.Operations.IncrementBy(obj1, this.offs - sum);
          if (this.ranges.Operations.Compare(obj3, obj2) <= 0)
            return obj3;
          long num1 = this.ranges.Operations.Distance(obj1, obj2) + 1L;
          long num2 = sum + num1;
          ++idx;
          sum = num2;
        }
        throw new IndexOutOfRangeException();
      }
    }

    [Serializable]
    internal sealed class op_LessDot<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal op_LessDot(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual bool Invoke(T a, T b)
      {
        return this.ranges.Operations.Compare(a, b) < 0;
      }
    }

    [Serializable]
    internal sealed class op_GreaterEqualsDot<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal op_GreaterEqualsDot(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual bool Invoke(T a, T b)
      {
        return this.ranges.Operations.Compare(a, b) >= 0;
      }
    }

    [Serializable]
    internal sealed class op_LessEqualsDot<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal op_LessEqualsDot(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual bool Invoke(T a, T b)
      {
        return this.ranges.Operations.Compare(a, b) <= 0;
      }
    }

    [Serializable]
    internal sealed class loop<T> : OptimizedClosures.FSharpFunc<long, int, long>
    {
      public T key;
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal loop(T key, Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.key = key;
        this.ranges = ranges;
      }

      public virtual long Invoke(long offs, int idx)
      {
        while (idx < this.ranges.Ranges.Length)
        {
          Tuple<T, T> range = this.ranges.Ranges[idx];
          T obj1 = range.Item1;
          T obj2 = range.Item2;
          if (this.ranges.Operations.Compare(this.key, obj1) < 0)
            return Deedle.Ranges.Ranges.invalid;
          if ((this.ranges.Operations.Compare(this.key, obj1) < 0 ? 0 : (this.ranges.Operations.Compare(this.key, obj2) <= 0 ? 1 : 0)) != 0)
            return offs + this.ranges.Operations.Distance(obj1, this.key);
          long num = offs + this.ranges.Operations.Distance(obj1, obj2) + 1L;
          ++idx;
          offs = num;
        }
        return Deedle.Ranges.Ranges.invalid;
      }
    }

    [Serializable]
    internal sealed class op_LessDot<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal op_LessDot(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual bool Invoke(T a, T b)
      {
        return this.ranges.Operations.Compare(a, b) < 0;
      }
    }

    [Serializable]
    internal sealed class op_GreaterDot<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal op_GreaterDot(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual bool Invoke(T a, T b)
      {
        return this.ranges.Operations.Compare(a, b) > 0;
      }
    }

    [Serializable]
    internal sealed class op_GreaterEqualsDot<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal op_GreaterEqualsDot(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual bool Invoke(T a, T b)
      {
        return this.ranges.Operations.Compare(a, b) >= 0;
      }
    }

    [Serializable]
    internal sealed class op_LessEqualsDot<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal op_LessEqualsDot(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual bool Invoke(T a, T b)
      {
        return this.ranges.Operations.Compare(a, b) <= 0;
      }
    }

    [Serializable]
    internal sealed class max<T> : OptimizedClosures.FSharpFunc<T, T, T>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal max(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual T Invoke(T a, T b)
      {
        if (this.ranges.Operations.Compare(a, b) >= 0)
          return a;
        return b;
      }
    }

    [Serializable]
    internal sealed class min<T> : OptimizedClosures.FSharpFunc<T, T, T>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal min(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual T Invoke(T a, T b)
      {
        if (this.ranges.Operations.Compare(a, b) <= 0)
          return a;
        return b;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class newRanges<T> : GeneratedSequenceBase<Tuple<T, T>>
    {
      public Deedle.Ranges.Ranges<T> ranges;
      public T loRestr;
      public T hiRestr;
      public T lo;
      public T hi;
      public T newLo;
      public T newHi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, T> patternInput;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, T> matchValue;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<Tuple<T, T>> @enum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, T> current;

      public newRanges(Deedle.Ranges.Ranges<T> ranges, T loRestr, T hiRestr, T lo, T hi, T newLo, T newHi, Tuple<T, T> patternInput, Tuple<T, T> matchValue, IEnumerator<Tuple<T, T>> @enum, int pc, Tuple<T, T> current)
      {
        this.ranges = ranges;
        this.loRestr = loRestr;
        this.hiRestr = hiRestr;
        this.lo = lo;
        this.hi = hi;
        this.newLo = newLo;
        this.newHi = newHi;
        this.patternInput = patternInput;
        this.matchValue = matchValue;
        this.@enum = @enum;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<Tuple<T, T>> next)
      {
        switch (this.pc)
        {
          case 1:
label_10:
            this.pc = 4;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<T, T>>>((M0) this.@enum);
            this.@enum = (IEnumerator<Tuple<T, T>>) null;
            this.pc = 4;
            goto case 4;
          case 2:
label_9:
            this.hi = default (T);
            this.lo = default (T);
            this.matchValue = (Tuple<T, T>) null;
            break;
          case 3:
label_8:
            this.newHi = default (T);
            this.newLo = default (T);
            this.patternInput = (Tuple<T, T>) null;
            goto case 2;
          case 4:
            this.current = (Tuple<T, T>) null;
            return 0;
          default:
            this.@enum = ((IEnumerable<Tuple<T, T>>) this.ranges.Ranges).GetEnumerator();
            this.pc = 1;
            break;
        }
        if (this.@enum.MoveNext())
        {
          this.matchValue = this.@enum.Current;
          this.lo = this.matchValue.Item1;
          this.hi = this.matchValue.Item2;
          if ((this.ranges.Operations.Compare(this.lo, this.loRestr) < 0 ? 0 : (this.ranges.Operations.Compare(this.hi, this.hiRestr) <= 0 ? 1 : 0)) != 0)
          {
            this.pc = 2;
            this.current = new Tuple<T, T>(this.lo, this.hi);
            return 1;
          }
          if ((this.ranges.Operations.Compare(this.hi, this.loRestr) >= 0 ? (this.ranges.Operations.Compare(this.lo, this.hiRestr) > 0 ? 1 : 0) : 1) == 0)
          {
            T lo = this.lo;
            T loRestr = this.loRestr;
            T obj1 = this.ranges.Operations.Compare(lo, loRestr) < 0 ? loRestr : lo;
            T hi = this.hi;
            T hiRestr = this.hiRestr;
            T obj2 = this.ranges.Operations.Compare(hi, hiRestr) > 0 ? hiRestr : hi;
            this.patternInput = new Tuple<T, T>(obj1, obj2);
            this.newLo = this.patternInput.Item1;
            this.newHi = this.patternInput.Item2;
            if (this.ranges.Operations.Compare(this.newLo, this.newHi) <= 0)
            {
              this.pc = 3;
              this.current = new Tuple<T, T>(this.newLo, this.newHi);
              return 1;
            }
            goto label_8;
          }
          else
            goto label_9;
        }
        else
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
                    this.current = (Tuple<T, T>) null;
                    unit = (Unit) null;
                    break;
                  default:
                    this.pc = 4;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<T, T>>>((M0) this.@enum);
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
      public virtual Tuple<T, T> get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<Tuple<T, T>> GetFreshEnumerator()
      {
        return (IEnumerator<Tuple<T, T>>) new Deedle.Ranges.Ranges.newRanges<T>(this.ranges, this.loRestr, this.hiRestr, default (T), default (T), default (T), default (T), (Tuple<T, T>) null, (Tuple<T, T>) null, (IEnumerator<Tuple<T, T>>) null, 0, (Tuple<T, T>) null);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class loop<T> : GeneratedSequenceBase<Tuple<T, T>>
    {
      public Deedle.Ranges.Ranges<T> ranges;
      public int step;
      public bool isStart;
      public FSharpFunc<int, FSharpFunc<int, IEnumerable<Tuple<T, T>>>> loop;
      public int rangeIdx;
      public int desiredCount;
      public T lo;
      public T hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, T> patternInput;
      public IEnumerable<T> range;
      public int length;
      public T last;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, int> patternInput0;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, T> current;

      public loop(Deedle.Ranges.Ranges<T> ranges, int step, bool isStart, FSharpFunc<int, FSharpFunc<int, IEnumerable<Tuple<T, T>>>> loop, int rangeIdx, int desiredCount, T lo, T hi, Tuple<T, T> patternInput, IEnumerable<T> range, int length, T last, Tuple<T, int> patternInput0, int pc, Tuple<T, T> current)
      {
        this.ranges = ranges;
        this.step = step;
        this.isStart = isStart;
        this.loop = loop;
        this.rangeIdx = rangeIdx;
        this.desiredCount = desiredCount;
        this.lo = lo;
        this.hi = hi;
        this.patternInput = patternInput;
        this.range = range;
        this.length = length;
        this.last = last;
        this.patternInput0 = patternInput0;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<Tuple<T, T>> next)
      {
        switch (this.pc)
        {
          case 1:
            this.pc = 2;
            next = (IEnumerable<Tuple<T, T>>) FSharpFunc<int, int>.InvokeFast<IEnumerable<Tuple<T, T>>>((FSharpFunc<int, FSharpFunc<int, M0>>) this.loop, this.rangeIdx + this.step, this.desiredCount - this.length);
            return 2;
          case 2:
            this.last = default (T);
            this.length = 0;
            this.patternInput0 = (Tuple<T, int>) null;
            this.range = (IEnumerable<T>) null;
            this.hi = default (T);
            this.lo = default (T);
            this.patternInput = (Tuple<T, T>) null;
            break;
          case 3:
label_20:
            this.current = (Tuple<T, T>) null;
            return 0;
          default:
            if (this.desiredCount > 0)
            {
              if ((this.rangeIdx >= 0 ? (this.rangeIdx >= this.ranges.Ranges.Length ? 1 : 0) : 1) != 0)
              {
                string message = "Insufficient number of elements in the range";
                if (true)
                  throw new InvalidOperationException(message);
                Unit unit = (Unit) null;
              }
              this.patternInput = this.ranges.Ranges[this.rangeIdx];
              this.lo = this.patternInput.Item1;
              this.hi = this.patternInput.Item2;
              this.range = !this.isStart ? this.ranges.Operations.Range(this.hi, this.lo) : this.ranges.Operations.Range(this.lo, this.hi);
              IEnumerable<T> objs = (IEnumerable<T>) SeqModule.Truncate<T>(this.desiredCount, (IEnumerable<M0>) this.range);
              T obj = default (T);
              int num = 0;
              IEnumerator<T> enumerator = objs.GetEnumerator();
              Deedle.Ranges.Ranges.loop<T> loop1978 = this;
              Tuple<T, int> tuple;
              try
              {
                while (enumerator.MoveNext())
                {
                  obj = enumerator.Current;
                  ++num;
                }
                if (num == 0)
                  throw new InvalidOperationException("Insufficient number of elements");
                tuple = new Tuple<T, int>(obj, num);
              }
              finally
              {
                (enumerator as IDisposable)?.Dispose();
              }
              loop1978.patternInput0 = tuple;
              this.length = this.patternInput0.Item2;
              this.last = this.patternInput0.Item1;
              this.pc = 1;
              this.current = !this.isStart ? new Tuple<T, T>(this.last, this.hi) : new Tuple<T, T>(this.lo, this.last);
              return 1;
            }
            break;
        }
        this.pc = 3;
        goto label_20;
      }

      public virtual void Close()
      {
        this.pc = 3;
      }

      public virtual bool get_CheckClose()
      {
        switch (this.pc)
        {
          case 0:
          case 3:
            return false;
          case 1:
            return false;
          default:
            return false;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual Tuple<T, T> get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<Tuple<T, T>> GetFreshEnumerator()
      {
        return (IEnumerator<Tuple<T, T>>) new Deedle.Ranges.Ranges.loop<T>(this.ranges, this.step, this.isStart, this.loop, this.rangeIdx, this.desiredCount, default (T), default (T), (Tuple<T, T>) null, (IEnumerable<T>) null, 0, default (T), (Tuple<T, int>) null, 0, (Tuple<T, T>) null);
      }
    }

    [Serializable]
    internal sealed class loop<T> : OptimizedClosures.FSharpFunc<int, int, IEnumerable<Tuple<T, T>>>
    {
      public Deedle.Ranges.Ranges<T> ranges;
      public int step;
      public bool isStart;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal loop(Deedle.Ranges.Ranges<T> ranges, int step, bool isStart)
      {
        this.ctor();
        this.ranges = ranges;
        this.step = step;
        this.isStart = isStart;
      }

      public virtual IEnumerable<Tuple<T, T>> Invoke(int rangeIdx, int desiredCount)
      {
        return (IEnumerable<Tuple<T, T>>) new Deedle.Ranges.Ranges.loop<T>(this.ranges, this.step, this.isStart, (FSharpFunc<int, FSharpFunc<int, IEnumerable<Tuple<T, T>>>>) this, rangeIdx, desiredCount, default (T), default (T), (Tuple<T, T>) null, (IEnumerable<T>) null, 0, default (T), (Tuple<T, int>) null, 0, (Tuple<T, T>) null);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class keys<T> : GeneratedSequenceBase<T>
    {
      public Deedle.Ranges.Ranges<T> ranges;
      public T l;
      public T h;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, T> matchValue;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<Tuple<T, T>> @enum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T v;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<T> enum0;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T current;

      public keys(Deedle.Ranges.Ranges<T> ranges, T l, T h, Tuple<T, T> matchValue, IEnumerator<Tuple<T, T>> @enum, T v, IEnumerator<T> enum0, int pc, T current)
      {
        this.ranges = ranges;
        this.l = l;
        this.h = h;
        this.matchValue = matchValue;
        this.@enum = @enum;
        this.v = v;
        this.enum0 = enum0;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<T> next)
      {
        switch (this.pc)
        {
          case 1:
label_8:
            this.pc = 4;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<T, T>>>((M0) this.@enum);
            this.@enum = (IEnumerator<Tuple<T, T>>) null;
            this.pc = 4;
            goto case 4;
          case 2:
label_7:
            this.pc = 1;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<T>>((M0) this.enum0);
            this.enum0 = (IEnumerator<T>) null;
            this.h = default (T);
            this.l = default (T);
            this.matchValue = (Tuple<T, T>) null;
            break;
          case 3:
            this.v = default (T);
            goto label_4;
          case 4:
            this.current = default (T);
            return 0;
          default:
            this.@enum = ((IEnumerable<Tuple<T, T>>) this.ranges.Ranges).GetEnumerator();
            this.pc = 1;
            break;
        }
        if (this.@enum.MoveNext())
        {
          this.matchValue = this.@enum.Current;
          this.l = this.matchValue.Item1;
          this.h = this.matchValue.Item2;
          this.enum0 = this.ranges.Operations.Range(this.l, this.h).GetEnumerator();
          this.pc = 2;
        }
        else
          goto label_8;
label_4:
        if (this.enum0.MoveNext())
        {
          this.v = this.enum0.Current;
          this.pc = 3;
          this.current = this.v;
          return 1;
        }
        goto label_7;
      }

      public virtual void Close()
      {
        Exception exception = (Exception) null;
        while (true)
        {
          switch (this.pc)
          {
            case 4:
              goto label_9;
            default:
              Unit unit;
              try
              {
                switch (this.pc)
                {
                  case 0:
                  case 4:
                    this.pc = 4;
                    this.current = default (T);
                    unit = (Unit) null;
                    break;
                  case 1:
                    this.pc = 4;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<T, T>>>((M0) this.@enum);
                    goto case 0;
                  case 2:
                    this.pc = 1;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<T>>((M0) this.enum0);
                    goto case 1;
                  default:
                    goto case 2;
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
label_9:
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
          case 2:
            return true;
          default:
            return true;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual T get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<T> GetFreshEnumerator()
      {
        return (IEnumerator<T>) new Deedle.Ranges.Ranges.keys<T>(this.ranges, default (T), default (T), (Tuple<T, T>) null, (IEnumerator<Tuple<T, T>>) null, default (T), (IEnumerator<T>) null, 0, default (T));
      }
    }

    [Serializable]
    internal sealed class length<T> : FSharpFunc<Tuple<T, T>, long>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal length(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual long Invoke(Tuple<T, T> tupledArg)
      {
        return this.ranges.Operations.Distance(tupledArg.Item1, tupledArg.Item2) + 1L;
      }
    }

    [Serializable]
    internal sealed class op_LessDot<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal op_LessDot(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual bool Invoke(T a, T b)
      {
        return this.ranges.Operations.Compare(a, b) < 0;
      }
    }

    [Serializable]
    internal sealed class op_LessDot<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal op_LessDot(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual bool Invoke(T a, T b)
      {
        return this.ranges.Operations.Compare(a, b) < 0;
      }
    }

    [Serializable]
    internal sealed class op_GreaterEqualsDot<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal op_GreaterEqualsDot(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual bool Invoke(T a, T b)
      {
        return this.ranges.Operations.Compare(a, b) >= 0;
      }
    }

    [Serializable]
    internal sealed class op_LessEqualsDot<T> : OptimizedClosures.FSharpFunc<T, T, bool>
    {
      public Deedle.Ranges.Ranges<T> ranges;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal op_LessEqualsDot(Deedle.Ranges.Ranges<T> ranges)
      {
        this.ctor();
        this.ranges = ranges;
      }

      public virtual bool Invoke(T a, T b)
      {
        return this.ranges.Operations.Compare(a, b) <= 0;
      }
    }

    [Serializable]
    internal sealed class step : FSharpFunc<long, long>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long x;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal step(long x)
      {
        this.ctor();
        this.x = x;
      }

      public virtual long Invoke(long y)
      {
        return this.x + y;
      }
    }

    [Serializable]
    internal sealed class step : FSharpFunc<long, long>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long x;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal step(long x)
      {
        this.ctor();
        this.x = x;
      }

      public virtual long Invoke(long y)
      {
        return this.x + y;
      }
    }

    [Serializable]
    internal sealed class loop<T> : OptimizedClosures.FSharpFunc<long, int, Tuple<long, int>>
    {
      public Lookup semantics;
      public Deedle.Ranges.Ranges<T> ranges;
      public T validKey;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal loop(Lookup semantics, Deedle.Ranges.Ranges<T> ranges, T validKey)
      {
        this.ctor();
        this.semantics = semantics;
        this.ranges = ranges;
        this.validKey = validKey;
      }

      public virtual Tuple<long, int> Invoke(long offs, int idx)
      {
        while ((idx < this.ranges.Ranges.Length ? 0 : ((this.semantics & Lookup.Greater) == Lookup.Greater ? 1 : 0)) == 0)
        {
          if ((idx < this.ranges.Ranges.Length ? 0 : ((this.semantics & Lookup.Smaller) == Lookup.Smaller ? 1 : 0)) != 0)
            return new Tuple<long, int>(offs - 1L, this.ranges.Ranges.Length - 1);
          Tuple<T, T> range = this.ranges.Ranges[idx];
          T obj1 = range.Item1;
          T obj2 = range.Item2;
          if ((this.ranges.Operations.Compare(this.validKey, obj1) < 0 ? 0 : (this.ranges.Operations.Compare(this.validKey, obj2) <= 0 ? 1 : 0)) != 0)
            return new Tuple<long, int>(offs + this.ranges.Operations.Distance(obj1, this.validKey), idx);
          if ((this.ranges.Operations.Compare(this.validKey, obj1) >= 0 ? 0 : ((this.semantics & Lookup.Greater) == Lookup.Greater ? 1 : 0)) != 0)
            return new Tuple<long, int>(offs, idx);
          if ((this.ranges.Operations.Compare(this.validKey, obj1) >= 0 ? 0 : ((this.semantics & Lookup.Smaller) == Lookup.Smaller ? 1 : 0)) != 0)
            return new Tuple<long, int>(offs - 1L, idx - 1);
          long num = offs + this.ranges.Operations.Distance(obj1, obj2) + 1L;
          ++idx;
          offs = num;
        }
        return new Tuple<long, int>(Deedle.Ranges.Ranges.invalid, -1);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class keysToScan<T> : GeneratedSequenceBase<T>
    {
      public T keyStart;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T current;

      public keysToScan(T keyStart, int pc, T current)
      {
        this.keyStart = keyStart;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<T> next)
      {
        switch (this.pc)
        {
          case 1:
            this.pc = 2;
            goto case 2;
          case 2:
            this.current = default (T);
            return 0;
          default:
            this.pc = 1;
            this.current = this.keyStart;
            return 1;
        }
      }

      public virtual void Close()
      {
        this.pc = 2;
      }

      public virtual bool get_CheckClose()
      {
        switch (this.pc)
        {
          case 0:
          case 2:
            return false;
          default:
            return false;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual T get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<T> GetFreshEnumerator()
      {
        return (IEnumerator<T>) new Deedle.Ranges.Ranges.keysToScan<T>(this.keyStart, 0, default (T));
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class keysToScan<T> : GeneratedSequenceBase<T>
    {
      public Deedle.Ranges.Ranges<T> ranges;
      public int rangeIdx;
      public T keyStart;
      public T lo;
      public T hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, T> matchValue;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T v;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<T> @enum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<Tuple<T, T>> enum0;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T v0;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<T> enum1;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T current;

      public keysToScan(Deedle.Ranges.Ranges<T> ranges, int rangeIdx, T keyStart, T lo, T hi, Tuple<T, T> matchValue, T v, IEnumerator<T> @enum, IEnumerator<Tuple<T, T>> enum0, T v0, IEnumerator<T> enum1, int pc, T current)
      {
        this.ranges = ranges;
        this.rangeIdx = rangeIdx;
        this.keyStart = keyStart;
        this.lo = lo;
        this.hi = hi;
        this.matchValue = matchValue;
        this.v = v;
        this.@enum = @enum;
        this.enum0 = enum0;
        this.v0 = v0;
        this.enum1 = enum1;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<T> next)
      {
        switch (this.pc)
        {
          case 1:
label_5:
            this.pc = 6;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<T>>((M0) this.@enum);
            this.@enum = (IEnumerator<T>) null;
            Tuple<T, T>[] ranges = this.ranges.Ranges;
            FSharpOption<int> fsharpOption1 = FSharpOption<int>.Some(this.rangeIdx + 1);
            FSharpOption<int> fsharpOption2 = (FSharpOption<int>) null;
            int length1 = ranges.Length;
            Tuple<int, int> tuple1;
            if (fsharpOption1 == null)
            {
              if (fsharpOption2 != null)
              {
                FSharpOption<int> fsharpOption3 = fsharpOption2;
                if (fsharpOption3.get_Value() >= 0)
                {
                  tuple1 = new Tuple<int, int>(0, fsharpOption3.get_Value());
                  goto label_17;
                }
              }
              else
              {
                tuple1 = new Tuple<int, int>(0, 0 + length1 - 1);
                goto label_17;
              }
            }
            if (fsharpOption1 != null)
            {
              FSharpOption<int> fsharpOption3 = fsharpOption1;
              if (fsharpOption2 == null && fsharpOption3.get_Value() <= 0 + length1)
              {
                tuple1 = new Tuple<int, int>(fsharpOption3.get_Value(), 0 + length1 - 1);
                goto label_17;
              }
            }
            if (fsharpOption1 != null)
            {
              FSharpOption<int> fsharpOption3 = fsharpOption1;
              if (fsharpOption2 != null)
              {
                int num = fsharpOption2.get_Value();
                tuple1 = new Tuple<int, int>(fsharpOption3.get_Value(), num);
                goto label_17;
              }
            }
            throw new IndexOutOfRangeException();
label_17:
            Tuple<int, int> tuple2 = tuple1;
            int num1 = tuple2.Item1;
            int num2 = tuple2.Item2 - num1 + 1;
            int length2 = num2 >= 0 ? num2 : 0;
            Tuple<T, T>[] tupleArray = new Tuple<T, T>[length2];
            Deedle.Ranges.Ranges.keysToScan<T> keysToScan2741 = this;
            int index = 0;
            int num3 = length2 - 1;
            if (num3 >= index)
            {
              do
              {
                tupleArray[index] = ranges[num1 + index];
                ++index;
              }
              while (index != num3 + 1);
            }
            keysToScan2741.enum0 = ((IEnumerable<Tuple<T, T>>) tupleArray).GetEnumerator();
            this.pc = 3;
            goto label_20;
          case 2:
            this.v = default (T);
            break;
          case 3:
label_26:
            this.pc = 6;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<T, T>>>((M0) this.enum0);
            this.enum0 = (IEnumerator<Tuple<T, T>>) null;
            this.pc = 6;
            goto case 6;
          case 4:
label_25:
            this.pc = 3;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<T>>((M0) this.enum1);
            this.enum1 = (IEnumerator<T>) null;
            this.hi = default (T);
            this.lo = default (T);
            this.matchValue = (Tuple<T, T>) null;
            goto label_20;
          case 5:
            this.v0 = default (T);
            goto label_22;
          case 6:
            this.current = default (T);
            return 0;
          default:
            this.@enum = this.ranges.Operations.Range(this.keyStart, this.ranges.Ranges[this.rangeIdx].Item2).GetEnumerator();
            this.pc = 1;
            break;
        }
        if (this.@enum.MoveNext())
        {
          this.v = this.@enum.Current;
          this.pc = 2;
          this.current = this.v;
          return 1;
        }
        goto label_5;
label_20:
        if (this.enum0.MoveNext())
        {
          this.matchValue = this.enum0.Current;
          this.lo = this.matchValue.Item1;
          this.hi = this.matchValue.Item2;
          this.enum1 = this.ranges.Operations.Range(this.lo, this.hi).GetEnumerator();
          this.pc = 4;
        }
        else
          goto label_26;
label_22:
        if (this.enum1.MoveNext())
        {
          this.v0 = this.enum1.Current;
          this.pc = 5;
          this.current = this.v0;
          return 1;
        }
        goto label_25;
      }

      public virtual void Close()
      {
        Exception exception = (Exception) null;
        while (true)
        {
          switch (this.pc)
          {
            case 6:
              goto label_10;
            default:
              Unit unit;
              try
              {
                switch (this.pc)
                {
                  case 0:
                  case 6:
                    this.pc = 6;
                    this.current = default (T);
                    unit = (Unit) null;
                    break;
                  case 1:
                  case 2:
                    this.pc = 6;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<T>>((M0) this.@enum);
                    goto case 0;
                  case 3:
                    this.pc = 6;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<T, T>>>((M0) this.enum0);
                    goto case 0;
                  case 4:
                    this.pc = 3;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<T>>((M0) this.enum1);
                    goto case 3;
                  default:
                    goto case 4;
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
label_10:
        if (exception != null)
          throw exception;
      }

      public virtual bool get_CheckClose()
      {
        switch (this.pc)
        {
          case 0:
          case 6:
            return false;
          case 1:
            return true;
          case 2:
            return true;
          case 3:
            return true;
          case 4:
            return true;
          default:
            return true;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual T get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<T> GetFreshEnumerator()
      {
        return (IEnumerator<T>) new Deedle.Ranges.Ranges.keysToScan<T>(this.ranges, this.rangeIdx, this.keyStart, default (T), default (T), (Tuple<T, T>) null, default (T), (IEnumerator<T>) null, (IEnumerator<Tuple<T, T>>) null, default (T), (IEnumerator<T>) null, 0, default (T));
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class keysToScan<T> : GeneratedSequenceBase<T>
    {
      public Deedle.Ranges.Ranges<T> ranges;
      public int rangeIdx;
      public T keyStart;
      public T lo;
      public T hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<T, T> matchValue;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T v;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<T> @enum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<Tuple<T, T>> enum0;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T v0;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<T> enum1;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public T current;

      public keysToScan(Deedle.Ranges.Ranges<T> ranges, int rangeIdx, T keyStart, T lo, T hi, Tuple<T, T> matchValue, T v, IEnumerator<T> @enum, IEnumerator<Tuple<T, T>> enum0, T v0, IEnumerator<T> enum1, int pc, T current)
      {
        this.ranges = ranges;
        this.rangeIdx = rangeIdx;
        this.keyStart = keyStart;
        this.lo = lo;
        this.hi = hi;
        this.matchValue = matchValue;
        this.v = v;
        this.@enum = @enum;
        this.enum0 = enum0;
        this.v0 = v0;
        this.enum1 = enum1;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<T> next)
      {
        switch (this.pc)
        {
          case 1:
label_5:
            this.pc = 6;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<T>>((M0) this.@enum);
            this.@enum = (IEnumerator<T>) null;
            Tuple<T, T>[] ranges = this.ranges.Ranges;
            FSharpOption<int> fsharpOption1 = (FSharpOption<int>) null;
            FSharpOption<int> fsharpOption2 = FSharpOption<int>.Some(this.rangeIdx - 1);
            int length1 = ranges.Length;
            Tuple<int, int> tuple1;
            if (fsharpOption1 == null)
            {
              if (fsharpOption2 != null)
              {
                FSharpOption<int> fsharpOption3 = fsharpOption2;
                if (fsharpOption3.get_Value() >= 0)
                {
                  tuple1 = new Tuple<int, int>(0, fsharpOption3.get_Value());
                  goto label_17;
                }
              }
              else
              {
                tuple1 = new Tuple<int, int>(0, 0 + length1 - 1);
                goto label_17;
              }
            }
            if (fsharpOption1 != null)
            {
              FSharpOption<int> fsharpOption3 = fsharpOption1;
              if (fsharpOption2 == null && fsharpOption3.get_Value() <= 0 + length1)
              {
                tuple1 = new Tuple<int, int>(fsharpOption3.get_Value(), 0 + length1 - 1);
                goto label_17;
              }
            }
            if (fsharpOption1 != null)
            {
              FSharpOption<int> fsharpOption3 = fsharpOption1;
              if (fsharpOption2 != null)
              {
                int num = fsharpOption2.get_Value();
                tuple1 = new Tuple<int, int>(fsharpOption3.get_Value(), num);
                goto label_17;
              }
            }
            throw new IndexOutOfRangeException();
label_17:
            Tuple<int, int> tuple2 = tuple1;
            int num1 = tuple2.Item1;
            int num2 = tuple2.Item2 - num1 + 1;
            int length2 = num2 >= 0 ? num2 : 0;
            Tuple<T, T>[] tupleArray = new Tuple<T, T>[length2];
            Deedle.Ranges.Ranges.keysToScan<T> keysToScan2782 = this;
            int index = 0;
            int num3 = length2 - 1;
            if (num3 >= index)
            {
              do
              {
                tupleArray[index] = ranges[num1 + index];
                ++index;
              }
              while (index != num3 + 1);
            }
            keysToScan2782.enum0 = ((IEnumerable<Tuple<T, T>>) ArrayModule.Reverse<Tuple<T, T>>((M0[]) tupleArray)).GetEnumerator();
            this.pc = 3;
            goto label_20;
          case 2:
            this.v = default (T);
            break;
          case 3:
label_26:
            this.pc = 6;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<T, T>>>((M0) this.enum0);
            this.enum0 = (IEnumerator<Tuple<T, T>>) null;
            this.pc = 6;
            goto case 6;
          case 4:
label_25:
            this.pc = 3;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<T>>((M0) this.enum1);
            this.enum1 = (IEnumerator<T>) null;
            this.hi = default (T);
            this.lo = default (T);
            this.matchValue = (Tuple<T, T>) null;
            goto label_20;
          case 5:
            this.v0 = default (T);
            goto label_22;
          case 6:
            this.current = default (T);
            return 0;
          default:
            this.@enum = this.ranges.Operations.Range(this.keyStart, this.ranges.Ranges[this.rangeIdx].Item1).GetEnumerator();
            this.pc = 1;
            break;
        }
        if (this.@enum.MoveNext())
        {
          this.v = this.@enum.Current;
          this.pc = 2;
          this.current = this.v;
          return 1;
        }
        goto label_5;
label_20:
        if (this.enum0.MoveNext())
        {
          this.matchValue = this.enum0.Current;
          this.lo = this.matchValue.Item1;
          this.hi = this.matchValue.Item2;
          this.enum1 = this.ranges.Operations.Range(this.hi, this.lo).GetEnumerator();
          this.pc = 4;
        }
        else
          goto label_26;
label_22:
        if (this.enum1.MoveNext())
        {
          this.v0 = this.enum1.Current;
          this.pc = 5;
          this.current = this.v0;
          return 1;
        }
        goto label_25;
      }

      public virtual void Close()
      {
        Exception exception = (Exception) null;
        while (true)
        {
          switch (this.pc)
          {
            case 6:
              goto label_10;
            default:
              Unit unit;
              try
              {
                switch (this.pc)
                {
                  case 0:
                  case 6:
                    this.pc = 6;
                    this.current = default (T);
                    unit = (Unit) null;
                    break;
                  case 1:
                  case 2:
                    this.pc = 6;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<T>>((M0) this.@enum);
                    goto case 0;
                  case 3:
                    this.pc = 6;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<T, T>>>((M0) this.enum0);
                    goto case 0;
                  case 4:
                    this.pc = 3;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<T>>((M0) this.enum1);
                    goto case 3;
                  default:
                    goto case 4;
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
label_10:
        if (exception != null)
          throw exception;
      }

      public virtual bool get_CheckClose()
      {
        switch (this.pc)
        {
          case 0:
          case 6:
            return false;
          case 1:
            return true;
          case 2:
            return true;
          case 3:
            return true;
          case 4:
            return true;
          default:
            return true;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual T get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<T> GetFreshEnumerator()
      {
        return (IEnumerator<T>) new Deedle.Ranges.Ranges.keysToScan<T>(this.ranges, this.rangeIdx, this.keyStart, default (T), default (T), (Tuple<T, T>) null, default (T), (IEnumerator<T>) null, (IEnumerator<Tuple<T, T>>) null, default (T), (IEnumerator<T>) null, 0, default (T));
      }
    }

    [Serializable]
    internal sealed class lookup<T> : FSharpFunc<Tuple<T, long>, bool>
    {
      public FSharpFunc<T, FSharpFunc<long, bool>> check;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal lookup(FSharpFunc<T, FSharpFunc<long, bool>> check)
      {
        this.ctor();
        this.check = check;
      }

      public virtual bool Invoke(Tuple<T, long> tupledArg)
      {
        return (bool) FSharpFunc<T, long>.InvokeFast<bool>((FSharpFunc<T, FSharpFunc<long, M0>>) this.check, tupledArg.Item1, tupledArg.Item2);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class lookup : GeneratedSequenceBase<long>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<long, long> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long start;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpRef<long> state;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long current;

      public lookup(FSharpFunc<long, long> f, long start, FSharpRef<long> state, int pc, long current)
      {
        this.f = f;
        this.start = start;
        this.state = state;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<long> next)
      {
        switch (this.pc)
        {
          case 1:
            Operators.op_ColonEquals<long>((FSharpRef<M0>) this.state, (M0) this.f.Invoke(this.state.get_Value()));
            break;
          case 2:
label_6:
            this.current = 0L;
            return 0;
          default:
            this.state = (FSharpRef<long>) Operators.Ref<long>((M0) this.start);
            break;
        }
        if (!false)
        {
          this.pc = 1;
          this.current = this.state.get_Value();
          return 1;
        }
        this.state = (FSharpRef<long>) null;
        this.pc = 2;
        goto label_6;
      }

      public virtual void Close()
      {
        this.pc = 2;
      }

      public virtual bool get_CheckClose()
      {
        switch (this.pc)
        {
          case 1:
            return false;
          default:
            return false;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual long get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<long> GetFreshEnumerator()
      {
        return (IEnumerator<long>) new Deedle.Ranges.Ranges.lookup(this.f, this.start, (FSharpRef<long>) null, 0, 0L);
      }
    }

    [Serializable]
    internal sealed class lookup<T> : FSharpFunc<IEnumerable<Tuple<T, long>>, IEnumerable<Tuple<T, long>>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int count;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal lookup(int count)
      {
        this.ctor();
        this.count = count;
      }

      public virtual IEnumerable<Tuple<T, long>> Invoke(IEnumerable<Tuple<T, long>> source)
      {
        return (IEnumerable<Tuple<T, long>>) SeqModule.Skip<Tuple<T, long>>(this.count, (IEnumerable<M0>) source);
      }
    }

    [Serializable]
    internal sealed class lookup<T> : FSharpFunc<IEnumerable<Tuple<T, long>>, IEnumerable<Tuple<T, long>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal lookup()
      {
        base.ctor();
      }

      public virtual IEnumerable<Tuple<T, long>> Invoke(IEnumerable<Tuple<T, long>> x)
      {
        return (IEnumerable<Tuple<T, long>>) Operators.Identity<IEnumerable<Tuple<T, long>>>((M0) x);
      }
    }
  }
}
