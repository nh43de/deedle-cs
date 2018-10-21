// Decompiled with JetBrains decompiler
// Type: Deedle.Frame
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Indices;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using Deedle.Indices;

namespace Deedle
{
  [Serializable]
  public class Frame
  {


    public static Frame<int, string> ReadCsv(string location, [Optional] bool? hasHeaders, [Optional] bool? inferTypes, [Optional] int? inferRows, [Optional] string schema, [Optional] string separators, [Optional] string culture, [Optional] int? maxRows, [Optional] string[] missingValues, [Optional] bool preferOptions)
    {
      StreamReader streamReader = new StreamReader(location);
      try
      {
        return FrameUtilsModule.readCsv((TextReader) streamReader, !hasHeaders.HasValue ? (FSharpOption<bool>) null : FSharpOption<bool>.Some(hasHeaders.Value), !inferTypes.HasValue ? (FSharpOption<bool>) null : FSharpOption<bool>.Some(inferTypes.Value), !inferRows.HasValue ? (FSharpOption<int>) null : FSharpOption<int>.Some(inferRows.Value), FSharpOption<string>.Some(schema), FSharpOption<string[]>.Some(missingValues), !string.Equals(separators, (string) null) ? FSharpOption<string>.Some(separators) : (FSharpOption<string>) null, FSharpOption<string>.Some(culture), !maxRows.HasValue ? (FSharpOption<int>) null : FSharpOption<int>.Some(maxRows.Value), FSharpOption<bool>.Some(preferOptions));
      }
      finally
      {
        (streamReader as IDisposable)?.Dispose();
      }
    }

