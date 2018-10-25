// Decompiled with JetBrains decompiler
// Type: Deedle.ObjectSeries`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Indices;
using Deedle.Vectors;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Deedle
{
    [Serializable]
    public class ObjectSeries<K> : Series<K, object>
    {
        internal new IVectorBuilder vectorBuilder;
        internal new IVector<object> vector;
        internal new IIndexBuilder indexBuilder;
        internal new IIndex<K> index;

        internal ObjectSeries(IIndex<K> index, IVector<object> vector, IVectorBuilder vectorBuilder, IIndexBuilder indexBuilder)
          : base(index, vector, vectorBuilder, indexBuilder)
        {
            ObjectSeries<K> objectSeries = this;
            this.index = index;
            this.vector = vector;
            this.vectorBuilder = vectorBuilder;
            this.indexBuilder = indexBuilder;
        }

        public ObjectSeries(Series<K, object> series)
          : this(series.Index, series.Vector, series.VectorBuilder, series.IndexBuilder)
        {
        }

        public IEnumerable<R> GetValues<R>(ConversionKind conversionKind)
        {
            return (IEnumerable<R>)SeqModule.Choose<object, R>((FSharpFunc<M0, FSharpOption<M1>>)new \u0024Series.GetValues\u00401220 < R > (conversionKind), (IEnumerable<M0>)this.Values);
        }

        public IEnumerable<R> GetValues<R>()
        {
            return this.GetValues<R>(ConversionKind.Safe);
        }

        public R GetAs<R>(K column)
        {
            return Deedle.Internal.Convert.convertType<R>(ConversionKind.Flexible, this.Get(column));
        }

        public R GetAs<R>(K column, R fallback)
        {
            FSharpChoice<Unit, Tuple<K, long>> fsharpChoice1 = OptionalValueModule.\u007CMissing\u007CPresent\u007C< Tuple<K, long> > (this.index.Lookup(column, Lookup.Exact, (FSharpFunc<long, bool>)new \u0024Series.address\u00401230\u002D1()));
            if (fsharpChoice1 is FSharpChoice<Unit, Tuple<K, long>>.Choice1Of2)
                throw new KeyNotFoundException(((FSharpFunc<K, string>)ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<K, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<FSharpFunc<K, string>, Unit, string, string, K>("The key %O is not present in the index"))).Invoke(column));
            FSharpChoice<Unit, object> fsharpChoice2 = OptionalValueModule.\u007CMissing\u007CPresent\u007C< object > (this.vector.GetValue(((FSharpChoice<Unit, Tuple<K, long>>.Choice2Of2)fsharpChoice1).get_Item().Item2));
            if (!(fsharpChoice2 is FSharpChoice<Unit, object>.Choice1Of2))
                return Deedle.Internal.Convert.convertType<R>(ConversionKind.Flexible, ((FSharpChoice<Unit, object>.Choice2Of2)fsharpChoice2).get_Item());
            return fallback;
        }

        public R GetAtAs<R>(int index)
        {
            return Deedle.Internal.Convert.convertType<R>(ConversionKind.Flexible, this.GetAt(index));
        }

        public R GetAtAs<R>(int index, ConversionKind conversionKind)
        {
            return Deedle.Internal.Convert.convertType<R>(conversionKind, this.GetAt(index));
        }

        public OptionalValue<R> TryGetAs<R>(K column)
        {
            OptionalValue<object> optionalValue1 = this.TryGet(column);
            FSharpFunc<object, R> fsharpFunc = (FSharpFunc<object, R>)new \u0024Series.TryGetAs\u00401245 < R > ();
            OptionalValue<object> optionalValue2 = optionalValue1;
            if (optionalValue2.HasValue)
                return new OptionalValue<R>(fsharpFunc.Invoke(optionalValue2.Value));
            return OptionalValue<R>.Missing;
        }

        public OptionalValue<R> TryGetAs<R>(K column, ConversionKind conversionKind)
        {
            var optionalValue1 = this.TryGet(column);
            Func<object, R> fsharpFunc = (FSharpFunc<object, R>)new \u0024Series.TryGetAs\u00401248\u002D1 < R > (conversionKind);
            var optionalValue2 = optionalValue1;

            if (optionalValue2.HasValue)
                return new OptionalValue<R>(fsharpFunc.Invoke(optionalValue2.Value));
            return OptionalValue<R>.Missing;
        }

        [SpecialName]
        public static double op_Dynamic(ObjectSeries<string> series, string name)
        {
            return series.GetAs<double>(name, Operators.get_NaN());
        }

        public OptionalValue<Series<K, R>> TryAs<R>(ConversionKind conversionKind)
        {
            OptionalValue<IVector<R>> optionalValue1 = VectorHelpers.tryConvertType<R>(conversionKind, (IVector)this.vector);
            FSharpFunc<IVector<R>, Series<K, R>> fsharpFunc = (FSharpFunc<IVector<R>, Series<K, R>>)new \u0024Series.TryAs\u00401255 < K, R > (this);
            OptionalValue<IVector<R>> optionalValue2 = optionalValue1;
            if (optionalValue2.HasValue)
                return new OptionalValue<Series<K, R>>(fsharpFunc.Invoke(optionalValue2.Value));
            return OptionalValue<Series<K, R>>.Missing;
        }

        public OptionalValue<Series<K, R>> TryAs<R>()
        {
            return this.TryAs<R>(ConversionKind.Safe);
        }

        public Series<K, R> As<R>()
        {
            return new Series<K, R>(this.indexBuilder.Project<K>(this.index), VectorHelpers.convertType<R>(ConversionKind.Flexible, (IVector)this.vector), this.vectorBuilder, this.indexBuilder);
        }

        [Obsolete("GetValues(bool) is obsolete. Use GetValues(ConversionKind) instead.")]
        public IEnumerable<object> GetValues<R>(bool strict)
        {
            return this.GetValues<object>(!strict ? ConversionKind.Flexible : ConversionKind.Exact);
        }

        [Obsolete("TryAs(bool) is obsolete. Use TryAs(ConversionKind) instead.")]
        public OptionalValue<Series<K, R>> TryAs<R>(bool strict)
        {
            return this.TryAs<R>(!strict ? ConversionKind.Flexible : ConversionKind.Exact);
        }
    }
}
