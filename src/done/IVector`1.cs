// Decompiled with JetBrains decompiler
// Type: Deedle.IVector`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Vectors;

using System;
using System.Runtime.InteropServices;

namespace Deedle
{
    public interface IVector<T> : IVector
    {
        T GetValue([In] long obj0);

        T GetValueAtLocation([In] IVectorLocation obj0);

        T Data { get; }

        IVector<TNew> Select<TNew>([In] Func<IVectorLocation, Func<T, TNew>> obj0);

        IVector<TNew> Convert<TNew>([In] Func<T, TNew> obj0, [In] Func<TNew, T> obj1);
    }
}