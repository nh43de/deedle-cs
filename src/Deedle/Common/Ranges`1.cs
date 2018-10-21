// Decompiled with JetBrains decompiler
// Type: Deedle.Ranges.Ranges`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using \u003CStartupCodeDeedle\u003E;


using System;
using System.Collections.Generic;

namespace Deedle.Ranges
{
  
  [Serializable]
  public class Ranges<T>
  {
    internal IRangeKeyOperations<T> ops;
    internal Tuple<T, T>[] ranges;

    public Ranges(IEnumerable<Tuple<T, T>> ranges, IRangeKeyOperations<T> ops)
    {
      Deedle.Ranges.Ranges<T> ranges1 = this;
      this.ops = ops;
      this.ranges = (Tuple<T, T>[]) ArrayModule.OfSeq<Tuple<T, T>>((IEnumerable<M0>) ranges);
      foreach (Tuple<T, T> tuple in this.ranges)
      {
        if (this.ops.Compare(tuple.Item1, tuple.Item2) > 0)
          throw new ArgumentException("Invalid range (first offset is greater than second)", nameof (ranges));
      }
    }

    public Tuple<T, T>[] Ranges
    {
      get
      {
        return this.ranges;
      }
    }

    public IRangeKeyOperations<T> Operations
    {
      get
      {
        return this.ops;
      }
    }

    public T FirstKey
    {
      get
      {
        Deedle.Ranges.Ranges<T> ranges = this;
        return new Tuple<T, T>(ranges.Ranges[0].Item1, ranges.Ranges[ranges.Ranges.Length - 1].Item2).Item1;
      }
    }

    public T LastKey
    {
      get
      {
        Deedle.Ranges.Ranges<T> ranges = this;
        return new Tuple<T, T>(ranges.Ranges[0].Item1, ranges.Ranges[ranges.Ranges.Length - 1].Item2).Item2;
      }
    }

    public T KeyAtOffset(long offset)
    {
      return Deedle.Ranges.Ranges.keyAtOffset<T>(offset, this);
    }

    public long OffsetOfKey(T key)
    {
      long num = Deedle.Ranges.Ranges.offsetOfKey<T>(key, this);
      if (num == Deedle.Ranges.Ranges.invalid)
        throw new IndexOutOfRangeException();
      return num;
    }

    public IEnumerable<T> Keys
    {
      get
      {
        return (IEnumerable<T>) new Ranges.get_Keys<T>(this, (Tuple<T, T>) null, default (T), default (T), (IEnumerator<Tuple<T, T>>) null, default (T), (IEnumerator<T>) null, 0, default (T));
      }
    }

    public long Length
    {
      get
      {
        Deedle.Ranges.Ranges<T> ranges1 = this;
        Tuple<T, T>[] ranges2 = ranges1.Ranges;
        FSharpFunc<Tuple<T, T>, long> fsharpFunc = (FSharpFunc<Tuple<T, T>, long>) new Ranges.get_Length<T>(ranges1);
        Tuple<T, T>[] tupleArray = ranges2;
        if ((object) tupleArray == null)
          throw new ArgumentNullException("array");
        long num = 0;
        for (int index = 0; index < tupleArray.Length; ++index)
          checked { num += fsharpFunc.Invoke(tupleArray[index]); }
        return num;
      }
    }

    public OptionalValue<Tuple<T, long>> Lookup(T key, Lookup semantics, Func<T, long, bool> check)
    {
      return Deedle.Ranges.Ranges.lookup<T>(key, semantics, (FSharpFunc<T, FSharpFunc<long, bool>>) new Ranges.Lookup<T>(check), this);
    }

    public Deedle.Ranges.Ranges<T> MergeWith(IEnumerable<Deedle.Ranges.Ranges<T>> ranges)
    {
      IEnumerable<Deedle.Ranges.Ranges<T>> list = (IEnumerable<Deedle.Ranges.Ranges<T>>) SeqModule.ToList<Deedle.Ranges.Ranges<T>>((IEnumerable<M0>) new Ranges.MergeWith<T>(this, ranges, 0, (Deedle.Ranges.Ranges<T>) null));
      if (SeqModule.IsEmpty<Deedle.Ranges.Ranges<T>>((IEnumerable<M0>) list))
        throw new ArgumentException("Range cannot be empty", nameof (ranges));
      IRangeKeyOperations<T> operations = ((Deedle.Ranges.Ranges<T>) SeqModule.Head<Deedle.Ranges.Ranges<T>>((IEnumerable<M0>) list)).Operations;
      FSharpList<Tuple<T, T>> fsharpList = (FSharpList<Tuple<T, T>>) ListModule.SortWith<Tuple<T, T>>((FSharpFunc<M0, FSharpFunc<M0, int>>) new Ranges.MergeWith<T>(operations), SeqModule.ToList<Tuple<T, T>>((IEnumerable<M0>) new Ranges.MergeWith<T>(list, (Deedle.Ranges.Ranges<T>) null, (IEnumerator<Deedle.Ranges.Ranges<T>>) null, (Tuple<T, T>) null, (IEnumerator<Tuple<T, T>>) null, 0, (Tuple<T, T>) null)));
      FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, IEnumerable<Tuple<T, T>>>> fsharpFunc = (FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, IEnumerable<Tuple<T, T>>>>) new Ranges.MergeWith<T>(operations);
      IRangeKeyOperations<T> ops = operations;
      return new Deedle.Ranges.Ranges<T>((IEnumerable<Tuple<T, T>>) FSharpFunc<Tuple<T, T>, FSharpList<Tuple<T, T>>>.InvokeFast<IEnumerable<Tuple<T, T>>>((FSharpFunc<Tuple<T, T>, FSharpFunc<FSharpList<Tuple<T, T>>, M0>>) fsharpFunc, (Tuple<T, T>) ListModule.Head<Tuple<T, T>>((FSharpList<M0>) fsharpList), (FSharpList<Tuple<T, T>>) ListModule.Tail<Tuple<T, T>>((FSharpList<M0>) fsharpList)), ops);
    }

    public Deedle.Ranges.Ranges<T> Restrict(RangeRestriction<T> restriction)
    {
      return Deedle.Ranges.Ranges.restrictRanges<T>(restriction, this);
    }
  }
}
