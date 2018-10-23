// Decompiled with JetBrains decompiler
// Type: Deedle.Series`2
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Indices;
using Deedle.Indices.Linear;
using Deedle.Internal;
using Deedle.Keys;
using Deedle.Vectors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using static Deedle.VectorHelpers;

namespace Deedle
{
    [Serializable]
    public class Series<K, V> : IFsiFormattable, ISeries<K>
    {
        internal IVectorBuilder vectorBuilder;
        internal IVector<V> vector;
        internal IIndexBuilder indexBuilder;
        internal IIndex<K> index;
        internal int valueCount;

        public Series(IIndex<K> index, IVector<V> vector, IVectorBuilder vectorBuilder, IIndexBuilder indexBuilder)
        {
            Series<K, V> series = this;
            this.index = index;
            this.vector = vector;
            this.vectorBuilder = vectorBuilder;
            this.indexBuilder = indexBuilder;

            if (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Addressing.IAddressingScheme>(this.index.AddressingScheme, this.vector.AddressingScheme))
                throw new InvalidOperationException("Index and vector of a series should share addressing scheme!");

            if ((!((object)this.index is LinearIndex<K>) ? 0 : ((object)this.vector is Deedle.Vectors.ArrayVector.ArrayVector<V> ? 1 : 0)) != 0 && this.index.KeyCount != this.vector.Length)
                throw new InvalidOperationException("Index and vector of a series should have the same length!");

            this.valueCount = -1;
        }

        public IVectorBuilder VectorBuilder
        {
            get
            {
                return this.vectorBuilder;
            }
        }

        public IIndexBuilder IndexBuilder
        {
            get
            {
                return this.indexBuilder;
            }
        }

        public IIndex<K> Index
        {
            get
            {
                return this.index;
            }
        }

        IVector ISeries<K>.Vector
        {
            get
            {
                return this.vector;
            }
        }

        public IVector<V> Vector
        {
            get
            {
                return this.vector;
            }
        }
        
        public IEnumerable<K> Keys
        {
            get
            {
                return index.Mappings.Select(kvp => kvp.Key);
            }
        }

        public IEnumerable<V> Values
        {
            get
            {                
                return index.Mappings.Select((kvp, idx) => this.vector.GetValueAtLocation(new KnownLocation(kvp.Value, idx)));
            }
        }

        //public IEnumerable<V> ValuesAll
        //{
        //    get
        //    {
        //        return (IEnumerable<V>)new Series.get_ValuesAll<V, K>((Func<long, Func<KeyValuePair<K, long>, V>>)new Series.get_ValuesAll<K, V>(this), this.index.Mappings, (FSharpRef<long>)null, new KeyValuePair<K, long>(), (IEnumerator<KeyValuePair<K, long>>)null, 0, default(V));
        //    }
        //}

        public IEnumerable<KeyValuePair<K, V>> Observations
        {
            get
            {
                return (IEnumerable<KeyValuePair<K, V>>)new Series.get_Observations<K, V>((Func<long, Func<KeyValuePair<K, long>, FSharpOption<KeyValuePair<K, V>>>>)new Series.get_Observations<K, V>(this), this.index.Mappings, (FSharpRef<long>)null, new KeyValuePair<K, long>(), (FSharpOption<KeyValuePair<K, V>>)null, (FSharpOption<KeyValuePair<K, V>>)null, (IEnumerator<KeyValuePair<K, long>>)null, 0, new KeyValuePair<K, V>());
            }
        }

        public IEnumerable<KeyValuePair<K, OptionalValue<V>>> ObservationsAll
        {
            get
            {
                return (IEnumerable<KeyValuePair<K, OptionalValue<V>>>)new Series.get_ObservationsAll<K, V>((Func<long, Func<KeyValuePair<K, long>, KeyValuePair<K, OptionalValue<V>>>>)new Series.get_ObservationsAll<K, V>(this), this.index.Mappings, (FSharpRef<long>)null, new KeyValuePair<K, long>(), (IEnumerator<KeyValuePair<K, long>>)null, 0, new KeyValuePair<K, OptionalValue<V>>());
            }
        }

        public bool IsEmpty
        {
            get
            {
                return SeqModule.IsEmpty<KeyValuePair<K, long>>((IEnumerable<M0>)this.index.Mappings);
            }
        }

        public bool IsOrdered
        {
            get
            {
                return this.index.IsOrdered;
            }
        }

        public Tuple<K, K> KeyRange
        {
            get
            {
                return this.index.KeyRange;
            }
        }

        public int KeyCount
        {
            get
            {
                return (int)this.index.KeyCount;
            }
        }

        public int ValueCount
        {
            get
            {
                if (this.valueCount == -1)
                {
                    int num = 0;
                    IEnumerator<KeyValuePair<K, long>> enumerator = this.index.Mappings.GetEnumerator();
                    try
                    {
                        while (enumerator.MoveNext())
                        {
                            if (this.vector.GetValue(enumerator.Current.Value).HasValue)
                                ++num;
                        }
                    }
                    finally
                    {
                        (enumerator as IDisposable)?.Dispose();
                    }
                    this.valueCount = num;
                }
                return this.valueCount;
            }
        }

