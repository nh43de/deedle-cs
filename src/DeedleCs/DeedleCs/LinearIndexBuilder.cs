// Decompiled with JetBrains decompiler
// Type: Deedle.Indices.Linear.LinearIndexBuilder
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Internal;
using Deedle.Vectors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace Deedle.Indices.Linear
{

    [Serializable]
    public class LinearIndexBuilder : IIndexBuilder
    {
        internal IVectorBuilder vectorBuilder;
        internal static LinearIndexBuilder indexBuilder;
        internal static int init;

        public LinearIndexBuilder(IVectorBuilder vectorBuilder)
        {
            LinearIndexBuilder linearIndexBuilder = this;
            this.vectorBuilder = vectorBuilder;
        }

        public static IIndexBuilder Instance
        {
            get
            {
                if (LinearIndexBuilder.init < 5)
                    LanguagePrimitives.IntrinsicFunctions.FailStaticInit();
                return (IIndexBuilder)LinearIndexBuilder.indexBuilder;
            }
        }

        IIndex<j> IIndexBuilder.Project<j>(IIndex<j> index)
        {
            return index;
        }

        IIndex<i> IIndexBuilder.Recreate<i>(IIndex<i> index)
        {
            IIndex<i> index1 = index;
            if (index1 is LinearIndex<i>)
                return index;
            IIndex<i> index2 = index1;
            return LinearIndexBuilder.Instance.Create<i>(index2.Keys, FSharpOption<bool>.Some(index2.IsOrdered));
        }

        Tuple<FSharpAsync<IIndex<h>>, VectorConstruction> IIndexBuilder.AsyncMaterialize<h>(Tuple<IIndex<h>, VectorConstruction> _arg1)
        {
            Tuple<IIndex<h>, VectorConstruction> tuple = _arg1;
            VectorConstruction vectorConstruction = tuple.Item2;
            return new Tuple<FSharpAsync<IIndex<h>>, VectorConstruction>((FSharpAsync<IIndex<h>>)ExtraTopLevelOperators.get_DefaultAsyncBuilder().Return<IIndex<h>>((M0)tuple.Item1), vectorConstruction);
        }

        IIndex<K> IIndexBuilder.Create<K>(IEnumerable<K> keys, FSharpOption<bool> ordered)
        {
            return (IIndex<K>)new LinearIndex<K>(System.Array.AsReadOnly<K>(ArrayModule.OfSeq<K>(keys)), (IIndexBuilder)this, ordered);
        }

        IIndex<K> IIndexBuilder.Create<K>(ReadOnlyCollection<K> keys, FSharpOption<bool> ordered)
        {
            return (IIndex<K>)new LinearIndex<K>(keys, (IIndexBuilder)this, ordered);
        }

        Tuple<IIndex<TNewKey>, IVector<R>> IIndexBuilder.Aggregate<K, TNewKey, R>(IIndex<K> index, Aggregation<K> aggregation, VectorConstruction vector, Func<Tuple<DataSegmentKind, Tuple<IIndex<K>, VectorConstruction>>, Tuple<TNewKey, R>> selector)
        {
            if (!index.IsOrdered)
                throw new InvalidOperationException("Floating window aggregation and chunking is only supported on ordered indices. Consider sorting the series before calling the operation.");
            IIndexBuilder indexBuilder1 = (IIndexBuilder)this;
            Aggregation<K> aggregation1 = aggregation;
            IEnumerable<Tuple<DataSegmentKind, long, long>> tuples1;
            switch (aggregation1.get_Tag())
            {
                case 0:
                    Aggregation<K>.WindowSize windowSize = (Aggregation<K>.WindowSize)aggregation1;
                    tuples1 = Seq.windowRangesWithBounds((long)windowSize.item1, windowSize.item2, index.KeyCount);
                    break;
                case 2:
                    tuples1 = (IEnumerable<Tuple<DataSegmentKind, long, long>>)SeqModule.Map<Tuple<long, long>, Tuple<DataSegmentKind, long, long>>((Func<M0, M1>)new LinearIndex.locations(), (IEnumerable<M0>)Seq.windowRangesWhile<K>(((Aggregation<K>.WindowWhile)aggregation1).item, (IEnumerable<K>)index.Keys));
                    break;
                case 3:
                    tuples1 = (IEnumerable<Tuple<DataSegmentKind, long, long>>)SeqModule.Map<Tuple<long, long>, Tuple<DataSegmentKind, long, long>>((Func<M0, M1>)new LinearIndex.locations(), (IEnumerable<M0>)Seq.chunkRangesWhile<K>(((Aggregation<K>.ChunkWhile)aggregation1).item, (IEnumerable<K>)index.Keys));
                    break;
                default:
                    Aggregation<K>.ChunkSize chunkSize = (Aggregation<K>.ChunkSize)aggregation1;
                    tuples1 = Seq.chunkRangesWithBounds((long)chunkSize.item1, chunkSize.item2, index.KeyCount);
                    break;
            }
            IEnumerable<Tuple<DataSegmentKind, long, long>> tuples2 = tuples1;
            IEnumerable<Tuple<DataSegmentKind, Tuple<IIndex<K>, VectorConstruction>>> tuples3 = (IEnumerable<Tuple<DataSegmentKind, Tuple<IIndex<K>, VectorConstruction>>>)SeqModule.Map<Tuple<DataSegmentKind, long, long>, Tuple<DataSegmentKind, Tuple<IIndex<K>, VectorConstruction>>>((Func<M0, M1>)new LinearIndex.vectorConstructions<K>(index, vector), (IEnumerable<M0>)tuples2);
            Tuple<TNewKey, R>[] tupleArray1 = (Tuple<TNewKey, R>[])ArrayModule.OfSeq<Tuple<TNewKey, R>>((IEnumerable<M0>)SeqModule.Map<Tuple<DataSegmentKind, Tuple<IIndex<K>, VectorConstruction>>, Tuple<TNewKey, R>>((Func<M0, M1>)selector, (IEnumerable<M0>)tuples3));
            IIndexBuilder indexBuilder2 = indexBuilder1;
            Func<Tuple<TNewKey, R>, TNewKey> Func1 = (Func<Tuple<TNewKey, R>, TNewKey>)new LinearIndex.newIndex<TNewKey, R>();
            Tuple<TNewKey, R>[] tupleArray2 = tupleArray1;
            if ((object)tupleArray2 == null)
                throw new ArgumentNullException("array");
            TNewKey[] array = new TNewKey[tupleArray2.Length];
            IIndexBuilder indexBuilder3 = indexBuilder2;
            for (int index1 = 0; index1 < array.Length; ++index1)
                array[index1] = Func1.Invoke(tupleArray2[index1]);
            IIndex<TNewKey> index2 = indexBuilder3.Create<TNewKey>(System.Array.AsReadOnly<TNewKey>(array), (FSharpOption<bool>)null);
            IVectorBuilder vectorBuilder1 = this.vectorBuilder;
            Func<Tuple<TNewKey, R>, R> Func2 = (Func<Tuple<TNewKey, R>, R>)new LinearIndex.vect<TNewKey, R>();
            Tuple<TNewKey, R>[] tupleArray3 = tupleArray1;
            if ((object)tupleArray3 == null)
                throw new ArgumentNullException("array");
            R[] optionalValueArray = new R[tupleArray3.Length];
            IVectorBuilder vectorBuilder2 = vectorBuilder1;
            for (int index1 = 0; index1 < optionalValueArray.Length; ++index1)
                optionalValueArray[index1] = Func2.Invoke(tupleArray3[index1]);
            IVector<R> missing = vectorBuilder2.CreateMissing<R>(optionalValueArray);
            return new Tuple<IIndex<TNewKey>, IVector<R>>(index2, missing);
        }

        ReadOnlyCollection<Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>> IIndexBuilder.GroupBy<K, TNewKey>(IIndex<K> index, Func<K, TNewKey> keySel, VectorConstruction vector)
        {
            IIndexBuilder builder = (IIndexBuilder)this;
            LinearIndex.windows<K, TNewKey> windows336 = new LinearIndex.windows<K, TNewKey>();
            ReadOnlyCollection<K> keys = index.Keys;
            IEnumerable<Tuple<M1, IEnumerable<M0>>> tuples1 = SeqModule.GroupBy<K, TNewKey>((Func<M0, M1>)keySel, (IEnumerable<M0>)keys);
            IEnumerable<Tuple<TNewKey, IEnumerable<K>>> tuples2 = (IEnumerable<Tuple<TNewKey, IEnumerable<K>>>)SeqModule.Choose<Tuple<TNewKey, IEnumerable<K>>, Tuple<TNewKey, IEnumerable<K>>>((Func<M0, FSharpOption<M1>>)windows336, (IEnumerable<M0>)tuples1);
            return System.Array.AsReadOnly<Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>>((Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>[])ArrayModule.OfSeq<Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>>((IEnumerable<M0>)SeqModule.Map<Tuple<TNewKey, IEnumerable<K>>, Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>>((Func<M0, M1>)new LinearIndex.DeedleIndicesIIndexBuilderGroupBy<K, TNewKey>(index, vector, builder), (IEnumerable<M0>)tuples2)));
        }

        Tuple<IIndex<TNewKey>, IVector<R>> IIndexBuilder.Resample<K, TNewKey, R>(IIndexBuilder chunkBuilder, IIndex<K> index, IEnumerable<K> keys, Deedle.Direction dir, VectorConstruction vector, Func<Tuple<K, Tuple<IIndex<K>, VectorConstruction>>, Tuple<TNewKey, R>> selector)
        {
            if (!index.IsOrdered)
                throw new InvalidOperationException("Resampling is only supported on ordered indices");
            IIndexBuilder indexBuilder = (IIndexBuilder)this;
            IEnumerable<M1> m1s;
            if (dir == Deedle.Direction.Forward)
            {
                IEnumerable<Tuple<K, long>> tuples = (IEnumerable<Tuple<K, long>>)SeqModule.Map<K, Tuple<K, long>>((Func<M0, M1>)new LinearIndex.keyLocations<K>(index), keys);
                m1s = SeqModule.MapIndexed<Tuple<Tuple<K, long>, Tuple<K, long>>, Tuple<K, Tuple<long, long>>>((Func<int, Func<M0, M1>>)new LinearIndex.locations<K>(index), (IEnumerable<M0>)SeqModule.Pairwise<Tuple<K, long>>(SeqModule.Append<Tuple<K, long>>((IEnumerable<M0>)tuples, (IEnumerable<M0>)FSharpList<Tuple<K, long>>.Cons(new Tuple<K, long>(default(K), Addressing.LinearAddress.invalid), FSharpList<Tuple<K, long>>.get_Empty()))));
            }
            else
            {
                int keyLen = SeqModule.Length<K>(keys);
                IEnumerable<Tuple<K, long>> tuples = (IEnumerable<Tuple<K, long>>)SeqModule.Map<K, Tuple<K, long>>((Func<M0, M1>)new LinearIndex.keyLocations<K>(index), keys);
                m1s = SeqModule.MapIndexed<Tuple<Tuple<K, long>, Tuple<K, long>>, Tuple<K, Tuple<long, long>>>((Func<int, Func<M0, M1>>)new LinearIndex.locations<K>(index, keyLen), (IEnumerable<M0>)SeqModule.Pairwise<Tuple<K, long>>(SeqModule.Append<Tuple<K, long>>((IEnumerable<M0>)FSharpList<Tuple<K, long>>.Cons(new Tuple<K, long>(default(K), Addressing.LinearAddress.invalid), FSharpList<Tuple<K, long>>.get_Empty()), (IEnumerable<M0>)tuples)));
            }
            Tuple<K, Tuple<long, long>>[] tupleArray1 = (Tuple<K, Tuple<long, long>>[])ArrayModule.OfSeq<Tuple<K, Tuple<long, long>>>((IEnumerable<M0>)m1s);
            Func<Tuple<K, Tuple<long, long>>, Tuple<K, Tuple<IIndex<K>, VectorConstruction>>> Func1 = (Func<Tuple<K, Tuple<long, long>>, Tuple<K, Tuple<IIndex<K>, VectorConstruction>>>)new LinearIndex.vectorConstructions<K>(chunkBuilder, index);
            Tuple<K, Tuple<long, long>>[] tupleArray2 = tupleArray1;
            if ((object)tupleArray2 == null)
                throw new ArgumentNullException("array");
            Tuple<K, Tuple<IIndex<K>, VectorConstruction>>[] tupleArray3 = new Tuple<K, Tuple<IIndex<K>, VectorConstruction>>[tupleArray2.Length];
            for (int index1 = 0; index1 < tupleArray3.Length; ++index1)
                tupleArray3[index1] = Func1.Invoke(tupleArray2[index1]);
            Tuple<K, Tuple<IIndex<K>, VectorConstruction>>[] tupleArray4 = tupleArray3;
            Func<Tuple<K, Tuple<IIndex<K>, VectorConstruction>>, Tuple<TNewKey, R>> Func2 = selector;
            Tuple<K, Tuple<IIndex<K>, VectorConstruction>>[] tupleArray5 = tupleArray4;
            if ((object)tupleArray5 == null)
                throw new ArgumentNullException("array");
            Tuple<TNewKey, R>[] tupleArray6 = new Tuple<TNewKey, R>[tupleArray5.Length];
            for (int index1 = 0; index1 < tupleArray6.Length; ++index1)
                tupleArray6[index1] = Func2.Invoke(tupleArray5[index1]);
            Tuple<TNewKey, R>[] tupleArray7 = tupleArray6;
            IIndex<TNewKey> index2 = indexBuilder.Create<TNewKey>(SeqModule.Map<Tuple<TNewKey, R>, TNewKey>((Func<M0, M1>)new LinearIndex.newIndex<TNewKey, R>(), (IEnumerable<M0>)tupleArray7), (FSharpOption<bool>)null);
            IVectorBuilder vectorBuilder1 = this.vectorBuilder;
            Func<Tuple<TNewKey, R>, R> Func3 = (Func<Tuple<TNewKey, R>, R>)new LinearIndex.vect<TNewKey, R>();
            Tuple<TNewKey, R>[] tupleArray8 = tupleArray7;
            if ((object)tupleArray8 == null)
                throw new ArgumentNullException("array");
            R[] optionalValueArray = new R[tupleArray8.Length];
            IVectorBuilder vectorBuilder2 = vectorBuilder1;
            for (int index1 = 0; index1 < optionalValueArray.Length; ++index1)
                optionalValueArray[index1] = Func3.Invoke(tupleArray8[index1]);
            IVector<R> missing = vectorBuilder2.CreateMissing<R>(optionalValueArray);
            return new Tuple<IIndex<TNewKey>, IVector<R>>(index2, missing);
        }

        Tuple<IIndex<g>, VectorConstruction> IIndexBuilder.OrderIndex<g>(Tuple<IIndex<g>, VectorConstruction> _arg2)
        {
            Tuple<IIndex<g>, VectorConstruction> tuple = _arg2;
            VectorConstruction vectorConstruction = tuple.Item2;
            IIndex<g> index = tuple.Item1;
            g[] array = ArrayModule.OfSeq<g>((IEnumerable<M0>)index.Keys);
            ArrayModule.SortInPlaceWith<g>((Func<M0, Func<M0, int>>)new LinearIndex.DeedleIndicesIIndexBuilderOrderIndex<g>(index), array);
            IIndex<g> newIndex = (IIndex<g>)new LinearIndex<g>(System.Array.AsReadOnly<g>(array), (IIndexBuilder)this, FSharpOption<bool>.Some(true));
            IEnumerable<Tuple<long, long>> tuples = (IEnumerable<Tuple<long, long>>)new LinearIndex.relocations<g>(index, newIndex, new KeyValuePair<g, long>(), (Tuple<g, long>)null, new long(), default(g), (IEnumerator<KeyValuePair<g, long>>)null, 0, (Tuple<long, long>)null);
            return new Tuple<IIndex<g>, VectorConstruction>(newIndex, VectorConstruction.NewRelocate(vectorConstruction, newIndex.KeyCount, tuples));
        }

        Tuple<IIndex<f>, VectorConstruction> IIndexBuilder.Shift<f>(Tuple<IIndex<f>, VectorConstruction> _arg3, int offset)
        {
            Tuple<IIndex<f>, VectorConstruction> tuple1 = _arg3;
            VectorConstruction vectorConstruction = tuple1.Item2;
            IIndex<f> index = tuple1.Item1;
            Tuple<Tuple<long, long>, RangeRestriction<long>> tuple2 = offset <= 0 ? new Tuple<Tuple<long, long>, RangeRestriction<long>>(new Tuple<long, long>(0L, index.KeyCount - 1L + (long)offset), RangeRestriction<long>.NewFixed((long)-offset, index.KeyCount - 1L)) : new Tuple<Tuple<long, long>, RangeRestriction<long>>(new Tuple<long, long>((long)offset, index.KeyCount - 1L), RangeRestriction<long>.NewFixed(0L, index.KeyCount - 1L - (long)offset));
            RangeRestriction<long> rangeRestriction = tuple2.Item2;
            long startAddress = tuple2.Item1.Item1;
            long endAddress = tuple2.Item1.Item2;
            if (startAddress > endAddress)
            {
                ReadOnlyCollection<f> keys = ReadOnlyCollection.empty<f>();
                if (LinearIndexBuilder.init < 5)
                    LanguagePrimitives.IntrinsicFunctions.FailStaticInit();
                LinearIndexBuilder indexBuilder = LinearIndexBuilder.indexBuilder;
                FSharpOption<bool> ordered = FSharpOption<bool>.Some(true);
                return new Tuple<IIndex<f>, VectorConstruction>((IIndex<f>)new LinearIndex<f>(keys, (IIndexBuilder)indexBuilder, ordered), VectorConstruction.NewEmpty(0L));
            }
            FSharpOption<bool> fsharpOption = !index.IsOrdered ? (FSharpOption<bool>)null : FSharpOption<bool>.Some(true);
            return new Tuple<IIndex<f>, VectorConstruction>(!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Addressing.IAddressingScheme>((M0)index.AddressingScheme, (M0)Addressing.LinearAddressingScheme.Instance) ? (IIndex<f>)new LinearIndex<f>(ReadOnlyCollectionExtensions.ReadOnlyCollection`1GetSlice<f>(index.Keys, FSharpOption<int>.Some((int)startAddress), FSharpOption<int>.Some((int)endAddress)), LinearIndexBuilder.Instance, FSharpOption<bool>.Some(index.IsOrdered)) : (IIndex<f>)new LinearRangeIndex<f>(index, startAddress, endAddress), VectorConstruction.NewGetRange(vectorConstruction, rangeRestriction));
        }

        Tuple<IIndex<K>, VectorConstruction, VectorConstruction> IIndexBuilder.Union<K>(Tuple<IIndex<K>, VectorConstruction> _arg4, Tuple<IIndex<K>, VectorConstruction> _arg5)
        {
            Tuple<IIndex<K>, VectorConstruction> tuple1 = _arg4;
            VectorConstruction vectorConstruction1 = tuple1.Item2;
            IIndex<K> index1 = tuple1.Item1;
            Tuple<IIndex<K>, VectorConstruction> tuple2 = _arg5;
            VectorConstruction vectorConstruction2 = tuple2.Item2;
            IIndex<K> index2 = tuple2.Item1;
            Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>> tuple3;
            if ((!index1.IsOrdered ? 0 : (index2.IsOrdered ? 1 : 0)) != 0)
            {
                Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>> tuple4;
                try
                {
                    tuple4 = new Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>>(Seq.alignOrdered<K>(index1.Keys, index2.Keys, (IComparer<K>)index1.Comparer, false), FSharpOption<bool>.Some(true));
                }
                catch (object ex)
                {
                    if ((Exception)ex is ComparisonFailedException)
                        tuple4 = new Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>>(Seq.alignUnordered<K>(index1.Keys, index2.Keys, false), (FSharpOption<bool>)null);
                    else
                        throw;
                }
                tuple3 = tuple4;
            }
            else
                tuple3 = new Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>>(Seq.alignUnordered<K>(index1.Keys, index2.Keys, false), FSharpOption<bool>.Some(false));
            Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>> tuple5 = tuple3;
            FSharpOption<bool> fsharpOption = tuple5.Item2;
            Tuple<K[], FSharpList<Tuple<long, long>[]>> tuple6 = tuple5.Item1;
            LinearIndexBuilder linearIndexBuilder = this;
            Tuple<K[], FSharpList<Tuple<long, long>[]>> tuple7 = tuple6;
            VectorConstruction v1 = vectorConstruction1;
            VectorConstruction v2 = vectorConstruction2;
            FSharpOption<bool> ordered = fsharpOption;
            K[] spec_0 = tuple7.Item1;
            FSharpList<Tuple<long, long>[]> spec_1 = tuple7.Item2;
            return linearIndexBuilder.makeTwoSeriesConstructions<K>(spec_0, spec_1, v1, v2, ordered);
        }

        Tuple<IIndex<K>, VectorConstruction, VectorConstruction> IIndexBuilder.Intersect<K>(Tuple<IIndex<K>, VectorConstruction> _arg6, Tuple<IIndex<K>, VectorConstruction> _arg7)
        {
            Tuple<IIndex<K>, VectorConstruction> tuple1 = _arg6;
            VectorConstruction vectorConstruction1 = tuple1.Item2;
            IIndex<K> index1 = tuple1.Item1;
            Tuple<IIndex<K>, VectorConstruction> tuple2 = _arg7;
            VectorConstruction vectorConstruction2 = tuple2.Item2;
            IIndex<K> index2 = tuple2.Item1;
            Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>> tuple3;
            if ((!index1.IsOrdered ? 0 : (index2.IsOrdered ? 1 : 0)) != 0)
            {
                Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>> tuple4;
                try
                {
                    tuple4 = new Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>>(Seq.alignOrdered<K>(index1.Keys, index2.Keys, (IComparer<K>)index1.Comparer, true), FSharpOption<bool>.Some(true));
                }
                catch (object ex)
                {
                    if ((Exception)ex is ComparisonFailedException)
                        tuple4 = new Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>>(Seq.alignUnordered<K>(index1.Keys, index2.Keys, true), (FSharpOption<bool>)null);
                    else
                        throw;
                }
                tuple3 = tuple4;
            }
            else
                tuple3 = new Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>>(Seq.alignUnordered<K>(index1.Keys, index2.Keys, true), FSharpOption<bool>.Some(false));
            Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>> tuple5 = tuple3;
            FSharpOption<bool> fsharpOption = tuple5.Item2;
            Tuple<K[], FSharpList<Tuple<long, long>[]>> tuple6 = tuple5.Item1;
            LinearIndexBuilder linearIndexBuilder = this;
            Tuple<K[], FSharpList<Tuple<long, long>[]>> tuple7 = tuple6;
            VectorConstruction v1 = vectorConstruction1;
            VectorConstruction v2 = vectorConstruction2;
            FSharpOption<bool> ordered = fsharpOption;
            K[] spec_0 = tuple7.Item1;
            FSharpList<Tuple<long, long>[]> spec_1 = tuple7.Item2;
            return linearIndexBuilder.makeTwoSeriesConstructions<K>(spec_0, spec_1, v1, v2, ordered);
        }

        Tuple<IIndex<K>, VectorConstruction> IIndexBuilder.Merge<K>(FSharpList<Tuple<IIndex<K>, VectorConstruction>> constructions, VectorListTransform transform)
        {
            bool flag = ListModule.ForAll<Tuple<IIndex<K>, VectorConstruction>>((Func<M0, bool>)new LinearIndex.allOrdered<K>(), (FSharpList<M0>)constructions);
            Func<IComparer<K>, Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>>> Func1 = (Func<IComparer<K>, Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>>>)new LinearIndex.mergeOrdered<K>(constructions);
            Func<Unit, Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>>> Func2 = (Func<Unit, Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>>>)new LinearIndex.mergeUnordered<K>(constructions);
            Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>> tuple1;
            if (flag)
            {
                Comparer<K> comparer = ((Tuple<IIndex<K>, VectorConstruction>)ListModule.Head<Tuple<IIndex<K>, VectorConstruction>>((FSharpList<M0>)constructions)).Item1.Comparer;
                Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>> tuple2;
                try
                {
                    tuple2 = Func1.Invoke((IComparer<K>)comparer);
                }
                catch (object ex)
                {
                    if ((Exception)ex is ComparisonFailedException)
                        tuple2 = Func2.Invoke((Unit)null);
                    else
                        throw;
                }
                tuple1 = tuple2;
            }
            else
                tuple1 = Func2.Invoke((Unit)null);
            Tuple<Tuple<K[], FSharpList<Tuple<long, long>[]>>, FSharpOption<bool>> tuple3 = tuple1;
            FSharpOption<bool> fsharpOption = tuple3.Item2;
            Tuple<K[], FSharpList<Tuple<long, long>[]>> tuple4 = tuple3.Item1;
            IEnumerable<VectorConstruction> fsharpList1 = (IEnumerable<VectorConstruction>)ListModule.Map<Tuple<IIndex<K>, VectorConstruction>, VectorConstruction>((Func<M0, M1>)new LinearIndex.vectors<K>(), (FSharpList<M0>)constructions);
            LinearIndexBuilder linearIndexBuilder = this;
            Tuple<K[], FSharpList<Tuple<long, long>[]>> tuple5 = tuple4;
            IEnumerable<VectorConstruction> vectors = fsharpList1;
            FSharpOption<bool> ordered = fsharpOption;
            K[] keys = tuple5.Item1;
            FSharpList<Tuple<long, long>[]> relocations = tuple5.Item2;
            Tuple<IIndex<K>, IEnumerable<VectorConstruction>> tuple6 = linearIndexBuilder.makeSeriesConstructions<K>(keys, relocations, vectors, ordered);
            IEnumerable<VectorConstruction> fsharpList2 = tuple6.Item2;
            IIndex<K> newIndex = tuple6.Item1;
            return new Tuple<IIndex<K>, VectorConstruction>(newIndex, VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new LinearIndex.DeedleIndicesIIndexBuilderMerge<K>(newIndex)), fsharpList2, transform));
        }

        Tuple<IIndex<TNewKey>, VectorConstruction> IIndexBuilder.WithIndex<K, TNewKey>(IIndex<K> index1, IVector<TNewKey> indexVector, VectorConstruction vector)
        {
            Tuple<TNewKey, long>[] array = (Tuple<TNewKey, long>[])SeqModule.ToArray<Tuple<TNewKey, long>>((IEnumerable<M0>)new LinearIndex.newKeys<TNewKey, K>(index1, indexVector, new KeyValuePair<K, long>(), (Tuple<K, long>)null, new long(), default(K), new TNewKey(), (IEnumerator<KeyValuePair<K, long>>)null, 0, (Tuple<TNewKey, long>)null));
            LinearIndex<TNewKey> linearIndex = new LinearIndex<TNewKey>(System.Array.AsReadOnly<TNewKey>((TNewKey[])ArrayModule.OfSeq<TNewKey>((IEnumerable<M0>)SeqModule.Map<Tuple<TNewKey, long>, TNewKey>((Func<M0, M1>)new LinearIndex.newIndex<TNewKey>(), (IEnumerable<M0>)array))), (IIndexBuilder)this, (FSharpOption<bool>)null);
            long keyCount = ((IIndex<TNewKey>)linearIndex).KeyCount;
            LinearIndex.relocations relocations5178 = new LinearIndex.relocations();
            long lo = 0;
            long hi = keyCount - 1L;
            object obj = lo > hi ? (object)new LinearIndex.relocations(lo, hi, 1L, (Func<long, Func<long, bool>>)new LinearIndex.relocations()) : (object)new LinearIndex.relocations(lo, hi, 1L, (Func<long, Func<long, bool>>)new LinearIndex.relocations());
            IEnumerable<Tuple<long, long>> tuples = (IEnumerable<Tuple<long, long>>)SeqModule.Zip<long, long>((IEnumerable<M0>)SeqModule.Map<long, long>((Func<M0, M1>)relocations5178, (IEnumerable<M0>)obj), SeqModule.Map<Tuple<TNewKey, long>, long>((Func<M0, M1>)new LinearIndex.relocations<TNewKey>(), (IEnumerable<M0>)array));
            return new Tuple<IIndex<TNewKey>, VectorConstruction>((IIndex<TNewKey>)linearIndex, VectorConstruction.NewRelocate(vector, (long)array.Length, tuples));
        }

        VectorConstruction IIndexBuilder.Reindex<e>(IIndex<e> index1, IIndex<e> index2, Lookup semantics, VectorConstruction vector, Func<long, bool> condition)
        {
            IEnumerable<Tuple<long, long>> tuples = semantics != Lookup.Exact ? (IEnumerable<Tuple<long, long>>)new LinearIndex.relocations<e>(index1, index2, semantics, condition, new KeyValuePair<e, long>(), (Tuple<e, long>)null, new long(), default(e), new OptionalValue<Tuple<e, long>>(), (IEnumerator<KeyValuePair<e, long>>)null, 0, (Tuple<long, long>)null) : (IEnumerable<Tuple<long, long>>)new LinearIndex.relocations<e>(index1, index2, condition, new KeyValuePair<e, long>(), (Tuple<e, long>)null, new long(), default(e), new long(), (IEnumerator<KeyValuePair<e, long>>)null, 0, (Tuple<long, long>)null);
            return VectorConstruction.NewRelocate(vector, index2.KeyCount, tuples);
        }

        Tuple<IIndex<c>, VectorConstruction> IIndexBuilder.Search<c, d>(Tuple<IIndex<c>, VectorConstruction> _arg8, IVector<d> searchVector, d searchValue)
        {
            Tuple<IIndex<c>, VectorConstruction> tuple = _arg8;
            VectorConstruction vectorConstruction = tuple.Item2;
            IIndex<c> index1 = tuple.Item1;
            List<c> cList = new List<c>();
            List<long> longList = new List<long>();
            int index2 = 0;
            int count = index1.Keys.Count;
            int length = (int)searchVector.Length;
            int num = (count >= length ? length : count) - 1;
            if (num >= index2)
            {
                do
                {
                    d optionalValue = searchVector.GetValue((long)index2);
                    if ((!optionalValue.HasValue ? 0 : (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<d>((M0)optionalValue.Value, (M0)searchValue) ? 1 : 0)) != 0)
                    {
                        cList.Add(index1.Keys[index2]);
                        longList.Add((long)index2);
                    }
                    ++index2;
                }
                while (index2 != num + 1);
            }
            LinearIndex<c> linearIndex = new LinearIndex<c>(System.Array.AsReadOnly<c>(ArrayModule.OfSeq<c>((IEnumerable<c>)cList)), (IIndexBuilder)this, (FSharpOption<bool>)null);
            IEnumerable<long> indices = (IEnumerable<long>)SeqModule.Map<long, long>((Func<M0, M1>)new LinearIndex.range(), (IEnumerable<M0>)longList);
            RangeRestriction<long> rangeRestriction = VectorHelpers.RangeRestriction.ofSeq((long)longList.Count, indices);
            return new Tuple<IIndex<c>, VectorConstruction>((IIndex<c>)linearIndex, VectorConstruction.NewGetRange(vectorConstruction, rangeRestriction));
        }

        Tuple<IIndex<b>, VectorConstruction> IIndexBuilder.LookupLevel<b>(Tuple<IIndex<b>, VectorConstruction> _arg9, ICustomLookup<b> searchKey)
        {
            Tuple<IIndex<b>, VectorConstruction> tuple = _arg9;
            VectorConstruction vectorConstruction = tuple.Item2;
            IIndex<b> index = tuple.Item1;
            Tuple<long, b>[] array = (Tuple<long, b>[])SeqModule.ToArray<Tuple<long, b>>((IEnumerable<M0>)new LinearIndex.matching<b>(searchKey, index, new KeyValuePair<b, long>(), (Tuple<b, long>)null, default(b), new long(), (IEnumerator<KeyValuePair<b, long>>)null, 0, (Tuple<long, b>)null));
            long length = (long)array.Length;
            LinearIndex.relocs relocs5601 = new LinearIndex.relocs();
            long lo = 0;
            long hi = length - 1L;
            object obj = lo > hi ? (object)new LinearIndex.relocs(lo, hi, 1L, (Func<long, Func<long, bool>>)new LinearIndex.relocs()) : (object)new LinearIndex.relocs(lo, hi, 1L, (Func<long, Func<long, bool>>)new LinearIndex.relocs());
            IEnumerable<Tuple<long, long>> tuples = (IEnumerable<Tuple<long, long>>)SeqModule.Zip<long, long>((IEnumerable<M0>)SeqModule.Map<long, long>((Func<M0, M1>)relocs5601, (IEnumerable<M0>)obj), SeqModule.Map<Tuple<long, b>, long>((Func<M0, M1>)new LinearIndex.relocs<b>(), (IEnumerable<M0>)array));
            return new Tuple<IIndex<b>, VectorConstruction>((IIndex<b>)new LinearIndex<b>(System.Array.AsReadOnly<b>(ArrayModule.OfSeq<b>((IEnumerable<b>)SeqModule.Map<Tuple<long, b>, b>((Func<M0, M1>)new LinearIndex.newIndex<b>(), (IEnumerable<M0>)array))), (IIndexBuilder)this, FSharpOption<bool>.Some(index.IsOrdered)), VectorConstruction.NewRelocate(vectorConstruction, length, tuples));
        }

        Tuple<IIndex<K>, VectorConstruction> IIndexBuilder.DropItem<K>(Tuple<IIndex<K>, VectorConstruction> _arg10, K key)
        {
            Tuple<IIndex<K>, VectorConstruction> tuple1 = _arg10;
            VectorConstruction vectorConstruction1 = tuple1.Item2;
            IIndex<K> index = tuple1.Item1;
            FSharpChoice<Unit, Tuple<K, long>> fsharpChoice = OptionalValueModule.MissingPresent<Tuple<K, long>>(index.Lookup(key, Lookup.Exact, (Func<long, bool>)new LinearIndex.DeedleIndicesIIndexBuilderDropItem()));
            if (fsharpChoice is FSharpChoice<Unit, Tuple<K, long>>.Choice2Of2)
            {
                Tuple<K, long> tuple2 = ((FSharpChoice<Unit, Tuple<K, long>>.Choice2Of2)fsharpChoice).get_Item();
                VectorConstruction vectorConstruction2 = VectorConstruction.NewDropRange(vectorConstruction1, RangeRestriction<long>.NewFixed(tuple2.Item2, tuple2.Item2));
                ReadOnlyCollection<K> keys = index.Keys;
                return new Tuple<IIndex<K>, VectorConstruction>((IIndex<K>)new LinearIndex<K>(System.Array.AsReadOnly<K>(ArrayModule.OfSeq<K>(SeqModule.Filter<K>((Func<K, bool>)new LinearIndex.newKeys<K>(key), (IEnumerable<M0>)keys))), (IIndexBuilder)this, FSharpOption<bool>.Some(index.IsOrdered)), vectorConstruction2);
            }
            string paramName = nameof(key);
            throw new ArgumentException(((Func<K, string>)ExtraTopLevelOperators.PrintFormatToString<Func<K, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<K, string>, Unit, string, string, K>("The key '%O' is not present in the index."))).Invoke(key), paramName);
        }

        Tuple<IIndex<a>, VectorConstruction> IIndexBuilder.GetAddressRange<a>(Tuple<IIndex<a>, VectorConstruction> _arg11, RangeRestriction<long> range)
        {
            Tuple<IIndex<a>, VectorConstruction> tuple = _arg11;
            VectorConstruction vectorConstruction1 = tuple.Item2;
            IIndex<a> index1 = tuple.Item1;
            IIndexBuilder builder = (IIndexBuilder)this;
            FSharpChoice<Tuple<long, long>, IRangeRestriction<long>> fsharpChoice = range.AsAbsolute(index1.KeyCount);
            if (fsharpChoice is FSharpChoice<Tuple<long, long>, IRangeRestriction<long>>.Choice1Of2)
            {
                FSharpChoice<Tuple<long, long>, IRangeRestriction<long>>.Choice1Of2 choice1Of2 = (FSharpChoice<Tuple<long, long>, IRangeRestriction<long>>.Choice1Of2)fsharpChoice;
                long num1 = choice1Of2.get_Item().Item1;
                if (choice1Of2.get_Item().Item2 < num1)
                {
                    long num2 = choice1Of2.get_Item().Item1;
                    long num3 = choice1Of2.get_Item().Item2;
                    return new Tuple<IIndex<a>, VectorConstruction>((IIndex<a>)new LinearIndex<a>(ReadOnlyCollection.empty<a>(), builder, FSharpOption<bool>.Some(true)), VectorConstruction.NewEmpty(0L));
                }
            }
            if (!(fsharpChoice is FSharpChoice<Tuple<long, long>, IRangeRestriction<long>>.Choice2Of2))
            {
                FSharpChoice<Tuple<long, long>, IRangeRestriction<long>>.Choice1Of2 choice1Of2 = (FSharpChoice<Tuple<long, long>, IRangeRestriction<long>>.Choice1Of2)fsharpChoice;
                long startAddress = choice1Of2.get_Item().Item1;
                long endAddress = choice1Of2.get_Item().Item2;
                VectorConstruction range1 = VectorConstruction.NewGetRange(vectorConstruction1, RangeRestriction<long>.NewFixed(startAddress, endAddress));
                return new Tuple<IIndex<a>, VectorConstruction>((IIndex<a>)new LinearRangeIndex<a>(index1, startAddress, endAddress), range1);
            }
            IRangeRestriction<long> indices = ((FSharpChoice<Tuple<long, long>, IRangeRestriction<long>>.Choice2Of2)fsharpChoice).get_Item();
            IEnumerable<a> @as = (IEnumerable<a>)new LinearIndex.newKeys<a>(index1, indices, new long(), (IEnumerator<long>)null, 0, default(a));
            IIndex<a> index2 = builder.Create<a>(@as, (FSharpOption<bool>)null);
            LinearIndex.relocations relocations59218 = new LinearIndex.relocations();
            long lo = 0;
            long hi = index2.KeyCount - 1L;
            object obj = lo > hi ? (object)new LinearIndex.relocations(lo, hi, 1L, (Func<long, Func<long, bool>>)new LinearIndex.relocations()) : (object)new LinearIndex.relocations(lo, hi, 1L, (Func<long, Func<long, bool>>)new LinearIndex.relocations());
            IEnumerable<Tuple<long, long>> tuples = (IEnumerable<Tuple<long, long>>)SeqModule.Zip<long, long>((IEnumerable<M0>)SeqModule.Map<long, long>((Func<M0, M1>)relocations59218, (IEnumerable<M0>)obj), (IEnumerable<M1>)indices);
            VectorConstruction vectorConstruction2 = VectorConstruction.NewRelocate(vectorConstruction1, index2.KeyCount, tuples);
            return new Tuple<IIndex<a>, VectorConstruction>(index2, vectorConstruction2);
        }

        Tuple<IIndex<K>, VectorConstruction> IIndexBuilder.GetRange<K>(Tuple<IIndex<K>, VectorConstruction> _arg12, Tuple<FSharpOption<Tuple<K, BoundaryBehavior>>, FSharpOption<Tuple<K, BoundaryBehavior>>> _arg13)
        {
            Tuple<IIndex<K>, VectorConstruction> tuple1 = _arg12;
            VectorConstruction vectorConstruction = tuple1.Item2;
            IIndex<K> index = tuple1.Item1;
            Tuple<FSharpOption<Tuple<K, BoundaryBehavior>>, FSharpOption<Tuple<K, BoundaryBehavior>>> tuple2 = _arg13;
            FSharpOption<Tuple<K, BoundaryBehavior>> fsharpOption1 = tuple2.Item1;
            FSharpOption<Tuple<K, BoundaryBehavior>> fsharpOption2 = tuple2.Item2;
            Tuple<long, long> tuple3 = new Tuple<long, long>(0L, index.KeyCount - 1L);
            long num1 = tuple3.Item1;
            long num2 = tuple3.Item2;
            FSharpOption<Tuple<K, BoundaryBehavior>> fsharpOption3 = fsharpOption1;
            long num3;
            if (fsharpOption3 != null)
            {
                FSharpOption<Tuple<K, BoundaryBehavior>> fsharpOption4 = fsharpOption3;
                K key = fsharpOption4.get_Value().Item1;
                Lookup lookup = !fsharpOption4.get_Value().Item2.Equals((object)BoundaryBehavior.get_Exclusive(), LanguagePrimitives.get_GenericEqualityComparer()) ? Lookup.ExactOrGreater : Lookup.Greater;
                FSharpChoice<Unit, Tuple<K, long>> fsharpChoice = OptionalValueModule.MissingPresent<Tuple<K, long>>(index.Lookup(key, lookup, (Func<long, bool>)new LinearIndex.loBound()));
                num3 = !(fsharpChoice is FSharpChoice<Unit, Tuple<K, long>>.Choice1Of2) ? ((FSharpChoice<Unit, Tuple<K, long>>.Choice2Of2)fsharpChoice).get_Item().Item2 : num2 + 1L;
            }
            else
                num3 = num1;
            long startAddress = num3;
            FSharpOption<Tuple<K, BoundaryBehavior>> fsharpOption5 = fsharpOption2;
            long num4;
            if (fsharpOption5 != null)
            {
                FSharpOption<Tuple<K, BoundaryBehavior>> fsharpOption4 = fsharpOption5;
                K key = fsharpOption4.get_Value().Item1;
                Lookup lookup = !fsharpOption4.get_Value().Item2.Equals((object)BoundaryBehavior.get_Exclusive(), LanguagePrimitives.get_GenericEqualityComparer()) ? Lookup.ExactOrSmaller : Lookup.Smaller;
                FSharpChoice<Unit, Tuple<K, long>> fsharpChoice = OptionalValueModule.MissingPresent<Tuple<K, long>>(index.Lookup(key, lookup, (Func<long, bool>)new LinearIndex.hiBound()));
                num4 = !(fsharpChoice is FSharpChoice<Unit, Tuple<K, long>>.Choice1Of2) ? ((FSharpChoice<Unit, Tuple<K, long>>.Choice2Of2)fsharpChoice).get_Item().Item2 : num1 - 1L;
            }
            else
                num4 = num2;
            long endAddress = num4;
            if (endAddress < startAddress)
                return new Tuple<IIndex<K>, VectorConstruction>((IIndex<K>)new LinearIndex<K>(System.Array.AsReadOnly<K>(new K[0]), (IIndexBuilder)this, FSharpOption<bool>.Some(true)), VectorConstruction.NewEmpty(0L));
            return new Tuple<IIndex<K>, VectorConstruction>((IIndex<K>)new LinearRangeIndex<K>(index, startAddress, endAddress), VectorConstruction.NewGetRange(vectorConstruction, RangeRestriction<long>.NewFixed(startAddress, endAddress)));
        }


        internal Tuple<long, long>[] unsafeRelocsToAddr(Tuple<long, long>[] relocs)
        {
            return relocs;
        }

        
        internal Tuple<IIndex<K>, IEnumerable<VectorConstruction>> makeSeriesConstructions<K>(K[] keys, FSharpList<Tuple<long, long>[]> relocations, IEnumerable<VectorConstruction> vectors, FSharpOption<bool> ordered)
        {
            return new Tuple<IIndex<K>, IEnumerable<VectorConstruction>>((IIndex<K>)new LinearIndex<K>(System.Array.AsReadOnly<K>(keys), LinearIndexBuilder.Instance, ordered), (IEnumerable<VectorConstruction>)ListModule.MapIndexed2<VectorConstruction, Tuple<long, long>[], VectorConstruction>((Func<int, Func<M0, Func<M1, M2>>>)new LinearIndex.newVectors<K>(this, keys), (FSharpList<M0>)vectors, (FSharpList<M1>)relocations));
        }



        internal Tuple<IIndex<a>, VectorConstruction, VectorConstruction> makeTwoSeriesConstructions<a>(a[] spec_0, FSharpList<Tuple<long, long>[]> spec_1, VectorConstruction v1, VectorConstruction v2, FSharpOption<bool> ordered)
        {
            Tuple<a[], FSharpList<Tuple<long, long>[]>> tuple1 = new Tuple<a[], FSharpList<Tuple<long, long>[]>>(spec_0, spec_1);
            LinearIndexBuilder linearIndexBuilder = this;
            Tuple<a[], FSharpList<Tuple<long, long>[]>> tuple2 = tuple1;
            IEnumerable<VectorConstruction> vectors = IEnumerable<VectorConstruction>.Cons(v1, IEnumerable<VectorConstruction>.Cons(v2, IEnumerable<VectorConstruction>.get_Empty()));
            FSharpOption<bool> ordered1 = ordered;
            a[] keys = tuple2.Item1;
            FSharpList<Tuple<long, long>[]> relocations = tuple2.Item2;
            Tuple<IIndex<a>, IEnumerable<VectorConstruction>> tuple3 = linearIndexBuilder.makeSeriesConstructions<a>(keys, relocations, vectors, ordered1);
            if (tuple3.Item2.get_TailOrNull() != null)
            {
                IEnumerable<VectorConstruction> fsharpList = tuple3.Item2;
                if (fsharpList.get_TailOrNull().get_TailOrNull() != null)
                {
                    IEnumerable<VectorConstruction> tailOrNull = fsharpList.get_TailOrNull();
                    if (tailOrNull.get_TailOrNull().get_TailOrNull() == null)
                    {
                        VectorConstruction headOrDefault1 = tailOrNull.get_HeadOrDefault();
                        VectorConstruction headOrDefault2 = fsharpList.get_HeadOrDefault();
                        return new Tuple<IIndex<a>, VectorConstruction, VectorConstruction>(tuple3.Item1, headOrDefault2, headOrDefault1);
                    }
                }
            }
            throw Operators.Failure("makeTwoSeriesConstructions: Expected two vectors");
        }


        internal Tuple<LinearIndex<a>, VectorConstruction> asLinearIndex<a>(IIndex<a> index, VectorConstruction vector)
        {
            LinearIndex<a> linearIndex = index as LinearIndex<a>;
            if (linearIndex != null)
                return new Tuple<LinearIndex<a>, VectorConstruction>(linearIndex, vector);
            IEnumerable<Tuple<long, long>> tuples = (IEnumerable<Tuple<long, long>>)SeqModule.MapIndexed<KeyValuePair<a, long>, Tuple<long, long>>((Func<int, Func<M0, M1>>)new LinearIndex.relocs<a>(), (IEnumerable<M0>)index.Mappings);
            VectorConstruction vectorConstruction = VectorConstruction.NewRelocate(vector, index.KeyCount, tuples);
            return new Tuple<LinearIndex<a>, VectorConstruction>(new LinearIndex<a>(System.Array.AsReadOnly<a>(ArrayModule.OfSeq<a>((IEnumerable<a>)SeqModule.Map<KeyValuePair<a, long>, a>((Func<M0, M1>)new LinearIndex.clo<a>(), (IEnumerable<M0>)index.Mappings))), LinearIndexBuilder.Instance, (FSharpOption<bool>)null), vectorConstruction);
        }

        static LinearIndexBuilder()
        {
            LinearIndex.init = 0;
            int init = LinearIndex.init;
        }
    }
}
