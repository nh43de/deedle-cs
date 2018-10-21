// Decompiled with JetBrains decompiler
// Type: Deedle.Internal.BinomialHeap`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll



using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Deedle.Internal
{
  
  [Serializable]
  public sealed class BinomialHeap<T> : IEquatable<BinomialHeap<T>>, IStructuralEquatable
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal IComparer<T> Comparer;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    internal FSharpList<RankedTree<T>> Heap;

    
    public IComparer<T> Comparer
    {
      get
      {
        return this.Comparer;
      }
    }

    
    public FSharpList<RankedTree<T>> Heap
    {
      get
      {
        return this.Heap;
      }
    }

    public BinomialHeap(IComparer<T> comparer, FSharpList<RankedTree<T>> heap)
    {
      this.Comparer = comparer;
      this.Heap = heap;
    }

    [CompilerGenerated]
    public override string ToString()
    {
      return ((FSharpFunc<BinomialHeap<T>, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<BinomialHeap<T>, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<BinomialHeap<T>, string>, Unit, string, string, BinomialHeap<T>>("%+A"))).Invoke(this);
    }

    [CompilerGenerated]
    public virtual int GetHashCode(IEqualityComparer comp)
    {
      if (this == null)
        return 0;
      int num1 = 0;
      int num2 = this.Heap.GetHashCode(comp) + ((num1 << 6) + (num1 >> 2)) - 1640531527;
      return LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<IComparer<T>>(comp, (M0) this.Comparer) + ((num2 << 6) + (num2 >> 2)) - 1640531527;
    }

    [CompilerGenerated]
    public override sealed int GetHashCode()
    {
      return this.GetHashCode(LanguagePrimitives.get_GenericEqualityComparer());
    }

    [CompilerGenerated]
    public virtual bool Equals(object obj, IEqualityComparer comp)
    {
      if (this == null)
        return obj == null;
      BinomialHeap<T> binomialHeap1 = obj as BinomialHeap<T>;
      if (binomialHeap1 == null)
        return false;
      BinomialHeap<T> binomialHeap2 = binomialHeap1;
      if (!LanguagePrimitives.HashCompare.GenericEqualityWithComparerIntrinsic<IComparer<T>>(comp, (M0) this.Comparer, (M0) binomialHeap2.Comparer))
        return false;
      IEqualityComparer equalityComparer = comp;
      return this.Heap.Equals((object) binomialHeap2.Heap, equalityComparer);
    }

    [CompilerGenerated]
    public virtual bool Equals(BinomialHeap<T> obj)
    {
      if (this == null)
        return obj == null;
      if (obj != null && LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<IComparer<T>>((M0) this.Comparer, (M0) obj.Comparer))
        return LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<FSharpList<RankedTree<T>>>((M0) this.Heap, (M0) obj.Heap);
      return false;
    }

    [CompilerGenerated]
    public override sealed bool Equals(object obj)
    {
      BinomialHeap<T> binomialHeap = obj as BinomialHeap<T>;
      if (binomialHeap != null)
        return this.Equals(binomialHeap);
      return false;
    }
  }
}
