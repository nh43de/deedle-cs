//// Decompiled with JetBrains decompiler
//// Type: Deedle.Vectors.VectorData`1
//// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
//// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Diagnostics;
//using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;

//namespace Deedle.Vectors
//{
//    public abstract class VectorData<T> : IEquatable<VectorData<T>>, IStructuralEquatable
//    {
        

//        //internal VectorData()
//        //{
//        //}


//        //public static VectorData<T> NewDenseList(ReadOnlyCollection<T> item)
//        //{
//        //    return (VectorData<T>)new VectorData<T>.DenseList(item);
//        //}




//        //public bool IsDenseList
//        //{
//        //    get
//        //    {
//        //        return this is VectorData<T>.DenseList;
//        //    }
//        //}


//        //public bool get_IsDenseList()
//        //{
//        //    return this is VectorData<T>.DenseList;
//        //}


//        //public static VectorData<T> NewSparseList(ReadOnlyCollection<OptionalValue<T>> item)
//        //{
//        //    return (VectorData<T>)new VectorData<T>.SparseList(item);
//        //}




//        //public bool IsSparseList
//        //{
//        //    get
//        //    {
//        //        return this is VectorData<T>.SparseList;
//        //    }
//        //}


//        //public bool get_IsSparseList()
//        //{
//        //    return this is VectorData<T>.SparseList;
//        //}


//        //public static VectorData<T> NewSequence(IEnumerable<OptionalValue<T>> item)
//        //{
//        //    return (VectorData<T>)new VectorData<T>.Sequence(item);
//        //}




//        //public bool IsSequence
//        //{
//        //    get
//        //    {
//        //        return this is VectorData<T>.Sequence;
//        //    }
//        //}


//        //public bool get_IsSequence()
//        //{
//        //    return this is VectorData<T>.Sequence;
//        //}


//        //public int get_Tag()
//        //{
//        //    if (this is VectorData<T>.Sequence)
//        //        return 2;
//        //    return this is VectorData<T>.SparseList ? 1 : 0;
//        //}




//        //public int Tag
//        //{
//        //    get
//        //    {
//        //        if (this is VectorData<T>.Sequence)
//        //            return 2;
//        //        return this is VectorData<T>.SparseList ? 1 : 0;
//        //    }
//        //}



//        //[SpecialName]
//        //internal object __DebugDisplay()
//        //{
//        //    return (object)((FSharpFunc<VectorData<T>, string>)ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<VectorData<T>, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<FSharpFunc<VectorData<T>, string>, Unit, string, string, string>("%+0.8A"))).Invoke(this);
//        //}


//        //public override string ToString()
//        //{
//        //    return ((FSharpFunc<VectorData<T>, string>)ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<VectorData<T>, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<FSharpFunc<VectorData<T>, string>, Unit, string, string, VectorData<T>>("%+A"))).Invoke(this);
//        //}


//        //public virtual int GetHashCode(IEqualityComparer comp)
//        //{
//        //    if (this == null)
//        //        return 0;
//        //    VectorData<T> vectorData = this;
//        //    if (!(vectorData is VectorData<T>.DenseList))
//        //    {
//        //        if (!(vectorData is VectorData<T>.SparseList))
//        //        {
//        //            if (vectorData is VectorData<T>.Sequence)
//        //            {
//        //                VectorData<T>.Sequence sequence = (VectorData<T>.Sequence)this;
//        //                int num = 2;
//        //                return LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<IEnumerable<OptionalValue<T>>>(comp, (M0)sequence.item) + ((num << 6) + (num >> 2)) - 1640531527;
//        //            }
//        //        }
//        //        else
//        //        {
//        //            VectorData<T>.SparseList sparseList = (VectorData<T>.SparseList)this;
//        //            int num = 1;
//        //            return LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<ReadOnlyCollection<OptionalValue<T>>>(comp, (M0)sparseList.item) + ((num << 6) + (num >> 2)) - 1640531527;
//        //        }
//        //    }
//        //    VectorData<T>.DenseList denseList = (VectorData<T>.DenseList)this;
//        //    int num1 = 0;
//        //    return LanguagePrimitives.HashCompare.GenericHashWithComparerIntrinsic<ReadOnlyCollection<T>>(comp, (M0)denseList.item) + ((num1 << 6) + (num1 >> 2)) - 1640531527;
//        //}


//        //public override sealed int GetHashCode()
//        //{
//        //    return this.GetHashCode(LanguagePrimitives.get_GenericEqualityComparer());
//        //}


