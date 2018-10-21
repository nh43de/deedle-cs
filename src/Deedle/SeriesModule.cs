// Decompiled with JetBrains decompiler
// Type: Deedle.SeriesModule
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Indices;
using Deedle.Internal;
using Deedle.Vectors;

using Microsoft.FSharp.Control;

using Microsoft.FSharp.Core.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deedle
{
  [CompilationRepresentation]
  
  public static class SeriesModule
  {
    [CompilationSourceName("observations")]
    public static IEnumerable<Tuple<K, T>> GetObservations<K, T>(Series<K, T> series)
    {
      IEnumerable<KeyValuePair<K, long>> mappings = series.Index.Mappings;
      return (IEnumerable<Tuple<K, T>>) new SeriesModule.GetObservations<K, T>((FSharpFunc<long, FSharpFunc<KeyValuePair<K, long>, FSharpOption<Tuple<K, T>>>>) new SeriesModule.GetObservations<K, T>(series), mappings, (FSharpRef<long>) null, new KeyValuePair<K, long>(), (FSharpOption<Tuple<K, T>>) null, (FSharpOption<Tuple<K, T>>) null, (IEnumerator<KeyValuePair<K, long>>) null, 0, (Tuple<K, T>) null);
    }

    [CompilationSourceName("observationsAll")]
    public static IEnumerable<Tuple<K, FSharpOption<T>>> GetAllObservations<K, T>(Series<K, T> series)
    {
      IEnumerable<KeyValuePair<K, long>> mappings = series.Index.Mappings;
      return (IEnumerable<Tuple<K, FSharpOption<T>>>) new SeriesModule.GetAllObservations<K, T>((FSharpFunc<long, FSharpFunc<KeyValuePair<K, long>, Tuple<K, FSharpOption<T>>>>) new SeriesModule.GetAllObservations<K, T>(series), mappings, (FSharpRef<long>) null, new KeyValuePair<K, long>(), (IEnumerator<KeyValuePair<K, long>>) null, 0, (Tuple<K, FSharpOption<T>>) null);
    }

    
    [CompilationSourceName("lookupAll")]
    public static Series<K, T> LookupAll<K, T>(IEnumerable<K> keys, Lookup lookup, Series<K, T> series)
    {
      return series.GetItems(keys, lookup);
    }

    
    [CompilationSourceName("sample")]
    public static Series<K, T> Sample<K, T>(IEnumerable<K> keys, Series<K, T> series)
    {
      return SeriesModule.LookupAll<K, T>(keys, Lookup.ExactOrSmaller, series);
    }

    
    [CompilationSourceName("getAll")]
    public static Series<K, T> GetObservations<K, T>(IEnumerable<K> keys, Series<K, T> series)
    {
      return series.GetItems(keys);
    }

    
    [CompilationSourceName("lookup")]
    public static T Lookup<K, T>(K key, Lookup lookup, Series<K, T> series)
    {
      return series.Get(key, lookup);
    }

    
    [CompilationSourceName("get")]
    public static T Get<K, T>(K key, Series<K, T> series)
    {
      return series.Get(key);
    }

    
    [CompilationSourceName("getAt")]
    public static T GetAt<K, T>(int index, Series<K, T> series)
    {
      return series.GetAt(index);
    }

    
    [CompilationSourceName("tryLookup")]
    public static FSharpOption<T> TryLookup<K, T>(K key, Lookup lookup, Series<K, T> series)
    {
      OptionalValue<T> optionalValue = series.TryGet(key, lookup);
      if (optionalValue.HasValue)
        return FSharpOption<T>.Some(optionalValue.Value);
      return (FSharpOption<T>) null;
    }

    
    [CompilationSourceName("tryLookupObservation")]
    public static FSharpOption<Tuple<K, T>> TryLookupObservation<K, T>(K key, Lookup lookup, Series<K, T> series)
    {
      SeriesModule.TryLookupObservation<K, T> lookupObservation301 = new SeriesModule.TryLookupObservation<K, T>();
      OptionalValue<KeyValuePair<K, T>> observation = series.TryGetObservation(key, lookup);
      FSharpOption<KeyValuePair<K, T>> fsharpOption = !observation.HasValue ? (FSharpOption<KeyValuePair<K, T>>) null : FSharpOption<KeyValuePair<K, T>>.Some(observation.Value);
      return (FSharpOption<Tuple<K, T>>) OptionModule.Map<KeyValuePair<K, T>, Tuple<K, T>>((FSharpFunc<M0, M1>) lookupObservation301, (FSharpOption<M0>) fsharpOption);
    }

    
    [CompilationSourceName("tryGet")]
    public static FSharpOption<T> TryGet<K, T>(K key, Series<K, T> series)
    {
      OptionalValue<T> optionalValue = series.TryGet(key);
      if (optionalValue.HasValue)
        return FSharpOption<T>.Some(optionalValue.Value);
      return (FSharpOption<T>) null;
    }

    
    [CompilationSourceName("tryGetAt")]
    public static FSharpOption<T> TryGetAt<K, T>(int index, Series<K, T> series)
    {
      OptionalValue<T> at = series.TryGetAt(index);
      if (at.HasValue)
        return FSharpOption<T>.Some(at.Value);
      return (FSharpOption<T>) null;
    }

    [CompilationSourceName("countValues")]
    public static int CountValues<K, T>(Series<K, T> series)
    {
      return series.ValueCount;
    }

    [CompilationSourceName("countKeys")]
    public static int CountKeys<K, T>(Series<K, T> series)
    {
      return series.KeyCount;
    }

    
    [CompilationSourceName("hasAll")]
    public static bool HasAll<K, T>(IEnumerable<K> keys, Series<K, T> series)
    {
      return SeqModule.ForAll<K>((FSharpFunc<M0, bool>) new SeriesModule.HasAll<K, T>(series), keys);
    }

    
    [CompilationSourceName("hasSome")]
    public static bool HasSome<K, T>(IEnumerable<K> keys, Series<K, T> series)
    {
      return SeqModule.Exists<K>((FSharpFunc<M0, bool>) new SeriesModule.HasSome<K, T>(series), keys);
    }

    
    [CompilationSourceName("hasNone")]
    public static bool HasNone<K, T>(IEnumerable<K> keys, Series<K, T> series)
    {
      return SeqModule.ForAll<K>((FSharpFunc<M0, bool>) new SeriesModule.HasNone<K, T>(series), keys);
    }

    
    [CompilationSourceName("has")]
    public static bool Has<K, T>(K key, Series<K, T> series)
    {
      return series.TryGet(key).HasValue;
    }

    
    [CompilationSourceName("hasNot")]
    public static bool HasNot<K, T>(K key, Series<K, T> series)
    {
      return !series.TryGet(key).HasValue;
    }

    [CompilationSourceName("lastKey")]
    public static K GetLastKey<K, V>(Series<K, V> series)
    {
      return series.Index.KeyAt(series.Index.AddressAt((long) (series.KeyCount - 1)));
    }

    [CompilationSourceName("firstKey")]
    public static K GetFirstKey<K, V>(Series<K, V> series)
    {
      return series.Index.KeyAt(series.Index.AddressAt(0L));
    }

    [CompilationSourceName("lastValue")]
    public static V GetLastValue<K, V>(Series<K, V> series)
    {
      Series<K, V> series1 = series;
      return SeriesModule.GetAt<K, V>(series.KeyCount - 1, series1);
    }

    [CompilationSourceName("tryLastValue")]
    public static FSharpOption<V> TryGetLastValue<K, V>(Series<K, V> series)
    {
      if (series.KeyCount <= 0)
        return (FSharpOption<V>) null;
      Series<K, V> series1 = series;
      return SeriesModule.TryGetAt<K, V>(series.KeyCount - 1, series1);
    }

    [CompilationSourceName("firstValue")]
    public static V GetFirstValue<K, V>(Series<K, V> series)
    {
      return SeriesModule.GetAt<K, V>(0, series);
    }

    [CompilationSourceName("tryFirstValue")]
    public static FSharpOption<V> TryGetFirstValue<K, V>(Series<K, V> series)
    {
      if (series.KeyCount > 0)
        return FSharpOption<V>.Some(SeriesModule.GetAt<K, V>(0, series));
      return (FSharpOption<V>) null;
    }

    [CompilationSourceName("values")]
    public static IEnumerable<T> GetValues<K, T>(Series<K, T> series)
    {
      return series.Values;
    }

    [CompilationSourceName("valuesAll")]
    public static IEnumerable<FSharpOption<T>> GetAllValues<K, T>(Series<K, T> series)
    {
      return (IEnumerable<FSharpOption<T>>) SeqModule.Map<OptionalValue<T>, FSharpOption<T>>((FSharpFunc<M0, M1>) new SeriesModule.GetAllValues<T>(), (IEnumerable<M0>) FVectorextensionscore.IVector`1get_DataSequence<T>(series.Vector));
    }

    [CompilationSourceName("keys")]
    public static IEnumerable<K> GetKeys<K, T>(Series<K, T> series)
    {
      return series.Keys;
    }

    
    [CompilationSourceName("filter")]
    public static Series<K, T> Filter<K, T>(FSharpFunc<K, FSharpFunc<T, bool>> f, Series<K, T> series)
    {
      return series.Where(new Func<KeyValuePair<K, T>, bool>(new SeriesModule.Filter<K, T>(f).Invoke));
    }

    
    [CompilationSourceName("filterValues")]
    public static Series<K, T> FilterValues<T, K>(FSharpFunc<T, bool> f, Series<K, T> series)
    {
      return series.Where(new Func<KeyValuePair<K, T>, bool>(new SeriesModule.FilterValues<K, T>(f).Invoke));
    }

    
    [CompilationSourceName("filterAll")]
    public static Series<K, T> FilterAll<K, T>(FSharpFunc<K, FSharpFunc<FSharpOption<T>, bool>> f, Series<K, T> series)
    {
      return series.WhereOptional(new Func<KeyValuePair<K, OptionalValue<T>>, bool>(new SeriesModule.FilterAll<K, T>(f).Invoke));
    }

    
    [CompilationSourceName("map")]
    public static Series<K, R> Map<K, T, R>(FSharpFunc<K, FSharpFunc<T, R>> f, Series<K, T> series)
    {
      return series.Select<R>(new Func<KeyValuePair<K, T>, R>(new SeriesModule.Map<K, T, R>(f).Invoke));
    }

    
    [CompilationSourceName("mapValues")]
    public static Series<K, R> MapValues<T, R, K>(FSharpFunc<T, R> f, Series<K, T> series)
    {
      return series.Select<R>(new Func<KeyValuePair<K, T>, R>(new SeriesModule.MapValues<T, R, K>(f).Invoke));
    }

    
    [CompilationSourceName("mapAll")]
    public static Series<K, R> MapAll<K, T, R>(FSharpFunc<K, FSharpFunc<FSharpOption<T>, FSharpOption<R>>> f, Series<K, T> series)
    {
      return series.SelectOptional<R>(new Func<KeyValuePair<K, OptionalValue<T>>, OptionalValue<R>>(new SeriesModule.MapAll<R, K, T>(f).Invoke));
    }

    
    [CompilationSourceName("mapKeys")]
    public static Series<R, T> MapKeys<K, R, T>(FSharpFunc<K, R> f, Series<K, T> series)
    {
      return series.SelectKeys<R>(new Func<KeyValuePair<K, OptionalValue<T>>, R>(new SeriesModule.MapKeys<K, R, T>(f).Invoke));
    }

    
    [CompilationSourceName("convert")]
    public static Series<K, R> Convert<T, R, K>(FSharpFunc<T, R> forward, FSharpFunc<R, T> backward, Series<K, T> series)
    {
      return series.Convert<R>(new Func<T, R>(new SeriesModule.Convert<T, R>(forward).Invoke), new Func<R, T>(new SeriesModule.Convert<T, R>(backward).Invoke));
    }

    [CompilationSourceName("flatten")]
    public static Series<K, T> Flatten<K, T>(Series<K, FSharpOption<T>> series)
    {
      return SeriesModule.MapAll<K, FSharpOption<T>, T>((FSharpFunc<K, FSharpFunc<FSharpOption<FSharpOption<T>>, FSharpOption<T>>>) new SeriesModule.Flatten<K, T>(), series);
    }

    
    [CompilationSourceName("take")]
    public static Series<K, T> Take<K, T>(int count, Series<K, T> series)
    {
      if ((count <= series.KeyCount ? (count < 0 ? 1 : 0) : 1) != 0)
        throw new ArgumentException("Must be greater than zero and less than the number of keys.", nameof (count));
      return series.GetAddressRange(RangeRestriction<long>.NewStart((long) count));
    }

    
    [CompilationSourceName("takeLast")]
    public static Series<K, T> TakeLast<K, T>(int count, Series<K, T> series)
    {
      if ((count <= series.KeyCount ? (count < 0 ? 1 : 0) : 1) != 0)
        throw new ArgumentException("Must be greater than zero and less than the number of keys.", nameof (count));
      return series.GetAddressRange(RangeRestriction<long>.NewEnd((long) count));
    }

    
    [CompilationSourceName("skip")]
    public static Series<K, T> Skip<K, T>(int count, Series<K, T> series)
    {
      if ((count <= series.KeyCount ? (count < 0 ? 1 : 0) : 1) != 0)
        throw new ArgumentException("Must be greater than zero and less than the number of keys.", nameof (count));
      return series.GetAddressRange(RangeRestriction<long>.NewFixed(series.Index.AddressAt((long) count), series.Index.AddressAt((long) (series.KeyCount - 1))));
    }

    
    [CompilationSourceName("skipLast")]
    public static Series<K, T> SkipLast<K, T>(int count, Series<K, T> series)
    {
      if ((count <= series.KeyCount ? (count < 0 ? 1 : 0) : 1) != 0)
        throw new ArgumentException("Must be greater than zero and less than the number of keys.", nameof (count));
      return series.GetAddressRange(RangeRestriction<long>.NewFixed(series.Index.AddressAt(0L), series.Index.AddressAt((long) (series.KeyCount - 1 - count))));
    }

    [CompilationSourceName("force")]
    public static Series<K, V> Force<K, V>(Series<K, V> series)
    {
      return series.Materialize();
    }

    
    [CompilationSourceName("scanValues")]
    public static Series<K, R> ScanValues<R, T, K>(FSharpFunc<R, FSharpFunc<T, R>> foldFunc, R init, Series<K, T> series)
    {
      return series.ScanValues<R>(new Func<R, T, R>(new SeriesModule.ScanValues<R, T>(foldFunc).Invoke), init);
    }

    
    [CompilationSourceName("scanAllValues")]
    public static Series<K, R> ScanAllValues<R, T, K>(FSharpFunc<FSharpOption<R>, FSharpFunc<FSharpOption<T>, FSharpOption<R>>> foldFunc, FSharpOption<R> init, Series<K, T> series)
    {
      FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<T>, OptionalValue<R>>> liftedFunc = (FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<T>, OptionalValue<R>>>) new SeriesModule.liftedFunc<R, T>(foldFunc);
      Series<K, T> series1 = series;
      Func<OptionalValue<R>, OptionalValue<T>, OptionalValue<R>> foldFunc1 = new Func<OptionalValue<R>, OptionalValue<T>, OptionalValue<R>>(new SeriesModule.ScanAllValues<R, T>(liftedFunc).Invoke);
      FSharpOption<R> fsharpOption = init;
      OptionalValue<R> init1 = fsharpOption == null ? OptionalValue<R>.Missing : new OptionalValue<R>(fsharpOption.get_Value());
      return series1.ScanAllValues<R>(foldFunc1, init1);
    }

    
    [CompilationSourceName("reduceValues")]
    public static T ReduceValues<T, K>(FSharpFunc<T, FSharpFunc<T, T>> op, Series<K, T> series)
    {
      VectorData<T> data = series.Vector.Data;
      VectorData<T> vectorData = data;
      if (!(vectorData is VectorData<T>.SparseList))
      {
        if (!(vectorData is VectorData<T>.Sequence))
        {
          ReadOnlyCollection<T> readOnlyCollection1 = ((VectorData<T>.DenseList) data).item;
          FSharpFunc<T, FSharpFunc<T, T>> fsharpFunc = op;
          ReadOnlyCollection<T> readOnlyCollection2 = readOnlyCollection1;
          T obj = readOnlyCollection2[0];
          int index = 1;
          int num = readOnlyCollection2.Count - 1;
          if (num >= index)
          {
            do
            {
              obj = FSharpFunc<T, T>.InvokeFast<T>(fsharpFunc, obj, readOnlyCollection2[index]);
              ++index;
            }
            while (index != num + 1);
          }
          return obj;
        }
        IEnumerable<OptionalValue<T>> optionalValues = ((VectorData<T>.Sequence) data).item;
        return SeqModule.Reduce<T>(op, (IEnumerable<M0>) SeqModule.Choose<OptionalValue<T>, T>((FSharpFunc<M0, FSharpOption<M1>>) new SeriesModule.ReduceValues<T>(), (IEnumerable<M0>) optionalValues));
      }
      ReadOnlyCollection<OptionalValue<T>> readOnlyCollection3 = ((VectorData<T>.SparseList) data).item;
      FSharpFunc<T, FSharpFunc<T, T>> fsharpFunc1 = op;
      ReadOnlyCollection<OptionalValue<T>> readOnlyCollection4 = readOnlyCollection3;
      FSharpOption<T> fsharpOption1 = (FSharpOption<T>) null;
      int index1 = 0;
      int num1 = readOnlyCollection4.Count - 1;
      if (num1 >= index1)
      {
        do
        {
          Tuple<FSharpOption<T>, OptionalValue<T>> tuple = new Tuple<FSharpOption<T>, OptionalValue<T>>(fsharpOption1, readOnlyCollection4[index1]);
          if (tuple.Item1 == null)
          {
            FSharpChoice<Unit, T> fsharpChoice = OptionalValueModule.MissingPresent<T>(tuple.Item2);
            if (fsharpChoice is FSharpChoice<Unit, T>.Choice2Of2)
            {
              fsharpOption1 = FSharpOption<T>.Some(((FSharpChoice<Unit, T>.Choice2Of2) fsharpChoice).get_Item());
              goto label_12;
            }
          }
          else
          {
            FSharpOption<T> fsharpOption2 = tuple.Item1;
            FSharpChoice<Unit, T> fsharpChoice = OptionalValueModule.MissingPresent<T>(tuple.Item2);
            if (fsharpChoice is FSharpChoice<Unit, T>.Choice2Of2)
            {
              T obj1 = ((FSharpChoice<Unit, T>.Choice2Of2) fsharpChoice).get_Item();
              T obj2 = fsharpOption2.get_Value();
              fsharpOption1 = FSharpOption<T>.Some(FSharpFunc<T, T>.InvokeFast<T>(fsharpFunc1, obj2, obj1));
              goto label_12;
            }
          }
label_12:
          ++index1;
        }
        while (index1 != num1 + 1);
      }
      FSharpOption<T> fsharpOption3 = fsharpOption1;
      return (fsharpOption3 == null ? OptionalValue<T>.Missing : new OptionalValue<T>(fsharpOption3.get_Value())).Value;
    }

    
    [CompilationSourceName("foldValues")]
    public static a FoldValues<a, T, K>(FSharpFunc<a, FSharpFunc<T, a>> op, a init, Series<K, T> series)
    {
      VectorData<T> data = series.Vector.Data;
      VectorData<T> vectorData = data;
      if (!(vectorData is VectorData<T>.SparseList))
      {
        if (!(vectorData is VectorData<T>.Sequence))
        {
          ReadOnlyCollection<T> readOnlyCollection1 = ((VectorData<T>.DenseList) data).item;
          FSharpFunc<a, FSharpFunc<T, a>> fsharpFunc = op;
          a a1 = init;
          ReadOnlyCollection<T> readOnlyCollection2 = readOnlyCollection1;
          a a2 = a1;
          int index = 0;
          int num = readOnlyCollection2.Count - 1;
          if (num >= index)
          {
            do
            {
              a2 = FSharpFunc<a, T>.InvokeFast<a>(fsharpFunc, a2, readOnlyCollection2[index]);
              ++index;
            }
            while (index != num + 1);
          }
          return a2;
        }
        IEnumerable<OptionalValue<T>> optionalValues = ((VectorData<T>.Sequence) data).item;
        return (a) SeqModule.Fold<T, a>((FSharpFunc<M1, FSharpFunc<M0, M1>>) op, (M1) init, (IEnumerable<M0>) SeqModule.Choose<OptionalValue<T>, T>((FSharpFunc<M0, FSharpOption<M1>>) new SeriesModule.FoldValues<T>(), (IEnumerable<M0>) optionalValues));
      }
      ReadOnlyCollection<OptionalValue<T>> readOnlyCollection3 = ((VectorData<T>.SparseList) data).item;
      FSharpFunc<a, FSharpFunc<T, a>> fsharpFunc1 = op;
      a a3 = init;
      ReadOnlyCollection<OptionalValue<T>> readOnlyCollection4 = readOnlyCollection3;
      a a4 = a3;
      int index1 = 0;
      int num1 = readOnlyCollection4.Count - 1;
      if (num1 >= index1)
      {
        do
        {
          FSharpChoice<Unit, T> fsharpChoice = OptionalValueModule.MissingPresent<T>(readOnlyCollection4[index1]);
          if (fsharpChoice is FSharpChoice<Unit, T>.Choice2Of2)
          {
            T obj = ((FSharpChoice<Unit, T>.Choice2Of2) fsharpChoice).get_Item();
            a4 = FSharpFunc<a, T>.InvokeFast<a>(fsharpFunc1, a4, obj);
          }
          ++index1;
        }
        while (index1 != num1 + 1);
      }
      return a4;
    }

    
    [CompilationSourceName("diff")]
    public static Series<K, T> Diff<K, T>(int offset, Series<K, T> series)
    {
      IVectorBuilder instance = FVectorBuilderimplementation.VectorBuilder.Instance;
      Tuple<IIndex<K>, VectorConstruction> tuple = series.Index.Builder.Shift<K>(new Tuple<IIndex<K>, VectorConstruction>(series.Index, VectorConstruction.NewReturn(0)), offset);
      VectorConstruction vectorConstruction1 = tuple.Item2;
      IIndex<K> index = tuple.Item1;
      VectorConstruction vectorConstruction2 = series.Index.Builder.Shift<K>(new Tuple<IIndex<K>, VectorConstruction>(series.Index, VectorConstruction.NewReturn(0)), -offset).Item2;
      VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new SeriesModule.cmd<K>(index)), FSharpList<VectorConstruction>.Cons(vectorConstruction2, FSharpList<VectorConstruction>.Cons(vectorConstruction1, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new SeriesModule.cmd<T>((FSharpFunc<OptionalValue<T>, FSharpFunc<OptionalValue<T>, OptionalValue<T>>>) new SeriesModule.cmd<T>((FSharpFunc<T, FSharpFunc<T, T>>) new SeriesModule.cmd<T>()))));
      IVector<T> vector = instance.Build<T>(index.AddressingScheme, vectorConstruction3, new IVector<T>[1]{ series.Vector });
      return new Series<K, T>(index, vector, instance, series.Index.Builder);
    }

    
    [CompilationSourceName("shift")]
    public static Series<K, T> Shift<K, T>(int offset, Series<K, T> series)
    {
      Tuple<IIndex<K>, VectorConstruction> tuple = series.IndexBuilder.Shift<K>(new Tuple<IIndex<K>, VectorConstruction>(series.Index, VectorConstruction.NewReturn(0)), offset);
      VectorConstruction vectorConstruction = tuple.Item2;
      IIndex<K> index = tuple.Item1;
      IVector<T> vector = series.VectorBuilder.Build<T>(index.AddressingScheme, vectorConstruction, new IVector<T>[1]{ series.Vector });
      return new Series<K, T>(index, vector, series.VectorBuilder, series.IndexBuilder);
    }

    
    public static Series<K, TryValue<R>> tryMap<K, T, R>(FSharpFunc<K, FSharpFunc<T, R>> f, Series<K, T> series)
    {
      return series.Select<TryValue<R>>(new Func<KeyValuePair<K, T>, TryValue<R>>(new SeriesModule.tryMap<K, T, R>(f).Invoke));
    }

    public static Series<K, T> tryValues<K, T>(Series<K, TryValue<T>> series)
    {
      FSharpList<Exception> fsharpList = (FSharpList<Exception>) ListModule.OfSeq<Exception>((IEnumerable<M0>) SeqModule.Choose<TryValue<T>, Exception>((FSharpFunc<M0, FSharpOption<M1>>) new SeriesModule.exceptions<T>(), (IEnumerable<M0>) series.Values));
      if (ListModule.IsEmpty<Exception>((FSharpList<M0>) fsharpList))
        return SeriesModule.MapValues<TryValue<T>, T, K>((FSharpFunc<TryValue<T>, T>) new SeriesModule.tryValues<T>(), series);
      throw new AggregateException((IEnumerable<Exception>) fsharpList);
    }

    public static Series<K, Exception> tryErrors<K, V>(Series<K, TryValue<V>> series)
    {
      return new Series<K, Exception>((IEnumerable<KeyValuePair<K, Exception>>) SeqModule.Choose<KeyValuePair<K, TryValue<V>>, KeyValuePair<K, Exception>>((FSharpFunc<M0, FSharpOption<M1>>) new SeriesModule.errors<K, V>(), (IEnumerable<M0>) series.Observations));
    }

    public static Series<K, V> trySuccesses<K, V>(Series<K, TryValue<V>> series)
    {
      return new Series<K, V>((IEnumerable<KeyValuePair<K, V>>) SeqModule.Choose<KeyValuePair<K, TryValue<V>>, KeyValuePair<K, V>>((FSharpFunc<M0, FSharpOption<M1>>) new SeriesModule.successes<K, V>(), (IEnumerable<M0>) series.Observations));
    }

    
    public static Series<K, T> fillErrorsWith<T, K>(T value, Series<K, TryValue<T>> series)
    {
      return SeriesModule.MapValues<TryValue<T>, T, K>((FSharpFunc<TryValue<T>, T>) new SeriesModule.fillErrorsWith<T>(value), series);
    }

    
    [CompilationSourceName("applyLevel")]
    public static Series<K2, R> ApplyLevel<K1, K2, V, R>(FSharpFunc<K1, K2> level, FSharpFunc<Series<K1, V>, R> op, Series<K1, V> series)
    {
      return SeriesModule.MapValues<Series<K1, V>, R, K2>(op, series.GroupBy<K2>(new Func<KeyValuePair<K1, V>, K2>(new SeriesModule.ApplyLevel<K1, K2, V>(level).Invoke)));
    }

    
    [CompilationSourceName("applyLevelOptional")]
    public static Series<K2, R> ApplyLevelOptional<K1, K2, V, R>(FSharpFunc<K1, K2> level, FSharpFunc<Series<K1, V>, FSharpOption<R>> op, Series<K1, V> series)
    {
      return SeriesModule.Flatten<K2, R>(SeriesModule.MapValues<Series<K1, V>, FSharpOption<R>, K2>(op, series.GroupBy<K2>(new Func<KeyValuePair<K1, V>, K2>(new SeriesModule.ApplyLevelOptional<K1, K2, V>(level).Invoke))));
    }

    
    [CompilationSourceName("reduceLevel")]
    public static Series<K2, T> ReduceLevel<K1, K2, T>(FSharpFunc<K1, K2> level, FSharpFunc<T, FSharpFunc<T, T>> op, Series<K1, T> series)
    {
      Series<K2, Series<K1, T>> series1 = series.GroupBy<K2>(new Func<KeyValuePair<K1, T>, K2>(new SeriesModule.ReduceLevel<K1, K2, T>(level).Invoke));
      return SeriesModule.MapValues<Series<K1, T>, T, K2>((FSharpFunc<Series<K1, T>, T>) new SeriesModule.ReduceLevel<K1, T>(op), series1);
    }

    
    [CompilationSourceName("aggregate")]
    public static Series<TNewKey, DataSegment<Series<K, T>>> Aggregate<K, T, TNewKey>(Aggregation<K> aggregation, FSharpFunc<DataSegment<Series<K, T>>, TNewKey> keySelector, Series<K, T> series)
    {
      return series.Aggregate<TNewKey, DataSegment<Series<K, T>>>(aggregation, new Func<DataSegment<Series<K, T>>, TNewKey>(new SeriesModule.Aggregate<K, T, TNewKey>(keySelector).Invoke), new Func<DataSegment<Series<K, T>>, OptionalValue<DataSegment<Series<K, T>>>>(new SeriesModule.Aggregate<K, T>().Invoke));
    }

    
    [CompilationSourceName("aggregateInto")]
    public static Series<TNewKey, R> AggregateInto<K, T, TNewKey, R>(Aggregation<K> aggregation, FSharpFunc<DataSegment<Series<K, T>>, TNewKey> keySelector, FSharpFunc<DataSegment<Series<K, T>>, OptionalValue<R>> f, Series<K, T> series)
    {
      return series.Aggregate<TNewKey, R>(aggregation, new Func<DataSegment<Series<K, T>>, TNewKey>(new SeriesModule.AggregateInto<K, T, TNewKey>(keySelector).Invoke), new Func<DataSegment<Series<K, T>>, OptionalValue<R>>(new SeriesModule.AggregateInto<K, T, R>(f).Invoke));
    }

    
    [CompilationSourceName("windowSizeInto")]
    public static Series<K, R> WindowSizeInto<K, T, R>(int bounds_0, Boundary bounds_1, FSharpFunc<DataSegment<Series<K, T>>, R> f, Series<K, T> series)
    {
      Tuple<int, Boundary> tuple1 = new Tuple<int, Boundary>(bounds_0, bounds_1);
      FSharpTypeFunc fsharpTypeFunc = (FSharpTypeFunc) new SeriesModule.keySel(tuple1.Item2 != Boundary.AtEnding ? Direction.Backward : Direction.Forward);
      Series<K, T> series1 = series;
      Tuple<int, Boundary> tuple2 = tuple1;
      Aggregation<K> aggregation = Aggregation<K>.NewWindowSize(tuple2.Item1, tuple2.Item2);
      Func<DataSegment<Series<K, T>>, K> keySelector = (Func<DataSegment<Series<K, T>>, K>) ((FSharpTypeFunc) fsharpTypeFunc.Specialize<K>()).Specialize<T>();
      Func<DataSegment<Series<K, T>>, OptionalValue<R>> valueSelector = new Func<DataSegment<Series<K, T>>, OptionalValue<R>>(new SeriesModule.WindowSizeInto<K, T, R>(f).Invoke);
      return series1.Aggregate<K, R>(aggregation, keySelector, valueSelector);
    }

    
    [CompilationSourceName("windowSize")]
    public static Series<K, Series<K, T>> WindowSize<K, T>(int bounds_0, Boundary bounds_1, Series<K, T> series)
    {
      Tuple<int, Boundary> tuple = new Tuple<int, Boundary>(bounds_0, bounds_1);
      return SeriesModule.WindowSizeInto<K, T, Series<K, T>>(tuple.Item1, tuple.Item2, (FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>) new SeriesModule.WindowSize<K, T>(), series);
    }

    
    [CompilationSourceName("windowDistInto")]
    public static Series<K, b> WindowDistanceInto<a, K, T, b>(a distance, FSharpFunc<Series<K, T>, b> f, Series<K, T> series)
    {
      return series.Aggregate<K, b>(Aggregation<K>.NewWindowWhile((FSharpFunc<K, FSharpFunc<K, bool>>) new SeriesModule.WindowDistanceInto<a, K>(distance)), new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.WindowDistanceInto<a, K, T>().Invoke), new Func<DataSegment<Series<K, T>>, OptionalValue<b>>(new SeriesModule.WindowDistanceInto<a, K, T, b>(f).Invoke));
    }

    
    [CompilationSourceName("windowDist")]
    public static Series<K, Series<K, T>> WindowDistance<D, K, T>(D distance, Series<K, T> series)
    {
      D distance1 = distance;
      FSharpFunc<Series<K, T>, Series<K, T>> f = (FSharpFunc<Series<K, T>, Series<K, T>>) new SeriesModule.WindowDistance<D, K, T>();
      return series.Aggregate<K, Series<K, T>>(Aggregation<K>.NewWindowWhile((FSharpFunc<K, FSharpFunc<K, bool>>) new SeriesModule.WindowDistance<D, K>(distance1)), new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.WindowDistance<D, K, T>().Invoke), new Func<DataSegment<Series<K, T>>, OptionalValue<Series<K, T>>>(new SeriesModule.WindowDistance<D, K, T>(f).Invoke));
    }

    
    [CompilationSourceName("windowWhileInto")]
    public static Series<K, a> WindowWhileInto<K, T, a>(FSharpFunc<K, FSharpFunc<K, bool>> cond, FSharpFunc<Series<K, T>, a> f, Series<K, T> series)
    {
      return series.Aggregate<K, a>(Aggregation<K>.NewWindowWhile(cond), new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.WindowWhileInto<K, T>().Invoke), new Func<DataSegment<Series<K, T>>, OptionalValue<a>>(new SeriesModule.WindowWhileInto<K, T, a>(f).Invoke));
    }

    
    [CompilationSourceName("windowWhile")]
    public static Series<K, Series<K, T>> WindowWhile<K, T>(FSharpFunc<K, FSharpFunc<K, bool>> cond, Series<K, T> series)
    {
      FSharpFunc<K, FSharpFunc<K, bool>> fsharpFunc = cond;
      FSharpFunc<Series<K, T>, Series<K, T>> f = (FSharpFunc<Series<K, T>, Series<K, T>>) new SeriesModule.WindowWhile<K, T>();
      return series.Aggregate<K, Series<K, T>>(Aggregation<K>.NewWindowWhile(fsharpFunc), new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.WindowWhile<K, T>().Invoke), new Func<DataSegment<Series<K, T>>, OptionalValue<Series<K, T>>>(new SeriesModule.WindowWhile<K, T>(f).Invoke));
    }

    
    [CompilationSourceName("chunkSizeInto")]
    public static Series<K, R> ChunkSizeInto<K, T, R>(int bounds_0, Boundary bounds_1, FSharpFunc<DataSegment<Series<K, T>>, R> f, Series<K, T> series)
    {
      Tuple<int, Boundary> bounds = new Tuple<int, Boundary>(bounds_0, bounds_1);
      Series<K, T> series1 = series;
      Tuple<int, Boundary> tuple = bounds;
      Aggregation<K> aggregation = Aggregation<K>.NewChunkSize(tuple.Item1, tuple.Item2);
      Func<DataSegment<Series<K, T>>, K> keySelector = new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.ChunkSizeInto<K, T>(bounds).Invoke);
      Func<DataSegment<Series<K, T>>, OptionalValue<R>> valueSelector = new Func<DataSegment<Series<K, T>>, OptionalValue<R>>(new SeriesModule.ChunkSizeInto<K, T, R>(f).Invoke);
      return series1.Aggregate<K, R>(aggregation, keySelector, valueSelector);
    }

    
    [CompilationSourceName("chunkSize")]
    public static Series<K, Series<K, T>> ChunkSize<K, T>(int bounds_0, Boundary bounds_1, Series<K, T> series)
    {
      Tuple<int, Boundary> tuple1 = new Tuple<int, Boundary>(bounds_0, bounds_1);
      FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>> f = (FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>) new SeriesModule.ChunkSize<K, T>();
      Series<K, T> series1 = series;
      Tuple<int, Boundary> bounds = new Tuple<int, Boundary>(tuple1.Item1, tuple1.Item2);
      Series<K, T> series2 = series1;
      Tuple<int, Boundary> tuple2 = bounds;
      Aggregation<K> aggregation = Aggregation<K>.NewChunkSize(tuple2.Item1, tuple2.Item2);
      Func<DataSegment<Series<K, T>>, K> keySelector = new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.ChunkSize<K, T>(bounds).Invoke);
      Func<DataSegment<Series<K, T>>, OptionalValue<Series<K, T>>> valueSelector = new Func<DataSegment<Series<K, T>>, OptionalValue<Series<K, T>>>(new SeriesModule.ChunkSize<K, T>(f).Invoke);
      return series2.Aggregate<K, Series<K, T>>(aggregation, keySelector, valueSelector);
    }

    
    [CompilationSourceName("chunkDistInto")]
    public static Series<K, R> ChunkDistanceInto<D, K, T, R>(D distance, FSharpFunc<Series<K, T>, R> f, Series<K, T> series)
    {
      return series.Aggregate<K, R>(Aggregation<K>.NewChunkWhile((FSharpFunc<K, FSharpFunc<K, bool>>) new SeriesModule.ChunkDistanceInto<D, K>(distance)), new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.ChunkDistanceInto<D, K, T>().Invoke), new Func<DataSegment<Series<K, T>>, OptionalValue<R>>(new SeriesModule.ChunkDistanceInto<D, K, T, R>(f).Invoke));
    }

    
    [CompilationSourceName("chunkDist")]
    public static Series<K, Series<K, T>> ChunkDistance<D, K, T>(D distance, Series<K, T> series)
    {
      D distance1 = distance;
      FSharpFunc<Series<K, T>, Series<K, T>> f = (FSharpFunc<Series<K, T>, Series<K, T>>) new SeriesModule.ChunkDistance0<D, K, T>();
      return series.Aggregate<K, Series<K, T>>(Aggregation<K>.NewChunkWhile((FSharpFunc<K, FSharpFunc<K, bool>>) new SeriesModule.ChunkDistance0<D, K>(distance1)), new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.ChunkDistance0<D, K, T>().Invoke), new Func<DataSegment<Series<K, T>>, OptionalValue<Series<K, T>>>(new SeriesModule.ChunkDistance0<D, K, T>(f).Invoke));
    }

    
    [CompilationSourceName("chunkWhileInto")]
    public static Series<K, a> ChunkWhileInto<K, T, a>(FSharpFunc<K, FSharpFunc<K, bool>> cond, FSharpFunc<Series<K, T>, a> f, Series<K, T> series)
    {
      return series.Aggregate<K, a>(Aggregation<K>.NewChunkWhile(cond), new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.ChunkWhileInto8<K, T>().Invoke), new Func<DataSegment<Series<K, T>>, OptionalValue<a>>(new SeriesModule.ChunkWhileInto8<K, T, a>(f).Invoke));
    }

    
    [CompilationSourceName("chunkWhile")]
    public static Series<K, Series<K, T>> ChunkWhile<K, T>(FSharpFunc<K, FSharpFunc<K, bool>> cond, Series<K, T> series)
    {
      FSharpFunc<K, FSharpFunc<K, bool>> fsharpFunc = cond;
      FSharpFunc<Series<K, T>, Series<K, T>> f = (FSharpFunc<Series<K, T>, Series<K, T>>) new SeriesModule.ChunkWhile3<K, T>();
      return series.Aggregate<K, Series<K, T>>(Aggregation<K>.NewChunkWhile(fsharpFunc), new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.ChunkWhile3<K, T>().Invoke), new Func<DataSegment<Series<K, T>>, OptionalValue<Series<K, T>>>(new SeriesModule.ChunkWhile3<K, T>(f).Invoke));
    }

    
    [CompilationSourceName("windowInto")]
    public static Series<K, R> WindowInto<K, T, R>(int size, FSharpFunc<Series<K, T>, R> f, Series<K, T> series)
    {
      return SeriesModule.WindowSizeInto<K, T, R>(size, Boundary.Skip, (FSharpFunc<DataSegment<Series<K, T>>, R>) new SeriesModule.WindowInto9<K, T, R>((FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>) new SeriesModule.WindowInto9<K, T>(), f), series);
    }

    
    [CompilationSourceName("window")]
    public static Series<K, Series<K, T>> Window<K, T>(int size, Series<K, T> series)
    {
      int num = size;
      Boundary boundary = Boundary.Skip;
      Series<K, T> series1 = series;
      Tuple<int, Boundary> tuple = new Tuple<int, Boundary>(num, boundary);
      return SeriesModule.WindowSizeInto<K, T, Series<K, T>>(tuple.Item1, tuple.Item2, (FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>) new SeriesModule.Window2<K, T>(), series1);
    }

    
    [CompilationSourceName("chunkInto")]
    public static Series<K, R> ChunkInto<K, T, R>(int size, FSharpFunc<Series<K, T>, R> f, Series<K, T> series)
    {
      int num = size;
      Boundary boundary = Boundary.Skip;
      FSharpFunc<DataSegment<Series<K, T>>, R> f1 = (FSharpFunc<DataSegment<Series<K, T>>, R>) new SeriesModule.ChunkInto6<K, T, R>((FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>) new SeriesModule.ChunkInto6<K, T>(), f);
      Series<K, T> series1 = series;
      Tuple<int, Boundary> bounds = new Tuple<int, Boundary>(num, boundary);
      Series<K, T> series2 = series1;
      Tuple<int, Boundary> tuple = bounds;
      Aggregation<K> aggregation = Aggregation<K>.NewChunkSize(tuple.Item1, tuple.Item2);
      Func<DataSegment<Series<K, T>>, K> keySelector = new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.ChunkInto6<K, T>(bounds).Invoke);
      Func<DataSegment<Series<K, T>>, OptionalValue<R>> valueSelector = new Func<DataSegment<Series<K, T>>, OptionalValue<R>>(new SeriesModule.ChunkInto6<K, T, R>(f1).Invoke);
      return series2.Aggregate<K, R>(aggregation, keySelector, valueSelector);
    }

    
    [CompilationSourceName("chunk")]
    public static Series<K, Series<K, T>> Chunk<K, T>(int size, Series<K, T> series)
    {
      int num = size;
      Boundary boundary = Boundary.Skip;
      Series<K, T> series1 = series;
      Tuple<int, Boundary> tuple1 = new Tuple<int, Boundary>(num, boundary);
      FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>> f = (FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>) new SeriesModule.Chunk9<K, T>();
      Series<K, T> series2 = series1;
      Tuple<int, Boundary> bounds = new Tuple<int, Boundary>(tuple1.Item1, tuple1.Item2);
      Series<K, T> series3 = series2;
      Tuple<int, Boundary> tuple2 = bounds;
      Aggregation<K> aggregation = Aggregation<K>.NewChunkSize(tuple2.Item1, tuple2.Item2);
      Func<DataSegment<Series<K, T>>, K> keySelector = new Func<DataSegment<Series<K, T>>, K>(new SeriesModule.Chunk9<K, T>(bounds).Invoke);
      Func<DataSegment<Series<K, T>>, OptionalValue<Series<K, T>>> valueSelector = new Func<DataSegment<Series<K, T>>, OptionalValue<Series<K, T>>>(new SeriesModule.Chunk9<K, T>(f).Invoke);
      return series3.Aggregate<K, Series<K, T>>(aggregation, keySelector, valueSelector);
    }

    [CompilationSourceName("pairwise")]
    public static Series<K, Tuple<T, T>> Pairwise<K, T>(Series<K, T> series)
    {
      return series.Pairwise();
    }

    
    [CompilationSourceName("pairwiseWith")]
    public static Series<K, a> PairwiseWith<K, T, a>(FSharpFunc<K, FSharpFunc<Tuple<T, T>, a>> f, Series<K, T> series)
    {
      return SeriesModule.Map<K, Tuple<T, T>, a>((FSharpFunc<K, FSharpFunc<Tuple<T, T>, a>>) new SeriesModule.PairwiseWith2<K, T, a>(f), series.Pairwise());
    }

    
    public static Series<TNewKey, TNewValue> groupInto<K, T, TNewKey, TNewValue>(FSharpFunc<K, FSharpFunc<T, TNewKey>> keySelector, FSharpFunc<TNewKey, FSharpFunc<Series<K, T>, TNewValue>> f, Series<K, T> series)
    {
      return SeriesModule.Map<TNewKey, Series<K, T>, TNewValue>((FSharpFunc<TNewKey, FSharpFunc<Series<K, T>, TNewValue>>) new SeriesModule.groupInto8<K, T, TNewKey, TNewValue>(f), series.GroupBy<TNewKey>(new Func<KeyValuePair<K, T>, TNewKey>(new SeriesModule.groupInto8<K, T, TNewKey>(keySelector).Invoke)));
    }

    
    public static Series<TNewKey, Series<K, T>> groupBy<K, T, TNewKey>(FSharpFunc<K, FSharpFunc<T, TNewKey>> keySelector, Series<K, T> series)
    {
      return SeriesModule.groupInto<K, T, TNewKey, Series<K, T>>(keySelector, (FSharpFunc<TNewKey, FSharpFunc<Series<K, T>, Series<K, T>>>) new SeriesModule.groupBy1<K, T, TNewKey>(), series);
    }

    
    [CompilationSourceName("withMissingFrom")]
    public static Series<K, T> WithMissingFrom<K, S, T>(Series<K, S> other, Series<K, T> series)
    {
      return series.WithMissingFrom<S>(other);
    }

    [CompilationSourceName("dropMissing")]
    public static Series<K, T> DropMissing<K, T>(Series<K, T> series)
    {
      return series.WhereOptional(new Func<KeyValuePair<K, OptionalValue<T>>, bool>(new SeriesModule.DropMissing2<K, T>().Invoke));
    }

    
    [CompilationSourceName("fillMissingUsing")]
    public static Series<K, T> FillMissingUsing<K, T>(FSharpFunc<K, T> f, Series<K, T> series)
    {
      return SeriesModule.MapAll<K, T, T>((FSharpFunc<K, FSharpFunc<FSharpOption<T>, FSharpOption<T>>>) new SeriesModule.FillMissingUsing1<K, T>(f), series);
    }

    
    [CompilationSourceName("fillMissingWith")]
    public static Series<K, T> FillMissingWith<a, K, T>(a value, Series<K, T> series)
    {
      VectorConstruction vectorConstruction = VectorConstruction.NewFillMissing(VectorConstruction.NewReturn(0), VectorFillMissing.NewConstant((object) value));
      IVector<T> vector = series.VectorBuilder.Build<T>(series.Index.AddressingScheme, vectorConstruction, new IVector<T>[1]{ series.Vector });
      return new Series<K, T>(series.Index, vector, series.VectorBuilder, series.IndexBuilder);
    }

    
    [CompilationSourceName("fillMissing")]
    public static Series<K, T> FillMissing<K, T>(Direction direction, Series<K, T> series)
    {
      VectorConstruction vectorConstruction = VectorConstruction.NewFillMissing(VectorConstruction.NewReturn(0), VectorFillMissing.NewDirection(direction));
      IVector<T> vector = series.VectorBuilder.Build<T>(series.Index.AddressingScheme, vectorConstruction, new IVector<T>[1]{ series.Vector });
      return new Series<K, T>(series.Index, vector, series.VectorBuilder, series.IndexBuilder);
    }

    
    [CompilationSourceName("fillMissingBetween")]
    public static Series<K, T> FillMissingBetween<K, T>(K startKey, K endKey, Direction direction, Series<K, T> series)
    {
      Series<K, T> otherSeries = SeriesModule.FillMissing<K, T>(direction, series.GetSlice(FSharpOption<K>.Some(startKey), FSharpOption<K>.Some(endKey)));
      return series.Zip<T>(otherSeries, JoinKind.Left).SelectOptional<T>(new Func<KeyValuePair<K, OptionalValue<Tuple<OptionalValue<T>, OptionalValue<T>>>>, OptionalValue<T>>(new SeriesModule.FillMissingBetween2<K, T>().Invoke));
    }

    
    [CompilationSourceName("fillMissingInside")]
    public static Series<K, T> FillMissingInside<K, T>(Direction direction, Series<K, T> series)
    {
      if (!series.IsOrdered)
        throw new InvalidOperationException("Series must be sorted to use fillMissingInside");
      FSharpOption<Tuple<KeyValuePair<K, T>, KeyValuePair<K, T>>> fsharpOption1 = Seq.tryFirstAndLast<KeyValuePair<K, T>>(series.Observations);
      if (fsharpOption1 != null)
      {
        FSharpOption<Tuple<KeyValuePair<K, T>, KeyValuePair<K, T>>> fsharpOption2 = fsharpOption1;
        KeyValuePair<K, T> keyValuePair1 = fsharpOption2.get_Value().Item2;
        if (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<KeyValuePair<K, T>>((M0) fsharpOption2.get_Value().Item1, (M0) keyValuePair1))
        {
          KeyValuePair<K, T> keyValuePair2 = fsharpOption2.get_Value().Item2;
          KeyValuePair<K, T> keyValuePair3 = fsharpOption2.get_Value().Item1;
          Series<K, T> series1 = series;
          return SeriesModule.FillMissingBetween<K, T>(keyValuePair3.Key, keyValuePair2.Key, direction, series1);
        }
      }
      return series;
    }

    
    internal static Tuple<IIndex<K>, VectorConstruction> sortWithCommand<T, K>(FSharpFunc<T, FSharpFunc<T, int>> compareFunc, Series<K, T> series)
    {
      IIndex<K> index1 = series.Index;
      IVector<T> vector = series.Vector;
      FSharpFunc<long, FSharpFunc<long, int>> missingCompare = (FSharpFunc<long, FSharpFunc<long, int>>) new SeriesModule.missingCompare8<T>(compareFunc, vector);
      KeyValuePair<K, long>[] keyValuePairArray1 = (KeyValuePair<K, long>[]) ArrayModule.OfSeq<KeyValuePair<K, long>>((IEnumerable<M0>) index1.Mappings);
      ArrayModule.SortInPlaceWith<KeyValuePair<K, long>>((FSharpFunc<M0, FSharpFunc<M0, int>>) new SeriesModule.sortWithCommand7<K>(missingCompare), (M0[]) keyValuePairArray1);
      KeyValuePair<K, long>[] keyValuePairArray2 = keyValuePairArray1;
      FSharpFunc<KeyValuePair<K, long>, K> fsharpFunc1 = (FSharpFunc<KeyValuePair<K, long>, K>) new SeriesModule.newKeys8<K>();
      KeyValuePair<K, long>[] keyValuePairArray3 = keyValuePairArray2;
      if ((object) keyValuePairArray3 == null)
        throw new ArgumentNullException("array");
      K[] kArray = new K[keyValuePairArray3.Length];
      for (int index2 = 0; index2 < kArray.Length; ++index2)
        kArray[index2] = fsharpFunc1.Invoke(keyValuePairArray3[index2]);
      K[] keys = kArray;
      KeyValuePair<K, long>[] keyValuePairArray4 = keyValuePairArray1;
      FSharpFunc<KeyValuePair<K, long>, long> fsharpFunc2 = (FSharpFunc<KeyValuePair<K, long>, long>) new SeriesModule.newLocs9<K>();
      KeyValuePair<K, long>[] keyValuePairArray5 = keyValuePairArray4;
      if ((object) keyValuePairArray5 == null)
        throw new ArgumentNullException("array");
      long[] numArray1 = new long[keyValuePairArray5.Length];
      for (int index2 = 0; index2 < numArray1.Length; ++index2)
        numArray1[index2] = fsharpFunc2.Invoke(keyValuePairArray5[index2]);
      long[] numArray2 = numArray1;
      IIndex<K> objectArg = FIndexextensions.Index.ofKeys<K>(keys);
      long length = (long) keys.Length;
      SeriesModule.reordering3<K> reordering1313 = new SeriesModule.reordering3<K>(objectArg);
      long lo = 0;
      long hi = length - 1L;
      object obj = lo > hi ? (object) new SeriesModule.reordering3(lo, hi, 1L, (FSharpFunc<long, FSharpFunc<long, bool>>) new SeriesModule.reordering3()) : (object) new SeriesModule.reordering3(lo, hi, 1L, (FSharpFunc<long, FSharpFunc<long, bool>>) new SeriesModule.reordering3());
      IEnumerable<Tuple<long, long>> tuples = (IEnumerable<Tuple<long, long>>) SeqModule.Zip<long, long>((IEnumerable<M0>) SeqModule.Map<long, long>((FSharpFunc<M0, M1>) reordering1313, (IEnumerable<M0>) obj), (IEnumerable<M1>) numArray2);
      return new Tuple<IIndex<K>, VectorConstruction>(objectArg, VectorConstruction.NewRelocate(VectorConstruction.NewReturn(0), length, tuples));
    }

    
    internal static Tuple<IIndex<K>, VectorConstruction> sortByCommand<T, V, K>(FSharpFunc<T, V> f, Series<K, T> series)
    {
      return SeriesModule.sortWithCommand<V, K>((FSharpFunc<V, FSharpFunc<V, int>>) new SeriesModule.sortByCommand1<V>(), new Series<K, V>(series.Index, series.Vector.Select<V>((FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<T>, OptionalValue<V>>>) new SeriesModule.fseries0<T, V>(f)), series.VectorBuilder, series.IndexBuilder));
    }

    
    [CompilationSourceName("sortWith")]
    public static Series<K, V> SortWith<V, K>(FSharpFunc<V, FSharpFunc<V, int>> comparer, Series<K, V> series)
    {
      Tuple<IIndex<K>, VectorConstruction> tuple = SeriesModule.sortWithCommand<V, K>(comparer, series);
      IIndex<K> index = tuple.Item1;
      VectorConstruction vectorConstruction = tuple.Item2;
      IVector<V> vector = series.VectorBuilder.Build<V>(index.AddressingScheme, vectorConstruction, new IVector<V>[1]{ series.Vector });
      return new Series<K, V>(index, vector, series.VectorBuilder, series.IndexBuilder);
    }

    
    [CompilationSourceName("sortBy")]
    public static Series<K, T> SortBy<T, V, K>(FSharpFunc<T, V> proj, Series<K, T> series)
    {
      Tuple<IIndex<K>, VectorConstruction> tuple = SeriesModule.sortByCommand<T, V, K>(proj, series);
      IIndex<K> index = tuple.Item1;
      VectorConstruction vectorConstruction = tuple.Item2;
      IVector<T> vector = series.VectorBuilder.Build<T>(index.AddressingScheme, vectorConstruction, new IVector<T>[1]{ series.Vector });
      return new Series<K, T>(index, vector, series.VectorBuilder, series.IndexBuilder);
    }

    [CompilationSourceName("sortByKey")]
    public static Series<K, T> SortByKey<K, T>(Series<K, T> series)
    {
      Tuple<IIndex<K>, VectorConstruction> tuple = series.IndexBuilder.OrderIndex<K>(new Tuple<IIndex<K>, VectorConstruction>(series.Index, VectorConstruction.NewReturn(0)));
      VectorConstruction vectorConstruction = tuple.Item2;
      IIndex<K> index = tuple.Item1;
      IVector<T> vector = series.VectorBuilder.Build<T>(index.AddressingScheme, vectorConstruction, new IVector<T>[1]{ series.Vector });
      return new Series<K, T>(index, vector, series.VectorBuilder, series.IndexBuilder);
    }

    [CompilationSourceName("sort")]
    public static Series<K, V> Sort<K, V>(Series<K, V> series)
    {
      return SeriesModule.SortWith<V, K>((FSharpFunc<V, FSharpFunc<V, int>>) new SeriesModule.Sort2<V>(), series);
    }

    [CompilationSourceName("rev")]
    public static Series<K, T> Reverse<K, T>(Series<K, T> series)
    {
      return series.Reversed;
    }

    
    [CompilationSourceName("realign")]
    public static Series<K, T> Realign<K, T>(IEnumerable<K> keys, Series<K, T> series)
    {
      return series.Realign(keys);
    }

    [CompilationSourceName("indexOrdinally")]
    public static Series<int, T> IndexOrdinally<K, T>(Series<K, T> series)
    {
      return series.IndexOrdinally();
    }

    
    [CompilationSourceName("indexWith")]
    public static Series<K2, T> IndexWith<K2, K1, T>(IEnumerable<K2> keys, Series<K1, T> series)
    {
      return series.IndexWith<K2>(keys);
    }

    
    public static Series<K, a> resampleInto<K, V, a>(IEnumerable<K> keys, Direction dir, FSharpFunc<K, FSharpFunc<Series<K, V>, a>> f, Series<K, V> series)
    {
      return series.Resample<a>(keys, dir, new Func<K, Series<K, V>, a>(new SeriesModule.resampleInto3<K, V, a>(f).Invoke));
    }

    
    public static Series<K, Series<K, V>> resample<K, V>(IEnumerable<K> keys, Direction dir, Series<K, V> series)
    {
      return SeriesModule.resampleInto<K, V, Series<K, V>>(keys, dir, (FSharpFunc<K, FSharpFunc<Series<K, V>, Series<K, V>>>) new SeriesModule.resample4<K, V>(), series);
    }

    
    public static Series<K2, V2> resampleEquivInto<K1, K2, V1, V2>(FSharpFunc<K1, K2> keyProj, FSharpFunc<Series<K1, V1>, V2> f, Series<K1, V1> series)
    {
      return SeriesModule.MapValues<Series<K1, V1>, V2, K2>(f, SeriesModule.MapKeys<K1, K2, Series<K1, V1>>(keyProj, series.Aggregate<K1, Series<K1, V1>>(Aggregation<K1>.NewChunkWhile((FSharpFunc<K1, FSharpFunc<K1, bool>>) new SeriesModule.resampleEquivInto9<K1, K2>(keyProj)), new Func<DataSegment<Series<K1, V1>>, K1>(new SeriesModule.resampleEquivInto9<K1, V1>().Invoke), new Func<DataSegment<Series<K1, V1>>, OptionalValue<Series<K1, V1>>>(new SeriesModule.resampleEquivInto9<K1, V1>((FSharpFunc<Series<K1, V1>, Series<K1, V1>>) new SeriesModule.resampleEquivInto9<K1, V1>()).Invoke))));
    }

    
    public static Series<K2, Series<K1, V1>> resampleEquiv<K1, K2, V1>(FSharpFunc<K1, K2> keyProj, Series<K1, V1> series)
    {
      FSharpFunc<K1, K2> fsharpFunc = keyProj;
      FSharpFunc<Series<K1, V1>, Series<K1, V1>> f = (FSharpFunc<Series<K1, V1>, Series<K1, V1>>) new SeriesModule.resampleEquiv3<K1, V1>();
      Series<K1, V1> series1 = series;
      return SeriesModule.MapValues<Series<K1, V1>, Series<K1, V1>, K2>(f, SeriesModule.MapKeys<K1, K2, Series<K1, V1>>(fsharpFunc, series1.Aggregate<K1, Series<K1, V1>>(Aggregation<K1>.NewChunkWhile((FSharpFunc<K1, FSharpFunc<K1, bool>>) new SeriesModule.resampleEquiv3<K1, K2>(fsharpFunc)), new Func<DataSegment<Series<K1, V1>>, K1>(new SeriesModule.resampleEquiv3<K1, V1>().Invoke), new Func<DataSegment<Series<K1, V1>>, OptionalValue<Series<K1, V1>>>(new SeriesModule.resampleEquiv3<K1, V1>((FSharpFunc<Series<K1, V1>, Series<K1, V1>>) new SeriesModule.resampleEquiv3<K1, V1>()).Invoke))));
    }

    
    public static Series<K2, a> resampleUniformInto<K1, K2, V, a>(Lookup fillMode, FSharpFunc<K1, K2> keyProj, FSharpFunc<K2, K2> nextKey, FSharpFunc<Series<K1, V>, a> f, Series<K1, V> series)
    {
      if (!fillMode.HasFlag((Enum) Lookup.Exact))
        throw new InvalidOperationException("resampleUniformInto: The value of 'fillMode' must include 'Exact'.");
      Tuple<K1, K1> keyRange = series.KeyRange;
      K1 k1_1 = keyRange.Item1;
      K1 k1_2 = keyRange.Item2;
      Tuple<K2, K2> tuple = new Tuple<K2, K2>(keyProj.Invoke(k1_1), keyProj.Invoke(k1_2));
      K2 k2 = tuple.Item1;
      K2 max = tuple.Item2;
      Series<K2, Series<K1, V>> series1 = SeriesModule.Realign<K2, Series<K1, V>>(SeqModule.Unfold<K2, K2>((FSharpFunc<M0, FSharpOption<Tuple<M1, M0>>>) new SeriesModule.keys0<K2>(nextKey, max), (M0) k2), SeriesModule.MapKeys<K1, K2, Series<K1, V>>(keyProj, series.Aggregate<K1, Series<K1, V>>(Aggregation<K1>.NewChunkWhile((FSharpFunc<K1, FSharpFunc<K1, bool>>) new SeriesModule.reindexed6<K1, K2>(keyProj)), new Func<DataSegment<Series<K1, V>>, K1>(new SeriesModule.reindexed6<K1, V>().Invoke), new Func<DataSegment<Series<K1, V>>, OptionalValue<Series<K1, V>>>(new SeriesModule.reindexed6<K1, V>((FSharpFunc<Series<K1, V>, Series<K1, V>>) new SeriesModule.reindexed6<K1, V>()).Invoke))));
      return SeriesModule.MapValues<Series<K1, V>, a, K2>(f, SeriesModule.FillMissingUsing<K2, Series<K1, V>>((FSharpFunc<K2, Series<K1, V>>) new SeriesModule.resampleUniformInto2<K1, K2, V>(fillMode, series1), series1));
    }

    
    public static Series<K2, Series<K1, V>> resampleUniform<K1, K2, V>(Lookup fillMode, FSharpFunc<K1, K2> keyProj, FSharpFunc<K2, K2> nextKey, Series<K1, V> series)
    {
      return SeriesModule.resampleUniformInto<K1, K2, V, Series<K1, V>>(fillMode, keyProj, nextKey, (FSharpFunc<Series<K1, V>, Series<K1, V>>) new SeriesModule.resampleUniform8<K1, V>(), series);
    }

    
    public static Series<K, a> sampleTimeInto<K, V, a>(TimeSpan interval, Direction dir, FSharpFunc<Series<K, V>, a> f, Series<K, V> series)
    {
      return SeriesModule.Implementation.sampleTimeIntoInternal<K, TimeSpan, V, a>((FSharpFunc<K, FSharpFunc<TimeSpan, K>>) new SeriesModule.add9<K>(), (FSharpOption<K>) null, interval, dir, (FSharpFunc<K, FSharpFunc<Series<K, V>, a>>) new SeriesModule.sampleTimeInto0<K, V, a>(f), series);
    }

    
    public static Series<K, a> sampleTimeAtInto<K, V, a>(K start, TimeSpan interval, Direction dir, FSharpFunc<Series<K, V>, a> f, Series<K, V> series)
    {
      return SeriesModule.Implementation.sampleTimeIntoInternal<K, TimeSpan, V, a>((FSharpFunc<K, FSharpFunc<TimeSpan, K>>) new SeriesModule.add2<K>(), FSharpOption<K>.Some(start), interval, dir, (FSharpFunc<K, FSharpFunc<Series<K, V>, a>>) new SeriesModule.sampleTimeAtInto3<K, V, a>(f), series);
    }

    
    public static Series<a, Series<a, b>> sampleTime<a, b>(TimeSpan interval, Direction dir, Series<a, b> series)
    {
      return SeriesModule.Implementation.sampleTimeIntoInternal<a, TimeSpan, b, Series<a, b>>((FSharpFunc<a, FSharpFunc<TimeSpan, a>>) new SeriesModule.sampleTime2<a>(), (FSharpOption<a>) null, interval, dir, (FSharpFunc<a, FSharpFunc<Series<a, b>, Series<a, b>>>) new SeriesModule.sampleTime2<a, b>((FSharpFunc<Series<a, b>, Series<a, b>>) new SeriesModule.sampleTime2<a, b>()), series);
    }

    
    public static Series<a, Series<a, b>> sampleTimeAt<a, b>(a start, TimeSpan interval, Direction dir, Series<a, b> series)
    {
      return SeriesModule.Implementation.sampleTimeIntoInternal<a, TimeSpan, b, Series<a, b>>((FSharpFunc<a, FSharpFunc<TimeSpan, a>>) new SeriesModule.sampleTimeAt2<a>(), FSharpOption<a>.Some(start), interval, dir, (FSharpFunc<a, FSharpFunc<Series<a, b>, Series<a, b>>>) new SeriesModule.sampleTimeAt2<a, b>((FSharpFunc<Series<a, b>, Series<a, b>>) new SeriesModule.sampleTimeAt2<a, b>()), series);
    }

    
    public static Series<K, V> lookupTime<K, V>(TimeSpan interval, Direction dir, Lookup lookup, Series<K, V> series)
    {
      return SeriesModule.Implementation.lookupTimeInternal<K, TimeSpan, V>((FSharpFunc<K, FSharpFunc<TimeSpan, K>>) new SeriesModule.add7<K>(), (FSharpOption<K>) null, interval, dir, lookup, series);
    }

    
    public static Series<K, V> lookupTimeAt<K, V>(K start, TimeSpan interval, Direction dir, Lookup lookup, Series<K, V> series)
    {
      return SeriesModule.Implementation.lookupTimeInternal<K, TimeSpan, V>((FSharpFunc<K, FSharpFunc<TimeSpan, K>>) new SeriesModule.add4<K>(), FSharpOption<K>.Some(start), interval, dir, lookup, series);
    }

    
    [CompilationSourceName("merge")]
    public static Series<K, V> Merge<K, V>(Series<K, V> series1, Series<K, V> series2)
    {
      return series1.Merge(series2);
    }

    
    [CompilationSourceName("replace")]
    public static Series<K, V> Replace<K, V>(K key, V value, Series<K, V> series)
    {
      return series.Replace(key, value);
    }

    
    [CompilationSourceName("replaceArray")]
    public static Series<K, V> replaceArray<K, V>(K[] keys, V value, Series<K, V> series)
    {
      return series.Replace(keys, value);
    }

    
    [CompilationSourceName("intersect")]
    public static Series<K, T> Intersect<K, T>(Series<K, T> s1, Series<K, T> s2)
    {
      return s1.Intersect(s2);
    }

    
    [CompilationSourceName("compare")]
    public static Series<K, Diff<T>> Compare<K, T>(Series<K, T> s1, Series<K, T> s2)
    {
      return s1.Compare(s2);
    }

    
    [CompilationSourceName("mergeUsing")]
    public static Series<K, V> MergeUsing<K, V>(UnionBehavior behavior, Series<K, V> series1, Series<K, V> series2)
    {
      return series1.Merge(series2, behavior);
    }

    [CompilationSourceName("mergeAll")]
    public static Series<K, V> MergeAll<K, V>(IEnumerable<Series<K, V>> series)
    {
      if (SeqModule.IsEmpty<Series<K, V>>((IEnumerable<M0>) series))
        return new Series<K, V>((IEnumerable<K>) FSharpList<K>.get_Empty(), (IEnumerable<V>) FSharpList<V>.get_Empty());
      Series<K, V>[] seriesArray1 = (Series<K, V>[]) ArrayModule.OfSeq<Series<K, V>>((IEnumerable<M0>) series);
      Series<K, V> series1 = seriesArray1[0];
      Series<K, V>[] seriesArray2 = seriesArray1;
      FSharpOption<int> fsharpOption1 = FSharpOption<int>.Some(1);
      FSharpOption<int> fsharpOption2 = (FSharpOption<int>) null;
      int length1 = seriesArray2.Length;
      Tuple<int, int> tuple1;
      if (fsharpOption1 == null)
      {
        if (fsharpOption2 != null)
        {
          FSharpOption<int> fsharpOption3 = fsharpOption2;
          if (fsharpOption3.get_Value() >= 0)
          {
            tuple1 = new Tuple<int, int>(0, fsharpOption3.get_Value());
            goto label_14;
          }
        }
        else
        {
          tuple1 = new Tuple<int, int>(0, 0 + length1 - 1);
          goto label_14;
        }
      }
      if (fsharpOption1 != null)
      {
        FSharpOption<int> fsharpOption3 = fsharpOption1;
        if (fsharpOption2 == null && fsharpOption3.get_Value() <= 0 + length1)
        {
          tuple1 = new Tuple<int, int>(fsharpOption3.get_Value(), 0 + length1 - 1);
          goto label_14;
        }
      }
      if (fsharpOption1 != null)
      {
        FSharpOption<int> fsharpOption3 = fsharpOption1;
        if (fsharpOption2 != null)
        {
          int num = fsharpOption2.get_Value();
          tuple1 = new Tuple<int, int>(fsharpOption3.get_Value(), num);
          goto label_14;
        }
      }
      throw new IndexOutOfRangeException();
label_14:
      Tuple<int, int> tuple2 = tuple1;
      int num1 = tuple2.Item1;
      int num2 = tuple2.Item2 - num1 + 1;
      int length2 = num2 >= 0 ? num2 : 0;
      Series<K, V>[] seriesArray3 = new Series<K, V>[length2];
      Series<K, V> series2 = series1;
      int index = 0;
      int num3 = length2 - 1;
      if (num3 >= index)
      {
        do
        {
          seriesArray3[index] = seriesArray2[num1 + index];
          ++index;
        }
        while (index != num3 + 1);
      }
      return series2.Merge(seriesArray3);
    }

    
    [CompilationSourceName("zip")]
    public static Series<K, Tuple<OptionalValue<V1>, OptionalValue<V2>>> Zip<K, V1, V2>(Series<K, V1> series1, Series<K, V2> series2)
    {
      return series1.Zip<V2>(series2);
    }

    
    [CompilationSourceName("zipAlign")]
    public static Series<K, Tuple<OptionalValue<V1>, OptionalValue<V2>>> ZipAlign<K, V1, V2>(JoinKind kind, Lookup lookup, Series<K, V1> series1, Series<K, V2> series2)
    {
      return series1.Zip<V2>(series2, kind, lookup);
    }

    
    [CompilationSourceName("zipInner")]
    public static Series<K, Tuple<V1, V2>> ZipInner<K, V1, V2>(Series<K, V1> series1, Series<K, V2> series2)
    {
      return series1.ZipInner<V2>(series2);
    }

    
    [CompilationSourceName("zipAlignInto")]
    public static Series<K, R> ZipAlignInto<V1, V2, R, K>(JoinKind kind, Lookup lookup, FSharpFunc<FSharpOption<V1>, FSharpFunc<FSharpOption<V2>, FSharpOption<R>>> op, Series<K, V1> series1, Series<K, V2> series2)
    {
      return series1.Zip<V2>(series2, kind, lookup).SelectOptional<R>(new Func<KeyValuePair<K, OptionalValue<Tuple<OptionalValue<V1>, OptionalValue<V2>>>>, OptionalValue<R>>(new SeriesModule.ZipAlignInto3<V1, V2, R, K>(op).Invoke));
    }

    
    [CompilationSourceName("zipInto")]
    public static Series<K, R> ZipInto<V1, V2, R, K>(FSharpFunc<V1, FSharpFunc<V2, R>> op, Series<K, V1> series1, Series<K, V2> series2)
    {
      return SeriesModule.ZipAlignInto<V1, V2, R, K>(JoinKind.Inner, Lookup.Exact, (FSharpFunc<FSharpOption<V1>, FSharpFunc<FSharpOption<V2>, FSharpOption<R>>>) new SeriesModule.ZipInto0<V1, V2, R>(op), series1, series2);
    }

    [Serializable]
    internal sealed class GetObservations<K, T> : FSharpFunc<T, Tuple<K, T>>
    {
      public KeyValuePair<K, long> kvp;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal GetObservations(KeyValuePair<K, long> kvp)
      {
        this.ctor();
        this.kvp = kvp;
      }

      public virtual Tuple<K, T> Invoke(T v)
      {
        return new Tuple<K, T>(this.kvp.Key, v);
      }
    }

    [Serializable]
    internal sealed class GetObservations<K, T> : OptimizedClosures.FSharpFunc<long, KeyValuePair<K, long>, FSharpOption<Tuple<K, T>>>
    {
      public Series<K, T> series;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal GetObservations(Series<K, T> series)
      {
        this.ctor();
        this.series = series;
      }

      public virtual FSharpOption<Tuple<K, T>> Invoke(long idx, KeyValuePair<K, long> kvp)
      {
        OptionalValue<T> valueAtLocation = this.series.Vector.GetValueAtLocation((IVectorLocation) new KnownLocation(kvp.Value, idx));
        FSharpFunc<T, Tuple<K, T>> fsharpFunc = (FSharpFunc<T, Tuple<K, T>>) new SeriesModule.GetObservations<K, T>(kvp);
        OptionalValue<T> optionalValue1 = valueAtLocation;
        OptionalValue<Tuple<K, T>> optionalValue2 = !optionalValue1.HasValue ? OptionalValue<Tuple<K, T>>.Missing : new OptionalValue<Tuple<K, T>>(fsharpFunc.Invoke(optionalValue1.Value));
        if (optionalValue2.HasValue)
          return FSharpOption<Tuple<K, T>>.Some(optionalValue2.Value);
        return (FSharpOption<Tuple<K, T>>) null;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class GetObservations<K, T> : GeneratedSequenceBase<Tuple<K, T>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<long, FSharpFunc<KeyValuePair<K, long>, FSharpOption<Tuple<K, T>>>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerable<KeyValuePair<K, long>> input;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpRef<long> i;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public KeyValuePair<K, long> v;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpOption<Tuple<K, T>> matchValue;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpOption<Tuple<K, T>> unionCase;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<KeyValuePair<K, long>> @enum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<K, T> current;

      public GetObservations(FSharpFunc<long, FSharpFunc<KeyValuePair<K, long>, FSharpOption<Tuple<K, T>>>> f, IEnumerable<KeyValuePair<K, long>> input, FSharpRef<long> i, KeyValuePair<K, long> v, FSharpOption<Tuple<K, T>> matchValue, FSharpOption<Tuple<K, T>> unionCase, IEnumerator<KeyValuePair<K, long>> @enum, int pc, Tuple<K, T> current)
      {
        this.f = f;
        this.input = input;
        this.i = i;
        this.v = v;
        this.matchValue = matchValue;
        this.unionCase = unionCase;
        this.@enum = @enum;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<Tuple<K, T>> next)
      {
        switch (this.pc)
        {
          case 1:
label_7:
            this.pc = 3;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<KeyValuePair<K, long>>>((M0) this.@enum);
            this.@enum = (IEnumerator<KeyValuePair<K, long>>) null;
            this.i = (FSharpRef<long>) null;
            this.pc = 3;
            goto case 3;
          case 2:
            this.unionCase = (FSharpOption<Tuple<K, T>>) null;
            goto label_6;
          case 3:
            this.current = (Tuple<K, T>) null;
            return 0;
          default:
            this.i = (FSharpRef<long>) Operators.Ref<long>((M0) 0L);
            this.@enum = this.input.GetEnumerator();
            this.pc = 1;
            break;
        }
label_2:
        if (this.@enum.MoveNext())
        {
          this.v = this.@enum.Current;
          this.matchValue = (FSharpOption<Tuple<K, T>>) FSharpFunc<long, KeyValuePair<K, long>>.InvokeFast<FSharpOption<Tuple<K, T>>>((FSharpFunc<long, FSharpFunc<KeyValuePair<K, long>, M0>>) this.f, this.i.get_Value(), this.v);
          if (this.matchValue != null)
          {
            this.unionCase = this.matchValue;
            this.pc = 2;
            this.current = this.unionCase.get_Value();
            return 1;
          }
        }
        else
          goto label_7;
label_6:
        this.matchValue = (FSharpOption<Tuple<K, T>>) null;
        Operators.op_ColonEquals<long>((FSharpRef<M0>) this.i, (M0) (Operators.op_Dereference<long>((FSharpRef<M0>) this.i) + 1L));
        this.v = new KeyValuePair<K, long>();
        goto label_2;
      }

      public virtual void Close()
      {
        Exception exception = (Exception) null;
        while (true)
        {
          switch (this.pc)
          {
            case 3:
              goto label_7;
            default:
              Unit unit;
              try
              {
                switch (this.pc)
                {
                  case 0:
                  case 3:
                    this.pc = 3;
                    this.current = (Tuple<K, T>) null;
                    unit = (Unit) null;
                    break;
                  default:
                    this.pc = 3;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<KeyValuePair<K, long>>>((M0) this.@enum);
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
          case 3:
            return false;
          case 2:
            return true;
          default:
            return true;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual Tuple<K, T> get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<Tuple<K, T>> GetFreshEnumerator()
      {
        return (IEnumerator<Tuple<K, T>>) new SeriesModule.GetObservations<K, T>(this.f, this.input, (FSharpRef<long>) null, new KeyValuePair<K, long>(), (FSharpOption<Tuple<K, T>>) null, (FSharpOption<Tuple<K, T>>) null, (IEnumerator<KeyValuePair<K, long>>) null, 0, (Tuple<K, T>) null);
      }
    }

    [Serializable]
    internal sealed class GetAllObservations<K, T> : OptimizedClosures.FSharpFunc<long, KeyValuePair<K, long>, Tuple<K, FSharpOption<T>>>
    {
      public Series<K, T> series;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal GetAllObservations(Series<K, T> series)
      {
        this.ctor();
        this.series = series;
      }

      public virtual Tuple<K, FSharpOption<T>> Invoke(long idx, KeyValuePair<K, long> kvp)
      {
        K key = kvp.Key;
        OptionalValue<T> valueAtLocation = this.series.Vector.GetValueAtLocation((IVectorLocation) new KnownLocation(kvp.Value, idx));
        FSharpOption<T> fsharpOption = !valueAtLocation.HasValue ? (FSharpOption<T>) null : FSharpOption<T>.Some(valueAtLocation.Value);
        return new Tuple<K, FSharpOption<T>>(key, fsharpOption);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class GetAllObservations<K, T> : GeneratedSequenceBase<Tuple<K, FSharpOption<T>>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<long, FSharpFunc<KeyValuePair<K, long>, Tuple<K, FSharpOption<T>>>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerable<KeyValuePair<K, long>> input;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpRef<long> i;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public KeyValuePair<K, long> v;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<KeyValuePair<K, long>> @enum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<K, FSharpOption<T>> current;

      public GetAllObservations(FSharpFunc<long, FSharpFunc<KeyValuePair<K, long>, Tuple<K, FSharpOption<T>>>> f, IEnumerable<KeyValuePair<K, long>> input, FSharpRef<long> i, KeyValuePair<K, long> v, IEnumerator<KeyValuePair<K, long>> @enum, int pc, Tuple<K, FSharpOption<T>> current)
      {
        this.f = f;
        this.input = input;
        this.i = i;
        this.v = v;
        this.@enum = @enum;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<Tuple<K, FSharpOption<T>>> next)
      {
        switch (this.pc)
        {
          case 1:
label_5:
            this.pc = 3;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<KeyValuePair<K, long>>>((M0) this.@enum);
            this.@enum = (IEnumerator<KeyValuePair<K, long>>) null;
            this.i = (FSharpRef<long>) null;
            this.pc = 3;
            goto case 3;
          case 2:
            Operators.op_ColonEquals<long>((FSharpRef<M0>) this.i, (M0) (Operators.op_Dereference<long>((FSharpRef<M0>) this.i) + 1L));
            this.v = new KeyValuePair<K, long>();
            break;
          case 3:
            this.current = (Tuple<K, FSharpOption<T>>) null;
            return 0;
          default:
            this.i = (FSharpRef<long>) Operators.Ref<long>((M0) 0L);
            this.@enum = this.input.GetEnumerator();
            this.pc = 1;
            break;
        }
        if (this.@enum.MoveNext())
        {
          this.v = this.@enum.Current;
          this.pc = 2;
          this.current = (Tuple<K, FSharpOption<T>>) FSharpFunc<long, KeyValuePair<K, long>>.InvokeFast<Tuple<K, FSharpOption<T>>>((FSharpFunc<long, FSharpFunc<KeyValuePair<K, long>, M0>>) this.f, this.i.get_Value(), this.v);
          return 1;
        }
        goto label_5;
      }

      public virtual void Close()
      {
        Exception exception = (Exception) null;
        while (true)
        {
          switch (this.pc)
          {
            case 3:
              goto label_7;
            default:
              Unit unit;
              try
              {
                switch (this.pc)
                {
                  case 0:
                  case 3:
                    this.pc = 3;
                    this.current = (Tuple<K, FSharpOption<T>>) null;
                    unit = (Unit) null;
                    break;
                  default:
                    this.pc = 3;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<KeyValuePair<K, long>>>((M0) this.@enum);
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
          case 3:
            return false;
          case 2:
            return true;
          default:
            return true;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual Tuple<K, FSharpOption<T>> get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<Tuple<K, FSharpOption<T>>> GetFreshEnumerator()
      {
        return (IEnumerator<Tuple<K, FSharpOption<T>>>) new SeriesModule.GetAllObservations<K, T>(this.f, this.input, (FSharpRef<long>) null, new KeyValuePair<K, long>(), (IEnumerator<KeyValuePair<K, long>>) null, 0, (Tuple<K, FSharpOption<T>>) null);
      }
    }

    [Serializable]
    internal sealed class TryLookupObservation<K, T> : FSharpFunc<KeyValuePair<K, T>, Tuple<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal TryLookupObservation()
      {
        base.ctor();
      }

      public virtual Tuple<K, T> Invoke(KeyValuePair<K, T> kvp)
      {
        return new Tuple<K, T>(kvp.Key, kvp.Value);
      }
    }

    [Serializable]
    internal sealed class HasAll<K, T> : FSharpFunc<K, bool>
    {
      public Series<K, T> series;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal HasAll(Series<K, T> series)
      {
        this.ctor();
        this.series = series;
      }

      public virtual bool Invoke(K k)
      {
        return this.series.TryGet(k).HasValue;
      }
    }

    [Serializable]
    internal sealed class HasSome<K, T> : FSharpFunc<K, bool>
    {
      public Series<K, T> series;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal HasSome(Series<K, T> series)
      {
        this.ctor();
        this.series = series;
      }

      public virtual bool Invoke(K k)
      {
        return this.series.TryGet(k).HasValue;
      }
    }

    [Serializable]
    internal sealed class HasNone<K, T> : FSharpFunc<K, bool>
    {
      public Series<K, T> series;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal HasNone(Series<K, T> series)
      {
        this.ctor();
        this.series = series;
      }

      public virtual bool Invoke(K k)
      {
        return !this.series.TryGet(k).HasValue;
      }
    }

    [Serializable]
    internal sealed class GetAllValues<T> : FSharpFunc<OptionalValue<T>, FSharpOption<T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal GetAllValues()
      {
        base.ctor();
      }

      public virtual FSharpOption<T> Invoke(OptionalValue<T> value)
      {
        if (value.HasValue)
          return FSharpOption<T>.Some(value.Value);
        return (FSharpOption<T>) null;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class Filter<K, T>
    {
      public FSharpFunc<K, FSharpFunc<T, bool>> f;

      public Filter(FSharpFunc<K, FSharpFunc<T, bool>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal bool Invoke(KeyValuePair<K, T> _arg1)
      {
        Tuple<K, T> tuple = (Tuple<K, T>) Operators.KeyValuePattern<K, T>((KeyValuePair<M0, M1>) _arg1);
        T obj = tuple.Item2;
        return (bool) FSharpFunc<K, T>.InvokeFast<bool>((FSharpFunc<K, FSharpFunc<T, M0>>) this.f, tuple.Item1, obj);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class FilterValues<K, T>
    {
      public FSharpFunc<T, bool> f;

      public FilterValues(FSharpFunc<T, bool> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal bool Invoke(KeyValuePair<K, T> _arg1)
      {
        Tuple<K, T> tuple = (Tuple<K, T>) Operators.KeyValuePattern<K, T>((KeyValuePair<M0, M1>) _arg1);
        T obj = tuple.Item2;
        K k = tuple.Item1;
        return this.f.Invoke(obj);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class FilterAll<K, T>
    {
      public FSharpFunc<K, FSharpFunc<FSharpOption<T>, bool>> f;

      public FilterAll(FSharpFunc<K, FSharpFunc<FSharpOption<T>, bool>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal bool Invoke(KeyValuePair<K, OptionalValue<T>> kvp)
      {
        FSharpFunc<K, FSharpFunc<FSharpOption<T>, bool>> f = this.f;
        K key = kvp.Key;
        OptionalValue<T> optionalValue = kvp.Value;
        FSharpOption<T> fsharpOption = !optionalValue.HasValue ? (FSharpOption<T>) null : FSharpOption<T>.Some(optionalValue.Value);
        return (bool) FSharpFunc<K, FSharpOption<T>>.InvokeFast<bool>((FSharpFunc<K, FSharpFunc<FSharpOption<T>, M0>>) f, key, fsharpOption);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class Map<K, T, R>
    {
      public FSharpFunc<K, FSharpFunc<T, R>> f;

      public Map(FSharpFunc<K, FSharpFunc<T, R>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal R Invoke(KeyValuePair<K, T> _arg1)
      {
        Tuple<K, T> tuple = (Tuple<K, T>) Operators.KeyValuePattern<K, T>((KeyValuePair<M0, M1>) _arg1);
        T obj = tuple.Item2;
        return (R) FSharpFunc<K, T>.InvokeFast<R>((FSharpFunc<K, FSharpFunc<T, M0>>) this.f, tuple.Item1, obj);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class MapValues<T, R, K>
    {
      public FSharpFunc<T, R> f;

      public MapValues(FSharpFunc<T, R> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal R Invoke(KeyValuePair<K, T> _arg1)
      {
        Tuple<K, T> tuple = (Tuple<K, T>) Operators.KeyValuePattern<K, T>((KeyValuePair<M0, M1>) _arg1);
        T obj = tuple.Item2;
        K k = tuple.Item1;
        return this.f.Invoke(obj);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class MapAll<R, K, T>
    {
      public FSharpFunc<K, FSharpFunc<FSharpOption<T>, FSharpOption<R>>> f;

      public MapAll(FSharpFunc<K, FSharpFunc<FSharpOption<T>, FSharpOption<R>>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<R> Invoke(KeyValuePair<K, OptionalValue<T>> kvp)
      {
        FSharpFunc<K, FSharpFunc<FSharpOption<T>, FSharpOption<R>>> f = this.f;
        K key = kvp.Key;
        OptionalValue<T> optionalValue = kvp.Value;
        FSharpOption<T> fsharpOption1 = !optionalValue.HasValue ? (FSharpOption<T>) null : FSharpOption<T>.Some(optionalValue.Value);
        FSharpOption<R> fsharpOption2 = (FSharpOption<R>) FSharpFunc<K, FSharpOption<T>>.InvokeFast<FSharpOption<R>>((FSharpFunc<K, FSharpFunc<FSharpOption<T>, M0>>) f, key, fsharpOption1);
        if (fsharpOption2 == null)
          return OptionalValue<R>.Missing;
        return new OptionalValue<R>(fsharpOption2.get_Value());
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class MapKeys<K, R, T>
    {
      public FSharpFunc<K, R> f;

      public MapKeys(FSharpFunc<K, R> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal R Invoke(KeyValuePair<K, OptionalValue<T>> kvp)
      {
        return this.f.Invoke(kvp.Key);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class Convert<T, R>
    {
      public FSharpFunc<T, R> forward;

      public Convert(FSharpFunc<T, R> forward)
      {
        this.forward = forward;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal R Invoke(T delegateArg0)
      {
        return this.forward.Invoke(delegateArg0);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class Convert<T, R>
    {
      public FSharpFunc<R, T> backward;

      public Convert(FSharpFunc<R, T> backward)
      {
        this.backward = backward;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal T Invoke(R delegateArg0)
      {
        return this.backward.Invoke(delegateArg0);
      }
    }

    [Serializable]
    internal sealed class Flatten<K, T> : OptimizedClosures.FSharpFunc<K, FSharpOption<FSharpOption<T>>, FSharpOption<T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Flatten()
      {
        base.ctor();
      }

      public virtual FSharpOption<T> Invoke(K _arg1, FSharpOption<FSharpOption<T>> v)
      {
        return v?.get_Value();
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ScanValues<R, T>
    {
      public FSharpFunc<R, FSharpFunc<T, R>> foldFunc;

      public ScanValues(FSharpFunc<R, FSharpFunc<T, R>> foldFunc)
      {
        this.foldFunc = foldFunc;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal R Invoke(R delegateArg0, T delegateArg1)
      {
        return (R) FSharpFunc<R, T>.InvokeFast<R>((FSharpFunc<R, FSharpFunc<T, M0>>) this.foldFunc, delegateArg0, delegateArg1);
      }
    }

    [Serializable]
    internal sealed class liftedFunc<R, T> : OptimizedClosures.FSharpFunc<OptionalValue<R>, OptionalValue<T>, OptionalValue<R>>
    {
      public FSharpFunc<FSharpOption<R>, FSharpFunc<FSharpOption<T>, FSharpOption<R>>> foldFunc;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal liftedFunc(FSharpFunc<FSharpOption<R>, FSharpFunc<FSharpOption<T>, FSharpOption<R>>> foldFunc)
      {
        this.ctor();
        this.foldFunc = foldFunc;
      }

      public virtual OptionalValue<R> Invoke(OptionalValue<R> a, OptionalValue<T> b)
      {
        FSharpFunc<FSharpOption<R>, FSharpFunc<FSharpOption<T>, FSharpOption<R>>> foldFunc = this.foldFunc;
        OptionalValue<R> optionalValue1 = a;
        FSharpOption<R> fsharpOption1 = !optionalValue1.HasValue ? (FSharpOption<R>) null : FSharpOption<R>.Some(optionalValue1.Value);
        OptionalValue<T> optionalValue2 = b;
        FSharpOption<T> fsharpOption2 = !optionalValue2.HasValue ? (FSharpOption<T>) null : FSharpOption<T>.Some(optionalValue2.Value);
        FSharpOption<R> fsharpOption3 = (FSharpOption<R>) FSharpFunc<FSharpOption<R>, FSharpOption<T>>.InvokeFast<FSharpOption<R>>((FSharpFunc<FSharpOption<R>, FSharpFunc<FSharpOption<T>, M0>>) foldFunc, fsharpOption1, fsharpOption2);
        if (fsharpOption3 == null)
          return OptionalValue<R>.Missing;
        return new OptionalValue<R>(fsharpOption3.get_Value());
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ScanAllValues<R, T>
    {
      public FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<T>, OptionalValue<R>>> liftedFunc;

      public ScanAllValues(FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<T>, OptionalValue<R>>> liftedFunc)
      {
        this.liftedFunc = liftedFunc;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<R> Invoke(OptionalValue<R> delegateArg0, OptionalValue<T> delegateArg1)
      {
        return (OptionalValue<R>) FSharpFunc<OptionalValue<R>, OptionalValue<T>>.InvokeFast<OptionalValue<R>>((FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<T>, M0>>) this.liftedFunc, delegateArg0, delegateArg1);
      }
    }

    [Serializable]
    internal sealed class ReduceValues<T> : FSharpFunc<OptionalValue<T>, FSharpOption<T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ReduceValues()
      {
        base.ctor();
      }

      public virtual FSharpOption<T> Invoke(OptionalValue<T> value)
      {
        if (value.HasValue)
          return FSharpOption<T>.Some(value.Value);
        return (FSharpOption<T>) null;
      }
    }

    [Serializable]
    internal sealed class FoldValues<T> : FSharpFunc<OptionalValue<T>, FSharpOption<T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal FoldValues()
      {
        base.ctor();
      }

      public virtual FSharpOption<T> Invoke(OptionalValue<T> value)
      {
        if (value.HasValue)
          return FSharpOption<T>.Some(value.Value);
        return (FSharpOption<T>) null;
      }
    }

    [Serializable]
    internal sealed class cmd<K> : FSharpFunc<Unit, long>
    {
      public IIndex<K> newIndex;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal cmd(IIndex<K> newIndex)
      {
        this.ctor();
        this.newIndex = newIndex;
      }

      public virtual long Invoke(Unit unitVar)
      {
        return this.newIndex.KeyCount;
      }
    }

    [Serializable]
    internal sealed class cmd<T> : OptimizedClosures.FSharpFunc<T, T, T>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal cmd()
      {
        base.ctor();
      }

      public virtual T Invoke(T x, T y)
      {
        throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
      }
    }

    [Serializable]
    internal sealed class cmd<T> : OptimizedClosures.FSharpFunc<OptionalValue<T>, OptionalValue<T>, OptionalValue<T>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, T>> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal cmd(FSharpFunc<T, FSharpFunc<T, T>> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual OptionalValue<T> Invoke(OptionalValue<T> input1, OptionalValue<T> input2)
      {
        if ((!input1.HasValue ? 0 : (input2.HasValue ? 1 : 0)) != 0)
          return new OptionalValue<T>((T) FSharpFunc<T, T>.InvokeFast<T>((FSharpFunc<T, FSharpFunc<T, M0>>) this.f, input1.Value, input2.Value));
        return OptionalValue<T>.Missing;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class cmd<T> : IBinaryTransform
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<OptionalValue<T>, FSharpFunc<OptionalValue<T>, OptionalValue<T>>> operation;

      public cmd(FSharpFunc<OptionalValue<T>, FSharpFunc<OptionalValue<T>, OptionalValue<T>>> operation)
      {
        this.operation = operation;
        // ISSUE: explicit constructor call
        base.ctor();
        SeriesModule.cmd<T> cmd6574 = this;
      }

      FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>> IBinaryTransform.GetFunction<R>()
      {
        return (FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>>) LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>>>((object) this.operation);
      }

      bool IBinaryTransform.get_IsMissingUnit()
      {
        return false;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class tryMap<K, T, R>
    {
      public FSharpFunc<K, FSharpFunc<T, R>> f;

      public tryMap(FSharpFunc<K, FSharpFunc<T, R>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal TryValue<R> Invoke(KeyValuePair<K, T> _arg1)
      {
        Tuple<K, T> tuple = (Tuple<K, T>) Operators.KeyValuePattern<K, T>((KeyValuePair<M0, M1>) _arg1);
        T obj = tuple.Item2;
        K k = tuple.Item1;
        try
        {
          return TryValue<R>.NewSuccess((R) FSharpFunc<K, T>.InvokeFast<R>((FSharpFunc<K, FSharpFunc<T, M0>>) this.f, k, obj));
        }
        catch (object ex)
        {
          return TryValue<R>.NewError((Exception) ex);
        }
      }
    }

    [Serializable]
    internal sealed class exceptions<T> : FSharpFunc<TryValue<T>, FSharpOption<Exception>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal exceptions()
      {
        base.ctor();
      }

      public virtual FSharpOption<Exception> Invoke(TryValue<T> tv)
      {
        if (tv.HasValue)
          return (FSharpOption<Exception>) null;
        return FSharpOption<Exception>.Some(tv.Exception);
      }
    }

    [Serializable]
    internal sealed class tryValues<T> : FSharpFunc<TryValue<T>, T>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal tryValues()
      {
        base.ctor();
      }

      public virtual T Invoke(TryValue<T> tv)
      {
        return tv.Value;
      }
    }

    [Serializable]
    internal sealed class errors<K, V> : FSharpFunc<KeyValuePair<K, TryValue<V>>, FSharpOption<KeyValuePair<K, Exception>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal errors()
      {
        base.ctor();
      }

      public virtual FSharpOption<KeyValuePair<K, Exception>> Invoke(KeyValuePair<K, TryValue<V>> _arg1)
      {
        Tuple<K, TryValue<V>> tuple = (Tuple<K, TryValue<V>>) Operators.KeyValuePattern<K, TryValue<V>>((KeyValuePair<M0, M1>) _arg1);
        if (!(tuple.Item2 is TryValue<V>.Error))
          return (FSharpOption<KeyValuePair<K, Exception>>) null;
        TryValue<V>.Error error = (TryValue<V>.Error) tuple.Item2;
        return FSharpOption<KeyValuePair<K, Exception>>.Some(new KeyValuePair<K, Exception>(tuple.Item1, error.item));
      }
    }

    [Serializable]
    internal sealed class successes<K, V> : FSharpFunc<KeyValuePair<K, TryValue<V>>, FSharpOption<KeyValuePair<K, V>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal successes()
      {
        base.ctor();
      }

      public virtual FSharpOption<KeyValuePair<K, V>> Invoke(KeyValuePair<K, TryValue<V>> _arg1)
      {
        Tuple<K, TryValue<V>> tuple = (Tuple<K, TryValue<V>>) Operators.KeyValuePattern<K, TryValue<V>>((KeyValuePair<M0, M1>) _arg1);
        if (!(tuple.Item2 is TryValue<V>.Success))
          return (FSharpOption<KeyValuePair<K, V>>) null;
        V v = ((TryValue<V>.Success) tuple.Item2).item;
        return FSharpOption<KeyValuePair<K, V>>.Some(new KeyValuePair<K, V>(tuple.Item1, v));
      }
    }

    [Serializable]
    internal sealed class fillErrorsWith<T> : FSharpFunc<TryValue<T>, T>
    {
      public T value;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal fillErrorsWith(T value)
      {
        this.ctor();
        this.value = value;
      }

      public virtual T Invoke(TryValue<T> _arg1)
      {
        TryValue<T> tryValue = _arg1;
        if (tryValue is TryValue<T>.Success)
          return ((TryValue<T>.Success) tryValue).item;
        return this.value;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ApplyLevel<K1, K2, V>
    {
      public FSharpFunc<K1, K2> level;

      public ApplyLevel(FSharpFunc<K1, K2> level)
      {
        this.level = level;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal K2 Invoke(KeyValuePair<K1, V> kvp)
      {
        return this.level.Invoke(kvp.Key);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ApplyLevelOptional<K1, K2, V>
    {
      public FSharpFunc<K1, K2> level;

      public ApplyLevelOptional(FSharpFunc<K1, K2> level)
      {
        this.level = level;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal K2 Invoke(KeyValuePair<K1, V> kvp)
      {
        return this.level.Invoke(kvp.Key);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ReduceLevel<K1, K2, T>
    {
      public FSharpFunc<K1, K2> level;

      public ReduceLevel(FSharpFunc<K1, K2> level)
      {
        this.level = level;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal K2 Invoke(KeyValuePair<K1, T> _arg1)
      {
        return this.level.Invoke(((Tuple<K1, T>) Operators.KeyValuePattern<K1, T>((KeyValuePair<M0, M1>) _arg1)).Item1);
      }
    }

    [Serializable]
    internal sealed class ReduceLevel<K1, T> : FSharpFunc<Series<K1, T>, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, T>> op;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ReduceLevel(FSharpFunc<T, FSharpFunc<T, T>> op)
      {
        this.ctor();
        this.op = op;
      }

      public virtual T Invoke(Series<K1, T> series)
      {
        return SeriesModule.ReduceValues<T, K1>(this.op, series);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class Aggregate<K, T, TNewKey>
    {
      public FSharpFunc<DataSegment<Series<K, T>>, TNewKey> keySelector;

      public Aggregate(FSharpFunc<DataSegment<Series<K, T>>, TNewKey> keySelector)
      {
        this.keySelector = keySelector;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal TNewKey Invoke(DataSegment<Series<K, T>> delegateArg0)
      {
        return this.keySelector.Invoke(delegateArg0);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class Aggregate<K, T>
    {
      internal OptionalValue<DataSegment<Series<K, T>>> Invoke(DataSegment<Series<K, T>> v)
      {
        return new OptionalValue<DataSegment<Series<K, T>>>(v);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class AggregateInto<K, T, TNewKey>
    {
      public FSharpFunc<DataSegment<Series<K, T>>, TNewKey> keySelector;

      public AggregateInto(FSharpFunc<DataSegment<Series<K, T>>, TNewKey> keySelector)
      {
        this.keySelector = keySelector;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal TNewKey Invoke(DataSegment<Series<K, T>> delegateArg0)
      {
        return this.keySelector.Invoke(delegateArg0);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class AggregateInto<K, T, R>
    {
      public FSharpFunc<DataSegment<Series<K, T>>, OptionalValue<R>> f;

      public AggregateInto(FSharpFunc<DataSegment<Series<K, T>>, OptionalValue<R>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<R> Invoke(DataSegment<Series<K, T>> delegateArg0)
      {
        return this.f.Invoke(delegateArg0);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class keySel<a, b>
    {
      public Direction dir;

      public keySel(Direction dir)
      {
        this.dir = dir;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal a Invoke(DataSegment<Series<a, b>> data)
      {
        if (this.dir == Direction.Backward)
          return data.Data.Index.KeyRange.Item2;
        return data.Data.Index.KeyRange.Item1;
      }
    }

    [Serializable]
    internal sealed class keySel : FSharpTypeFunc
    {
      public Direction dir;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal keySel(Direction dir)
      {
        this.ctor();
        this.dir = dir;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual object Specialize<a>()
      {
        return (object) (FSharpTypeFunc) new SeriesModule.keySelT<a>(this.dir, this);
      }
    }

    [Serializable]
    internal sealed class keySelT<a> : FSharpTypeFunc
    {
      public Direction dir;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public SeriesModule.keySel self1;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal keySelT(Direction dir, SeriesModule.keySel _param2)
      {
        this.ctor();
        this.dir = dir;
        this.self1 = _param2;
      }

      public virtual object Specialize<b>()
      {
        return (object) new Func<DataSegment<Series<a, b>>, a>(new SeriesModule.keySel<a, b>(this.self1.dir).Invoke);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class WindowSizeInto<K, T, R>
    {
      public FSharpFunc<DataSegment<Series<K, T>>, R> f;

      public WindowSizeInto(FSharpFunc<DataSegment<Series<K, T>>, R> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<R> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<R>(this.f.Invoke(ds));
      }
    }

    [Serializable]
    internal sealed class WindowSize<K, T> : FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowSize()
      {
        base.ctor();
      }

      public virtual Series<K, T> Invoke(DataSegment<Series<K, T>> ds)
      {
        return DataSegment.GetData<Series<K, T>>(ds);
      }
    }

    [Serializable]
    internal sealed class WindowDistanceInto<a, K> : OptimizedClosures.FSharpFunc<K, K, bool>
    {
      public a distance;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowDistanceInto(a distance)
      {
        this.ctor();
        this.distance = distance;
      }

      public virtual bool Invoke(K skey, K ekey)
      {
        if (!true)
          return LanguagePrimitives.HashCompare.GenericLessThanIntrinsic<a>((M0) null, (M0) this.distance);
        throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class WindowDistanceInto<a, K, T>
    {
      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class WindowDistanceInto<a, K, T, b>
    {
      public FSharpFunc<Series<K, T>, b> f;

      public WindowDistanceInto(FSharpFunc<Series<K, T>, b> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<b> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<b>(this.f.Invoke(ds.Data));
      }
    }

    [Serializable]
    internal sealed class WindowDistance<D, K, T> : FSharpFunc<Series<K, T>, Series<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowDistance()
      {
        base.ctor();
      }

      public virtual Series<K, T> Invoke(Series<K, T> x)
      {
        return (Series<K, T>) Operators.Identity<Series<K, T>>((M0) x);
      }
    }

    [Serializable]
    internal sealed class WindowDistance<D, K> : OptimizedClosures.FSharpFunc<K, K, bool>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public D distance;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowDistance(D distance)
      {
        this.ctor();
        this.distance = distance;
      }

      public virtual bool Invoke(K skey, K ekey)
      {
        if (!true)
          return LanguagePrimitives.HashCompare.GenericLessThanIntrinsic<D>((M0) null, (M0) this.distance);
        throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class WindowDistance<D, K, T>
    {
      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class WindowDistance<D, K, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<K, T>, Series<K, T>> f;

      public WindowDistance(FSharpFunc<Series<K, T>, Series<K, T>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<Series<K, T>> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<Series<K, T>>(this.f.Invoke(ds.Data));
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class WindowWhileInto<K, T>
    {
      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class WindowWhileInto<K, T, a>
    {
      public FSharpFunc<Series<K, T>, a> f;

      public WindowWhileInto(FSharpFunc<Series<K, T>, a> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<a> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<a>(this.f.Invoke(ds.Data));
      }
    }

    [Serializable]
    internal sealed class WindowWhile<K, T> : FSharpFunc<Series<K, T>, Series<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowWhile()
      {
        base.ctor();
      }

      public virtual Series<K, T> Invoke(Series<K, T> x)
      {
        return (Series<K, T>) Operators.Identity<Series<K, T>>((M0) x);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class WindowWhile<K, T>
    {
      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class WindowWhile<K, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<K, T>, Series<K, T>> f;

      public WindowWhile(FSharpFunc<Series<K, T>, Series<K, T>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<Series<K, T>> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<Series<K, T>>(this.f.Invoke(ds.Data));
      }
    }

    [Serializable]
    internal sealed class ChunkSizeInto<K> : FSharpFunc<IEnumerable<K>, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkSizeInto()
      {
        base.ctor();
      }

      public virtual K Invoke(IEnumerable<K> source)
      {
        return (K) SeqModule.Last<K>((IEnumerable<M0>) source);
      }
    }

    [Serializable]
    internal sealed class ChunkSizeInto<K> : FSharpFunc<IEnumerable<K>, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkSizeInto()
      {
        base.ctor();
      }

      public virtual K Invoke(IEnumerable<K> source)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) source);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkSizeInto<K, T>
    {
      public Tuple<int, Boundary> bounds;

      public ChunkSizeInto(Tuple<int, Boundary> bounds)
      {
        this.bounds = bounds;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (!this.bounds.Item2.HasFlag((Enum) Boundary.AtBeginning) ? (FSharpFunc<IEnumerable<K>, K>) new SeriesModule.ChunkSizeInto<K>() : (FSharpFunc<IEnumerable<K>, K>) new SeriesModule.ChunkSizeInto<K>()).Invoke(d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkSizeInto<K, T, R>
    {
      public FSharpFunc<DataSegment<Series<K, T>>, R> f;

      public ChunkSizeInto(FSharpFunc<DataSegment<Series<K, T>>, R> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<R> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<R>(this.f.Invoke(ds));
      }
    }

    [Serializable]
    internal sealed class ChunkSize<K, T> : FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkSize()
      {
        base.ctor();
      }

      public virtual Series<K, T> Invoke(DataSegment<Series<K, T>> ds)
      {
        return DataSegment.GetData<Series<K, T>>(ds);
      }
    }

    [Serializable]
    internal sealed class ChunkSize<K> : FSharpFunc<IEnumerable<K>, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkSize()
      {
        base.ctor();
      }

      public virtual K Invoke(IEnumerable<K> source)
      {
        return (K) SeqModule.Last<K>((IEnumerable<M0>) source);
      }
    }

    [Serializable]
    internal sealed class ChunkSize<K> : FSharpFunc<IEnumerable<K>, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkSize()
      {
        base.ctor();
      }

      public virtual K Invoke(IEnumerable<K> source)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) source);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkSize<K, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<int, Boundary> bounds;

      public ChunkSize(Tuple<int, Boundary> bounds)
      {
        this.bounds = bounds;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (!this.bounds.Item2.HasFlag((Enum) Boundary.AtBeginning) ? (FSharpFunc<IEnumerable<K>, K>) new SeriesModule.ChunkSize<K>() : (FSharpFunc<IEnumerable<K>, K>) new SeriesModule.ChunkSize<K>()).Invoke(d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkSize<K, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>> f;

      public ChunkSize(FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<Series<K, T>> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<Series<K, T>>(this.f.Invoke(ds));
      }
    }

    [Serializable]
    internal sealed class ChunkDistanceInto<D, K> : OptimizedClosures.FSharpFunc<K, K, bool>
    {
      public D distance;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkDistanceInto(D distance)
      {
        this.ctor();
        this.distance = distance;
      }

      public virtual bool Invoke(K skey, K ekey)
      {
        if (!true)
          return LanguagePrimitives.HashCompare.GenericLessThanIntrinsic<D>((M0) null, (M0) this.distance);
        throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkDistanceInto<D, K, T>
    {
      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkDistanceInto<D, K, T, R>
    {
      public FSharpFunc<Series<K, T>, R> f;

      public ChunkDistanceInto(FSharpFunc<Series<K, T>, R> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<R> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<R>(this.f.Invoke(ds.Data));
      }
    }

    [Serializable]
    internal sealed class ChunkDistance0<D, K, T> : FSharpFunc<Series<K, T>, Series<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkDistance0()
      {
        base.ctor();
      }

      public virtual Series<K, T> Invoke(Series<K, T> x)
      {
        return (Series<K, T>) Operators.Identity<Series<K, T>>((M0) x);
      }
    }

    [Serializable]
    internal sealed class ChunkDistance0<D, K> : OptimizedClosures.FSharpFunc<K, K, bool>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public D distance;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkDistance0(D distance)
      {
        this.ctor();
        this.distance = distance;
      }

      public virtual bool Invoke(K skey, K ekey)
      {
        if (!true)
          return LanguagePrimitives.HashCompare.GenericLessThanIntrinsic<D>((M0) null, (M0) this.distance);
        throw new NotSupportedException("Dynamic invocation of op_Subtraction is not supported");
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkDistance0<D, K, T>
    {
      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkDistance0<D, K, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<K, T>, Series<K, T>> f;

      public ChunkDistance0(FSharpFunc<Series<K, T>, Series<K, T>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<Series<K, T>> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<Series<K, T>>(this.f.Invoke(ds.Data));
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkWhileInto8<K, T>
    {
      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkWhileInto8<K, T, a>
    {
      public FSharpFunc<Series<K, T>, a> f;

      public ChunkWhileInto8(FSharpFunc<Series<K, T>, a> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<a> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<a>(this.f.Invoke(ds.Data));
      }
    }

    [Serializable]
    internal sealed class ChunkWhile3<K, T> : FSharpFunc<Series<K, T>, Series<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkWhile3()
      {
        base.ctor();
      }

      public virtual Series<K, T> Invoke(Series<K, T> x)
      {
        return (Series<K, T>) Operators.Identity<Series<K, T>>((M0) x);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkWhile3<K, T>
    {
      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkWhile3<K, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<K, T>, Series<K, T>> f;

      public ChunkWhile3(FSharpFunc<Series<K, T>, Series<K, T>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<Series<K, T>> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<Series<K, T>>(this.f.Invoke(ds.Data));
      }
    }

    [Serializable]
    internal sealed class WindowInto9<K, T> : FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowInto9()
      {
        base.ctor();
      }

      public virtual Series<K, T> Invoke(DataSegment<Series<K, T>> ds)
      {
        return DataSegment.GetData<Series<K, T>>(ds);
      }
    }

    [Serializable]
    internal sealed class WindowInto9<K, T, R> : FSharpFunc<DataSegment<Series<K, T>>, R>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<K, T>, R> g;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowInto9(FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>> f, FSharpFunc<Series<K, T>, R> g)
      {
        this.ctor();
        this.f = f;
        this.g = g;
      }

      public virtual R Invoke(DataSegment<Series<K, T>> x)
      {
        return this.g.Invoke(this.f.Invoke(x));
      }
    }

    [Serializable]
    internal sealed class Window2<K, T> : FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Window2()
      {
        base.ctor();
      }

      public virtual Series<K, T> Invoke(DataSegment<Series<K, T>> ds)
      {
        return DataSegment.GetData<Series<K, T>>(ds);
      }
    }

    [Serializable]
    internal sealed class ChunkInto6<K, T> : FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkInto6()
      {
        base.ctor();
      }

      public virtual Series<K, T> Invoke(DataSegment<Series<K, T>> ds)
      {
        return DataSegment.GetData<Series<K, T>>(ds);
      }
    }

    [Serializable]
    internal sealed class ChunkInto6<K, T, R> : FSharpFunc<DataSegment<Series<K, T>>, R>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<K, T>, R> g;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkInto6(FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>> f, FSharpFunc<Series<K, T>, R> g)
      {
        this.ctor();
        this.f = f;
        this.g = g;
      }

      public virtual R Invoke(DataSegment<Series<K, T>> x)
      {
        return this.g.Invoke(this.f.Invoke(x));
      }
    }

    [Serializable]
    internal sealed class ChunkInto6<K> : FSharpFunc<IEnumerable<K>, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkInto6()
      {
        base.ctor();
      }

      public virtual K Invoke(IEnumerable<K> source)
      {
        return (K) SeqModule.Last<K>((IEnumerable<M0>) source);
      }
    }

    [Serializable]
    internal sealed class ChunkInto6<K> : FSharpFunc<IEnumerable<K>, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkInto6()
      {
        base.ctor();
      }

      public virtual K Invoke(IEnumerable<K> source)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) source);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkInto6<K, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<int, Boundary> bounds;

      public ChunkInto6(Tuple<int, Boundary> bounds)
      {
        this.bounds = bounds;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (!this.bounds.Item2.HasFlag((Enum) Boundary.AtBeginning) ? (FSharpFunc<IEnumerable<K>, K>) new SeriesModule.ChunkInto6<K>() : (FSharpFunc<IEnumerable<K>, K>) new SeriesModule.ChunkInto6<K>()).Invoke(d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ChunkInto6<K, T, R>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<DataSegment<Series<K, T>>, R> f;

      public ChunkInto6(FSharpFunc<DataSegment<Series<K, T>>, R> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<R> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<R>(this.f.Invoke(ds));
      }
    }

    [Serializable]
    internal sealed class Chunk9<K, T> : FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Chunk9()
      {
        base.ctor();
      }

      public virtual Series<K, T> Invoke(DataSegment<Series<K, T>> ds)
      {
        return DataSegment.GetData<Series<K, T>>(ds);
      }
    }

    [Serializable]
    internal sealed class Chunk9<K> : FSharpFunc<IEnumerable<K>, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Chunk9()
      {
        base.ctor();
      }

      public virtual K Invoke(IEnumerable<K> source)
      {
        return (K) SeqModule.Last<K>((IEnumerable<M0>) source);
      }
    }

    [Serializable]
    internal sealed class Chunk9<K> : FSharpFunc<IEnumerable<K>, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Chunk9()
      {
        base.ctor();
      }

      public virtual K Invoke(IEnumerable<K> source)
      {
        return (K) SeqModule.Head<K>((IEnumerable<M0>) source);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class Chunk9<K, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<int, Boundary> bounds;

      public Chunk9(Tuple<int, Boundary> bounds)
      {
        this.bounds = bounds;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal K Invoke(DataSegment<Series<K, T>> d)
      {
        return (!this.bounds.Item2.HasFlag((Enum) Boundary.AtBeginning) ? (FSharpFunc<IEnumerable<K>, K>) new SeriesModule.Chunk9<K>() : (FSharpFunc<IEnumerable<K>, K>) new SeriesModule.Chunk9<K>()).Invoke(d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class Chunk9<K, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>> f;

      public Chunk9(FSharpFunc<DataSegment<Series<K, T>>, Series<K, T>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<Series<K, T>> Invoke(DataSegment<Series<K, T>> ds)
      {
        return new OptionalValue<Series<K, T>>(this.f.Invoke(ds));
      }
    }

    [Serializable]
    internal sealed class PairwiseWith2<K, T, a> : OptimizedClosures.FSharpFunc<K, Tuple<T, T>, a>
    {
      public FSharpFunc<K, FSharpFunc<Tuple<T, T>, a>> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal PairwiseWith2(FSharpFunc<K, FSharpFunc<Tuple<T, T>, a>> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual a Invoke(K k, Tuple<T, T> v)
      {
        return (a) FSharpFunc<K, Tuple<T, T>>.InvokeFast<a>((FSharpFunc<K, FSharpFunc<Tuple<T, T>, M0>>) this.f, k, v);
      }
    }

    [Serializable]
    internal sealed class groupInto8<K, T, TNewKey, TNewValue> : OptimizedClosures.FSharpFunc<TNewKey, Series<K, T>, TNewValue>
    {
      public FSharpFunc<TNewKey, FSharpFunc<Series<K, T>, TNewValue>> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal groupInto8(FSharpFunc<TNewKey, FSharpFunc<Series<K, T>, TNewValue>> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual TNewValue Invoke(TNewKey k, Series<K, T> s)
      {
        return (TNewValue) FSharpFunc<TNewKey, Series<K, T>>.InvokeFast<TNewValue>((FSharpFunc<TNewKey, FSharpFunc<Series<K, T>, M0>>) this.f, k, s);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class groupInto8<K, T, TNewKey>
    {
      public FSharpFunc<K, FSharpFunc<T, TNewKey>> keySelector;

      public groupInto8(FSharpFunc<K, FSharpFunc<T, TNewKey>> keySelector)
      {
        this.keySelector = keySelector;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal TNewKey Invoke(KeyValuePair<K, T> _arg1)
      {
        Tuple<K, T> tuple = (Tuple<K, T>) Operators.KeyValuePattern<K, T>((KeyValuePair<M0, M1>) _arg1);
        T obj = tuple.Item2;
        return (TNewKey) FSharpFunc<K, T>.InvokeFast<TNewKey>((FSharpFunc<K, FSharpFunc<T, M0>>) this.keySelector, tuple.Item1, obj);
      }
    }

    [Serializable]
    internal sealed class groupBy1<K, T, TNewKey> : OptimizedClosures.FSharpFunc<TNewKey, Series<K, T>, Series<K, T>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal groupBy1()
      {
        base.ctor();
      }

      public virtual Series<K, T> Invoke(TNewKey k, Series<K, T> s)
      {
        return s;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class DropMissing2<K, T>
    {
      internal bool Invoke(KeyValuePair<K, OptionalValue<T>> _arg1)
      {
        Tuple<K, OptionalValue<T>> tuple = (Tuple<K, OptionalValue<T>>) Operators.KeyValuePattern<K, OptionalValue<T>>((KeyValuePair<M0, M1>) _arg1);
        OptionalValue<T> optionalValue = tuple.Item2;
        K k = tuple.Item1;
        return optionalValue.HasValue;
      }
    }

    [Serializable]
    internal sealed class FillMissingUsing1<K, T> : OptimizedClosures.FSharpFunc<K, FSharpOption<T>, FSharpOption<T>>
    {
      public FSharpFunc<K, T> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal FillMissingUsing1(FSharpFunc<K, T> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual FSharpOption<T> Invoke(K k, FSharpOption<T> _arg1)
      {
        return _arg1 ?? FSharpOption<T>.Some(this.f.Invoke(k));
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class FillMissingBetween2<K, T>
    {
      internal OptionalValue<T> Invoke(KeyValuePair<K, OptionalValue<Tuple<OptionalValue<T>, OptionalValue<T>>>> kvp)
      {
        FSharpChoice<Unit, Tuple<OptionalValue<T>, OptionalValue<T>>> fsharpChoice1 = OptionalValueModule.MissingPresent<Tuple<OptionalValue<T>, OptionalValue<T>>>(kvp.Value);
        if (fsharpChoice1 is FSharpChoice<Unit, Tuple<OptionalValue<T>, OptionalValue<T>>>.Choice2Of2)
        {
          FSharpChoice<Unit, T> fsharpChoice2 = OptionalValueModule.MissingPresent<T>(((FSharpChoice<Unit, Tuple<OptionalValue<T>, OptionalValue<T>>>.Choice2Of2) fsharpChoice1).get_Item().Item2);
          if (fsharpChoice2 is FSharpChoice<Unit, T>.Choice2Of2)
            return new OptionalValue<T>(((FSharpChoice<Unit, T>.Choice2Of2) fsharpChoice2).get_Item());
          FSharpChoice<Unit, T> fsharpChoice3 = OptionalValueModule.MissingPresent<T>(((FSharpChoice<Unit, Tuple<OptionalValue<T>, OptionalValue<T>>>.Choice2Of2) fsharpChoice1).get_Item().Item1);
          if (fsharpChoice3 is FSharpChoice<Unit, T>.Choice2Of2)
            return new OptionalValue<T>(((FSharpChoice<Unit, T>.Choice2Of2) fsharpChoice3).get_Item());
        }
        return OptionalValue<T>.Missing;
      }
    }

    [Serializable]
    internal sealed class missingCompare8<T> : OptimizedClosures.FSharpFunc<long, long, int>
    {
      public FSharpFunc<T, FSharpFunc<T, int>> compareFunc;
      public IVector<T> values;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal missingCompare8(FSharpFunc<T, FSharpFunc<T, int>> compareFunc, IVector<T> values)
      {
        this.ctor();
        this.compareFunc = compareFunc;
        this.values = values;
      }

      public virtual int Invoke(long a, long b)
      {
        OptionalValue<T> optionalValue1 = this.values.GetValue(a);
        FSharpOption<T> fsharpOption1 = !optionalValue1.HasValue ? (FSharpOption<T>) null : FSharpOption<T>.Some(optionalValue1.Value);
        OptionalValue<T> optionalValue2 = this.values.GetValue(b);
        FSharpOption<T> fsharpOption2 = !optionalValue2.HasValue ? (FSharpOption<T>) null : FSharpOption<T>.Some(optionalValue2.Value);
        Tuple<FSharpOption<T>, FSharpOption<T>> tuple = new Tuple<FSharpOption<T>, FSharpOption<T>>(fsharpOption1, fsharpOption2);
        if (tuple.Item1 == null)
        {
          if (tuple.Item2 == null)
            return 0;
          tuple.Item2.get_Value();
          return -1;
        }
        FSharpOption<T> fsharpOption3 = tuple.Item1;
        if (tuple.Item2 != null)
        {
          T obj = tuple.Item2.get_Value();
          return (int) FSharpFunc<T, T>.InvokeFast<int>((FSharpFunc<T, FSharpFunc<T, M0>>) this.compareFunc, fsharpOption3.get_Value(), obj);
        }
        fsharpOption3.get_Value();
        return 1;
      }
    }

    [Serializable]
    internal sealed class sortWithCommand7<K> : OptimizedClosures.FSharpFunc<KeyValuePair<K, long>, KeyValuePair<K, long>, int>
    {
      public FSharpFunc<long, FSharpFunc<long, int>> missingCompare;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sortWithCommand7(FSharpFunc<long, FSharpFunc<long, int>> missingCompare)
      {
        this.ctor();
        this.missingCompare = missingCompare;
      }

      public virtual int Invoke(KeyValuePair<K, long> kva, KeyValuePair<K, long> kvb)
      {
        return (int) FSharpFunc<long, long>.InvokeFast<int>((FSharpFunc<long, FSharpFunc<long, M0>>) this.missingCompare, kva.Value, kvb.Value);
      }
    }

    [Serializable]
    internal sealed class newKeys8<K> : FSharpFunc<KeyValuePair<K, long>, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newKeys8()
      {
        base.ctor();
      }

      public virtual K Invoke(KeyValuePair<K, long> kvp)
      {
        return kvp.Key;
      }
    }

    [Serializable]
    internal sealed class newLocs9<K> : FSharpFunc<KeyValuePair<K, long>, long>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newLocs9()
      {
        base.ctor();
      }

      public virtual long Invoke(KeyValuePair<K, long> kvp)
      {
        return kvp.Value;
      }
    }

    [Serializable]
    internal sealed class reordering3<K> : FSharpFunc<long, long>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IIndex<K> objectArg;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal reordering3(IIndex<K> objectArg)
      {
        this.ctor();
        this.objectArg = objectArg;
      }

      public virtual long Invoke(long arg00)
      {
        return this.objectArg.AddressAt(arg00);
      }
    }

    [Serializable]
    internal sealed class reordering3 : OptimizedClosures.FSharpFunc<long, long, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal reordering3()
      {
        base.ctor();
      }

      public virtual bool Invoke(long x, long y)
      {
        return x >= y;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class reordering3 : IEnumerator, IDisposable, IEnumerator<long>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<long, FSharpFunc<long, bool>> geq;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpRef<long> current;

      public reordering3(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq, FSharpRef<long> current)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        this.current = current;
        // ISSUE: explicit constructor call
        base.ctor();
        SeriesModule.reordering3 reordering13133 = this;
      }

      long IEnumerator<long>.get_Current()
      {
        return this.current.get_Value();
      }

      object IEnumerator.get_Current()
      {
        return (object) this.current.get_Value();
      }

      bool IEnumerator.MoveNext()
      {
        if (FSharpFunc<long, long>.InvokeFast<bool>((FSharpFunc<long, FSharpFunc<long, M0>>) this.geq, this.current.get_Value(), this.hi) != null)
          return false;
        this.current.set_Value(this.current.get_Value() + this.step);
        return true;
      }

      void IEnumerator.Reset()
      {
        this.current.set_Value(this.lo - this.step);
      }

      void IDisposable.Dispose()
      {
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class reordering3 : IEnumerable, IEnumerable<long>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<long, FSharpFunc<long, bool>> geq;

      public reordering3(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        // ISSUE: explicit constructor call
        base.ctor();
        SeriesModule.reordering3 reordering13132 = this;
      }

      IEnumerator<long> IEnumerable<long>.GetEnumerator()
      {
        return (IEnumerator<long>) new SeriesModule.reordering3(this.lo, this.hi, this.step, this.geq, (FSharpRef<long>) Operators.Ref<long>((M0) (this.lo - this.step)));
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) ((IEnumerable<long>) this).GetEnumerator();
      }
    }

    [Serializable]
    internal sealed class reordering3 : OptimizedClosures.FSharpFunc<long, long, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal reordering3()
      {
        base.ctor();
      }

      public virtual bool Invoke(long x, long y)
      {
        return x <= y;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class reordering3 : IEnumerator, IDisposable, IEnumerator<long>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<long, FSharpFunc<long, bool>> geq;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpRef<long> current;

      public reordering3(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq, FSharpRef<long> current)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        this.current = current;
        // ISSUE: explicit constructor call
        base.ctor();
        SeriesModule.reordering3 reordering13136 = this;
      }

      long IEnumerator<long>.get_Current()
      {
        return this.current.get_Value();
      }

      object IEnumerator.get_Current()
      {
        return (object) this.current.get_Value();
      }

      bool IEnumerator.MoveNext()
      {
        if (FSharpFunc<long, long>.InvokeFast<bool>((FSharpFunc<long, FSharpFunc<long, M0>>) this.geq, this.current.get_Value(), this.hi) != null)
          return false;
        this.current.set_Value(this.current.get_Value() + this.step);
        return true;
      }

      void IEnumerator.Reset()
      {
        this.current.set_Value(this.lo - this.step);
      }

      void IDisposable.Dispose()
      {
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class reordering3 : IEnumerable, IEnumerable<long>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long lo;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long hi;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long step;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<long, FSharpFunc<long, bool>> geq;

      public reordering3(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq)
      {
        this.lo = lo;
        this.hi = hi;
        this.step = step;
        this.geq = geq;
        // ISSUE: explicit constructor call
        base.ctor();
        SeriesModule.reordering3 reordering13135 = this;
      }

      IEnumerator<long> IEnumerable<long>.GetEnumerator()
      {
        return (IEnumerator<long>) new SeriesModule.reordering3(this.lo, this.hi, this.step, this.geq, (FSharpRef<long>) Operators.Ref<long>((M0) (this.lo - this.step)));
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) ((IEnumerable<long>) this).GetEnumerator();
      }
    }

    [Serializable]
    internal sealed class fseries0<T, V> : FSharpFunc<OptionalValue<T>, OptionalValue<V>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, V> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal fseries0(FSharpFunc<T, V> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual OptionalValue<V> Invoke(OptionalValue<T> input)
      {
        if (input.HasValue)
          return new OptionalValue<V>(this.f.Invoke(input.Value));
        return OptionalValue<V>.Missing;
      }
    }

    [Serializable]
    internal sealed class fseries0<T, V> : FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<T>, OptionalValue<V>>>
    {
      public FSharpFunc<T, V> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal fseries0(FSharpFunc<T, V> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual FSharpFunc<OptionalValue<T>, OptionalValue<V>> Invoke(IVectorLocation _arg1)
      {
        return (FSharpFunc<OptionalValue<T>, OptionalValue<V>>) new SeriesModule.fseries0<T, V>(this.f);
      }
    }

    [Serializable]
    internal sealed class sortByCommand1<V> : OptimizedClosures.FSharpFunc<V, V, int>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sortByCommand1()
      {
        base.ctor();
      }

      public virtual int Invoke(V x, V y)
      {
        return LanguagePrimitives.HashCompare.GenericComparisonIntrinsic<V>((M0) x, (M0) y);
      }
    }

    [Serializable]
    internal sealed class Sort2<V> : OptimizedClosures.FSharpFunc<V, V, int>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Sort2()
      {
        base.ctor();
      }

      public virtual int Invoke(V x, V y)
      {
        return LanguagePrimitives.HashCompare.GenericComparisonIntrinsic<V>((M0) x, (M0) y);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class resampleInto3<K, V, a>
    {
      public FSharpFunc<K, FSharpFunc<Series<K, V>, a>> f;

      public resampleInto3(FSharpFunc<K, FSharpFunc<Series<K, V>, a>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal a Invoke(K k, Series<K, V> s)
      {
        return (a) FSharpFunc<K, Series<K, V>>.InvokeFast<a>((FSharpFunc<K, FSharpFunc<Series<K, V>, M0>>) this.f, k, s);
      }
    }

    [Serializable]
    internal sealed class resample4<K, V> : OptimizedClosures.FSharpFunc<K, Series<K, V>, Series<K, V>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal resample4()
      {
        base.ctor();
      }

      public virtual Series<K, V> Invoke(K k, Series<K, V> s)
      {
        return s;
      }
    }

    [Serializable]
    internal sealed class resampleEquivInto9<K1, K2> : OptimizedClosures.FSharpFunc<K1, K1, bool>
    {
      public FSharpFunc<K1, K2> keyProj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal resampleEquivInto9(FSharpFunc<K1, K2> keyProj)
      {
        this.ctor();
        this.keyProj = keyProj;
      }

      public virtual bool Invoke(K1 k1, K1 k2)
      {
        return LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<K2>((M0) this.keyProj.Invoke(k1), (M0) this.keyProj.Invoke(k2));
      }
    }

    [Serializable]
    internal sealed class resampleEquivInto9<K1, V1> : FSharpFunc<Series<K1, V1>, Series<K1, V1>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal resampleEquivInto9()
      {
        base.ctor();
      }

      public virtual Series<K1, V1> Invoke(Series<K1, V1> x)
      {
        return (Series<K1, V1>) Operators.Identity<Series<K1, V1>>((M0) x);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class resampleEquivInto9<K1, V1>
    {
      internal K1 Invoke(DataSegment<Series<K1, V1>> d)
      {
        return (K1) SeqModule.Head<K1>((IEnumerable<M0>) d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class resampleEquivInto9<K1, V1>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<K1, V1>, Series<K1, V1>> f;

      public resampleEquivInto9(FSharpFunc<Series<K1, V1>, Series<K1, V1>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<Series<K1, V1>> Invoke(DataSegment<Series<K1, V1>> ds)
      {
        return new OptionalValue<Series<K1, V1>>(this.f.Invoke(ds.Data));
      }
    }

    [Serializable]
    internal sealed class resampleEquiv3<K1, V1> : FSharpFunc<Series<K1, V1>, Series<K1, V1>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal resampleEquiv3()
      {
        base.ctor();
      }

      public virtual Series<K1, V1> Invoke(Series<K1, V1> x)
      {
        return (Series<K1, V1>) Operators.Identity<Series<K1, V1>>((M0) x);
      }
    }

    [Serializable]
    internal sealed class resampleEquiv3<K1, K2> : OptimizedClosures.FSharpFunc<K1, K1, bool>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<K1, K2> keyProj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal resampleEquiv3(FSharpFunc<K1, K2> keyProj)
      {
        this.ctor();
        this.keyProj = keyProj;
      }

      public virtual bool Invoke(K1 k1, K1 k2)
      {
        return LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<K2>((M0) this.keyProj.Invoke(k1), (M0) this.keyProj.Invoke(k2));
      }
    }

    [Serializable]
    internal sealed class resampleEquiv3<K1, V1> : FSharpFunc<Series<K1, V1>, Series<K1, V1>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal resampleEquiv3()
      {
        base.ctor();
      }

      public virtual Series<K1, V1> Invoke(Series<K1, V1> x)
      {
        return (Series<K1, V1>) Operators.Identity<Series<K1, V1>>((M0) x);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class resampleEquiv3<K1, V1>
    {
      internal K1 Invoke(DataSegment<Series<K1, V1>> d)
      {
        return (K1) SeqModule.Head<K1>((IEnumerable<M0>) d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class resampleEquiv3<K1, V1>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<K1, V1>, Series<K1, V1>> f;

      public resampleEquiv3(FSharpFunc<Series<K1, V1>, Series<K1, V1>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<Series<K1, V1>> Invoke(DataSegment<Series<K1, V1>> ds)
      {
        return new OptionalValue<Series<K1, V1>>(this.f.Invoke(ds.Data));
      }
    }

    [Serializable]
    internal sealed class keys0<K2> : FSharpFunc<K2, FSharpOption<Tuple<K2, K2>>>
    {
      public FSharpFunc<K2, K2> nextKey;
      public K2 max;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal keys0(FSharpFunc<K2, K2> nextKey, K2 max)
      {
        this.ctor();
        this.nextKey = nextKey;
        this.max = max;
      }

      public virtual FSharpOption<Tuple<K2, K2>> Invoke(K2 dt)
      {
        if (LanguagePrimitives.HashCompare.GenericLessOrEqualIntrinsic<K2>((M0) dt, (M0) this.max))
          return FSharpOption<Tuple<K2, K2>>.Some(new Tuple<K2, K2>(dt, this.nextKey.Invoke(dt)));
        return (FSharpOption<Tuple<K2, K2>>) null;
      }
    }

    [Serializable]
    internal sealed class reindexed6<K1, K2> : OptimizedClosures.FSharpFunc<K1, K1, bool>
    {
      public FSharpFunc<K1, K2> keyProj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal reindexed6(FSharpFunc<K1, K2> keyProj)
      {
        this.ctor();
        this.keyProj = keyProj;
      }

      public virtual bool Invoke(K1 k1, K1 k2)
      {
        return LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<K2>((M0) this.keyProj.Invoke(k1), (M0) this.keyProj.Invoke(k2));
      }
    }

    [Serializable]
    internal sealed class reindexed6<K1, V> : FSharpFunc<Series<K1, V>, Series<K1, V>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal reindexed6()
      {
        base.ctor();
      }

      public virtual Series<K1, V> Invoke(Series<K1, V> x)
      {
        return (Series<K1, V>) Operators.Identity<Series<K1, V>>((M0) x);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class reindexed6<K1, V>
    {
      internal K1 Invoke(DataSegment<Series<K1, V>> d)
      {
        return (K1) SeqModule.Head<K1>((IEnumerable<M0>) d.Data.Keys);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class reindexed6<K1, V>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<K1, V>, Series<K1, V>> f;

      public reindexed6(FSharpFunc<Series<K1, V>, Series<K1, V>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<Series<K1, V>> Invoke(DataSegment<Series<K1, V>> ds)
      {
        return new OptionalValue<Series<K1, V>>(this.f.Invoke(ds.Data));
      }
    }

    [Serializable]
    internal sealed class resampleUniformInto2<K1, K2, V> : FSharpFunc<K2, Series<K1, V>>
    {
      public Lookup fillMode;
      public Series<K2, Series<K1, V>> reindexed;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal resampleUniformInto2(Lookup fillMode, Series<K2, Series<K1, V>> reindexed)
      {
        this.ctor();
        this.fillMode = fillMode;
        this.reindexed = reindexed;
      }

      public virtual Series<K1, V> Invoke(K2 k)
      {
        switch (this.fillMode)
        {
          case Lookup.ExactOrGreater:
            Series<K1, V> series1 = this.reindexed.Get(k, this.fillMode);
            return new Series<K1, V>((IEnumerable<K1>) FSharpList<K1>.Cons(series1.KeyRange.Item1, FSharpList<K1>.get_Empty()), (IEnumerable<V>) FSharpList<V>.Cons(series1[series1.KeyRange.Item1], FSharpList<V>.get_Empty()));
          case Lookup.ExactOrSmaller:
            Series<K1, V> series2 = this.reindexed.Get(k, this.fillMode);
            return new Series<K1, V>((IEnumerable<K1>) FSharpList<K1>.Cons(series2.KeyRange.Item2, FSharpList<K1>.get_Empty()), (IEnumerable<V>) FSharpList<V>.Cons(series2[series2.KeyRange.Item2], FSharpList<V>.get_Empty()));
          default:
            return new Series<K1, V>((IEnumerable<K1>) FSharpList<K1>.get_Empty(), (IEnumerable<V>) FSharpList<V>.get_Empty());
        }
      }
    }

    [Serializable]
    internal sealed class resampleUniform8<K1, V> : FSharpFunc<Series<K1, V>, Series<K1, V>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal resampleUniform8()
      {
        base.ctor();
      }

      public virtual Series<K1, V> Invoke(Series<K1, V> x)
      {
        return (Series<K1, V>) Operators.Identity<Series<K1, V>>((M0) x);
      }
    }

    [Serializable]
    internal sealed class add9<K> : OptimizedClosures.FSharpFunc<K, TimeSpan, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal add9()
      {
        base.ctor();
      }

      public virtual K Invoke(K dt, TimeSpan ts)
      {
        throw new NotSupportedException("Dynamic invocation of op_Addition is not supported");
      }
    }

    [Serializable]
    internal sealed class sampleTimeInto0<K, V, a> : FSharpFunc<K, FSharpFunc<Series<K, V>, a>>
    {
      public FSharpFunc<Series<K, V>, a> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sampleTimeInto0(FSharpFunc<Series<K, V>, a> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual FSharpFunc<Series<K, V>, a> Invoke(K _arg1)
      {
        return this.f;
      }
    }

    [Serializable]
    internal sealed class add2<K> : OptimizedClosures.FSharpFunc<K, TimeSpan, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal add2()
      {
        base.ctor();
      }

      public virtual K Invoke(K dt, TimeSpan ts)
      {
        throw new NotSupportedException("Dynamic invocation of op_Addition is not supported");
      }
    }

    [Serializable]
    internal sealed class sampleTimeAtInto3<K, V, a> : FSharpFunc<K, FSharpFunc<Series<K, V>, a>>
    {
      public FSharpFunc<Series<K, V>, a> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sampleTimeAtInto3(FSharpFunc<Series<K, V>, a> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual FSharpFunc<Series<K, V>, a> Invoke(K _arg1)
      {
        return this.f;
      }
    }

    [Serializable]
    internal sealed class sampleTime2<a, b> : FSharpFunc<Series<a, b>, Series<a, b>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sampleTime2()
      {
        base.ctor();
      }

      public virtual Series<a, b> Invoke(Series<a, b> x)
      {
        return (Series<a, b>) Operators.Identity<Series<a, b>>((M0) x);
      }
    }

    [Serializable]
    internal sealed class sampleTime2<a> : OptimizedClosures.FSharpFunc<a, TimeSpan, a>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sampleTime2()
      {
        base.ctor();
      }

      public virtual a Invoke(a dt, TimeSpan ts)
      {
        throw new NotSupportedException("Dynamic invocation of op_Addition is not supported");
      }
    }

    [Serializable]
    internal sealed class sampleTime2<a, b> : FSharpFunc<a, FSharpFunc<Series<a, b>, Series<a, b>>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<a, b>, Series<a, b>> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sampleTime2(FSharpFunc<Series<a, b>, Series<a, b>> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual FSharpFunc<Series<a, b>, Series<a, b>> Invoke(a _arg1)
      {
        return this.f;
      }
    }

    [Serializable]
    internal sealed class sampleTimeAt2<a, b> : FSharpFunc<Series<a, b>, Series<a, b>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sampleTimeAt2()
      {
        base.ctor();
      }

      public virtual Series<a, b> Invoke(Series<a, b> x)
      {
        return (Series<a, b>) Operators.Identity<Series<a, b>>((M0) x);
      }
    }

    [Serializable]
    internal sealed class sampleTimeAt2<a> : OptimizedClosures.FSharpFunc<a, TimeSpan, a>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sampleTimeAt2()
      {
        base.ctor();
      }

      public virtual a Invoke(a dt, TimeSpan ts)
      {
        throw new NotSupportedException("Dynamic invocation of op_Addition is not supported");
      }
    }

    [Serializable]
    internal sealed class sampleTimeAt2<a, b> : FSharpFunc<a, FSharpFunc<Series<a, b>, Series<a, b>>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<a, b>, Series<a, b>> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal sampleTimeAt2(FSharpFunc<Series<a, b>, Series<a, b>> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual FSharpFunc<Series<a, b>, Series<a, b>> Invoke(a _arg1)
      {
        return this.f;
      }
    }

    [Serializable]
    internal sealed class add7<K> : OptimizedClosures.FSharpFunc<K, TimeSpan, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal add7()
      {
        base.ctor();
      }

      public virtual K Invoke(K dt, TimeSpan ts)
      {
        throw new NotSupportedException("Dynamic invocation of op_Addition is not supported");
      }
    }

    [Serializable]
    internal sealed class add4<K> : OptimizedClosures.FSharpFunc<K, TimeSpan, K>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal add4()
      {
        base.ctor();
      }

      public virtual K Invoke(K dt, TimeSpan ts)
      {
        throw new NotSupportedException("Dynamic invocation of op_Addition is not supported");
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ZipAlignInto3<V1, V2, R, K>
    {
      public FSharpFunc<FSharpOption<V1>, FSharpFunc<FSharpOption<V2>, FSharpOption<R>>> op;

      public ZipAlignInto3(FSharpFunc<FSharpOption<V1>, FSharpFunc<FSharpOption<V2>, FSharpOption<R>>> op)
      {
        this.op = op;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal OptionalValue<R> Invoke(KeyValuePair<K, OptionalValue<Tuple<OptionalValue<V1>, OptionalValue<V2>>>> _arg1)
      {
        FSharpChoice<Unit, Tuple<OptionalValue<V1>, OptionalValue<V2>>> fsharpChoice = OptionalValueModule.MissingPresent<Tuple<OptionalValue<V1>, OptionalValue<V2>>>(((Tuple<K, OptionalValue<Tuple<OptionalValue<V1>, OptionalValue<V2>>>>) Operators.KeyValuePattern<K, OptionalValue<Tuple<OptionalValue<V1>, OptionalValue<V2>>>>((KeyValuePair<M0, M1>) _arg1)).Item2);
        if (!(fsharpChoice is FSharpChoice<Unit, Tuple<OptionalValue<V1>, OptionalValue<V2>>>.Choice2Of2))
          return OptionalValue<R>.Missing;
        OptionalValue<V2> optionalValue1 = ((FSharpChoice<Unit, Tuple<OptionalValue<V1>, OptionalValue<V2>>>.Choice2Of2) fsharpChoice).get_Item().Item2;
        OptionalValue<V1> optionalValue2 = ((FSharpChoice<Unit, Tuple<OptionalValue<V1>, OptionalValue<V2>>>.Choice2Of2) fsharpChoice).get_Item().Item1;
        FSharpFunc<FSharpOption<V1>, FSharpFunc<FSharpOption<V2>, FSharpOption<R>>> op = this.op;
        OptionalValue<V1> optionalValue3 = optionalValue2;
        FSharpOption<V1> fsharpOption1 = !optionalValue3.HasValue ? (FSharpOption<V1>) null : FSharpOption<V1>.Some(optionalValue3.Value);
        OptionalValue<V2> optionalValue4 = optionalValue1;
        FSharpOption<V2> fsharpOption2 = !optionalValue4.HasValue ? (FSharpOption<V2>) null : FSharpOption<V2>.Some(optionalValue4.Value);
        FSharpOption<R> fsharpOption3 = (FSharpOption<R>) FSharpFunc<FSharpOption<V1>, FSharpOption<V2>>.InvokeFast<FSharpOption<R>>((FSharpFunc<FSharpOption<V1>, FSharpFunc<FSharpOption<V2>, M0>>) op, fsharpOption1, fsharpOption2);
        if (fsharpOption3 == null)
          return OptionalValue<R>.Missing;
        return new OptionalValue<R>(fsharpOption3.get_Value());
      }
    }

    [Serializable]
    internal sealed class ZipInto0<V1, V2, R> : OptimizedClosures.FSharpFunc<FSharpOption<V1>, FSharpOption<V2>, FSharpOption<R>>
    {
      public FSharpFunc<V1, FSharpFunc<V2, R>> op;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ZipInto0(FSharpFunc<V1, FSharpFunc<V2, R>> op)
      {
        this.ctor();
        this.op = op;
      }

      public virtual FSharpOption<R> Invoke(FSharpOption<V1> a, FSharpOption<V2> b)
      {
        Tuple<FSharpOption<V1>, FSharpOption<V2>> tuple = new Tuple<FSharpOption<V1>, FSharpOption<V2>>(a, b);
        if (tuple.Item1 != null)
        {
          FSharpOption<V1> fsharpOption = tuple.Item1;
          if (tuple.Item2 != null)
          {
            V2 v2 = tuple.Item2.get_Value();
            return FSharpOption<R>.Some((R) FSharpFunc<V1, V2>.InvokeFast<R>((FSharpFunc<V1, FSharpFunc<V2, M0>>) this.op, fsharpOption.get_Value(), v2));
          }
        }
        return (FSharpOption<R>) null;
      }
    }

    
    public static class Implementation
    {
      
      public static IEnumerable<K> generateKeys<K, a, V>(FSharpFunc<K, FSharpFunc<a, K>> add, FSharpOption<K> startOpt, a interval, Direction dir, Series<K, V> series)
      {
        Tuple<K, K> keyRange = series.KeyRange;
        K k = keyRange.Item1;
        K largest = keyRange.Item2;
        Comparer<K> comparer = Comparer<K>.Default;
        return new SeriesModule.Implementation.genKeys8<K, a>(add, interval, dir, largest, comparer).Invoke(Operators.DefaultArg<K>(startOpt, k));
      }

      
      public static Series<K, a> sampleTimeIntoInternal<K, T, V, a>(FSharpFunc<K, FSharpFunc<T, K>> add, FSharpOption<K> startOpt, T interval, Direction dir, FSharpFunc<K, FSharpFunc<Series<K, V>, a>> f, Series<K, V> series)
      {
        if (series.IsEmpty)
          return new Series<K, a>((IEnumerable<KeyValuePair<K, a>>) FSharpList<KeyValuePair<K, a>>.get_Empty());
        return SeriesModule.resampleInto<K, V, a>(SeriesModule.Implementation.generateKeys<K, T, V>(add, startOpt, interval, dir, series), dir, f, series);
      }

      
      public static Series<K, V> lookupTimeInternal<K, T, V>(FSharpFunc<K, FSharpFunc<T, K>> add, FSharpOption<K> startOpt, T interval, Direction dir, Lookup lookup, Series<K, V> series)
      {
        return SeriesModule.LookupAll<K, V>(SeriesModule.Implementation.generateKeys<K, T, V>(add, startOpt, interval, dir, series), lookup, series);
      }

      
      [Serializable]
      
      [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
      internal sealed class genKeys0<K, a> : GeneratedSequenceBase<K>
      {
        public FSharpFunc<K, FSharpFunc<a, K>> add;
        public a interval;
        public Direction dir;
        public K largest;
        public Comparer<K> comparer;
        public FSharpFunc<K, IEnumerable<K>> genKeys;
        public K current;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [CompilerGenerated]
        [DebuggerNonUserCode]
        public int pc;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [CompilerGenerated]
        [DebuggerNonUserCode]
        public K current0;

        public genKeys0(FSharpFunc<K, FSharpFunc<a, K>> add, a interval, Direction dir, K largest, Comparer<K> comparer, FSharpFunc<K, IEnumerable<K>> genKeys, K current, int pc, K current0)
        {
          this.add = add;
          this.interval = interval;
          this.dir = dir;
          this.largest = largest;
          this.comparer = comparer;
          this.genKeys = genKeys;
          this.current = current;
          this.pc = pc;
          this.current0 = current0;
          this.ctor();
        }

        public virtual int GenerateNext(ref IEnumerable<K> next)
        {
          switch (this.pc)
          {
            case 1:
              if (this.comparer.Compare(this.current, this.largest) <= 0)
              {
                if (this.dir == Direction.Forward)
                {
                  this.pc = 2;
                  this.current0 = this.current;
                  return 1;
                }
                goto case 2;
              }
              else
                goto case 3;
            case 2:
              this.pc = 3;
              next = this.genKeys.Invoke((K) FSharpFunc<K, a>.InvokeFast<K>((FSharpFunc<K, FSharpFunc<a, M0>>) this.add, this.current, this.interval));
              return 2;
            case 3:
              this.pc = 4;
              goto case 4;
            case 4:
              this.current0 = default (K);
              return 0;
            default:
              if (this.dir == Direction.Backward)
              {
                this.pc = 1;
                this.current0 = this.current;
                return 1;
              }
              goto case 1;
          }
        }

        public virtual void Close()
        {
          this.pc = 4;
        }

        public virtual bool get_CheckClose()
        {
          switch (this.pc)
          {
            case 0:
            case 4:
              return false;
            case 1:
              return false;
            case 2:
              return false;
            default:
              return false;
          }
        }

        [CompilerGenerated]
        [DebuggerNonUserCode]
        public virtual K get_LastGenerated()
        {
          return this.current0;
        }

        [CompilerGenerated]
        [DebuggerNonUserCode]
        public virtual IEnumerator<K> GetFreshEnumerator()
        {
          return (IEnumerator<K>) new SeriesModule.Implementation.genKeys0<K, a>(this.add, this.interval, this.dir, this.largest, this.comparer, this.genKeys, this.current, 0, default (K));
        }
      }

      [Serializable]
      internal sealed class genKeys8<K, a> : FSharpFunc<K, IEnumerable<K>>
      {
        public FSharpFunc<K, FSharpFunc<a, K>> add;
        public a interval;
        public Direction dir;
        public K largest;
        public Comparer<K> comparer;

        [CompilerGenerated]
        [DebuggerNonUserCode]
        internal genKeys8(FSharpFunc<K, FSharpFunc<a, K>> add, a interval, Direction dir, K largest, Comparer<K> comparer)
        {
          this.ctor();
          this.add = add;
          this.interval = interval;
          this.dir = dir;
          this.largest = largest;
          this.comparer = comparer;
        }

        public virtual IEnumerable<K> Invoke(K current)
        {
          return (IEnumerable<K>) new SeriesModule.Implementation.genKeys0<K, a>(this.add, this.interval, this.dir, this.largest, this.comparer, (FSharpFunc<K, IEnumerable<K>>) this, current, 0, default (K));
        }
      }
    }
  }
}
