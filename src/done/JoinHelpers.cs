// Decompiled with JetBrains decompiler
// Type: Deedle.JoinHelpers
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Indices;
using Deedle.Internal;
using Deedle.Vectors;

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Deedle
{
  
  internal static class JoinHelpers
  {
    
    internal static Tuple<IIndex<a>, VectorConstruction> restrictToRowIndex<a>(Lookup lookup, IIndex<a> restriction, IIndex<a> sourceIndex, VectorConstruction vector)
    {
      if ((((lookup != Lookup.Exact ? 0 : (restriction.IsOrdered ? 1 : 0)) == 0 ? 0 : (sourceIndex.IsOrdered ? 1 : 0)) == 0 ? 0 : (!restriction.IsEmpty ? 1 : 0)) == 0)
        return new Tuple<IIndex<a>, VectorConstruction>(sourceIndex, vector);
      Tuple<a, a> keyRange = restriction.KeyRange;
      a a1 = keyRange.Item1;
      a a2 = keyRange.Item2;
      return sourceIndex.Builder.GetRange<a>(new Tuple<IIndex<a>, VectorConstruction>(sourceIndex, vector), new Tuple<FSharpOption<Tuple<a, BoundaryBehavior>>, FSharpOption<Tuple<a, BoundaryBehavior>>>(FSharpOption<Tuple<a, BoundaryBehavior>>.Some(new Tuple<a, BoundaryBehavior>(a1, BoundaryBehavior.get_Inclusive())), FSharpOption<Tuple<a, BoundaryBehavior>>.Some(new Tuple<a, BoundaryBehavior>(a2, BoundaryBehavior.get_Inclusive()))));
    }

    
    internal static VectorConstruction fillMissing(VectorConstruction vector, Lookup lookup)
    {
      switch (lookup)
      {
        case Lookup.Exact:
          return vector;
        case Lookup.ExactOrGreater:
          return VectorConstruction.NewFillMissing(vector, VectorFillMissing.NewDirection(Direction.Backward));
        case Lookup.ExactOrSmaller:
          return VectorConstruction.NewFillMissing(vector, VectorFillMissing.NewDirection(Direction.Forward));
        default:
          throw new InvalidOperationException("Lookup.Smaller and Lookup.Greater are not supported when joining");
      }
    }

    
    internal static Tuple<a, b, c> returnLeft<a, b, c>(a index, b left, c right)
    {
      return new Tuple<a, b, c>(index, left, right);
    }

    
    internal static Tuple<a, c, b> returnRight<a, b, c>(a index, b left, c right)
    {
      return new Tuple<a, c, b>(index, right, left);
    }

    
    internal static Tuple<IIndex<a>, VectorConstruction, VectorConstruction> createJoinTransformation<a>(IIndexBuilder indexBuilder, IIndexBuilder otherIndexBuilder, JoinKind kind, Lookup lookup, IIndex<a> thisIndex, IIndex<a> otherIndex, VectorConstruction thisVector, VectorConstruction otherVector)
    {
      if ((lookup == Lookup.Exact ? 0 : (kind == JoinKind.Inner ? 1 : 0)) != 0)
        throw new InvalidOperationException("Join/Zip - Inner join can only be used with Lookup.Exact.");
      if ((lookup == Lookup.Exact ? 0 : (kind == JoinKind.Outer ? 1 : 0)) != 0)
        throw new InvalidOperationException("Join/Zip - Outer join can only be used with Lookup.Exact.");
      if (((!thisIndex.IsOrdered ? 0 : (otherIndex.IsOrdered ? 1 : 0)) != 0 ? 0 : (lookup != Lookup.Exact ? 1 : 0)) != 0)
        throw new InvalidOperationException("Join/Zip - Lookup can be only used when joining/zipping ordered series/frames.");
      JoinKind input = kind;
      Tuple<Tuple<VectorConstruction, VectorConstruction, IIndex<a>, IIndex<a>, FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>>>>, JoinKind> tuple1 = MatchingHelpers.Let<Tuple<VectorConstruction, VectorConstruction, IIndex<a>, IIndex<a>, FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>>>>, JoinKind>(new Tuple<VectorConstruction, VectorConstruction, IIndex<a>, IIndex<a>, FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>>>>(thisVector, otherVector, thisIndex, otherIndex, (FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>>>) new JoinHelpers.createJoinTransformation<a>()), input);
      IIndex<a> sourceIndex;
      VectorConstruction vector1;
      FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>>> fsharpFunc1;
      IIndex<a> restriction;
      VectorConstruction vectorConstruction1;
      switch (tuple1.Item2)
      {
        case JoinKind.Left:
          VectorConstruction vectorConstruction2 = tuple1.Item1.Item1;
          IIndex<a> index1 = tuple1.Item1.Item3;
          FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>>> fsharpFunc2 = tuple1.Item1.Item5;
          VectorConstruction vectorConstruction3 = tuple1.Item1.Item2;
          sourceIndex = tuple1.Item1.Item4;
          vector1 = vectorConstruction3;
          fsharpFunc1 = fsharpFunc2;
          restriction = index1;
          vectorConstruction1 = vectorConstruction2;
          break;
        default:
          Tuple<Tuple<VectorConstruction, VectorConstruction, IIndex<a>, IIndex<a>, FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>>>>, JoinKind> tuple2 = MatchingHelpers.Let<Tuple<VectorConstruction, VectorConstruction, IIndex<a>, IIndex<a>, FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>>>>, JoinKind>(new Tuple<VectorConstruction, VectorConstruction, IIndex<a>, IIndex<a>, FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>>>>(thisVector, otherVector, thisIndex, otherIndex, (FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>>>) new JoinHelpers.createJoinTransformation<a>()), input);
          switch (tuple2.Item2)
          {
            case JoinKind.Right:
              vectorConstruction1 = tuple2.Item1.Item2;
              restriction = tuple2.Item1.Item4;
              fsharpFunc1 = tuple2.Item1.Item5;
              vector1 = tuple2.Item1.Item1;
              sourceIndex = tuple2.Item1.Item3;
              break;
            default:
              switch (input)
              {
                case JoinKind.Outer:
                  if ((!object.ReferenceEquals((object) thisIndex, (object) otherIndex) ? (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<IIndex<a>>((M0) thisIndex, (M0) otherIndex) ? 1 : 0) : 1) == 0)
                    goto default;
                  else
                    break;
                case JoinKind.Inner:
                  if ((!object.ReferenceEquals((object) thisIndex, (object) otherIndex) ? (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<IIndex<a>>((M0) thisIndex, (M0) otherIndex) ? 1 : 0) : 1) == 0)
                    goto default;
                  else
                    break;
                default:
                  switch (input)
                  {
                    case JoinKind.Outer:
                      return indexBuilder.Union<a>(new Tuple<IIndex<a>, VectorConstruction>(thisIndex, thisVector), new Tuple<IIndex<a>, VectorConstruction>(otherIndex, otherVector));
                    case JoinKind.Inner:
                      return indexBuilder.Intersect<a>(new Tuple<IIndex<a>, VectorConstruction>(thisIndex, thisVector), new Tuple<IIndex<a>, VectorConstruction>(otherIndex, otherVector));
                    default:
                      throw new InvalidOperationException("Join/Zip - Invalid JoinKind value!");
                  }
              }
              return new Tuple<IIndex<a>, VectorConstruction, VectorConstruction>(thisIndex, thisVector, otherVector);
          }
      }
      if ((lookup != Lookup.Exact ? 0 : (object.ReferenceEquals((object) restriction, (object) sourceIndex) ? 1 : 0)) != 0)
        return (Tuple<IIndex<a>, VectorConstruction, VectorConstruction>) FSharpFunc<IIndex<a>, VectorConstruction>.InvokeFast<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>((FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<M0, M1>>>) fsharpFunc1, restriction, vectorConstruction1, (M0) vector1);
      Tuple<IIndex<a>, VectorConstruction> rowIndex = JoinHelpers.restrictToRowIndex<a>(lookup, restriction, sourceIndex, vector1);
      VectorConstruction vector2 = rowIndex.Item2;
      IIndex<a> index2 = rowIndex.Item1;
      if ((lookup != Lookup.Exact ? 0 : (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<IIndex<a>>((M0) restriction, (M0) index2) ? 1 : 0)) != 0)
        return (Tuple<IIndex<a>, VectorConstruction, VectorConstruction>) FSharpFunc<IIndex<a>, VectorConstruction>.InvokeFast<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>((FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<M0, M1>>>) fsharpFunc1, restriction, vectorConstruction1, (M0) vector2);
      VectorConstruction vectorConstruction4 = JoinHelpers.fillMissing(vector2, lookup);
      VectorConstruction vectorConstruction5 = otherIndexBuilder.Reindex<a>(index2, restriction, lookup, vectorConstruction4, (FSharpFunc<long, bool>) new JoinHelpers.otherRowCmd());
      return (Tuple<IIndex<a>, VectorConstruction, VectorConstruction>) FSharpFunc<IIndex<a>, VectorConstruction>.InvokeFast<VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>((FSharpFunc<IIndex<a>, FSharpFunc<VectorConstruction, FSharpFunc<M0, M1>>>) fsharpFunc1, restriction, vectorConstruction1, (M0) vectorConstruction5);
    }

    [Serializable]
    internal sealed class createJoinTransformation<a> : OptimizedClosures.FSharpFunc<IIndex<a>, VectorConstruction, VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal createJoinTransformation()
      {
        base.ctor();
      }

      public virtual Tuple<IIndex<a>, VectorConstruction, VectorConstruction> Invoke(IIndex<a> index, VectorConstruction left, VectorConstruction right)
      {
        return new Tuple<IIndex<a>, VectorConstruction, VectorConstruction>(index, left, right);
      }
    }

    [Serializable]
    internal sealed class createJoinTransformation<a> : OptimizedClosures.FSharpFunc<IIndex<a>, VectorConstruction, VectorConstruction, Tuple<IIndex<a>, VectorConstruction, VectorConstruction>>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal createJoinTransformation()
      {
        base.ctor();
      }

      public virtual Tuple<IIndex<a>, VectorConstruction, VectorConstruction> Invoke(IIndex<a> index, VectorConstruction left, VectorConstruction right)
      {
        return new Tuple<IIndex<a>, VectorConstruction, VectorConstruction>(index, right, left);
      }
    }

    [Serializable]
    internal sealed class otherRowCmd : FSharpFunc<long, bool>
    {
      [CompilerGenerated]
      [DebuggerNonUserCode]
      internal otherRowCmd()
      {
        base.ctor();
      }

      public virtual bool Invoke(long _arg1)
      {
        return true;
      }
    }
  }
}
