// Decompiled with JetBrains decompiler
// Type: Deedle.ColumnSeries`2
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Indices;
using Deedle.Vectors;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Deedle
{
    [Serializable]
    public class ColumnSeries<TRowKey, TColumnKey> : Series<TColumnKey, ObjectSeries<TRowKey>>
    {
        internal new IVectorBuilder vectorBuilder;
        internal new IIndexBuilder indexBuilder;

        public ColumnSeries(IIndex<TColumnKey> index, IVector<ObjectSeries<TRowKey>> vector, IVectorBuilder vectorBuilder, IIndexBuilder indexBuilder)
          : base(index, vector, vectorBuilder, indexBuilder)
        {
            ColumnSeries<TRowKey, TColumnKey> columnSeries = this;
            this.vectorBuilder = vectorBuilder;
            this.indexBuilder = indexBuilder;
        }

        public ColumnSeries(Series<TColumnKey, ObjectSeries<TRowKey>> series)
          : this(series.Index, series.Vector, series.VectorBuilder, series.IndexBuilder)
        {
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Frame<TRowKey, TColumnKey> GetSlice(FSharpOption<TColumnKey> lo, FSharpOption<TColumnKey> hi)
        {
            return FrameUtils.fromColumns<TRowKey, TColumnKey, ObjectSeries<TRowKey>>(this.indexBuilder, this.vectorBuilder, base.GetSlice(lo, hi));
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Frame<TRowKey, TColumnKey> GetByLevel(ICustomLookup<TColumnKey> level)
        {
            return FrameUtils.fromColumns<TRowKey, TColumnKey, ObjectSeries<TRowKey>>(this.indexBuilder, this.vectorBuilder, base.GetByLevel(level));
        }

        public Frame<TRowKey, TColumnKey> this[IEnumerable<TColumnKey> items]
        {
            get
            {
                return FrameUtils.fromColumns<TRowKey, TColumnKey, ObjectSeries<TRowKey>>(this.indexBuilder, this.vectorBuilder, this.GetItems(items));
            }
        }

        public Frame<TRowKey, TColumnKey> this[ICustomLookup<TColumnKey> level]
        {
            get
            {
                return this.GetByLevel(level);
            }
        }
    }
}