    public static Frame<int, string> ReadReader(IDataReader reader)
    {
      return FrameUtilsModule.readReader(reader);
    }

    
    public static Frame<ColKey, int> FromColumns<ColKey, V>(IEnumerable<Series<ColKey, V>> cols)
    {
      return FrameUtils.fromColumns<ColKey, int, Series<ColKey, V>>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, new Series<int, Series<ColKey, V>>((IEnumerable<int>) SeqModule.MapIndexed<Series<ColKey, V>, int>((FSharpFunc<int, FSharpFunc<M0, M1>>) new FrameExtensions.FromColumns<ColKey, V>(), (IEnumerable<M0>) cols), cols));
    }

    
    public static Frame<RowKey, ColKey> FromColumns<ColKey, RowKey, V>(IEnumerable<KeyValuePair<ColKey, Series<RowKey, V>>> columns)
    {
      return FrameUtils.fromColumns<RowKey, ColKey, Series<RowKey, V>>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, new Series<ColKey, Series<RowKey, V>>((IEnumerable<ColKey>) SeqModule.Map<KeyValuePair<ColKey, Series<RowKey, V>>, ColKey>((FSharpFunc<M0, M1>) new FrameExtensions.colKeys<ColKey, RowKey, V>(), (IEnumerable<M0>) columns), (IEnumerable<Series<RowKey, V>>) SeqModule.Map<KeyValuePair<ColKey, Series<RowKey, V>>, Series<RowKey, V>>((FSharpFunc<M0, M1>) new FrameExtensions.colSeries<ColKey, RowKey, V>(), (IEnumerable<M0>) columns)));
    }

    
    public static Frame<RowKey, ColKey> FromColumns<ColKey, RowKey>(IEnumerable<KeyValuePair<ColKey, ObjectSeries<RowKey>>> columns)
    {
      return FrameUtils.fromColumns<RowKey, ColKey, ObjectSeries<RowKey>>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, new Series<ColKey, ObjectSeries<RowKey>>((IEnumerable<ColKey>) SeqModule.Map<KeyValuePair<ColKey, ObjectSeries<RowKey>>, ColKey>((FSharpFunc<M0, M1>) new FrameExtensions.colKeys<ColKey, RowKey>(), (IEnumerable<M0>) columns), (IEnumerable<ObjectSeries<RowKey>>) SeqModule.Map<KeyValuePair<ColKey, ObjectSeries<RowKey>>, ObjectSeries<RowKey>>((FSharpFunc<M0, M1>) new FrameExtensions.colSeries<ColKey, RowKey>(), (IEnumerable<M0>) columns)));
    }

    

    public static Frame<TRowKey, TColKey> FromColumns<TColKey, TRowKey>(Series<TColKey, ObjectSeries<TRowKey>> cols)
    {
      return FrameUtils.fromColumns<TRowKey, TColKey, ObjectSeries<TRowKey>>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, cols);
    }

    

    public static Frame<TRowKey, TColKey> FromColumns<TColKey, TRowKey, V>(Series<TColKey, Series<TRowKey, V>> cols)
    {
      return FrameUtils.fromColumns<TRowKey, TColKey, Series<TRowKey, V>>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, cols);
    }

    
    public static Frame<int, ColKey> FromRows<ColKey, V>(IEnumerable<Series<ColKey, V>> rows)
    {
      return FrameUtils.fromRows<int, ColKey, Series<ColKey, V>>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, new Series<int, Series<ColKey, V>>((IEnumerable<int>) SeqModule.MapIndexed<Series<ColKey, V>, int>((FSharpFunc<int, FSharpFunc<M0, M1>>) new FrameExtensions.FromRows<ColKey, V>(), (IEnumerable<M0>) rows), rows));
    }

    

    public static Frame<RowKey, ColKey> FromRows<RowKey, ColKey, V>(IEnumerable<KeyValuePair<RowKey, Series<ColKey, V>>> rows)
    {
      return FrameUtils.fromRows<RowKey, ColKey, Series<ColKey, V>>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, new Series<RowKey, Series<ColKey, V>>((IEnumerable<RowKey>) SeqModule.Map<KeyValuePair<RowKey, Series<ColKey, V>>, RowKey>((FSharpFunc<M0, M1>) new FrameExtensions.rowKeys<RowKey, ColKey, V>(), (IEnumerable<M0>) rows), (IEnumerable<Series<ColKey, V>>) SeqModule.Map<KeyValuePair<RowKey, Series<ColKey, V>>, Series<ColKey, V>>((FSharpFunc<M0, M1>) new FrameExtensions.rowSeries<RowKey, ColKey, V>(), (IEnumerable<M0>) rows)));
    }

    

    public static Frame<RowKey, ColKey> FromRows<RowKey, ColKey>(IEnumerable<KeyValuePair<RowKey, ObjectSeries<ColKey>>> rows)
    {
      return FrameUtils.fromRows<RowKey, ColKey, ObjectSeries<ColKey>>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, new Series<RowKey, ObjectSeries<ColKey>>((IEnumerable<RowKey>) SeqModule.Map<KeyValuePair<RowKey, ObjectSeries<ColKey>>, RowKey>((FSharpFunc<M0, M1>) new FrameExtensions.rowKeys<RowKey, ColKey>(), (IEnumerable<M0>) rows), (IEnumerable<ObjectSeries<ColKey>>) SeqModule.Map<KeyValuePair<RowKey, ObjectSeries<ColKey>>, ObjectSeries<ColKey>>((FSharpFunc<M0, M1>) new FrameExtensions.rowSeries<RowKey, ColKey>(), (IEnumerable<M0>) rows)));
    }

    

    public static Frame<TColKey, TRowKey> FromRows<TColKey, TRowKey>(Series<TColKey, ObjectSeries<TRowKey>> rows)
    {
      return FrameUtils.fromRows<TColKey, TRowKey, ObjectSeries<TRowKey>>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, rows);
    }

    

    public static Frame<TColKey, TRowKey> FromRows<TColKey, TRowKey, V>(Series<TColKey, Series<TRowKey, V>> rows)
    {
      return FrameUtils.fromRows<TColKey, TRowKey, Series<TRowKey, V>>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, rows);
    }

    

    public static Frame<R, C> FromValues<T, C, R, V>(IEnumerable<T> values, Func<T, C> colSel, Func<T, R> rowSel, Func<T, V> valSel)
    {
      return FrameUtilsModule.fromValues<T, C, R, V>(values, (FSharpFunc<T, C>) new FrameExtensions.FromValues<T, C>(colSel), (FSharpFunc<T, R>) new FrameExtensions.FromValues<T, R>(rowSel), (FSharpFunc<T, V>) new FrameExtensions.FromValues<T, V>(valSel));
    }

    

    public static Frame<a, b> FromValues<a, b, c>(IEnumerable<Tuple<a, b, c>> values)
    {
      return FrameUtilsModule.fromValues<Tuple<a, b, c>, b, a, c>(values, (FSharpFunc<Tuple<a, b, c>, b>) new FrameExtensions.FromValues<a, b, c>(), (FSharpFunc<Tuple<a, b, c>, a>) new FrameExtensions.FromValues<a, b, c>(), (FSharpFunc<Tuple<a, b, c>, c>) new FrameExtensions.FromValues<a, b, c>());
    }

    

    public static Frame<K, string> FromRecords<K, R>(Series<K, R> series)
    {
      IEnumerable<Tuple<K, R>> tuples = (IEnumerable<Tuple<K, R>>) new FrameExtensions.keyValuePairs<K, R>(series, (Tuple<K, FSharpOption<R>>) null, (FSharpOption<R>) null, default (K), (IEnumerator<Tuple<K, FSharpOption<R>>>) null, 0, (Tuple<K, R>) null);
      Frame<int, string> frame = Reflection.convertRecordSequence<R>(SeqModule.Map<Tuple<K, R>, R>((FSharpFunc<M0, M1>) new FrameExtensions.recordsToConvert<K, R>(), (IEnumerable<M0>) tuples));
      return FrameModule.IndexRowsWith<K, int, string>((IEnumerable<K>) SeqModule.Map<Tuple<K, R>, K>((FSharpFunc<M0, M1>) new FrameExtensions.FromRecords<K, R>(), (IEnumerable<M0>) tuples), frame);
    }

    

    public static Frame<int, string> FromRecords<T>(IEnumerable<T> values)
    {
      return Reflection.convertRecordSequence<T>(values);
    }

    

    public static Frame<int, int> FromArray2D<T>(T[,] array)
    {
      IIndexBuilder instance1 = FIndexBuilderimplementation.IndexBuilder.Instance;
      int length1 = array.GetLength(0);
      FSharpFunc<int, int> fsharpFunc1 = (FSharpFunc<int, int>) new FrameExtensions.rowIndex();
      if (length1 < 0)
        throw new ArgumentException(string.Format("{0}\n{1} = {2}", new object[3]
        {
          (object) LanguagePrimitives.ErrorStrings.get_InputMustBeNonNegativeString(),
          (object) "count",
          (object) length1
        }), "count");
      int[] numArray1 = new int[length1];
            IIndexBuilder indexBuilder1 = instance1;
      for (int index = 0; index < numArray1.Length; ++index)
        numArray1[index] = fsharpFunc1.Invoke(index);
      IIndex<int> rowIndex = indexBuilder1.Create<int>((IEnumerable<int>) numArray1, FSharpOption<bool>.Some(true));
      IIndexBuilder instance2 = FIndexBuilderimplementation.IndexBuilder.Instance;
      int length2 = array.GetLength(1);
      FSharpFunc<int, int> fsharpFunc2 = (FSharpFunc<int, int>) new FrameExtensions.colIndex();
      if (length2 < 0)
        throw new ArgumentException(string.Format("{0}\n{1} = {2}", new object[3]
        {
          (object) LanguagePrimitives.ErrorStrings.get_InputMustBeNonNegativeString(),
          (object) "count",
          (object) length2
        }), "count");
      int[] numArray2 = new int[length2];
      IIndexBuilder indexBuilder2 = instance2;
      for (int index = 0; index < numArray2.Length; ++index)
        numArray2[index] = fsharpFunc2.Invoke(index);
      IIndex<int> columnIndex = indexBuilder2.Create<int>((IEnumerable<int>) numArray2, FSharpOption<bool>.Some(true));
      IVector[] vectorArray = (IVector[]) ArrayModule.ZeroCreate<IVector>(array.GetLength(1));
      for (int c = 0; c < vectorArray.Length; ++c)
      {
        int length3 = array.GetLength(0);
        FSharpFunc<int, T> fsharpFunc3 = (FSharpFunc<int, T>) new FrameExtensions.col<T>(array, c);
        if (length3 < 0)
          throw new ArgumentException(string.Format("{0}\n{1} = {2}", new object[3]
          {
            (object) LanguagePrimitives.ErrorStrings.get_InputMustBeNonNegativeString(),
            (object) "count",
            (object) length3
          }), "count");
        T[] objArray1 = new T[length3];
        for (int index = 0; index < objArray1.Length; ++index)
          objArray1[index] = fsharpFunc3.Invoke(index);
        T[] objArray2 = objArray1;
        vectorArray[c] = (IVector) FVectorBuilderimplementation.VectorBuilder.Instance.Create<T>(objArray2);
      }
      IVector<IVector> data = FVectorBuilderimplementation.VectorBuilder.Instance.Create<IVector>(vectorArray);
      return new Frame<int, int>(rowIndex, columnIndex, data, FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance);
    }

    

    public static Frame<R, C> CreateEmpty<R, C>()
    {
      return new Frame<R, C>(FIndexextensions.Index.ofKeys<R>(FSharpList<R>.get_Empty()), FIndexextensions.Index.ofKeys<C>(FSharpList<C>.get_Empty()), FVectorBuilderimplementation.VectorBuilder.Instance.Create<IVector>((IVector[]) ArrayModule.OfSeq<IVector>((IEnumerable<M0>) FSharpList<IVector>.get_Empty())), FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance);
    }

    

    public static Frame<K, string> FromRowKeys<K>(IEnumerable<K> keys)
    {
      return new Frame<K, string>(FrameUtils.indexBuilder.Create<K>(keys, (FSharpOption<bool>) null), FrameUtils.indexBuilder.Create<string>((IEnumerable<string>) FSharpList<string>.get_Empty(), (FSharpOption<bool>) null), FrameUtils.vectorBuilder.Create<IVector>(new IVector[0]), FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance);
    }
  }
}