        public Series<K, V> GetAddressRange(RangeRestriction<long> range)
        {
            Tuple<IIndex<K>, VectorConstruction> addressRange = this.indexBuilder.GetAddressRange<K>(new Tuple<IIndex<K>, VectorConstruction>(this.index, VectorConstruction.NewReturn(0)), range);
            IIndex<K> index = addressRange.Item1;
            VectorConstruction vectorConstruction = addressRange.Item2;
            IVector<V> vector = this.vectorBuilder.Build<V>(index.AddressingScheme, vectorConstruction, new IVector<V>[1]
            {
        this.vector
            });
            return new Series<K, V>(index, vector, this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, V> GetSubrange(Tuple<K, BoundaryBehavior> lo, Tuple<K, BoundaryBehavior> hi)
        {
            Tuple<IIndex<K>, VectorConstruction> range = this.indexBuilder.GetRange<K>(new Tuple<IIndex<K>, VectorConstruction>(this.index, VectorConstruction.NewReturn(0)), new Tuple<FSharpOption<Tuple<K, BoundaryBehavior>>, FSharpOption<Tuple<K, BoundaryBehavior>>>(lo, hi));
            VectorConstruction vectorConstruction = range.Item2;
            IIndex<K> index = range.Item1;
            IVector<V> vector = this.vectorBuilder.Build<V>(index.AddressingScheme, vectorConstruction, new IVector<V>[1]
            {
        this.vector
            });
            return new Series<K, V>(index, vector, this.vectorBuilder, this.indexBuilder);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Series<K, V> GetSlice(K lo, K hi)
        {
            FSharpTypeFunc fsharpTypeFunc = (FSharpTypeFunc)new Series.inclusive();
            return this.GetSubrange(((Func<FSharpOption<K>, FSharpOption<Tuple<K, BoundaryBehavior>>>)fsharpTypeFunc.Specialize<K>()).Invoke(lo), ((Func<FSharpOption<K>, FSharpOption<Tuple<K, BoundaryBehavior>>>)fsharpTypeFunc.Specialize<K>()).Invoke(hi));
        }

        public Series<K, V> Between(K lowerInclusive, K upperInclusive)
        {
            return this.GetSubrange(FSharpOption<Tuple<K, BoundaryBehavior>>.Some(new Tuple<K, BoundaryBehavior>(lowerInclusive, BoundaryBehavior.get_Inclusive())), FSharpOption<Tuple<K, BoundaryBehavior>>.Some(new Tuple<K, BoundaryBehavior>(upperInclusive, BoundaryBehavior.get_Inclusive())));
        }

        public Series<K, V> After(K lowerExclusive)
        {
            return this.GetSubrange(FSharpOption<Tuple<K, BoundaryBehavior>>.Some(new Tuple<K, BoundaryBehavior>(lowerExclusive, BoundaryBehavior.get_Exclusive())), (FSharpOption<Tuple<K, BoundaryBehavior>>)null);
        }

        public Series<K, V> Before(K upperExclusive)
        {
            return this.GetSubrange((FSharpOption<Tuple<K, BoundaryBehavior>>)null, FSharpOption<Tuple<K, BoundaryBehavior>>.Some(new Tuple<K, BoundaryBehavior>(upperExclusive, BoundaryBehavior.get_Exclusive())));
        }

        public Series<K, V> StartAt(K lowerInclusive)
        {
            return this.GetSubrange(FSharpOption<Tuple<K, BoundaryBehavior>>.Some(new Tuple<K, BoundaryBehavior>(lowerInclusive, BoundaryBehavior.get_Inclusive())), (FSharpOption<Tuple<K, BoundaryBehavior>>)null);
        }

        public Series<K, V> EndAt(K upperInclusive)
        {
            return this.GetSubrange((FSharpOption<Tuple<K, BoundaryBehavior>>)null, FSharpOption<Tuple<K, BoundaryBehavior>>.Some(new Tuple<K, BoundaryBehavior>(upperInclusive, BoundaryBehavior.get_Inclusive())));
        }

        public Series<K, V> GetItems(IEnumerable<K> keys)
        {
            return this.GetItems(keys, Lookup.Exact);
        }

        public Series<K, V> GetItems(IEnumerable<K> keys, Lookup lookup)
        {
            IIndex<K> index = this.indexBuilder.Create<K>(keys, (FSharpOption<bool>)null);
            VectorConstruction vectorConstruction = this.indexBuilder.Reindex<K>(this.index, index, lookup, VectorConstruction.NewReturn(0), (Func<long, bool>)new Series.cmd<K, V>(this));
            IVector<V> vector = this.vectorBuilder.Build<V>(index.AddressingScheme, vectorConstruction, new IVector<V>[1]
            {
        this.vector
            });
            return new Series<K, V>(index, vector, this.vectorBuilder, this.indexBuilder);
        }

        public OptionalValue<KeyValuePair<K, V>> TryGetObservation(K key, Lookup lookup)
        {
            FSharpChoice<Unit, Tuple<K, long>> fsharpChoice = OptionalValueModule.MissingPresent<Tuple<K, long>>(this.index.Lookup(key, lookup, (Func<long, bool>)new Series.address<K, V>(this)));
            if (!(fsharpChoice is FSharpChoice<Unit, Tuple<K, long>>.Choice2Of2))
                return OptionalValue<KeyValuePair<K, V>>.Missing;
            K key1 = ((FSharpChoice<Unit, Tuple<K, long>>.Choice2Of2)fsharpChoice).get_Item().Item1;
            OptionalValue<V> optionalValue1 = this.vector.GetValue(((FSharpChoice<Unit, Tuple<K, long>>.Choice2Of2)fsharpChoice).get_Item().Item2);
            Func<V, KeyValuePair<K, V>> Func = (Func<V, KeyValuePair<K, V>>)new Series.TryGetObservation<K, V>(key1);
            OptionalValue<V> optionalValue2 = optionalValue1;
            if (optionalValue2.HasValue)
                return new OptionalValue<KeyValuePair<K, V>>(Func.Invoke(optionalValue2.Value));
            return OptionalValue<KeyValuePair<K, V>>.Missing;
        }

        public KeyValuePair<K, V> GetObservation(K key, Lookup lookup)
        {
            OptionalValue<Tuple<K, long>> optionalValue1 = this.index.Lookup(key, lookup, (Func<long, bool>)new Series.mapping<K, V>(this));
            if (!optionalValue1.HasValue)
                throw new KeyNotFoundException(((Func<K, string>)ExtraTopLevelOperators.PrintFormatToString<Func<K, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<K, string>, Unit, string, string, K>("The key %O is not present in the index"))).Invoke(key));
            OptionalValue<V> optionalValue2 = this.vector.GetValue(optionalValue1.Value.Item2);
            if (!optionalValue2.HasValue)
            {
                K k = key;
                throw new MissingValueException((object)k, ((Func<K, string>)ExtraTopLevelOperators.PrintFormatToString<Func<K, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<K, string>, Unit, string, string, K>("Value at the key %O is missing"))).Invoke(k));
            }
            return new KeyValuePair<K, V>(optionalValue1.Value.Item1, optionalValue2.Value);
        }

        public OptionalValue<V> TryGet(K key, Lookup lookup)
        {
            OptionalValue<KeyValuePair<K, V>> observation = this.TryGetObservation(key, lookup);
            Func<KeyValuePair<K, V>, V> Func = (Func<KeyValuePair<K, V>, V>)new Series.TryGet<K, V>();
            OptionalValue<KeyValuePair<K, V>> optionalValue = observation;
            if (optionalValue.HasValue)
                return new OptionalValue<V>(Func.Invoke(optionalValue.Value));
            return OptionalValue<V>.Missing;
        }

        public V Get(K key, Lookup lookup)
        {
            return this.GetObservation(key, lookup).Value;
        }

        public Series<K, V> GetByLevel(ICustomLookup<K> key)
        {
            Tuple<IIndex<K>, VectorConstruction> tuple = this.indexBuilder.LookupLevel<K>(new Tuple<IIndex<K>, VectorConstruction>(this.index, VectorConstruction.NewReturn(0)), key);
            IIndex<K> index = tuple.Item1;
            VectorConstruction vectorConstruction = tuple.Item2;
            IVector<V> vector = this.vectorBuilder.Build<V>(index.AddressingScheme, vectorConstruction, new IVector<V>[1]
            {
        this.vector
            });
            return new Series<K, V>(index, vector, this.vectorBuilder, this.indexBuilder);
        }

        public OptionalValue<KeyValuePair<K, OptionalValue<V>>> TryGetObservation(K key)
        {
            long num = this.index.Locate(key);
            if (num == Addressing.AddressModule.invalid)
                return OptionalValue<KeyValuePair<K, OptionalValue<V>>>.Missing;
            OptionalValue<V> optionalValue = this.vector.GetValue(num);
            return new OptionalValue<KeyValuePair<K, OptionalValue<V>>>(new KeyValuePair<K, OptionalValue<V>>(key, optionalValue));
        }

        public KeyValuePair<K, V> GetObservation(K key)
        {
            long num = this.index.Locate(key);
            if (num == Addressing.AddressModule.invalid)
                throw new KeyNotFoundException(((Func<K, string>)ExtraTopLevelOperators.PrintFormatToString<Func<K, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<K, string>, Unit, string, string, K>("The key %O is not present in the index"))).Invoke(key));
            OptionalValue<V> optionalValue = this.vector.GetValue(num);
            if (!optionalValue.HasValue)
            {
                K k = key;
                throw new MissingValueException((object)k, ((Func<K, string>)ExtraTopLevelOperators.PrintFormatToString<Func<K, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<K, string>, Unit, string, string, K>("Value at the key %O is missing"))).Invoke(k));
            }
            return new KeyValuePair<K, V>(key, optionalValue.Value);
        }

        public OptionalValue<V> TryGet(K key)
        {
            long num = this.Index.Locate(key);
            if (num == Addressing.AddressModule.invalid)
                return OptionalValue<V>.Missing;
            return this.Vector.GetValue(num);
        }

