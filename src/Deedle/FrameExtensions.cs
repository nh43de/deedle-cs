// Decompiled with JetBrains decompiler
// Type: Deedle.FrameExtensions
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using \u003CStartupCodeDeedle\u003E;
using Deedle.Keys;


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace Deedle
{
  
  [Serializable]
  public class FrameExtensions
  {
    public static Frame<R, C> RealignRows<R, C>(this Frame<R, C> frame, IEnumerable<R> keys)
    {
      return FrameModule.RealignRows<R, C>(keys, frame);
    }

    public static Frame<int, TColumnKey> IndexRowsOrdinally<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame)
    {
      return FrameModule.IndexRowsOrdinally<TRowKey, TColumnKey>(frame);
    }

    public static Frame<TNewRowIndex, C> IndexRowsWith<R, C, TNewRowIndex>(this Frame<R, C> frame, IEnumerable<TNewRowIndex> keys)
    {
      return FrameModule.IndexRowsWith<TNewRowIndex, R, C>(keys, frame);
    }

    public static Frame<R2, C> IndexRowsUsing<R, C, R2>(this Frame<R, C> frame, Func<ObjectSeries<C>, R2> f)
    {
      return FrameModule.IndexRowsUsing<C, R2, R>((FSharpFunc<ObjectSeries<C>, R2>) new FrameExtensions.IndexRowsUsing<C, R2>(f), frame);
    }

    public static Frame<R, TNewRowIndex> IndexColumnsWith<R, C, TNewRowIndex>(this Frame<R, C> frame, IEnumerable<TNewRowIndex> keys)
    {
      return FrameModule.IndexColumnsWith<TNewRowIndex, R, C>(keys, frame);
    }

    public static Frame<TRowKey, TColumnKey> SortRowsByKey<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame)
    {
      return FrameModule.SortRowsByKey<TRowKey, TColumnKey>(frame);
    }

    public static Frame<TRowKey, TColumnKey> SortColumnsByKey<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame)
    {
      return FrameModule.SortColumnsByKey<TRowKey, TColumnKey>(frame);
    }

    public static Frame<TRowKey, TColumnKey> SortRows<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame, TColumnKey key)
    {
      return FrameModule.SortRows<TColumnKey, TRowKey>(key, frame);
    }

    public static Frame<TRowKey, TColumnKey> SortRowsWith<TRowKey, TColumnKey, V>(this Frame<TRowKey, TColumnKey> frame, TColumnKey key, Comparer<V> cmp)
    {
      return FrameModule.SortRowsWith<TColumnKey, V, TRowKey>(key, (FSharpFunc<V, FSharpFunc<V, int>>) new FrameExtensions.SortRowsWith<V>(cmp), frame);
    }

    public static Frame<TRowKey, TColumnKey> SortRowsBy<TRowKey, TColumnKey, V, V2>(this Frame<TRowKey, TColumnKey> frame, TColumnKey key, Func<V, V2> f)
    {
      return FrameModule.SortRowBy<TColumnKey, V, V2, TRowKey>(key, (FSharpFunc<V, V2>) new FrameExtensions.SortRowsBy<V, V2>(f), frame);
    }

    public static Frame<TColumnKey, TRowKey> Transpose<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame)
    {
      ColumnSeries<TRowKey, TColumnKey> columns = frame.Columns;
      return FrameUtils.fromRows<TColumnKey, TRowKey, ObjectSeries<TRowKey>>(frame.IndexBuilder, frame.VectorBuilder, (Series<TColumnKey, ObjectSeries<TRowKey>>) columns);
    }

    public static Frame<R, string> ExpandColumns<R>(this Frame<R, string> frame, int nesting, [Optional] bool dynamic)
    {
      return FrameUtilsModule.expandVectors<R>(nesting, dynamic, frame);
    }

    public static Frame<R, string> ExpandColumns<R>(this Frame<R, string> frame, IEnumerable<string> names)
    {
      return FrameUtilsModule.expandColumns<R>((FSharpSet<string>) ExtraTopLevelOperators.CreateSet<string>((IEnumerable<M0>) names), frame);
    }

    public static Series<TRowKey1, Frame<TRowKey2, TColumnKey>> Nest<TRowKey1, TRowKey2, TColumnKey>(this Frame<Tuple<TRowKey1, TRowKey2>, TColumnKey> frame)
    {
      return FrameModule.Nest<TRowKey1, TRowKey2, TColumnKey>(FrameModule.SelectRowKeys<Tuple<TRowKey1, TRowKey2>, Tuple<TRowKey1, TRowKey2>, TColumnKey>((FSharpFunc<Tuple<TRowKey1, TRowKey2>, Tuple<TRowKey1, TRowKey2>>) new FrameExtensions.Nest<TRowKey1, TRowKey2>(), frame));
    }

    public static Series<TRowKey2, Frame<TRowKey1, TColumnKey>> NestBy<TRowKey1, TColumnKey, TRowKey2>(this Frame<TRowKey1, TColumnKey> frame, Func<TRowKey1, TRowKey2> keyselector)
    {
      return FrameModule.NestBy<TRowKey1, TRowKey2, TColumnKey>((FSharpFunc<TRowKey1, TRowKey2>) new FrameExtensions.NestBy<TRowKey1, TRowKey2>(keyselector), frame);
    }

    public static Frame<Tuple<TRowKey1, TRowKey2>, TColumnKey> Unnest<TRowKey1, TRowKey2, TColumnKey>(this Series<TRowKey1, Frame<TRowKey2, TColumnKey>> series)
    {
      return FrameModule.Unnest<TRowKey1, TRowKey2, TColumnKey>(series);
    }

    public static void SaveCsv<R, C>(this Frame<R, C> frame, TextWriter writer, [Optional] bool includeRowKeys, [Optional] IEnumerable<string> keyNames, [Optional] char separator, [Optional] CultureInfo culture)
    {
      FSharpOption<char> separatorOpt = separator != char.MinValue ? FSharpOption<char>.Some(separator) : (FSharpOption<char>) null;
      FSharpOption<CultureInfo> cultureOpt = !LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<CultureInfo>((M0) culture, (M0) null) ? FSharpOption<CultureInfo>.Some(culture) : (FSharpOption<CultureInfo>) null;
      FSharpOption<IEnumerable<string>> rowKeyNames = !LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<IEnumerable<string>>((M0) keyNames, (M0) null) ? FSharpOption<IEnumerable<string>>.Some(keyNames) : (FSharpOption<IEnumerable<string>>) null;
      FrameUtilsModule.writeCsv<R, C>(writer, (FSharpOption<string>) null, separatorOpt, cultureOpt, FSharpOption<bool>.Some(includeRowKeys), rowKeyNames, frame);
    }

    public static void SaveCsv<R, C>(this Frame<R, C> frame, string path, [Optional] bool includeRowKeys, [Optional] IEnumerable<string> keyNames, [Optional] char separator, [Optional] CultureInfo culture)
    {
      FSharpOption<char> separatorOpt = separator != char.MinValue ? FSharpOption<char>.Some(separator) : (FSharpOption<char>) null;
      FSharpOption<CultureInfo> cultureOpt = !LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<CultureInfo>((M0) culture, (M0) null) ? FSharpOption<CultureInfo>.Some(culture) : (FSharpOption<CultureInfo>) null;
      FSharpOption<IEnumerable<string>> rowKeyNames = !LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<IEnumerable<string>>((M0) keyNames, (M0) null) ? FSharpOption<IEnumerable<string>>.Some(keyNames) : (FSharpOption<IEnumerable<string>>) null;
      StreamWriter streamWriter = new StreamWriter(path);
      try
      {
        FrameUtilsModule.writeCsv<R, C>((TextWriter) streamWriter, FSharpOption<string>.Some(path), separatorOpt, cultureOpt, FSharpOption<bool>.Some(includeRowKeys), rowKeyNames, frame);
      }
      finally
      {
        (streamWriter as IDisposable)?.Dispose();
      }
    }

    public static void SaveCsv<R, C>(this Frame<R, C> frame, string path, IEnumerable<string> keyNames, [Optional] char separator, [Optional] CultureInfo culture)
    {
      StreamWriter streamWriter = new StreamWriter(path);
      try
      {
        FSharpOption<char> separatorOpt = separator != char.MinValue ? FSharpOption<char>.Some(separator) : (FSharpOption<char>) null;
        FSharpOption<CultureInfo> cultureOpt = !LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<CultureInfo>((M0) culture, (M0) null) ? FSharpOption<CultureInfo>.Some(culture) : (FSharpOption<CultureInfo>) null;
        FrameUtilsModule.writeCsv<R, C>((TextWriter) streamWriter, FSharpOption<string>.Some(path), separatorOpt, cultureOpt, FSharpOption<bool>.Some(true), FSharpOption<IEnumerable<string>>.Some(keyNames), frame);
      }
      finally
      {
        (streamWriter as IDisposable)?.Dispose();
      }
    }

    public static DataTable ToDataTable<R, C>(this Frame<R, C> frame, IEnumerable<string> rowKeyNames)
    {
      return FrameUtilsModule.toDataTable<R, C>(rowKeyNames, frame);
    }

    public static Frame<RNew, CNew> PivotTable<R, C, RNew, CNew, T>(this Frame<R, C> frame, C r, C c, Func<Frame<R, C>, T> op)
    {
      return FrameModule.PivotTable<R, C, RNew, CNew, T>((FSharpFunc<R, FSharpFunc<ObjectSeries<C>, RNew>>) new FrameExtensions.PivotTable7<R, C, RNew>(r), (FSharpFunc<R, FSharpFunc<ObjectSeries<C>, CNew>>) new FrameExtensions.PivotTable7<R, C, CNew>(c), (FSharpFunc<Frame<R, C>, T>) new FrameExtensions.PivotTable7<R, C, T>(op), frame);
    }

    public static void Print<K, V>(this Frame<K, V> frame)
    {
      Console.WriteLine(frame.Format());
    }

    public static void Print<K, V>(this Frame<K, V> frame, bool printTypes)
    {
      Console.WriteLine(frame.Format(printTypes));
    }

    public static Series<C, double> Sum<R, C>(this Frame<R, C> frame)
    {
      return Stats.sum<R, C>(frame);
    }

    public static Series<R, Frame<R, C>> Window<R, C>(this Frame<R, C> frame, int size)
    {
      return FrameModule.Window<R, C>(size, frame);
    }

    public static Series<R, a> Window<R, C, a>(this Frame<R, C> frame, int size, Func<Frame<R, C>, a> aggregate)
    {
      return FrameModule.WindowInto<R, C, a>(size, (FSharpFunc<Frame<R, C>, a>) new FrameExtensions.Window7<R, C, a>(aggregate), frame);
    }

    [Obsolete("Use df.RowCount")]
    public static int CountRows<R, C>(this Frame<R, C> frame)
    {
      return SeqModule.Length<KeyValuePair<R, long>>((IEnumerable<M0>) frame.RowIndex.Mappings);
    }

    [Obsolete("Use df.ColumnCount")]
    public static int CountColumns<R, C>(this Frame<R, C> frame)
    {
      return SeqModule.Length<KeyValuePair<C, long>>((IEnumerable<M0>) frame.ColumnIndex.Mappings);
    }

    public static Frame<TRowKey, TColumnKey> Where<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame, Func<KeyValuePair<TRowKey, ObjectSeries<TColumnKey>>, bool> condition)
    {
      Series<TRowKey, ObjectSeries<TColumnKey>> nested = frame.Rows.Where(condition);
      return FrameUtils.fromRowsAndColumnKeys<TRowKey, TColumnKey, ObjectSeries<TColumnKey>>(frame.IndexBuilder, frame.VectorBuilder, frame.ColumnIndex.Keys, nested);
    }

    public static Frame<TRowKey, TColumnKey> Where<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame, Func<KeyValuePair<TRowKey, ObjectSeries<TColumnKey>>, int, bool> condition)
    {
      Series<TRowKey, ObjectSeries<TColumnKey>> nested = frame.Rows.Where(condition);
      return FrameUtils.fromRowsAndColumnKeys<TRowKey, TColumnKey, ObjectSeries<TColumnKey>>(frame.IndexBuilder, frame.VectorBuilder, frame.ColumnIndex.Keys, nested);
    }

    public static Frame<TRowKey, b> Select<TRowKey, TColumnKey, a, b>(this Frame<TRowKey, TColumnKey> frame, Func<KeyValuePair<TRowKey, ObjectSeries<TColumnKey>>, a> projection) where a : ISeries<b>
    {
      Series<TRowKey, a> nested = frame.Rows.Select<a>(projection);
      return FrameUtils.fromRows<TRowKey, b, a>(frame.IndexBuilder, frame.VectorBuilder, nested);
    }

    public static Frame<TRowKey, b> Select<TRowKey, TColumnKey, a, b>(this Frame<TRowKey, TColumnKey> frame, Func<KeyValuePair<TRowKey, ObjectSeries<TColumnKey>>, int, a> projection) where a : ISeries<b>
    {
      Series<TRowKey, a> nested = frame.Rows.Select<a>(projection);
      return FrameUtils.fromRows<TRowKey, b, a>(frame.IndexBuilder, frame.VectorBuilder, nested);
    }

    public static Frame<a, TColumnKey> SelectRowKeys<TRowKey, TColumnKey, a>(this Frame<TRowKey, TColumnKey> frame, Func<KeyValuePair<TRowKey, OptionalValue<ObjectSeries<TColumnKey>>>, a> projection)
    {
      Series<a, ObjectSeries<TColumnKey>> nested = frame.Rows.SelectKeys<a>(projection);
      return FrameUtils.fromRows<a, TColumnKey, ObjectSeries<TColumnKey>>(frame.IndexBuilder, frame.VectorBuilder, nested);
    }

    public static Frame<TRowKey, a> SelectColumnKeys<TRowKey, TColumnKey, a>(this Frame<TRowKey, TColumnKey> frame, Func<KeyValuePair<TColumnKey, OptionalValue<ObjectSeries<TRowKey>>>, a> projection)
    {
      Series<a, ObjectSeries<TRowKey>> nested = frame.Columns.SelectKeys<a>(projection);
      return FrameUtils.fromColumns<TRowKey, a, ObjectSeries<TRowKey>>(frame.IndexBuilder, frame.VectorBuilder, nested);
    }

    public static Frame<TRowKey, TColumnKey> Merge<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame, TRowKey rowKey, ISeries<TColumnKey> row)
    {
      return frame.Merge(FFrameextensions.FrameofRowsStatic<TRowKey, ISeries<TColumnKey>, TColumnKey>((IEnumerable<Tuple<TRowKey, ISeries<TColumnKey>>>) FSharpList<Tuple<TRowKey, ISeries<TColumnKey>>>.Cons(FFrameextensions.op_EqualsGreater<TRowKey, ISeries<TColumnKey>>(rowKey, row), FSharpList<Tuple<TRowKey, ISeries<TColumnKey>>>.get_Empty())));
    }

    public static Frame<TRowKey, TColumnKey> Shift<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame, int offset)
    {
      return FrameModule.Shift<TRowKey, TColumnKey>(offset, frame);
    }

    public static Frame<TRowKey, TColumnKey> Diff<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame, int offset)
    {
      return FrameModule.Diff<TRowKey, TColumnKey>(offset, frame);
    }

    public static Series<TColumnKey, T> Reduce<TRowKey, TColumnKey, T>(this Frame<TRowKey, TColumnKey> frame, Func<T, T, T> aggregation)
    {
      return FrameModule.ReduceValues<T, TRowKey, TColumnKey>((FSharpFunc<T, FSharpFunc<T, T>>) new FrameExtensions.Reduce5<T>(aggregation), frame);
    }

    public static Frame<TRowKey, TColumnKey> GetRows<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame, params TRowKey[] rowKeys)
    {
      Series<TRowKey, ObjectSeries<TColumnKey>> items = frame.Rows.GetItems((IEnumerable<TRowKey>) rowKeys);
      return FrameUtils.fromRows<TRowKey, TColumnKey, ObjectSeries<TColumnKey>>(frame.IndexBuilder, frame.VectorBuilder, items);
    }

    public static Frame<TRowKey, TColumnKey> FilterRowsBy<TRowKey, TColumnKey, V>(this Frame<TRowKey, TColumnKey> frame, TColumnKey column, object value)
    {
      return FrameModule.WhereRowsBy<TColumnKey, object, TRowKey>(column, value, frame);
    }

    public static Frame<TRowKey, TColumnKey> GetRowsAt<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame, params int[] indices)
    {
      int[] numArray1 = indices;
      FSharpFunc<int, TRowKey> fsharpFunc1 = (FSharpFunc<int, TRowKey>) new FrameExtensions.keys9<TRowKey, TColumnKey>(frame.Rows);
      int[] numArray2 = numArray1;
      if ((object) numArray2 == null)
        throw new ArgumentNullException("array");
      TRowKey[] rowKeyArray1 = new TRowKey[numArray2.Length];
      for (int index = 0; index < rowKeyArray1.Length; ++index)
        rowKeyArray1[index] = fsharpFunc1.Invoke(numArray2[index]);
      TRowKey[] rowKeyArray2 = rowKeyArray1;
      int[] numArray3 = indices;
      FSharpFunc<int, ISeries<TColumnKey>> fsharpFunc2 = (FSharpFunc<int, ISeries<TColumnKey>>) new FrameExtensions.values0<TColumnKey, TRowKey>(frame);
      int[] numArray4 = numArray3;
      if ((object) numArray4 == null)
        throw new ArgumentNullException("array");
      ISeries<TColumnKey>[] seriesArray1 = new ISeries<TColumnKey>[numArray4.Length];
      for (int index = 0; index < seriesArray1.Length; ++index)
        seriesArray1[index] = fsharpFunc2.Invoke(numArray4[index]);
      ISeries<TColumnKey>[] seriesArray2 = seriesArray1;
      return FFrameextensions.FrameofRowsStatic<TRowKey, ISeries<TColumnKey>, TColumnKey>((IEnumerable<Tuple<TRowKey, ISeries<TColumnKey>>>) SeqModule.Zip<TRowKey, ISeries<TColumnKey>>((IEnumerable<M0>) rowKeyArray2, (IEnumerable<M1>) seriesArray2));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Frame<TRowKey, Tuple<TColKey1, TColKey2>> GetSlice<TRowKey, TColKey1, TColKey2>(this ColumnSeries<TRowKey, Tuple<TColKey1, TColKey2>> series, FSharpOption<TColKey1> lo1, FSharpOption<TColKey1> hi1, FSharpOption<TColKey2> lo2, FSharpOption<TColKey2> hi2)
    {
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TColKey1>>((M0) lo1, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TColKey1>>((M0) hi1, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TColKey2>>((M0) lo2, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TColKey2>>((M0) hi2, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      return series.GetByLevel((ICustomLookup<Tuple<TColKey1, TColKey2>>) new SimpleLookup<Tuple<TColKey1, TColKey2>>(new FSharpOption<object>[2]
      {
        (FSharpOption<object>) OptionModule.Map<TColKey1, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice7<TColKey1>(), (FSharpOption<M0>) lo1),
        (FSharpOption<object>) OptionModule.Map<TColKey2, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice7<TColKey2>(), (FSharpOption<M0>) lo2)
      }));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Frame<TRowKey, Tuple<TColKey1, TColKey2>> GetSlice<TRowKey, TColKey1, TColKey2>(this ColumnSeries<TRowKey, Tuple<TColKey1, TColKey2>> series, FSharpOption<TColKey1> lo1, FSharpOption<TColKey1> hi1, TColKey2 k2)
    {
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TColKey1>>((M0) lo1, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TColKey1>>((M0) hi1, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      return series.GetByLevel((ICustomLookup<Tuple<TColKey1, TColKey2>>) new SimpleLookup<Tuple<TColKey1, TColKey2>>(new FSharpOption<object>[2]
      {
        (FSharpOption<object>) OptionModule.Map<TColKey1, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice2<TColKey1>(), (FSharpOption<M0>) lo1),
        FSharpOption<object>.Some((object) k2)
      }));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Frame<TRowKey, Tuple<TColKey1, TColKey2>> GetSlice<TRowKey, TColKey1, TColKey2>(this ColumnSeries<TRowKey, Tuple<TColKey1, TColKey2>> series, TColKey1 k1, FSharpOption<TColKey2> lo2, FSharpOption<TColKey2> hi2)
    {
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TColKey2>>((M0) lo2, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TColKey2>>((M0) hi2, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      return series.GetByLevel((ICustomLookup<Tuple<TColKey1, TColKey2>>) new SimpleLookup<Tuple<TColKey1, TColKey2>>(new FSharpOption<object>[2]
      {
        FSharpOption<object>.Some((object) k1),
        (FSharpOption<object>) OptionModule.Map<TColKey2, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice7<TColKey2>(), (FSharpOption<M0>) lo2)
      }));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Frame<TRowKey, Tuple<TColKey1, TColKey2>> GetSlice<TRowKey, TColKey1, TColKey2, K1, K2>(this ColumnSeries<TRowKey, Tuple<TColKey1, TColKey2>> series, FSharpOption<K1> lo1, FSharpOption<K1> hi1, FSharpOption<K2> lo2, FSharpOption<K2> hi2)
    {
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K1>>((M0) lo1, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K1>>((M0) hi1, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K2>>((M0) lo2, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K2>>((M0) hi2, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      return series.GetByLevel((ICustomLookup<Tuple<TColKey1, TColKey2>>) new SimpleLookup<Tuple<TColKey1, TColKey2>>(new FSharpOption<object>[2]
      {
        (FSharpOption<object>) OptionModule.Map<K1, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice3<K1>(), (FSharpOption<M0>) lo1),
        (FSharpOption<object>) OptionModule.Map<K2, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice3<K2>(), (FSharpOption<M0>) lo2)
      }));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Frame<Tuple<TRowKey1, TRowKey2>, TColKey> GetSlice<TRowKey1, TRowKey2, TColKey>(this RowSeries<Tuple<TRowKey1, TRowKey2>, TColKey> series, FSharpOption<TRowKey1> lo1, FSharpOption<TRowKey1> hi1, FSharpOption<TRowKey2> lo2, FSharpOption<TRowKey2> hi2)
    {
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TRowKey1>>((M0) lo1, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TRowKey1>>((M0) hi1, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TRowKey2>>((M0) lo2, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TRowKey2>>((M0) hi2, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      return series.GetByLevel((ICustomLookup<Tuple<TRowKey1, TRowKey2>>) new SimpleLookup<Tuple<TRowKey1, TRowKey2>>(new FSharpOption<object>[2]
      {
        (FSharpOption<object>) OptionModule.Map<TRowKey1, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice90<TRowKey1>(), lo1),
        (FSharpOption<object>) OptionModule.Map<TRowKey2, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice91<TRowKey2>(), (FSharpOption<M0>) lo2)
      }));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Frame<Tuple<TRowKey1, TRowKey2>, TColKey> GetSlice<TRowKey1, TRowKey2, TColKey>(this RowSeries<Tuple<TRowKey1, TRowKey2>, TColKey> series, FSharpOption<TRowKey1> lo1, FSharpOption<TRowKey1> hi1, TRowKey2 k2)
    {
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TRowKey1>>((M0) lo1, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TRowKey1>>((M0) hi1, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      return series.GetByLevel((ICustomLookup<Tuple<TRowKey1, TRowKey2>>) new SimpleLookup<Tuple<TRowKey1, TRowKey2>>(new FSharpOption<object>[2]
      {
        (FSharpOption<object>) OptionModule.Map<TRowKey1, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice42<TRowKey1>(), lo1),
        FSharpOption<object>.Some((object) k2)
      }));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Frame<Tuple<TRowKey1, TRowKey2>, TColKey> GetSlice<TRowKey1, TRowKey2, TColKey>(this RowSeries<Tuple<TRowKey1, TRowKey2>, TColKey> series, TRowKey1 k1, FSharpOption<TRowKey2> lo2, FSharpOption<TRowKey2> hi2)
    {
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TRowKey2>>((M0) lo2, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<TRowKey2>>((M0) hi2, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      return series.GetByLevel((ICustomLookup<Tuple<TRowKey1, TRowKey2>>) new SimpleLookup<Tuple<TRowKey1, TRowKey2>>(new FSharpOption<object>[2]
      {
        FSharpOption<object>.Some((object) k1),
        (FSharpOption<object>) OptionModule.Map<TRowKey2, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice93<TRowKey2>(), (FSharpOption<M0>) lo2)
      }));
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static Frame<Tuple<TRowKey1, TRowKey2>, TColKey> GetSlice<TRowKey1, TRowKey2, TColKey, K1, K2>(this RowSeries<Tuple<TRowKey1, TRowKey2>, TColKey> series, FSharpOption<K1> lo1, FSharpOption<K1> hi1, FSharpOption<K2> lo2, FSharpOption<K2> hi2)
    {
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K1>>((M0) lo1, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K1>>((M0) hi1, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      if ((LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K2>>((M0) lo2, (M0) null) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<K2>>((M0) hi2, (M0) null) ? 1 : 0) : 1) != 0)
        throw new InvalidOperationException("Slicing on level of a hierarchical indices is not supported");
      return series.GetByLevel((ICustomLookup<Tuple<TRowKey1, TRowKey2>>) new SimpleLookup<Tuple<TRowKey1, TRowKey2>>(new FSharpOption<object>[2]
      {
        (FSharpOption<object>) OptionModule.Map<K1, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice54<K1>(), (FSharpOption<M0>) lo1),
        (FSharpOption<object>) OptionModule.Map<K2, object>((FSharpFunc<M0, M1>) new FrameExtensions.GetSlice55<K2>(), (FSharpOption<M0>) lo2)
      }));
    }

    public static Frame<TRowKey, TColumnKey> FillMissing<TRowKey, TColumnKey, T>(this Frame<TRowKey, TColumnKey> frame, T value)
    {
      return FrameModule.FillMissingWith<T, TRowKey, TColumnKey>(value, frame);
    }

    public static Frame<TRowKey, TColumnKey> FillMissing<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame, Direction direction)
    {
      return FrameModule.FillMissing<TRowKey, TColumnKey>(direction, frame);
    }

    public static Frame<TRowKey, TColumnKey> FillMissing<TRowKey, TColumnKey, T>(this Frame<TRowKey, TColumnKey> frame, Func<Series<TRowKey, T>, TRowKey, T> f)
    {
      return FrameModule.FillMissingUsing<TRowKey, T, TColumnKey>((FSharpFunc<Series<TRowKey, T>, FSharpFunc<TRowKey, T>>) new FrameExtensions.FillMissing2<TRowKey, T>(f), frame);
    }

    public static Frame<TRowKey, TColumnKey> DropSparseRows<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame)
    {
      return FrameModule.DropSparseRows<TRowKey, TColumnKey>(frame);
    }

    public static Frame<TRowKey, TColumnKey> DropSparseColumns<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame)
    {
      return FrameModule.DropSparseColumns<TRowKey, TColumnKey>(frame);
    }

    [Obsolete("Use SortByKeys instead. This function will be removed in futrue versions.")]
    public static Frame<TRowKey, TColumnKey> OrderRows<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame)
    {
      return FrameModule.SortRowsByKey<TRowKey, TColumnKey>(frame);
    }

    [Obsolete("Use SortByKeys instead. This function will be removed in futrue versions.")]
    public static Frame<TRowKey, TColumnKey> SortByRowKey<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame)
    {
      return FrameModule.SortRowsByKey<TRowKey, TColumnKey>(frame);
    }

    [Obsolete("Use SortByKeys instead. This function will be removed in futrue versions.")]
    public static Frame<TRowKey, TColumnKey> OrderColumns<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame)
    {
      return FrameModule.SortColumnsByKey<TRowKey, TColumnKey>(frame);
    }

    [Obsolete("Use SortByKeys instead. This function will be removed in futrue versions.")]
    public static Frame<TRowKey, TColumnKey> SortByColKey<TRowKey, TColumnKey>(this Frame<TRowKey, TColumnKey> frame)
    {
      return FrameModule.SortColumnsByKey<TRowKey, TColumnKey>(frame);
    }

    [Obsolete("Use overload taking TextWriter instead")]
    public static void SaveCsv<R, C>(this Frame<R, C> frame, Stream stream, [Optional] bool includeRowKeys, [Optional] IEnumerable<string> keyNames, [Optional] char separator, [Optional] CultureInfo culture)
    {
      FSharpOption<char> separatorOpt = separator != char.MinValue ? FSharpOption<char>.Some(separator) : (FSharpOption<char>) null;
      FSharpOption<CultureInfo> cultureOpt = !LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<CultureInfo>((M0) culture, (M0) null) ? FSharpOption<CultureInfo>.Some(culture) : (FSharpOption<CultureInfo>) null;
      FSharpOption<IEnumerable<string>> rowKeyNames = !LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<IEnumerable<string>>((M0) keyNames, (M0) null) ? FSharpOption<IEnumerable<string>>.Some(keyNames) : (FSharpOption<IEnumerable<string>>) null;
      StreamWriter streamWriter = new StreamWriter(stream);
      try
      {
        FrameUtilsModule.writeCsv<R, C>((TextWriter) streamWriter, (FSharpOption<string>) null, separatorOpt, cultureOpt, FSharpOption<bool>.Some(includeRowKeys), rowKeyNames, frame);
      }
      finally
      {
        (streamWriter as IDisposable)?.Dispose();
      }
    }
  }
}
