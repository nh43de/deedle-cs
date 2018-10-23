// Decompiled with JetBrains decompiler
// Type: Deedle.RangeRestriction`1
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Deedle
{
    public static class RangeRestriction
    {
        public static RangeRestriction<TNewAddress> map<TOldAddress, TNewAddress>(Func<TOldAddress, TNewAddress> f, RangeRestriction<TOldAddress> _arg1)
        {
            RangeRestriction<TOldAddress> rangeRestriction = _arg1;
            switch (rangeRestriction.Tag)
            {
                case 1:
                    return RangeRestriction<TNewAddress>.NewStart(((RangeRestriction<TOldAddress>.Start)rangeRestriction).item);
                case 2:
                    return RangeRestriction<TNewAddress>.NewEnd(((RangeRestriction<TOldAddress>.End)rangeRestriction).item);
                case 3:
                    IRangeRestriction<TOldAddress> c = ((RangeRestriction<TOldAddress>.Custom)rangeRestriction).item;
                    return RangeRestriction<TNewAddress>.NewCustom(new RangeRestriction.map<TNewAddress, TOldAddress>(f, c));
                default:
                    RangeRestriction<TOldAddress>.Fixed @fixed = (RangeRestriction<TOldAddress>.Fixed)rangeRestriction;
                    TOldAddress oldAddress1 = @fixed.item1;
                    TOldAddress oldAddress2 = @fixed.item2;
                    return RangeRestriction<TNewAddress>.NewFixed(f.Invoke(oldAddress1), f.Invoke(oldAddress2));
            }
        }


        /// <summary>
        /// Transforms all absolute addresses in the specified range restriction
        /// using the provided function (this is useful for mapping between different
        /// address spaces).
        /// </summary>
        /// <typeparam name="TNewAddress"></typeparam>
        /// <typeparam name="TOldAddress"></typeparam>
        [Serializable]
        [SpecialName]
        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class map<TNewAddress, TOldAddress> : IEnumerable, IEnumerable<TNewAddress>, IRangeRestriction<TNewAddress>
        {
            public Func<TOldAddress, TNewAddress> f;
            public IRangeRestriction<TOldAddress> c;

            public map(Func<TOldAddress, TNewAddress> f, IRangeRestriction<TOldAddress> c)
            {
                this.f = f;
                this.c = c;
                // ISSUE: explicit constructor call
                base.ctor();
                RangeRestriction.map<TNewAddress, TOldAddress> map136 = this;
            }

            long IRangeRestriction<TNewAddress>.get_Count()
            {
                return this.c.Count;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)((IEnumerable<TNewAddress>)this).GetEnumerator();
            }

            IEnumerator<TNewAddress> IEnumerable<TNewAddress>.GetEnumerator()
            {
                return ((IEnumerable<TNewAddress>)SeqModule.Map<TOldAddress, TNewAddress>((Func<M0, M1>)this.f, (IEnumerable<M0>)this.c)).GetEnumerator();
            }
        }
    }
}