//        //public virtual bool Equals(object obj, IEqualityComparer comp)
//        //{
//        //    if (this == null)
//        //        return obj == null;
//        //    VectorData<T> vectorData1 = obj as VectorData<T>;
//        //    if (vectorData1 == null)
//        //        return false;
//        //    VectorData<T> vectorData2 = vectorData1;
//        //    VectorData<T> vectorData3 = this;
//        //    int num1 = !(vectorData3 is VectorData<T>.Sequence) ? (!(vectorData3 is VectorData<T>.SparseList) ? 0 : 1) : 2;
//        //    VectorData<T> vectorData4 = vectorData2;
//        //    int num2 = !(vectorData4 is VectorData<T>.Sequence) ? (!(vectorData4 is VectorData<T>.SparseList) ? 0 : 1) : 2;
//        //    if (num1 != num2)
//        //        return false;
//        //    VectorData<T> vectorData5 = this;
//        //    if (!(vectorData5 is VectorData<T>.DenseList))
//        //    {
//        //        if (!(vectorData5 is VectorData<T>.SparseList))
//        //        {
//        //            if (vectorData5 is VectorData<T>.Sequence)
//        //            {
//        //                VectorData<T>.Sequence sequence1 = (VectorData<T>.Sequence)this;
//        //                VectorData<T>.Sequence sequence2 = (VectorData<T>.Sequence)vectorData2;
//        //                return LanguagePrimitives.HashCompare.GenericEqualityWithComparerIntrinsic<IEnumerable<OptionalValue<T>>>(comp, (M0)sequence1.item, (M0)sequence2.item);
//        //            }
//        //        }
//        //        else
//        //        {
//        //            VectorData<T>.SparseList sparseList1 = (VectorData<T>.SparseList)this;
//        //            VectorData<T>.SparseList sparseList2 = (VectorData<T>.SparseList)vectorData2;
//        //            return LanguagePrimitives.HashCompare.GenericEqualityWithComparerIntrinsic<ReadOnlyCollection<OptionalValue<T>>>(comp, (M0)sparseList1.item, (M0)sparseList2.item);
//        //        }
//        //    }
//        //    VectorData<T>.DenseList denseList1 = (VectorData<T>.DenseList)this;
//        //    VectorData<T>.DenseList denseList2 = (VectorData<T>.DenseList)vectorData2;
//        //    return LanguagePrimitives.HashCompare.GenericEqualityWithComparerIntrinsic<ReadOnlyCollection<T>>(comp, (M0)denseList1.item, (M0)denseList2.item);
//        //}


//        //public virtual bool Equals(VectorData<T> obj)
//        //{
//        //    if (this == null)
//        //        return obj == null;
//        //    if (obj == null)
//        //        return false;
//        //    VectorData<T> vectorData1 = this;
//        //    int num1 = !(vectorData1 is VectorData<T>.Sequence) ? (!(vectorData1 is VectorData<T>.SparseList) ? 0 : 1) : 2;
//        //    VectorData<T> vectorData2 = obj;
//        //    int num2 = !(vectorData2 is VectorData<T>.Sequence) ? (!(vectorData2 is VectorData<T>.SparseList) ? 0 : 1) : 2;
//        //    if (num1 != num2)
//        //        return false;
//        //    VectorData<T> vectorData3 = this;
//        //    if (!(vectorData3 is VectorData<T>.DenseList))
//        //    {
//        //        if (vectorData3 is VectorData<T>.SparseList)
//        //            return LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<ReadOnlyCollection<OptionalValue<T>>>((M0)((VectorData<T>.SparseList)this).item, (M0)((VectorData<T>.SparseList)obj).item);
//        //        if (vectorData3 is VectorData<T>.Sequence)
//        //            return LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<IEnumerable<OptionalValue<T>>>((M0)((VectorData<T>.Sequence)this).item, (M0)((VectorData<T>.Sequence)obj).item);
//        //    }
//        //    return LanguagePrimitives.HashCompare.GenericEqualityERIntrinsic<ReadOnlyCollection<T>>((M0)((VectorData<T>.DenseList)this).item, (M0)((VectorData<T>.DenseList)obj).item);
//        //}


//        //public override sealed bool Equals(object obj)
//        //{
//        //    VectorData<T> vectorData = obj as VectorData<T>;
//        //    if (vectorData != null)
//        //        return this.Equals(vectorData);
//        //    return false;
//        //}

//        //public static class Tags
//        //{
//        //    public const int DenseList = 0;
//        //    public const int SparseList = 1;
//        //    public const int Sequence = 2;
//        //}

//        [Serializable]
//        [SpecialName]
//        public class DenseList : VectorData<T>
//        {
//            internal readonly ReadOnlyCollection<T> item;
            
//            internal DenseList(ReadOnlyCollection<T> item)
//            {
//                this.item = item;
//            }

//            public ReadOnlyCollection<T> Item
//            {
//                get
//                {
//                    return this.item;
//                }
//            }
//        }

//        [Serializable]
//        [SpecialName]
//        public class SparseList : VectorData<T>
//        {
//            internal readonly ReadOnlyCollection<T> item;

//            internal SparseList(ReadOnlyCollection<T> item)
//            {
//                this.item = item;
//            }

//            public ReadOnlyCollection<T> Item
//            {
//                get
//                {
//                    return this.item;
//                }
//            }
//        }

//        [Serializable]
//        [SpecialName]
//        public class Sequence : VectorData<T>
//        {
//            internal readonly IEnumerable<T> item;

//            internal Sequence(IEnumerable<T> item)
//            {
//                this.item = item;
//            }

//            public IEnumerable<T> Item
//            {
//                get
//                {
//                    return this.item;
//                }
//            }
//        }

//    }
//}
