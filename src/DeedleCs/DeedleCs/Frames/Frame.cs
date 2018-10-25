using Deedle;
using Deedle.Vectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeedleCs.Frames
{
    internal static class FrameHelpers
    {
        [Serializable]
        internal sealed class f1trans 
        {
            public IVectorBuilder vectorBuilder;
            public Addressing.IAddressingScheme scheme;
            public VectorConstruction rowCmd;
                       
            internal f1trans(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
            {
                this.vectorBuilder = vectorBuilder;
                this.scheme = scheme;
                this.rowCmd = rowCmd;
                Func = vec => Invoke(vec);
            }

            public IVector Invoke(IVector vector)
            {
                return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
            }

            public Func<IVector, IVector> Func { get; }
        }
               
        internal sealed class f2trans
        {
            public IVectorBuilder vectorBuilder;

            public Addressing.IAddressingScheme scheme;
                       
            public VectorConstruction rowCmd;
                       
            internal f2trans(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
            {
                this.vectorBuilder = vectorBuilder;
                this.scheme = scheme;
                this.rowCmd = rowCmd;
                Func = vec => Invoke(vec);
            }

            public IVector Invoke(IVector vector)
            {
                return VectorHelpers.transformColumn(this.vectorBuilder, this.scheme, this.rowCmd, vector);
            }

            public Func<IVector, IVector> Func { get; }
        }

        [Serializable]
        internal sealed class RenameColumns<TColumnKey> : Func<TColumnKey, TColumnKey>
        {
            public Func<TColumnKey, TColumnKey> objectArg;

            internal RenameColumns(Func<TColumnKey, TColumnKey> objectArg)
            {
                this.objectArg = objectArg;
            }

            public virtual TColumnKey Invoke(TColumnKey arg00)
            {
                return this.objectArg(arg00);
            }
        }

    }
}
