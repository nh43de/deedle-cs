// Decompiled with JetBrains decompiler
// Type: Deedle.Internal.RankedTree`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll



using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deedle.Internal
{
  [DebuggerDisplay("{__DebugDisplay(),nq}")]
  
  [Serializable]
  [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
  public sealed class RankedTree<T> : IEquatable<RankedTree<T>>, IStructuralEquatable, IComparable<RankedTree<T>>, IComparable, IStructuralComparable
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [CompilerGenerated]
    [DebuggerNonUserCode]
    internal readonly int item1;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [CompilerGenerated]
    [DebuggerNonUserCode]
    internal readonly T item2;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [CompilerGenerated]
    [DebuggerNonUserCode]
    internal readonly FSharpList<RankedTree<T>> item3;

    
    public static RankedTree<T> NewNode(int item1, T item2, FSharpList<RankedTree<T>> item3)
    {
      return new RankedTree<T>(item1, item2, item3);
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    internal RankedTree(int item1, T item2, FSharpList<RankedTree<T>> item3)
    {
      this.item1 = item1;
      this.item2 = item2;
      this.item3 = item3;
    }

    [DebuggerNonUserCode]
    public int get_Item1()
    {
      return this.item1;
    }

    
    [CompilerGenerated]
    [DebuggerNonUserCode]
    public int Item1
    {
      [DebuggerNonUserCode] get
      {
        return this.item1;
      }
    }

    
    [CompilerGenerated]
    [DebuggerNonUserCode]
    public T Item2
    {
      [DebuggerNonUserCode] get
      {
        return this.item2;
      }
    }

    [DebuggerNonUserCode]
    public T get_Item2()
    {
      return this.item2;
    }

    [DebuggerNonUserCode]
    public FSharpList<RankedTree<T>> get_Item3()
    {
      return this.item3;
    }

    
    [CompilerGenerated]
    [DebuggerNonUserCode]
    public FSharpList<RankedTree<T>> Item3
    {
      [DebuggerNonUserCode] get
      {
        return this.item3;
      }
    }

    [DebuggerNonUserCode]
    public int get_Tag()
    {
      RankedTree<T> rankedTree = this;
      return 0;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public int Tag
    {
      [DebuggerNonUserCode] get
      {
        RankedTree<T> rankedTree = this;
        return 0;
      }
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    
    internal object __DebugDisplay()
    {
      return (object) ((FSharpFunc<RankedTree<T>, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<RankedTree<T>, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<RankedTree<T>, string>, Unit, string, string, string>("%+0.8A"))).Invoke(this);
    }

    [CompilerGenerated]
    public override string ToString()
    {
      return ((FSharpFunc<RankedTree<T>, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<RankedTree<T>, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<RankedTree<T>, string>, Unit, string, string, RankedTree<T>>("%+A"))).Invoke(this);
    }

    [CompilerGenerated]
    public virtual int CompareTo(RankedTree<T> obj)
    {
      if (this != null)
      {
        if (obj == null)
          return 1;
        RankedTree<T> rankedTree1 = this;
        RankedTree<T> rankedTree2 = this;
        RankedTree<T> rankedTree3 = obj;
        LanguagePrimitives.get_GenericComparer();
        int num1 = rankedTree2.item1;
        int num2 = rankedTree3.item1;
        int num3 = num1 >= num2 ? (num1 > num2 ? 1 : 0) : -1;
        if (num3 < 0 || num3 > 0)
          return num3;
        int num4 = LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<T>(LanguagePrimitives.get_GenericComparer(), (M0) rankedTree2.item2, (M0) rankedTree3.item2);
        if (num4 < 0 || num4 > 0)
          return num4;
        IComparer genericComparer = LanguagePrimitives.get_GenericComparer();
        return rankedTree2.item3.CompareTo((object) rankedTree3.item3, genericComparer);
      }
      return obj != null ? -1 : 0;
    }

    [CompilerGenerated]
    public virtual int CompareTo(object obj)
    {
      return this.CompareTo((RankedTree<T>) obj);
    }

    [CompilerGenerated]
    public virtual int CompareTo(object obj, IComparer comp)
    {
      RankedTree<T> rankedTree1 = (RankedTree<T>) obj;
      if (this != null)
      {
        if ((RankedTree<T>) obj == null)
          return 1;
        RankedTree<T> rankedTree2 = this;
        RankedTree<T> rankedTree3 = this;
        RankedTree<T> rankedTree4 = rankedTree1;
        int num1 = rankedTree3.item1;
        int num2 = rankedTree4.item1;
        int num3 = num1 >= num2 ? (num1 > num2 ? 1 : 0) : -1;
        if (num3 < 0 || num3 > 0)
          return num3;
        int num4 = LanguagePrimitives.HashCompare.GenericComparisonWithComparerIntrinsic<T>(comp, (M0) rankedTree3.item2, (M0) rankedTree4.item2);
        if (num4 < 0 || num4 > 0)
          return num4;
        IComparer comparer = comp;
        return rankedTree3.item3.CompareTo((object) rankedTree4.item3, comparer);
      }
      return (RankedTree<T>) obj != null ? -1 : 0;
    }

    [CompilerGenerated]
    public virtual int GetHashCode(IEqualityComparer comp)
    {
      if (this == null)
        return 0;
      RankedTree<T> rankedTree1 = this;
      RankedTree<T> rankedTree2 = this;
      int num1 = 0;
      int num2 = rankedTree2.item3.GetHashCode(comp) + ((num1 << 6) + (num1 >> 2)) - 1640531527;
      int num3 = LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<T>(comp, (M0) rankedTree2.item2) + ((num2 << 6) + (num2 >> 2)) - 1640531527;
      return rankedTree2.item1 + ((num3 << 6) + (num3 >> 2)) - 1640531527;
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
      RankedTree<T> rankedTree1 = obj as RankedTree<T>;
      if (rankedTree1 == null)
        return false;
      RankedTree<T> rankedTree2 = rankedTree1;
      RankedTree<T> rankedTree3 = this;
      RankedTree<T> rankedTree4 = this;
      RankedTree<T> rankedTree5 = rankedTree2;
      if (rankedTree4.item1 != rankedTree5.item1 || !LanguagePrimitives.HashCompare.GenericEqualityWithComparerIntrinsic<T>(comp, (M0) rankedTree4.item2, (M0) rankedTree5.item2))
        return false;
      IEqualityComparer equalityComparer = comp;
      return rankedTree4.item3.Equals((object) rankedTree5.item3, equalityComparer);
    }

    [CompilerGenerated]
    public virtual bool Equals(RankedTree<T> obj)
    {
      if (this == null)
        return obj == null;
      if (obj == null)
        return false;
      RankedTree<T> rankedTree1 = this;
      RankedTree<T> rankedTree2 = this;
      RankedTree<T> rankedTree3 = obj;
      if (rankedTree2.item1 == rankedTree3.item1 && LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<T>((M0) rankedTree2.item2, (M0) rankedTree3.item2))
        return LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<FSharpList<RankedTree<T>>>((M0) rankedTree2.item3, (M0) rankedTree3.item3);
      return false;
    }

    [CompilerGenerated]
    public override sealed bool Equals(object obj)
    {
      RankedTree<T> rankedTree = obj as RankedTree<T>;
      if (rankedTree != null)
        return this.Equals(rankedTree);
      return false;
    }
  }
}
