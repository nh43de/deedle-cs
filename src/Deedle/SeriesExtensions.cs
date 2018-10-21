// Decompiled with JetBrains decompiler
// Type: Deedle.SeriesExtensions
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using \u003CStartupCodeDeedle\u003E;
using Deedle.Indices;
using Deedle.Keys;
using Deedle.Vectors;

using Microsoft.FSharp.Control;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Deedle
{
  
  [Serializable]
  public class SeriesExtensions
  {
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Series<Tuple<K1, K2>, V> GetSlice<K1, K2, V>(this Series<Tuple<K1, K2>, V> series, FSharpOption<K1> lo1, FSharpOption<K1> hi1, FSharpOption<K2> lo2, FSharpOption<K2> hi2)
    {
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K1>>((M0) lo1, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K1>>((M0) hi1, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K2>>((M0) lo2, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K2>>((M0) hi2, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      return series.GetByLevel((ICustomLookup<Tuple<K1, K2>>) new SimpleLookup<Tuple<K1, K2>>(new FSharpOption<object>[2]{ (FSharpOption<object>) OptionModule.Map<K1, object>((FSharpFunc<M0, M1>) new SeriesExtensions.GetSlice<K1>(), lo1), (FSharpOption<object>) OptionModule.Map<K2, object>((FSharpFunc<M0, M1>) new SeriesExtensions.GetSlice<K2>(), (FSharpOption<M0>) lo2) }));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Series<Tuple<K1, K2>, V> GetSlice<K1, K2, V>(this Series<Tuple<K1, K2>, V> series, FSharpOption<K1> lo1, FSharpOption<K1> hi1, K2 k2)
    {
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K1>>((M0) lo1, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K1>>((M0) hi1, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      return series.GetByLevel((ICustomLookup<Tuple<K1, K2>>) new SimpleLookup<Tuple<K1, K2>>(new FSharpOption<object>[2]
      {
        (FSharpOption<object>) OptionModule.Map<K1, object>((FSharpFunc<M0, M1>) new SeriesExtensions.GetSlice<K1>(), lo1),
        FSharpOption<object>.Some((object) k2)
      }));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Series<Tuple<K1, K2>, V> GetSlice<K1, K2, V>(this Series<Tuple<K1, K2>, V> series, K1 k1, FSharpOption<K2> lo2, FSharpOption<K2> hi2)
    {
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K2>>((M0) lo2, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K2>>((M0) hi2, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      return series.GetByLevel((ICustomLookup<Tuple<K1, K2>>) new SimpleLookup<Tuple<K1, K2>>(new FSharpOption<object>[2]
      {
        FSharpOption<object>.Some((object) k1),
        (FSharpOption<object>) OptionModule.Map<K2, object>((FSharpFunc<M0, M1>) new SeriesExtensions.GetSlice<K2>(), (FSharpOption<M0>) lo2)
      }));
    }

    public static void Print<K, V>(this Series<K, V> series)
    {
      Console.WriteLine(series.Format());
    }

    public static Series<K, double> Log<K>(this Series<K, double> series)
    {
      return Series<K, double>.Log(series);
    }

    public static Series<K, V> Shift<K, V>(this Series<K, V> series, int offset)
    {
      return SeriesModule.Shift<K, V>(offset, series);
    }

    public static Series<K, V> Sort<K, V>(this Series<K, V> series)
    {
      return SeriesModule.Sort<K, V>(series);
    }

    public static Series<K, V> SortBy<K, V, V2>(this Series<K, V> series, Func<V, V2> f)
    {
      return SeriesModule.SortBy<V, V2, K>((FSharpFunc<V, V2>) new SeriesExtensions.SortBy<V, V2>(f), series);
    }

    public static Series<K, V> SortWith<K, V>(this Series<K, V> series, Comparer<V> cmp)
    {
      return SeriesModule.SortWith<V, K>((FSharpFunc<V, FSharpFunc<V, int>>) new SeriesExtensions.SortWith<V>(cmp), series);
    }

    public static bool ContainsKey<K, T>(this Series<K, T> series, K key)
    {
      return series.TryGet(key).HasValue;
    }

    public static Series<K, T> Flatten<K, T>(this Series<K, OptionalValue<T>> series)
    {
      return SeriesModule.Flatten<K, T>(SeriesModule.MapValues<OptionalValue<T>, FSharpOption<T>, K>((FSharpFunc<OptionalValue<T>, FSharpOption<T>>) new SeriesExtensions.Flatten<T>(), series));
    }

    public static IEnumerable<KeyValuePair<K, OptionalValue<T>>> GetAllObservations<K, T>(this Series<K, T> series)
    {
      return (IEnumerable<KeyValuePair<K, OptionalValue<T>>>) new SeriesExtensions.GetAllObservations<K, T>(series, new KeyValuePair<K, long>(), (Tuple<K, long>) null, default (K), new long(), (IEnumerator<KeyValuePair<K, long>>) null, 0, new KeyValuePair<K, OptionalValue<T>>());
    }

    public static IEnumerable<OptionalValue<T>> GetAllValues<K, T>(this Series<K, T> series)
    {
      return (IEnumerable<OptionalValue<T>>) new SeriesExtensions.GetAllValues<T, K>(series, new KeyValuePair<K, long>(), (Tuple<K, long>) null, default (K), new long(), (IEnumerator<KeyValuePair<K, long>>) null, 0, new OptionalValue<T>());
    }

    public static IEnumerable<KeyValuePair<K, T>> GetObservations<K, T>(this Series<K, T> series)
    {
      return (IEnumerable<KeyValuePair<K, T>>) new SeriesExtensions.GetObservations<K, T>(series, new KeyValuePair<K, long>(), (Tuple<K, long>) null, default (K), new long(), new OptionalValue<T>(), (IEnumerator<KeyValuePair<K, long>>) null, 0, new KeyValuePair<K, T>());
    }

    public static Series<K, double> Diff<K>(this Series<K, double> series, int offset)
    {
      Series<K, double> series1 = series;
      int num = offset;
      Series<K, double> series2 = series1;
      IVectorBuilder instance = FVectorBuilderimplementation.VectorBuilder.Instance;
      Tuple<IIndex<K>, VectorConstruction> tuple = series2.Index.Builder.Shift<K>(new Tuple<IIndex<K>, VectorConstruction>(series2.Index, VectorConstruction.NewReturn(0)), num);
      VectorConstruction vectorConstruction1 = tuple.Item2;
      IIndex<K> index = tuple.Item1;
      VectorConstruction vectorConstruction2 = series2.Index.Builder.Shift<K>(new Tuple<IIndex<K>, VectorConstruction>(series2.Index, VectorConstruction.NewReturn(0)), -num).Item2;
      VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new SeriesExtensions.Diff<K>(index)), FSharpList<VectorConstruction>.Cons(vectorConstruction2, FSharpList<VectorConstruction>.Cons(vectorConstruction1, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new SeriesExtensions.Diff((FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, OptionalValue<double>>>) new SeriesExtensions.Diff((FSharpFunc<double, FSharpFunc<double, double>>) new SeriesExtensions.Diff()))));
      IVector<double> vector = instance.Build<double>(index.AddressingScheme, vectorConstruction3, new IVector<double>[1]
      {
        series2.Vector
      });
      return new Series<K, double>(index, vector, instance, series2.Index.Builder);
    }

    public static Series<K, float> Diff<K>(this Series<K, float> series, int offset)
    {
      Series<K, float> series1 = series;
      int num = offset;
      Series<K, float> series2 = series1;
      IVectorBuilder instance = FVectorBuilderimplementation.VectorBuilder.Instance;
      Tuple<IIndex<K>, VectorConstruction> tuple = series2.Index.Builder.Shift<K>(new Tuple<IIndex<K>, VectorConstruction>(series2.Index, VectorConstruction.NewReturn(0)), num);
      VectorConstruction vectorConstruction1 = tuple.Item2;
      IIndex<K> index = tuple.Item1;
      VectorConstruction vectorConstruction2 = series2.Index.Builder.Shift<K>(new Tuple<IIndex<K>, VectorConstruction>(series2.Index, VectorConstruction.NewReturn(0)), -num).Item2;
      VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new SeriesExtensions.Diff<K>(index)), FSharpList<VectorConstruction>.Cons(vectorConstruction2, FSharpList<VectorConstruction>.Cons(vectorConstruction1, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new SeriesExtensions.Diff((FSharpFunc<OptionalValue<float>, FSharpFunc<OptionalValue<float>, OptionalValue<float>>>) new SeriesExtensions.Diff((FSharpFunc<float, FSharpFunc<float, float>>) new SeriesExtensions.Diff()))));
      IVector<float> vector = instance.Build<float>(index.AddressingScheme, vectorConstruction3, new IVector<float>[1]
      {
        series2.Vector
      });
      return new Series<K, float>(index, vector, instance, series2.Index.Builder);
    }

    public static Series<K, Decimal> Diff<K>(this Series<K, Decimal> series, int offset)
    {
      Series<K, Decimal> series1 = series;
      int num = offset;
      Series<K, Decimal> series2 = series1;
      IVectorBuilder instance = FVectorBuilderimplementation.VectorBuilder.Instance;
      Tuple<IIndex<K>, VectorConstruction> tuple = series2.Index.Builder.Shift<K>(new Tuple<IIndex<K>, VectorConstruction>(series2.Index, VectorConstruction.NewReturn(0)), num);
      VectorConstruction vectorConstruction1 = tuple.Item2;
      IIndex<K> index = tuple.Item1;
      VectorConstruction vectorConstruction2 = series2.Index.Builder.Shift<K>(new Tuple<IIndex<K>, VectorConstruction>(series2.Index, VectorConstruction.NewReturn(0)), -num).Item2;
      VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new SeriesExtensions.Diff<K>(index)), FSharpList<VectorConstruction>.Cons(vectorConstruction2, FSharpList<VectorConstruction>.Cons(vectorConstruction1, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new SeriesExtensions.Diff1((FSharpFunc<OptionalValue<Decimal>, FSharpFunc<OptionalValue<Decimal>, OptionalValue<Decimal>>>) new SeriesExtensions.Diff0((FSharpFunc<Decimal, FSharpFunc<Decimal, Decimal>>) new SeriesExtensions.Diff()))));
      IVector<Decimal> vector = instance.Build<Decimal>(index.AddressingScheme, vectorConstruction3, new IVector<Decimal>[1]
      {
        series2.Vector
      });
      return new Series<K, Decimal>(index, vector, instance, series2.Index.Builder);
    }

    public static Series<K, int> Diff<K>(this Series<K, int> series, int offset)
    {
      Series<K, int> series1 = series;
      int num = offset;
      Series<K, int> series2 = series1;
      IVectorBuilder instance = FVectorBuilderimplementation.VectorBuilder.Instance;
      Tuple<IIndex<K>, VectorConstruction> tuple = series2.Index.Builder.Shift<K>(new Tuple<IIndex<K>, VectorConstruction>(series2.Index, VectorConstruction.NewReturn(0)), num);
      VectorConstruction vectorConstruction1 = tuple.Item2;
      IIndex<K> index = tuple.Item1;
      VectorConstruction vectorConstruction2 = series2.Index.Builder.Shift<K>(new Tuple<IIndex<K>, VectorConstruction>(series2.Index, VectorConstruction.NewReturn(0)), -num).Item2;
      VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new SeriesExtensions.Diff2<K>(index)), FSharpList<VectorConstruction>.Cons(vectorConstruction2, FSharpList<VectorConstruction>.Cons(vectorConstruction1, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new SeriesExtensions.Diff5((FSharpFunc<OptionalValue<int>, FSharpFunc<OptionalValue<int>, OptionalValue<int>>>) new SeriesExtensions.Diff4((FSharpFunc<int, FSharpFunc<int, int>>) new SeriesExtensions.Diff3()))));
      IVector<int> vector = instance.Build<int>(index.AddressingScheme, vectorConstruction3, new IVector<int>[1]
      {
        series2.Vector
      });
      return new Series<K, int>(index, vector, instance, series2.Index.Builder);
    }

    public static Series<K2, U> WindowInto<K1, V, K2, U>(this Series<K1, V> series, int size, Func<Series<K1, V>, KeyValuePair<K2, U>> selector)
    {
      return series.Aggregate<K2, U>(Aggregation<K1>.NewWindowSize(size, Boundary.Skip), new Func<DataSegment<Series<K1, V>>, KeyValuePair<K2, OptionalValue<U>>>(new SeriesExtensions.WindowInto<K1, V, K2, U>(selector).Invoke));
    }

    public static Series<K, U> WindowInto<K, V, U>(this Series<K, V> series, int size, Func<Series<K, V>, U> reduce)
    {
      return SeriesModule.WindowSizeInto<K, V, U>(size, Boundary.Skip, (FSharpFunc<DataSegment<Series<K, V>>, U>) new SeriesExtensions.WindowInto<K, V, U>((FSharpFunc<DataSegment<Series<K, V>>, Series<K, V>>) new SeriesExtensions.WindowInto<K, V>(), (FSharpFunc<Series<K, V>, U>) new SeriesExtensions.WindowInto<K, V, U>(reduce)), series);
    }

    public static Series<K, Series<K, V>> Window<K, V>(this Series<K, V> series, int size)
    {
      int num1 = size;
      Series<K, V> series1 = series;
      int num2 = num1;
      Boundary boundary = Boundary.Skip;
      Series<K, V> series2 = series1;
      Tuple<int, Boundary> tuple = new Tuple<int, Boundary>(num2, boundary);
      return SeriesModule.WindowSizeInto<K, V, Series<K, V>>(tuple.Item1, tuple.Item2, (FSharpFunc<DataSegment<Series<K, V>>, Series<K, V>>) new SeriesExtensions.Window<K, V>(), series2);
    }

    public static Series<K2, U> ChunkInto<K1, V, K2, U>(this Series<K1, V> series, int size, Func<Series<K1, V>, KeyValuePair<K2, U>> selector)
    {
      return series.Aggregate<K2, U>(Aggregation<K1>.NewChunkSize(size, Boundary.Skip), new Func<DataSegment<Series<K1, V>>, KeyValuePair<K2, OptionalValue<U>>>(new SeriesExtensions.ChunkInto<K1, V, K2, U>(selector).Invoke));
    }

    public static Series<K, U> ChunkInto<K, V, U>(this Series<K, V> series, int size, Func<Series<K, V>, U> reduce)
    {
      int num1 = size;
      FSharpFunc<Series<K, V>, U> g = (FSharpFunc<Series<K, V>, U>) new SeriesExtensions.ChunkInto<K, V, U>(reduce);
      Series<K, V> series1 = series;
      int num2 = num1;
      Boundary boundary = Boundary.Skip;
      FSharpFunc<DataSegment<Series<K, V>>, U> f = (FSharpFunc<DataSegment<Series<K, V>>, U>) new SeriesExtensions.ChunkInto<K, V, U>((FSharpFunc<DataSegment<Series<K, V>>, Series<K, V>>) new SeriesExtensions.ChunkInto<K, V>(), g);
      Series<K, V> series2 = series1;
      Tuple<int, Boundary> bounds = new Tuple<int, Boundary>(num2, boundary);
      Series<K, V> series3 = series2;
      Tuple<int, Boundary> tuple = bounds;
      Aggregation<K> aggregation = Aggregation<K>.NewChunkSize(tuple.Item1, tuple.Item2);
      Func<DataSegment<Series<K, V>>, K> keySelector = new Func<DataSegment<Series<K, V>>, K>(new SeriesExtensions.ChunkInto0<K, V>(bounds).Invoke);
      Func<DataSegment<Series<K, V>>, OptionalValue<U>> valueSelector = new Func<DataSegment<Series<K, V>>, OptionalValue<U>>(new SeriesExtensions.ChunkInto3<K, V, U>(f).Invoke);
      return series3.Aggregate<K, U>(aggregation, keySelector, valueSelector);
    }

    public static Series<K, Series<K, V>> Chunk<K, V>(this Series<K, V> series, int size)
    {
      int num1 = size;
      Series<K, V> series1 = series;
      int num2 = num1;
      Boundary boundary = Boundary.Skip;
      Series<K, V> series2 = series1;
      Tuple<int, Boundary> tuple1 = new Tuple<int, Boundary>(num2, boundary);
      FSharpFunc<DataSegment<Series<K, V>>, Series<K, V>> f = (FSharpFunc<DataSegment<Series<K, V>>, Series<K, V>>) new SeriesExtensions.Chunk<K, V>();
      Series<K, V> series3 = series2;
      Tuple<int, Boundary> bounds = new Tuple<int, Boundary>(tuple1.Item1, tuple1.Item2);
      Series<K, V> series4 = series3;
      Tuple<int, Boundary> tuple2 = bounds;
      Aggregation<K> aggregation = Aggregation<K>.NewChunkSize(tuple2.Item1, tuple2.Item2);
      Func<DataSegment<Series<K, V>>, K> keySelector = new Func<DataSegment<Series<K, V>>, K>(new SeriesExtensions.Chunk<K, V>(bounds).Invoke);
      Func<DataSegment<Series<K, V>>, OptionalValue<Series<K, V>>> valueSelector = new Func<DataSegment<Series<K, V>>, OptionalValue<Series<K, V>>>(new SeriesExtensions.Chunk<K, V>(f).Invoke);
      return series4.Aggregate<K, Series<K, V>>(aggregation, keySelector, valueSelector);
    }

    public static K FirstKey<K, V>(this Series<K, V> series)
    {
      return SeriesModule.GetFirstKey<K, V>(series);
    }

    public static K LastKey<K, V>(this Series<K, V> series)
    {
      return SeriesModule.GetLastKey<K, V>(series);
    }

    public static V FirstValue<K, V>(this Series<K, V> series)
    {
      return SeriesModule.GetFirstValue<K, V>(series);
    }

    public static V LastValue<K, V>(this Series<K, V> series)
    {
      return SeriesModule.GetLastValue<K, V>(series);
    }

    public static FSharpOption<V> TryFirstValue<K, V>(this Series<K, V> series)
    {
      return SeriesModule.TryGetFirstValue<K, V>(series);
    }

    public static FSharpOption<V> TryLastValue<K, V>(this Series<K, V> series)
    {
      return SeriesModule.TryGetLastValue<K, V>(series);
    }

    public static Series<K, V> DropMissing<K, V>(this Series<K, V> series)
    {
      return SeriesModule.DropMissing<K, V>(series);
    }

    public static Series<K, T> FillMissing<K, T>(this Series<K, T> series, T value)
    {
      return SeriesModule.FillMissingWith<T, K, T>(value, series);
    }

    public static Series<K, T> FillMissing<K, T>(this Series<K, T> series, [Optional] Direction direction)
    {
      return SeriesModule.FillMissing<K, T>(direction, series);
    }

    public static Series<K, T> FillMissing<K, T>(this Series<K, T> series, K startKey, K endKey, [Optional] Direction direction)
    {
      return SeriesModule.FillMissingBetween<K, T>(startKey, endKey, direction, series);
    }

    public static Series<K, T> FillMissing<K, T>(this Series<K, T> series, Func<K, T> filler)
    {
      return SeriesModule.FillMissingUsing<K, T>((FSharpFunc<K, T>) new SeriesExtensions.FillMissing<K, T>(filler), series);
    }

    public static Series<K, T> SortByKey<K, T>(this Series<K, T> series)
    {
      return SeriesModule.SortByKey<K, T>(series);
    }

    public static Series<a, Series<K, V>> ResampleEquivalence<K, V, a>(this Series<K, V> series, Func<K, a> keyProj)
    {
      FSharpFunc<K, a> fsharpFunc1 = (FSharpFunc<K, a>) new SeriesExtensions.ResampleEquivalence<K, a>(keyProj);
      Series<K, V> series1 = series;
      FSharpFunc<K, a> fsharpFunc2 = fsharpFunc1;
      FSharpFunc<Series<K, V>, Series<K, V>> f = (FSharpFunc<Series<K, V>, Series<K, V>>) new SeriesExtensions.ResampleEquivalence<K, V>();
      Series<K, V> series2 = series1;
      return SeriesModule.MapValues<Series<K, V>, Series<K, V>, a>(f, SeriesModule.MapKeys<K, a, Series<K, V>>(fsharpFunc2, series2.Aggregate<K, Series<K, V>>(Aggregation<K>.NewChunkWhile((FSharpFunc<K, FSharpFunc<K, bool>>) new SeriesExtensions.ResampleEquivalence<K, a>(fsharpFunc2)), new Func<DataSegment<Series<K, V>>, K>(new SeriesExtensions.ResampleEquivalence<K, V>().Invoke), new Func<DataSegment<Series<K, V>>, OptionalValue<Series<K, V>>>(new SeriesExtensions.ResampleEquivalence<K, V>((FSharpFunc<Series<K, V>, Series<K, V>>) new SeriesExtensions.ResampleEquivalence<K, V>()).Invoke))));
    }

    public static Series<a, b> ResampleEquivalence<K, V, a, b>(this Series<K, V> series, Func<K, a> keyProj, Func<Series<K, V>, b> aggregate)
    {
      FSharpFunc<K, a> fsharpFunc = (FSharpFunc<K, a>) new SeriesExtensions.ResampleEquivalence<K, a>(keyProj);
      FSharpFunc<Series<K, V>, b> f = (FSharpFunc<Series<K, V>, b>) new SeriesExtensions.ResampleEquivalence<K, V, b>(aggregate);
      Series<K, V> series1 = series;
      return SeriesModule.MapValues<Series<K, V>, b, a>(f, SeriesModule.MapKeys<K, a, Series<K, V>>(fsharpFunc, series1.Aggregate<K, Series<K, V>>(Aggregation<K>.NewChunkWhile((FSharpFunc<K, FSharpFunc<K, bool>>) new SeriesExtensions.ResampleEquivalence<K, a>(fsharpFunc)), new Func<DataSegment<Series<K, V>>, K>(new SeriesExtensions.ResampleEquivalence0<K, V>().Invoke), new Func<DataSegment<Series<K, V>>, OptionalValue<Series<K, V>>>(new SeriesExtensions.ResampleEquivalence1<K, V>((FSharpFunc<Series<K, V>, Series<K, V>>) new SeriesExtensions.ResampleEquivalence<K, V>()).Invoke))));
    }

    public static Series<a, V> ResampleUniform<K, V, a>(this Series<K, V> series, Func<K, a> keyProj, Func<a, a> nextKey)
    {
      return SeriesModule.Flatten<a, V>(SeriesModule.resampleUniformInto<K, a, V, FSharpOption<V>>(Lookup.ExactOrSmaller, (FSharpFunc<K, a>) new SeriesExtensions.ResampleUniform<K, a>(keyProj), (FSharpFunc<a, a>) new SeriesExtensions.ResampleUniform<a>(nextKey), (FSharpFunc<Series<K, V>, FSharpOption<V>>) new SeriesExtensions.ResampleUniform<K, V>(), series));
    }

    public static Series<K2, V> ResampleUniform<K1, V, K2>(this Series<K1, V> series, Func<K1, K2> keyProj, Func<K2, K2> nextKey, Lookup fillMode)
    {
      return SeriesModule.Flatten<K2, V>(SeriesModule.resampleUniformInto<K1, K2, V, FSharpOption<V>>(fillMode, (FSharpFunc<K1, K2>) new SeriesExtensions.ResampleUniform<K1, K2>(keyProj), (FSharpFunc<K2, K2>) new SeriesExtensions.ResampleUniform<K2>(nextKey), (FSharpFunc<Series<K1, V>, FSharpOption<V>>) new SeriesExtensions.ResampleUniform<K1, V>(), series));
    }

    public static Series<K, V> Sample<K, V>(this Series<K, V> series, IEnumerable<K> keys)
    {
      return SeriesModule.Sample<K, V>(keys, series);
    }

    public static Series<DateTime, V> Sample<V>(this Series<DateTime, V> series, DateTime start, TimeSpan interval, Direction dir)
    {
      return SeriesModule.Implementation.lookupTimeInternal<DateTime, TimeSpan, V>((FSharpFunc<DateTime, FSharpFunc<TimeSpan, DateTime>>) new SeriesExtensions.Sample(), FSharpOption<DateTime>.Some(start), interval, dir, Lookup.ExactOrSmaller, series);
    }

    public static Series<DateTimeOffset, V> Sample<V>(this Series<DateTimeOffset, V> series, DateTimeOffset start, TimeSpan interval, Direction dir)
    {
      return SeriesModule.Implementation.lookupTimeInternal<DateTimeOffset, TimeSpan, V>((FSharpFunc<DateTimeOffset, FSharpFunc<TimeSpan, DateTimeOffset>>) new SeriesExtensions.Sample(), FSharpOption<DateTimeOffset>.Some(start), interval, dir, Lookup.ExactOrSmaller, series);
    }

    public static Series<DateTime, V> Sample<V>(this Series<DateTime, V> series, TimeSpan interval, Direction dir)
    {
      return SeriesModule.Implementation.lookupTimeInternal<DateTime, TimeSpan, V>((FSharpFunc<DateTime, FSharpFunc<TimeSpan, DateTime>>) new SeriesExtensions.Sample(), (FSharpOption<DateTime>) null, interval, dir, Lookup.ExactOrSmaller, series);
    }

    public static Series<DateTimeOffset, V> Sample<V>(this Series<DateTimeOffset, V> series, TimeSpan interval, Direction dir)
    {
      return SeriesModule.Implementation.lookupTimeInternal<DateTimeOffset, TimeSpan, V>((FSharpFunc<DateTimeOffset, FSharpFunc<TimeSpan, DateTimeOffset>>) new SeriesExtensions.Sample(), (FSharpOption<DateTimeOffset>) null, interval, dir, Lookup.ExactOrSmaller, series);
    }

    public static Series<DateTime, V> Sample<V>(this Series<DateTime, V> series, TimeSpan interval)
    {
      return series.Sample<V>(interval, Direction.Backward);
    }

    public static Series<DateTimeOffset, V> Sample<V>(this Series<DateTimeOffset, V> series, TimeSpan interval)
    {
      return series.Sample<V>(interval, Direction.Backward);
    }

    public static Series<DateTime, object> SampleInto<V>(this Series<DateTime, V> series, TimeSpan interval, Direction dir, Func<DateTime, FSharpFunc<Series<DateTime, V>, object>> aggregate)
    {
      return SeriesModule.Implementation.sampleTimeIntoInternal<DateTime, TimeSpan, V, object>((FSharpFunc<DateTime, FSharpFunc<TimeSpan, DateTime>>) new SeriesExtensions.SampleInto(), (FSharpOption<DateTime>) null, interval, dir, (FSharpFunc<DateTime, FSharpFunc<Series<DateTime, V>, object>>) new SeriesExtensions.SampleInto<V>(aggregate), series);
    }

    public static Series<DateTimeOffset, object> SampleInto<V>(this Series<DateTimeOffset, V> series, TimeSpan interval, Direction dir, Func<DateTimeOffset, FSharpFunc<Series<DateTimeOffset, V>, object>> aggregate)
    {
      return SeriesModule.Implementation.sampleTimeIntoInternal<DateTimeOffset, TimeSpan, V, object>((FSharpFunc<DateTimeOffset, FSharpFunc<TimeSpan, DateTimeOffset>>) new SeriesExtensions.SampleInto(), (FSharpOption<DateTimeOffset>) null, interval, dir, (FSharpFunc<DateTimeOffset, FSharpFunc<Series<DateTimeOffset, V>, object>>) new SeriesExtensions.SampleInto<V>(aggregate), series);
    }

    [Obsolete("Use SortByKeys instead. This function will be removed in futrue versions.")]
    public static Series<K, T> OrderByKey<K, T>(this Series<K, T> series)
    {
      return SeriesModule.SortByKey<K, T>(series);
    }
  }
}
