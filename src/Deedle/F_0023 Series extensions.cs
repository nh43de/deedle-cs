// Decompiled with JetBrains decompiler
// Type: Deedle.F# Series extensions
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll



using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deedle
{
  [AutoOpen]
  
  public static class FSeriesextensions
  {
    public static Deedle.Series<a, b> series<a, b>(IEnumerable<Tuple<a, b>> observations)
    {
      return FSeriesextensions.Series.ofObservations<a, b>(observations);
    }

    
    [Serializable]
    public class Series
    {
      public static Deedle.Series<a, b> ofObservations<a, b>(IEnumerable<Tuple<a, b>> observations)
      {
        Tuple<a, b>[] tupleArray1 = (Tuple<a, b>[]) ArrayModule.OfSeq<Tuple<a, b>>((IEnumerable<M0>) observations);
        FSharpFunc<Tuple<a, b>, a> fsharpFunc1 = (FSharpFunc<Tuple<a, b>, a>) new FSeriesextensions.ofObservations<a, b>();
        Tuple<a, b>[] tupleArray2 = tupleArray1;
        if ((object) tupleArray2 == null)
          throw new ArgumentNullException("array");
        a[] aArray1 = new a[tupleArray2.Length];
        for (int index = 0; index < aArray1.Length; ++index)
          aArray1[index] = fsharpFunc1.Invoke(tupleArray2[index]);
        a[] aArray2 = aArray1;
        FSharpFunc<Tuple<a, b>, b> fsharpFunc2 = (FSharpFunc<Tuple<a, b>, b>) new FSeriesextensions.ofObservations<a, b>();
        Tuple<a, b>[] tupleArray3 = tupleArray1;
        if ((object) tupleArray3 == null)
          throw new ArgumentNullException("array");
        b[] values = new b[tupleArray3.Length];
        a[] keys = aArray2;
        for (int index = 0; index < values.Length; ++index)
          values[index] = fsharpFunc2.Invoke(tupleArray3[index]);
        return new Deedle.Series<a, b>(keys, values);
      }

      public static Deedle.Series<int, a> ofValues<a>(IEnumerable<a> values)
      {
        return new Deedle.Series<int, a>((IEnumerable<int>) SeqModule.MapIndexed<a, int>((FSharpFunc<int, FSharpFunc<M0, M1>>) new FSeriesextensions.keys<a>(), values), values);
      }

      public static Deedle.Series<int, a> ofNullables<a>(IEnumerable<a?> values) where a : struct
      {
        return new Deedle.Series<int, a?>((IEnumerable<int>) SeqModule.MapIndexed<a?, int>((FSharpFunc<int, FSharpFunc<M0, M1>>) new FSeriesextensions.keys<a>(), (IEnumerable<M0>) values), values).Select<a>(new Func<KeyValuePair<int, a?>, a>(new FSeriesextensions.ofNullables<a>().Invoke));
      }

      public static Deedle.Series<K, a> ofOptionalObservations<K, a>(IEnumerable<Tuple<K, FSharpOption<a>>> observations)
      {
        return new Deedle.Series<K, FSharpOption<a>>((IEnumerable<K>) SeqModule.Map<Tuple<K, FSharpOption<a>>, K>((FSharpFunc<M0, M1>) new FSeriesextensions.ofOptionalObservations<K, a>(), (IEnumerable<M0>) observations), (IEnumerable<FSharpOption<a>>) SeqModule.Map<Tuple<K, FSharpOption<a>>, FSharpOption<a>>((FSharpFunc<M0, M1>) new FSeriesextensions.ofOptionalObservations<K, a>(), (IEnumerable<M0>) observations)).SelectOptional<a>(new Func<KeyValuePair<K, OptionalValue<FSharpOption<a>>>, OptionalValue<a>>(new FSeriesextensions.ofOptionalObservations<K, a>().Invoke));
      }
    }

    [Serializable]
    internal sealed class ofObservations<a, b> : FSharpFunc<Tuple<a, b>, a>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ofObservations()
      {
        base.ctor();
      }

      public virtual a Invoke(Tuple<a, b> tupledArg)
      {
        return tupledArg.Item1;
      }
    }

    [Serializable]
    internal sealed class ofObservations<a, b> : FSharpFunc<Tuple<a, b>, b>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ofObservations()
      {
        base.ctor();
      }

      public virtual b Invoke(Tuple<a, b> tupledArg)
      {
        return tupledArg.Item2;
      }
    }

    [Serializable]
    internal sealed class keys<a> : OptimizedClosures.FSharpFunc<int, a, int>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal keys()
      {
        base.ctor();
      }

      public virtual int Invoke(int i, a _arg1)
      {
        return i;
      }
    }

    [Serializable]
    internal sealed class keys<a> : OptimizedClosures.FSharpFunc<int, a?, int> where a : struct
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal keys()
      {
        base.ctor();
      }

      public virtual int Invoke(int i, a? _arg2)
      {
        return i;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ofNullables<a> where a : struct
    {
      internal a Invoke(KeyValuePair<int, a?> _arg3)
      {
        return ((Tuple<int, a?>) Operators.KeyValuePattern<int, a?>((KeyValuePair<M0, M1>) _arg3)).Item2.Value;
      }
    }

    [Serializable]
    internal sealed class ofOptionalObservations<K, a> : FSharpFunc<Tuple<K, FSharpOption<a>>, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ofOptionalObservations()
      {
        base.ctor();
      }

      public virtual K Invoke(Tuple<K, FSharpOption<a>> tupledArg)
      {
        return tupledArg.Item1;
      }
    }

    [Serializable]
    internal sealed class ofOptionalObservations<K, a> : FSharpFunc<Tuple<K, FSharpOption<a>>, FSharpOption<a>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ofOptionalObservations()
      {
        base.ctor();
      }

      public virtual FSharpOption<a> Invoke(Tuple<K, FSharpOption<a>> tupledArg)
      {
        return tupledArg.Item2;
      }
    }

    [Serializable]
    internal sealed class ofOptionalObservations<a> : FSharpFunc<FSharpOption<a>, OptionalValue<a>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ofOptionalObservations()
      {
        base.ctor();
      }

      public virtual OptionalValue<a> Invoke(FSharpOption<a> opt)
      {
        FSharpOption<a> fsharpOption = opt;
        if (fsharpOption == null)
          return OptionalValue<a>.Missing;
        return new OptionalValue<a>(fsharpOption.get_Value());
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ofOptionalObservations<K, a>
    {
      internal OptionalValue<a> Invoke(KeyValuePair<K, OptionalValue<FSharpOption<a>>> kvp)
      {
        FSharpFunc<FSharpOption<a>, OptionalValue<a>> fsharpFunc = (FSharpFunc<FSharpOption<a>, OptionalValue<a>>) new FSeriesextensions.ofOptionalObservations<a>();
        OptionalValue<FSharpOption<a>> optionalValue = kvp.Value;
        if (optionalValue.HasValue)
          return fsharpFunc.Invoke(optionalValue.Value);
        return OptionalValue<a>.Missing;
      }
    }
  }
}
