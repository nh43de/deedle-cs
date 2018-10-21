// Decompiled with JetBrains decompiler
// Type: Deedle.F# Frame extensions
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Indices;
using Deedle.Vectors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deedle
{
  
  public static class FFrameextensions
  {
    public static Tuple<TLeft, TRight> op_EqualsGreater<TLeft, TRight>(TLeft a, TRight b)
    {
        return new Tuple<TLeft, TRight>(a, b);
    }

    
    
    public static Tuple<a, ISeries<b>> op_EqualsQmarkGreater<TLeft, TRight>(a a, ISeries<b> b)
    {
      return new Tuple<a, ISeries<b>>(a, b);
    }

    public static Frame<c, a> frame<a, b, c>(IEnumerable<Tuple<TLeft, TRight>> columns) where b : ISeries<c>
    {
      Tuple<a[], b[]> tuple1 = ArrayModule.Unzip<TLeft, TRight>((Tuple<M0, M1>[]) ArrayModule.OfSeq<Tuple<TLeft, TRight>>((IEnumerable<M0>) columns));
      b[] values = tuple1.Item2;
      a[] keys = tuple1.Item1;
      Tuple<FSharpList<IVectorBuilder>, FSharpList<IIndexBuilder>> tuple2 = new Tuple<FSharpList<IVectorBuilder>, FSharpList<IIndexBuilder>>((FSharpList<IVectorBuilder>) ListModule.OfSeq<IVectorBuilder>(SeqModule.Distinct<IVectorBuilder>((IEnumerable<M0>) SeqModule.ToList<IVectorBuilder>((IEnumerable<M0>) new FFrameextensions.vbs<b, c>(values, default (b), (IEnumerator<b>) null, 0, (IVectorBuilder) null)))), (FSharpList<IIndexBuilder>) ListModule.OfSeq<IIndexBuilder>(SeqModule.Distinct<IIndexBuilder>((IEnumerable<M0>) SeqModule.ToList<IIndexBuilder>((IEnumerable<M0>) new FFrameextensions.ibs<b, c>(values, default (b), (IEnumerator<b>) null, 0, (IIndexBuilder) null)))));
      Tuple<IVectorBuilder, IIndexBuilder> tuple3;
      if (tuple2.Item1.get_TailOrNull() != null)
      {
        FSharpList<IVectorBuilder> fsharpList1 = tuple2.Item1;
        if (fsharpList1.get_TailOrNull().get_TailOrNull() == null && tuple2.Item2.get_TailOrNull() != null)
        {
          FSharpList<IIndexBuilder> fsharpList2 = tuple2.Item2;
          if (fsharpList2.get_TailOrNull().get_TailOrNull() == null)
          {
            tuple3 = new Tuple<IVectorBuilder, IIndexBuilder>(fsharpList1.get_HeadOrDefault(), fsharpList2.get_HeadOrDefault());
            goto label_5;
          }
        }
      }
      tuple3 = new Tuple<IVectorBuilder, IIndexBuilder>(FVectorBuilderimplementation.VectorBuilder.Instance, FIndexBuilderimplementation.IndexBuilder.Instance);
label_5:
      Tuple<IVectorBuilder, IIndexBuilder> tuple4 = tuple3;
      IVectorBuilder vectorBuilder = tuple4.Item1;
      return FrameUtils.fromColumns<c, a, b>(tuple4.Item2, vectorBuilder, new Series<TLeft, TRight>(keys, values));
    }

    public static Frame<R, string> FrameReadCsvStatic<R>(string path, string indexCol, [OptionalArgument] FSharpOption<bool> hasHeaders, [OptionalArgument] FSharpOption<bool> inferTypes, [OptionalArgument] FSharpOption<int> inferRows, [OptionalArgument] FSharpOption<string> schema, [OptionalArgument] FSharpOption<string> separators, [OptionalArgument] FSharpOption<string> culture, [OptionalArgument] FSharpOption<int> maxRows, [OptionalArgument] FSharpOption<string[]> missingValues, [OptionalArgument] FSharpOption<bool> preferOptions)
    {
      StreamReader streamReader = new StreamReader(path);
      try
      {
        return FrameModule.IndexRows<string, int, R>(indexCol, FrameUtilsModule.readCsv((TextReader) streamReader, hasHeaders, inferTypes, inferRows, schema, missingValues, separators, culture, maxRows, preferOptions));
      }
      finally
      {
        (streamReader as IDisposable)?.Dispose();
      }
    }

    public static Frame<int, string> FrameReadCsvStatic(string path, [OptionalArgument] FSharpOption<bool> hasHeaders, [OptionalArgument] FSharpOption<bool> inferTypes, [OptionalArgument] FSharpOption<int> inferRows, [OptionalArgument] FSharpOption<string> schema, [OptionalArgument] FSharpOption<string> separators, [OptionalArgument] FSharpOption<string> culture, [OptionalArgument] FSharpOption<int> maxRows, [OptionalArgument] FSharpOption<string[]> missingValues, [OptionalArgument] FSharpOption<bool> preferOptions)
    {
      StreamReader streamReader = new StreamReader(path);
      try
      {
        return FrameUtilsModule.readCsv((TextReader) streamReader, hasHeaders, inferTypes, inferRows, schema, missingValues, separators, culture, maxRows, preferOptions);
      }
      finally
      {
        (streamReader as IDisposable)?.Dispose();
      }
    }

    public static Frame<int, string> FrameReadCsvStatic(Stream stream, [OptionalArgument] FSharpOption<bool> hasHeaders, [OptionalArgument] FSharpOption<bool> inferTypes, [OptionalArgument] FSharpOption<int> inferRows, [OptionalArgument] FSharpOption<string> schema, [OptionalArgument] FSharpOption<string> separators, [OptionalArgument] FSharpOption<string> culture, [OptionalArgument] FSharpOption<int> maxRows, [OptionalArgument] FSharpOption<string[]> missingValues, [OptionalArgument] FSharpOption<bool> preferOptions)
    {
      return FrameUtilsModule.readCsv((TextReader) new StreamReader(stream), hasHeaders, inferTypes, inferRows, schema, missingValues, separators, culture, maxRows, preferOptions);
    }

    public static Frame<int, string> FrameReadCsvStatic(TextReader reader, [OptionalArgument] FSharpOption<bool> hasHeaders, [OptionalArgument] FSharpOption<bool> inferTypes, [OptionalArgument] FSharpOption<int> inferRows, [OptionalArgument] FSharpOption<string> schema, [OptionalArgument] FSharpOption<string> separators, [OptionalArgument] FSharpOption<string> culture, [OptionalArgument] FSharpOption<int> maxRows, [OptionalArgument] FSharpOption<string[]> missingValues, [OptionalArgument] FSharpOption<bool> preferOptions)
    {
      return FrameUtilsModule.readCsv(reader, hasHeaders, inferTypes, inferRows, schema, missingValues, separators, culture, maxRows, preferOptions);
    }

    public static Frame<long, K> FrameofRowsOrdinalStatic<e, K, V>(IEnumerable<e> rows) where e : Series<K, V>
    {
      IVector<e> vector = FVectorBuilderimplementation.VectorBuilder.Instance.Create<e>(ArrayModule.OfSeq<e>(rows));
      return FrameUtils.fromRows<long, K, e>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, new Series<long, e>(FIndexBuilderimplementation.IndexBuilder.Instance.Create<long>((IEnumerable<long>) Operators.CreateSequence<long>((IEnumerable<M0>) Operators.OperatorIntrinsics.RangeInt64(0L, 1L, vector.Length - 1L)), FSharpOption<bool>.Some(true)), vector, FVectorBuilderimplementation.VectorBuilder.Instance, FIndexBuilderimplementation.IndexBuilder.Instance));
    }

    public static Frame<R, C> FrameofRowsStatic<R, d, C>(IEnumerable<Tuple<R, d>> rows) where d : ISeries<C>
    {
      Tuple<FSharpList<R>, FSharpList<d>> tuple = ListModule.Unzip<R, d>((FSharpList<Tuple<M0, M1>>) ListModule.OfSeq<Tuple<R, d>>((IEnumerable<M0>) rows));
      FSharpList<d> fsharpList = tuple.Item2;
      return FrameUtils.fromRows<R, C, d>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, new Series<R, d>((IEnumerable<R>) tuple.Item1, (IEnumerable<d>) fsharpList));
    }

    public static Frame<R, C> FrameofRowsStatic<R, c, C>(Series<R, c> rows) where c : ISeries<C>
    {
      return FrameUtils.fromRows<R, C, c>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, rows);
    }

    public static Frame<R, string> FrameofRowKeysStatic<R>(IEnumerable<R> keys)
    {
      return Frame.FromRowKeys<R>(keys);
    }

    public static Frame<R, C> FrameofColumnsStatic<C, b, R>(Series<C, b> cols) where b : ISeries<R>
    {
      return FrameUtils.fromColumns<R, C, b>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, cols);
    }

    public static Frame<R, C> FrameofColumnsStatic<C, a, R>(IEnumerable<Tuple<C, a>> cols) where a : ISeries<R>
    {
      Tuple<FSharpList<C>, FSharpList<a>> tuple = ListModule.Unzip<C, a>((FSharpList<Tuple<M0, M1>>) ListModule.OfSeq<Tuple<C, a>>((IEnumerable<M0>) cols));
      FSharpList<a> fsharpList = tuple.Item2;
      return FrameUtils.fromColumns<R, C, a>(FIndexBuilderimplementation.IndexBuilder.Instance, FVectorBuilderimplementation.VectorBuilder.Instance, new Series<C, a>((IEnumerable<C>) tuple.Item1, (IEnumerable<a>) fsharpList));
    }

    public static Frame<R, C> FrameofValuesStatic<R, C, V>(IEnumerable<Tuple<R, C, V>> values)
    {
      return Frame.FromValues<R, C, V>(values);
    }

    public static Frame<K, string> FrameofRecordsStatic<K, R>(Series<K, R> series)
    {
      return Frame.FromRecords<K, R>(series);
    }

    public static Frame<int, string> FrameofRecordsStatic<T>(IEnumerable<T> values)
    {
      return Reflection.convertRecordSequence<T>(values);
    }

    public static Frame<R, string> FrameofRecordsStatic<R>(IEnumerable values, string indexCol)
    {
      return Reflection.convertRecordSequenceUntyped(values).IndexRows<R>(indexCol);
    }

    public static Frame<int, int> FrameofArray2DStatic<T>(T[,] array)
    {
      return Frame.FromArray2D<T>(array);
    }

    
    public static Frame<R, C> Frame`2PivotTable<TRowKey, TColumnKey, R, C, T>(Frame<TRowKey, TColumnKey> frame, TColumnKey r, TColumnKey c, FSharpFunc<Frame<TRowKey, TColumnKey>, T> op)
    {
      return FrameModule.PivotTable<TRowKey, TColumnKey, R, C, T>((FSharpFunc<TRowKey, FSharpFunc<ObjectSeries<TColumnKey>, R>>) new FFrameextensions.Frame\u002DPivotTable<TRowKey, TColumnKey, R>(r), (FSharpFunc<TRowKey, FSharpFunc<ObjectSeries<TColumnKey>, C>>) new FFrameextensions.Frame\u002DPivotTable<TRowKey, TColumnKey, C>(c), op, frame);
    }

    
    public static void Frame`2SaveCsv<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame, TextWriter writer, [OptionalArgument] FSharpOption<bool> includeRowKeys, [OptionalArgument] FSharpOption<IEnumerable<string>> keyNames, [OptionalArgument] FSharpOption<char> separator, [OptionalArgument] FSharpOption<CultureInfo> culture)
    {
      FrameUtilsModule.writeCsv<TRowKey, TColumnKey>(writer, (FSharpOption<string>) null, separator, culture, includeRowKeys, keyNames, frame);
    }

    
    public static void Frame`2SaveCsv<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame, string path, [OptionalArgument] FSharpOption<bool> includeRowKeys, [OptionalArgument] FSharpOption<IEnumerable<string>> keyNames, [OptionalArgument] FSharpOption<char> separator, [OptionalArgument] FSharpOption<CultureInfo> culture)
    {
      StreamWriter streamWriter = new StreamWriter(path);
      try
      {
        FrameUtilsModule.writeCsv<TRowKey, TColumnKey>((TextWriter) streamWriter, FSharpOption<string>.Some(path), separator, culture, includeRowKeys, keyNames, frame);
      }
      finally
      {
        (streamWriter as IDisposable)?.Dispose();
      }
    }

    
    public static void Frame`2SaveCsv<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame, string path, IEnumerable<string> keyNames)
    {
      StreamWriter streamWriter = new StreamWriter(path);
      try
      {
        FrameUtilsModule.writeCsv<TRowKey, TColumnKey>((TextWriter) streamWriter, FSharpOption<string>.Some(path), (FSharpOption<char>) null, (FSharpOption<CultureInfo>) null, FSharpOption<bool>.Some(true), FSharpOption<IEnumerable<string>>.Some(keyNames), frame);
      }
      finally
      {
        (streamWriter as IDisposable)?.Dispose();
      }
    }

    
    public static DataTable Frame`2ToDataTable<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame, IEnumerable<string> rowKeyNames)
    {
      return FrameUtilsModule.toDataTable<TRowKey, TColumnKey>(rowKeyNames, frame);
    }

    [Obsolete("Use overload taking TextWriter instead")]
    
    public static void Frame`2SaveCsv<TRowKey, TColumnKey>(Frame<TRowKey, TColumnKey> frame, Stream stream, [OptionalArgument] FSharpOption<bool> includeRowKeys, [OptionalArgument] FSharpOption<IEnumerable<string>> keyNames, [OptionalArgument] FSharpOption<char> separator, [OptionalArgument] FSharpOption<CultureInfo> culture)
    {
      StreamWriter streamWriter = new StreamWriter(stream);
      try
      {
        FrameUtilsModule.writeCsv<TRowKey, TColumnKey>((TextWriter) streamWriter, (FSharpOption<string>) null, separator, culture, includeRowKeys, keyNames, frame);
      }
      finally
      {
        (streamWriter as IDisposable)?.Dispose();
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class vbs<b, c> : GeneratedSequenceBase<IVectorBuilder> where b : ISeries<c>
    {
      public b[] values;
      public b s;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<b> @enum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IVectorBuilder current;

      public vbs(b[] values, b s, IEnumerator<b> @enum, int pc, IVectorBuilder current)
      {
        this.values = values;
        this.s = s;
        this.@enum = @enum;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<IVectorBuilder> next)
      {
        switch (this.pc)
        {
          case 1:
label_5:
            this.pc = 3;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<b>>((M0) this.@enum);
            this.@enum = (IEnumerator<b>) null;
            this.pc = 3;
            goto case 3;
          case 2:
            this.s = default (b);
            break;
          case 3:
            this.current = (IVectorBuilder) null;
            return 0;
          default:
            this.@enum = ((IEnumerable<b>) this.values).GetEnumerator();
            this.pc = 1;
            break;
        }
        if (this.@enum.MoveNext())
        {
          this.s = this.@enum.Current;
          this.pc = 2;
          this.current = this.s.VectorBuilder;
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
                    this.current = (IVectorBuilder) null;
                    unit = (Unit) null;
                    break;
                  case 1:
                    this.pc = 3;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<b>>((M0) this.@enum);
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
      public virtual IVectorBuilder get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<IVectorBuilder> GetFreshEnumerator()
      {
        return (IEnumerator<IVectorBuilder>) new FFrameextensions.vbs<b, c>(this.values, default (b), (IEnumerator<b>) null, 0, (IVectorBuilder) null);
      }
    }

    
    [Serializable]
    
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    internal sealed class ibs<b, c> : GeneratedSequenceBase<IIndexBuilder> where b : ISeries<c>
    {
      public b[] values;
      public b s;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerator<b> @enum;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int pc;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IIndexBuilder current;

      public ibs(b[] values, b s, IEnumerator<b> @enum, int pc, IIndexBuilder current)
      {
        this.values = values;
        this.s = s;
        this.@enum = @enum;
        this.pc = pc;
        this.current = current;
        this.ctor();
      }

      public virtual int GenerateNext(ref IEnumerable<IIndexBuilder> next)
      {
        switch (this.pc)
        {
          case 1:
label_5:
            this.pc = 3;
            LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<b>>((M0) this.@enum);
            this.@enum = (IEnumerator<b>) null;
            this.pc = 3;
            goto case 3;
          case 2:
            this.s = default (b);
            break;
          case 3:
            this.current = (IIndexBuilder) null;
            return 0;
          default:
            this.@enum = ((IEnumerable<b>) this.values).GetEnumerator();
            this.pc = 1;
            break;
        }
        if (this.@enum.MoveNext())
        {
          this.s = this.@enum.Current;
          this.pc = 2;
          this.current = this.s.Index.Builder;
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
                    this.current = (IIndexBuilder) null;
                    unit = (Unit) null;
                    break;
                  case 1:
                    this.pc = 3;
                    LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<b>>((M0) this.@enum);
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
      public virtual IIndexBuilder get_LastGenerated()
      {
        return this.current;
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public virtual IEnumerator<IIndexBuilder> GetFreshEnumerator()
      {
        return (IEnumerator<IIndexBuilder>) new FFrameextensions.ibs<b, c>(this.values, default (b), (IEnumerator<b>) null, 0, (IIndexBuilder) null);
      }
    }

    [Serializable]
    internal sealed class Frame\u002DPivotTable<TRowKey, TColumnKey, R> : OptimizedClosures.FSharpFunc<TRowKey, ObjectSeries<TColumnKey>, R>
    {
      public TColumnKey r;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Frame\u002DPivotTable(TColumnKey r)
      {
        this.ctor();
        this.r = r;
      }

      public virtual R Invoke(TRowKey k, ObjectSeries<TColumnKey> os)
      {
        return os.GetAs<R>(this.r);
      }
    }

    [Serializable]
    internal sealed class Frame\u002DPivotTable<TRowKey, TColumnKey, C> : OptimizedClosures.FSharpFunc<TRowKey, ObjectSeries<TColumnKey>, C>
    {
      public TColumnKey c;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Frame\u002DPivotTable(TColumnKey c)
      {
        this.ctor();
        this.c = c;
      }

      public virtual C Invoke(TRowKey k, ObjectSeries<TColumnKey> os)
      {
        return os.GetAs<C>(this.c);
      }
    }
  }
}
