// Decompiled with JetBrains decompiler
// Type: Deedle.FrameModule
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Indices;
using Deedle.Vectors;

using Microsoft.FSharp.Control;

using Microsoft.FSharp.Core.CompilerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deedle
{
  [CompilationRepresentation]
  
  public static class FrameModule
  {
    [CompilationSourceName("countRows")]
    public static int CountRows<R, C>(Frame<R, C> frame)
    {
      return (int) frame.RowIndex.KeyCount;
    }

    [CompilationSourceName("countCols")]
    public static int CountColumns<R, C>(Frame<R, C> frame)
    {
      return (int) frame.ColumnIndex.KeyCount;
    }

    [CompilationSourceName("countValues")]
    public static Series<C, int> CountValues<R, C>(Frame<R, C> frame)
    {
      return SeriesModule.Map<C, ObjectSeries<R>, int>((FSharpFunc<C, FSharpFunc<ObjectSeries<R>, int>>) new FrameModule.CountValues<R, C>(), (Series<C, ObjectSeries<R>>) frame.Columns);
    }

    [CompilationSourceName("cols")]
    public static ColumnSeries<R, C> Columns<R, C>(Frame<R, C> frame)
    {
      return frame.Columns;
    }

    
    [CompilationSourceName("getCol")]
    public static Series<R, V> GetColumn<C, R, V>(C column, Frame<R, C> frame)
    {
      return frame.GetColumn<V>(column);
    }

    [CompilationSourceName("getCols")]
    public static Series<C, Series<R, T>> GetColumns<R, C, T>(Frame<R, C> frame)
    {
      return SeriesModule.Flatten<C, Series<R, T>>(SeriesModule.Map<C, ObjectSeries<R>, FSharpOption<Series<R, T>>>((FSharpFunc<C, FSharpFunc<ObjectSeries<R>, FSharpOption<Series<R, T>>>>) new FrameModule.GetColumns<R, C, T>(), (Series<C, ObjectSeries<R>>) frame.Columns));
    }

    [CompilationSourceName("getNumericCols")]
    public static Series<C, Series<R, double>> GetNumericColumns<R, C>(Frame<R, C> frame)
    {
      return FrameModule.GetColumns<R, C, double>(frame);
    }

    [CompilationSourceName("rows")]
    public static RowSeries<R, C> Rows<R, C>(Frame<R, C> frame)
    {
      return frame.Rows;
    }

    
    [CompilationSourceName("getRow")]
    public static Series<C, a> GetRow<R, C, a>(R row, Frame<R, C> frame)
    {
      return frame.GetRow<a>(row);
    }

    [CompilationSourceName("getRows")]
    public static Series<R, Series<C, T>> GetRows<R, C, T>(Frame<R, C> frame)
    {
      return SeriesModule.Flatten<R, Series<C, T>>(SeriesModule.Map<R, ObjectSeries<C>, FSharpOption<Series<C, T>>>((FSharpFunc<R, FSharpFunc<ObjectSeries<C>, FSharpOption<Series<C, T>>>>) new FrameModule.GetRows<R, C, T>(), (Series<R, ObjectSeries<C>>) frame.Rows));
    }

    
    [CompilationSourceName("lookupCol")]
    public static Series<R, V> LookupColumn<C, R, V>(C column, Lookup lookup, Frame<R, C> frame)
    {
      return frame.GetColumn<V>(column, lookup);
    }

    
    [CompilationSourceName("tryLookupCol")]
    public static FSharpOption<Series<R, V>> TryLookupColumn<C, R, V>(C column, Lookup lookup, Frame<R, C> frame)
    {
      OptionalValue<Series<R, V>> column1 = frame.TryGetColumn<V>(column, lookup);
      if (column1.HasValue)
        return FSharpOption<Series<R, V>>.Some(column1.Value);
      return (FSharpOption<Series<R, V>>) null;
    }

    
    [CompilationSourceName("tryLookupColObservation")]
    public static FSharpOption<Tuple<C, Series<R, a>>> TryLookupColObservation<C, R, a>(C column, Lookup lookup, Frame<R, C> frame)
    {
      FrameModule.TryLookupColObservation<R, C, a> colObservation347 = new FrameModule.TryLookupColObservation<R, C, a>();
      OptionalValue<KeyValuePair<C, Series<R, a>>> columnObservation = frame.TryGetColumnObservation<a>(column, lookup);
      FSharpOption<KeyValuePair<C, Series<R, a>>> fsharpOption = !columnObservation.HasValue ? (FSharpOption<KeyValuePair<C, Series<R, a>>>) null : FSharpOption<KeyValuePair<C, Series<R, a>>>.Some(columnObservation.Value);
      return (FSharpOption<Tuple<C, Series<R, a>>>) OptionModule.Map<KeyValuePair<C, Series<R, a>>, Tuple<C, Series<R, a>>>((FSharpFunc<M0, M1>) colObservation347, (FSharpOption<M0>) fsharpOption);
    }

    
    [CompilationSourceName("lookupRow")]
    public static Series<C, a> LookupRow<R, C, a>(R row, Lookup lookup, Frame<R, C> frame)
    {
      return frame.GetRow<a>(row, lookup);
    }

    
    [CompilationSourceName("tryLookupRow")]
    public static FSharpOption<Series<C, a>> TryLookupRow<R, C, a>(R row, Lookup lookup, Frame<R, C> frame)
    {
      OptionalValue<Series<C, a>> row1 = frame.TryGetRow<a>(row, lookup);
      if (row1.HasValue)
        return FSharpOption<Series<C, a>>.Some(row1.Value);
      return (FSharpOption<Series<C, a>>) null;
    }

    
    [CompilationSourceName("tryLookupRowObservation")]
    public static FSharpOption<Tuple<R, Series<C, a>>> TryLookupRowObservation<R, C, a>(R row, Lookup lookup, Frame<R, C> frame)
    {
      FrameModule.TryLookupRowObservation<R, C, a> rowObservation371 = new FrameModule.TryLookupRowObservation<R, C, a>();
      OptionalValue<KeyValuePair<R, Series<C, a>>> rowObservation = frame.TryGetRowObservation<a>(row, lookup);
      FSharpOption<KeyValuePair<R, Series<C, a>>> fsharpOption = !rowObservation.HasValue ? (FSharpOption<KeyValuePair<R, Series<C, a>>>) null : FSharpOption<KeyValuePair<R, Series<C, a>>>.Some(rowObservation.Value);
      return (FSharpOption<Tuple<R, Series<C, a>>>) OptionModule.Map<KeyValuePair<R, Series<C, a>>, Tuple<R, Series<C, a>>>((FSharpFunc<M0, M1>) rowObservation371, (FSharpOption<M0>) fsharpOption);
    }

    
    [CompilationSourceName("sliceCols")]
    public static Frame<R, C> SliceCols<C, R>(IEnumerable<C> columns, Frame<R, C> frame)
    {
      return frame.Columns[columns];
    }

    
    [CompilationSourceName("sliceRows")]
    public static Frame<R, C> SliceRows<R, C>(IEnumerable<R> rows, Frame<R, C> frame)
    {
      return frame.Rows[rows];
    }

    
    [CompilationSourceName("addCol")]
    public static Frame<R, C> AddColumn<C, R, V>(C column, Series<R, V> series, Frame<R, C> frame)
    {
      Frame<R, C> frame1 = frame.Clone();
      frame1.AddColumn(column, (ISeries<R>) series);
      return frame1;
    }

    
    [CompilationSourceName("dropCol")]
    public static Frame<R, C> DropColumn<C, R>(C column, Frame<R, C> frame)
    {
      Frame<R, C> frame1 = frame.Clone();
      frame1.DropColumn(column);
      return frame1;
    }

    
    [CompilationSourceName("replaceCol")]
    public static Frame<R, C> ReplaceColumn<C, R>(C column, ISeries<R> series, Frame<R, C> frame)
    {
      Frame<R, C> frame1 = frame.Clone();
      frame1.ReplaceColumn(column, series);
      return frame1;
    }

    [CompilationSourceName("toArray2D")]
    public static double[,] ToArray2D<R, C>(Frame<R, C> frame)
    {
      return frame.ToArray2D<double>();
    }

    
    [CompilationSourceName("groupRowsUsing")]
    public static Frame<Tuple<K, R>, C> GroupRowsUsing<R, C, K>(FSharpFunc<R, FSharpFunc<ObjectSeries<C>, K>> selector, Frame<R, C> frame)
    {
      return frame.GroupRowsUsing<K>(new Func<R, ObjectSeries<C>, K>(new FrameModule.GroupRowsUsing<K, R, C>(selector).Invoke));
    }

    
    [CompilationSourceName("groupRowsBy")]
    public static Frame<Tuple<K, R>, C> GroupRowsBy<C, R, K>(C column, Frame<R, C> frame)
    {
      return frame.GroupRowsBy<K>(column);
    }

    
    [CompilationSourceName("groupRowsByObj")]
    public static Frame<Tuple<object, R>, C> GroupRowsByObj<C, R>(C column, Frame<R, C> frame)
    {
      return FrameModule.GroupRowsBy<C, R, object>(column, frame);
    }

    
    [CompilationSourceName("groupRowsByInt")]
    public static Frame<Tuple<int, R>, C> GroupRowsByInt<C, R>(C column, Frame<R, C> frame)
    {
      return FrameModule.GroupRowsBy<C, R, int>(column, frame);
    }

    
    [CompilationSourceName("groupRowsByString")]
    public static Frame<Tuple<string, R>, C> GroupRowsByString<C, R>(C column, Frame<R, C> frame)
    {
      return FrameModule.GroupRowsBy<C, R, string>(column, frame);
    }

    
    [CompilationSourceName("groupRowsByBool")]
    public static Frame<Tuple<bool, R>, C> GroupRowsByBool<C, R>(C column, Frame<R, C> frame)
    {
      return FrameModule.GroupRowsBy<C, R, bool>(column, frame);
    }

    
    [CompilationSourceName("groupRowsByIndex")]
    public static Frame<Tuple<K, R>, C> GroupRowsByIndex<R, K, C>(FSharpFunc<R, K> keySelector, Frame<R, C> frame)
    {
      return frame.GroupRowsByIndex<K>(new Func<R, K>(new FrameModule.GroupRowsByIndex<K, R>(keySelector).Invoke));
    }

    
    [CompilationSourceName("aggregateRowsBy")]
    public static Frame<int, C> AggregateRowsBy<C, R, V1, V2>(IEnumerable<C> groupBy, IEnumerable<C> aggBy, FSharpFunc<Series<R, V1>, V2> aggFunc, Frame<R, C> frame)
    {
      return frame.AggregateRowsBy<V1, V2>(groupBy, aggBy, new Func<Series<R, V1>, V2>(new FrameModule.AggregateRowsBy<R, V1, V2>(aggFunc).Invoke));
    }

    
    [CompilationSourceName("pivotTable")]
    public static Frame<RNew, CNew> PivotTable<R, C, RNew, CNew, T>(FSharpFunc<R, FSharpFunc<ObjectSeries<C>, RNew>> rowGrp, FSharpFunc<R, FSharpFunc<ObjectSeries<C>, CNew>> colGrp, FSharpFunc<Frame<R, C>, T> op, Frame<R, C> frame)
    {
      FSharpFunc<Series<R, ObjectSeries<C>>, Frame<R, C>> f = (FSharpFunc<Series<R, ObjectSeries<C>>, Frame<R, C>>) new FrameModule.fromRows<R, C>(frame.IndexBuilder, frame.VectorBuilder);
      RowSeries<R, C> rows = frame.Rows;
      Series<CNew, Series<R, ObjectSeries<C>>> series1 = SeriesModule.groupInto<R, ObjectSeries<C>, CNew, Series<R, ObjectSeries<C>>>((FSharpFunc<R, FSharpFunc<ObjectSeries<C>, CNew>>) new FrameModule.PivotTable<R, C, CNew>(colGrp), (FSharpFunc<CNew, FSharpFunc<Series<R, ObjectSeries<C>>, Series<R, ObjectSeries<C>>>>) new FrameModule.PivotTable<R, C, CNew>(), (Series<R, ObjectSeries<C>>) rows);
      Series<CNew, Series<RNew, Series<R, ObjectSeries<C>>>> series2 = SeriesModule.MapValues<Series<R, ObjectSeries<C>>, Series<RNew, Series<R, ObjectSeries<C>>>, CNew>((FSharpFunc<Series<R, ObjectSeries<C>>, Series<RNew, Series<R, ObjectSeries<C>>>>) new FrameModule.PivotTable<R, C, RNew>((FSharpFunc<R, FSharpFunc<ObjectSeries<C>, RNew>>) new FrameModule.PivotTable<R, C, RNew>(rowGrp), (FSharpFunc<RNew, FSharpFunc<Series<R, ObjectSeries<C>>, Series<R, ObjectSeries<C>>>>) new FrameModule.PivotTable<R, C, RNew>()), series1);
      Series<CNew, Series<RNew, T>> nested = SeriesModule.MapValues<Series<RNew, Series<R, ObjectSeries<C>>>, Series<RNew, T>, CNew>((FSharpFunc<Series<RNew, Series<R, ObjectSeries<C>>>, Series<RNew, T>>) new FrameModule.PivotTable<R, C, RNew, T>((FSharpFunc<Series<R, ObjectSeries<C>>, T>) new FrameModule.PivotTable<R, C, T>(f, op)), series2);
      return FrameUtils.fromColumns<RNew, CNew, Series<RNew, T>>(frame.IndexBuilder, frame.VectorBuilder, nested);
    }

    
    [CompilationSourceName("window")]
    public static Series<R, Frame<R, C>> Window<R, C>(int size, Frame<R, C> frame)
    {
      object obj = (object) new FrameModule.fromRows<C, R>(frame);
      RowSeries<R, C> rows = frame.Rows;
      return SeriesModule.WindowSizeInto<R, ObjectSeries<C>, Frame<R, C>>(size, Boundary.Skip, (FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, Frame<R, C>>) new FrameModule.Window<R, C>((FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, Series<R, ObjectSeries<C>>>) new FrameModule.Window<R, C>(), ((FrameModule.fromRowscontract<C>) obj).DirectInvoke<R, ObjectSeries<C>>()), (Series<R, ObjectSeries<C>>) rows);
    }

    
    [CompilationSourceName("windowInto")]
    public static Series<R, a> WindowInto<R, C, a>(int size, FSharpFunc<Frame<R, C>, a> f, Frame<R, C> frame)
    {
      object obj = (object) new FrameModule.fromRows<C, R>(frame);
      RowSeries<R, C> rows = frame.Rows;
      return SeriesModule.WindowSizeInto<R, ObjectSeries<C>, a>(size, Boundary.Skip, (FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, a>) new FrameModule.WindowInto<R, C, a>((FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, Series<R, ObjectSeries<C>>>) new FrameModule.WindowInto<R, C>(), (FSharpFunc<Series<R, ObjectSeries<C>>, a>) new FrameModule.WindowInto<R, C, a>(((FrameModule.fromRowscontract<C>) obj).DirectInvoke<R, ObjectSeries<C>>(), f)), (Series<R, ObjectSeries<C>>) rows);
    }

    [CompilationSourceName("melt")]
    public static Frame<int, string> Melt<R, C>(Frame<R, C> frame)
    {
      ReadOnlyCollection<R> keys1 = frame.RowIndex.Keys;
      ReadOnlyCollection<C> keys2 = frame.ColumnIndex.Keys;
      List<R> rList = new List<R>();
      List<C> cList = new List<C>();
      List<object> objectList = new List<object>();
      IEnumerator<R> enumerator1 = keys1.GetEnumerator();
      try
      {
        while (enumerator1.MoveNext())
        {
          R current1 = enumerator1.Current;
          IEnumerator<C> enumerator2 = keys2.GetEnumerator();
          try
          {
            while (enumerator2.MoveNext())
            {
              C current2 = enumerator2.Current;
              OptionalValue<IVector> optionalValue1 = frame.Data.GetValue(frame.ColumnIndex.Locate(current2));
              if (optionalValue1.HasValue)
              {
                OptionalValue<object> optionalValue2 = optionalValue1.Value.GetObject(frame.RowIndex.Locate(current1));
                if (optionalValue2.HasValue)
                {
                  rList.Add(current1);
                  cList.Add(current2);
                  objectList.Add(optionalValue2.Value);
                }
              }
            }
          }
          finally
          {
            (enumerator2 as IDisposable)?.Dispose();
          }
        }
      }
      finally
      {
        (enumerator1 as IDisposable)?.Dispose();
      }
      Type commonSupertype = VectorHelpers.findCommonSupertype<Type>((IEnumerable<Type>) SeqModule.Choose<OptionalValue<IVector>, Type>((FSharpFunc<M0, FSharpOption<M1>>) new FrameModule.valTyp(), (IEnumerable<M0>) FVectorextensionscore.IVector`1get_DataSequence<IVector>(frame.Data)));
      IIndex<string> columnIndex = FIndexextensions.Index.ofKeys<string>(FSharpList<string>.Cons("Row", FSharpList<string>.Cons("Column", FSharpList<string>.Cons("Value", FSharpList<string>.get_Empty()))));
      int count = objectList.Count;
      FSharpFunc<int, int> fsharpFunc = (FSharpFunc<int, int>) new FrameModule.rowIndex();
      if (count < 0)
        throw new ArgumentException(string.Format("{0}\n{1} = {2}", new object[3]{ (object) LanguagePrimitives.ErrorStrings.get_InputMustBeNonNegativeString(), (object) "count", (object) count }), "count");
      int[] keys3 = new int[count];
      for (int index = 0; index < keys3.Length; ++index)
        keys3[index] = fsharpFunc.Invoke(index);
      IIndex<int> rowIndex = FIndexextensions.Index.ofKeys<int>(keys3);
      IVector<IVector> data = FVectorBuilderimplementation.VectorBuilder.Instance.Create<IVector>((IVector[]) ArrayModule.OfSeq<IVector>((IEnumerable<M0>) FSharpList<IVector>.Cons((IVector) FVectorBuilderimplementation.VectorBuilder.Instance.Create<R>(rList.ToArray()), FSharpList<IVector>.Cons((IVector) FVectorBuilderimplementation.VectorBuilder.Instance.Create<C>(cList.ToArray()), FSharpList<IVector>.Cons(VectorHelpers.createTypedVector(frame.VectorBuilder, commonSupertype, objectList.ToArray()), FSharpList<IVector>.get_Empty())))));
      return new Frame<int, string>(rowIndex, columnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    [Obsolete("The Frame.Stack method has been renamed to Frame.Melt")]
    [CompilationSourceName("stack")]
    public static Frame<int, string> Stack<R, C>(Frame<R, C> frame)
    {
      return FrameModule.Melt<R, C>(frame);
    }

    [CompilationSourceName("unmelt")]
    public static Frame<R, C> Unmelt<O, R, C>(Frame<O, string> frame)
    {
      return FrameUtilsModule.fromValues<ObjectSeries<string>, C, R, object>(frame.Rows.Values, (FSharpFunc<ObjectSeries<string>, C>) new FrameModule.Unmelt<C>(), (FSharpFunc<ObjectSeries<string>, R>) new FrameModule.Unmelt<R>(), (FSharpFunc<ObjectSeries<string>, object>) new FrameModule.Unmelt());
    }

    [Obsolete("The Frame.UnStack method has been renamed to Frame.Unmelt")]
    [CompilationSourceName("unstack")]
    public static Frame<R, C> UnStack<O, R, C>(Frame<O, string> frame)
    {
      return FrameModule.Unmelt<O, R, C>(frame);
    }

    
    [CompilationSourceName("realignRows")]
    public static Frame<R, C> RealignRows<R, C>(IEnumerable<R> keys, Frame<R, C> frame)
    {
      IIndex<R> index = FIndexextensions.Index.ofKeys<R>(Array.AsReadOnly<R>(ArrayModule.OfSeq<R>(keys)));
      VectorConstruction relocs = frame.IndexBuilder.Reindex<R>(frame.RowIndex, index, Lookup.Exact, VectorConstruction.NewReturn(0), (FSharpFunc<long, bool>) new FrameModule.relocs6());
      FSharpFunc<IVector, IVector> f = (FSharpFunc<IVector, IVector>) new FrameModule.cmd<R, C>(frame, index, relocs);
      return new Frame<R, C>(index, frame.ColumnIndex, FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, f), frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("indexRows")]
    public static Frame<R2, C> IndexRows<C, R1, R2>(C column, Frame<R1, C> frame)
    {
      return frame.IndexRows<R2>(column);
    }

    
    [CompilationSourceName("indexRowsObj")]
    public static Frame<object, C> IndexRowsByObject<C, R1>(C column, Frame<R1, C> frame)
    {
      return FrameModule.IndexRows<C, R1, object>(column, frame);
    }

    
    [CompilationSourceName("indexRowsInt")]
    public static Frame<int, C> IndexRowsByInt<C, R1>(C column, Frame<R1, C> frame)
    {
      return FrameModule.IndexRows<C, R1, int>(column, frame);
    }

    
    [CompilationSourceName("indexRowsDate")]
    public static Frame<DateTime, C> IndexRowsByDateTime<C, R1>(C column, Frame<R1, C> frame)
    {
      return FrameModule.IndexRows<C, R1, DateTime>(column, frame);
    }

    
    [CompilationSourceName("indexRowsDateOffs")]
    public static Frame<DateTimeOffset, C> IndexRowsByDateTimeOffset<C, R1>(C column, Frame<R1, C> frame)
    {
      return FrameModule.IndexRows<C, R1, DateTimeOffset>(column, frame);
    }

    
    [CompilationSourceName("indexRowsString")]
    public static Frame<string, C> IndexRowsByString<C, R1>(C column, Frame<R1, C> frame)
    {
      return FrameModule.IndexRows<C, R1, string>(column, frame);
    }

    
    [CompilationSourceName("indexColsWith")]
    public static Frame<R, C2> IndexColumnsWith<C2, R, C1>(IEnumerable<C2> keys, Frame<R, C1> frame)
    {
      if (SeqModule.Length<C1>((IEnumerable<M0>) frame.ColumnKeys) != SeqModule.Length<C2>(keys))
        throw new ArgumentException("New keys do not match current column index length", nameof (keys));
      return new Frame<R, C2>(frame.RowIndex, FIndexextensions.Index.ofKeys<C2>(Array.AsReadOnly<C2>(ArrayModule.OfSeq<C2>(keys))), frame.Data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("indexRowsWith")]
    public static Frame<R2, C> IndexRowsWith<R2, R1, C>(IEnumerable<R2> keys, Frame<R1, C> frame)
    {
      IIndex<R2> rowIndex = frame.IndexBuilder.Create<R2>(keys, (FSharpOption<bool>) null);
      VectorConstruction range = VectorConstruction.NewGetRange(VectorConstruction.NewReturn(0), RangeRestriction<long>.NewFixed(frame.RowIndex.AddressAt(0L), frame.RowIndex.AddressAt(frame.RowIndex.KeyCount - 1L)));
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData2(frame.VectorBuilder, rowIndex.AddressingScheme, range));
      return new Frame<R2, C>(rowIndex, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    [CompilationSourceName("indexRowsOrdinally")]
    public static Frame<int, TColumnKey> IndexRowsOrdinally<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      return FrameModule.IndexRowsWith<int, TRowKey, TColumnKey>((IEnumerable<int>) SeqModule.ToList<int>(Operators.CreateSequence<int>((IEnumerable<M0>) Operators.OperatorIntrinsics.RangeInt32(0, 1, frame.RowCount - 1))), frame1);
    }

    
    [CompilationSourceName("indexRowsUsing")]
    public static Frame<R2, C> IndexRowsUsing<C, R2, R1>(FSharpFunc<ObjectSeries<C>, R2> f, Frame<R1, C> frame)
    {
      RowSeries<R1, C> rows = frame.Rows;
      return FrameModule.IndexRowsWith<R2, R1, C>(SeriesModule.GetValues<R1, R2>(SeriesModule.Map<R1, ObjectSeries<C>, R2>((FSharpFunc<R1, FSharpFunc<ObjectSeries<C>, R2>>) new FrameModule.IndexRowsUsing<C, R2, R1>(f), (Series<R1, ObjectSeries<C>>) rows)), frame);
    }

    [CompilationSourceName("transpose")]
    public static Frame<TColumnKey, R> Transpose<R, TColumnKey>(Frame<R, TColumnKey> frame)
    {
      ColumnSeries<R, TColumnKey> columns = frame.Columns;
      return FrameUtils.fromRows<TColumnKey, R, ObjectSeries<R>>(frame.IndexBuilder, frame.VectorBuilder, (Series<TColumnKey, ObjectSeries<R>>) columns);
    }

    [CompilationSourceName("sortRowsByKey")]
    public static Frame<R, C> SortRowsByKey<R, C>(Frame<R, C> frame)
    {
      Tuple<IIndex<R>, VectorConstruction> tuple = frame.IndexBuilder.OrderIndex<R>(new Tuple<IIndex<R>, VectorConstruction>(frame.RowIndex, VectorConstruction.NewReturn(0)));
      VectorConstruction rowCmd = tuple.Item2;
      IIndex<R> rowIndex = tuple.Item1;
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData3(frame.VectorBuilder, rowIndex.AddressingScheme, rowCmd));
      return new Frame<R, C>(rowIndex, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("sortRows")]
    public static Frame<R, C> SortRows<C, R>(C colKey, Frame<R, C> frame)
    {
      Tuple<IIndex<R>, VectorConstruction> tuple = SeriesModule.sortWithCommand<IComparable, R>((FSharpFunc<IComparable, FSharpFunc<IComparable, int>>) new FrameModule.SortRows(), frame.GetColumn<IComparable>(colKey));
      VectorConstruction rowCmd = tuple.Item2;
      IIndex<R> rowIndex = tuple.Item1;
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData4(frame.VectorBuilder, rowIndex.AddressingScheme, rowCmd));
      return new Frame<R, C>(rowIndex, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("sortRowsWith")]
    public static Frame<R, C> SortRowsWith<C, a, R>(C colKey, FSharpFunc<a, FSharpFunc<a, int>> compareFunc, Frame<R, C> frame)
    {
      Tuple<IIndex<R>, VectorConstruction> tuple = SeriesModule.sortWithCommand<a, R>(compareFunc, frame.GetColumn<a>(colKey));
      VectorConstruction rowCmd = tuple.Item2;
      IIndex<R> rowIndex = tuple.Item1;
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData5(frame.VectorBuilder, rowIndex.AddressingScheme, rowCmd));
      return new Frame<R, C>(rowIndex, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("sortRowsBy")]
    public static Frame<R, C> SortRowBy<C, T, V, R>(C colKey, FSharpFunc<T, V> f, Frame<R, C> frame)
    {
      Tuple<IIndex<R>, VectorConstruction> tuple = SeriesModule.sortByCommand<T, V, R>(f, frame.GetColumn<T>(colKey));
      VectorConstruction rowCmd = tuple.Item2;
      IIndex<R> rowIndex = tuple.Item1;
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData6(frame.VectorBuilder, rowIndex.AddressingScheme, rowCmd));
      return new Frame<R, C>(rowIndex, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    [CompilationSourceName("sortColsByKey")]
    public static Frame<R, C> SortColumnsByKey<R, C>(Frame<R, C> frame)
    {
      Tuple<IIndex<C>, VectorConstruction> tuple = frame.IndexBuilder.OrderIndex<C>(new Tuple<IIndex<C>, VectorConstruction>(frame.ColumnIndex, VectorConstruction.NewReturn(0)));
      IIndex<C> columnIndex = tuple.Item1;
      VectorConstruction vectorConstruction = tuple.Item2;
      IVector<IVector> data = frame.VectorBuilder.Build<IVector>(columnIndex.AddressingScheme, vectorConstruction, new IVector<IVector>[1]{ frame.Data });
      return new Frame<R, C>(frame.RowIndex, columnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("expandAllCols")]
    public static Frame<R, string> ExpandAllColumns<R>(int nesting, Frame<R, string> frame)
    {
      return FrameUtilsModule.expandVectors<R>(nesting, false, frame);
    }

    
    [CompilationSourceName("expandCols")]
    public static Frame<R, string> ExpandColumns<R>(IEnumerable<string> names, Frame<R, string> frame)
    {
      return FrameUtilsModule.expandColumns<R>((FSharpSet<string>) ExtraTopLevelOperators.CreateSet<string>((IEnumerable<M0>) names), frame);
    }

    
    internal static Frame<R, C> getRange<R, C>(long lo, long hi, Frame<R, C> frame)
    {
      if (hi < lo)
      {
        IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData7());
        return new Frame<R, C>(FIndexextensions.Index.ofKeys<R>(FSharpList<R>.get_Empty()), frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
      }
      VectorConstruction range = VectorConstruction.NewGetRange(VectorConstruction.NewReturn(0), RangeRestriction<long>.NewFixed(lo, hi));
      IIndex<R> rowIndex = frame.RowIndex.Builder.GetAddressRange<R>(new Tuple<IIndex<R>, VectorConstruction>(frame.RowIndex, VectorConstruction.NewReturn(0)), RangeRestriction<long>.NewFixed(lo, hi)).Item1;
      IVector<IVector> data1 = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData9(frame.VectorBuilder, rowIndex.AddressingScheme, range));
      return new Frame<R, C>(rowIndex, frame.ColumnIndex, data1, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("take")]
    public static Frame<R, C> Take<R, C>(int count, Frame<R, C> frame)
    {
      if ((count <= frame.RowCount ? (count < 0 ? 1 : 0) : 1) != 0)
        throw new ArgumentException("Must be greater than zero and less than the number of keys.", nameof (count));
      return FrameModule.getRange<R, C>(frame.RowIndex.AddressAt(0L), frame.RowIndex.AddressAt((long) (count - 1)), frame);
    }

    
    [CompilationSourceName("takeLast")]
    public static Frame<R, C> TakeLast<R, C>(int count, Frame<R, C> frame)
    {
      if ((count <= frame.RowCount ? (count < 0 ? 1 : 0) : 1) != 0)
        throw new ArgumentException("Must be greater than zero and less than the number of rows.", nameof (count));
      return FrameModule.getRange<R, C>(frame.RowIndex.AddressAt((long) (frame.RowCount - count)), frame.RowIndex.AddressAt((long) (frame.RowCount - 1)), frame);
    }

    
    [CompilationSourceName("skip")]
    public static Frame<R, C> Skip<R, C>(int count, Frame<R, C> frame)
    {
      if ((count <= frame.RowCount ? (count < 0 ? 1 : 0) : 1) != 0)
        throw new ArgumentException("Must be greater than zero and less than the number of rows.", nameof (count));
      return FrameModule.getRange<R, C>(frame.RowIndex.AddressAt((long) count), frame.RowIndex.AddressAt((long) (frame.RowCount - 1)), frame);
    }

    
    [CompilationSourceName("skipLast")]
    public static Frame<R, C> SkipLast<R, C>(int count, Frame<R, C> frame)
    {
      if ((count <= frame.RowCount ? (count < 0 ? 1 : 0) : 1) != 0)
        throw new ArgumentException("Must be greater than zero and less than the number of keys.", nameof (count));
      return FrameModule.getRange<R, C>(frame.RowIndex.AddressAt(0L), frame.RowIndex.AddressAt((long) (frame.RowCount - 1 - count)), frame);
    }

    
    [CompilationSourceName("filterRows")]
    public static Frame<R, C> WhereRows<R, C>(FSharpFunc<R, FSharpFunc<ObjectSeries<C>, bool>> f, Frame<R, C> frame)
    {
      RowSeries<R, C> rows = frame.Rows;
      Series<R, ObjectSeries<C>> nested = SeriesModule.Filter<R, ObjectSeries<C>>(f, (Series<R, ObjectSeries<C>>) rows);
      return FrameUtils.fromRowsAndColumnKeys<R, C, ObjectSeries<C>>(frame.IndexBuilder, frame.VectorBuilder, frame.ColumnIndex.Keys, nested);
    }

    
    [CompilationSourceName("filterRowValues")]
    public static Frame<R, C> WhereRowValues<C, R>(FSharpFunc<ObjectSeries<C>, bool> f, Frame<R, C> frame)
    {
      RowSeries<R, C> rows = frame.Rows;
      Series<R, ObjectSeries<C>> nested = SeriesModule.FilterValues<ObjectSeries<C>, R>(f, (Series<R, ObjectSeries<C>>) rows);
      return FrameUtils.fromRowsAndColumnKeys<R, C, ObjectSeries<C>>(frame.IndexBuilder, frame.VectorBuilder, frame.ColumnIndex.Keys, nested);
    }

    
    [CompilationSourceName("filterRowsBy")]
    public static Frame<R, C> WhereRowsBy<C, V, R>(C column, V value, Frame<R, C> frame)
    {
      Series<R, V> column1 = frame.GetColumn<V>(column);
      Tuple<IIndex<R>, VectorConstruction> tuple = frame.IndexBuilder.Search<R, V>(new Tuple<IIndex<R>, VectorConstruction>(frame.RowIndex, VectorConstruction.NewReturn(0)), column1.Vector, value);
      IIndex<R> rowIndex = tuple.Item1;
      VectorConstruction rowCmd = tuple.Item2;
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData40(frame.VectorBuilder, rowIndex.AddressingScheme, rowCmd));
      return new Frame<R, C>(rowIndex, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("mapRows")]
    public static Series<R, V> SelectRows<R, C, V>(FSharpFunc<R, FSharpFunc<ObjectSeries<C>, V>> f, Frame<R, C> frame)
    {
      RowSeries<R, C> rows = frame.Rows;
      return SeriesModule.Map<R, ObjectSeries<C>, V>(f, (Series<R, ObjectSeries<C>>) rows);
    }

    
    [CompilationSourceName("mapRowValues")]
    public static Series<R, V> SelectRowValues<C, V, R>(FSharpFunc<ObjectSeries<C>, V> f, Frame<R, C> frame)
    {
      RowSeries<R, C> rows = frame.Rows;
      return SeriesModule.MapValues<ObjectSeries<C>, V, R>(f, (Series<R, ObjectSeries<C>>) rows);
    }

    
    [CompilationSourceName("mapRowKeys")]
    public static Frame<R2, C> SelectRowKeys<R1, R2, C>(FSharpFunc<R1, R2> f, Frame<R1, C> frame)
    {
      IIndexBuilder indexBuilder = frame.IndexBuilder;
      ReadOnlyCollection<R1> keys = frame.RowIndex.Keys;
      IEnumerable<M1> m1s = SeqModule.Map<R1, R2>(f, (IEnumerable<M0>) keys);
      // ISSUE: variable of the null type
      __Null local = null;
      IIndex<R2> rowIndex = indexBuilder.Create<R2>(m1s, (FSharpOption<bool>) local);
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData61(frame.VectorBuilder, rowIndex.AddressingScheme, VectorConstruction.NewReturn(0)));
      return new Frame<R2, C>(rowIndex, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("filterCols")]
    public static Frame<R, C> WhereColumns<C, R>(FSharpFunc<C, FSharpFunc<ObjectSeries<R>, bool>> f, Frame<R, C> frame)
    {
      ColumnSeries<R, C> columns = frame.Columns;
      Series<C, ObjectSeries<R>> nested = SeriesModule.Filter<C, ObjectSeries<R>>(f, (Series<C, ObjectSeries<R>>) columns);
      return FrameUtils.fromColumns<R, C, ObjectSeries<R>>(frame.IndexBuilder, frame.VectorBuilder, nested);
    }

    
    [CompilationSourceName("filterColValues")]
    public static Frame<R, C> WhereColumnValues<R, C>(FSharpFunc<ObjectSeries<R>, bool> f, Frame<R, C> frame)
    {
      ColumnSeries<R, C> columns = frame.Columns;
      Series<C, ObjectSeries<R>> nested = SeriesModule.FilterValues<ObjectSeries<R>, C>(f, (Series<C, ObjectSeries<R>>) columns);
      return FrameUtils.fromColumns<R, C, ObjectSeries<R>>(frame.IndexBuilder, frame.VectorBuilder, nested);
    }

    
    [CompilationSourceName("mapCols")]
    public static Frame<b, C> SelectColumns<C, R, a, b>(FSharpFunc<C, FSharpFunc<ObjectSeries<R>, a>> f, Frame<R, C> frame) where a : ISeries<b>
    {
      ColumnSeries<R, C> columns = frame.Columns;
      Series<C, a> nested = SeriesModule.Map<C, ObjectSeries<R>, a>(f, (Series<C, ObjectSeries<R>>) columns);
      return FrameUtils.fromColumns<b, C, a>(frame.IndexBuilder, frame.VectorBuilder, nested);
    }

    
    [CompilationSourceName("mapColValues")]
    public static Frame<b, C> SelectColumnValues<R, a, b, C>(FSharpFunc<ObjectSeries<R>, a> f, Frame<R, C> frame) where a : ISeries<b>
    {
      ColumnSeries<R, C> columns = frame.Columns;
      Series<C, a> nested = SeriesModule.MapValues<ObjectSeries<R>, a, C>(f, (Series<C, ObjectSeries<R>>) columns);
      return FrameUtils.fromColumns<b, C, a>(frame.IndexBuilder, frame.VectorBuilder, nested);
    }

    
    [CompilationSourceName("mapColKeys")]
    public static Frame<R, a> SelectColumnKeys<C, a, R>(FSharpFunc<C, a> f, Frame<R, C> frame)
    {
      IIndexBuilder indexBuilder = frame.IndexBuilder;
      ReadOnlyCollection<C> keys = frame.ColumnIndex.Keys;
      IEnumerable<M1> m1s = SeqModule.Map<C, a>(f, (IEnumerable<M0>) keys);
      // ISSUE: variable of the null type
      __Null local = null;
      IIndex<a> columnIndex = indexBuilder.Create<a>(m1s, (FSharpOption<bool>) local);
      IVector<IVector> data = frame.VectorBuilder.Build<IVector>(columnIndex.AddressingScheme, VectorConstruction.NewReturn(0), new IVector<IVector>[1]{ frame.Data });
      return new Frame<R, a>(frame.RowIndex, columnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("mapValues")]
    public static Frame<R, C> MapValues<a, b, R, C>(FSharpFunc<a, b> f, Frame<R, C> frame)
    {
      return frame.SelectValues<a, b>(new Func<a, b>(new FrameModule.MapValues8<a, b>(f).Invoke));
    }

    
    public static Frame<R, C> map<R, C, a, b>(FSharpFunc<R, FSharpFunc<C, FSharpFunc<a, b>>> f, Frame<R, C> frame)
    {
      return frame.Select<a, b>(new Func<R, C, a, b>(new FrameModule.map9<R, C, a, b>(f).Invoke));
    }

    
    [CompilationSourceName("reduceValues")]
    public static Series<C, T> ReduceValues<T, R, C>(FSharpFunc<T, FSharpFunc<T, T>> op, Frame<R, C> frame)
    {
      return SeriesModule.Map<C, Series<R, T>, T>((FSharpFunc<C, FSharpFunc<Series<R, T>, T>>) new FrameModule.ReduceValues5<T, R, C>(op), frame.GetColumns<T>());
    }

    
    [CompilationSourceName("maxRowBy")]
    public static FSharpOption<Tuple<R, ObjectSeries<C>>> MaxRowBy<C, R>(C column, Frame<R, C> frame)
    {
      RowSeries<R, C> rows = frame.Rows;
      FSharpFunc<ObjectSeries<C>, double> fsharpFunc1 = (FSharpFunc<ObjectSeries<C>, double>) new FrameModule.MaxRowBy5<C>(column);
      RowSeries<R, C> rowSeries = rows;
      FSharpFunc<ObjectSeries<C>, double> g = fsharpFunc1;
      Series<R, ObjectSeries<C>> series = (Series<R, ObjectSeries<C>>) rowSeries;
      if (series.ValueCount == 0)
        return (FSharpOption<Tuple<R, ObjectSeries<C>>>) null;
      IEnumerable<Tuple<R, ObjectSeries<C>>> observations = SeriesModule.GetObservations<R, ObjectSeries<C>>(series);
      FSharpFunc<Tuple<R, ObjectSeries<C>>, double> fsharpFunc2 = (FSharpFunc<Tuple<R, ObjectSeries<C>>, double>) new FrameModule.MaxRowBy5<R, C>((FSharpFunc<Tuple<R, ObjectSeries<C>>, ObjectSeries<C>>) new FrameModule.MaxRowBy5<R, C>(), g);
      IEnumerable<Tuple<R, ObjectSeries<C>>> tuples = observations;
      if ((object) tuples == null)
        throw new ArgumentNullException("source");
      IEnumerator<Tuple<R, ObjectSeries<C>>> enumerator = tuples.GetEnumerator();
      Tuple<R, ObjectSeries<C>> tuple1;
      try
      {
        if (!enumerator.MoveNext())
          throw new ArgumentException(LanguagePrimitives.ErrorStrings.get_InputSequenceEmptyString(), "source");
        Tuple<R, ObjectSeries<C>> current1 = enumerator.Current;
        double num1 = fsharpFunc2.Invoke(current1);
        Tuple<R, ObjectSeries<C>> tuple2 = current1;
        while (enumerator.MoveNext())
        {
          Tuple<R, ObjectSeries<C>> current2 = enumerator.Current;
          double num2 = fsharpFunc2.Invoke(current2);
          if (num2 > num1)
          {
            num1 = num2;
            tuple2 = current2;
          }
        }
        tuple1 = tuple2;
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
      return FSharpOption<Tuple<R, ObjectSeries<C>>>.Some(tuple1);
    }

    
    [CompilationSourceName("minRowBy")]
    public static FSharpOption<Tuple<R, ObjectSeries<C>>> MinRowBy<C, R>(C column, Frame<R, C> frame)
    {
      RowSeries<R, C> rows = frame.Rows;
      FSharpFunc<ObjectSeries<C>, double> fsharpFunc1 = (FSharpFunc<ObjectSeries<C>, double>) new FrameModule.MinRowBy5<C>(column);
      RowSeries<R, C> rowSeries = rows;
      FSharpFunc<ObjectSeries<C>, double> g = fsharpFunc1;
      Series<R, ObjectSeries<C>> series = (Series<R, ObjectSeries<C>>) rowSeries;
      if (series.ValueCount == 0)
        return (FSharpOption<Tuple<R, ObjectSeries<C>>>) null;
      IEnumerable<Tuple<R, ObjectSeries<C>>> observations = SeriesModule.GetObservations<R, ObjectSeries<C>>(series);
      FSharpFunc<Tuple<R, ObjectSeries<C>>, double> fsharpFunc2 = (FSharpFunc<Tuple<R, ObjectSeries<C>>, double>) new FrameModule.MinRowBy5<R, C>((FSharpFunc<Tuple<R, ObjectSeries<C>>, ObjectSeries<C>>) new FrameModule.MinRowBy5<R, C>(), g);
      IEnumerable<Tuple<R, ObjectSeries<C>>> tuples = observations;
      if ((object) tuples == null)
        throw new ArgumentNullException("source");
      IEnumerator<Tuple<R, ObjectSeries<C>>> enumerator = tuples.GetEnumerator();
      Tuple<R, ObjectSeries<C>> tuple1;
      try
      {
        if (!enumerator.MoveNext())
          throw new ArgumentException(LanguagePrimitives.ErrorStrings.get_InputSequenceEmptyString(), "source");
        Tuple<R, ObjectSeries<C>> current1 = enumerator.Current;
        double num1 = fsharpFunc2.Invoke(current1);
        Tuple<R, ObjectSeries<C>> tuple2 = current1;
        while (enumerator.MoveNext())
        {
          Tuple<R, ObjectSeries<C>> current2 = enumerator.Current;
          double num2 = fsharpFunc2.Invoke(current2);
          if (num2 < num1)
          {
            num1 = num2;
            tuple2 = current2;
          }
        }
        tuple1 = tuple2;
      }
      finally
      {
        (enumerator as IDisposable)?.Dispose();
      }
      return FSharpOption<Tuple<R, ObjectSeries<C>>>.Some(tuple1);
    }

    
    [CompilationSourceName("shift")]
    public static Frame<R, C> Shift<R, C>(int offset, Frame<R, C> frame)
    {
      Tuple<IIndex<R>, VectorConstruction> tuple = frame.RowIndex.Builder.Shift<R>(new Tuple<IIndex<R>, VectorConstruction>(frame.RowIndex, VectorConstruction.NewReturn(0)), offset);
      IIndex<R> rowIndex = tuple.Item1;
      VectorConstruction rowCmd = tuple.Item2;
      IVectorBuilder instance = FVectorBuilderimplementation.VectorBuilder.Instance;
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData72(instance, rowIndex.AddressingScheme, rowCmd));
      return new Frame<R, C>(rowIndex, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("diff")]
    public static Frame<R, C> Diff<R, C>(int offset, Frame<R, C> frame)
    {
      IVectorBuilder instance = FVectorBuilderimplementation.VectorBuilder.Instance;
      Tuple<IIndex<R>, VectorConstruction> tuple = frame.RowIndex.Builder.Shift<R>(new Tuple<IIndex<R>, VectorConstruction>(frame.RowIndex, VectorConstruction.NewReturn(0)), offset);
      VectorConstruction vectorConstruction1 = tuple.Item2;
      IIndex<R> index = tuple.Item1;
      VectorConstruction vectorConstruction2 = frame.RowIndex.Builder.Shift<R>(new Tuple<IIndex<R>, VectorConstruction>(frame.RowIndex, VectorConstruction.NewReturn(0)), -offset).Item2;
      VectorConstruction cmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new FrameModule.cmd0<R>(index)), FSharpList<VectorConstruction>.Cons(vectorConstruction2, FSharpList<VectorConstruction>.Cons(vectorConstruction1, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new FrameModule.cmd01((FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, OptionalValue<double>>>) new FrameModule.cmd00((FSharpFunc<double, FSharpFunc<double, double>>) new FrameModule.cmd0()))));
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData13<R>(index, cmd));
      return new Frame<R, C>(index, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("tryMapRows")]
    public static Series<R, TryValue<V>> TryMapRows<R, C, V>(FSharpFunc<R, FSharpFunc<ObjectSeries<C>, V>> f, Frame<R, C> frame)
    {
      Frame<R, C> frame1 = frame;
      return SeriesModule.Map<R, ObjectSeries<C>, TryValue<V>>((FSharpFunc<R, FSharpFunc<ObjectSeries<C>, TryValue<V>>>) new FrameModule.TryMapRows8<V, R, C>(f), (Series<R, ObjectSeries<C>>) frame1.Rows);
    }

    [CompilationSourceName("tryValues")]
    public static Frame<R, C> TryValues<R, C>(Frame<R, C> frame)
    {
      IVector<TryValue<IVector>> x = FVectorextensionscore.IVector`1Select<IVector, TryValue<IVector>>(frame.Data, (FSharpFunc<IVector, TryValue<IVector>>) new FrameModule.newTryData7());
      FSharpList<Exception> fsharpList = (FSharpList<Exception>) ListModule.OfSeq<Exception>((IEnumerable<M0>) SeqModule.Choose<TryValue<IVector>, Exception>((FSharpFunc<M0, FSharpOption<M1>>) new FrameModule.exceptions8(), (IEnumerable<M0>) SeqModule.Choose<OptionalValue<TryValue<IVector>>, TryValue<IVector>>((FSharpFunc<M0, FSharpOption<M1>>) new FrameModule.exceptions8(), (IEnumerable<M0>) FVectorextensionscore.IVector`1get_DataSequence<TryValue<IVector>>(x))));
      if (!ListModule.IsEmpty<Exception>((FSharpList<M0>) fsharpList))
        throw new AggregateException((IEnumerable<Exception>) ListModule.Collect<Exception, Exception>((FSharpFunc<M0, FSharpList<M1>>) new FrameModule.exceptions6(), (FSharpList<M0>) fsharpList));
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<TryValue<IVector>, IVector>(x, (FSharpFunc<TryValue<IVector>, IVector>) new FrameModule.newData24());
      return new Frame<R, C>(frame.RowIndex, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("fillErrorsWith")]
    public static Frame<R, C> FillErrorsWith<T, R, C>(T value, Frame<R, C> frame)
    {
      return frame.ColumnApply<TryValue<T>>(ConversionKind.Safe, new Func<Series<R, TryValue<T>>, ISeries<R>>(new FrameModule.FillErrorsWith7<T, R>(value).Invoke));
    }

    
    [CompilationSourceName("fillMissingWith")]
    public static Frame<R, C> FillMissingWith<T, R, C>(T value, Frame<R, C> frame)
    {
      return frame.ColumnApply<T>(ConversionKind.Safe, new Func<Series<R, T>, ISeries<R>>(new FrameModule.FillMissingWith7<T, R>(value).Invoke));
    }

    
    [CompilationSourceName("fillMissing")]
    public static Frame<R, C> FillMissing<R, C>(Direction direction, Frame<R, C> frame)
    {
      VectorConstruction rowCmd = VectorConstruction.NewFillMissing(VectorConstruction.NewReturn(0), VectorFillMissing.NewDirection(direction));
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData65(frame.VectorBuilder, frame.RowIndex.AddressingScheme, rowCmd));
      return new Frame<R, C>(frame.RowIndex, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("fillMissingUsing")]
    public static Frame<R, C> FillMissingUsing<R, T, C>(FSharpFunc<Series<R, T>, FSharpFunc<R, T>> f, Frame<R, C> frame)
    {
      return frame.ColumnApply<T>(ConversionKind.Safe, new Func<Series<R, T>, ISeries<R>>(new FrameModule.FillMissingUsing7<R, T>(f).Invoke));
    }

    [CompilationSourceName("dropSparseRows")]
    public static Frame<R, C> DropSparseRows<R, C>(Frame<R, C> frame)
    {
      IVector<IVector> data1 = frame.Data;
      IVector<bool> rowVector = VectorHelperExtensions.createRowVector<bool>(frame.VectorBuilder, frame.RowIndex.AddressingScheme, (Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new FrameModule.hasAllFlagVector1<R, C>(frame)), frame.ColumnIndex.KeyCount, (FSharpFunc<long, long>) new FrameModule.hasAllFlagVector2<C>(frame.ColumnIndex), (FSharpFunc<IVector<object>, bool>) new FrameModule.hasAllFlagVector3(), data1);
      Tuple<IIndex<R>, VectorConstruction> tuple = frame.IndexBuilder.Search<R, bool>(new Tuple<IIndex<R>, VectorConstruction>(frame.RowIndex, VectorConstruction.NewReturn(0)), rowVector, true);
      IIndex<R> rowIndex = tuple.Item1;
      VectorConstruction rowCmd = tuple.Item2;
      IVector<IVector> data2 = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData76(frame.VectorBuilder, rowIndex.AddressingScheme, rowCmd));
      return new Frame<R, C>(rowIndex, frame.ColumnIndex, data2, frame.IndexBuilder, frame.VectorBuilder);
    }

    
    [CompilationSourceName("dropSparseRowsBy")]
    public static Frame<R, C> DropSparseRowsBy<C, R>(C colKey, Frame<R, C> frame)
    {
      IVector<bool> vector = FVectorBuilderimplementation.VectorBuilder.Instance.Create<bool>((bool[]) ArrayModule.OfSeq<bool>((IEnumerable<M0>) SeqModule.Map<OptionalValue<object>, bool>((FSharpFunc<M0, M1>) new FrameModule.hasAllFlagVector1(), (IEnumerable<M0>) FVectorextensionscore.IVector`1get_DataSequence<object>(frame.GetColumn<object>(colKey).Vector))));
      Tuple<IIndex<R>, VectorConstruction> tuple = frame.IndexBuilder.Search<R, bool>(new Tuple<IIndex<R>, VectorConstruction>(frame.RowIndex, VectorConstruction.NewReturn(0)), vector, true);
      IIndex<R> rowIndex = tuple.Item1;
      VectorConstruction rowCmd = tuple.Item2;
      IVector<IVector> data = FVectorextensionscore.IVector`1Select<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new FrameModule.newData67(frame.VectorBuilder, rowIndex.AddressingScheme, rowCmd));
      return new Frame<R, C>(rowIndex, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    [CompilationSourceName("dropSparseCols")]
    public static Frame<R, C> DropSparseColumns<R, C>(Frame<R, C> frame)
    {
      Tuple<C[], IVector[]> tuple = (Tuple<C[], IVector[]>) ArrayModule.Unzip<C, IVector>((Tuple<M0, M1>[]) SeqModule.ToArray<Tuple<C, IVector>>((IEnumerable<M0>) new FrameModule.DropSparseColumns8<C, R>(frame, default (C), new long(), new OptionalValue<IVector>(), new KeyValuePair<C, long>(), (Tuple<C, long>) null, (IEnumerator<KeyValuePair<C, long>>) null, (Tuple<C, IVector>) null, (IEnumerator<Tuple<C, IVector>>) null, 0, (Tuple<C, IVector>) null)));
      IVector[] vectorArray = tuple.Item2;
      C[] array = tuple.Item1;
      IIndex<C> columnIndex = frame.IndexBuilder.Create<C>(Array.AsReadOnly<C>(array), (FSharpOption<bool>) null);
      return new Frame<R, C>(frame.RowIndex, columnIndex, frame.VectorBuilder.Create<IVector>(vectorArray), frame.IndexBuilder, frame.VectorBuilder);
    }

    [CompilationSourceName("denseCols")]
    public static ColumnSeries<R, C> DenseColumns<R, C>(Frame<R, C> frame)
    {
      return frame.ColumnsDense;
    }

    [CompilationSourceName("denseRows")]
    public static RowSeries<R, C> DenseRows<R, C>(Frame<R, C> frame)
    {
      return frame.RowsDense;
    }

    
    [CompilationSourceName("join")]
    public static Frame<R, C> Join<R, C>(JoinKind kind, Frame<R, C> frame1, Frame<R, C> frame2)
    {
      return frame1.Join(frame2, kind);
    }

    
    [CompilationSourceName("joinAlign")]
    public static Frame<R, C> JoinAlign<R, C>(JoinKind kind, Lookup lookup, Frame<R, C> frame1, Frame<R, C> frame2)
    {
      return frame1.Join(frame2, kind, lookup);
    }

    [CompilationSourceName("mergeAll")]
    public static Frame<R, C> MergeAll<R, C>(IEnumerable<Frame<R, C>> frames)
    {
      if (SeqModule.IsEmpty<Frame<R, C>>((IEnumerable<M0>) frames))
        return new Frame<R, C>((IEnumerable<C>) FSharpList<C>.get_Empty(), (IEnumerable<ISeries<R>>) FSharpList<ISeries<R>>.get_Empty());
      return ((Frame<R, C>) SeqModule.Head<Frame<R, C>>((IEnumerable<M0>) frames)).Merge((IEnumerable<Frame<R, C>>) SeqModule.Skip<Frame<R, C>>(1, (IEnumerable<M0>) frames));
    }

    
    [CompilationSourceName("merge")]
    public static Frame<R, C> Merge<R, C>(Frame<R, C> frame1, Frame<R, C> frame2)
    {
      return FrameModule.MergeAll<R, C>((IEnumerable<Frame<R, C>>) FSharpList<Frame<R, C>>.Cons(frame1, FSharpList<Frame<R, C>>.Cons(frame2, FSharpList<Frame<R, C>>.get_Empty())));
    }

    
    [CompilationSourceName("zipAlign")]
    public static Frame<R, C> ZipAlignInto<V1, V2, V, R, C>(JoinKind columnKind, JoinKind rowKind, Lookup lookup, FSharpFunc<V1, FSharpFunc<V2, V>> op, Frame<R, C> frame1, Frame<R, C> frame2)
    {
      return frame1.Zip<V1, V2, V>(frame2, columnKind, rowKind, lookup, false, new Func<V1, V2, V>(new FrameModule.ZipAlignInto3<V1, V2, V>(op).Invoke));
    }

    
    [CompilationSourceName("zip")]
    public static Frame<R, C> ZipInto<V1, V2, V, R, C>(FSharpFunc<V1, FSharpFunc<V2, V>> op, Frame<R, C> frame1, Frame<R, C> frame2)
    {
      return FrameModule.ZipAlignInto<V1, V2, V, R, C>(JoinKind.Inner, JoinKind.Inner, Lookup.Exact, op, frame1, frame2);
    }

    
    [CompilationSourceName("reduceLevel")]
    public static Frame<K, C> ReduceLevel<R, K, T, C>(FSharpFunc<R, K> levelSel, FSharpFunc<T, FSharpFunc<T, T>> op, Frame<R, C> frame)
    {
      Series<C, Series<K, T>> nested = SeriesModule.Map<C, Series<R, T>, Series<K, T>>((FSharpFunc<C, FSharpFunc<Series<R, T>, Series<K, T>>>) new FrameModule.ReduceLevel1<K, T, R, C>(levelSel, op), frame.GetColumns<T>());
      return FrameUtils.fromColumns<K, C, Series<K, T>>(frame.IndexBuilder, frame.VectorBuilder, nested);
    }

    
    [CompilationSourceName("applyLevel")]
    public static Frame<K, C> ApplyLevel<R, K, T, C>(FSharpFunc<R, K> levelSel, FSharpFunc<Series<R, T>, T> op, Frame<R, C> frame)
    {
      Series<C, Series<K, T>> nested = SeriesModule.Map<C, Series<R, T>, Series<K, T>>((FSharpFunc<C, FSharpFunc<Series<R, T>, Series<K, T>>>) new FrameModule.ApplyLevel4<K, T, R, C>(levelSel, op), frame.GetColumns<T>());
      return FrameUtils.fromColumns<K, C, Series<K, T>>(frame.IndexBuilder, frame.VectorBuilder, nested);
    }

    [CompilationSourceName("nest")]
    public static Series<R1, Frame<R2, C>> Nest<R1, R2, C>(Frame<Tuple<R1, R2>, C> frame)
    {
      IEnumerable<R1> labels = (IEnumerable<R1>) SeqModule.Map<Tuple<R1, R2>, R1>((FSharpFunc<M0, M1>) new FrameModule.labels5<R1, R2>(), (IEnumerable<M0>) frame.RowKeys);
      return SeriesModule.Map<R1, Frame<Tuple<R1, R2>, C>, Frame<R2, C>>((FSharpFunc<R1, FSharpFunc<Frame<Tuple<R1, R2>, C>, Frame<R2, C>>>) new FrameModule.Nest7<R1, R2, C>(), frame.NestRowsBy<R1>(labels));
    }

    
    [CompilationSourceName("nestBy")]
    public static Series<K1, Frame<K2, C>> NestBy<K2, K1, C>(FSharpFunc<K2, K1> keySelector, Frame<K2, C> frame)
    {
      IEnumerable<K1> labels = SeqModule.Map<K2, K1>(keySelector, frame.RowKeys);
      if (frame.RowCount != SeqModule.Length<K1>((IEnumerable<M0>) labels))
        throw Operators.Failure("nestBy: Generated labels contain missing values and cannot be used for grouping. Make sure the keySelector function does not return null.");
      return FrameModule.Nest<K1, K2, C>(frame.GroupByLabels<K1>(labels, frame.RowCount));
    }

    [CompilationSourceName("unnest")]
    public static Frame<Tuple<R1, R2>, C> Unnest<R1, R2, C>(Series<R1, Frame<R2, C>> series)
    {
      return FrameModule.MergeAll<Tuple<R1, R2>, C>(SeriesModule.GetValues<R1, Frame<Tuple<R1, R2>, C>>(SeriesModule.Map<R1, Frame<R2, C>, Frame<Tuple<R1, R2>, C>>((FSharpFunc<R1, FSharpFunc<Frame<R2, C>, Frame<Tuple<R1, R2>, C>>>) new FrameModule.Unnest2<R1, R2, C>(), series)));
    }

    [Serializable]
    internal sealed class CountValues<R> : FSharpFunc<ObjectSeries<R>, int>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal CountValues()
      {
        base.ctor();
      }

      public virtual int Invoke(ObjectSeries<R> series)
      {
        return SeriesModule.CountValues<R, object>((Series<R, object>) series);
      }
    }

    [Serializable]
    internal sealed class CountValues<R, C> : FSharpFunc<C, FSharpFunc<ObjectSeries<R>, int>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal CountValues()
      {
        base.ctor();
      }

      public virtual FSharpFunc<ObjectSeries<R>, int> Invoke(C _arg1)
      {
        return (FSharpFunc<ObjectSeries<R>, int>) new FrameModule.CountValues<R>();
      }
    }

    [Serializable]
    internal sealed class GetColumns<R, C, T> : OptimizedClosures.FSharpFunc<C, ObjectSeries<R>, FSharpOption<Series<R, T>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal GetColumns()
      {
        base.ctor();
      }

      public virtual FSharpOption<Series<R, T>> Invoke(C _arg1, ObjectSeries<R> v)
      {
        OptionalValue<Series<R, T>> optionalValue = v.TryAs<T>();
        if (optionalValue.HasValue)
          return FSharpOption<Series<R, T>>.Some(optionalValue.Value);
        return (FSharpOption<Series<R, T>>) null;
      }
    }

    [Serializable]
    internal sealed class GetRows<R, C, T> : OptimizedClosures.FSharpFunc<R, ObjectSeries<C>, FSharpOption<Series<C, T>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal GetRows()
      {
        base.ctor();
      }

      public virtual FSharpOption<Series<C, T>> Invoke(R _arg1, ObjectSeries<C> v)
      {
        OptionalValue<Series<C, T>> optionalValue = v.TryAs<T>();
        if (optionalValue.HasValue)
          return FSharpOption<Series<C, T>>.Some(optionalValue.Value);
        return (FSharpOption<Series<C, T>>) null;
      }
    }

    [Serializable]
    internal sealed class TryLookupColObservation<R, C, a> : FSharpFunc<KeyValuePair<C, Series<R, a>>, Tuple<C, Series<R, a>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal TryLookupColObservation()
      {
        base.ctor();
      }

      public virtual Tuple<C, Series<R, a>> Invoke(KeyValuePair<C, Series<R, a>> kvp)
      {
        return new Tuple<C, Series<R, a>>(kvp.Key, kvp.Value);
      }
    }

    [Serializable]
    internal sealed class TryLookupRowObservation<R, C, a> : FSharpFunc<KeyValuePair<R, Series<C, a>>, Tuple<R, Series<C, a>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal TryLookupRowObservation()
      {
        base.ctor();
      }

      public virtual Tuple<R, Series<C, a>> Invoke(KeyValuePair<R, Series<C, a>> kvp)
      {
        return new Tuple<R, Series<C, a>>(kvp.Key, kvp.Value);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class GroupRowsUsing<K, R, C>
    {
      public FSharpFunc<R, FSharpFunc<ObjectSeries<C>, K>> selector;

      public GroupRowsUsing(FSharpFunc<R, FSharpFunc<ObjectSeries<C>, K>> selector)
      {
        this.selector = selector;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal K Invoke(R delegateArg0, ObjectSeries<C> delegateArg1)
      {
        return (K) FSharpFunc<R, ObjectSeries<C>>.InvokeFast<K>((FSharpFunc<R, FSharpFunc<ObjectSeries<C>, M0>>) this.selector, delegateArg0, delegateArg1);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class GroupRowsByIndex<K, R>
    {
      public FSharpFunc<R, K> keySelector;

      public GroupRowsByIndex(FSharpFunc<R, K> keySelector)
      {
        this.keySelector = keySelector;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal K Invoke(R delegateArg0)
      {
        return this.keySelector.Invoke(delegateArg0);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class AggregateRowsBy<R, V1, V2>
    {
      public FSharpFunc<Series<R, V1>, V2> aggFunc;

      public AggregateRowsBy(FSharpFunc<Series<R, V1>, V2> aggFunc)
      {
        this.aggFunc = aggFunc;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal V2 Invoke(Series<R, V1> delegateArg0)
      {
        return this.aggFunc.Invoke(delegateArg0);
      }
    }

    [Serializable]
    internal sealed class fromRows<R, C> : FSharpFunc<Series<R, ObjectSeries<C>>, Frame<R, C>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IIndexBuilder arg00;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder arg10;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal fromRows(IIndexBuilder arg00, IVectorBuilder arg10)
      {
        this.ctor();
        this.arg00 = arg00;
        this.arg10 = arg10;
      }

      public virtual Frame<R, C> Invoke(Series<R, ObjectSeries<C>> arg20)
      {
        return FrameUtils.fromRows<R, C, ObjectSeries<C>>(this.arg00, this.arg10, arg20);
      }
    }

    [Serializable]
    internal sealed class PivotTable<R, C, CNew> : OptimizedClosures.FSharpFunc<R, ObjectSeries<C>, CNew>
    {
      public FSharpFunc<R, FSharpFunc<ObjectSeries<C>, CNew>> colGrp;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal PivotTable(FSharpFunc<R, FSharpFunc<ObjectSeries<C>, CNew>> colGrp)
      {
        this.ctor();
        this.colGrp = colGrp;
      }

      public virtual CNew Invoke(R r, ObjectSeries<C> g)
      {
        return (CNew) FSharpFunc<R, ObjectSeries<C>>.InvokeFast<CNew>((FSharpFunc<R, FSharpFunc<ObjectSeries<C>, M0>>) this.colGrp, r, g);
      }
    }

    [Serializable]
    internal sealed class PivotTable<R, C, CNew> : OptimizedClosures.FSharpFunc<CNew, Series<R, ObjectSeries<C>>, Series<R, ObjectSeries<C>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal PivotTable()
      {
        base.ctor();
      }

      public virtual Series<R, ObjectSeries<C>> Invoke(CNew _arg1, Series<R, ObjectSeries<C>> g)
      {
        return g;
      }
    }

    [Serializable]
    internal sealed class PivotTable<R, C, RNew> : OptimizedClosures.FSharpFunc<R, ObjectSeries<C>, RNew>
    {
      public FSharpFunc<R, FSharpFunc<ObjectSeries<C>, RNew>> rowGrp;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal PivotTable(FSharpFunc<R, FSharpFunc<ObjectSeries<C>, RNew>> rowGrp)
      {
        this.ctor();
        this.rowGrp = rowGrp;
      }

      public virtual RNew Invoke(R c, ObjectSeries<C> g)
      {
        return (RNew) FSharpFunc<R, ObjectSeries<C>>.InvokeFast<RNew>((FSharpFunc<R, FSharpFunc<ObjectSeries<C>, M0>>) this.rowGrp, c, g);
      }
    }

    [Serializable]
    internal sealed class PivotTable<R, C, RNew> : OptimizedClosures.FSharpFunc<RNew, Series<R, ObjectSeries<C>>, Series<R, ObjectSeries<C>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal PivotTable()
      {
        base.ctor();
      }

      public virtual Series<R, ObjectSeries<C>> Invoke(RNew _arg2, Series<R, ObjectSeries<C>> g)
      {
        return g;
      }
    }

    [Serializable]
    internal sealed class PivotTable<R, C, RNew> : FSharpFunc<Series<R, ObjectSeries<C>>, Series<RNew, Series<R, ObjectSeries<C>>>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<R, FSharpFunc<ObjectSeries<C>, RNew>> keySelector;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<RNew, FSharpFunc<Series<R, ObjectSeries<C>>, Series<R, ObjectSeries<C>>>> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal PivotTable(FSharpFunc<R, FSharpFunc<ObjectSeries<C>, RNew>> keySelector, FSharpFunc<RNew, FSharpFunc<Series<R, ObjectSeries<C>>, Series<R, ObjectSeries<C>>>> f)
      {
        this.ctor();
        this.keySelector = keySelector;
        this.f = f;
      }

      public virtual Series<RNew, Series<R, ObjectSeries<C>>> Invoke(Series<R, ObjectSeries<C>> series)
      {
        return SeriesModule.groupInto<R, ObjectSeries<C>, RNew, Series<R, ObjectSeries<C>>>(this.keySelector, this.f, series);
      }
    }

    [Serializable]
    internal sealed class PivotTable<R, C, T> : FSharpFunc<Series<R, ObjectSeries<C>>, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<R, ObjectSeries<C>>, Frame<R, C>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Frame<R, C>, T> g;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal PivotTable(FSharpFunc<Series<R, ObjectSeries<C>>, Frame<R, C>> f, FSharpFunc<Frame<R, C>, T> g)
      {
        this.ctor();
        this.f = f;
        this.g = g;
      }

      public virtual T Invoke(Series<R, ObjectSeries<C>> x)
      {
        return this.g.Invoke(this.f.Invoke(x));
      }
    }

    [Serializable]
    internal sealed class PivotTable<R, C, RNew, T> : FSharpFunc<Series<RNew, Series<R, ObjectSeries<C>>>, Series<RNew, T>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<R, ObjectSeries<C>>, T> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal PivotTable(FSharpFunc<Series<R, ObjectSeries<C>>, T> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual Series<RNew, T> Invoke(Series<RNew, Series<R, ObjectSeries<C>>> series)
      {
        return SeriesModule.MapValues<Series<R, ObjectSeries<C>>, T, RNew>(this.f, series);
      }
    }

    [Serializable]
    internal sealed class fromRows<C, a, b, R> : FSharpFunc<Series<a, b>, Frame<a, C>> where b : ISeries<C>
    {
      public Frame<R, C> frame;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal fromRows(Frame<R, C> frame)
      {
        this.ctor();
        this.frame = frame;
      }

      public virtual Frame<a, C> Invoke(Series<a, b> rs)
      {
        return FrameUtils.fromRowsAndColumnKeys<a, C, b>(this.frame.IndexBuilder, this.frame.VectorBuilder, this.frame.ColumnIndex.Keys, rs);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal abstract class fromRowscontract<C>
    {
      internal abstract FSharpFunc<Series<a, b>, Frame<a, C>> DirectInvoke<a, b>() where b : ISeries<C>;
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class fromRows<C, R> : FrameModule.fromRowscontract<C>
    {
      public Frame<R, C> frame;

      public fromRows(Frame<R, C> frame)
      {
        this.frame = frame;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal override FSharpFunc<Series<a, b>, Frame<a, C>> DirectInvoke<a, b>()
      {
        return (FSharpFunc<Series<a, b>, Frame<a, C>>) new FrameModule.fromRows<C, a, b, R>(this.frame);
      }
    }

    [Serializable]
    internal sealed class Window<R, C> : FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, Series<R, ObjectSeries<C>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Window()
      {
        base.ctor();
      }

      public virtual Series<R, ObjectSeries<C>> Invoke(DataSegment<Series<R, ObjectSeries<C>>> ds)
      {
        return DataSegment.GetData<Series<R, ObjectSeries<C>>>(ds);
      }
    }

    [Serializable]
    internal sealed class Window<R, C> : FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, Frame<R, C>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, Series<R, ObjectSeries<C>>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<R, ObjectSeries<C>>, Frame<R, C>> g;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Window(FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, Series<R, ObjectSeries<C>>> f, FSharpFunc<Series<R, ObjectSeries<C>>, Frame<R, C>> g)
      {
        this.ctor();
        this.f = f;
        this.g = g;
      }

      public virtual Frame<R, C> Invoke(DataSegment<Series<R, ObjectSeries<C>>> x)
      {
        return this.g.Invoke(this.f.Invoke(x));
      }
    }

    [Serializable]
    internal sealed class fromRows<C, b, c, R> : FSharpFunc<Series<b, c>, Frame<b, C>> where c : ISeries<C>
    {
      public Frame<R, C> frame;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal fromRows(Frame<R, C> frame)
      {
        this.ctor();
        this.frame = frame;
      }

      public virtual Frame<b, C> Invoke(Series<b, c> rs)
      {
        return FrameUtils.fromRowsAndColumnKeys<b, C, c>(this.frame.IndexBuilder, this.frame.VectorBuilder, this.frame.ColumnIndex.Keys, rs);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal abstract class fromRowscontract<C>
    {
      internal abstract FSharpFunc<Series<b, c>, Frame<b, C>> DirectInvoke<b, c>() where c : ISeries<C>;
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class fromRows<C, R> : FrameModule.fromRowscontract<C>
    {
      public Frame<R, C> frame;

      public fromRows(Frame<R, C> frame)
      {
        this.frame = frame;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal override FSharpFunc<Series<b, c>, Frame<b, C>> DirectInvoke<b, c>()
      {
        return (FSharpFunc<Series<b, c>, Frame<b, C>>) new FrameModule.fromRows<C, b, c, R>(this.frame);
      }
    }

    [Serializable]
    internal sealed class WindowInto<R, C, a> : FSharpFunc<Series<R, ObjectSeries<C>>, a>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<R, ObjectSeries<C>>, Frame<R, C>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Frame<R, C>, a> g;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowInto(FSharpFunc<Series<R, ObjectSeries<C>>, Frame<R, C>> f, FSharpFunc<Frame<R, C>, a> g)
      {
        this.ctor();
        this.f = f;
        this.g = g;
      }

      public virtual a Invoke(Series<R, ObjectSeries<C>> x)
      {
        return this.g.Invoke(this.f.Invoke(x));
      }
    }

    [Serializable]
    internal sealed class WindowInto<R, C> : FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, Series<R, ObjectSeries<C>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowInto()
      {
        base.ctor();
      }

      public virtual Series<R, ObjectSeries<C>> Invoke(DataSegment<Series<R, ObjectSeries<C>>> ds)
      {
        return DataSegment.GetData<Series<R, ObjectSeries<C>>>(ds);
      }
    }

    [Serializable]
    internal sealed class WindowInto<R, C, a> : FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, a>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, Series<R, ObjectSeries<C>>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Series<R, ObjectSeries<C>>, a> g;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowInto(FSharpFunc<DataSegment<Series<R, ObjectSeries<C>>>, Series<R, ObjectSeries<C>>> f, FSharpFunc<Series<R, ObjectSeries<C>>, a> g)
      {
        this.ctor();
        this.f = f;
        this.g = g;
      }

      public virtual a Invoke(DataSegment<Series<R, ObjectSeries<C>>> x)
      {
        return this.g.Invoke(this.f.Invoke(x));
      }
    }

    [Serializable]
    internal sealed class valTyp : FSharpFunc<OptionalValue<IVector>, FSharpOption<Type>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal valTyp()
      {
        base.ctor();
      }

      public virtual FSharpOption<Type> Invoke(OptionalValue<IVector> dt)
      {
        if (dt.HasValue)
          return FSharpOption<Type>.Some(dt.Value.ElementType);
        return (FSharpOption<Type>) null;
      }
    }

    [Serializable]
    internal sealed class rowIndex : FSharpFunc<int, int>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal rowIndex()
      {
        base.ctor();
      }

      public virtual int Invoke(int x)
      {
        return (int) Operators.Identity<int>((M0) x);
      }
    }

    [Serializable]
    internal sealed class Unmelt<C> : FSharpFunc<ObjectSeries<string>, C>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Unmelt()
      {
        base.ctor();
      }

      public virtual C Invoke(ObjectSeries<string> row)
      {
        return row.GetAs<C>("Column");
      }
    }

    [Serializable]
    internal sealed class Unmelt<R> : FSharpFunc<ObjectSeries<string>, R>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Unmelt()
      {
        base.ctor();
      }

      public virtual R Invoke(ObjectSeries<string> row)
      {
        return row.GetAs<R>("Row");
      }
    }

    [Serializable]
    internal sealed class Unmelt : FSharpFunc<ObjectSeries<string>, object>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Unmelt()
      {
        base.ctor();
      }

      public virtual object Invoke(ObjectSeries<string> row)
      {
        return row.GetAs<object>("Value");
      }
    }

    [Serializable]
    internal sealed class relocs6 : FSharpFunc<long, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal relocs6()
      {
        base.ctor();
      }

      public virtual bool Invoke(long _arg1)
      {
        return true;
      }
    }

    [Serializable]
    internal sealed class cmd<R, C> : FSharpFunc<IVector, IVector>
    {
      public Frame<R, C> frame;
      public IIndex<R> newIdx;
      public VectorConstruction relocs;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal cmd(Frame<R, C> frame, IIndex<R> newIdx, VectorConstruction relocs)
      {
        this.ctor();
        this.frame = frame;
        this.newIdx = newIdx;
        this.relocs = relocs;
      }

      public virtual IVector Invoke(IVector v)
      {
        return VectorHelpers.transformColumn(this.frame.VectorBuilder, this.newIdx.AddressingScheme, this.relocs, v);
      }
    }

    [Serializable]
    internal sealed class newData2 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData2(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    [Serializable]
    internal sealed class IndexRowsUsing<C, R2, R1> : OptimizedClosures.FSharpFunc<R1, ObjectSeries<C>, R2>
    {
      public FSharpFunc<ObjectSeries<C>, R2> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal IndexRowsUsing(FSharpFunc<ObjectSeries<C>, R2> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual R2 Invoke(R1 k, ObjectSeries<C> v)
      {
        return this.f.Invoke(v);
      }
    }

    [Serializable]
    internal sealed class newData3 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData3(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    [Serializable]
    internal sealed class SortRows : OptimizedClosures.FSharpFunc<IComparable, IComparable, int>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal SortRows()
      {
        base.ctor();
      }

      public virtual int Invoke(IComparable x, IComparable y)
      {
        return LanguagePrimitives.HashCompare.GenericComparisonIntrinsic<IComparable>((M0) x, (M0) y);
      }
    }

    [Serializable]
    internal sealed class newData4 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData4(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    [Serializable]
    internal sealed class newData5 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData5(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    [Serializable]
    internal sealed class newData6 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData6(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class newData8 : VectorCallSite<IVector>
    {
      public newData8()
      {
        FrameModule.newData8 newData90318 = this;
      }

      IVector VectorCallSite<IVector>.Invoke<T>(IVector<T> v)
      {
        return (IVector) FVectorBuilderimplementation.VectorBuilder.Instance.Create<T>(ArrayModule.OfSeq<T>((IEnumerable<T>) FSharpList<T>.get_Empty()));
      }
    }

    [Serializable]
    internal sealed class newData7 : FSharpFunc<IVector, IVector>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData7()
      {
        base.ctor();
      }

      public virtual IVector Invoke(IVector v)
      {
        VectorCallSite<IVector> vectorCallSite = (VectorCallSite<IVector>) new FrameModule.newData8();
        return v.Invoke<IVector>(vectorCallSite);
      }
    }

    [Serializable]
    internal sealed class newData9 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData9(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    [Serializable]
    internal sealed class newData40 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData40(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    [Serializable]
    internal sealed class newData61 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData61(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class MapValues8<a, b>
    {
      public FSharpFunc<a, b> f;

      public MapValues8(FSharpFunc<a, b> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal b Invoke(a delegateArg0)
      {
        return this.f.Invoke(delegateArg0);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class map9<R, C, a, b>
    {
      public FSharpFunc<R, FSharpFunc<C, FSharpFunc<a, b>>> f;

      public map9(FSharpFunc<R, FSharpFunc<C, FSharpFunc<a, b>>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal b Invoke(R delegateArg0, C delegateArg1, a delegateArg2)
      {
        return (b) FSharpFunc<R, C>.InvokeFast<a, b>((FSharpFunc<R, FSharpFunc<C, FSharpFunc<M0, M1>>>) this.f, delegateArg0, delegateArg1, (M0) delegateArg2);
      }
    }

    [Serializable]
    internal sealed class ReduceValues5<T, R> : FSharpFunc<Series<R, T>, T>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, T>> op;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ReduceValues5(FSharpFunc<T, FSharpFunc<T, T>> op)
      {
        this.ctor();
        this.op = op;
      }

      public virtual T Invoke(Series<R, T> series)
      {
        return SeriesModule.ReduceValues<T, R>(this.op, series);
      }
    }

    [Serializable]
    internal sealed class ReduceValues5<T, R, C> : FSharpFunc<C, FSharpFunc<Series<R, T>, T>>
    {
      public FSharpFunc<T, FSharpFunc<T, T>> op;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ReduceValues5(FSharpFunc<T, FSharpFunc<T, T>> op)
      {
        this.ctor();
        this.op = op;
      }

      public virtual FSharpFunc<Series<R, T>, T> Invoke(C _arg1)
      {
        return (FSharpFunc<Series<R, T>, T>) new FrameModule.ReduceValues5<T, R>(this.op);
      }
    }

    [Serializable]
    internal sealed class MaxRowBy5<C> : FSharpFunc<ObjectSeries<C>, double>
    {
      public C column;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal MaxRowBy5(C column)
      {
        this.ctor();
        this.column = column;
      }

      public virtual double Invoke(ObjectSeries<C> row)
      {
        return row.GetAs<double>(this.column);
      }
    }

    [Serializable]
    internal sealed class MaxRowBy5<R, C> : FSharpFunc<Tuple<R, ObjectSeries<C>>, ObjectSeries<C>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal MaxRowBy5()
      {
        base.ctor();
      }

      public virtual ObjectSeries<C> Invoke(Tuple<R, ObjectSeries<C>> tupledArg)
      {
        return tupledArg.Item2;
      }
    }

    [Serializable]
    internal sealed class MaxRowBy5<R, C> : FSharpFunc<Tuple<R, ObjectSeries<C>>, double>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Tuple<R, ObjectSeries<C>>, ObjectSeries<C>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<ObjectSeries<C>, double> g;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal MaxRowBy5(FSharpFunc<Tuple<R, ObjectSeries<C>>, ObjectSeries<C>> f, FSharpFunc<ObjectSeries<C>, double> g)
      {
        this.ctor();
        this.f = f;
        this.g = g;
      }

      public virtual double Invoke(Tuple<R, ObjectSeries<C>> x)
      {
        return this.g.Invoke(this.f.Invoke(x));
      }
    }

    [Serializable]
    internal sealed class MinRowBy5<C> : FSharpFunc<ObjectSeries<C>, double>
    {
      public C column;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal MinRowBy5(C column)
      {
        this.ctor();
        this.column = column;
      }

      public virtual double Invoke(ObjectSeries<C> row)
      {
        return row.GetAs<double>(this.column);
      }
    }

    [Serializable]
    internal sealed class MinRowBy5<R, C> : FSharpFunc<Tuple<R, ObjectSeries<C>>, ObjectSeries<C>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal MinRowBy5()
      {
        base.ctor();
      }

      public virtual ObjectSeries<C> Invoke(Tuple<R, ObjectSeries<C>> tupledArg)
      {
        return tupledArg.Item2;
      }
    }

    [Serializable]
    internal sealed class MinRowBy5<R, C> : FSharpFunc<Tuple<R, ObjectSeries<C>>, double>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<Tuple<R, ObjectSeries<C>>, ObjectSeries<C>> f;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<ObjectSeries<C>, double> g;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal MinRowBy5(FSharpFunc<Tuple<R, ObjectSeries<C>>, ObjectSeries<C>> f, FSharpFunc<ObjectSeries<C>, double> g)
      {
        this.ctor();
        this.f = f;
        this.g = g;
      }

      public virtual double Invoke(Tuple<R, ObjectSeries<C>> x)
      {
        return this.g.Invoke(this.f.Invoke(x));
      }
    }

    [Serializable]
    internal sealed class newData72 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData72(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    [Serializable]
    internal sealed class cmd0<R> : FSharpFunc<Unit, long>
    {
      public IIndex<R> newRowIndex;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal cmd0(IIndex<R> newRowIndex)
      {
        this.ctor();
        this.newRowIndex = newRowIndex;
      }

      public virtual long Invoke(Unit unitVar)
      {
        return this.newRowIndex.KeyCount;
      }
    }

    [Serializable]
    internal sealed class cmd0 : OptimizedClosures.FSharpFunc<double, double, double>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal cmd0()
      {
        base.ctor();
      }

      public virtual double Invoke(double x, double y)
      {
        return x - y;
      }
    }

    [Serializable]
    internal sealed class cmd00 : OptimizedClosures.FSharpFunc<OptionalValue<double>, OptionalValue<double>, OptionalValue<double>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<double, FSharpFunc<double, double>> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal cmd00(FSharpFunc<double, FSharpFunc<double, double>> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual OptionalValue<double> Invoke(OptionalValue<double> input1, OptionalValue<double> input2)
      {
        if ((!input1.HasValue ? 0 : (input2.HasValue ? 1 : 0)) != 0)
          return new OptionalValue<double>((double) FSharpFunc<double, double>.InvokeFast<double>((FSharpFunc<double, FSharpFunc<double, M0>>) this.f, input1.Value, input2.Value));
        return OptionalValue<double>.Missing;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class cmd01 : IBinaryTransform
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, OptionalValue<double>>> operation;

      public cmd01(FSharpFunc<OptionalValue<double>, FSharpFunc<OptionalValue<double>, OptionalValue<double>>> operation)
      {
        this.operation = operation;
        // ISSUE: explicit constructor call
        base.ctor();
        FrameModule.cmd01 cmd122011 = this;
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
    internal sealed class newData13<R> : FSharpFunc<IVector, IVector>
    {
      public IIndex<R> newRowIndex;
      public VectorConstruction cmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData13(IIndex<R> newRowIndex, VectorConstruction cmd)
      {
        this.ctor();
        this.newRowIndex = newRowIndex;
        this.cmd = cmd;
      }

      public virtual IVector Invoke(IVector _arg1)
      {
        IVector v = _arg1;
        FSharpOption<IVector<double>> fsharpOption = VectorHelpers.AsFloatVector_(v);
        if (fsharpOption == null)
          return v;
        return (IVector) FVectorBuilderimplementation.VectorBuilder.Instance.Build<double>(this.newRowIndex.AddressingScheme, this.cmd, new IVector<double>[1]{ fsharpOption.get_Value() });
      }
    }

    [Serializable]
    internal sealed class TryMapRows8<V, R, C> : OptimizedClosures.FSharpFunc<R, ObjectSeries<C>, TryValue<V>>
    {
      public FSharpFunc<R, FSharpFunc<ObjectSeries<C>, V>> f;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal TryMapRows8(FSharpFunc<R, FSharpFunc<ObjectSeries<C>, V>> f)
      {
        this.ctor();
        this.f = f;
      }

      public virtual TryValue<V> Invoke(R k, ObjectSeries<C> row)
      {
        try
        {
          return TryValue<V>.NewSuccess((V) FSharpFunc<R, ObjectSeries<C>>.InvokeFast<V>((FSharpFunc<R, FSharpFunc<ObjectSeries<C>, M0>>) this.f, k, row));
        }
        catch (object ex)
        {
          return TryValue<V>.NewError((Exception) ex);
        }
      }
    }

    [Serializable]
    internal sealed class newTryData7 : FSharpFunc<IVector, TryValue<IVector>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newTryData7()
      {
        base.ctor();
      }

      public virtual TryValue<IVector> Invoke(IVector vect)
      {
        return VectorHelpers.tryValues(vect);
      }
    }

    [Serializable]
    internal sealed class exceptions8 : FSharpFunc<TryValue<IVector>, FSharpOption<Exception>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal exceptions8()
      {
        base.ctor();
      }

      public virtual FSharpOption<Exception> Invoke(TryValue<IVector> v)
      {
        if (v.HasValue)
          return (FSharpOption<Exception>) null;
        return FSharpOption<Exception>.Some(v.Exception);
      }
    }

    [Serializable]
    internal sealed class exceptions8 : FSharpFunc<OptionalValue<TryValue<IVector>>, FSharpOption<TryValue<IVector>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal exceptions8()
      {
        base.ctor();
      }

      public virtual FSharpOption<TryValue<IVector>> Invoke(OptionalValue<TryValue<IVector>> value)
      {
        if (value.HasValue)
          return FSharpOption<TryValue<IVector>>.Some(value.Value);
        return (FSharpOption<TryValue<IVector>>) null;
      }
    }

    [Serializable]
    internal sealed class newData24 : FSharpFunc<TryValue<IVector>, IVector>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData24()
      {
        base.ctor();
      }

      public virtual IVector Invoke(TryValue<IVector> v)
      {
        return v.Value;
      }
    }

    [Serializable]
    internal sealed class exceptions6 : FSharpFunc<Exception, FSharpList<Exception>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal exceptions6()
      {
        base.ctor();
      }

      public virtual FSharpList<Exception> Invoke(Exception _arg1)
      {
        Exception exception = _arg1;
        AggregateException aggregateException = exception as AggregateException;
        if (aggregateException != null)
          return (FSharpList<Exception>) ListModule.OfSeq<Exception>((IEnumerable<M0>) aggregateException.InnerExceptions);
        return FSharpList<Exception>.Cons(exception, FSharpList<Exception>.get_Empty());
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class FillErrorsWith7<T, R>
    {
      public T value;

      public FillErrorsWith7(T value)
      {
        this.value = value;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal ISeries<R> Invoke(Series<R, TryValue<T>> s)
      {
        return (ISeries<R>) SeriesModule.fillErrorsWith<T, R>(this.value, s);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class FillMissingWith7<T, R>
    {
      public T value;

      public FillMissingWith7(T value)
      {
        this.value = value;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal ISeries<R> Invoke(Series<R, T> s)
      {
        return (ISeries<R>) SeriesModule.FillMissingWith<T, R, T>(this.value, s);
      }
    }

    [Serializable]
    internal sealed class newData65 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData65(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class FillMissingUsing7<R, T>
    {
      public FSharpFunc<Series<R, T>, FSharpFunc<R, T>> f;

      public FillMissingUsing7(FSharpFunc<Series<R, T>, FSharpFunc<R, T>> f)
      {
        this.f = f;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal ISeries<R> Invoke(Series<R, T> s)
      {
        return (ISeries<R>) SeriesModule.FillMissingUsing<R, T>(this.f.Invoke(s), s);
      }
    }

    [Serializable]
    internal sealed class hasAllFlagVector1<R, C> : FSharpFunc<Unit, long>
    {
      public Frame<R, C> frame;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal hasAllFlagVector1(Frame<R, C> frame)
      {
        this.ctor();
        this.frame = frame;
      }

      public virtual long Invoke(Unit unitVar)
      {
        return this.frame.RowIndex.KeyCount;
      }
    }

    [Serializable]
    internal sealed class hasAllFlagVector2<C> : FSharpFunc<long, long>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IIndex<C> objectArg;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal hasAllFlagVector2(IIndex<C> objectArg)
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
    internal sealed class hasAllFlagVector3 : FSharpFunc<OptionalValue<object>, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal hasAllFlagVector3()
      {
        base.ctor();
      }

      public virtual bool Invoke(OptionalValue<object> opt)
      {
        return opt.HasValue;
      }
    }

    [Serializable]
    internal sealed class hasAllFlagVector3 : FSharpFunc<IVector<object>, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal hasAllFlagVector3()
      {
        base.ctor();
      }

      public virtual bool Invoke(IVector<object> rowReader)
      {
        return SeqModule.ForAll<OptionalValue<object>>((FSharpFunc<M0, bool>) new FrameModule.hasAllFlagVector3(), (IEnumerable<M0>) FVectorextensionscore.IVector`1get_DataSequence<object>(rowReader));
      }
    }

    [Serializable]
    internal sealed class newData76 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData76(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    [Serializable]
    internal sealed class hasAllFlagVector1 : FSharpFunc<OptionalValue<object>, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal hasAllFlagVector1()
      {
        base.ctor();
      }

      public virtual bool Invoke(OptionalValue<object> opt)
      {
        return opt.HasValue;
      }
    }

    [Serializable]
    internal sealed class newData67 : FSharpFunc<IVector, IVector>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder vectorBuilder;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Addressing.IAddressingScheme scheme;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction rowCmd;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal newData67(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
      {
        this.ctor();
        this.vectorBuilder = vectorBuilder;
        this.scheme = scheme;
        this.rowCmd = rowCmd;
      }

      public virtual IVector Invoke(IVector vector)
      {
        return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
      }
    }

    [Serializable]
    internal sealed class DropSparseColumns0 : FSharpFunc<OptionalValue<object>, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal DropSparseColumns0()
      {
        base.ctor();
      }

      public virtual bool Invoke(OptionalValue<object> o)
      {
        return o.HasValue;
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class DropSparseColumns8<C, R> : GeneratedSequenceBase<Tuple<C, IVector>>
    {
      public Frame<R, C> frame;
      public C colKey;
      public long addr;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public OptionalValue<IVector> matchValue;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public KeyValuePair<C, long> matchValue0;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<C, long> activePatternResult16348;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<KeyValuePair<C, long>> @enum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<C, IVector> v;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<Tuple<C, IVector>> enum0;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Tuple<C, IVector> current;

      public DropSparseColumns8(Frame<R, C> frame, C colKey, long addr, OptionalValue<IVector> matchValue, KeyValuePair<C, long> matchValue0, Tuple<C, long> activePatternResult16348, IEnumerator<KeyValuePair<C, long>> @enum, Tuple<C, IVector> v, IEnumerator<Tuple<C, IVector>> enum0, int pc, Tuple<C, IVector> current)
      {
        this.frame = frame;
        this.colKey = colKey;
        this.addr = addr;
        this.matchValue = matchValue;
        this.matchValue0 = matchValue0;
        this.activePatternResult16348 = activePatternResult16348;
        this.@enum = @enum;
        this.v = v;
        this.enum0 = enum0;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<Tuple<C, IVector>> next)
      {
        switch (this.pc)
        {
          case 1:
label_8:
            this.pc = 4;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<KeyValuePair<C, long>>>((M0) this.@enum);
            this.@enum = (IEnumerator<KeyValuePair<C, long>>) null;
            this.pc = 4;
            goto case 4;
          case 2:
label_7:
            this.pc = 1;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<C, IVector>>>((M0) this.enum0);
            this.enum0 = (IEnumerator<Tuple<C, IVector>>) null;
            this.matchValue = new OptionalValue<IVector>();
            this.addr = new long();
            this.colKey = default (C);
            this.activePatternResult16348 = (Tuple<C, long>) null;
            this.matchValue0 = new KeyValuePair<C, long>();
            break;
          case 3:
            this.v = (Tuple<C, IVector>) null;
            goto label_4;
          case 4:
            this.current = (Tuple<C, IVector>) null;
            return 0;
          default:
            this.@enum = this.frame.ColumnIndex.Mappings.GetEnumerator();
            this.pc = 1;
            break;
        }
        if (this.@enum.MoveNext())
        {
          this.matchValue0 = this.@enum.Current;
          this.activePatternResult16348 = (Tuple<C, long>) Operators.KeyValuePattern<C, long>((KeyValuePair<M0, M1>) this.matchValue0);
          this.colKey = this.activePatternResult16348.Item1;
          this.addr = this.activePatternResult16348.Item2;
          this.matchValue = this.frame.Data.GetValue(this.addr);
          FSharpChoice<Unit, IVector> fsharpChoice = OptionalValueModule.MissingPresent<IVector>(this.matchValue);
          this.enum0 = (!(fsharpChoice is FSharpChoice<Unit, IVector>.Choice2Of2) || !SeqModule.ForAll<OptionalValue<object>>((FSharpFunc<M0, bool>) new FrameModule.DropSparseColumns0(), (IEnumerable<M0>) ((FSharpChoice<Unit, IVector>.Choice2Of2) fsharpChoice).get_Item().ObjectSequence) ? (IEnumerable<Tuple<C, IVector>>) SeqModule.Empty<Tuple<C, IVector>>() : (IEnumerable<Tuple<C, IVector>>) SeqModule.Singleton<Tuple<C, IVector>>((M0) new Tuple<C, IVector>(this.colKey, ((FSharpChoice<Unit, IVector>.Choice2Of2) fsharpChoice).get_Item()))).GetEnumerator();
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
                    this.current = (Tuple<C, IVector>) null;
                    unit = (Unit) null;
                    break;
                  case 1:
                    this.pc = 4;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<KeyValuePair<C, long>>>((M0) this.@enum);
                    goto case 0;
                  case 2:
                    this.pc = 1;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<C, IVector>>>((M0) this.enum0);
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
      public virtual Tuple<C, IVector> get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<Tuple<C, IVector>> GetFreshEnumerator()
      {
        return (IEnumerator<Tuple<C, IVector>>) new FrameModule.DropSparseColumns8<C, R>(this.frame, default (C), new long(), new OptionalValue<IVector>(), new KeyValuePair<C, long>(), (Tuple<C, long>) null, (IEnumerator<KeyValuePair<C, long>>) null, (Tuple<C, IVector>) null, (IEnumerator<Tuple<C, IVector>>) null, 0, (Tuple<C, IVector>) null);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ZipAlignInto3<V1, V2, V>
    {
      public FSharpFunc<V1, FSharpFunc<V2, V>> op;

      public ZipAlignInto3(FSharpFunc<V1, FSharpFunc<V2, V>> op)
      {
        this.op = op;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal V Invoke(V1 a, V2 b)
      {
        return (V) FSharpFunc<V1, V2>.InvokeFast<V>((FSharpFunc<V1, FSharpFunc<V2, M0>>) this.op, a, b);
      }
    }

    [Serializable]
    internal sealed class ReduceLevel1<K, T, R> : FSharpFunc<Series<R, T>, Series<K, T>>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<R, K> level;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<T, FSharpFunc<T, T>> op;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ReduceLevel1(FSharpFunc<R, K> level, FSharpFunc<T, FSharpFunc<T, T>> op)
      {
        this.ctor();
        this.level = level;
        this.op = op;
      }

      public virtual Series<K, T> Invoke(Series<R, T> series)
      {
        return SeriesModule.ReduceLevel<R, K, T>(this.level, this.op, series);
      }
    }

    [Serializable]
    internal sealed class ReduceLevel1<K, T, R, C> : FSharpFunc<C, FSharpFunc<Series<R, T>, Series<K, T>>>
    {
      public FSharpFunc<R, K> levelSel;
      public FSharpFunc<T, FSharpFunc<T, T>> op;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ReduceLevel1(FSharpFunc<R, K> levelSel, FSharpFunc<T, FSharpFunc<T, T>> op)
      {
        this.ctor();
        this.levelSel = levelSel;
        this.op = op;
      }

      public virtual FSharpFunc<Series<R, T>, Series<K, T>> Invoke(C _arg1)
      {
        return (FSharpFunc<Series<R, T>, Series<K, T>>) new FrameModule.ReduceLevel1<K, T, R>(this.levelSel, this.op);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ApplyLevel4<K, T, R>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<R, K> level;

      public ApplyLevel4(FSharpFunc<R, K> level)
      {
        this.level = level;
        // ISSUE: explicit constructor call
        base.ctor();
      }

      internal K Invoke(KeyValuePair<R, T> kvp)
      {
        return this.level.Invoke(kvp.Key);
      }
    }

    [Serializable]
    internal sealed class ApplyLevel4<K, T, R, C> : OptimizedClosures.FSharpFunc<C, Series<R, T>, Series<K, T>>
    {
      public FSharpFunc<R, K> levelSel;
      public FSharpFunc<Series<R, T>, T> op;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ApplyLevel4(FSharpFunc<R, K> levelSel, FSharpFunc<Series<R, T>, T> op)
      {
        this.ctor();
        this.levelSel = levelSel;
        this.op = op;
      }

      public virtual Series<K, T> Invoke(C _arg1, Series<R, T> s)
      {
        FSharpFunc<R, K> levelSel = this.levelSel;
        return SeriesModule.MapValues<Series<R, T>, T, K>(this.op, s.GroupBy<K>(new Func<KeyValuePair<R, T>, K>(new FrameModule.ApplyLevel4<K, T, R>(levelSel).Invoke)));
      }
    }

    [Serializable]
    internal sealed class labels5<R1, R2> : FSharpFunc<Tuple<R1, R2>, R1>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal labels5()
      {
        base.ctor();
      }

      public virtual R1 Invoke(Tuple<R1, R2> tupledArg)
      {
        return tupledArg.Item1;
      }
    }

    [Serializable]
    internal sealed class Nest7<R1, R2> : FSharpFunc<Tuple<R1, R2>, R2>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Nest7()
      {
        base.ctor();
      }

      public virtual R2 Invoke(Tuple<R1, R2> tupledArg)
      {
        return tupledArg.Item2;
      }
    }

    [Serializable]
    internal sealed class Nest7<R1, R2, C> : OptimizedClosures.FSharpFunc<R1, Frame<Tuple<R1, R2>, C>, Frame<R2, C>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Nest7()
      {
        base.ctor();
      }

      public virtual Frame<R2, C> Invoke(R1 r, Frame<Tuple<R1, R2>, C> df)
      {
        Frame<Tuple<R1, R2>, C> frame = df;
        return FrameModule.IndexRowsWith<R2, Tuple<R1, R2>, C>((IEnumerable<R2>) SeqModule.Map<Tuple<R1, R2>, R2>((FSharpFunc<M0, M1>) new FrameModule.Nest7<R1, R2>(), (IEnumerable<M0>) df.RowKeys), frame);
      }
    }

    [Serializable]
    internal sealed class Unnest4<R1, R2> : FSharpFunc<R2, Tuple<R1, R2>>
    {
      public R1 k1;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Unnest4(R1 k1)
      {
        this.ctor();
        this.k1 = k1;
      }

      public virtual Tuple<R1, R2> Invoke(R2 k2)
      {
        return new Tuple<R1, R2>(this.k1, k2);
      }
    }

    [Serializable]
    internal sealed class Unnest2<R1, R2, C> : OptimizedClosures.FSharpFunc<R1, Frame<R2, C>, Frame<Tuple<R1, R2>, C>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Unnest2()
      {
        base.ctor();
      }

      public virtual Frame<Tuple<R1, R2>, C> Invoke(R1 k1, Frame<R2, C> df)
      {
        return FrameModule.IndexRowsWith<Tuple<R1, R2>, R2, C>((IEnumerable<Tuple<R1, R2>>) SeqModule.Map<R2, Tuple<R1, R2>>((FSharpFunc<M0, M1>) new FrameModule.Unnest4<R1, R2>(k1), (IEnumerable<M0>) df.RowKeys), df);
      }
    }
  }
}
