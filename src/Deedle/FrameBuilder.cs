// Decompiled with JetBrains decompiler
// Type: Deedle.FrameBuilder
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll



using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Deedle
{
  
  public static class FrameBuilder
  {
    
    [Serializable]
    public class Columns<R, C> : IEnumerable<KeyValuePair<C, ISeries<R>>>, IEnumerable
    {
      internal FSharpList<Tuple<C, ISeries<R>>> series;

      public Columns()
      {
        FrameBuilder.Columns<R, C> columns = this;
        this.series = FSharpList<Tuple<C, ISeries<R>>>.get_Empty();
      }

      public void Add(C key, ISeries<R> value)
      {
        this.series = FSharpList<Tuple<C, ISeries<R>>>.Cons(new Tuple<C, ISeries<R>>(key, value), this.series);
      }

      public Frame<R, C> Frame
      {
        get
        {
          return FFrameextensions.FrameofColumnsStatic<C, ISeries<R>, R>((IEnumerable<Tuple<C, ISeries<R>>>) ListModule.Reverse<Tuple<C, ISeries<R>>>((FSharpList<M0>) this.series));
        }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) ((IEnumerable<KeyValuePair<C, ISeries<R>>>) this).GetEnumerator();
      }

      IEnumerator<KeyValuePair<C, ISeries<R>>> IEnumerable<KeyValuePair<C, ISeries<R>>>.GetEnumerator()
      {
        return ((IEnumerable<KeyValuePair<C, ISeries<R>>>) SeqModule.Map<Tuple<C, ISeries<R>>, KeyValuePair<C, ISeries<R>>>((FSharpFunc<M0, M1>) new FrameBuilder.System\u002DCollections\u002DGeneric\u002DIEnumerable\u002DGetEnumerator<R, C>(), (IEnumerable<M0>) ListModule.Reverse<Tuple<C, ISeries<R>>>((FSharpList<M0>) this.series))).GetEnumerator();
      }
    }

    [Serializable]
    internal sealed class System\u002DCollections\u002DGeneric\u002DIEnumerable\u002DGetEnumerator<R, C> : FSharpFunc<Tuple<C, ISeries<R>>, KeyValuePair<C, ISeries<R>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal System\u002DCollections\u002DGeneric\u002DIEnumerable\u002DGetEnumerator()
      {
        base.ctor();
      }

      public virtual KeyValuePair<C, ISeries<R>> Invoke(Tuple<C, ISeries<R>> tupledArg)
      {
        return new KeyValuePair<C, ISeries<R>>(tupledArg.Item1, tupledArg.Item2);
      }
    }

    
    [Serializable]
    public class Rows<R, C> : IEnumerable<KeyValuePair<R, ISeries<C>>>, IEnumerable
    {
      internal FSharpList<Tuple<R, ISeries<C>>> series;

      public Rows()
      {
        FrameBuilder.Rows<R, C> rows = this;
        this.series = FSharpList<Tuple<R, ISeries<C>>>.get_Empty();
      }

      public void Add(R key, ISeries<C> value)
      {
        this.series = FSharpList<Tuple<R, ISeries<C>>>.Cons(new Tuple<R, ISeries<C>>(key, value), this.series);
      }

      public Frame<R, C> Frame
      {
        get
        {
          return FFrameextensions.FrameofRowsStatic<R, ISeries<C>, C>((IEnumerable<Tuple<R, ISeries<C>>>) ListModule.Reverse<Tuple<R, ISeries<C>>>((FSharpList<M0>) this.series));
        }
      }

      IEnumerator IEnumerable.GetEnumerator()
      {
        return (IEnumerator) ((IEnumerable<KeyValuePair<R, ISeries<C>>>) this).GetEnumerator();
      }

      IEnumerator<KeyValuePair<R, ISeries<C>>> IEnumerable<KeyValuePair<R, ISeries<C>>>.GetEnumerator()
      {
        return ((IEnumerable<KeyValuePair<R, ISeries<C>>>) SeqModule.Map<Tuple<R, ISeries<C>>, KeyValuePair<R, ISeries<C>>>((FSharpFunc<M0, M1>) new FrameBuilder.System\u002DCollections\u002DGeneric\u002DIEnumerable\u002DGetEnumerator<R, C>(), (IEnumerable<M0>) ListModule.Reverse<Tuple<R, ISeries<C>>>((FSharpList<M0>) this.series))).GetEnumerator();
      }
    }

    [Serializable]
    internal sealed class System\u002DCollections\u002DGeneric\u002DIEnumerable\u002DGetEnumerator<R, C> : FSharpFunc<Tuple<R, ISeries<C>>, KeyValuePair<R, ISeries<C>>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal System\u002DCollections\u002DGeneric\u002DIEnumerable\u002DGetEnumerator()
      {
        base.ctor();
      }

      public virtual KeyValuePair<R, ISeries<C>> Invoke(Tuple<R, ISeries<C>> tupledArg)
      {
        return new KeyValuePair<R, ISeries<C>>(tupledArg.Item1, tupledArg.Item2);
      }
    }
  }
}
