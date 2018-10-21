// Decompiled with JetBrains decompiler
// Type: Deedle.RangeRestriction`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deedle
{
    [Serializable]
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    public abstract class RangeRestriction<TAddress> : IEquatable<RangeRestriction<TAddress>>, IStructuralEquatable
    {

        internal readonly int _tag;



        internal RangeRestriction(int _tag)
        {
            this._tag = _tag;
        }

        public static RangeRestriction<TAddress> NewFixed(TAddress item1, TAddress item2)
        {
            return (RangeRestriction<TAddress>)new RangeRestriction<TAddress>.Fixed(item1, item2);
        }

        public bool IsFixed
        {
            get
            {
                return this.Tag == 0;
            }
        }

        public static RangeRestriction<TAddress> NewStart(long item)
        {
            return (RangeRestriction<TAddress>)new RangeRestriction<TAddress>.Start(item);
        }

        public bool IsStart
        {

            get
            {
                return this.Tag == 1;
            }
        }

        public static RangeRestriction<TAddress> NewEnd(long item)
        {
            return (RangeRestriction<TAddress>)new RangeRestriction<TAddress>.End(item);
        }

        public bool IsEnd
        {
            get
            {
                return this.Tag == 2;
            }
        }

        public static RangeRestriction<TAddress> NewCustom(IRangeRestriction<TAddress> item)
        {
            return (RangeRestriction<TAddress>)new RangeRestriction<TAddress>.Custom(item);
        }

        public bool IsCustom
        {
            get
            {
                return this.Tag == 3;
            }
        }
        
        public int Tag
        {

            get
            {
                return this._tag;
            }
        }
        
        public override string ToString()
        {
            return ((FSharpFunc<RangeRestriction<TAddress>, string>)ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<RangeRestriction<TAddress>, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<FSharpFunc<RangeRestriction<TAddress>, string>, Unit, string, string, RangeRestriction<TAddress>>("%+A"))).Invoke(this);
        }

        public virtual int GetHashCode(IEqualityComparer comp)
        {
            if (this == null)
                return 0;
            switch (this.Tag)
            {
                case 1:
                    RangeRestriction<TAddress>.Start start = (RangeRestriction<TAddress>.Start)this;
                    int num1 = 1;
                    int num2 = -1640531527;
                    long num3 = start.item;
                    int num4 = ((int)num3 ^ (int)(num3 >> 32)) + ((num1 << 6) + (num1 >> 2));
                    return num2 + num4;
                case 2:
                    RangeRestriction<TAddress>.End end = (RangeRestriction<TAddress>.End)this;
                    int num5 = 2;
                    int num6 = -1640531527;
                    long num7 = end.item;
                    int num8 = ((int)num7 ^ (int)(num7 >> 32)) + ((num5 << 6) + (num5 >> 2));
                    return num6 + num8;
                case 3:
                    RangeRestriction<TAddress>.Custom custom = (RangeRestriction<TAddress>.Custom)this;
                    int num9 = 3;
                    return LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<IRangeRestriction<TAddress>>(comp, (M0)custom.item) + ((num9 << 6) + (num9 >> 2)) - 1640531527;
                default:
                    RangeRestriction<TAddress>.Fixed @fixed = (RangeRestriction<TAddress>.Fixed)this;
                    int num10 = 0;
                    int num11 = LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<TAddress>(comp, (M0)@fixed.item2) + ((num10 << 6) + (num10 >> 2)) - 1640531527;
                    return LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<TAddress>(comp, (M0)@fixed.item1) + ((num11 << 6) + (num11 >> 2)) - 1640531527;
            }
        }
        
        public override sealed int GetHashCode()
        {
            return this.GetHashCode(LanguagePrimitives.get_GenericEqualityComparer());
        }


        public virtual bool Equals(object obj, IEqualityComparer comp)
        {
            if (this == null)
                return obj == null;
            RangeRestriction<TAddress> rangeRestriction1 = obj as RangeRestriction<TAddress>;
            if (rangeRestriction1 == null)
                return false;
            RangeRestriction<TAddress> rangeRestriction2 = rangeRestriction1;
            if (this._tag != rangeRestriction2._tag)
                return false;
            switch (this.Tag)
            {
                case 1:
                    return ((RangeRestriction<TAddress>.Start)this).item == ((RangeRestriction<TAddress>.Start)rangeRestriction2).item;
                case 2:
                    return ((RangeRestriction<TAddress>.End)this).item == ((RangeRestriction<TAddress>.End)rangeRestriction2).item;
                case 3:
                    RangeRestriction<TAddress>.Custom custom1 = (RangeRestriction<TAddress>.Custom)this;
                    RangeRestriction<TAddress>.Custom custom2 = (RangeRestriction<TAddress>.Custom)rangeRestriction2;
                    return LanguagePrimitives.HashCompare.GenericEqualityWithComparerIntrinsic<IRangeRestriction<TAddress>>(comp, (M0)custom1.item, (M0)custom2.item);
                default:
                    RangeRestriction<TAddress>.Fixed fixed1 = (RangeRestriction<TAddress>.Fixed)this;
                    RangeRestriction<TAddress>.Fixed fixed2 = (RangeRestriction<TAddress>.Fixed)rangeRestriction2;
                    if (LanguagePrimitives.HashCompare.GenericEqualityWithComparerIntrinsic<TAddress>(comp, (M0)fixed1.item1, (M0)fixed2.item1))
                        return LanguagePrimitives.HashCompare.GenericEqualityWithComparerIntrinsic<TAddress>(comp, (M0)fixed1.item2, (M0)fixed2.item2);
                    return false;
            }
        }


        public virtual bool Equals(RangeRestriction<TAddress> obj)
        {
            if (this == null)
                return obj == null;
            if (obj == null || this._tag != obj._tag)
                return false;
            switch (this.Tag)
            {
                case 1:
                    return ((RangeRestriction<TAddress>.Start)this).item == ((RangeRestriction<TAddress>.Start)obj).item;
                case 2:
                    return ((RangeRestriction<TAddress>.End)this).item == ((RangeRestriction<TAddress>.End)obj).item;
                case 3:
                    return LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<IRangeRestriction<TAddress>>((M0)((RangeRestriction<TAddress>.Custom)this).item, (M0)((RangeRestriction<TAddress>.Custom)obj).item);
                default:
                    RangeRestriction<TAddress>.Fixed fixed1 = (RangeRestriction<TAddress>.Fixed)this;
                    RangeRestriction<TAddress>.Fixed fixed2 = (RangeRestriction<TAddress>.Fixed)obj;
                    if (LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<TAddress>((M0)fixed1.item1, (M0)fixed2.item1))
                        return LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<TAddress>((M0)fixed1.item2, (M0)fixed2.item2);
                    return false;
            }
        }
        
        public override sealed bool Equals(object obj)
        {
            RangeRestriction<TAddress> rangeRestriction = obj as RangeRestriction<TAddress>;
            if (rangeRestriction != null)
                return this.Equals(rangeRestriction);
            return false;
        }

        public RangeRestriction<a> Select<a>(Func<TAddress, a> f)
        {
            return RangeRestriction.map<TAddress, a>((FSharpFunc<TAddress, a>) new Address.Select<TAddress, a>(f), this);
        }

        public static class Tags
        {
            public const int Fixed = 0;
            public const int Start = 1;
            public const int End = 2;
            public const int Custom = 3;
        }

        [Serializable]
        [SpecialName]
        public class Fixed : RangeRestriction<TAddress>
        {
            internal readonly TAddress item1;
            internal readonly TAddress item2;

            internal Fixed(TAddress item1, TAddress item2)
              : base(0)
            {
                this.item1 = item1;
                this.item2 = item2;
            }

            public TAddress Item1
            {
                get
                {
                    return this.item1;
                }
            }

            public TAddress Item2
            {
                get
                {
                    return this.item2;
                }
            }
        }

        [Serializable]
        [SpecialName]
        public class Start : RangeRestriction<TAddress>
        {
            internal readonly long item;

            internal Start(long item)
              : base(1)
            {
                this.item = item;
            }

            public long Item
            {

                get
                {
                    return this.item;
                }
            }
        }

        [Serializable]
        [SpecialName]
        public class End : RangeRestriction<TAddress>
        {
            internal readonly long item;

            internal End(long item)
              : base(2)
            {
                this.item = item;
            }


            public long Item
            {

                get
                {
                    return this.item;
                }
            }
        }


        [Serializable]
        [SpecialName]
        public class Custom : RangeRestriction<TAddress>
        {
            internal readonly IRangeRestriction<TAddress> item;

            internal Custom(IRangeRestriction<TAddress> item)
              : base(3)
            {
                this.item = item;
            }

            public IRangeRestriction<TAddress> Item
            {

                get
                {
                    return this.item;
                }
            }
        }
        
    }
}
