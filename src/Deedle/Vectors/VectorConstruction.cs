// Decompiled with JetBrains decompiler
// Type: Deedle.Vectors.VectorConstruction
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using Microsoft.FSharp.Control;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deedle.Vectors
{
  [DebuggerDisplay("{__DebugDisplay(),nq}")]
  
  [Serializable]
  [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
  public abstract class VectorConstruction
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [CompilerGenerated]
    [DebuggerNonUserCode]
    internal readonly int _tag;

    [CompilerGenerated]
    [DebuggerNonUserCode]
    internal VectorConstruction(int _tag)
    {
      this._tag = _tag;
    }

    
    public static VectorConstruction NewReturn(int item)
    {
      return (VectorConstruction) new VectorConstruction.Return(item);
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsReturn
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 0;
      }
    }

    [DebuggerNonUserCode]
    public bool get_IsReturn()
    {
      return this.get_Tag() == 0;
    }

    
    public static VectorConstruction NewEmpty(long item)
    {
      return (VectorConstruction) new VectorConstruction.Empty(item);
    }

    [DebuggerNonUserCode]
    public bool get_IsEmpty()
    {
      return this.get_Tag() == 1;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsEmpty
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 1;
      }
    }

    
    public static VectorConstruction NewRelocate(VectorConstruction item1, long item2, IEnumerable<Tuple<long, long>> item3)
    {
      return (VectorConstruction) new VectorConstruction.Relocate(item1, item2, item3);
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsRelocate
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 2;
      }
    }

    [DebuggerNonUserCode]
    public bool get_IsRelocate()
    {
      return this.get_Tag() == 2;
    }

    
    public static VectorConstruction NewDropRange(VectorConstruction item1, RangeRestriction<long> item2)
    {
      return (VectorConstruction) new VectorConstruction.DropRange(item1, item2);
    }

    [DebuggerNonUserCode]
    public bool get_IsDropRange()
    {
      return this.get_Tag() == 3;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsDropRange
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 3;
      }
    }

    
    public static VectorConstruction NewGetRange(VectorConstruction item1, RangeRestriction<long> item2)
    {
      return (VectorConstruction) new VectorConstruction.GetRange(item1, item2);
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsGetRange
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 4;
      }
    }

    [DebuggerNonUserCode]
    public bool get_IsGetRange()
    {
      return this.get_Tag() == 4;
    }

    
    public static VectorConstruction NewAppend(VectorConstruction item1, VectorConstruction item2)
    {
      return (VectorConstruction) new VectorConstruction.Append(item1, item2);
    }

    [DebuggerNonUserCode]
    public bool get_IsAppend()
    {
      return this.get_Tag() == 5;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsAppend
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 5;
      }
    }
    
    public static VectorConstruction NewCombine(Lazy<long> item1, FSharpList<VectorConstruction> item2, VectorListTransform item3)
    {
      return (VectorConstruction) new VectorConstruction.Combine(item1, item2, item3);
    }

    [DebuggerNonUserCode]
    public bool get_IsCombine()
    {
      return this.get_Tag() == 6;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsCombine
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 6;
      }
    }

    
    public static VectorConstruction NewFillMissing(VectorConstruction item1, VectorFillMissing item2)
    {
      return (VectorConstruction) new VectorConstruction.FillMissing(item1, item2);
    }

    [DebuggerNonUserCode]
    public bool get_IsFillMissing()
    {
      return this.get_Tag() == 7;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsFillMissing
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 7;
      }
    }

    
    public static VectorConstruction NewCustomCommand(FSharpList<VectorConstruction> item1, FSharpFunc<FSharpList<IVector>, IVector> item2)
    {
      return (VectorConstruction) new VectorConstruction.CustomCommand(item1, item2);
    }

    [DebuggerNonUserCode]
    public bool get_IsCustomCommand()
    {
      return this.get_Tag() == 8;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsCustomCommand
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 8;
      }
    }

    
    public static VectorConstruction NewAsyncCustomCommand(FSharpList<VectorConstruction> item1, FSharpFunc<FSharpList<IVector>, FSharpAsync<IVector>> item2)
    {
      return (VectorConstruction) new VectorConstruction.AsyncCustomCommand(item1, item2);
    }

    [DebuggerNonUserCode]
    public bool get_IsAsyncCustomCommand()
    {
      return this.get_Tag() == 9;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsAsyncCustomCommand
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 9;
      }
    }

    [DebuggerNonUserCode]
    public int get_Tag()
    {
      return this._tag;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public int Tag
    {
      [DebuggerNonUserCode] get
      {
        return this._tag;
      }
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    
    internal object __DebugDisplay()
    {
      return (object) ((FSharpFunc<VectorConstruction, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<VectorConstruction, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<VectorConstruction, string>, Unit, string, string, string>("%+0.8A"))).Invoke(this);
    }

    [CompilerGenerated]
    public override string ToString()
    {
      return ((FSharpFunc<VectorConstruction, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<VectorConstruction, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<VectorConstruction, string>, Unit, string, string, VectorConstruction>("%+A"))).Invoke(this);
    }

    public static class Tags
    {
      public const int Return = 0;
      public const int Empty = 1;
      public const int Relocate = 2;
      public const int DropRange = 3;
      public const int GetRange = 4;
      public const int Append = 5;
      public const int Combine = 6;
      public const int FillMissing = 7;
      public const int CustomCommand = 8;
      public const int AsyncCustomCommand = 9;
    }

    [DebuggerTypeProxy(typeof (VectorConstruction.ReturnDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class Return : VectorConstruction
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly int item;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Return(int item)
        : base(0)
      {
        this.item = item;
      }

      [DebuggerNonUserCode]
      public int get_Item()
      {
        return this.item;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int Item
      {
        [DebuggerNonUserCode] get
        {
          return this.item;
        }
      }
    }

    [DebuggerTypeProxy(typeof (VectorConstruction.EmptyDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class Empty : VectorConstruction
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly long item;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Empty(long item)
        : base(1)
      {
        this.item = item;
      }

      [DebuggerNonUserCode]
      public long get_Item()
      {
        return this.item;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long Item
      {
        [DebuggerNonUserCode] get
        {
          return this.item;
        }
      }
    }

    [DebuggerTypeProxy(typeof (VectorConstruction.RelocateDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class Relocate : VectorConstruction
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly VectorConstruction item1;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly long item2;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly IEnumerable<Tuple<long, long>> item3;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Relocate(VectorConstruction item1, long item2, IEnumerable<Tuple<long, long>> item3)
        : base(2)
      {
        this.item1 = item1;
        this.item2 = item2;
        this.item3 = item3;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item1
      {
        [DebuggerNonUserCode] get
        {
          return this.item1;
        }
      }

      [DebuggerNonUserCode]
      public VectorConstruction get_Item1()
      {
        return this.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long Item2
      {
        [DebuggerNonUserCode] get
        {
          return this.item2;
        }
      }

      [DebuggerNonUserCode]
      public long get_Item2()
      {
        return this.item2;
      }

      [DebuggerNonUserCode]
      public IEnumerable<Tuple<long, long>> get_Item3()
      {
        return this.item3;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerable<Tuple<long, long>> Item3
      {
        [DebuggerNonUserCode] get
        {
          return this.item3;
        }
      }
    }

    [DebuggerTypeProxy(typeof (VectorConstruction.DropRangeDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class DropRange : VectorConstruction
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly VectorConstruction item1;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly RangeRestriction<long> item2;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal DropRange(VectorConstruction item1, RangeRestriction<long> item2)
        : base(3)
      {
        this.item1 = item1;
        this.item2 = item2;
      }

      [DebuggerNonUserCode]
      public VectorConstruction get_Item1()
      {
        return this.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item1
      {
        [DebuggerNonUserCode] get
        {
          return this.item1;
        }
      }

      [DebuggerNonUserCode]
      public RangeRestriction<long> get_Item2()
      {
        return this.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public RangeRestriction<long> Item2
      {
        [DebuggerNonUserCode] get
        {
          return this.item2;
        }
      }
    }

    [DebuggerTypeProxy(typeof (VectorConstruction.GetRangeDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class GetRange : VectorConstruction
    {
      internal readonly VectorConstruction item1;
      internal readonly RangeRestriction<long> item2;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal GetRange(VectorConstruction item1, RangeRestriction<long> item2)
        : base(4)
      {
        this.item1 = item1;
        this.item2 = item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item1
      {
        [DebuggerNonUserCode] get
        {
          return this.item1;
        }
      }

    
      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public RangeRestriction<long> Item2
      {
        [DebuggerNonUserCode] get
        {
          return this.item2;
        }
      }
    }

    [DebuggerTypeProxy(typeof (VectorConstruction.AppendDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable
    public class Append : VectorConstruction
    {
      internal readonly VectorConstruction item1;
      internal readonly VectorConstruction item2;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Append(VectorConstruction item1, VectorConstruction item2)
        : base(5)
      {
        this.item1 = item1;
        this.item2 = item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item1
      {
        [DebuggerNonUserCode] get
        {
          return this.item1;
        }
      }

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item2
      {
        [DebuggerNonUserCode] get
        {
          return this.item2;
        }
      }
    }

    [DebuggerTypeProxy(typeof (VectorConstruction.CombineDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    public class Combine : VectorConstruction
    {
      internal readonly Lazy<long> item1;

      internal readonly FSharpList<VectorConstruction> item2;
      
      internal readonly VectorListTransform item3;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Combine(Lazy<long> item1, FSharpList<VectorConstruction> item2, VectorListTransform item3)
        : base(6)
      {
        this.item1 = item1;
        this.item2 = item2;
        this.item3 = item3;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Lazy<long> Item1
      {
        [DebuggerNonUserCode] get
        {
          return this.item1;
        }
      }

      [DebuggerNonUserCode]
      public Lazy<long> get_Item1()
      {
        return this.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> Item2
      {
        [DebuggerNonUserCode] get
        {
          return this.item2;
        }
      }

      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> get_Item2()
      {
        return this.item2;
      }

      [DebuggerNonUserCode]
      public VectorListTransform get_Item3()
      {
        return this.item3;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorListTransform Item3
      {
        [DebuggerNonUserCode] get
        {
          return this.item3;
        }
      }
    }

    [DebuggerTypeProxy(typeof (VectorConstruction.FillMissingDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class FillMissing : VectorConstruction
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly VectorConstruction item1;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly VectorFillMissing item2;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal FillMissing(VectorConstruction item1, VectorFillMissing item2)
        : base(7)
      {
        this.item1 = item1;
        this.item2 = item2;
      }

      [DebuggerNonUserCode]
      public VectorConstruction get_Item1()
      {
        return this.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item1
      {
        [DebuggerNonUserCode] get
        {
          return this.item1;
        }
      }

      [DebuggerNonUserCode]
      public VectorFillMissing get_Item2()
      {
        return this.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorFillMissing Item2
      {
        [DebuggerNonUserCode] get
        {
          return this.item2;
        }
      }
    }

    [DebuggerTypeProxy(typeof (VectorConstruction.CustomCommandDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class CustomCommand : VectorConstruction
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly FSharpList<VectorConstruction> item1;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly FSharpFunc<FSharpList<IVector>, IVector> item2;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal CustomCommand(FSharpList<VectorConstruction> item1, FSharpFunc<FSharpList<IVector>, IVector> item2)
        : base(8)
      {
        this.item1 = item1;
        this.item2 = item2;
      }

      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> get_Item1()
      {
        return this.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> Item1
      {
        [DebuggerNonUserCode] get
        {
          return this.item1;
        }
      }

      [DebuggerNonUserCode]
      public FSharpFunc<FSharpList<IVector>, IVector> get_Item2()
      {
        return this.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<FSharpList<IVector>, IVector> Item2
      {
        [DebuggerNonUserCode] get
        {
          return this.item2;
        }
      }
    }

    [DebuggerTypeProxy(typeof (VectorConstruction.AsyncCustomCommandDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class AsyncCustomCommand : VectorConstruction
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly FSharpList<VectorConstruction> item1;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly FSharpFunc<FSharpList<IVector>, FSharpAsync<IVector>> item2;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal AsyncCustomCommand(FSharpList<VectorConstruction> item1, FSharpFunc<FSharpList<IVector>, FSharpAsync<IVector>> item2)
        : base(9)
      {
        this.item1 = item1;
        this.item2 = item2;
      }

      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> get_Item1()
      {
        return this.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> Item1
      {
        [DebuggerNonUserCode] get
        {
          return this.item1;
        }
      }

      [DebuggerNonUserCode]
      public FSharpFunc<FSharpList<IVector>, FSharpAsync<IVector>> get_Item2()
      {
        return this.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<FSharpList<IVector>, FSharpAsync<IVector>> Item2
      {
        [DebuggerNonUserCode] get
        {
          return this.item2;
        }
      }
    }

    
    internal class ReturnDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal VectorConstruction.Return _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public ReturnDebugTypeProxy(VectorConstruction.Return obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public int get_Item()
      {
        return this._obj.item;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int Item
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item;
        }
      }
    }

    
    internal class EmptyDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal VectorConstruction.Empty _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public EmptyDebugTypeProxy(VectorConstruction.Empty obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public long get_Item()
      {
        return this._obj.item;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long Item
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item;
        }
      }
    }

    
    internal class RelocateDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal VectorConstruction.Relocate _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public RelocateDebugTypeProxy(VectorConstruction.Relocate obj)
      {
        this._obj = obj;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item1
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item1;
        }
      }

      [DebuggerNonUserCode]
      public VectorConstruction get_Item1()
      {
        return this._obj.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public long Item2
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item2;
        }
      }

      [DebuggerNonUserCode]
      public long get_Item2()
      {
        return this._obj.item2;
      }

      [DebuggerNonUserCode]
      public IEnumerable<Tuple<long, long>> get_Item3()
      {
        return this._obj.item3;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public IEnumerable<Tuple<long, long>> Item3
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item3;
        }
      }
    }

    
    internal class DropRangeDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal VectorConstruction.DropRange _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public DropRangeDebugTypeProxy(VectorConstruction.DropRange obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public VectorConstruction get_Item1()
      {
        return this._obj.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item1
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item1;
        }
      }

      [DebuggerNonUserCode]
      public RangeRestriction<long> get_Item2()
      {
        return this._obj.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public RangeRestriction<long> Item2
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item2;
        }
      }
    }

    
    internal class GetRangeDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal VectorConstruction.GetRange _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public GetRangeDebugTypeProxy(VectorConstruction.GetRange obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public VectorConstruction get_Item1()
      {
        return this._obj.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item1
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item1;
        }
      }

      [DebuggerNonUserCode]
      public RangeRestriction<long> get_Item2()
      {
        return this._obj.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public RangeRestriction<long> Item2
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item2;
        }
      }
    }

    
    internal class AppendDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal VectorConstruction.Append _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public AppendDebugTypeProxy(VectorConstruction.Append obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public VectorConstruction get_Item1()
      {
        return this._obj.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item1
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item1;
        }
      }

      [DebuggerNonUserCode]
      public VectorConstruction get_Item2()
      {
        return this._obj.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item2
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item2;
        }
      }
    }

    
    internal class CombineDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal VectorConstruction.Combine _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public CombineDebugTypeProxy(VectorConstruction.Combine obj)
      {
        this._obj = obj;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Lazy<long> Item1
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item1;
        }
      }

      [DebuggerNonUserCode]
      public Lazy<long> get_Item1()
      {
        return this._obj.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> Item2
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item2;
        }
      }

      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> get_Item2()
      {
        return this._obj.item2;
      }

      [DebuggerNonUserCode]
      public VectorListTransform get_Item3()
      {
        return this._obj.item3;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorListTransform Item3
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item3;
        }
      }
    }

    
    internal class FillMissingDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal VectorConstruction.FillMissing _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FillMissingDebugTypeProxy(VectorConstruction.FillMissing obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public VectorConstruction get_Item1()
      {
        return this._obj.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorConstruction Item1
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item1;
        }
      }

      [DebuggerNonUserCode]
      public VectorFillMissing get_Item2()
      {
        return this._obj.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public VectorFillMissing Item2
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item2;
        }
      }
    }

    
    internal class CustomCommandDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal VectorConstruction.CustomCommand _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public CustomCommandDebugTypeProxy(VectorConstruction.CustomCommand obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> get_Item1()
      {
        return this._obj.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> Item1
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item1;
        }
      }

      [DebuggerNonUserCode]
      public FSharpFunc<FSharpList<IVector>, IVector> get_Item2()
      {
        return this._obj.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<FSharpList<IVector>, IVector> Item2
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item2;
        }
      }
    }

    
    internal class AsyncCustomCommandDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal VectorConstruction.AsyncCustomCommand _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public AsyncCustomCommandDebugTypeProxy(VectorConstruction.AsyncCustomCommand obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> get_Item1()
      {
        return this._obj.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpList<VectorConstruction> Item1
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item1;
        }
      }

      [DebuggerNonUserCode]
      public FSharpFunc<FSharpList<IVector>, FSharpAsync<IVector>> get_Item2()
      {
        return this._obj.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<FSharpList<IVector>, FSharpAsync<IVector>> Item2
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item2;
        }
      }
    }
  }
}
