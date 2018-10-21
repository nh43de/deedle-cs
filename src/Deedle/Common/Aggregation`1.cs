// Decompiled with JetBrains decompiler
// Type: Deedle.Aggregation`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deedle
{
  [DebuggerDisplay("{__DebugDisplay(),nq}")]
  
  [Serializable]
  [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
  public abstract class Aggregation<K>
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    [CompilerGenerated]
    [DebuggerNonUserCode]
    internal readonly int _tag;

    [CompilerGenerated]
    [DebuggerNonUserCode]
    internal Aggregation(int _tag)
    {
      this._tag = _tag;
    }

    
    public static Aggregation<K> NewWindowSize(int item1, Boundary item2)
    {
      return (Aggregation<K>) new Aggregation<K>.WindowSize(item1, item2);
    }

    [DebuggerNonUserCode]
    public bool get_IsWindowSize()
    {
      return this.get_Tag() == 0;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsWindowSize
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 0;
      }
    }

    
    public static Aggregation<K> NewChunkSize(int item1, Boundary item2)
    {
      return (Aggregation<K>) new Aggregation<K>.ChunkSize(item1, item2);
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsChunkSize
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 1;
      }
    }

    [DebuggerNonUserCode]
    public bool get_IsChunkSize()
    {
      return this.get_Tag() == 1;
    }

    
    public static Aggregation<K> NewWindowWhile(FSharpFunc<K, FSharpFunc<K, bool>> item)
    {
      return (Aggregation<K>) new Aggregation<K>.WindowWhile(item);
    }

    [DebuggerNonUserCode]
    public bool get_IsWindowWhile()
    {
      return this.get_Tag() == 2;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsWindowWhile
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 2;
      }
    }

    
    public static Aggregation<K> NewChunkWhile(FSharpFunc<K, FSharpFunc<K, bool>> item)
    {
      return (Aggregation<K>) new Aggregation<K>.ChunkWhile(item);
    }

    [DebuggerNonUserCode]
    public bool get_IsChunkWhile()
    {
      return this.get_Tag() == 3;
    }

    [CompilerGenerated]
    [DebuggerNonUserCode]
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsChunkWhile
    {
      [DebuggerNonUserCode] get
      {
        return this.get_Tag() == 3;
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
      return (object) ((FSharpFunc<Aggregation<K>, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<Aggregation<K>, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<Aggregation<K>, string>, Unit, string, string, string>("%+0.8A"))).Invoke(this);
    }

    [CompilerGenerated]
    public override string ToString()
    {
      return ((FSharpFunc<Aggregation<K>, string>) ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<Aggregation<K>, string>>((PrintfFormat<M0, Unit, string, string>) new PrintfFormat<FSharpFunc<Aggregation<K>, string>, Unit, string, string, Aggregation<K>>("%+A"))).Invoke(this);
    }

    public static class Tags
    {
      public const int WindowSize = 0;
      public const int ChunkSize = 1;
      public const int WindowWhile = 2;
      public const int ChunkWhile = 3;
    }

    [DebuggerTypeProxy(typeof (Aggregation<>.WindowSizeDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class WindowSize : Aggregation<K>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly int item1;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly Boundary item2;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowSize(int item1, Boundary item2)
        : base(0)
      {
        this.item1 = item1;
        this.item2 = item2;
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

      [DebuggerNonUserCode]
      public Boundary get_Item2()
      {
        return this.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Boundary Item2
      {
        [DebuggerNonUserCode] get
        {
          return this.item2;
        }
      }
    }

    [DebuggerTypeProxy(typeof (Aggregation<>.ChunkSizeDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class ChunkSize : Aggregation<K>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly int item1;
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly Boundary item2;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkSize(int item1, Boundary item2)
        : base(1)
      {
        this.item1 = item1;
        this.item2 = item2;
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

      [DebuggerNonUserCode]
      public Boundary get_Item2()
      {
        return this.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Boundary Item2
      {
        [DebuggerNonUserCode] get
        {
          return this.item2;
        }
      }
    }

    [DebuggerTypeProxy(typeof (Aggregation<>.WindowWhileDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class WindowWhile : Aggregation<K>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly FSharpFunc<K, FSharpFunc<K, bool>> item;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal WindowWhile(FSharpFunc<K, FSharpFunc<K, bool>> item)
        : base(2)
      {
        this.item = item;
      }

      [DebuggerNonUserCode]
      public FSharpFunc<K, FSharpFunc<K, bool>> get_Item()
      {
        return this.item;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<K, FSharpFunc<K, bool>> Item
      {
        [DebuggerNonUserCode] get
        {
          return this.item;
        }
      }
    }

    [DebuggerTypeProxy(typeof (Aggregation<>.ChunkWhileDebugTypeProxy))]
    [DebuggerDisplay("{__DebugDisplay(),nq}")]
    [Serializable]
    
    public class ChunkWhile : Aggregation<K>
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal readonly FSharpFunc<K, FSharpFunc<K, bool>> item;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal ChunkWhile(FSharpFunc<K, FSharpFunc<K, bool>> item)
        : base(3)
      {
        this.item = item;
      }

      [DebuggerNonUserCode]
      public FSharpFunc<K, FSharpFunc<K, bool>> get_Item()
      {
        return this.item;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<K, FSharpFunc<K, bool>> Item
      {
        [DebuggerNonUserCode] get
        {
          return this.item;
        }
      }
    }

    
    internal class WindowSizeDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Aggregation<K>.WindowSize _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public WindowSizeDebugTypeProxy(Aggregation<K>.WindowSize obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public int get_Item1()
      {
        return this._obj.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int Item1
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item1;
        }
      }

      [DebuggerNonUserCode]
      public Boundary get_Item2()
      {
        return this._obj.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Boundary Item2
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item2;
        }
      }
    }

    
    internal class ChunkSizeDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Aggregation<K>.ChunkSize _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public ChunkSizeDebugTypeProxy(Aggregation<K>.ChunkSize obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public int get_Item1()
      {
        return this._obj.item1;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public int Item1
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item1;
        }
      }

      [DebuggerNonUserCode]
      public Boundary get_Item2()
      {
        return this._obj.item2;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public Boundary Item2
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item2;
        }
      }
    }

    
    internal class WindowWhileDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Aggregation<K>.WindowWhile _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public WindowWhileDebugTypeProxy(Aggregation<K>.WindowWhile obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public FSharpFunc<K, FSharpFunc<K, bool>> get_Item()
      {
        return this._obj.item;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<K, FSharpFunc<K, bool>> Item
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item;
        }
      }
    }

    
    internal class ChunkWhileDebugTypeProxy
    {
      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal Aggregation<K>.ChunkWhile _obj;

      [CompilerGenerated]
      [DebuggerNonUserCode]
      public ChunkWhileDebugTypeProxy(Aggregation<K>.ChunkWhile obj)
      {
        this._obj = obj;
      }

      [DebuggerNonUserCode]
      public FSharpFunc<K, FSharpFunc<K, bool>> get_Item()
      {
        return this._obj.item;
      }

      
      [CompilerGenerated]
      [DebuggerNonUserCode]
      public FSharpFunc<K, FSharpFunc<K, bool>> Item
      {
        [DebuggerNonUserCode] get
        {
          return this._obj.item;
        }
      }
    }
  }
}