        public V Get(K key)
        {
            long num = this.Index.Locate(key);
            if (num == Addressing.AddressModule.invalid)
                throw new KeyNotFoundException(((Func<K, string>)ExtraTopLevelOperators.PrintFormatToString<Func<K, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<K, string>, Unit, string, string, K>("The key %O is not present in the index"))).Invoke(key));
            FSharpChoice<Unit, V> fsharpChoice = OptionalValueModule.MissingPresent<V>(this.Vector.GetValue(num));
            if (!(fsharpChoice is FSharpChoice<Unit, V>.Choice2Of2))
            {
                K k = key;
                throw new MissingValueException((object)k, ((Func<K, string>)ExtraTopLevelOperators.PrintFormatToString<Func<K, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<K, string>, Unit, string, string, K>("Value at the key %O is missing"))).Invoke(k));
            }
            return ((FSharpChoice<Unit, V>.Choice2Of2)fsharpChoice).get_Item();
        }

        public OptionalValue<V> TryGetAt(int index)
        {
            return this.Vector.GetValue(this.Index.AddressAt((long)index));
        }

        public K GetKeyAt(int index)
        {
            return this.Index.KeyAt(this.Index.AddressAt((long)index));
        }

        public V GetAt(int index)
        {
            if (this.Index.IsEmpty)
                throw new IndexOutOfRangeException("Series is empty");
            return this.TryGetAt(index).Value;
        }

        public V this[K a]
        {
            get
            {
                return this.Get(a);
            }
        }

        public Series<K, V> this[IEnumerable<K> items]
        {
            get
            {
                return this.GetItems(items);
            }
        }

        public Series<K, V> this[ICustomLookup<K> a]
        {
            get
            {
                return this.GetByLevel(a);
            }
        }

        [SpecialName]
        public static a op_Dynamic<a>(Series<string, a> series, string name)
        {
            return series.Get(name, Lookup.Exact);
        }

        public Series<K, V> Where(Func<KeyValuePair<K, V>, int, bool> f)
        {
            KeyValuePair<K, long>[] keyValuePairArray1 = (KeyValuePair<K, long>[])ArrayModule.OfSeq<KeyValuePair<K, long>>((IEnumerable<M0>)this.index.Mappings);
            Func<int, Func<KeyValuePair<K, long>, FSharpOption<Tuple<K, OptionalValue<V>>>>> Func = (Func<int, Func<KeyValuePair<K, long>, FSharpOption<Tuple<K, OptionalValue<V>>>>>)new Series.Where<K, V>(this, f);
            KeyValuePair<K, long>[] keyValuePairArray2 = keyValuePairArray1;
            List<Tuple<K, OptionalValue<V>>> tupleList = new List<Tuple<K, OptionalValue<V>>>();
            for (int index = 0; index < keyValuePairArray2.Length; ++index)
            {
                FSharpOption<Tuple<K, OptionalValue<V>>> fsharpOption = (FSharpOption<Tuple<K, OptionalValue<V>>>)Func<int, KeyValuePair<K, long>>.InvokeFast<FSharpOption<Tuple<K, OptionalValue<V>>>>((Func<int, Func<KeyValuePair<K, long>, M0>>)Func, index, keyValuePairArray2[index]);
                if (fsharpOption != null)
                    tupleList.Add(fsharpOption.get_Value());
            }
            Tuple<K[], OptionalValue<V>[]> tuple = (Tuple<K[], OptionalValue<V>[]>)ArrayModule.Unzip<K, OptionalValue<V>>((Tuple<M0, M1>[])tupleList.ToArray());
            OptionalValue<V>[] optionalValueArray = tuple.Item2;
            return new Series<K, V>(this.indexBuilder.Create<K>((IEnumerable<K>)tuple.Item1, (FSharpOption<bool>)null), this.vectorBuilder.CreateMissing<V>(optionalValueArray), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, V> Where(Func<KeyValuePair<K, V>, bool> f)
        {
            return this.Where(new Func<KeyValuePair<K, V>, int, bool>(new Series.Where<K, V>(f).Invoke));
        }

        public Series<K, V> WhereOptional(Func<KeyValuePair<K, OptionalValue<V>>, bool> f)
        {
            Tuple<K[], OptionalValue<V>[]> tuple = (Tuple<K[], OptionalValue<V>[]>)ArrayModule.Unzip<K, OptionalValue<V>>((Tuple<M0, M1>[])SeqModule.ToArray<Tuple<K, OptionalValue<V>>>((IEnumerable<M0>)new Series.WhereOptional<K, V>(this, f, new KeyValuePair<K, long>(), (Tuple<K, long>)null, default(K), new long(), new OptionalValue<V>(), (IEnumerator<KeyValuePair<K, long>>)null, 0, (Tuple<K, OptionalValue<V>>)null)));
            OptionalValue<V>[] optionalValueArray = tuple.Item2;
            return new Series<K, V>(this.indexBuilder.Create<K>((IEnumerable<K>)tuple.Item1, (FSharpOption<bool>)null), this.vectorBuilder.CreateMissing<V>(optionalValueArray), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, R> Select<R>(Func<KeyValuePair<K, V>, int, R> f)
        {
            return new Series<K, R>(this.indexBuilder.Project<K>(this.index), this.vector.Select<R>((Func<IVectorLocation, Func<OptionalValue<V>, OptionalValue<R>>>)new Series.newVector<V, R, K>(this, f)), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, R> Convert<R>(Func<V, R> forward, Func<R, V> backward)
        {
            return new Series<K, R>(this.indexBuilder.Project<K>(this.index), this.vector.Convert<R>((Func<V, R>)new Series.newVector<V, R>(forward), (Func<R, V>)new Series.newVector<V, R>(backward)), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, R> Select<R>(Func<KeyValuePair<K, V>, R> f)
        {
            return this.SelectOptional<R>(new Func<KeyValuePair<K, OptionalValue<V>>, OptionalValue<R>>(new Series.Select<K, V, R>(f).Invoke));
        }

        public Series<R, V> SelectKeys<R>(Func<KeyValuePair<K, OptionalValue<V>>, R> f)
        {
            IIndex<R> index = this.indexBuilder.Create<R>((IEnumerable<R>)SeqModule.ToArray<R>((IEnumerable<M0>)new Series.newKeys<R, K, V>(this, f, new KeyValuePair<K, long>(), (Tuple<K, long>)null, default(K), new long(), (IEnumerator<KeyValuePair<K, long>>)null, 0, default(R))), (FSharpOption<bool>)null);
            IVector<V> vector = this.vectorBuilder.Build<V>(index.AddressingScheme, VectorConstruction.NewReturn(0), new IVector<V>[1]
            {
        this.vector
            });
            return new Series<R, V>(index, vector, this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, R> SelectOptional<R>(Func<KeyValuePair<K, OptionalValue<V>>, OptionalValue<R>> f)
        {
            return new Series<K, R>(this.indexBuilder.Project<K>(this.index), this.vector.Select<R>((Func<IVectorLocation, Func<OptionalValue<V>, OptionalValue<R>>>)new Series.newVector<V, R, K>(this, f)), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, T> SelectValues<T>(Func<V, T> f)
        {
            return this.Select<T>(new Func<KeyValuePair<K, V>, T>(new Series.SelectValues<K, V, T>(f).Invoke));
        }

        [SpecialName]
        public static Series<K, a> op_Dollar<a>(Func<V, a> f, Series<K, V> series)
        {
            return series.SelectValues<a>(new Func<V, a>(new Series.op_Dollar<V, a>(f).Invoke));
        }

        public Series<K, V> Reversed
        {
            get
            {
                return new Series<K, V>(FIndexextensions.Index.ofKeys<K>((K[])ArrayModule.Reverse<K>(ArrayModule.OfSeq<K>((IEnumerable<M0>)this.index.Keys))), this.vectorBuilder.CreateMissing<V>((OptionalValue<V>[])ArrayModule.Reverse<OptionalValue<V>>(ArrayModule.OfSeq<OptionalValue<V>>((IEnumerable<M0>)FVectorextensionscore.IVector`1get_DataSequence<V>(this.vector)))), this.vectorBuilder, this.indexBuilder);
            }
        }

        public Series<K, S> ScanValues<S>(Func<S, V, S> foldFunc, S init)
        {
            return new Series<K, S>(this.index, this.vectorBuilder.CreateMissing<S>((OptionalValue<S>[])SeqModule.ToArray<OptionalValue<S>>((IEnumerable<M0>)new Series.newVector<S, K, V>(this, foldFunc, init, (FSharpRef<S>)null, new OptionalValue<V>(), (IEnumerator<OptionalValue<V>>)null, 0, new OptionalValue<S>()))), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, S> ScanAllValues<S>(Func<OptionalValue<S>, OptionalValue<V>, OptionalValue<S>> foldFunc, OptionalValue<S> init)
        {
            return new Series<K, S>(this.index, this.vectorBuilder.CreateMissing<S>((OptionalValue<S>[])SeqModule.ToArray<OptionalValue<S>>(SeqModule.Skip<OptionalValue<S>>(1, (IEnumerable<M0>)SeqModule.Scan<OptionalValue<V>, OptionalValue<S>>((Func<M1, Func<M0, M1>>)new Series.newVector<V, S>(foldFunc), (M1)init, (IEnumerable<M0>)FVectorextensionscore.IVector`1get_DataSequence<V>(this.vector))))), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, V> WithMissingFrom<a>(Series<K, a> otherSeries)
        {
            return new Series<K, V>(this.Index, this.vectorBuilder.CreateMissing<V>((OptionalValue<V>[])ArrayModule.OfSeq<OptionalValue<V>>((IEnumerable<M0>)SeqModule.Map<KeyValuePair<K, OptionalValue<V>>, OptionalValue<V>>((Func<M0, M1>)new Series.newVec<K, V, a>(otherSeries), (IEnumerable<M0>)this.ObservationsAll))), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, V> Merge(Series<K, V> otherSeries)
        {
            Tuple<IIndex<K>, VectorConstruction> tuple = this.indexBuilder.Merge<K>(FSharpList<Tuple<IIndex<K>, VectorConstruction>>.Cons(new Tuple<IIndex<K>, VectorConstruction>(this.index, VectorConstruction.NewReturn(0)), FSharpList<Tuple<IIndex<K>, VectorConstruction>>.Cons(new Tuple<IIndex<K>, VectorConstruction>(otherSeries.Index, VectorConstruction.NewReturn(1)), FSharpList<Tuple<IIndex<K>, VectorConstruction>>.get_Empty())), VectorHelpers.BinaryTransform.AtMostOne);
            IIndex<K> index = tuple.Item1;
            VectorConstruction vectorConstruction = tuple.Item2;
            IVector<V> vector = this.vectorBuilder.Build<V>(index.AddressingScheme, vectorConstruction, new IVector<V>[2]
            {
        this.Vector,
        otherSeries.Vector
            });
            return new Series<K, V>(index, vector, this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, V> Merge(IEnumerable<Series<K, V>> otherSeries)
        {
            return this.Merge((Series<K, V>[])ArrayModule.OfSeq<Series<K, V>>((IEnumerable<M0>)otherSeries));
        }

        public Series<K, V> Merge(params Series<K, V>[] otherSeries)
        {
            FSharpList<Tuple<IIndex<K>, VectorConstruction>> fsharpList = (FSharpList<Tuple<IIndex<K>, VectorConstruction>>)ListModule.OfSeq<Tuple<IIndex<K>, VectorConstruction>>((IEnumerable<M0>)ArrayModule.MapIndexed<Series<K, V>, Tuple<IIndex<K>, VectorConstruction>>((Func<int, Func<M0, M1>>)new Series.constrs<K, V>(), (M0[])otherSeries));
            Series<K, V>[] seriesArray1 = otherSeries;
            Func<Series<K, V>, IVector<V>> Func = (Func<Series<K, V>, IVector<V>>)new Series.vectors<K, V>();
            Series<K, V>[] seriesArray2 = seriesArray1;
            if ((object)seriesArray2 == null)
                throw new ArgumentNullException("array");
            IVector<V>[] vectorArray = new IVector<V>[seriesArray2.Length];
            for (int index = 0; index < vectorArray.Length; ++index)
                vectorArray[index] = Func.Invoke(seriesArray2[index]);
            IVector<V>[] vectors = vectorArray;
            Tuple<IIndex<K>, VectorConstruction> tuple = this.indexBuilder.Merge<K>(FSharpList<Tuple<IIndex<K>, VectorConstruction>>.Cons(new Tuple<IIndex<K>, VectorConstruction>(this.index, VectorConstruction.NewReturn(0)), fsharpList), VectorHelpers.BinaryTransform.AtMostOne);
            IIndex<K> index1 = tuple.Item1;
            VectorConstruction vectorConstruction = tuple.Item2;
            IVector<V> vector = this.vectorBuilder.Build<V>(index1.AddressingScheme, vectorConstruction, (IVector<V>[])SeqModule.ToArray<IVector<V>>((IEnumerable<M0>)new Series.newVector<V, K>(this, vectors, 0, (IVector<V>)null)));
            return new Series<K, V>(index1, vector, this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, V> Merge(Series<K, V> another, UnionBehavior behavior)
        {
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> tuple = this.indexBuilder.Union<K>(new Tuple<IIndex<K>, VectorConstruction>(this.Index, VectorConstruction.NewReturn(0)), new Tuple<IIndex<K>, VectorConstruction>(another.Index, VectorConstruction.NewReturn(1)));
            VectorConstruction vectorConstruction1 = tuple.Item3;
            VectorConstruction vectorConstruction2 = tuple.Item2;
            IIndex<K> index = tuple.Item1;
            VectorListTransform vectorListTransform1;
            switch (behavior)
            {
                case UnionBehavior.PreferRight:
                    vectorListTransform1 = VectorHelpers.BinaryTransform.RightIfAvailable;
                    break;
                case UnionBehavior.Exclusive:
                    vectorListTransform1 = VectorHelpers.BinaryTransform.AtMostOne;
                    break;
                default:
                    vectorListTransform1 = VectorHelpers.BinaryTransform.LeftIfAvailable;
                    break;
            }
            VectorListTransform vectorListTransform2 = vectorListTransform1;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.vecCmd<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), vectorListTransform2);
            IVector<V> vector = this.vectorBuilder.Build<V>(index.AddressingScheme, vectorConstruction3, new IVector<V>[2]
            {
                this.Vector,
                another.Vector
            });
            return new Series<K, V>(index, vector, this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, V> Replace(K[] keys, V value)
        {
            return this.Select<V>(new Func<KeyValuePair<K, V>, V>(new Series.Replace<K, V>(keys, value).Invoke));
        }

        public Series<K, V> Replace(K key, V value)
        {
            return this.Select<V>(new Func<KeyValuePair<K, V>, V>(new Series.Replace<K, V>(key, value).Invoke));
        }

        public Series<K, V> Intersect(Series<K, V> another)
        {
            IIndex<K> intersectIndex = this.indexBuilder.Intersect<K>(new Tuple<IIndex<K>, VectorConstruction>(this.Index, VectorConstruction.NewReturn(0)), new Tuple<IIndex<K>, VectorConstruction>(another.Index, VectorConstruction.NewReturn(1))).Item1;
            Tuple<K[], OptionalValue<V>[]> tuple = (Tuple<K[], OptionalValue<V>[]>)ArrayModule.Unzip<K, OptionalValue<V>>((Tuple<M0, M1>[])SeqModule.ToArray<Tuple<K, OptionalValue<V>>>((IEnumerable<M0>)new Series.Intersect<K, V>(this, another, intersectIndex, default(K), new OptionalValue<V>(), new OptionalValue<V>(), (IEnumerator<K>)null, 0, (Tuple<K, OptionalValue<V>>)null)));
            OptionalValue<V>[] optionalValueArray = tuple.Item2;
            return new Series<K, V>(this.indexBuilder.Create<K>((IEnumerable<K>)tuple.Item1, (FSharpOption<bool>)null), this.vectorBuilder.CreateMissing<V>(optionalValueArray), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, Diff<V>> Compare(Series<K, V> another)
        {
            IIndex<K> unionIndex = this.indexBuilder.Union<K>(new Tuple<IIndex<K>, VectorConstruction>(this.Index, VectorConstruction.NewReturn(0)), new Tuple<IIndex<K>, VectorConstruction>(another.Index, VectorConstruction.NewReturn(1))).Item1;
            Tuple<K[], Diff<V>[]> tuple = (Tuple<K[], Diff<V>[]>)ArrayModule.Unzip<K, Diff<V>>((Tuple<M0, M1>[])SeqModule.ToArray<Tuple<K, Diff<V>>>((IEnumerable<M0>)new Series.Compare<K, V>(this, another, unionIndex, default(K), new OptionalValue<V>(), new OptionalValue<V>(), (IEnumerator<K>)null, 0, (Tuple<K, Diff<V>>)null)));
            Diff<V>[] diffArray = tuple.Item2;
            return new Series<K, Diff<V>>(this.indexBuilder.Create<K>((IEnumerable<K>)tuple.Item1, (FSharpOption<bool>)null), this.vectorBuilder.Create<Diff<V>>(diffArray), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, Tuple<OptionalValue<V>, OptionalValue<V2>>> Zip<V2>(Series<K, V2> otherSeries)
        {
            return this.Zip<V2>(otherSeries, JoinKind.Outer, Lookup.Exact);
        }

        public Series<K, Tuple<OptionalValue<V>, OptionalValue<V2>>> Zip<V2>(Series<K, V2> otherSeries, JoinKind kind)
        {
            return this.Zip<V2>(otherSeries, kind, Lookup.Exact);
        }

        internal Tuple<IIndex<K>, IVector<V>, IVector<V2>> ZipHelper<V2>(Series<K, V2> otherSeries, JoinKind kind, Lookup lookup)
        {
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(this.indexBuilder, otherSeries.IndexBuilder, kind, lookup, this.index, otherSeries.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(0));
            VectorConstruction vectorConstruction1 = joinTransformation.Item2;
            VectorConstruction vectorConstruction2 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            IVector<V> vector1 = this.vectorBuilder.Build<V>(index.AddressingScheme, vectorConstruction1, new IVector<V>[1]
            {
        this.Vector
            });
            IVector<V2> vector2 = this.vectorBuilder.Build<V2>(index.AddressingScheme, vectorConstruction2, new IVector<V2>[1]
            {
        otherSeries.Vector
            });
            return new Tuple<IIndex<K>, IVector<V>, IVector<V2>>(index, vector1, vector2);
        }

        public Series<K, Tuple<OptionalValue<V>, OptionalValue<V2>>> Zip<V2>(Series<K, V2> otherSeries, JoinKind kind, Lookup lookup)
        {
            Tuple<IIndex<K>, IVector<V>, IVector<V2>> tuple = this.ZipHelper<V2>(otherSeries, kind, lookup);
            IVector<V2> vector1 = tuple.Item3;
            IIndex<K> index = tuple.Item1;
            IVector<V> vector2 = tuple.Item2;
            VectorConstruction vectorConstruction = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.vecRes<K>(index)), IEnumerable<VectorConstruction>.Cons(VectorConstruction.NewReturn(0), IEnumerable<VectorConstruction>.Cons(VectorConstruction.NewReturn(1), IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.vecRes<V, V2>((Func<Tuple<OptionalValue<V>, OptionalValue<V2>>, Func<Tuple<OptionalValue<V>, OptionalValue<V2>>, Tuple<OptionalValue<V>, OptionalValue<V2>>>>)new Series.vecRes<V, V2>())));
            IVector<Tuple<OptionalValue<V>, OptionalValue<V2>>>[] vectorArray = new IVector<Tuple<OptionalValue<V>, OptionalValue<V2>>>[2]
            {
        vector2.Select<Tuple<OptionalValue<V>, OptionalValue<V2>>>((Func<IVectorLocation, Func<OptionalValue<V>, OptionalValue<Tuple<OptionalValue<V>, OptionalValue<V2>>>>>) new Series.args<V, V2>()),
        vector1.Select<Tuple<OptionalValue<V>, OptionalValue<V2>>>((Func<IVectorLocation, Func<OptionalValue<V2>, OptionalValue<Tuple<OptionalValue<V>, OptionalValue<V2>>>>>) new Series.args<V, V2>())
            };
            IVector<Tuple<OptionalValue<V>, OptionalValue<V2>>> vector3 = this.vectorBuilder.Build<Tuple<OptionalValue<V>, OptionalValue<V2>>>(index.AddressingScheme, vectorConstruction, vectorArray);
            return new Series<K, Tuple<OptionalValue<V>, OptionalValue<V2>>>(index, vector3, this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, Tuple<V, V2>> ZipInner<V2>(Series<K, V2> otherSeries)
        {
            Tuple<IIndex<K>, IVector<V>, IVector<V2>> tuple = this.ZipHelper<V2>(otherSeries, JoinKind.Inner, Lookup.Exact);
            IVector<V2> x1 = tuple.Item3;
            IIndex<K> index = tuple.Item1;
            IVector<V> x2 = tuple.Item2;
            VectorConstruction vectorConstruction = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.vecRes<K>(index)), IEnumerable<VectorConstruction>.Cons(VectorConstruction.NewReturn(0), IEnumerable<VectorConstruction>.Cons(VectorConstruction.NewReturn(1), IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.vecRes<V, V2>((Func<FSharpChoice<V, V2, Tuple<V, V2>>, Func<FSharpChoice<V, V2, Tuple<V, V2>>, FSharpChoice<V, V2, Tuple<V, V2>>>>)new Series.vecRes<V, V2>())));
            IVector<Tuple<V, V2>> vector = FVectorextensionscore.IVector`1Select<FSharpChoice<V, V2, Tuple<V, V2>>, Tuple<V, V2>>(this.vectorBuilder.Build<FSharpChoice<V, V2, Tuple<V, V2>>>(index.AddressingScheme, vectorConstruction, new IVector<FSharpChoice<V, V2, Tuple<V, V2>>>[2]
            {
        FVectorextensionscore.IVector`1Select<V, FSharpChoice<V, V2, Tuple<V, V2>>>(x2, (Func<V, FSharpChoice<V, V2, Tuple<V, V2>>>) new Series.zipV<V, V2>()),
        FVectorextensionscore.IVector`1Select<V2, FSharpChoice<V, V2, Tuple<V, V2>>>(x1, (Func<V2, FSharpChoice<V, V2, Tuple<V, V2>>>) new Series.zipV<V, V2>())
            }), (Func<FSharpChoice<V, V2, Tuple<V, V2>>, Tuple<V, V2>>)new Series.zipV<V, V2>());
            return new Series<K, Tuple<V, V2>>(index, vector, this.vectorBuilder, this.indexBuilder);
        }

        public Series<TNewKey, R> Resample<TNewKey, R>(IEnumerable<K> keys, Direction direction, Func<TNewKey, Series<K, V>, R> valueSelector, Func<K, Series<K, V>, TNewKey> keySelector)
        {
            Tuple<IIndex<TNewKey>, IVector<R>> tuple = this.indexBuilder.Resample<K, TNewKey, R>(this.indexBuilder, this.Index, keys, direction, VectorConstruction.NewReturn(0), (Func<Tuple<K, Tuple<IIndex<K>, VectorConstruction>>, Tuple<TNewKey, OptionalValue<R>>>)new Series.Resample<K, TNewKey, R, V>(this, valueSelector, keySelector));
            IVector<R> vector = tuple.Item2;
            return new Series<TNewKey, R>(tuple.Item1, vector, this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, a> Resample<a>(IEnumerable<K> keys, Direction direction, Func<K, Series<K, V>, a> valueSelector)
        {
            return this.Resample<K, a>(keys, direction, valueSelector, new Func<K, Series<K, V>, K>(new Series.Resample<K, V>().Invoke));
        }

        public Series<K, Series<K, V>> Resample(IEnumerable<K> keys, Direction direction)
        {
            return this.Resample<K, Series<K, V>>(keys, direction, new Func<K, Series<K, V>, Series<K, V>>(new Series.Resample<K, V>().Invoke), new Func<K, Series<K, V>, K>(new Series.Resample<K, V>().Invoke));
        }

        public Series<K, DataSegment<Tuple<V, V>>> Pairwise(Boundary boundary)
        {
            Direction dir = boundary != Boundary.AtEnding ? Direction.Backward : Direction.Forward;
            Tuple<IIndex<K>, IVector<DataSegment<Tuple<V, V>>>> tuple = this.indexBuilder.Aggregate<K, K, DataSegment<Tuple<V, V>>>(this.Index, Aggregation<K>.NewWindowSize(2, boundary), VectorConstruction.NewReturn(0), (Func<Tuple<DataSegmentKind, Tuple<IIndex<K>, VectorConstruction>>, Tuple<K, OptionalValue<DataSegment<Tuple<V, V>>>>>)new Series.Pairwise<K, V>(this, dir));
            IVector<DataSegment<Tuple<V, V>>> vector = tuple.Item2;
            return new Series<K, DataSegment<Tuple<V, V>>>(tuple.Item1, vector, this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, Tuple<V, V>> Pairwise()
        {
            return this.Pairwise(Boundary.Skip).Select<Tuple<V, V>>(new Func<KeyValuePair<K, DataSegment<Tuple<V, V>>>, Tuple<V, V>>(new Series.Pairwise<K, V>().Invoke));
        }

        public Series<TNewKey, R> Aggregate<TNewKey, R>(Aggregation<K> aggregation, Func<DataSegment<Series<K, V>>, TNewKey> keySelector, Func<DataSegment<Series<K, V>>, OptionalValue<R>> valueSelector)
        {
            Tuple<IIndex<TNewKey>, IVector<R>> tuple = this.indexBuilder.Aggregate<K, TNewKey, R>(this.Index, aggregation, VectorConstruction.NewReturn(0), (Func<Tuple<DataSegmentKind, Tuple<IIndex<K>, VectorConstruction>>, Tuple<TNewKey, OptionalValue<R>>>)new Series.Aggregate<K, TNewKey, R, V>(this, keySelector, valueSelector));
            IVector<R> vector = tuple.Item2;
            return new Series<TNewKey, R>(tuple.Item1, vector, this.vectorBuilder, this.indexBuilder);
        }

        public Series<TNewKey, R> Aggregate<TNewKey, R>(Aggregation<K> aggregation, Func<DataSegment<Series<K, V>>, KeyValuePair<TNewKey, OptionalValue<R>>> observationSelector)
        {
            Tuple<IIndex<TNewKey>, IVector<R>> tuple = this.indexBuilder.Aggregate<K, TNewKey, R>(this.Index, aggregation, VectorConstruction.NewReturn(0), (Func<Tuple<DataSegmentKind, Tuple<IIndex<K>, VectorConstruction>>, Tuple<TNewKey, OptionalValue<R>>>)new Series.Aggregate<K, TNewKey, R, V>(this, observationSelector));
            IVector<R> vector = tuple.Item2;
            return new Series<TNewKey, R>(tuple.Item1, vector, this.vectorBuilder, this.indexBuilder);
        }

        public Series<TNewKey, Series<K, V>> GroupBy<TNewKey>(Func<KeyValuePair<K, V>, TNewKey> keySelector)
        {
            ReadOnlyCollection<Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>> readOnlyCollection1 = this.indexBuilder.GroupBy<K, TNewKey>(this.Index, (Func<K, OptionalValue<TNewKey>>)new Series.ks<K, TNewKey, V>(this, keySelector), VectorConstruction.NewReturn(0));
            ReadOnlyCollection<Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>> readOnlyCollection2 = readOnlyCollection1;
            Func<Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>, TNewKey> Func = (Func<Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>, TNewKey>)new Series.newIndex<K, TNewKey>();
            ReadOnlyCollection<Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>> readOnlyCollection3 = readOnlyCollection2;
            TNewKey[] array = ArrayModule.ZeroCreate<TNewKey>(readOnlyCollection3.Count);
            int index1 = 0;
            int num = readOnlyCollection3.Count - 1;
            if (num >= index1)
            {
                do
                {
                    array[index1] = Func.Invoke(readOnlyCollection3[index1]);
                    ++index1;
                }
                while (index1 != num + 1);
            }
            IIndex<TNewKey> index2 = FIndexextensions.Index.ofKeys<TNewKey>(System.Array.AsReadOnly<TNewKey>(array));
            IEnumerable<Series<K, V>> series = (IEnumerable<Series<K, V>>)SeqModule.Map<Tuple<IIndex<K>, VectorConstruction>, Series<K, V>>((Func<M0, M1>)new Series.newGroups<K, V, TNewKey>(this, index2), (IEnumerable<M0>)SeqModule.Map<Tuple<TNewKey, Tuple<IIndex<K>, VectorConstruction>>, Tuple<IIndex<K>, VectorConstruction>>((Func<M0, M1>)new Series.newGroups<K, TNewKey>(), (IEnumerable<M0>)readOnlyCollection1));
            return new Series<TNewKey, Series<K, V>>(index2, FVectorBuilderimplementation.VectorBuilder.Instance.Create<Series<K, V>>((Series<K, V>[])ArrayModule.OfSeq<Series<K, V>>((IEnumerable<M0>)series)), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, V> Interpolate(IEnumerable<K> keys, Func<K, OptionalValue<KeyValuePair<K, V>>, OptionalValue<KeyValuePair<K, V>>, V> f)
        {
            V[] array = (V[])SeqModule.ToArray<V>((IEnumerable<M0>)new Series.newObs<V, K>(this, keys, f, default(K), new OptionalValue<KeyValuePair<K, V>>(), new OptionalValue<KeyValuePair<K, V>>(), (IEnumerator<K>)null, 0, default(V)));
            return new Series<K, V>(FIndexextensions.Index.ofKeys<K>(System.Array.AsReadOnly<K>((K[])ArrayModule.OfSeq<K>((IEnumerable<M0>)keys))), FVectorBuilderimplementation.VectorBuilder.Instance.Create<V>(array), this.vectorBuilder, this.indexBuilder);
        }

        public Series<K, V> Realign(IEnumerable<K> newKeys)
        {
            FSharpTypeFunc fsharpTypeFunc = (FSharpTypeFunc)new Series.findAll<K, V>(this, newKeys);
            return new Series<K, V>(FIndexextensions.Index.ofKeys<K>(System.Array.AsReadOnly<K>((K[])ArrayModule.OfSeq<K>((IEnumerable<M0>)newKeys))), FVectorBuilderimplementation.VectorBuilder.Instance.CreateMissing<V>((OptionalValue<V>[])ArrayModule.OfSeq<OptionalValue<V>>((IEnumerable<M0>)((Func<Func<long, OptionalValue<V>>, IEnumerable<OptionalValue<V>>>)fsharpTypeFunc.Specialize<V>()).Invoke((Func<long, OptionalValue<V>>)new Series.newVector<V>(this.Vector)))), this.vectorBuilder, this.indexBuilder);
        }

        public Series<int, V> IndexOrdinally()
        {
            IIndex<int> index = this.indexBuilder.Create<int>((IEnumerable<int>)SeqModule.MapIndexed<K, int>((Func<int, Func<M0, M1>>)new Series.newIndex<K>(), (IEnumerable<M0>)this.Index.Keys), FSharpOption<bool>.Some(true));
            IVector<V> vector = this.vectorBuilder.Build<V>(index.AddressingScheme, VectorConstruction.NewReturn(0), new IVector<V>[1]
            {
        this.vector
            });
            return new Series<int, V>(index, vector, this.vectorBuilder, this.indexBuilder);
        }

        public Series<TNewKey, V> IndexWith<TNewKey>(IEnumerable<TNewKey> keys)
        {
            IIndex<TNewKey> index = this.indexBuilder.Create<TNewKey>(keys, (FSharpOption<bool>)null);
            VectorConstruction vectorConstruction = index.KeyCount != (long)this.KeyCount ? (index.KeyCount <= (long)this.KeyCount ? VectorConstruction.NewGetRange(VectorConstruction.NewReturn(0), RangeRestriction<long>.NewFixed(this.Index.AddressAt(0L), this.Index.AddressAt(index.KeyCount - 1L))) : VectorConstruction.NewAppend(VectorConstruction.NewReturn(0), VectorConstruction.NewEmpty(index.KeyCount - (long)this.KeyCount))) : VectorConstruction.NewReturn(0);
            IVector<V> vector = this.vectorBuilder.Build<V>(index.AddressingScheme, vectorConstruction, new IVector<V>[1]
            {
        this.vector
            });
            return new Series<TNewKey, V>(index, vector, this.vectorBuilder, this.indexBuilder);
        }

        public FSharpAsync<Series<K, V>> AsyncMaterialize()
        {
            FSharpAsyncBuilder defaultAsyncBuilder = ExtraTopLevelOperators.get_DefaultAsyncBuilder();
            return (FSharpAsync<Series<K, V>>)defaultAsyncBuilder.Delay<Series<K, V>>((Func<Unit, FSharpAsync<M0>>)new Series.AsyncMaterialize<K, V>(this, defaultAsyncBuilder));
        }

        public Task<Series<K, V>> MaterializeAsync()
        {
            return (Task<Series<K, V>>)FSharpAsync.StartAsTask<Series<K, V>>((FSharpAsync<M0>)this.AsyncMaterialize(), (FSharpOption<TaskCreationOptions>)null, (FSharpOption<CancellationToken>)null);
        }

        public Series<K, V> Materialize()
        {
            Tuple<FSharpAsync<IIndex<K>>, VectorConstruction> tuple = this.indexBuilder.AsyncMaterialize<K>(new Tuple<IIndex<K>, VectorConstruction>(this.index, VectorConstruction.NewReturn(0)));
            FSharpAsync<IIndex<K>> fsharpAsync = tuple.Item1;
            VectorConstruction vectorConstruction = tuple.Item2;
            IIndex<K> index = (IIndex<K>)FSharpAsync.RunSynchronously<IIndex<K>>((FSharpAsync<M0>)fsharpAsync, (FSharpOption<int>)null, (FSharpOption<CancellationToken>)null);
            IVector<V> vector = this.vectorBuilder.Build<V>(index.AddressingScheme, vectorConstruction, new IVector<V>[1]
            {
        this.vector
            });
            return new Series<K, V>(index, vector, this.vectorBuilder, index.Builder);
        }

        internal static Series<K, T2> UnaryGenericOperation<K, T1, T2>(Series<K, T1> series, Func<T1, T2> op)
        {
            return series.Select<T2>(new Func<KeyValuePair<K, T1>, T2>(new Series.UnaryGenericOperation<K, T1, T2>(op).Invoke));
        }

        internal static Series<K, T> UnaryOperation<T>(Series<K, T> series, Func<T, T> op)
        {
            return series.Select<T>(new Func<KeyValuePair<K, T>, T>(new Series.UnaryOperation<K, T>(op).Invoke));
        }

        internal static Series<K, T> ScalarOperationL<T>(Series<K, T> series, T scalar, Func<T, Func<T, T>> op)
        {
            return series.Select<T>(new Func<KeyValuePair<K, T>, T>(new Series.ScalarOperationL<K, T>(scalar, op).Invoke));
        }

        internal static Series<K, T> ScalarOperationR<T>(T scalar, Series<K, T> series, Func<T, Func<T, T>> op)
        {
            return series.Select<T>(new Func<KeyValuePair<K, T>, T>(new Series.ScalarOperationR<K, T>(scalar, op).Invoke));
        }

        internal static Series<K, T> VectorOperation<T>(Series<K, T> series1, Series<K, T> series2, Func<T, Func<T, T>> op)
        {
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));

            VectorConstruction vectorConstruction1 = joinTransformation.Item3;

            IIndex<K> index = joinTransformation.Item1;

            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.vecRes<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.vecRes<T>(op)));

            IVector<T> vector = series1.VectorBuilder.Build<T>(index.AddressingScheme, vectorConstruction3, new IVector<T>[2]
            {
                series1.Vector,
                series2.Vector
            });

            return new Series<K, T>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, double> operator -(Series<K, double> series)
        {
            return series.Select<double>(new Func<KeyValuePair<K, double>, double>(new Series.op_UnaryNegation<K>((Func<double, double>)new Series.op_UnaryNegation()).Invoke));
        }

        public static Series<K, Decimal> operator -(Series<K, Decimal> series)
        {
            return series.Select<Decimal>(new Func<KeyValuePair<K, Decimal>, Decimal>(new Series.op_UnaryNegation<K>((Func<Decimal, Decimal>)new Series.op_UnaryNegation()).Invoke));
        }

        public static Series<K, int> operator -(Series<K, int> series)
        {
            return series.Select<int>(new Func<KeyValuePair<K, int>, int>(new Series.op_UnaryNegation<K>((Func<int, int>)new Series.op_UnaryNegation()).Invoke));
        }

        public static Series<K, int> operator +(int scalar, Series<K, int> series)
        {
            int scalar1 = scalar;
            Series<K, int> series1 = series;
            Func<int, Func<int, int>> op = (Func<int, Func<int, int>>)new Series.op_Addition();
            return series1.Select<int>(new Func<KeyValuePair<K, int>, int>(new Series.op_Addition<K>(scalar1, op).Invoke));
        }

        public static Series<K, int> operator +(Series<K, int> series, int scalar)
        {
            return series.Select<int>(new Func<KeyValuePair<K, int>, int>(new Series.op_Addition<K>(scalar, (Func<int, Func<int, int>>)new Series.op_Addition()).Invoke));
        }

        public static Series<K, int> operator -(int scalar, Series<K, int> series)
        {
            int scalar1 = scalar;
            Series<K, int> series1 = series;
            Func<int, Func<int, int>> op = (Func<int, Func<int, int>>)new Series.op_Subtraction();
            return series1.Select<int>(new Func<KeyValuePair<K, int>, int>(new Series.op_Subtraction<K>(scalar1, op).Invoke));
        }

        public static Series<K, int> operator -(Series<K, int> series, int scalar)
        {
            return series.Select<int>(new Func<KeyValuePair<K, int>, int>(new Series.op_Subtraction<K>(scalar, (Func<int, Func<int, int>>)new Series.op_Subtraction()).Invoke));
        }

        public static Series<K, int> operator *(int scalar, Series<K, int> series)
        {
            int scalar1 = scalar;
            Series<K, int> series1 = series;
            Func<int, Func<int, int>> op = (Func<int, Func<int, int>>)new Series.op_Multiply();
            return series1.Select<int>(new Func<KeyValuePair<K, int>, int>(new Series.op_Multiply<K>(scalar1, op).Invoke));
        }

        public static Series<K, int> operator *(Series<K, int> series, int scalar)
        {
            return series.Select<int>(new Func<KeyValuePair<K, int>, int>(new Series.op_Multiply<K>(scalar, (Func<int, Func<int, int>>)new Series.op_Multiply()).Invoke));
        }

        public static Series<K, int> operator /(int scalar, Series<K, int> series)
        {
            int scalar1 = scalar;
            Series<K, int> series1 = series;
            Func<int, Func<int, int>> op = (Func<int, Func<int, int>>)new Series.op_Division();
            return series1.Select<int>(new Func<KeyValuePair<K, int>, int>(new Series.op_Division<K>(scalar1, op).Invoke));
        }

        public static Series<K, int> operator /(Series<K, int> series, int scalar)
        {
            return series.Select<int>(new Func<KeyValuePair<K, int>, int>(new Series.op_Division<K>(scalar, (Func<int, Func<int, int>>)new Series.op_Division()).Invoke));
        }

        public static Series<K, double> operator +(double scalar, Series<K, double> series)
        {
            double scalar1 = scalar;
            Series<K, double> series1 = series;
            Func<double, Func<double, double>> op = (Func<double, Func<double, double>>)new Series.op_Addition();
            return series1.Select<double>(new Func<KeyValuePair<K, double>, double>(new Series.op_Addition<K>(scalar1, op).Invoke));
        }

        public static Series<K, double> operator +(Series<K, double> series, double scalar)
        {
            return series.Select<double>(new Func<KeyValuePair<K, double>, double>(new Series.op_Addition<K>(scalar, (Func<double, Func<double, double>>)new Series.op_Addition()).Invoke));
        }

        public static Series<K, double> operator -(double scalar, Series<K, double> series)
        {
            double scalar1 = scalar;
            Series<K, double> series1 = series;
            Func<double, Func<double, double>> op = (Func<double, Func<double, double>>)new Series.op_Subtraction();
            return series1.Select<double>(new Func<KeyValuePair<K, double>, double>(new Series.op_Subtraction<K>(scalar1, op).Invoke));
        }

        public static Series<K, double> operator -(Series<K, double> series, double scalar)
        {
            return series.Select<double>(new Func<KeyValuePair<K, double>, double>(new Series.op_Subtraction<K>(scalar, (Func<double, Func<double, double>>)new Series.op_Subtraction()).Invoke));
        }

        public static Series<K, double> operator *(double scalar, Series<K, double> series)
        {
            double scalar1 = scalar;
            Series<K, double> series1 = series;
            Func<double, Func<double, double>> op = (Func<double, Func<double, double>>)new Series.op_Multiply();
            return series1.Select<double>(new Func<KeyValuePair<K, double>, double>(new Series.op_Multiply<K>(scalar1, op).Invoke));
        }

        public static Series<K, double> operator *(Series<K, double> series, double scalar)
        {
            return series.Select<double>(new Func<KeyValuePair<K, double>, double>(new Series.op_Multiply<K>(scalar, (Func<double, Func<double, double>>)new Series.op_Multiply()).Invoke));
        }

        public static Series<K, double> operator /(double scalar, Series<K, double> series)
        {
            double scalar1 = scalar;
            Series<K, double> series1 = series;
            Func<double, Func<double, double>> op = (Func<double, Func<double, double>>)new Series.op_Division();
            return series1.Select<double>(new Func<KeyValuePair<K, double>, double>(new Series.op_Division<K>(scalar1, op).Invoke));
        }

        public static Series<K, double> operator /(Series<K, double> series, double scalar)
        {
            return series.Select<double>(new Func<KeyValuePair<K, double>, double>(new Series.op_Division<K>(scalar, (Func<double, Func<double, double>>)new Series.op_Division()).Invoke));
        }

        public static Series<K, double> Pow(double scalar, Series<K, double> series)
        {
            double scalar1 = scalar;
            Series<K, double> series1 = series;
            Func<double, Func<double, double>> op = (Func<double, Func<double, double>>)new Series.Pow();
            return series1.Select<double>(new Func<KeyValuePair<K, double>, double>(new Series.Pow<K>(scalar1, op).Invoke));
        }

        public static Series<K, double> Pow(Series<K, double> series, double scalar)
        {
            return series.Select<double>(new Func<KeyValuePair<K, double>, double>(new Series.Pow<K>(scalar, (Func<double, Func<double, double>>)new Series.Pow()).Invoke));
        }

        public static Series<K, Decimal> operator +(Decimal scalar, Series<K, Decimal> series)
        {
            Decimal scalar1 = scalar;
            Series<K, Decimal> series1 = series;
            Func<Decimal, Func<Decimal, Decimal>> op = (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Addition();
            return series1.Select<Decimal>(new Func<KeyValuePair<K, Decimal>, Decimal>(new Series.op_Addition<K>(scalar1, op).Invoke));
        }

        public static Series<K, Decimal> operator +(Series<K, Decimal> series, Decimal scalar)
        {
            return series.Select<Decimal>(new Func<KeyValuePair<K, Decimal>, Decimal>(new Series.op_Addition<K>(scalar, (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Addition()).Invoke));
        }

        public static Series<K, Decimal> operator -(Decimal scalar, Series<K, Decimal> series)
        {
            Decimal scalar1 = scalar;
            Series<K, Decimal> series1 = series;
            Func<Decimal, Func<Decimal, Decimal>> op = (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Subtraction();
            return series1.Select<Decimal>(new Func<KeyValuePair<K, Decimal>, Decimal>(new Series.op_Subtraction<K>(scalar1, op).Invoke));
        }

        public static Series<K, Decimal> operator -(Series<K, Decimal> series, Decimal scalar)
        {
            return series.Select<Decimal>(new Func<KeyValuePair<K, Decimal>, Decimal>(new Series.op_Subtraction<K>(scalar, (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Subtraction()).Invoke));
        }

        public static Series<K, Decimal> operator *(Decimal scalar, Series<K, Decimal> series)
        {
            Decimal scalar1 = scalar;
            Series<K, Decimal> series1 = series;
            Func<Decimal, Func<Decimal, Decimal>> op = (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Multiply();
            return series1.Select<Decimal>(new Func<KeyValuePair<K, Decimal>, Decimal>(new Series.op_Multiply<K>(scalar1, op).Invoke));
        }

        public static Series<K, Decimal> operator *(Series<K, Decimal> series, Decimal scalar)
        {
            return series.Select<Decimal>(new Func<KeyValuePair<K, Decimal>, Decimal>(new Series.op_Multiply<K>(scalar, (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Multiply()).Invoke));
        }

        public static Series<K, Decimal> operator /(Decimal scalar, Series<K, Decimal> series)
        {
            Decimal scalar1 = scalar;
            Series<K, Decimal> series1 = series;
            Func<Decimal, Func<Decimal, Decimal>> op = (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Division();
            return series1.Select<Decimal>(new Func<KeyValuePair<K, Decimal>, Decimal>(new Series.op_Division<K>(scalar1, op).Invoke));
        }

        public static Series<K, Decimal> operator /(Series<K, Decimal> series, Decimal scalar)
        {
            return series.Select<Decimal>(new Func<KeyValuePair<K, Decimal>, Decimal>(new Series.op_Division<K>(scalar, (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Division()).Invoke));
        }

        public static Series<K, int> operator +(Series<K, int> s1, Series<K, int> s2)
        {
            Series<K, int> series1 = s1;
            Series<K, int> series2 = s2;
            Func<int, Func<int, int>> operation = (Func<int, Func<int, int>>)new Series.op_Addition();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Addition<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Addition(operation)));
            IVector<int> vector = series1.VectorBuilder.Build<int>(index.AddressingScheme, vectorConstruction3, new IVector<int>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, int>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, int> operator -(Series<K, int> s1, Series<K, int> s2)
        {
            Series<K, int> series1 = s1;
            Series<K, int> series2 = s2;
            Func<int, Func<int, int>> operation = (Func<int, Func<int, int>>)new Series.op_Subtraction();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Subtraction<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Subtraction(operation)));
            IVector<int> vector = series1.VectorBuilder.Build<int>(index.AddressingScheme, vectorConstruction3, new IVector<int>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, int>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, int> operator *(Series<K, int> s1, Series<K, int> s2)
        {
            Series<K, int> series1 = s1;
            Series<K, int> series2 = s2;
            Func<int, Func<int, int>> operation = (Func<int, Func<int, int>>)new Series.op_Multiply();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Multiply<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Multiply(operation)));
            IVector<int> vector = series1.VectorBuilder.Build<int>(index.AddressingScheme, vectorConstruction3, new IVector<int>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, int>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, int> operator /(Series<K, int> s1, Series<K, int> s2)
        {
            Series<K, int> series1 = s1;
            Series<K, int> series2 = s2;
            Func<int, Func<int, int>> operation = (Func<int, Func<int, int>>)new Series.op_Division();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Division<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Division(operation)));
            IVector<int> vector = series1.VectorBuilder.Build<int>(index.AddressingScheme, vectorConstruction3, new IVector<int>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, int>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, double> operator +(Series<K, double> s1, Series<K, double> s2)
        {
            Series<K, double> series1 = s1;
            Series<K, double> series2 = s2;
            Func<double, Func<double, double>> operation = (Func<double, Func<double, double>>)new Series.op_Addition();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Addition<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Addition(operation)));
            IVector<double> vector = series1.VectorBuilder.Build<double>(index.AddressingScheme, vectorConstruction3, new IVector<double>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, double>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, double> operator -(Series<K, double> s1, Series<K, double> s2)
        {
            Series<K, double> series1 = s1;
            Series<K, double> series2 = s2;
            Func<double, Func<double, double>> operation = (Func<double, Func<double, double>>)new Series.op_Subtraction();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Subtraction<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Subtraction(operation)));
            IVector<double> vector = series1.VectorBuilder.Build<double>(index.AddressingScheme, vectorConstruction3, new IVector<double>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, double>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, double> operator *(Series<K, double> s1, Series<K, double> s2)
        {
            Series<K, double> series1 = s1;
            Series<K, double> series2 = s2;
            Func<double, Func<double, double>> operation = (Func<double, Func<double, double>>)new Series.op_Multiply();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Multiply<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Multiply(operation)));
            IVector<double> vector = series1.VectorBuilder.Build<double>(index.AddressingScheme, vectorConstruction3, new IVector<double>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, double>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, double> operator /(Series<K, double> s1, Series<K, double> s2)
        {
            Series<K, double> series1 = s1;
            Series<K, double> series2 = s2;
            Func<double, Func<double, double>> operation = (Func<double, Func<double, double>>)new Series.op_Division();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Division<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Division(operation)));
            IVector<double> vector = series1.VectorBuilder.Build<double>(index.AddressingScheme, vectorConstruction3, new IVector<double>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, double>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, double> Pow(Series<K, double> s1, Series<K, double> s2)
        {
            Series<K, double> series1 = s1;
            Series<K, double> series2 = s2;
            Func<double, Func<double, double>> operation = (Func<double, Func<double, double>>)new Series.Pow();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.Pow<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.Pow(operation)));
            IVector<double> vector = series1.VectorBuilder.Build<double>(index.AddressingScheme, vectorConstruction3, new IVector<double>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, double>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, Decimal> operator +(Series<K, Decimal> s1, Series<K, Decimal> s2)
        {
            Series<K, Decimal> series1 = s1;
            Series<K, Decimal> series2 = s2;
            Func<Decimal, Func<Decimal, Decimal>> operation = (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Addition();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Addition<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Addition(operation)));
            IVector<Decimal> vector = series1.VectorBuilder.Build<Decimal>(index.AddressingScheme, vectorConstruction3, new IVector<Decimal>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, Decimal>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, Decimal> operator -(Series<K, Decimal> s1, Series<K, Decimal> s2)
        {
            Series<K, Decimal> series1 = s1;
            Series<K, Decimal> series2 = s2;
            Func<Decimal, Func<Decimal, Decimal>> operation = (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Subtraction();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Subtraction<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Subtraction(operation)));
            IVector<Decimal> vector = series1.VectorBuilder.Build<Decimal>(index.AddressingScheme, vectorConstruction3, new IVector<Decimal>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, Decimal>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, Decimal> operator *(Series<K, Decimal> s1, Series<K, Decimal> s2)
        {
            Series<K, Decimal> series1 = s1;
            Series<K, Decimal> series2 = s2;
            Func<Decimal, Func<Decimal, Decimal>> operation = (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Multiply();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Multiply<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Multiply(operation)));
            IVector<Decimal> vector = series1.VectorBuilder.Build<Decimal>(index.AddressingScheme, vectorConstruction3, new IVector<Decimal>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, Decimal>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }

        public static Series<K, Decimal> operator /(Series<K, Decimal> s1, Series<K, Decimal> s2)
        {
            Series<K, Decimal> series1 = s1;
            Series<K, Decimal> series2 = s2;
            Func<Decimal, Func<Decimal, Decimal>> operation = (Func<Decimal, Func<Decimal, Decimal>>)new Series.op_Division();
            Tuple<IIndex<K>, VectorConstruction, VectorConstruction> joinTransformation = JoinHelpers.createJoinTransformation<K>(series1.IndexBuilder, series2.IndexBuilder, JoinKind.Outer, Lookup.Exact, series1.Index, series2.Index, VectorConstruction.NewReturn(0), VectorConstruction.NewReturn(1));
            VectorConstruction vectorConstruction1 = joinTransformation.Item3;
            IIndex<K> index = joinTransformation.Item1;
            VectorConstruction vectorConstruction2 = joinTransformation.Item2;
            VectorConstruction vectorConstruction3 = VectorConstruction.NewCombine((Lazy<long>)LazyExtensions.Create<long>((Func<Unit, M0>)new Series.op_Division<K>(index)), IEnumerable<VectorConstruction>.Cons(vectorConstruction2, IEnumerable<VectorConstruction>.Cons(vectorConstruction1, IEnumerable<VectorConstruction>.get_Empty())), VectorListTransform.NewBinary((IBinaryTransform)new Series.op_Division(operation)));
            IVector<Decimal> vector = series1.VectorBuilder.Build<Decimal>(index.AddressingScheme, vectorConstruction3, new IVector<Decimal>[2]
            {
        series1.Vector,
        series2.Vector
            });
            return new Series<K, Decimal>(index, vector, series1.VectorBuilder, series1.IndexBuilder);
        }


        public override bool Equals(object another)
        {
            object obj = another;
            if (obj == null)
                return false;
            Series<K, V> series1 = obj as Series<K, V>;
            if (series1 == null)
                return false;
            Series<K, V> series2 = series1;
            if (this.Index.Equals((object)series2.Index))
                return this.Vector.Equals((object)series2.Vector);
            return false;
        }

        public override int GetHashCode()
        {
            return (int)Func<int, int>.InvokeFast<int>((Func<int, Func<int, M0>>)new Series.combine(), this.Index.GetHashCode(), this.Vector.GetHashCode());
        }

        internal IEnumerable<FSharpChoice<Tuple<K, OptionalValue<V>>, Unit, a>> GetPrintedObservations<a>(int startCount, int endCount)
        {
            if (SeqModule.IsEmpty<KeyValuePair<K, long>>((IEnumerable<M0>)Seq.skipAtMost<KeyValuePair<K, long>>(startCount + endCount, this.Index.Mappings)))
                return (IEnumerable<FSharpChoice<Tuple<K, OptionalValue<V>>, Unit, a>>)new Series.GetPrintedObservations<K, V, a>(this, new KeyValuePair<K, OptionalValue<V>>(), (IEnumerator<KeyValuePair<K, OptionalValue<V>>>)null, 0, (FSharpChoice<Tuple<K, OptionalValue<V>>, Unit, a>)null);
            return (IEnumerable<FSharpChoice<Tuple<K, OptionalValue<V>>, Unit, a>>)new Series.GetPrintedObservations<K, V, a>(this.GetAddressRange(RangeRestriction<long>.NewStart((long)startCount)), this.GetAddressRange(RangeRestriction<long>.NewEnd((long)endCount)), new KeyValuePair<K, OptionalValue<V>>(), new KeyValuePair<K, OptionalValue<V>>(), (IEnumerator<KeyValuePair<K, OptionalValue<V>>>)null, (IEnumerator<KeyValuePair<K, OptionalValue<V>>>)null, 0, (FSharpChoice<Tuple<K, OptionalValue<V>>, Unit, a>)null);
        }

        public override string ToString()
        {
            if (this.vector.SuppressPrinting)
                return "(Suppressed)";
            return ((Func<string, string>)ExtraTopLevelOperators.PrintFormatToString<Func<string, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<string, string>, Unit, string, string, string>("series [ %s]"))).Invoke(StringModule.Concat("; ", (IEnumerable<string>)SeqModule.Map<FSharpChoice<Tuple<K, OptionalValue<V>>, Unit, Tuple<K, OptionalValue<V>>>, string>((Func<M0, M1>)new Series.ToString<K, V>(), (IEnumerable<M0>)this.GetPrintedObservations<Tuple<K, OptionalValue<V>>>(Formatting.StartInlineItemCount, Formatting.EndInlineItemCount))));
        }

        public string Format()
        {
            return this.Format(Formatting.StartItemCount, Formatting.EndItemCount);
        }

        public string Format(int itemCount)
        {
            int num = itemCount / 2;
            return this.Format(num, num);
        }


        public string Format(int startCount, int endCount)
        {
            Func<bool, Func<FSharpRef<FSharpOption<object>>, Func<Func<Unit, Unit>, Func<int, Func<int, Func<K, string>>>>>> getLevel = (Func<bool, Func<FSharpRef<FSharpOption<object>>, Func<Func<Unit, Unit>, Func<int, Func<int, Func<K, string>>>>>>)new Series.getLevel<K>();
            if (this.vector.SuppressPrinting)
                return "(Suppressed)";
            if (this.IsEmpty)
                return "(Empty)";
            int levels = CustomKey.Get((object)this.GetKeyAt(0)).Levels;
            int length = levels;
            Func<int, FSharpRef<FSharpOption<object>>> Func = (Func<int, FSharpRef<FSharpOption<object>>>)new Series.previous();
            if (length < 0)
                throw new ArgumentException(string.Format("{0}\n{1} = {2}", new object[3]
                {
          (object) LanguagePrimitives.ErrorStrings.get_InputMustBeNonNegativeString(),
          (object) "count",
          (object) length
                }), "count");
            FSharpRef<FSharpOption<object>>[] fsharpRefArray = new FSharpRef<FSharpOption<object>>[length];
            for (int index = 0; index < fsharpRefArray.Length; ++index)
                fsharpRefArray[index] = Func.Invoke(index);
            FSharpRef<FSharpOption<object>>[] previous = fsharpRefArray;
            Func<int, Func<Unit, Unit>> reset = (Func<int, Func<Unit, Unit>>)new Series.reset(levels, previous);
            return Formatting.formatTable((string[,])ExtraTopLevelOperators.CreateArray2D<IEnumerable<string>, string>((IEnumerable<M0>)SeqModule.Map<FSharpChoice<Tuple<K, V>, Unit, Tuple<K, V>>, IEnumerable<string>>((Func<M0, M1>)new Series.Format<K, V>(this, getLevel, levels, previous, reset), (IEnumerable<M0>)this.GetPrintedObservations<Tuple<K, V>>(startCount, endCount))));
        }

        public Series(IEnumerable<KeyValuePair<K, V>> pairs)
          : this((IEnumerable<K>)SeqModule.Map<KeyValuePair<K, V>, K>((Func<M0, M1>)new Series.ctor<K, V>(), (IEnumerable<M0>)pairs), (IEnumerable<V>)SeqModule.Map<KeyValuePair<K, V>, V>((Func<M0, M1>)new Series.ctor<K, V>(), (IEnumerable<M0>)pairs))
        {
        }

        public Series(IEnumerable<K> keys, IEnumerable<V> values)
        {
            IVectorBuilder instance1 = FVectorBuilderimplementation.VectorBuilder.Instance;
            IIndexBuilder instance2 = FIndexBuilderimplementation.IndexBuilder.Instance;
            // ISSUE: explicit constructor call
            this.ctor(FIndexextensions.Index.ofKeys<K>(System.Array.AsReadOnly<K>((K[])ArrayModule.OfSeq<K>((IEnumerable<M0>)keys))), instance1.Create<V>((V[])ArrayModule.OfSeq<V>((IEnumerable<M0>)values)), instance1, instance2);
        }

        public Series(K[] keys, V[] values)
        {
            IVectorBuilder instance1 = FVectorBuilderimplementation.VectorBuilder.Instance;
            IIndexBuilder instance2 = FIndexBuilderimplementation.IndexBuilder.Instance;
            // ISSUE: explicit constructor call
            this.ctor(FIndexextensions.Index.ofKeys<K>(System.Array.AsReadOnly<K>(keys)), instance1.Create<V>(values), instance1, instance2);
        }

        object ISeries<K>.TryGetObject(K k)
        {
            V optionalValue1 = this.TryGet(k);
            Func<V, object> Func = (Func<V, object>)new Series.DeedleISeriesTryGetObject<V>();
            V optionalValue2 = optionalValue1;
            if (optionalValue2.HasValue)
                return Func.Invoke(optionalValue2.Value);
            return object.Missing;
        }

        string IFsiFormattable.Format()
        {
            return this.Format();
        }
    }
}
