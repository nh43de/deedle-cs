// Decompiled with JetBrains decompiler
// Type: Deedle.Vectors.VectorConstruction
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Deedle.Vectors
{
    [Serializable]
    [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
    public abstract class VectorConstruction
    {
        internal readonly int _tag;
               
        internal VectorConstruction(int _tag)
        {
            this._tag = _tag;
        }
        
        public static VectorConstruction NewReturn(int item)
        {
            return new VectorConstruction.Return(item);
        }
        
        public bool IsReturn
        {
            get
            {
                return this.Tag == 0;
            }
        }



        public static VectorConstruction NewEmpty(long item)
        {
            return (VectorConstruction)new VectorConstruction.Empty(item);
        }

        

        public bool IsEmpty
        {
            get
            {
                return this.Tag == 1;
            }
        }


        public static VectorConstruction NewRelocate(VectorConstruction item1, long item2, IEnumerable<Tuple<long, long>> item3)
        {
            return new VectorConstruction.Relocate(item1, item2, item3);
        }
        
        public bool IsRelocate
        {
            get
            {
                return this.Tag == 2;
            }
        }


        public static VectorConstruction NewDropRange(VectorConstruction item1, RangeRestriction<long> item2)
        {
            return (VectorConstruction)new VectorConstruction.DropRange(item1, item2);
        }




        public bool IsDropRange
        {
            get
            {
                return this.Tag == 3;
            }
        }


        public static VectorConstruction NewGetRange(VectorConstruction item1, RangeRestriction<long> item2)
        {
            return (VectorConstruction)new VectorConstruction.GetRange(item1, item2);
        }




        public bool IsGetRange
        {
            get
            {
                return this.Tag == 4;
            }
        }



        public static VectorConstruction NewAppend(VectorConstruction item1, VectorConstruction item2)
        {
            return (VectorConstruction)new VectorConstruction.Append(item1, item2);
        }




        public bool IsAppend
        {
            get
            {
                return this.Tag == 5;
            }
        }

        public static VectorConstruction NewCombine(Lazy<long> item1, IList<VectorConstruction> item2, VectorListTransform item3)
        {
            return (VectorConstruction)new VectorConstruction.Combine(item1, item2, item3);
        }



        public bool IsCombine
        {
            get
            {
                return this.Tag == 6;
            }
        }


        public static VectorConstruction NewFillMissing(VectorConstruction item1, VectorFillMissing item2)
        {
            return (VectorConstruction)new VectorConstruction.FillMissing(item1, item2);
        }



        public bool IsFillMissing
        {
            get
            {
                return this.Tag == 7;
            }
        }


        public static VectorConstruction NewCustomCommand(IList<VectorConstruction> item1, Func<IList<IVector>, IVector> item2)
        {
            return (VectorConstruction)new VectorConstruction.CustomCommand(item1, item2);
        }




        public bool IsCustomCommand
        {
            get
            {
                return this.Tag == 8;
            }
        }


        public static VectorConstruction NewAsyncCustomCommand(IList<VectorConstruction> item1, Func<IList<IVector>, FSharpAsync<IVector>> item2)
        {
            return (VectorConstruction)new VectorConstruction.AsyncCustomCommand(item1, item2);
        }




        public bool IsAsyncCustomCommand
        {
            get
            {
                return this.Tag == 9;
            }
        }

        


        public int Tag
        {
            get
            {
                return this._tag;
            }
        }




        internal object __DebugDisplay()
        {
            return (object)((Func<VectorConstruction, string>)ExtraTopLevelOperators.PrintFormatToString<Func<VectorConstruction, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<VectorConstruction, string>, Unit, string, string, string>("%+0.8A"))).Invoke(this);
        }


        public override string ToString()
        {
            return ((Func<VectorConstruction, string>)ExtraTopLevelOperators.PrintFormatToString<Func<VectorConstruction, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<Func<VectorConstruction, string>, Unit, string, string, VectorConstruction>("%+A"))).Invoke(this);
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

        [Serializable]

        public class Return : VectorConstruction
        {
            internal readonly int item;

            internal Return(int item)
              : base(0)
            {
                this.item = item;
            }
            
            public int Item
            {
                get
                {
                    return this.item;
                }
            }
        }

        [DebuggerTypeProxy(typeof(VectorConstruction.EmptyDebugTypeProxy))]
        [DebuggerDisplay("{__DebugDisplay(),nq}")]
        [Serializable]

        public class Empty : VectorConstruction
        {



            internal readonly long item;



            internal Empty(long item)
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

        public class Relocate : VectorConstruction
        {



            internal readonly VectorConstruction item1;



            internal readonly long item2;



            internal readonly IEnumerable<Tuple<long, long>> item3;



            internal Relocate(VectorConstruction item1, long item2, IEnumerable<Tuple<long, long>> item3)
              : base(2)
            {
                this.item1 = item1;
                this.item2 = item2;
                this.item3 = item3;
            }

            public VectorConstruction Item1
            {
                get
                {
                    return this.item1;
                }
            }

            public long Item2
            {
                get
                {
                    return this.item2;
                }
            }

            public IEnumerable<Tuple<long, long>> Item3
            {
                get
                {
                    return this.item3;
                }
            }
        }

        [DebuggerTypeProxy(typeof(VectorConstruction.DropRangeDebugTypeProxy))]
        [DebuggerDisplay("{__DebugDisplay(),nq}")]
        [Serializable]

        public class DropRange : VectorConstruction
        {



            internal readonly VectorConstruction item1;



            internal readonly RangeRestriction<long> item2;



            internal DropRange(VectorConstruction item1, RangeRestriction<long> item2)
              : base(3)
            {
                this.item1 = item1;
                this.item2 = item2;
            }




            public VectorConstruction Item1
            {
                get
                {
                    return this.item1;
                }
            }


            public RangeRestriction<long> Item2
            {
                get
                {
                    return this.item2;
                }
            }
        }

        [Serializable]

        public class GetRange : VectorConstruction
        {
            internal readonly VectorConstruction item1;
            internal readonly RangeRestriction<long> item2;



            internal GetRange(VectorConstruction item1, RangeRestriction<long> item2)
              : base(4)
            {
                this.item1 = item1;
                this.item2 = item2;
            }




            public VectorConstruction Item1
            {
                get
                {
                    return this.item1;
                }
            }





            public RangeRestriction<long> Item2
            {
                get
                {
                    return this.item2;
                }
            }
        }

        [Serializable]
        public class Append : VectorConstruction
        {
            internal readonly VectorConstruction item1;
            internal readonly VectorConstruction item2;



            internal Append(VectorConstruction item1, VectorConstruction item2)
              : base(5)
            {
                this.item1 = item1;
                this.item2 = item2;
            }




            public VectorConstruction Item1
            {
                get
                {
                    return this.item1;
                }
            }



            public VectorConstruction Item2
            {
                get
                {
                    return this.item2;
                }
            }
        }

        [Serializable]
        public class Combine : VectorConstruction
        {
            internal readonly Lazy<long> item1;

            internal readonly IList<VectorConstruction> item2;

            internal readonly VectorListTransform item3;



            internal Combine(Lazy<long> item1, IList<VectorConstruction> item2, VectorListTransform item3)
              : base(6)
            {
                this.item1 = item1;
                this.item2 = item2;
                this.item3 = item3;
            }




            public Lazy<long> Item1
            {
                get
                {
                    return this.item1;
                }
            }
            


            public IList<VectorConstruction> Item2
            {
                get
                {
                    return this.item2;
                }
            }

                       

            public VectorListTransform Item3
            {
                get
                {
                    return this.item3;
                }
            }
        }


        [Serializable]

        public class FillMissing : VectorConstruction
        {
            internal readonly VectorConstruction item1;

            internal readonly VectorFillMissing item2;

            internal FillMissing(VectorConstruction item1, VectorFillMissing item2)
              : base(7)
            {
                this.item1 = item1;
                this.item2 = item2;
            }

            public VectorConstruction Item1
            {
                get
                {
                    return this.item1;
                }
            }

            public VectorFillMissing Item2
            {
                get
                {
                    return this.item2;
                }
            }
        }


        [Serializable]

        public class CustomCommand : VectorConstruction
        {



            internal readonly IList<VectorConstruction> item1;



            internal readonly Func<IList<IVector>, IVector> item2;



            internal CustomCommand(IList<VectorConstruction> item1, Func<IList<IVector>, IVector> item2)
              : base(8)
            {
                this.item1 = item1;
                this.item2 = item2;
            }




            public IList<VectorConstruction> Item1
            {
                get
                {
                    return this.item1;
                }
            }
            
            public Func<IList<IVector>, IVector> Item2
            {
                get
                {
                    return this.item2;
                }
            }
        }


        [Serializable]

        public class AsyncCustomCommand : VectorConstruction
        {
            internal readonly IList<VectorConstruction> item1;
            
            internal readonly Func<IList<IVector>, Task<IVector>> item2;


            internal AsyncCustomCommand(IList<VectorConstruction> item1, Func<IList<IVector>, Task<IVector>> item2)
              : base(9)
            {
                this.item1 = item1;
                this.item2 = item2;
            }


            public IList<VectorConstruction> Item1
            {
                get
                {
                    return this.item1;
                }
            }

            


            public Func<IList<IVector>, Task<IVector>> Item2
            {
                get
                {
                    return this.item2;
                }
            }
        }
    }
}