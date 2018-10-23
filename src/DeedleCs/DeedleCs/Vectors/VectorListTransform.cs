// Decompiled with JetBrains decompiler
// Type: Deedle.Vectors.VectorListTransform
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deedle.Vectors
{

    [Serializable]
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    public abstract class VectorListTransform : IEquatable<VectorListTransform>, IStructuralEquatable
    {
        internal VectorListTransform()
        {
        }

        public static VectorListTransform NewBinary(IBinaryTransform item)
        {
            return (VectorListTransform)new VectorListTransform.Binary(item);
        }

        public bool IsBinary
        {
            get
            {
                return this is VectorListTransform.Binary;
            }
        }

        public static VectorListTransform NewNary(INaryTransform item)
        {
            return (VectorListTransform)new VectorListTransform.Nary(item);
        }

        public bool IsNary
        {
            get
            {
                return this is VectorListTransform.Nary;
            }
        }

        public int Tag
        {
            get
            {
                return this is VectorListTransform.Nary ? 1 : 0;
            }
        }

        public override string ToString()
        {
            return ((Func<VectorListTransform, string>)ExtraTopLevelOperators.PrintFormatToString<Func<VectorListTransform, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<VectorListTransform, string>, Unit, string, string, VectorListTransform>("%+A"))).Invoke(this);
        }

        public virtual int GetHashCode(IEqualityComparer comp)
        {
            if (this == null)
                return 0;
            if (this is VectorListTransform.Binary)
            {
                VectorListTransform.Binary binary = (VectorListTransform.Binary)this;
                int num = 0;
                return LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<IBinaryTransform>(comp, (M0)binary.item) + ((num << 6) + (num >> 2)) - 1640531527;
            }
            VectorListTransform.Nary nary = (VectorListTransform.Nary)this;
            int num1 = 1;
            return LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<INaryTransform>(comp, (M0)nary.item) + ((num1 << 6) + (num1 >> 2)) - 1640531527;
        }

        public override sealed int GetHashCode()
        {
            return this.GetHashCode(LanguagePrimitives.get_GenericEqualityComparer());
        }

        public virtual bool Equals(object obj, IEqualityComparer comp)
        {
            if (this == null)
                return obj == null;
            VectorListTransform vectorListTransform1 = obj as VectorListTransform;
            if (vectorListTransform1 == null)
                return false;
            VectorListTransform vectorListTransform2 = vectorListTransform1;
            if ((!(this is VectorListTransform.Nary) ? 0 : 1) != (!(vectorListTransform2 is VectorListTransform.Nary) ? 0 : 1))
                return false;
            if (this is VectorListTransform.Binary)
            {
                VectorListTransform.Binary binary1 = (VectorListTransform.Binary)this;
                VectorListTransform.Binary binary2 = (VectorListTransform.Binary)vectorListTransform2;
                return LanguagePrimitives.HashCompare.GenericEqualityWithComparerIntrinsic<IBinaryTransform>(comp, (M0)binary1.item, (M0)binary2.item);
            }
            VectorListTransform.Nary nary1 = (VectorListTransform.Nary)this;
            VectorListTransform.Nary nary2 = (VectorListTransform.Nary)vectorListTransform2;
            return LanguagePrimitives.HashCompare.GenericEqualityWithComparerIntrinsic<INaryTransform>(comp, (M0)nary1.item, (M0)nary2.item);
        }

        public virtual bool Equals(VectorListTransform obj)
        {
            if (this == null)
                return obj == null;
            if (obj == null || (!(this is VectorListTransform.Nary) ? 0 : 1) != (!(obj is VectorListTransform.Nary) ? 0 : 1))
                return false;
            if (this is VectorListTransform.Binary)
                return LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<IBinaryTransform>((M0)((VectorListTransform.Binary)this).item, (M0)((VectorListTransform.Binary)obj).item);
            return LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<INaryTransform>((M0)((VectorListTransform.Nary)this).item, (M0)((VectorListTransform.Nary)obj).item);
        }

        public override sealed bool Equals(object obj)
        {
            VectorListTransform vectorListTransform = obj as VectorListTransform;
            if (vectorListTransform != null)
                return this.Equals(vectorListTransform);
            return false;
        }

        public static class Tags
        {
            public const int Binary = 0;
            public const int Nary = 1;
        }

        [Serializable]
        [SpecialName]
        public class Binary : VectorListTransform
        {
            internal readonly IBinaryTransform item;

            internal Binary(IBinaryTransform item)
            {
                this.item = item;
            }

            public IBinaryTransform Item
            {
                get
                {
                    return this.item;
                }
            }
        }

        [Serializable]
        [SpecialName]
        public class Nary : VectorListTransform
        {
            internal readonly INaryTransform item;

            internal Nary(INaryTransform item)
            {
                this.item = item;
            }

            public INaryTransform Item
            {
                get
                {
                    return this.item;
                }
            }
        }

    }
}
