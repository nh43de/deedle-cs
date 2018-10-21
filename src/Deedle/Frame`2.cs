// Decompiled with JetBrains decompiler
// Type: Deedle.Frame`2
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using Deedle.Indices;
using Deedle.Internal;
using Deedle.Keys;
using Deedle.Vectors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Deedle
{
    [Serializable]
    public class Frame<TRowKey, TColumnKey> : IDynamicMetaObjectProvider, INotifyCollectionChanged, IFsiFormattable, IFrame
    {
        internal IVectorBuilder vectorBuilder;
        internal IIndexBuilder indexBuilder;
        internal bool isEmpty;
        internal IIndex<TRowKey> rowIndex;
        internal IIndex<TColumnKey> columnIndex;
        internal IVector<IVector> data;
        internal FSharpDelegateEvent<NotifyCollectionChangedEventHandler> frameColumnsChanged;

        public Frame(IIndex<TRowKey> rowIndex, IIndex<TColumnKey> columnIndex, IVector<IVector> data, IIndexBuilder indexBuilder, IVectorBuilder vectorBuilder)
        {
            Frame<TRowKey, TColumnKey> frame = this;
            this.indexBuilder = indexBuilder;
            this.vectorBuilder = vectorBuilder;
            IEnumerator<OptionalValue<IVector>> enumerator = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002Eget_DataSequence<IVector>(data).GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    OptionalValue<IVector> current = enumerator.Current;
                    if (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Addressing.IAddressingScheme>((M0)rowIndex.AddressingScheme, (M0)current.Value.AddressingScheme))
                        throw new InvalidOperationException("Row index and vectors of a frame should share addressing scheme!");
                }
            }
            finally
            {
                (enumerator as IDisposable)?.Dispose();
            }
            if (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Addressing.IAddressingScheme>((M0)columnIndex.AddressingScheme, (M0)data.AddressingScheme))
                throw new InvalidOperationException("Column index and data vector of a frame should share addressing scheme!");
            this.isEmpty = rowIndex.IsEmpty && columnIndex.IsEmpty;
            this.rowIndex\u004099 = rowIndex;
            this.columnIndex\u0040100 = columnIndex;
            this.data = data;
            this.frameColumnsChanged = new FSharpDelegateEvent<NotifyCollectionChangedEventHandler>();
        }

        [CompilationArgumentCounts(new int[] { 1, 1, 1, 1 })]
        internal static Frame<b, c> FromColumnsNonGeneric<a, b, c>(IIndexBuilder indexBuilder, IVectorBuilder vectorBuilder, FSharpFunc<a, ISeries<b>> seriesConv, Series<c, a> nested)
        {
            return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<a, b, c>(indexBuilder, vectorBuilder, seriesConv, nested);
        }

        internal void setColumnIndex(IIndex<TColumnKey> newColumnIndex)
        {
            this.columnIndex\u0040100 = newColumnIndex;
            this.frameColumnsChanged.Trigger(new object[2]
            {
        (object) this,
        (object) new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset)
            });
        }

        internal IIndexBuilder IndexBuilder
        {
            get
            {
                return this.indexBuilder;
            }
        }

        internal IVectorBuilder VectorBuilder
        {
            get
            {
                return this.vectorBuilder;
            }
        }

        internal IVector<IVector> Data
        {
            get
            {
                return this.data;
            }
        }

        public IIndex<TRowKey> RowIndex
        {
            get
            {
                return this.rowIndex;
            }
        }

        public IIndex<TColumnKey> ColumnIndex
        {
            get
            {
                return this.columnIndex;
            }
        }

        public int RowCount
        {
            get
            {
                return (int)this.RowIndex.KeyCount;
            }
        }

        public int ColumnCount
        {
            get
            {
                return (int)this.ColumnIndex.KeyCount;
            }
        }

        public Frame<TRowKey, TColumnKey> Zip<V1, V2, V3>(Frame<TRowKey, TColumnKey> otherFrame, JoinKind columnKind, JoinKind rowKind, Lookup lookup, bool pointwise, Func<V1, V2, V3> op)
        {
            Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(this.indexBuilder, otherFrame.IndexBuilder, rowKind, lookup, this.rowIndex\u004099, otherFrame.RowIndex, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            IIndex<TRowKey> rowIndex = joinTransformation.Item1;
            VectorConstruction f2cmd = joinTransformation.Item3;
            VectorConstruction vectorConstruction = joinTransformation.Item2;
            FSharpFunc<IVector, IVector> f1trans = (FSharpFunc<IVector, IVector>)new Frame.f1trans(this.vectorBuilder, rowIndex.AddressingScheme, vectorConstruction);
            FSharpFunc<IVector, IVector> f2trans = (FSharpFunc<IVector, IVector>)new Frame.f2trans(this.vectorBuilder, rowIndex.AddressingScheme, VectorHelpers.substitute(1, VectorConstruction.NewReturn(0)).Invoke(f2cmd));
            Series<TColumnKey, IVector> series1 = new Series<TColumnKey, IVector>(this.ColumnIndex, this.Data, this.VectorBuilder, this.IndexBuilder);
            Series<TColumnKey, IVector> otherSeries = new Series<TColumnKey, IVector>(otherFrame.ColumnIndex, otherFrame.Data, otherFrame.VectorBuilder, otherFrame.IndexBuilder);
            FSharpFunc<IVector, OptionalValue<IVector<V1>>> asV1 = (FSharpFunc<IVector, OptionalValue<IVector<V1>>>)new Frame.asV1 < V1 > (ConversionKind.Flexible);
            FSharpFunc<IVector, OptionalValue<IVector<V2>>> asV2 = (FSharpFunc<IVector, OptionalValue<IVector<V2>>>)new Frame.asV2 < V2 > (ConversionKind.Flexible);
            FSharpTypeFunc fsharpTypeFunc = (FSharpTypeFunc)new Frame.TryConvert_();
            Series<TColumnKey, IVector> series2 = series1.Zip<IVector>(otherSeries, columnKind).Select<IVector>(new Func<KeyValuePair<TColumnKey, Tuple<OptionalValue<IVector>, OptionalValue<IVector>>>, IVector>(new Frame.newColumns\u0040229 < TColumnKey, TRowKey, V1, V2, V3 > (this, pointwise, op, rowIndex, f2cmd, vectorConstruction, f1trans, f2trans, asV1, asV2, fsharpTypeFunc).Invoke));
            return new Frame<TRowKey, TColumnKey>(rowIndex, series2.Index, series2.Vector, this.indexBuilder, this.vectorBuilder);
        }

        public Frame<TRowKey, TColumnKey> Zip<V1, V2, V3>(Frame<TRowKey, TColumnKey> otherFrame, Func<V1, V2, V3> op)
        {
            return this.Zip<V1, V2, V3>(otherFrame, JoinKind.Outer, JoinKind.Outer, Lookup.Exact, false, op);
        }

        public Frame<TRowKey, TColumnKey> Join(Frame<TRowKey, TColumnKey> otherFrame, JoinKind kind, Lookup lookup)
        {
            Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(this.indexBuilder, otherFrame.IndexBuilder, kind, lookup, this.rowIndex\u004099, otherFrame.RowIndex, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(0));
            VectorConstruction rowCmd1 = joinTransformation.Item2;
            VectorConstruction rowCmd2 = joinTransformation.Item3;
            IIndex<TRowKey> rowIndex = joinTransformation.Item1;
            Tuple<IIndex<TColumnKey>, VectorConstruction> tuple = this.indexBuilder.Merge<TColumnKey>(FSharpList<Tuple<IIndex<TColumnKey>, VectorConstruction>>.Cons(new Tuple<IIndex<TColumnKey>, VectorConstruction>(this.columnIndex\u0040100, VectorConstruction.NewReturn(0)), FSharpList<Tuple<IIndex<TColumnKey>, VectorConstruction>>.Cons(new Tuple<IIndex<TColumnKey>, VectorConstruction>(otherFrame.ColumnIndex, VectorConstruction.NewReturn(1)), FSharpList<Tuple<IIndex<TColumnKey>, VectorConstruction>>.get_Empty())), VectorHelpers.BinaryTransform.AtMostOne);
            IIndex<TColumnKey> columnIndex = tuple.Item1;
            VectorConstruction vectorConstruction = tuple.Item2;
            IVector<IVector> vector1 = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(this.data, (FSharpFunc<IVector, IVector>)new Frame.newThisData\u0040299(this.vectorBuilder, rowIndex.AddressingScheme, rowCmd1));
            IVector<IVector> vector2 = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(otherFrame.Data, (FSharpFunc<IVector, IVector>)new Frame.newOtherData\u0040300(otherFrame.VectorBuilder, rowIndex.AddressingScheme, rowCmd2));
            IVector<IVector> data = this.vectorBuilder.Build<IVector>(columnIndex.AddressingScheme, vectorConstruction, new IVector<IVector>[2]
            {
        vector1,
        vector2
            });
            return new Frame<TRowKey, TColumnKey>(rowIndex, columnIndex, data, this.indexBuilder, this.vectorBuilder);
        }

        public Frame<TRowKey, TColumnKey> Join(Frame<TRowKey, TColumnKey> otherFrame, JoinKind kind)
        {
            return this.Join(otherFrame, kind, Lookup.Exact);
        }

        public Frame<TRowKey, TColumnKey> Join(Frame<TRowKey, TColumnKey> otherFrame)
        {
            return this.Join(otherFrame, JoinKind.Outer, Lookup.Exact);
        }

        public Frame<TRowKey, TColumnKey> Join<V>(TColumnKey colKey, Series<TRowKey, V> series, JoinKind kind, Lookup lookup)
        {
            return this.Join(new Frame<TRowKey, TColumnKey>((IEnumerable<TColumnKey>)FSharpList<TColumnKey>.Cons(colKey, FSharpList<TColumnKey>.get_Empty()), (IEnumerable<ISeries<TRowKey>>)FSharpList<ISeries<TRowKey>>.Cons((ISeries<TRowKey>)series, FSharpList<ISeries<TRowKey>>.get_Empty())), kind, lookup);
        }

        public Frame<TRowKey, TColumnKey> Join<V>(TColumnKey colKey, Series<TRowKey, V> series, JoinKind kind)
        {
            return this.Join<V>(colKey, series, kind, Lookup.Exact);
        }

        public Frame<TRowKey, TColumnKey> Join<V>(TColumnKey colKey, Series<TRowKey, V> series)
        {
            return this.Join<V>(colKey, series, JoinKind.Outer, Lookup.Exact);
        }

        public Frame<TRowKey, TColumnKey> Merge(Frame<TRowKey, TColumnKey> otherFrame)
        {
            return this.Merge(new Frame<TRowKey, TColumnKey>[1]
            {
        otherFrame
            });
        }

        public Frame<TRowKey, TColumnKey> Merge(IEnumerable<Frame<TRowKey, TColumnKey>> otherFrames)
        {
            return this.Merge((Frame<TRowKey, TColumnKey>[])ArrayModule.OfSeq<Frame<TRowKey, TColumnKey>>((IEnumerable<M0>)otherFrames));
        }

        public Frame<TRowKey, TColumnKey> Merge(params Frame<TRowKey, TColumnKey>[] otherFrames)
        {
            Frame<TRowKey, TColumnKey>[] frameArray1 = (Frame<TRowKey, TColumnKey>[])ArrayModule.Append<Frame<TRowKey, TColumnKey>>((M0[])new Frame<TRowKey, TColumnKey>[1]
            {
        this
            }, (M0[])otherFrames);
            Tuple<IIndex<TRowKey>, VectorConstruction> tuple1 = this.indexBuilder.Merge<TRowKey>((FSharpList<Tuple<IIndex<TRowKey>, VectorConstruction>>)ListModule.OfSeq<Tuple<IIndex<TRowKey>, VectorConstruction>>((IEnumerable<M0>)SeqModule.MapIndexed<Frame<TRowKey, TColumnKey>, Tuple<IIndex<TRowKey>, VectorConstruction>>((FSharpFunc<int, FSharpFunc<M0, M1>>)new Frame.constrs\u0040435\u002D1 < TRowKey, TColumnKey > (), (IEnumerable<M0>)frameArray1)), VectorHelpers.NaryTransform.AtMostOne);
            VectorConstruction rowCmd = tuple1.Item2;
            IIndex<TRowKey> index1 = tuple1.Item1;
            VectorListTransform vectorListTransform = VectorHelpers.NaryTransform.Create<IVector>((FSharpFunc<FSharpList<OptionalValue<IVector>>, OptionalValue<IVector>>)new Frame.append\u0040452((FSharpFunc<IVector, FSharpFunc<FSharpList<IVector>, IVector>>)new Frame.convertAndAppendVectors\u0040445 < TRowKey, TColumnKey > (this, rowCmd, index1)));
            Tuple<IIndex<TColumnKey>, VectorConstruction> tuple2 = this.indexBuilder.Merge<TColumnKey>((FSharpList<Tuple<IIndex<TColumnKey>, VectorConstruction>>)ListModule.OfSeq<Tuple<IIndex<TColumnKey>, VectorConstruction>>((IEnumerable<M0>)SeqModule.MapIndexed<Frame<TRowKey, TColumnKey>, Tuple<IIndex<TColumnKey>, VectorConstruction>>((FSharpFunc<int, FSharpFunc<M0, M1>>)new Frame.colConstrs\u0040462 < TRowKey, TColumnKey > (), (IEnumerable<M0>)frameArray1)), vectorListTransform);
            IIndex<TColumnKey> columnIndex = tuple2.Item1;
            VectorConstruction vectorConstruction = tuple2.Item2;
            Frame<TRowKey, TColumnKey>[] frameArray2 = frameArray1;
            FSharpFunc<Frame<TRowKey, TColumnKey>, IVector<IVector>> fsharpFunc = (FSharpFunc<Frame<TRowKey, TColumnKey>, IVector<IVector>>)new Frame.frameData\u0040465 < TRowKey, TColumnKey > ();
            Frame<TRowKey, TColumnKey>[] frameArray3 = frameArray2;
            if ((object)frameArray3 == null)
                throw new ArgumentNullException("array");
            IVector<IVector>[] vectorArray1 = new IVector<IVector>[frameArray3.Length];
            for (int index2 = 0; index2 < vectorArray1.Length; ++index2)
                vectorArray1[index2] = fsharpFunc.Invoke(frameArray3[index2]);
            IVector<IVector>[] vectorArray2 = vectorArray1;
            IVector<IVector> data = this.vectorBuilder.Build<IVector>(columnIndex.AddressingScheme, vectorConstruction, vectorArray2);
            return new Frame<TRowKey, TColumnKey>(index1, columnIndex, data, this.indexBuilder, this.vectorBuilder);
        }

        public bool IsEmpty
        {
            get
            {
                return SeqModule.IsEmpty<KeyValuePair<TRowKey, long>>((IEnumerable<M0>)this.rowIndex\u004099.Mappings);
            }
        }

        public IEnumerable<TRowKey> RowKeys
        {
            get
            {
                return (IEnumerable<TRowKey>)this.rowIndex\u004099.Keys;
            }
        }

        public IEnumerable<TColumnKey> ColumnKeys
        {
            get
            {
                return (IEnumerable<TColumnKey>)this.columnIndex\u0040100.Keys;
            }
        }

        public IEnumerable<Type> ColumnTypes
        {
            get
            {
                return (IEnumerable<Type>)new Frame.get_ColumnTypes\u0040484 < TRowKey, TColumnKey > (this, new KeyValuePair<TColumnKey, long>(), (IEnumerator<KeyValuePair<TColumnKey, long>>)null, 0, (Type)null);
            }
        }

        public ColumnSeries<TRowKey, TColumnKey> Columns
        {
            get
            {
                return new ColumnSeries<TRowKey, TColumnKey>(new Series<TColumnKey, ObjectSeries<TRowKey>>(this.columnIndex\u0040100, F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, ObjectSeries<TRowKey>>(this.data, (FSharpFunc<IVector, ObjectSeries<TRowKey>>)new Frame.newData\u0040488\u002D1 < TRowKey, TColumnKey > (this)), this.vectorBuilder, this.indexBuilder));
            }
        }

        public ColumnSeries<TRowKey, TColumnKey> ColumnsDense
        {
            get
            {
                return new ColumnSeries<TRowKey, TColumnKey>(new Series<TColumnKey, ObjectSeries<TRowKey>>(this.columnIndex\u0040100, this.data.Select<ObjectSeries<TRowKey>>((FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<IVector>, OptionalValue<ObjectSeries<TRowKey>>>>)new Frame.newData\u0040493\u002D2 < TRowKey, TColumnKey > (this)), this.vectorBuilder, this.indexBuilder));
            }
        }

        public RowSeries<TRowKey, TColumnKey> RowsDense
        {
            get
            {
                return RowSeries<TRowKey, TColumnKey>.FromSeries(SeriesModule.DropMissing<TRowKey, ObjectSeries<TColumnKey>>(this.Rows.SelectOptional<ObjectSeries<TColumnKey>>(new Func<KeyValuePair<TRowKey, OptionalValue<ObjectSeries<TColumnKey>>>, OptionalValue<ObjectSeries<TColumnKey>>>(new Frame.res\u0040502\u002D4 < TRowKey, TColumnKey > (this).Invoke))));
            }
        }

        public RowSeries<TRowKey, TColumnKey> Rows
        {
            get
            {
                return (RowSeries<TRowKey, TColumnKey>)new Frame.get_Rows\u0040529 < TRowKey, TColumnKey > (this, VectorHelperExtensions.createRowVector<ObjectSeries<TColumnKey>>(this.vectorBuilder, this.rowIndex\u004099.AddressingScheme, (Lazy<long>)LazyExtensions.Create<long>((FSharpFunc<Unit, M0>)new Frame.vector\u0040519 < TRowKey, TColumnKey > (this)), this.columnIndex\u0040100.KeyCount, (FSharpFunc<long, long>)new Frame.vector\u0040520\u002D1 < TColumnKey > (this.columnIndex\u0040100), (FSharpFunc<IVector<object>, ObjectSeries<TColumnKey>>)new Frame.vector\u0040520\u002D2 < TColumnKey, TRowKey > (this), this.data));
            }
        }

        public Series<TRowKey, TRow> GetRowsAs<TRow>()
        {
            if (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)typeof(TColumnKey), (M0)typeof(string)))
                throw Operators.Failure("The GetRows operation can only be used when column key is a string.");
            return new Series<TRowKey, TRow>(this.rowIndex\u004099, (IVector<TRow>)FSharpFunc<long, FSharpFunc<long, long>>.InvokeFast<IVector<IVector>, IVector<TRow>>((FSharpFunc<long, FSharpFunc<FSharpFunc<long, long>, FSharpFunc<M0, M1>>>)new Frame.rowBuilder\u0040540\u002D1 < TRow > ((FSharpList<string>)ListModule.OfSeq<string>((IEnumerable<M0>)SeqModule.Map<TColumnKey, string>((FSharpFunc<M0, M1>)new Frame.keys\u0040539\u002D6 < TColumnKey > (), (IEnumerable<M0>)this.columnIndex\u0040100.Keys)), (FSharpFunc<string, long>)new Frame.rowBuilder\u0040540 < TRowKey, TColumnKey > (this)), this.rowIndex\u004099.KeyCount, (FSharpFunc<long, long>)new Frame.vector\u0040546\u002D3 < TRowKey > (this.rowIndex\u004099), (M0)this.data), this.vectorBuilder, this.indexBuilder);
        }

        public object this[TColumnKey column, TRowKey row]
        {
            get
            {
                return this.Columns[column][row];
            }
        }

        public TRowKey GetRowKeyAt(long index)
        {
            return this.RowIndex.KeyAt(this.RowIndex.AddressAt(index));
        }

        public Series<TColumnKey, T> GetRowAt<T>(int index)
        {
            if ((index >= 0 ? ((long)index >= this.rowIndex\u004099.KeyCount ? 1 : 0) : 1) != 0)
        throw new ArgumentOutOfRangeException(nameof(index), "Index must be positive and smaller than the number of rows.");
            return new Series<TColumnKey, T>(this.columnIndex\u0040100, (IVector<T>)new VectorHelpers.RowReaderVector<T>(this.data, this.vectorBuilder, this.rowIndex\u004099.AddressAt((long)index), (FSharpFunc<long, long>)new Frame.vector\u0040583\u002D4 < TColumnKey > (this.columnIndex\u0040100)), this.vectorBuilder, this.indexBuilder);
        }

        public OptionalValue<Series<TColumnKey, T>> TryGetRow<T>(TRowKey rowKey)
        {
            long rowAddress = this.rowIndex\u004099.Locate(rowKey);
            if (rowAddress == Addressing.AddressModule.invalid)
                return OptionalValue<Series<TColumnKey, T>>.Missing;
            return new OptionalValue<Series<TColumnKey, T>>(new Series<TColumnKey, T>(this.columnIndex\u0040100, (IVector<T>)new VectorHelpers.RowReaderVector<T>(this.data, this.vectorBuilder, rowAddress, (FSharpFunc<long, long>)new Frame.vector\u0040599\u002D5 < TColumnKey > (this.columnIndex\u0040100)), this.vectorBuilder, this.indexBuilder));
        }

        public OptionalValue<Series<TColumnKey, T>> TryGetRow<T>(TRowKey rowKey, Lookup lookup)
        {
            OptionalValue<Tuple<TRowKey, long>> optionalValue = this.rowIndex\u004099.Lookup(rowKey, lookup, (FSharpFunc<long, bool>)new Frame.rowAddress\u0040614());
            if (!optionalValue.HasValue)
                return OptionalValue<Series<TColumnKey, T>>.Missing;
            return new OptionalValue<Series<TColumnKey, T>>(new Series<TColumnKey, T>(this.columnIndex\u0040100, (IVector<T>)new VectorHelpers.RowReaderVector<T>(this.data, this.vectorBuilder, optionalValue.Value.Item2, (FSharpFunc<long, long>)new Frame.vector\u0040617\u002D6 < TColumnKey > (this.columnIndex\u0040100)), this.vectorBuilder, this.indexBuilder));
        }

        public Series<TColumnKey, T> GetRow<T>(TRowKey rowKey)
        {
            return this.TryGetRow<T>(rowKey).Value;
        }

        public Series<TColumnKey, T> GetRow<T>(TRowKey rowKey, Lookup lookup)
        {
            return this.TryGetRow<T>(rowKey, lookup).Value;
        }

        public OptionalValue<KeyValuePair<TRowKey, Series<TColumnKey, T>>> TryGetRowObservation<T>(TRowKey rowKey, Lookup lookup)
        {
            OptionalValue<Tuple<TRowKey, long>> optionalValue = this.rowIndex\u004099.Lookup(rowKey, lookup, (FSharpFunc<long, bool>)new Frame.rowAddress\u0040653\u002D1());
            if (!optionalValue.HasValue)
                return OptionalValue<KeyValuePair<TRowKey, Series<TColumnKey, T>>>.Missing;
            Series<TColumnKey, T> series = new Series<TColumnKey, T>(this.columnIndex\u0040100, (IVector<T>)new VectorHelpers.RowReaderVector<T>(this.data, this.vectorBuilder, optionalValue.Value.Item2, (FSharpFunc<long, long>)new Frame.vector\u0040656\u002D7 < TColumnKey > (this.columnIndex\u0040100)), this.vectorBuilder, this.indexBuilder);
            return new OptionalValue<KeyValuePair<TRowKey, Series<TColumnKey, T>>>(new KeyValuePair<TRowKey, Series<TColumnKey, T>>(optionalValue.Value.Item1, series));
        }

        public Series<TColumnKey, Series<TRowKey, R>> GetColumns<R>()
        {
            return this.Columns.SelectOptional<Series<TRowKey, R>>(new Func<KeyValuePair<TColumnKey, OptionalValue<ObjectSeries<TRowKey>>>, OptionalValue<Series<TRowKey, R>>>(new Frame.GetColumns\u0040666 < TRowKey, TColumnKey, R > ().Invoke));
        }

        public Series<TRowKey, Series<TColumnKey, R>> GetRows<R>()
        {
            return this.Rows.SelectOptional<Series<TColumnKey, R>>(new Func<KeyValuePair<TRowKey, OptionalValue<ObjectSeries<TColumnKey>>>, OptionalValue<Series<TColumnKey, R>>>(new Frame.GetRows\u0040671 < TRowKey, TColumnKey, R > ().Invoke));
        }

        public IEnumerable<R> GetAllValues<R>()
        {
            return this.GetAllValues<R>(ConversionKind.Safe);
        }

        public IEnumerable<R> GetAllValues<R>(ConversionKind strict)
        {
            return (IEnumerable<R>)new Frame.GetAllValues\u0040679\u002D2 < R, TRowKey, TColumnKey > (this, new KeyValuePair<TColumnKey, Series<TRowKey, R>>(), (Tuple<TColumnKey, Series<TRowKey, R>>)null, (Series<TRowKey, R>)null, (IEnumerator<KeyValuePair<TColumnKey, Series<TRowKey, R>>>)null, default(R), (IEnumerator<R>)null, 0, default(R));
        }

        public R[,] ToArray2D<R>(R defaultValue)
        {
            return VectorHelpers.toArray2D<R>(this.RowCount, this.ColumnCount, this.Data, LazyExtensions.Create<R>((FSharpFunc<Unit, M0>)new Frame.ToArray2D\u0040690 < R > (defaultValue)));
        }

        public R[,] ToArray2D<R>()
        {
            return VectorHelpers.toArray2D<R>(this.RowCount, this.ColumnCount, this.Data, !LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)typeof(R), (M0)typeof(double)) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)typeof(R), (M0)typeof(float)) ? LazyExtensions.Create<R>((FSharpFunc<Unit, M0>)new Frame.defaultValue\u0040702\u002D2 < R > ()) : LazyExtensions.Create<R>((FSharpFunc<Unit, M0>)new Frame.defaultValue\u0040701\u002D1 < R > ())) : LazyExtensions.Create<R>((FSharpFunc<Unit, M0>)new Frame.defaultValue\u0040700 < R > ()));
        }

        public void AddColumn<V>(TColumnKey column, IEnumerable<V> series)
        {
            this.AddColumn<V>(column, series, Lookup.Exact);
        }

        public void AddColumn(TColumnKey column, ISeries<TRowKey> series)
        {
            this.AddColumn<object>(column, series, Lookup.Exact);
        }

        public void AddColumn<V>(TColumnKey column, IEnumerable<V> series, Lookup lookup)
        {
            if (this.isEmpty)
            {
                if (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)typeof(TRowKey), (M0)typeof(int)))
                    throw new InvalidOperationException("Adding data sequence to an empty frame with non-integer columns is not supported.");
                Series<TRowKey, V> series1 = (Series<TRowKey, V>)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<Series<TRowKey, V>>((object)F\u0023\u0020Series\u0020extensions.Series.ofValues<V>(series));
                this.AddColumn<object>(column, (ISeries<TRowKey>)series1, lookup);
            }
            else
            {
                int count = SeqModule.Length<V>(series);
                int rowCount = SeqModule.Length<TRowKey>((IEnumerable<M0>)this.RowIndex.Keys);
                IVector<V> missing;
                if (count >= rowCount)
                {
                    missing = F\u0023\u0020VectorBuilder\u0020implementation.VectorBuilder.Instance.Create<V>(ArrayModule.OfSeq<V>(SeqModule.Take<V>(count, series)));
                }
                else
                {
                    IEnumerable<FSharpOption<V>> fsharpOptions = (IEnumerable<FSharpOption<V>>)new Frame.nulls\u0040763 < V > (count, rowCount, 0, (IEnumerator<int>)null, 0, (FSharpOption<V>)null);
                    missing = F\u0023\u0020VectorBuilder\u0020implementation.VectorBuilder.Instance.CreateMissing<V>((OptionalValue<V>[])ArrayModule.OfSeq<OptionalValue<V>>((IEnumerable<M0>)SeqModule.Map<FSharpOption<V>, OptionalValue<V>>((FSharpFunc<M0, M1>)new Frame.vector\u0040764\u002D9 < V > (), SeqModule.Append<FSharpOption<V>>((IEnumerable<M0>)SeqModule.Map<V, FSharpOption<V>>((FSharpFunc<M0, M1>)new Frame.vector\u0040764\u002D8 < V > (), series), (IEnumerable<M0>)fsharpOptions))));
                }
                Series<TRowKey, V> series1 = new Series<TRowKey, V>(this.RowIndex, missing, this.vectorBuilder, this.indexBuilder);
                this.AddColumn<object>(column, (ISeries<TRowKey>)series1, lookup);
            }
        }

        public void AddColumn<V>(TColumnKey column, ISeries<TRowKey> series, Lookup lookup)
        {
            if (this.isEmpty)
            {
                this.rowIndex\u004099 = series.Index;
                this.isEmpty = false;
                this.data = F\u0023\u0020VectorBuilder\u0020implementation.VectorBuilder.Instance.Create<IVector>((IVector[])ArrayModule.OfSeq<IVector>((IEnumerable<M0>)FSharpList<IVector>.Cons(series.Vector, FSharpList<IVector>.get_Empty())));
                this.setColumnIndex(F\u0023\u0020Index\u0020extensions.Index.ofKeys<TColumnKey>(new TColumnKey[1]
        {
          column
        }));
      }
      else
      {
        Frame<TRowKey, TColumnKey> frame = this.Join(new Frame<TRowKey, TColumnKey>(series.Index, F\u0023\u0020Index\u0020extensions.Index.ofUnorderedKeys<TColumnKey>(new TColumnKey[1]
        {
          column
        }), F\u0023\u0020VectorBuilder\u0020implementation.VectorBuilder.Instance.Create<IVector>((IVector[]) ArrayModule.OfSeq<IVector>((IEnumerable<M0>) FSharpList<IVector>.Cons(series.Vector, FSharpList<IVector>.get_Empty()))), series.Index.Builder, series.VectorBuilder), JoinKind.Left, lookup);
        this.data = frame.Data;
        this.setColumnIndex(frame.ColumnIndex);
      }
    }

    public void DropColumn(TColumnKey column)
    {
      Tuple<IIndex<TColumnKey>, VectorConstruction> tuple = this.indexBuilder.DropItem<TColumnKey>(new Tuple<IIndex<TColumnKey>, VectorConstruction>(this.columnIndex\u0040100, VectorConstruction.NewReturn(0)), column);
      IIndex<TColumnKey> newColumnIndex = tuple.Item1;
      VectorConstruction vectorConstruction = tuple.Item2;
      this.data = this.vectorBuilder.Build<IVector>(newColumnIndex.AddressingScheme, vectorConstruction, new IVector<IVector>[1]
      {
        this.data
      });
      this.setColumnIndex(newColumnIndex);
    }

    public void ReplaceColumn(TColumnKey column, ISeries<TRowKey> series, Lookup lookup)
    {
      if (this.columnIndex\u0040100.Lookup(column, Lookup.Exact, (FSharpFunc<long, bool>) new Frame.ReplaceColumn\u0040824()).HasValue)
        this.DropColumn(column);
      this.AddColumn<object>(column, series, lookup);
    }

    public void ReplaceColumn<V>(TColumnKey column, IEnumerable<V> data, Lookup lookup)
    {
      Series<TRowKey, V> series = new Series<TRowKey, V>(this.RowIndex, F\u0023\u0020VectorBuilder\u0020implementation.VectorBuilder.Instance.Create<V>(ArrayModule.OfSeq<V>(data)), this.vectorBuilder, this.indexBuilder);
      this.ReplaceColumn(column, (ISeries<TRowKey>) series, lookup);
    }

    public void ReplaceColumn(TColumnKey column, ISeries<TRowKey> series)
    {
      this.ReplaceColumn(column, series, Lookup.Exact);
    }

    public void ReplaceColumn<V>(TColumnKey column, IEnumerable<V> data)
    {
      this.ReplaceColumn<V>(column, data, Lookup.Exact);
    }

    public Series<TRowKey, double> this[TColumnKey column]
    {
      get
      {
        return this.GetColumn<double>(column);
      }
      set
      {
        this.ReplaceColumn(column, (ISeries<TRowKey>) value);
      }
    }

    public Frame<TRowKey, TColumnKey> ColumnApply<T>(Func<Series<TRowKey, T>, ISeries<TRowKey>> f)
    {
      return this.ColumnApply<T>(ConversionKind.Safe, f);
    }

    public Frame<TRowKey, TColumnKey> ColumnApply<T>(ConversionKind conversionKind, Func<Series<TRowKey, T>, ISeries<TRowKey>> f)
    {
      ColumnSeries<TRowKey, TColumnKey> columns = this.Columns;
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(this.indexBuilder, this.vectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.ColumnApply\u0040883\u002D1<TRowKey>(), SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.ColumnApply\u0040879<TRowKey, T>(conversionKind, f), (Series<TColumnKey, ObjectSeries<TRowKey>>) columns));
    }

    public Frame<TRowKey, TColumnKey> Select<T1, T2>(Func<TRowKey, TColumnKey, T1, T2> f)
    {
      ColumnSeries<TRowKey, TColumnKey> columns = this.Columns;
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(this.indexBuilder, this.vectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.Select\u0040891\u002D5<TRowKey>(), SeriesModule.Map<TColumnKey, ObjectSeries<TRowKey>, ISeries<TRowKey>>((FSharpFunc<TColumnKey, FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>>) new Frame.Select\u0040887\u002D3<TRowKey, TColumnKey, T1, T2>(f), (Series<TColumnKey, ObjectSeries<TRowKey>>) columns));
    }

    public Frame<TRowKey, TColumnKey> SelectValues<T1, T2>(Func<T1, T2> f)
    {
      return this.ColumnApply<T1>(ConversionKind.Safe, new Func<Series<TRowKey, T1>, ISeries<TRowKey>>(new Frame.SelectValues\u0040895\u002D1<TRowKey, T1, T2>(f).Invoke));
    }

    [SpecialName]
    public static Frame<TRowKey, TColumnKey> op_Dollar<a, b>(FSharpFunc<a, b> f, Frame<TRowKey, TColumnKey> frame)
    {
      return frame.SelectValues<a, b>(new Func<a, b>(new Frame.op_Dollar\u0040901\u002D1<a, b>(f).Invoke));
    }

    public Series<TRowKey, R> GetColumn<R>(TColumnKey column, Lookup lookup)
    {
      IVector colVector = this.safeGetColVector(column, lookup, (FSharpFunc<long, bool>) new Frame.GetColumn\u0040905());
      VectorHelpers.IBoxedVector boxedVector = colVector as VectorHelpers.IBoxedVector;
      IVector vector1 = boxedVector == null ? colVector : boxedVector.UnboxedVector;
      IVector<R> vector2 = vector1 as IVector<R>;
      if (vector2 != null)
        return new Series<TRowKey, R>(this.rowIndex\u004099, vector2, this.vectorBuilder, this.indexBuilder);
      return new Series<TRowKey, R>(this.rowIndex\u004099, VectorHelpers.convertType<R>(ConversionKind.Flexible, vector1), this.vectorBuilder, this.indexBuilder);
    }

    public Series<TRowKey, R> GetColumnAt<R>(int index)
    {
      return this.Columns.GetAt(index).As<R>();
    }

    public Series<TRowKey, R> GetColumn<R>(TColumnKey column)
    {
      return this.GetColumn<R>(column, Lookup.Exact);
    }

    public IEnumerable<KeyValuePair<TColumnKey, Series<TRowKey, R>>> GetAllColumns<R>()
    {
      return this.GetAllColumns<R>(ConversionKind.Flexible);
    }

    public OptionalValue<Series<TRowKey, R>> TryGetColumn<R>(TColumnKey column, Lookup lookup)
    {
      OptionalValue<IVector> colVector = this.tryGetColVector(column, lookup, (FSharpFunc<long, bool>) new Frame.TryGetColumn\u0040924());
      FSharpFunc<IVector, Series<TRowKey, R>> fsharpFunc = (FSharpFunc<IVector, Series<TRowKey, R>>) new Frame.TryGetColumn\u0040925\u002D1<TRowKey, R, TColumnKey>(this);
      OptionalValue<IVector> optionalValue = colVector;
      if (optionalValue.HasValue)
        return new OptionalValue<Series<TRowKey, R>>(fsharpFunc.Invoke(optionalValue.Value));
      return OptionalValue<Series<TRowKey, R>>.Missing;
    }

    public OptionalValue<KeyValuePair<TColumnKey, Series<TRowKey, R>>> TryGetColumnObservation<R>(TColumnKey column, Lookup lookup)
    {
      OptionalValue<Tuple<TColumnKey, long>> columnIndex = this.columnIndex\u0040100.Lookup(column, lookup, (FSharpFunc<long, bool>) new Frame.columnIndex\u0040929());
      if (!columnIndex.HasValue)
        return OptionalValue<KeyValuePair<TColumnKey, Series<TRowKey, R>>>.Missing;
      OptionalValue<IVector> optionalValue1 = this.data.GetValue(columnIndex.Value.Item2);
      FSharpFunc<IVector, KeyValuePair<TColumnKey, Series<TRowKey, R>>> fsharpFunc = (FSharpFunc<IVector, KeyValuePair<TColumnKey, Series<TRowKey, R>>>) new Frame.TryGetColumnObservation\u0040934<TRowKey, TColumnKey, R>(this, columnIndex);
      OptionalValue<IVector> optionalValue2 = optionalValue1;
      if (optionalValue2.HasValue)
        return new OptionalValue<KeyValuePair<TColumnKey, Series<TRowKey, R>>>(fsharpFunc.Invoke(optionalValue2.Value));
      return OptionalValue<KeyValuePair<TColumnKey, Series<TRowKey, R>>>.Missing;
    }

    public IEnumerable<KeyValuePair<TColumnKey, Series<TRowKey, R>>> GetAllColumns<R>(ConversionKind conversionKind)
    {
      return (IEnumerable<KeyValuePair<TColumnKey, Series<TRowKey, R>>>) SeqModule.Choose<KeyValuePair<TColumnKey, ObjectSeries<TRowKey>>, KeyValuePair<TColumnKey, Series<TRowKey, R>>>((FSharpFunc<M0, FSharpOption<M1>>) new Frame.GetAllColumns\u0040940<TRowKey, TColumnKey, R>(conversionKind), (IEnumerable<M0>) this.Columns.Observations);
    }

    public void RenameColumns(IEnumerable<TColumnKey> columnKeys)
    {
      if (SeqModule.Length<TColumnKey>((IEnumerable<M0>) this.columnIndex\u0040100.Keys) != SeqModule.Length<TColumnKey>((IEnumerable<M0>) columnKeys))
        throw new ArgumentException("The number of new column keys does not match with the number of columns", nameof (columnKeys));
      this.setColumnIndex(F\u0023\u0020Index\u0020extensions.Index.ofKeys<TColumnKey>(System.Array.AsReadOnly<TColumnKey>((TColumnKey[]) ArrayModule.OfSeq<TColumnKey>((IEnumerable<M0>) columnKeys))));
    }

    public void RenameColumn(TColumnKey oldKey, TColumnKey newKey)
    {
      ReadOnlyCollection<TColumnKey> keys = this.columnIndex\u0040100.Keys;
      this.setColumnIndex(F\u0023\u0020Index\u0020extensions.Index.ofKeys<TColumnKey>(System.Array.AsReadOnly<TColumnKey>((TColumnKey[]) ArrayModule.OfSeq<TColumnKey>((IEnumerable<M0>) SeqModule.Map<TColumnKey, TColumnKey>((FSharpFunc<M0, M1>) new Frame.newKeys\u0040953\u002D5<TColumnKey>(oldKey, newKey), (IEnumerable<M0>) keys)))));
    }

    public void RenameColumns(Func<TColumnKey, TColumnKey> mapping)
    {
      FSharpFunc<TColumnKey, TColumnKey> fsharpFunc = (FSharpFunc<TColumnKey, TColumnKey>) new Frame.RenameColumns\u0040958<TColumnKey>(mapping);
      ReadOnlyCollection<TColumnKey> keys = this.columnIndex\u0040100.Keys;
      TColumnKey[] array = (TColumnKey[]) ArrayModule.ZeroCreate<TColumnKey>(keys.Count);
      Frame<TRowKey, TColumnKey> frame = this;
      int index = 0;
      int num = keys.Count - 1;
      if (num >= index)
      {
        do
        {
          array[index] = fsharpFunc.Invoke(keys[index]);
          ++index;
        }
        while (index != num + 1);
      }
      frame.setColumnIndex(F\u0023\u0020Index\u0020extensions.Index.ofKeys<TColumnKey>(System.Array.AsReadOnly<TColumnKey>(array)));
    }

    [SpecialName]
    public static void op_DynamicAssignment<T, a, V>(Frame<T, a> frame, a column, Series<T, V> series)
    {
      frame.ReplaceColumn(column, (ISeries<T>) series);
    }

    [SpecialName]
    public static void op_DynamicAssignment<a, b, V>(Frame<a, b> frame, b column, IEnumerable<V> data)
    {
      frame.ReplaceColumn<V>(column, data);
    }

    [SpecialName]
    public static Series<T, double> op_Dynamic<T, a>(Frame<T, a> frame, a column)
    {
      return frame.GetColumn<double>(column);
    }

    internal static Frame<TRowKey, TColumnKey> PointwiseFrameSeriesR<T>(Frame<TRowKey, TColumnKey> frame, Series<TRowKey, T> series, FSharpFunc<T, FSharpFunc<T, T>> op)
    {
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame.IndexBuilder, series.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame.RowIndex, series.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.opCmd\u0040983<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.opCmd\u0040983\u002D1<T>(op)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame.Data, (FSharpFunc<IVector, IVector>) new Frame.newData\u0040986\u002D3<TRowKey, TColumnKey, T>(frame, series, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame.ColumnIndex, data, frame.IndexBuilder, frame.VectorBuilder);
    }

    internal static Frame<TRowKey, TColumnKey> PointwiseFrameFrame<T>(Frame<TRowKey, TColumnKey> frame1, Frame<TRowKey, TColumnKey> frame2, FSharpFunc<T, FSharpFunc<T, T>> op)
    {
      return frame1.Zip<T, T, T>(frame2, JoinKind.Outer, JoinKind.Outer, Lookup.Exact, true, new Func<T, T, T>(new Frame.PointwiseFrameFrame\u0040999<T>(op).Invoke));
    }

    internal static Frame<TRowKey, TColumnKey> ScalarOperationR<T>(Frame<TRowKey, TColumnKey> frame, T scalar, FSharpFunc<T, FSharpFunc<T, T>> op)
    {
      ColumnSeries<TRowKey, TColumnKey> columns = frame.Columns;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.ScalarOperationR\u00401003\u002D1<TRowKey, T>(scalar, op), (Series<TColumnKey, ObjectSeries<TRowKey>>) columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame.IndexBuilder, frame.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.ScalarOperationR\u00401007\u002D3<TRowKey>(), nested);
    }

    internal static Frame<TRowKey, TColumnKey> PointwiseFrameSeriesL<T>(Frame<TRowKey, TColumnKey> frame, Series<TRowKey, T> series, FSharpFunc<T, FSharpFunc<T, T>> op)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, T> series1 = series;
      FSharpFunc<T, FSharpFunc<T, T>> operation = (FSharpFunc<T, FSharpFunc<T, T>>) new Frame.PointwiseFrameSeriesL1<T>(op);
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame1.IndexBuilder, series1.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame1.RowIndex, series1.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.PointwiseFrameSeriesL1\u002D1<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.PointwiseFrameSeriesL1\u002D2<T>(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame1.Data, (FSharpFunc<IVector, IVector>) new Frame.PointwiseFrameSeriesL1\u002D4<TRowKey, TColumnKey, T>(frame1, series1, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame1.ColumnIndex, data, frame1.IndexBuilder, frame1.VectorBuilder);
    }

    internal static Frame<TRowKey, TColumnKey> ScalarOperationL<T>(Frame<TRowKey, TColumnKey> frame, T scalar, FSharpFunc<T, FSharpFunc<T, T>> op)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.ScalarOperationL4\u002D2<TRowKey, T>(scalar, (FSharpFunc<T, FSharpFunc<T, T>>) new Frame.ScalarOperationL4\u002D1<T>(op)), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame1.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame1.IndexBuilder, frame1.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.ScalarOperationL4\u002D4<TRowKey>(), nested);
    }

    internal static Frame<TRowKey, TColumnKey> UnaryOperation<T>(Frame<TRowKey, TColumnKey> frame, FSharpFunc<T, T> op)
    {
      return frame.ColumnApply<T>(ConversionKind.Safe, new Func<Series<TRowKey, T>, ISeries<TRowKey>>(new Frame.UnaryOperation7\u002D1<TRowKey, T>(op).Invoke));
    }

    internal static Frame<TRowKey, TColumnKey> UnaryGenericOperation<T1, T2>(Frame<TRowKey, TColumnKey> frame, FSharpFunc<T1, T2> op)
    {
      return frame.ColumnApply<T1>(ConversionKind.Safe, new Func<Series<TRowKey, T1>, ISeries<TRowKey>>(new Frame.UnaryGenericOperation\u00401020\u002D1<TRowKey, T1, T2>(op).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> operator -(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.op_UnaryNegation\u00401025\u002D7<TRowKey>((FSharpFunc<double, double>) new Frame.op_UnaryNegation\u00401025\u002D6()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> operator +(Frame<TRowKey, TColumnKey> frame1, Frame<TRowKey, TColumnKey> frame2)
    {
      return frame1.Zip<double, double, double>(frame2, JoinKind.Outer, JoinKind.Outer, Lookup.Exact, true, new Func<double, double, double>(new Frame.op_Addition\u00401031\u002D25((FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401031\u002D24()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> operator -(Frame<TRowKey, TColumnKey> frame1, Frame<TRowKey, TColumnKey> frame2)
    {
      return frame1.Zip<double, double, double>(frame2, JoinKind.Outer, JoinKind.Outer, Lookup.Exact, true, new Func<double, double, double>(new Frame.op_Subtraction\u00401034\u002D25((FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401034\u002D24()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> operator *(Frame<TRowKey, TColumnKey> frame1, Frame<TRowKey, TColumnKey> frame2)
    {
      return frame1.Zip<double, double, double>(frame2, JoinKind.Outer, JoinKind.Outer, Lookup.Exact, true, new Func<double, double, double>(new Frame.op_Multiply\u00401037\u002D25((FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401037\u002D24()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> operator /(Frame<TRowKey, TColumnKey> frame1, Frame<TRowKey, TColumnKey> frame2)
    {
      return frame1.Zip<double, double, double>(frame2, JoinKind.Outer, JoinKind.Outer, Lookup.Exact, true, new Func<double, double, double>(new Frame.op_Division\u00401040\u002D25((FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401040\u002D24()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> operator +(Frame<TRowKey, TColumnKey> frame, Series<TRowKey, double> series)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = series;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401046\u002D26();
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame1.IndexBuilder, series1.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame1.RowIndex, series1.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Addition\u00401046\u002D27<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Addition\u00401046\u002D28(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame1.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Addition\u00401046\u002D30<TRowKey, TColumnKey>(frame1, series1, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame1.ColumnIndex, data, frame1.IndexBuilder, frame1.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator -(Frame<TRowKey, TColumnKey> frame, Series<TRowKey, double> series)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = series;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401049\u002D26();
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame1.IndexBuilder, series1.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame1.RowIndex, series1.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Subtraction\u00401049\u002D27<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Subtraction\u00401049\u002D28(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame1.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Subtraction\u00401049\u002D30<TRowKey, TColumnKey>(frame1, series1, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame1.ColumnIndex, data, frame1.IndexBuilder, frame1.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator *(Frame<TRowKey, TColumnKey> frame, Series<TRowKey, double> series)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = series;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401052\u002D26();
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame1.IndexBuilder, series1.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame1.RowIndex, series1.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Multiply\u00401052\u002D27<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Multiply\u00401052\u002D28(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame1.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Multiply\u00401052\u002D30<TRowKey, TColumnKey>(frame1, series1, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame1.ColumnIndex, data, frame1.IndexBuilder, frame1.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator /(Frame<TRowKey, TColumnKey> frame, Series<TRowKey, double> series)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = series;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401055\u002D26();
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame1.IndexBuilder, series1.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame1.RowIndex, series1.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Division\u00401055\u002D27<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Division\u00401055\u002D28(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame1.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Division\u00401055\u002D30<TRowKey, TColumnKey>(frame1, series1, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame1.ColumnIndex, data, frame1.IndexBuilder, frame1.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator +(Series<TRowKey, double> series, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = series;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401058\u002D32();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TRowKey, double> series2 = series1;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401058\u002D33(op);
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame2.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame2.RowIndex, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Addition\u00401058\u002D34<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Addition\u00401058\u002D35(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame2.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Addition\u00401058\u002D37<TRowKey, TColumnKey>(frame2, series2, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame2.ColumnIndex, data, frame2.IndexBuilder, frame2.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator -(Series<TRowKey, double> series, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = series;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401061\u002D32();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TRowKey, double> series2 = series1;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401061\u002D33(op);
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame2.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame2.RowIndex, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Subtraction\u00401061\u002D34<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Subtraction\u00401061\u002D35(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame2.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Subtraction\u00401061\u002D37<TRowKey, TColumnKey>(frame2, series2, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame2.ColumnIndex, data, frame2.IndexBuilder, frame2.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator *(Series<TRowKey, double> series, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = series;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401064\u002D32();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TRowKey, double> series2 = series1;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401064\u002D33(op);
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame2.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame2.RowIndex, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Multiply\u00401064\u002D34<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Multiply\u00401064\u002D35(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame2.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Multiply\u00401064\u002D37<TRowKey, TColumnKey>(frame2, series2, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame2.ColumnIndex, data, frame2.IndexBuilder, frame2.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator /(Series<TRowKey, double> series, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = series;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401067\u002D32();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TRowKey, double> series2 = series1;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401067\u002D33(op);
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame2.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame2.RowIndex, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Division\u00401067\u002D34<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Division\u00401067\u002D35(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame2.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Division\u00401067\u002D37<TRowKey, TColumnKey>(frame2, series2, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame2.ColumnIndex, data, frame2.IndexBuilder, frame2.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator +(Frame<TRowKey, TColumnKey> frame, Series<TRowKey, int> series)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = SeriesModule.MapValues<int, double, TRowKey>((FSharpFunc<int, double>) new Frame.op_Addition\u00401071\u002D39(), series);
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401071\u002D40();
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame1.IndexBuilder, series1.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame1.RowIndex, series1.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Addition\u00401071\u002D41<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Addition\u00401071\u002D42(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame1.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Addition\u00401071\u002D44<TRowKey, TColumnKey>(frame1, series1, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame1.ColumnIndex, data, frame1.IndexBuilder, frame1.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator -(Frame<TRowKey, TColumnKey> frame, Series<TRowKey, int> series)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = SeriesModule.MapValues<int, double, TRowKey>((FSharpFunc<int, double>) new Frame.op_Subtraction\u00401074\u002D39(), series);
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401074\u002D40();
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame1.IndexBuilder, series1.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame1.RowIndex, series1.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Subtraction\u00401074\u002D41<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Subtraction\u00401074\u002D42(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame1.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Subtraction\u00401074\u002D44<TRowKey, TColumnKey>(frame1, series1, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame1.ColumnIndex, data, frame1.IndexBuilder, frame1.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator *(Frame<TRowKey, TColumnKey> frame, Series<TRowKey, int> series)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = SeriesModule.MapValues<int, double, TRowKey>((FSharpFunc<int, double>) new Frame.op_Multiply\u00401077\u002D39(), series);
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401077\u002D40();
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame1.IndexBuilder, series1.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame1.RowIndex, series1.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Multiply\u00401077\u002D41<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Multiply\u00401077\u002D42(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame1.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Multiply\u00401077\u002D44<TRowKey, TColumnKey>(frame1, series1, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame1.ColumnIndex, data, frame1.IndexBuilder, frame1.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator /(Frame<TRowKey, TColumnKey> frame, Series<TRowKey, int> series)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = SeriesModule.MapValues<int, double, TRowKey>((FSharpFunc<int, double>) new Frame.op_Division\u00401080\u002D39(), series);
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401080\u002D40();
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame1.IndexBuilder, series1.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame1.RowIndex, series1.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Division\u00401080\u002D41<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Division\u00401080\u002D42(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame1.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Division\u00401080\u002D44<TRowKey, TColumnKey>(frame1, series1, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame1.ColumnIndex, data, frame1.IndexBuilder, frame1.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator +(Series<TRowKey, int> series, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = SeriesModule.MapValues<int, double, TRowKey>((FSharpFunc<int, double>) new Frame.op_Addition\u00401083\u002D46(), series);
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401083\u002D47();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TRowKey, double> series2 = series1;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401083\u002D48(op);
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame2.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame2.RowIndex, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Addition\u00401083\u002D49<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Addition\u00401083\u002D50(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame2.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Addition\u00401083\u002D52<TRowKey, TColumnKey>(frame2, series2, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame2.ColumnIndex, data, frame2.IndexBuilder, frame2.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator -(Series<TRowKey, int> series, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = SeriesModule.MapValues<int, double, TRowKey>((FSharpFunc<int, double>) new Frame.op_Subtraction\u00401086\u002D46(), series);
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401086\u002D47();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TRowKey, double> series2 = series1;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401086\u002D48(op);
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame2.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame2.RowIndex, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Subtraction\u00401086\u002D49<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Subtraction\u00401086\u002D50(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame2.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Subtraction\u00401086\u002D52<TRowKey, TColumnKey>(frame2, series2, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame2.ColumnIndex, data, frame2.IndexBuilder, frame2.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator *(Series<TRowKey, int> series, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = SeriesModule.MapValues<int, double, TRowKey>((FSharpFunc<int, double>) new Frame.op_Multiply\u00401089\u002D46(), series);
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401089\u002D47();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TRowKey, double> series2 = series1;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401089\u002D48(op);
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame2.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame2.RowIndex, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Multiply\u00401089\u002D49<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Multiply\u00401089\u002D50(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame2.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Multiply\u00401089\u002D52<TRowKey, TColumnKey>(frame2, series2, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame2.ColumnIndex, data, frame2.IndexBuilder, frame2.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator /(Series<TRowKey, int> series, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TRowKey, double> series1 = SeriesModule.MapValues<int, double, TRowKey>((FSharpFunc<int, double>) new Frame.op_Division\u00401092\u002D46(), series);
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401092\u002D47();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TRowKey, double> series2 = series1;
      FSharpFunc<double, FSharpFunc<double, double>> operation = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401092\u002D48(op);
      Tuple<IIndex<TRowKey>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<TRowKey>(frame2.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, frame2.RowIndex, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
      VectorConstruction vectorConstruction = joinTransformation.Item3;
      IIndex<TRowKey> index = joinTransformation.Item1;
      VectorConstruction frameCmd = joinTransformation.Item2;
      VectorConstruction opCmd = VectorConstruction.NewCombine((Lazy<long>) LazyExtensions.Create<long>((FSharpFunc<Unit, M0>) new Frame.op_Division\u00401092\u002D49<TRowKey>(index)), FSharpList<VectorConstruction>.Cons(frameCmd, FSharpList<VectorConstruction>.Cons(vectorConstruction, FSharpList<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform) new Frame.op_Division\u00401092\u002D50(operation)));
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(frame2.Data, (FSharpFunc<IVector, IVector>) new Frame.op_Division\u00401092\u002D52<TRowKey, TColumnKey>(frame2, series2, index, frameCmd, opCmd));
      return new Frame<TRowKey, TColumnKey>(index, frame2.ColumnIndex, data, frame2.IndexBuilder, frame2.VectorBuilder);
    }

    public static Frame<TRowKey, TColumnKey> operator +(Frame<TRowKey, TColumnKey> frame, double scalar)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Addition\u00401099\u002D55<TRowKey>(scalar, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401099\u002D54()), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame1.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame1.IndexBuilder, frame1.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Addition\u00401099\u002D57<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator +(double scalar, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      double scalar1 = scalar;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401102\u002D58();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Addition\u00401102\u002D60<TRowKey>(scalar1, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401102\u002D59(op)), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame2.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame2.IndexBuilder, frame2.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Addition\u00401102\u002D62<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator -(Frame<TRowKey, TColumnKey> frame, double scalar)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Subtraction\u00401105\u002D55<TRowKey>(scalar, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401105\u002D54()), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame1.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame1.IndexBuilder, frame1.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Subtraction\u00401105\u002D57<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator -(double scalar, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      double scalar1 = scalar;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401108\u002D58();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Subtraction\u00401108\u002D60<TRowKey>(scalar1, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401108\u002D59(op)), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame2.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame2.IndexBuilder, frame2.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Subtraction\u00401108\u002D62<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator *(Frame<TRowKey, TColumnKey> frame, double scalar)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Multiply\u00401111\u002D55<TRowKey>(scalar, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401111\u002D54()), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame1.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame1.IndexBuilder, frame1.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Multiply\u00401111\u002D57<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator *(double scalar, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      double scalar1 = scalar;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401114\u002D58();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Multiply\u00401114\u002D60<TRowKey>(scalar1, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401114\u002D59(op)), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame2.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame2.IndexBuilder, frame2.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Multiply\u00401114\u002D62<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator /(Frame<TRowKey, TColumnKey> frame, double scalar)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Division\u00401117\u002D55<TRowKey>(scalar, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401117\u002D54()), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame1.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame1.IndexBuilder, frame1.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Division\u00401117\u002D57<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator /(double scalar, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      double scalar1 = scalar;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401120\u002D58();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Division\u00401120\u002D60<TRowKey>(scalar1, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401120\u002D59(op)), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame2.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame2.IndexBuilder, frame2.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Division\u00401120\u002D62<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> Pow(Frame<TRowKey, TColumnKey> frame, double scalar)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.Pow\u00401123\u002D9<TRowKey>(scalar, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.Pow\u00401123\u002D8()), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame1.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame1.IndexBuilder, frame1.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.Pow\u00401123\u002D11<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> Pow(double scalar, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      double scalar1 = scalar;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.Pow\u00401126\u002D12();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.Pow\u00401126\u002D14<TRowKey>(scalar1, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.Pow\u00401126\u002D13(op)), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame2.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame2.IndexBuilder, frame2.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.Pow\u00401126\u002D16<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator +(Frame<TRowKey, TColumnKey> frame, int scalar)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Addition\u00401130\u002D64<TRowKey>((double) scalar, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401130\u002D63()), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame1.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame1.IndexBuilder, frame1.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Addition\u00401130\u002D66<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator +(int scalar, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      double scalar1 = (double) scalar;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401133\u002D67();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Addition\u00401133\u002D69<TRowKey>(scalar1, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Addition\u00401133\u002D68(op)), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame2.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame2.IndexBuilder, frame2.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Addition\u00401133\u002D71<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator -(Frame<TRowKey, TColumnKey> frame, int scalar)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Subtraction\u00401136\u002D64<TRowKey>((double) scalar, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401136\u002D63()), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame1.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame1.IndexBuilder, frame1.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Subtraction\u00401136\u002D66<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator -(int scalar, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      double scalar1 = (double) scalar;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401139\u002D67();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Subtraction\u00401139\u002D69<TRowKey>(scalar1, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Subtraction\u00401139\u002D68(op)), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame2.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame2.IndexBuilder, frame2.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Subtraction\u00401139\u002D71<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator *(Frame<TRowKey, TColumnKey> frame, int scalar)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Multiply\u00401142\u002D64<TRowKey>((double) scalar, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401142\u002D63()), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame1.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame1.IndexBuilder, frame1.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Multiply\u00401142\u002D66<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator *(int scalar, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      double scalar1 = (double) scalar;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401145\u002D67();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Multiply\u00401145\u002D69<TRowKey>(scalar1, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Multiply\u00401145\u002D68(op)), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame2.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame2.IndexBuilder, frame2.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Multiply\u00401145\u002D71<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator /(Frame<TRowKey, TColumnKey> frame, int scalar)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Division\u00401148\u002D64<TRowKey>((double) scalar, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401148\u002D63()), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame1.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame1.IndexBuilder, frame1.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Division\u00401148\u002D66<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> operator /(int scalar, Frame<TRowKey, TColumnKey> frame)
    {
      Frame<TRowKey, TColumnKey> frame1 = frame;
      double scalar1 = (double) scalar;
      FSharpFunc<double, FSharpFunc<double, double>> op = (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401151\u002D67();
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      Series<TColumnKey, ISeries<TRowKey>> nested = SeriesModule.MapValues<ObjectSeries<TRowKey>, ISeries<TRowKey>, TColumnKey>((FSharpFunc<ObjectSeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Division\u00401151\u002D69<TRowKey>(scalar1, (FSharpFunc<double, FSharpFunc<double, double>>) new Frame.op_Division\u00401151\u002D68(op)), (Series<TColumnKey, ObjectSeries<TRowKey>>) frame2.Columns);
      return Frame<TRowKey, TColumnKey>.fromColumnsNonGeneric<ISeries<TRowKey>, TRowKey, TColumnKey>(frame2.IndexBuilder, frame2.VectorBuilder, (FSharpFunc<ISeries<TRowKey>, ISeries<TRowKey>>) new Frame.op_Division\u00401151\u002D71<TRowKey>(), nested);
    }

    public static Frame<TRowKey, TColumnKey> Acos(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Acos\u00401156\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Acos\u00401156\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Asin(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Asin\u00401158\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Asin\u00401158\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Atan(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Atan\u00401160\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Atan\u00401160\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Sin(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Sin\u00401162\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Sin\u00401162\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Sinh(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Sinh\u00401164\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Sinh\u00401164\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Cos(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Cos\u00401166\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Cos\u00401166\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Cosh(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Cosh\u00401168\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Cosh\u00401168\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Tan(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Tan\u00401170\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Tan\u00401170\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Tanh(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Tanh\u00401172\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Tanh\u00401172\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Abs(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Abs\u00401177\u002D5<TRowKey>((FSharpFunc<double, double>) new Frame.Abs\u00401177\u002D4()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Ceiling(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Ceiling\u00401179\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Ceiling\u00401179\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Exp(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Exp\u00401181\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Exp\u00401181\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Floor(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Floor\u00401183\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Floor\u00401183\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Truncate(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Truncate\u00401185\u002D4<TRowKey>((FSharpFunc<double, double>) new Frame.Truncate\u00401185\u002D3()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Log(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Log\u00401187\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Log\u00401187\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Log10(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Log10\u00401189\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Log10\u00401189\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Round(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Round\u00401191\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Round\u00401191\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Sign(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Sign\u00401193\u002D3<TRowKey>((FSharpFunc<double, int>) new Frame.Sign\u00401193\u002D2()).Invoke));
    }

    public static Frame<TRowKey, TColumnKey> Sqrt(Frame<TRowKey, TColumnKey> frame)
    {
      return frame.ColumnApply<double>(ConversionKind.Safe, new Func<Series<TRowKey, double>, ISeries<TRowKey>>(new Frame.Sqrt\u00401195\u002D3<TRowKey>((FSharpFunc<double, double>) new Frame.Sqrt\u00401195\u002D2()).Invoke));
    }

    public Frame(IEnumerable<TColumnKey> names, IEnumerable<ISeries<TRowKey>> columns)
    {
      Frame<TRowKey, TColumnKey> frame = (Frame<TRowKey, TColumnKey>) SeqModule.Fold<Tuple<TColumnKey, ISeries<TRowKey>>, Frame<TRowKey, TColumnKey>>((FSharpFunc<M1, FSharpFunc<M0, M1>>) new Frame.df\u00401203<TRowKey, TColumnKey>(), (M1) new Frame<TRowKey, TColumnKey>(F\u0023\u0020Index\u0020extensions.Index.ofKeys<TRowKey>(FSharpList<TRowKey>.get_Empty()), F\u0023\u0020Index\u0020extensions.Index.ofKeys<TColumnKey>(FSharpList<TColumnKey>.get_Empty()), F\u0023\u0020VectorBuilder\u0020implementation.VectorBuilder.Instance.Create<IVector>((IVector[]) ArrayModule.OfSeq<IVector>((IEnumerable<M0>) FSharpList<IVector>.get_Empty())), F\u0023\u0020IndexBuilder\u0020implementation.IndexBuilder.Instance, F\u0023\u0020VectorBuilder\u0020implementation.VectorBuilder.Instance), (IEnumerable<M0>) SeqModule.Zip<TColumnKey, ISeries<TRowKey>>((IEnumerable<M0>) names, (IEnumerable<M1>) columns));
      // ISSUE: explicit constructor call
      this.\u002Ector(frame.RowIndex, frame.ColumnIndex, frame.Data, F\u0023\u0020IndexBuilder\u0020implementation.IndexBuilder.Instance, F\u0023\u0020VectorBuilder\u0020implementation.VectorBuilder.Instance);
    }

    public Frame<TRowKey, TColumnKey> Clone()
    {
      return new Frame<TRowKey, TColumnKey>(this.rowIndex\u004099, this.columnIndex\u0040100, this.data, this.indexBuilder, this.vectorBuilder);
    }

    public override bool Equals(object another)
    {
      object obj = another;
      if (obj == null)
        return false;
      Frame<TRowKey, TColumnKey> frame1 = obj as Frame<TRowKey, TColumnKey>;
      if (frame1 == null)
        return false;
      Frame<TRowKey, TColumnKey> frame2 = frame1;
      if ((!this.RowIndex.Equals((object) frame2.RowIndex) ? 0 : (this.ColumnIndex.Equals((object) frame2.ColumnIndex) ? 1 : 0)) != 0)
        return this.Data.Equals((object) frame2.Data);
      return false;
    }

    public override int GetHashCode()
    {
      FSharpFunc<int, FSharpFunc<int, int>> fsharpFunc = (FSharpFunc<int, FSharpFunc<int, int>>) new Frame.op_PlusPlus\u00401225();
      return (int) FSharpFunc<int, int>.InvokeFast<int>((FSharpFunc<int, FSharpFunc<int, M0>>) fsharpFunc, (int) FSharpFunc<int, int>.InvokeFast<int>((FSharpFunc<int, FSharpFunc<int, M0>>) fsharpFunc, this.RowIndex.GetHashCode(), this.ColumnIndex.GetHashCode()), this.Data.GetHashCode());
    }

    public FrameData GetFrameData()
    {
      FSharpTypeFunc fsharpTypeFunc = (FSharpTypeFunc) new Frame.getKeys\u00401235();
      IEnumerable<object[]> rowKeys = ((FSharpFunc<IIndex<TRowKey>, IEnumerable<object[]>>) fsharpTypeFunc.Specialize<TRowKey>()).Invoke(this.RowIndex);
      IEnumerable<object[]> columnKeys = ((FSharpFunc<IIndex<TColumnKey>, IEnumerable<object[]>>) fsharpTypeFunc.Specialize<TColumnKey>()).Invoke(this.ColumnIndex);
      IEnumerable<Tuple<Type, IVector<object>>> columns = (IEnumerable<Tuple<Type, IVector<object>>>) SeqModule.Map<OptionalValue<IVector>, Tuple<Type, IVector<object>>>((FSharpFunc<M0, M1>) new Frame.columns\u00401246(), (IEnumerable<M0>) F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002Eget_DataSequence<IVector>(this.data));
      return new FrameData(columnKeys, rowKeys, columns);
    }

    public Frame<TRowKey, TColumnKey> GetSubrange(FSharpOption<Tuple<TRowKey, BoundaryBehavior>> lo, FSharpOption<Tuple<TRowKey, BoundaryBehavior>> hi)
    {
      Tuple<IIndex<TRowKey>, VectorConstruction> range = this.indexBuilder.GetRange<TRowKey>(new Tuple<IIndex<TRowKey>, VectorConstruction>(this.rowIndex\u004099, VectorConstruction.NewReturn(0)), new Tuple<FSharpOption<Tuple<TRowKey, BoundaryBehavior>>, FSharpOption<Tuple<TRowKey, BoundaryBehavior>>>(lo, hi));
      IIndex<TRowKey> rowIndex = range.Item1;
      VectorConstruction rowCmd = range.Item2;
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(this.data, (FSharpFunc<IVector, IVector>) new Frame.newData\u00401255\u002D5(this.vectorBuilder, rowIndex.AddressingScheme, rowCmd));
      return new Frame<TRowKey, TColumnKey>(rowIndex, this.columnIndex\u0040100, data, this.indexBuilder, this.vectorBuilder);
    }

    public Frame<TRowKey, TColumnKey> GetAddressRange(RangeRestriction<long> range)
    {
      Tuple<IIndex<TRowKey>, VectorConstruction> addressRange = this.indexBuilder.GetAddressRange<TRowKey>(new Tuple<IIndex<TRowKey>, VectorConstruction>(this.RowIndex, VectorConstruction.NewReturn(0)), range);
      IIndex<TRowKey> rowIndex = addressRange.Item1;
      VectorConstruction rowCmd = addressRange.Item2;
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(this.Data, (FSharpFunc<IVector, IVector>) new Frame.newData\u00401261\u002D6(this.vectorBuilder, rowIndex.AddressingScheme, rowCmd));
      return new Frame<TRowKey, TColumnKey>(rowIndex, this.columnIndex\u0040100, data, this.indexBuilder, this.vectorBuilder);
    }

    internal IEnumerable<FSharpChoice<Tuple<TRowKey, ObjectSeries<TColumnKey>>, Unit, a>> GetPrintedRowObservations<a>(int startCount, int endCount)
    {
      if (SeqModule.IsEmpty<KeyValuePair<TRowKey, long>>((IEnumerable<M0>) Seq.skipAtMost<KeyValuePair<TRowKey, long>>(startCount + endCount, this.RowIndex.Mappings)))
        return (IEnumerable<FSharpChoice<Tuple<TRowKey, ObjectSeries<TColumnKey>>, Unit, a>>) new Frame.GetPrintedRowObservations\u00401267<TRowKey, TColumnKey, a>(this, new KeyValuePair<TRowKey, ObjectSeries<TColumnKey>>(), (IEnumerator<KeyValuePair<TRowKey, ObjectSeries<TColumnKey>>>) null, 0, (FSharpChoice<Tuple<TRowKey, ObjectSeries<TColumnKey>>, Unit, a>) null);
      return (IEnumerable<FSharpChoice<Tuple<TRowKey, ObjectSeries<TColumnKey>>, Unit, a>>) new Frame.GetPrintedRowObservations\u00401271\u002D1<TRowKey, TColumnKey, a>(this.GetAddressRange(RangeRestriction<long>.NewStart((long) startCount)), this.GetAddressRange(RangeRestriction<long>.NewEnd((long) endCount)), new KeyValuePair<TRowKey, ObjectSeries<TColumnKey>>(), new KeyValuePair<TRowKey, ObjectSeries<TColumnKey>>(), (IEnumerator<KeyValuePair<TRowKey, ObjectSeries<TColumnKey>>>) null, (IEnumerator<KeyValuePair<TRowKey, ObjectSeries<TColumnKey>>>) null, 0, (FSharpChoice<Tuple<TRowKey, ObjectSeries<TColumnKey>>, Unit, a>) null);
    }

    public string Format()
    {
      return this.Format(Formatting.StartItemCount, Formatting.EndItemCount);
    }

    public string Format(bool printTypes)
    {
      return this.Format(Formatting.StartItemCount, Formatting.EndItemCount, printTypes);
    }

    public string Format(int count)
    {
      int num = count / 2;
      return this.Format(num, num);
    }

    public string Format(int startCount, int endCount)
    {
      return this.Format(startCount, endCount, false);
    }

    public string Format(int startCount, int endCount, bool printTypes)
    {
      try
      {
        int colLevels = !this.ColumnIndex.IsEmpty ? CustomKey.Get((object) this.ColumnIndex.KeyAt(this.ColumnIndex.AddressAt(0L))).Levels : 1;
        int rowLevels = !this.RowIndex.IsEmpty ? CustomKey.Get((object) this.RowIndex.KeyAt(this.RowIndex.AddressAt(0L))).Levels : 1;
        FSharpFunc<Type, string> formatType = (FSharpFunc<Type, string>) new Frame.formatType\u00401335();
        FSharpTypeFunc getLevel = (FSharpTypeFunc) new Frame.getLevel\u00401343\u002D1();
        return Formatting.formatTable((string[,]) ExtraTopLevelOperators.CreateArray2D<FSharpList<string>, string>((IEnumerable<M0>) new Frame.Format\u00401352\u002D3<TRowKey, TColumnKey>(this, startCount, endCount, printTypes, colLevels, rowLevels, formatType, getLevel, 0, (FSharpRef<FSharpOption<object>>[]) null, (FSharpFunc<int, FSharpFunc<Unit, Unit>>) null, (FSharpChoice<Tuple<TRowKey, ObjectSeries<TColumnKey>>, Unit, Tuple<TRowKey, ObjectSeries<TColumnKey>>>) null, (FSharpChoice<Tuple<TRowKey, ObjectSeries<TColumnKey>>, Unit, Tuple<TRowKey, ObjectSeries<TColumnKey>>>) null, (IEnumerator<int>) null, (IEnumerator<FSharpChoice<Tuple<TRowKey, ObjectSeries<TColumnKey>>, Unit, Tuple<TRowKey, ObjectSeries<TColumnKey>>>>) null, (FSharpList<string>) null, (IEnumerator<FSharpList<string>>) null, 0, (FSharpList<string>) null)));
      }
      catch (object ex)
      {
        return ((FSharpFunc<Exception, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<Exception, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<Exception, string>, Unit, string, string, Exception>("Formatting failed: %A"))).Invoke((Exception) ex);
      }
    }

    [Obsolete("GetAllColumns(bool) is obsolete. Use GetAllColumns(ConversionKind) instead.")]
    public IEnumerable<KeyValuePair<TColumnKey, Series<TRowKey, R>>> GetAllColumns<R>(bool strict)
    {
      return this.GetAllColumns<R>(!strict ? ConversionKind.Flexible : ConversionKind.Exact);
    }

    [Obsolete("ColumnApply(bool, Func) is obsolete. Use ColumnApply(ConversionKind, Func) instead.")]
    public Frame<TRowKey, TColumnKey> ColumnApply<T>(bool strict, Func<Series<TRowKey, T>, ISeries<TRowKey>> f)
    {
      return this.ColumnApply<T>(!strict ? ConversionKind.Flexible : ConversionKind.Exact, f);
    }

    a IFrame.Apply<a>(IFrameOperation<a> op)
    {
      return op.Invoke<TRowKey, TColumnKey>(this);
    }

    string IFsiFormattable.Format()
    {
      return this.Format();
    }

    [SpecialName]
    void INotifyCollectionChanged.add_CollectionChanged(NotifyCollectionChangedEventHandler handler)
    {
      this.frameColumnsChanged.get_Publish().AddHandler(handler);
    }

    [SpecialName]
    void INotifyCollectionChanged.remove_CollectionChanged(NotifyCollectionChangedEventHandler handler)
    {
      this.frameColumnsChanged.get_Publish().RemoveHandler(handler);
    }

    DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression expr)
    {
      return DynamicExtensions.createPropertyMetaObject<Frame<TRowKey, TColumnKey>>(expr, this, (FSharpFunc<FSharpExpr, FSharpFunc<string, FSharpFunc<Type, FSharpExpr>>>) new Frame.System\u002DDynamic\u002DIDynamicMetaObjectProvider\u002DGetMetaObject\u00401415\u002D2<TRowKey, TColumnKey>(), (FSharpFunc<FSharpExpr, FSharpFunc<string, FSharpFunc<Type, FSharpFunc<FSharpExpr, FSharpExpr>>>>) new Frame.System\u002DDynamic\u002DIDynamicMetaObjectProvider\u002DGetMetaObject\u00401423\u002D3<TRowKey, TColumnKey>());
    }

    [CompilerGenerated]
    internal IVector safeGetColVector(TColumnKey column_0, Lookup column_1, FSharpFunc<long, bool> column_2)
    {
      Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>> tuple1 = new Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>(column_0, column_1, column_2);
      IIndex<TColumnKey> columnIndex100 = this.columnIndex\u0040100;
      Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>> tuple2 = tuple1;
      TColumnKey key = tuple2.Item1;
      Lookup lookup1 = tuple2.Item2;
      FSharpFunc<long, bool> condition = tuple2.Item3;
      OptionalValue<Tuple<TColumnKey, long>> optionalValue1 = columnIndex100.Lookup(key, lookup1, condition);
      if (!optionalValue1.HasValue)
      {
        string paramName = "column";
        FSharpFunc<Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>, string> fsharpFunc1 = (FSharpFunc<Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>, string>, Unit, string, string, Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>>("Column with a key '%O' does not exist in the data frame"));
        Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>> tuple3 = tuple1;
        TColumnKey columnKey = tuple3.Item1;
        Lookup lookup2 = tuple3.Item2;
        FSharpFunc<long, bool> fsharpFunc2 = tuple3.Item3;
        throw new ArgumentException(fsharpFunc1.Invoke(new Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>(columnKey, lookup2, fsharpFunc2)), paramName);
      }
      OptionalValue<IVector> optionalValue2 = this.data.GetValue(optionalValue1.Value.Item2);
      if (!optionalValue2.HasValue)
      {
        string paramName = "column";
        FSharpFunc<Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>, string> fsharpFunc1 = (FSharpFunc<Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>, string>, Unit, string, string, Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>>("Column with a key '%O' is present, but does not contain a value"));
        Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>> tuple3 = tuple1;
        TColumnKey columnKey = tuple3.Item1;
        Lookup lookup2 = tuple3.Item2;
        FSharpFunc<long, bool> fsharpFunc2 = tuple3.Item3;
        throw new ArgumentException(fsharpFunc1.Invoke(new Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>(columnKey, lookup2, fsharpFunc2)), paramName);
      }
      return optionalValue2.Value;
    }

    [CompilerGenerated]
    internal OptionalValue<IVector> tryGetColVector(TColumnKey column_0, Lookup column_1, FSharpFunc<long, bool> column_2)
    {
      Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>> tuple1 = new Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>>(column_0, column_1, column_2);
      IIndex<TColumnKey> columnIndex100 = this.columnIndex\u0040100;
      Tuple<TColumnKey, Lookup, FSharpFunc<long, bool>> tuple2 = tuple1;
      TColumnKey key = tuple2.Item1;
      Lookup lookup = tuple2.Item2;
      FSharpFunc<long, bool> condition = tuple2.Item3;
      OptionalValue<Tuple<TColumnKey, long>> optionalValue = columnIndex100.Lookup(key, lookup, condition);
      if (!optionalValue.HasValue)
        return OptionalValue<IVector>.Missing;
      return this.data.GetValue(optionalValue.Value.Item2);
    }

    [CompilationArgumentCounts(new int[] {1, 1, 1, 1})]
    [CompilerGenerated]
    internal static Frame<d, e> fromColumnsNonGeneric<S, d, e>(IIndexBuilder indexBuilder, IVectorBuilder vectorBuilder, FSharpFunc<S, ISeries<d>> seriesConv, Series<e, S> nested)
    {
      Tuple<e, S>[] tupleArray1 = (Tuple<e, S>[]) ArrayModule.OfSeq<Tuple<e, S>>((IEnumerable<M0>) SeriesModule.GetObservations<e, S>(nested));
      FSharpOption<IIndex<d>> fsharpOption1 = (FSharpOption<IIndex<d>>) OptionModule.Map<Tuple<e, S>, IIndex<d>>((FSharpFunc<M0, M1>) new Frame.rowIndex\u0040124<S, d, e>(seriesConv), (FSharpOption<M0>) Seq.headOrNone<Tuple<e, S>>((IEnumerable<Tuple<e, S>>) tupleArray1));
      if (fsharpOption1 != null)
      {
        FSharpOption<IIndex<d>> fsharpOption2 = fsharpOption1;
        IIndex<d> rowIndex1 = fsharpOption2.get_Value();
        Tuple<e, S>[] tupleArray2 = tupleArray1;
        if (SeqModule.ForAll<Tuple<e, S>>((FSharpFunc<M0, bool>) new Frame.clo\u0040126\u002D7<S, e, d>(seriesConv, rowIndex1), (IEnumerable<M0>) tupleArray2))
        {
          IIndex<d> index1 = fsharpOption2.get_Value();
          Tuple<e, S>[] tupleArray3 = tupleArray1;
          IVector<IVector> data = F\u0023\u0020VectorBuilder\u0020implementation.VectorBuilder.Instance.Create<IVector>((IVector[]) ArrayModule.OfSeq<IVector>((IEnumerable<M0>) SeqModule.Map<Tuple<e, S>, IVector>((FSharpFunc<M0, M1>) new Frame.vector\u0040129\u002D10<S, e, d>(seriesConv), (IEnumerable<M0>) tupleArray3)));
          IIndex<d> index2 = index1;
          FSharpFunc<Tuple<e, S>, e> fsharpFunc = (FSharpFunc<Tuple<e, S>, e>) new Frame.clo\u0040132\u002D8<S, e>();
          Tuple<e, S>[] tupleArray4 = tupleArray1;
          if ((object) tupleArray4 == null)
            throw new ArgumentNullException("array");
          e[] keys = new e[tupleArray4.Length];
          IIndex<d> rowIndex2 = index2;
          for (int index3 = 0; index3 < keys.Length; ++index3)
            keys[index3] = fsharpFunc.Invoke(tupleArray4[index3]);
          return new Frame<d, e>(rowIndex2, F\u0023\u0020Index\u0020extensions.Index.ofKeys<e>(keys), data, indexBuilder, vectorBuilder);
        }
      }
      Tuple<e, S>[] tupleArray5 = tupleArray1;
      d[] dArray = (d[]) ArrayModule.OfSeq<d>(SeqModule.Distinct<d>((IEnumerable<M0>) SeqModule.Collect<Tuple<e, S>, ReadOnlyCollection<d>, d>((FSharpFunc<M0, M1>) new Frame.rowKeys\u0040136<S, d, e>(seriesConv), (IEnumerable<M0>) tupleArray5)));
      int num;
      if (nested.ValueCount > 0)
      {
        Tuple<e, S>[] tupleArray2 = tupleArray1;
        num = SeqModule.ForAll<Tuple<e, S>>((FSharpFunc<M0, bool>) new Frame.sorted\u0040139<S, e, d>(seriesConv), (IEnumerable<M0>) tupleArray2) ? 1 : 0;
      }
      else
        num = 0;
      FSharpOption<bool> fsharpOption3;
      if (num != 0)
      {
        Tuple<e, S>[] tupleArray2 = tupleArray1;
        ArrayModule.SortInPlaceWith<d>((FSharpFunc<M0, FSharpFunc<M0, int>>) new Frame.sorted\u0040141\u002D1<d>((Comparer<d>) SeqModule.Pick<Tuple<e, S>, Comparer<d>>((FSharpFunc<M0, FSharpOption<M1>>) new Frame.comparer\u0040140<S, d, e>(seriesConv), (IEnumerable<M0>) tupleArray2)), (M0[]) dArray);
        fsharpOption3 = FSharpOption<bool>.Some(true);
      }
      else
        fsharpOption3 = (FSharpOption<bool>) null;
      FSharpOption<bool> fsharpOption4 = fsharpOption3;
      IIndex<d> rowIndex = nested.IndexBuilder.Create<d>((IEnumerable<d>) dArray, fsharpOption4);
      e[] eArray = (e[]) ArrayModule.OfSeq<e>((IEnumerable<M0>) SeqModule.Map<Tuple<e, FSharpOption<S>>, e>((FSharpFunc<M0, M1>) new Frame.colKeys\u0040147<S, e>(), (IEnumerable<M0>) SeriesModule.GetAllObservations<e, S>(nested)));
      IIndex<e> columnIndex = nested.IndexBuilder.Create<e>((IEnumerable<e>) eArray, (FSharpOption<bool>) null);
      IVector<IVector> data1 = F\u0023\u0020VectorBuilder\u0020implementation.VectorBuilder.Instance.Create<IVector>((IVector[]) ArrayModule.OfSeq<IVector>((IEnumerable<M0>) SeqModule.Map<Tuple<e, FSharpOption<S>>, IVector>((FSharpFunc<M0, M1>) new Frame.data\u0040151\u002D14<S, e, d>(seriesConv, nested, rowIndex), (IEnumerable<M0>) SeriesModule.GetAllObservations<e, S>(nested))));
      return new Frame<d, e>(rowIndex, columnIndex, data1, indexBuilder, vectorBuilder);
    }

    [CompilationArgumentCounts(new int[] {1, 1})]
    internal Frame<Tuple<d, TRowKey>, TColumnKey> GroupByLabels<d>(IEnumerable<d> labels, int n)
    {
      FSharpList<int> list = (FSharpList<int>) SeqModule.ToList<int>(Operators.CreateSequence<int>((IEnumerable<M0>) Operators.OperatorIntrinsics.RangeInt32(0, 1, n - 1)));
      if (n != SeqModule.Length<d>(labels))
        throw Operators.Failure("GroupByLabels: Wrong number of labels. Make sure that your frame does not contain missing values.");
      Frame.relocs\u00401550\u002D10<TRowKey, d> relocs155010 = new Frame.relocs\u00401550\u002D10<TRowKey, d>();
      Frame.relocs\u00401547\u002D11<TRowKey, d> relocs154711 = new Frame.relocs\u00401547\u002D11<TRowKey, d>();
      Frame.relocs\u00401546\u002D12<TRowKey, d> relocs154612 = new Frame.relocs\u00401546\u002D12<TRowKey, d>();
      IEnumerable<d> ds = labels;
      IEnumerable<Tuple<M0, M1>> tuples1 = SeqModule.Zip<TRowKey, Tuple<int, d>>((IEnumerable<M0>) this.RowKeys, (IEnumerable<M1>) SeqModule.Zip<int, d>((IEnumerable<M0>) list, (IEnumerable<M1>) ds));
      IEnumerable<Tuple<M1, IEnumerable<M0>>> tuples2 = SeqModule.GroupBy<Tuple<TRowKey, Tuple<int, d>>, d>((FSharpFunc<M0, M1>) relocs154612, (IEnumerable<M0>) tuples1);
      IEnumerable<Tuple<TRowKey, Tuple<int, d>>> tuples3 = (IEnumerable<Tuple<TRowKey, Tuple<int, d>>>) SeqModule.Concat<IEnumerable<Tuple<TRowKey, Tuple<int, d>>>, Tuple<TRowKey, Tuple<int, d>>>((IEnumerable<M0>) SeqModule.Map<Tuple<d, IEnumerable<Tuple<TRowKey, Tuple<int, d>>>>, IEnumerable<Tuple<TRowKey, Tuple<int, d>>>>((FSharpFunc<M0, M1>) relocs154711, (IEnumerable<M0>) tuples2));
      IEnumerable<Tuple<M0, M1>> tuples4 = SeqModule.Zip<int, Tuple<TRowKey, Tuple<int, d>>>((IEnumerable<M0>) list, (IEnumerable<M1>) tuples3);
      ReadOnlyCollection<Tuple<Tuple<d, TRowKey>, Tuple<int, int>>> readOnlyCollection1 = System.Array.AsReadOnly<Tuple<Tuple<d, TRowKey>, Tuple<int, int>>>((Tuple<Tuple<d, TRowKey>, Tuple<int, int>>[]) ArrayModule.OfSeq<Tuple<Tuple<d, TRowKey>, Tuple<int, int>>>((IEnumerable<M0>) SeqModule.Map<Tuple<int, Tuple<TRowKey, Tuple<int, d>>>, Tuple<Tuple<d, TRowKey>, Tuple<int, int>>>((FSharpFunc<M0, M1>) relocs155010, (IEnumerable<M0>) tuples4)));
      FSharpFunc<Tuple<int, int>, Tuple<long, long>> g = (FSharpFunc<Tuple<int, int>, Tuple<long, long>>) new Frame.addressify\u00401554<TRowKey, TColumnKey>(this);
      FSharpFunc<Tuple<Tuple<d, TRowKey>, Tuple<int, int>>, Tuple<d, TRowKey>> fsharpFunc1 = (FSharpFunc<Tuple<Tuple<d, TRowKey>, Tuple<int, int>>, Tuple<d, TRowKey>>) new Frame.keys\u00401556\u002D7<TRowKey, d>();
      ReadOnlyCollection<Tuple<Tuple<d, TRowKey>, Tuple<int, int>>> readOnlyCollection2 = readOnlyCollection1;
      Tuple<d, TRowKey>[] array1 = (Tuple<d, TRowKey>[]) ArrayModule.ZeroCreate<Tuple<d, TRowKey>>(readOnlyCollection2.Count);
      int index1 = 0;
      int num1 = readOnlyCollection2.Count - 1;
      if (num1 >= index1)
      {
        do
        {
          array1[index1] = fsharpFunc1.Invoke(readOnlyCollection2[index1]);
          ++index1;
        }
        while (index1 != num1 + 1);
      }
      ReadOnlyCollection<Tuple<d, TRowKey>> keys = System.Array.AsReadOnly<Tuple<d, TRowKey>>(array1);
      FSharpFunc<Tuple<Tuple<d, TRowKey>, Tuple<int, int>>, Tuple<long, long>> fsharpFunc2 = (FSharpFunc<Tuple<Tuple<d, TRowKey>, Tuple<int, int>>, Tuple<long, long>>) new Frame.locs\u00401557\u002D1<TRowKey, d>((FSharpFunc<Tuple<Tuple<d, TRowKey>, Tuple<int, int>>, Tuple<int, int>>) new Frame.locs\u00401557<TRowKey, d>(), g);
      ReadOnlyCollection<Tuple<Tuple<d, TRowKey>, Tuple<int, int>>> readOnlyCollection3 = readOnlyCollection1;
      Tuple<long, long>[] array2 = (Tuple<long, long>[]) ArrayModule.ZeroCreate<Tuple<long, long>>(readOnlyCollection3.Count);
      int index2 = 0;
      int num2 = readOnlyCollection3.Count - 1;
      if (num2 >= index2)
      {
        do
        {
          array2[index2] = fsharpFunc2.Invoke(readOnlyCollection3[index2]);
          ++index2;
        }
        while (index2 != num2 + 1);
      }
      ReadOnlyCollection<Tuple<long, long>> readOnlyCollection4 = System.Array.AsReadOnly<Tuple<long, long>>(array2);
      IIndex<Tuple<d, TRowKey>> rowIndex = F\u0023\u0020Index\u0020extensions.Index.ofKeys<Tuple<d, TRowKey>>(keys);
      VectorConstruction rowCmd = VectorConstruction.NewRelocate(VectorConstruction.NewReturn(0), (long) n, (IEnumerable<Tuple<long, long>>) readOnlyCollection4);
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(this.Data, (FSharpFunc<IVector, IVector>) new Frame.newData\u00401561\u002D7(this.VectorBuilder, rowIndex.AddressingScheme, rowCmd));
      return new Frame<Tuple<d, TRowKey>, TColumnKey>(rowIndex, this.ColumnIndex, data, this.IndexBuilder, this.VectorBuilder);
    }

    internal Series<TGroup, Frame<TRowKey, TColumnKey>> NestRowsBy<TGroup>(IEnumerable<TGroup> labels)
    {
      IIndexBuilder indexBuilder = this.IndexBuilder;
      IVectorBuilder vectorBuilder = this.VectorBuilder;
      FSharpList<int> list = (FSharpList<int>) SeqModule.ToList<int>(Operators.CreateSequence<int>((IEnumerable<M0>) Operators.OperatorIntrinsics.RangeInt32(0, 1, this.RowCount - 1)));
      ReadOnlyCollection<Tuple<TGroup, IEnumerable<Tuple<TRowKey, int>>>> readOnlyCollection1 = System.Array.AsReadOnly<Tuple<TGroup, IEnumerable<Tuple<TRowKey, int>>>>((Tuple<TGroup, IEnumerable<Tuple<TRowKey, int>>>[]) ArrayModule.OfSeq<Tuple<TGroup, IEnumerable<Tuple<TRowKey, int>>>>((IEnumerable<M0>) SeqModule.Map<Tuple<TGroup, IEnumerable<Tuple<TGroup, Tuple<TRowKey, int>>>>, Tuple<TGroup, IEnumerable<Tuple<TRowKey, int>>>>((FSharpFunc<M0, M1>) new Frame.relocs\u00401575\u002D13<TRowKey, TGroup>(), (IEnumerable<M0>) SeqModule.GroupBy<Tuple<TGroup, Tuple<TRowKey, int>>, TGroup>((FSharpFunc<M0, M1>) new Frame.relocs\u00401574\u002D15<TRowKey, TGroup>(), (IEnumerable<M0>) SeqModule.Zip<TGroup, Tuple<TRowKey, int>>(labels, (IEnumerable<M1>) SeqModule.Zip<TRowKey, int>((IEnumerable<M0>) this.RowKeys, (IEnumerable<M1>) list))))));
      ReadOnlyCollection<Tuple<TGroup, IEnumerable<Tuple<TRowKey, int>>>> readOnlyCollection2 = readOnlyCollection1;
      FSharpFunc<Tuple<TGroup, IEnumerable<Tuple<TRowKey, int>>>, TGroup> fsharpFunc = (FSharpFunc<Tuple<TGroup, IEnumerable<Tuple<TRowKey, int>>>, TGroup>) new Frame.newIndex\u00401578\u002D7<TRowKey, TGroup>();
      ReadOnlyCollection<Tuple<TGroup, IEnumerable<Tuple<TRowKey, int>>>> readOnlyCollection3 = readOnlyCollection2;
      TGroup[] array = ArrayModule.ZeroCreate<TGroup>(readOnlyCollection3.Count);
      int index1 = 0;
      int num = readOnlyCollection3.Count - 1;
      if (num >= index1)
      {
        do
        {
          array[index1] = fsharpFunc.Invoke(readOnlyCollection3[index1]);
          ++index1;
        }
        while (index1 != num + 1);
      }
      IIndex<TGroup> index2 = F\u0023\u0020Index\u0020extensions.Index.ofKeys<TGroup>(System.Array.AsReadOnly<TGroup>(array));
      ReadOnlyCollection<Tuple<TGroup, IEnumerable<Tuple<TRowKey, int>>>> readOnlyCollection4 = readOnlyCollection1;
      IEnumerable<Frame<TRowKey, TColumnKey>> frames = (IEnumerable<Frame<TRowKey, TColumnKey>>) SeqModule.Map<Tuple<TGroup, IEnumerable<Tuple<TRowKey, int>>>, Frame<TRowKey, TColumnKey>>((FSharpFunc<M0, M1>) new Frame.groups\u00401580<TRowKey, TColumnKey, TGroup>(this, indexBuilder, vectorBuilder), (IEnumerable<M0>) readOnlyCollection4);
      return new Series<TGroup, Frame<TRowKey, TColumnKey>>(index2, F\u0023\u0020VectorBuilder\u0020implementation.VectorBuilder.Instance.Create<Frame<TRowKey, TColumnKey>>((Frame<TRowKey, TColumnKey>[]) ArrayModule.OfSeq<Frame<TRowKey, TColumnKey>>((IEnumerable<M0>) frames)), vectorBuilder, indexBuilder);
    }

    public Frame<Tuple<TGroup, TRowKey>, TColumnKey> GroupRowsBy<TGroup>(TColumnKey colKey)
    {
      IEnumerable<TGroup> values = this.GetColumn<TGroup>(colKey).Values;
      if (this.RowCount != SeqModule.Length<TGroup>(values))
        throw Operators.Failure("GroupRowsBy: Specified column contains missing values and cannot be used for grouping. Remove missing values first (e.g., by using dropSparseRowsBy).");
      return this.GroupByLabels<TGroup>(values, this.RowCount);
    }

    public Frame<Tuple<c, TRowKey>, TColumnKey> GroupRowsByIndex<c>(Func<TRowKey, c> keySelector)
    {
      ReadOnlyCollection<TRowKey> keys = this.RowIndex.Keys;
      return this.GroupByLabels<c>((IEnumerable<c>) SeqModule.Map<TRowKey, c>((FSharpFunc<M0, M1>) new Frame.labels\u00401604<TRowKey, c>(keySelector), (IEnumerable<M0>) keys), this.RowCount);
    }

    public Frame<Tuple<TGroup, TRowKey>, TColumnKey> GroupRowsUsing<TGroup>(Func<TRowKey, ObjectSeries<TColumnKey>, TGroup> f)
    {
      RowSeries<TRowKey, TColumnKey> rows = this.Rows;
      IEnumerable<TGroup> values = SeriesModule.GetValues<TRowKey, TGroup>(SeriesModule.Map<TRowKey, ObjectSeries<TColumnKey>, TGroup>((FSharpFunc<TRowKey, FSharpFunc<ObjectSeries<TColumnKey>, TGroup>>) new Frame.labels\u00401608\u002D1<TRowKey, TColumnKey, TGroup>(f), (Series<TRowKey, ObjectSeries<TColumnKey>>) rows));
      if (this.RowCount != SeqModule.Length<TGroup>(values))
        throw Operators.Failure("GroupRowsUsing: Generated labels contain missing values and cannot be used for grouping. Make sure the projection function does not return null or filter out the corresponding rows before grouping.");
      return this.GroupByLabels<TGroup>(values, this.RowCount);
    }

    public Frame<int, TColumnKey> AggregateRowsBy<a, b>(IEnumerable<TColumnKey> groupBy, IEnumerable<TColumnKey> aggBy, Func<Series<TRowKey, a>, b> aggFunc)
    {
      IEnumerable<TColumnKey> columnKeys = (IEnumerable<TColumnKey>) SeqModule.Filter<TColumnKey>((FSharpFunc<M0, bool>) new Frame.filterFunc\u00401628<TColumnKey>(new HashSet<TColumnKey>(groupBy)), (IEnumerable<M0>) this.ColumnKeys);
      Series<FSharpOption<object>[], Frame<TRowKey, TColumnKey>> series = this.NestRowsBy<FSharpOption<object>[]>((IEnumerable<FSharpOption<object>[]>) SeqModule.Map<ObjectSeries<TColumnKey>, FSharpOption<object>[]>((FSharpFunc<M0, M1>) new Frame.labels\u00401630\u002D6<TColumnKey>((FSharpFunc<ObjectSeries<TColumnKey>, IEnumerable<FSharpOption<object>>>) new Frame.labels\u00401630\u002D4<TColumnKey>((FSharpFunc<ObjectSeries<TColumnKey>, Series<TColumnKey, object>>) new Frame.labels\u00401630\u002D2<TColumnKey>(columnKeys), (FSharpFunc<Series<TColumnKey, object>, IEnumerable<FSharpOption<object>>>) new Frame.labels\u00401630\u002D3<TColumnKey>()), (FSharpFunc<IEnumerable<FSharpOption<object>>, FSharpOption<object>[]>) new Frame.labels\u00401630\u002D5()), (IEnumerable<M0>) this.Rows.Values));
      return FrameUtils.fromRows<int, TColumnKey, Series<TColumnKey, object>>(this.IndexBuilder, this.VectorBuilder, SeriesModule.IndexOrdinally<FSharpOption<object>[], Series<TColumnKey, object>>(SeriesModule.Map<FSharpOption<object>[], FSharpList<Tuple<TColumnKey, FSharpOption<object>>>, Series<TColumnKey, object>>((FSharpFunc<FSharpOption<object>[], FSharpFunc<FSharpList<Tuple<TColumnKey, FSharpOption<object>>>, Series<TColumnKey, object>>>) new Frame.AggregateRowsBy\u00401634<TColumnKey>(columnKeys), SeriesModule.Map<FSharpOption<object>[], Frame<TRowKey, TColumnKey>, FSharpList<Tuple<TColumnKey, FSharpOption<object>>>>((FSharpFunc<FSharpOption<object>[], FSharpFunc<Frame<TRowKey, TColumnKey>, FSharpList<Tuple<TColumnKey, FSharpOption<object>>>>>) new Frame.AggregateRowsBy\u00401633\u002D1<TRowKey, TColumnKey, b, a>(aggBy, aggFunc), series))));
    }

    public Frame<TNewRowIndex, TColumnKey> IndexRows<TNewRowIndex>(TColumnKey column, bool keepColumn)
    {
      Tuple<IIndex<TNewRowIndex>, VectorConstruction> tuple1 = this.IndexBuilder.WithIndex<TRowKey, TNewRowIndex>(this.RowIndex, this.GetColumn<TNewRowIndex>(column).Vector, VectorConstruction.NewReturn(0));
      VectorConstruction rowCmd = tuple1.Item2;
      IIndex<TNewRowIndex> rowIndex = tuple1.Item1;
      Tuple<IIndex<TColumnKey>, VectorConstruction> tuple2 = !keepColumn ? this.IndexBuilder.DropItem<TColumnKey>(new Tuple<IIndex<TColumnKey>, VectorConstruction>(this.ColumnIndex, VectorConstruction.NewReturn(0)), column) : new Tuple<IIndex<TColumnKey>, VectorConstruction>(this.ColumnIndex, VectorConstruction.NewReturn(0));
      IIndex<TColumnKey> columnIndex = tuple2.Item1;
      VectorConstruction vectorConstruction = tuple2.Item2;
      IVector<IVector> data = F\u0023\u0020Vector\u0020extensions\u0020\u0028core\u0029.IVector`1\u002ESelect<IVector, IVector>(this.VectorBuilder.Build<IVector>(columnIndex.AddressingScheme, vectorConstruction, new IVector<IVector>[1]
      {
        this.Data
      }), (FSharpFunc<IVector, IVector>) new Frame.newData\u00401661\u002D9(this.VectorBuilder, rowIndex.AddressingScheme, rowCmd));
      return new Frame<TNewRowIndex, TColumnKey>(rowIndex, columnIndex, data, this.IndexBuilder, this.VectorBuilder);
    }

    public Frame<TNewRowIndex, TColumnKey> IndexRows<TNewRowIndex>(TColumnKey column)
    {
      return this.IndexRows<TNewRowIndex>(column, false);
    }
  }
}
