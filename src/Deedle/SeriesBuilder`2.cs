// Decompiled with JetBrains decompiler
// Type: Deedle.SeriesBuilder`2
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using \u003CStartupCodeDeedle\u003E;
using Deedle.Internal;


using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Deedle
{
  
  [Serializable]
  public class SeriesBuilder<K, V> : IDynamicMetaObjectProvider, IDictionary<K, V>, IEnumerable<KeyValuePair<K, V>>, IEnumerable
  {
    internal FSharpList<K> keys;
    internal FSharpList<V> values;

    public SeriesBuilder()
    {
      SeriesBuilder<K, V> seriesBuilder = this;
      this.keys = FSharpList<K>.get_Empty();
      this.values = FSharpList<V>.get_Empty();
    }

    public void Add(K key, V value)
    {
      this.keys = FSharpList<K>.Cons(key, this.keys);
      this.values = FSharpList<V>.Cons(value, this.values);
    }

    public Deedle.Series<K, V> Series
    {
      get
      {
        return new Deedle.Series<K, V>(FIndexextensions.Index.ofKeys<K>((FSharpList<K>) ListModule.Reverse<K>((FSharpList<M0>) this.keys)), FVectorBuilderimplementation.VectorBuilder.Instance.Create<V>((V[]) ArrayModule.OfSeq<V>((IEnumerable<M0>) ListModule.Reverse<V>((FSharpList<M0>) this.values))), FVectorBuilderimplementation.VectorBuilder.Instance, FIndexBuilderimplementation.IndexBuilder.Instance);
      }
    }

    
    public static void op_DynamicAssignment(SeriesBuilder<string, V> builder, string name, V value)
    {
      builder.Add(name, value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) ((IEnumerable<KeyValuePair<K, V>>) this).GetEnumerator();
    }

    IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator()
    {
      return ((IEnumerable<KeyValuePair<K, V>>) SeqModule.Map<Tuple<K, V>, KeyValuePair<K, V>>((FSharpFunc<M0, M1>) new SeriesExtensions.System\u002DCollections\u002DGeneric\u002DIEnumerable\u002DGetEnumerator<K, V>(), (IEnumerable<M0>) ListModule.Reverse<Tuple<K, V>>((FSharpList<M0>) ListModule.Zip<K, V>((FSharpList<M0>) this.keys, (FSharpList<M1>) this.values)))).GetEnumerator();
    }

    
    ICollection<K> IDictionary<K, V>.get_Keys()
    {
      return (ICollection<K>) System.Array.AsReadOnly<K>((K[]) ArrayModule.OfSeq<K>((IEnumerable<M0>) this.keys));
    }

    
    ICollection<V> IDictionary<K, V>.get_Values()
    {
      return (ICollection<V>) System.Array.AsReadOnly<V>((V[]) ArrayModule.OfSeq<V>((IEnumerable<M0>) this.values));
    }

    void ICollection<KeyValuePair<K, V>>.Clear()
    {
      this.keys = FSharpList<K>.get_Empty();
      this.values = FSharpList<V>.get_Empty();
    }

    
    V IDictionary<K, V>.get_Item(K key)
    {
      throw Operators.Failure("!");
    }

    
    void IDictionary<K, V>.set_Item(K key, V value)
    {
      throw Operators.Failure("!");
    }

    void IDictionary<K, V>.Add(K k, V v)
    {
      this.Add(k, v);
    }

    void ICollection<KeyValuePair<K, V>>.Add(KeyValuePair<K, V> kvp)
    {
      this.Add(kvp.Key, kvp.Value);
    }

    bool IDictionary<K, V>.ContainsKey(K k)
    {
      return SeqModule.Exists<K>((FSharpFunc<M0, bool>) new SeriesExtensions.System\u002DCollections\u002DGeneric\u002DIDictionary\u002DContainsKey<K>(k), (IEnumerable<M0>) this.keys);
    }

    bool ICollection<KeyValuePair<K, V>>.Contains(KeyValuePair<K, V> kvp)
    {
      return this.Contains<KeyValuePair<K, V>>(kvp);
    }

    bool ICollection<KeyValuePair<K, V>>.Remove(KeyValuePair<K, V> kvp)
    {
      FSharpList<Tuple<K, V>> fsharpList = (FSharpList<Tuple<K, V>>) ListModule.Filter<Tuple<K, V>>((FSharpFunc<M0, bool>) new SeriesExtensions.newPairs<K, V>(kvp), (FSharpList<M0>) ListModule.Zip<K, V>((FSharpList<M0>) this.keys, (FSharpList<M1>) this.values));
      bool flag = fsharpList.get_Length() < this.keys.get_Length();
      this.keys = (FSharpList<K>) ListModule.Map<Tuple<K, V>, K>((FSharpFunc<M0, M1>) new SeriesExtensions.System\u002DCollections\u002DGeneric\u002DIDictionary\u002DRemove<K, V>(), (FSharpList<M0>) fsharpList);
      this.values = (FSharpList<V>) ListModule.Map<Tuple<K, V>, V>((FSharpFunc<M0, M1>) new SeriesExtensions.System\u002DCollections\u002DGeneric\u002DIDictionary\u002DRemove<K, V>(), (FSharpList<M0>) fsharpList);
      return flag;
    }

    bool IDictionary<K, V>.Remove(K key)
    {
      FSharpList<Tuple<K, V>> fsharpList = (FSharpList<Tuple<K, V>>) ListModule.Filter<Tuple<K, V>>((FSharpFunc<M0, bool>) new SeriesExtensions.newPairs<K, V>(key), (FSharpList<M0>) ListModule.Zip<K, V>((FSharpList<M0>) this.keys, (FSharpList<M1>) this.values));
      bool flag = fsharpList.get_Length() < this.keys.get_Length();
      this.keys = (FSharpList<K>) ListModule.Map<Tuple<K, V>, K>((FSharpFunc<M0, M1>) new SeriesExtensions.System\u002DCollections\u002DGeneric\u002DIDictionary\u002DRemove<K, V>(), (FSharpList<M0>) fsharpList);
      this.values = (FSharpList<V>) ListModule.Map<Tuple<K, V>, V>((FSharpFunc<M0, M1>) new SeriesExtensions.System\u002DCollections\u002DGeneric\u002DIDictionary\u002DRemove<K, V>(), (FSharpList<M0>) fsharpList);
      return flag;
    }

    void ICollection<KeyValuePair<K, V>>.CopyTo(KeyValuePair<K, V>[] array, int offset)
    {
      SeriesBuilder<K, V> seriesBuilder = this;
      SeqModule.IterateIndexed<KeyValuePair<K, V>>((FSharpFunc<int, FSharpFunc<M0, Unit>>) new SeriesExtensions.System\u002DCollections\u002DGeneric\u002DIDictionary\u002DCopyTo<K, V>(offset, array), (IEnumerable<M0>) seriesBuilder);
    }

    
    int ICollection<KeyValuePair<K, V>>.get_Count()
    {
      return ListModule.Length<K>((FSharpList<M0>) this.keys);
    }

    
    bool ICollection<KeyValuePair<K, V>>.get_IsReadOnly()
    {
      return false;
    }

    bool IDictionary<K, V>.TryGetValue(K key, ref V value)
    {
      FSharpOption<Tuple<K, V>> fsharpOption = (FSharpOption<Tuple<K, V>>) SeqModule.TryFind<Tuple<K, V>>((FSharpFunc<M0, bool>) new SeriesExtensions.System\u002DCollections\u002DGeneric\u002DIDictionary\u002DTryGetValue<K, V>(key), (IEnumerable<M0>) SeqModule.Zip<K, V>((IEnumerable<M0>) this.keys, (IEnumerable<M1>) this.values));
      if (fsharpOption == null)
        return false;
      V v = fsharpOption.get_Value().Item2;
      value = v;
      return true;
    }

    DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression expr)
    {
      return DynamicExtensions.createGetterAndSetterFromFunc<SeriesBuilder<K, V>>(expr, this, (FSharpFunc<SeriesBuilder<K, V>, FSharpFunc<string, object>>) new SeriesExtensions.System\u002DDynamic\u002DIDynamicMetaObjectProvider\u002DGetMetaObject<K, V>(), (FSharpFunc<SeriesBuilder<K, V>, FSharpFunc<string, FSharpFunc<object, Unit>>>) new SeriesExtensions.System\u002DDynamic\u002DIDynamicMetaObjectProvider\u002DGetMetaObject<K, V>());
    }
  }
}
