// Decompiled with JetBrains decompiler
// Type: Deedle.Internal.BinomialHeap
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll



using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Deedle.Internal
{
  
  public static class BinomialHeap
  {
    
    public static FSharpChoice<Unit, Unit, Unit> LTGTEQ(int n)
    {
      if (n < 0)
        return FSharpChoice<Unit, Unit, Unit>.NewChoice1Of3((Unit) null);
      if (n > 0)
        return FSharpChoice<Unit, Unit, Unit>.NewChoice2Of3((Unit) null);
      return FSharpChoice<Unit, Unit, Unit>.NewChoice3Of3((Unit) null);
    }

    public static BinomialHeap<T> empty<T>()
    {
      return new BinomialHeap<T>((IComparer<T>) Comparer<T>.Default, FSharpList<RankedTree<T>>.get_Empty());
    }

    public static BinomialHeap<a> emptyCustom<a>(IComparer<a> comparer)
    {
      return new BinomialHeap<a>(comparer, FSharpList<RankedTree<a>>.get_Empty());
    }

    public static bool isEmpty<a>(BinomialHeap<a> heap)
    {
      return heap.Heap.Equals((object) FSharpList<RankedTree<a>>.get_Empty(), LanguagePrimitives.get_GenericEqualityComparer());
    }

    internal static int rank<a>(RankedTree<a> _arg1)
    {
      return _arg1.item1;
    }

    internal static a root<a>(RankedTree<a> _arg1)
    {
      return _arg1.item2;
    }

    
    internal static RankedTree<a> link<a>(IComparer<a> comparer, RankedTree<a> _arg2, RankedTree<a> _arg1)
    {
      RankedTree<a> rankedTree1 = _arg2;
      a x = rankedTree1.item2;
      int num = rankedTree1.item1;
      FSharpList<RankedTree<a>> fsharpList1 = rankedTree1.item3;
      RankedTree<a> rankedTree2 = _arg1;
      a y = rankedTree2.item2;
      FSharpList<RankedTree<a>> fsharpList2 = rankedTree2.item3;
      if (BinomialHeap.LTGTEQ(comparer.Compare(x, y)) is FSharpChoice<Unit, Unit, Unit>.Choice2Of3)
        return RankedTree<a>.NewNode(num + 1, y, FSharpList<RankedTree<a>>.Cons(rankedTree1, fsharpList2));
      return RankedTree<a>.NewNode(num + 1, x, FSharpList<RankedTree<a>>.Cons(rankedTree2, fsharpList1));
    }

    
    public static FSharpList<RankedTree<a>> insertTree<a>(IComparer<a> comparer, RankedTree<a> t, FSharpList<RankedTree<a>> ts)
    {
      while (true)
      {
        FSharpList<RankedTree<a>> fsharpList1 = ts;
        if (fsharpList1.get_TailOrNull() != null)
        {
          FSharpList<RankedTree<a>> fsharpList2 = fsharpList1;
          FSharpList<RankedTree<a>> tailOrNull = fsharpList2.get_TailOrNull();
          RankedTree<a> headOrDefault = fsharpList2.get_HeadOrDefault();
          if (BinomialHeap.rank<a>(t) >= BinomialHeap.rank<a>(headOrDefault))
          {
            IComparer<a> comparer1 = comparer;
            RankedTree<a> rankedTree = BinomialHeap.link<a>(comparer, t, headOrDefault);
            ts = tailOrNull;
            t = rankedTree;
            comparer = comparer1;
          }
          else
            goto label_3;
        }
        else
          break;
      }
      return FSharpList<RankedTree<a>>.Cons(t, FSharpList<RankedTree<a>>.get_Empty());
label_3:
      return FSharpList<RankedTree<a>>.Cons(t, ts);
    }

    
    public static BinomialHeap<a> insert<a>(a x, BinomialHeap<a> heap)
    {
      FSharpList<RankedTree<a>> heap1 = BinomialHeap.insertTree<a>(heap.Comparer, RankedTree<a>.NewNode(0, x, FSharpList<RankedTree<a>>.get_Empty()), heap.Heap);
      return new BinomialHeap<a>(heap.Comparer, heap1);
    }

    
    public static FSharpList<RankedTree<a>> mergeTrees<a>(IComparer<a> comparer, FSharpList<RankedTree<a>> _arg1_0, FSharpList<RankedTree<a>> _arg1_1)
    {
      Tuple<FSharpList<RankedTree<a>>, FSharpList<RankedTree<a>>> tuple = new Tuple<FSharpList<RankedTree<a>>, FSharpList<RankedTree<a>>>(_arg1_0, _arg1_1);
      FSharpList<RankedTree<a>> fsharpList1;
      if (tuple.Item2.get_TailOrNull() != null)
      {
        FSharpList<RankedTree<a>> fsharpList2 = tuple.Item2;
        if (tuple.Item1.get_TailOrNull() != null)
        {
          FSharpList<RankedTree<a>> fsharpList3 = tuple.Item1;
          FSharpList<RankedTree<a>> tailOrNull1 = fsharpList2.get_TailOrNull();
          FSharpList<RankedTree<a>> fsharpList4 = tuple.Item2;
          FSharpList<RankedTree<a>> tailOrNull2 = fsharpList3.get_TailOrNull();
          FSharpList<RankedTree<a>> fsharpList5 = tuple.Item1;
          RankedTree<a> headOrDefault1 = fsharpList2.get_HeadOrDefault();
          RankedTree<a> headOrDefault2 = fsharpList3.get_HeadOrDefault();
          int num1 = BinomialHeap.rank<a>(headOrDefault2);
          int num2 = BinomialHeap.rank<a>(headOrDefault1);
          FSharpChoice<Unit, Unit, Unit> fsharpChoice = BinomialHeap.LTGTEQ(num1 >= num2 ? (num1 > num2 ? 1 : 0) : -1);
          if (fsharpChoice is FSharpChoice<Unit, Unit, Unit>.Choice2Of3)
            return FSharpList<RankedTree<a>>.Cons(headOrDefault1, BinomialHeap.mergeTrees<a>(comparer, fsharpList5, tailOrNull1));
          if (!(fsharpChoice is FSharpChoice<Unit, Unit, Unit>.Choice3Of3))
            return FSharpList<RankedTree<a>>.Cons(headOrDefault2, BinomialHeap.mergeTrees<a>(comparer, tailOrNull2, fsharpList4));
          return BinomialHeap.insertTree<a>(comparer, BinomialHeap.link<a>(comparer, headOrDefault2, headOrDefault1), BinomialHeap.mergeTrees<a>(comparer, tailOrNull2, tailOrNull1));
        }
        fsharpList1 = tuple.Item2;
      }
      else
        fsharpList1 = tuple.Item1;
      return fsharpList1;
    }

    
    public static BinomialHeap<a> merge<a>(BinomialHeap<a> heap1, BinomialHeap<a> heap2)
    {
      FSharpList<RankedTree<a>> heap = BinomialHeap.mergeTrees<a>(heap1.Comparer, heap1.Heap, heap2.Heap);
      return new BinomialHeap<a>(heap1.Comparer, heap);
    }

    
    internal static Tuple<RankedTree<a>, FSharpList<RankedTree<a>>> removeMinTree<a>(IComparer<a> comparer, FSharpList<RankedTree<a>> heap)
    {
      FSharpList<RankedTree<a>> fsharpList1 = heap;
      if (fsharpList1.get_TailOrNull() == null)
        throw new InvalidOperationException("The heap is empty.");
      FSharpList<RankedTree<a>> fsharpList2 = fsharpList1;
      if (fsharpList2.get_TailOrNull().get_TailOrNull() == null)
        return new Tuple<RankedTree<a>, FSharpList<RankedTree<a>>>(fsharpList2.get_HeadOrDefault(), FSharpList<RankedTree<a>>.get_Empty());
      FSharpList<RankedTree<a>> tailOrNull = fsharpList2.get_TailOrNull();
      RankedTree<a> headOrDefault = fsharpList2.get_HeadOrDefault();
      Tuple<RankedTree<a>, FSharpList<RankedTree<a>>> tuple = BinomialHeap.removeMinTree<a>(comparer, tailOrNull);
      FSharpList<RankedTree<a>> fsharpList3 = tuple.Item2;
      RankedTree<a> rankedTree = tuple.Item1;
      FSharpChoice<Unit, Unit, Unit> fsharpChoice = BinomialHeap.LTGTEQ(comparer.Compare(BinomialHeap.root<a>(headOrDefault), BinomialHeap.root<a>(rankedTree)));
      if (fsharpChoice is FSharpChoice<Unit, Unit, Unit>.Choice1Of3 || fsharpChoice is FSharpChoice<Unit, Unit, Unit>.Choice3Of3)
        return new Tuple<RankedTree<a>, FSharpList<RankedTree<a>>>(headOrDefault, tailOrNull);
      return new Tuple<RankedTree<a>, FSharpList<RankedTree<a>>>(rankedTree, FSharpList<RankedTree<a>>.Cons(headOrDefault, fsharpList3));
    }

    public static a findMin<a>(BinomialHeap<a> _arg1)
    {
      BinomialHeap<a> binomialHeap = _arg1;
      FSharpList<RankedTree<a>> heap = binomialHeap.Heap;
      return BinomialHeap.root<a>(BinomialHeap.removeMinTree<a>(binomialHeap.Comparer, heap).Item1);
    }

    public static Tuple<a, BinomialHeap<a>> removeMin<a>(BinomialHeap<a> _arg1)
    {
      BinomialHeap<a> binomialHeap = _arg1;
      FSharpList<RankedTree<a>> heap = binomialHeap.Heap;
      IComparer<a> comparer = binomialHeap.Comparer;
      Tuple<RankedTree<a>, FSharpList<RankedTree<a>>> tuple = BinomialHeap.removeMinTree<a>(comparer, heap);
      a a = tuple.Item1.item2;
      FSharpList<RankedTree<a>> fsharpList1 = tuple.Item2;
      FSharpList<RankedTree<a>> fsharpList2 = tuple.Item1.item3;
      return new Tuple<a, BinomialHeap<a>>(a, new BinomialHeap<a>(comparer, BinomialHeap.mergeTrees<a>(comparer, (FSharpList<RankedTree<a>>) ListModule.Reverse<RankedTree<a>>((FSharpList<M0>) fsharpList2), fsharpList1)));
    }

    public static BinomialHeap<a> deleteMin<a>(BinomialHeap<a> heap)
    {
      return BinomialHeap.removeMin<a>(heap).Item2;
    }
  }
}
