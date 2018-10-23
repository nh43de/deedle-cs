// Decompiled with JetBrains decompiler
// Type: Deedle.Indices.Linear.LinearIndex`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Deedle.Indices.Linear
{

    [Serializable]
    public class LinearIndex<K> : IIndex<K>
    {
        internal bool? ordered;
        internal ReadOnlyCollection<K> keys;
        internal IIndexBuilder builder;
        internal Comparer<K> comparer;
        internal Dictionary<K, long> lookup;

        internal LinearIndex(ReadOnlyCollection<K> keys, IIndexBuilder builder, bool? ordered = null)
        {
            LinearIndex<K> linearIndex = this;
            this.keys = keys;
            this.builder = builder;
            this.ordered = ordered;
            this.comparer = Comparer<K>.Default;
            this.lookup = null;
        }

        internal Dictionary<K, long> lookupMap
        {
            get
            {
                this.ensureLookup();
                return this.lookup;
            }
        }

        internal bool isOrdered
        {
            get
            {
                this.ensureLookup();
                if (!this.ordered.HasValue)
                    this.ordered = new bool?(this.isOrdered());
                return this.ordered.Value;
            }
        }

        internal ReadOnlyCollection<K> KeyCollection
        {
            get
            {
                return this.keys;
            }
        }

        public override bool Equals(object another)
        {
            object obj = another;
            if (obj == null)
                return false;
            IIndex<K> index = obj as IIndex<K>;
            if (index != null)
                return Seq.structuralEquals<K>((IEnumerable<K>)this.keys, index.KeySequence);
            return false;
        }

        public override int GetHashCode()
        {
            return Seq.structuralHash<K>((IEnumerable<K>)this.keys);
        }

        [SpecialName]
        Addressing.IAddressingScheme IIndex<K>.get_AddressingScheme()
        {
            return Addressing.LinearAddressingScheme.Instance;
        }

        [SpecialName]
        Addressing.IAddressOperations IIndex<K>.get_AddressOperations()
        {
            return (Addressing.IAddressOperations)LinearAddressOperations.NewLinearAddressOperations(0L, (long)this.keys.Count - 1L);
        }

        [SpecialName]
        ReadOnlyCollection<K> IIndex<K>.get_Keys()
        {
            this.ensureLookup();
            return this.keys;
        }

        [SpecialName]
        IEnumerable<K> IIndex<K>.get_KeySequence()
        {
            this.ensureLookup();
            return (IEnumerable<K>)this.keys;
        }

        [SpecialName]
        long IIndex<K>.get_KeyCount()
        {
            this.ensureLookup();
            return (long)this.keys.Count;
        }

        [SpecialName]
        IIndexBuilder IIndex<K>.get_Builder()
        {
            return this.builder;
        }

        long IIndex<K>.AddressAt(long idx)
        {
            return idx;
        }

        K IIndex<K>.KeyAt(long address)
        {
            return this.keys[(int)address];
        }

        [SpecialName]
        bool IIndex<K>.get_IsEmpty()
        {
            return SeqModule.IsEmpty<K>((IEnumerable<M0>)this.keys);
        }

        [SpecialName]
        Tuple<K, K> IIndex<K>.get_KeyRange()
        {
            if (!this.isOrdered)
                throw new InvalidOperationException("KeyRange is not supported for unordered index.");
            return new Tuple<K, K>(this.keys[0], this.keys[this.keys.Count - 1]);
        }

        long IIndex<K>.Locate(K key)
        {
            long num = new long();
            Tuple<bool, long> tuple = new Tuple<bool, long>(this.lookupMap.TryGetValue(key, out num), num);
            if (tuple.Item1)
                return tuple.Item2;
            return Addressing.LinearAddress.invalid;
        }

        OptionalValue<Tuple<K, long>> IIndex<K>.Lookup(K key, Lookup semantics, Func<long, bool> check)
        {
            long num1 = new long();
            Tuple<Tuple<bool, long>, Lookup> tuple = new Tuple<Tuple<bool, long>, Lookup>(new Tuple<bool, long>(this.lookupMap.TryGetValue(key, out num1), num1), semantics);
            if (tuple.Item1.Item1)
            {
                switch (tuple.Item2)
                {
                    case Lookup.Exact:
                        long num2 = tuple.Item1.Item2;
                        return new OptionalValue<Tuple<K, long>>(new Tuple<K, long>(key, num2));
                    default:
                        long num3;
                        switch (tuple.Item2)
                        {
                            case Lookup.ExactOrGreater:
                                long num4 = tuple.Item1.Item2;
                                if (check.Invoke(num4))
                                {
                                    num3 = tuple.Item1.Item2;
                                    break;
                                }
                                goto label_10;
                            default:
                                switch (tuple.Item2)
                                {
                                    case Lookup.ExactOrSmaller:
                                        long num5 = tuple.Item1.Item2;
                                        if (check.Invoke(num5))
                                        {
                                            num3 = tuple.Item1.Item2;
                                            break;
                                        }
                                        goto label_10;
                                    default:
                                        goto label_10;
                                }
                        }
                        return new OptionalValue<Tuple<K, long>>(new Tuple<K, long>(key, num3));
                }
            }
            label_10:
            switch (tuple.Item2)
            {
                case Lookup.Smaller:
                    if (!this.isOrdered)
                        goto default;
                    else
                        break;
                case Lookup.ExactOrSmaller:
                    if (!this.isOrdered)
                        goto default;
                    else
                        break;
                default:
                    switch (tuple.Item2)
                    {
                        case Lookup.Greater:
                            if (!this.isOrdered)
                                goto default;
                            else
                                break;
                        case Lookup.ExactOrGreater:
                            if (!this.isOrdered)
                                goto default;
                            else
                                break;
                        default:
                            return OptionalValue<Tuple<K, long>>.Missing;
                    }
                    bool inclusive1 = semantics == Lookup.ExactOrGreater;
                    IEnumerable<int> ints1 = (IEnumerable<int>)Operators.DefaultArg<IEnumerable<int>>((FSharpOption<M0>)OptionModule.Map<int, IEnumerable<int>>((Func<M0, M1>)new LinearIndex.indices<K>(this), (FSharpOption<M0>)Deedle.Internal.Array.binarySearchNearestGreater<K>(key, (IComparer<K>)this.comparer, inclusive1, this.keys)), (M0)SeqModule.Empty<int>());
                    FSharpOption<int> fsharpOption1 = Seq.headOrNone<int>((IEnumerable<int>)SeqModule.Filter<int>((Func<M0, bool>)new LinearIndex.DeedleIndicesIIndexLookup((Func<int, long>)new LinearIndex.DeedleIndicesIIndexLookup(), check), (IEnumerable<M0>)ints1));
                    int optionalValue1 = fsharpOption1 == null ? int.Missing : new int(fsharpOption1.get_Value());
                    Func<int, Tuple<K, long>> Func1 = (Func<int, Tuple<K, long>>)new LinearIndex.DeedleIndicesIIndexLookup<K>(this);
                    int optionalValue2 = optionalValue1;
                    if (optionalValue2.HasValue)
                        return new OptionalValue<Tuple<K, long>>(Func1.Invoke(optionalValue2.Value));
                    return OptionalValue<Tuple<K, long>>.Missing;
            }
            bool inclusive2 = semantics == Lookup.ExactOrSmaller;
            IEnumerable<int> ints2 = (IEnumerable<int>)Operators.DefaultArg<IEnumerable<int>>((FSharpOption<M0>)OptionModule.Map<int, IEnumerable<int>>((Func<M0, M1>)new LinearIndex.indices(), (FSharpOption<M0>)Deedle.Internal.Array.binarySearchNearestSmaller<K>(key, (IComparer<K>)this.comparer, inclusive2, this.keys)), (M0)SeqModule.Empty<int>());
            FSharpOption<int> fsharpOption2 = Seq.headOrNone<int>((IEnumerable<int>)SeqModule.Filter<int>((Func<M0, bool>)new LinearIndex.DeedleIndicesIIndexLookup((Func<int, long>)new LinearIndex.DeedleIndicesIIndexLookup(), check), (IEnumerable<M0>)ints2));
            int optionalValue3 = fsharpOption2 == null ? int.Missing : new int(fsharpOption2.get_Value());
            Func<int, Tuple<K, long>> Func2 = (Func<int, Tuple<K, long>>)new LinearIndex.DeedleIndicesIIndexLookup<K>(this);
            int optionalValue4 = optionalValue3;
            if (optionalValue4.HasValue)
                return new OptionalValue<Tuple<K, long>>(Func2.Invoke(optionalValue4.Value));
            return OptionalValue<Tuple<K, long>>.Missing;
        }

        [SpecialName]
        IEnumerable<KeyValuePair<K, long>> IIndex<K>.get_Mappings()
        {
            this.ensureLookup();
            return (IEnumerable<KeyValuePair<K, long>>)SeqModule.MapIndexed<K, KeyValuePair<K, long>>((Func<int, Func<M0, M1>>)new LinearIndex.DeedleIndicesIIndexget_Mappings<K>(), (IEnumerable<M0>)this.keys);
        }

        [SpecialName]
        bool IIndex<K>.get_IsOrdered()
        {
            return this.isOrdered;
        }

        [SpecialName]
        Comparer<K> IIndex<K>.get_Comparer()
        {
            return this.comparer;
        }


        internal bool isOrdered()
        {
            var ordered = this.ordered;
            if (ordered != null)
                return ordered.get_Value();
            if ((!typeof(IComparable).IsAssignableFrom(typeof(K)) ? (typeof(IComparable<K>).IsAssignableFrom(typeof(K)) ? 1 : 0) : 1) != 0)
            {
                try
                {
                    return Seq.isSorted<K>((IEnumerable<K>)this.keys, (IComparer<K>)this.comparer);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                int num;
                if (typeof(K).IsGenericTypeDefinition)
                {
                    Type genericTypeDefinition = typeof(K).GetGenericTypeDefinition();
                    Type type1 = typeof(int?);
                    Type type2 = !type1.GetTypeInfo().IsGenericType ? type1 : type1.GetGenericTypeDefinition();
                    num = LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)genericTypeDefinition, (M0)type2) ? 1 : 0;
                }
                else
                    num = 0;
                if (num == 0)
                    return false;
                try
                {
                    return Seq.isSorted<K>((IEnumerable<K>)this.keys, (IComparer<K>)this.comparer);
                }
                catch
                {
                    return false;
                }
            }
        }


        internal void ensureLookup()
        {
            if (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Dictionary<K, long>>((M0)this.lookup, (M0)null))
                return;
            Dictionary<K, long> dictionary = new Dictionary<K, long>();
            long num1 = 0;
            IEnumerator<K> enumerator = this.keys.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    K current = enumerator.Current;
                    long num2 = new long();
                    Tuple<bool, long> tuple = new Tuple<bool, long>(dictionary.TryGetValue(current, out num2), num2);
                    if (tuple.Item1)
                    {
                        long num3 = tuple.Item2;
                        throw new ArgumentException(((Func<K, string>)ExtraTopLevelOperators.PrintFormatToString<Func<K, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<K, string>, Unit, string, string, K>("Duplicate key '%A'. Duplicate keys are not allowed in the index."))).Invoke(current), "keys");
                    }
                    dictionary[current] = num1;
                    ++num1;
                }
            }
            finally
            {
                (enumerator as IDisposable)?.Dispose();
            }
            this.lookup = dictionary;
        }
    }
}