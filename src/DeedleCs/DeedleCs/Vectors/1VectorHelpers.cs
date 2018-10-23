// Decompiled with JetBrains decompiler
// Type: Deedle.VectorHelpers
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle.Internal;
using Deedle.Vectors;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Deedle;

namespace Deedle
{
    internal static partial class VectorHelpers
    {
        public interface IBoxedVector : IVector<object>
        {

        }


        internal static VectorHelpers.IBoxedVector createBoxedVector<TValue>(IVector<TValue> vector)
        {
            return (VectorHelpers.IBoxedVector)new VectorHelpers.createBoxedVector<TValue>(vector);
        }


        internal static IVector<TResult> lazyMapVector<TValue, TResult>(FSharpFunc<TValue, TResult> f, IVector<TValue> vector)
        {
            Lazy<IVector<TResult>> unwrapVector = (Lazy<IVector<TResult>>)LazyExtensions.Create<IVector<TResult>>((FSharpFunc<Unit, M0>)new VectorHelpers.unwrapVector<TResult, TValue>(f, vector));
            return (IVector<TResult>)new VectorHelpers.lazyMapVector<TValue, TResult>(f, vector, unwrapVector);
        }

        internal static IntPtr doubleCode
        {
            get
            {
                return VectorHelpers.doubleCode;
            }
        }

        internal static IntPtr intCode
        {
            get
            {
                return VectorHelpers.intCode;
            }
        }

        internal static IntPtr stringCode
        {
            get
            {
                return VectorHelpers.stringCode;
            }
        }

        internal static FSharpFunc<object, R> createValueDispatcher<R>(VectorHelpers.ValueCallSite<R> callSite)
        {
            Lazy<Dictionary<IntPtr, Func<VectorHelpers.ValueCallSite<R>, object, R>>> dict = (Lazy<Dictionary<IntPtr, Func<VectorHelpers.ValueCallSite<R>, object, R>>>)LazyExtensions.Create<Dictionary<IntPtr, Func<VectorHelpers.ValueCallSite<R>, object, R>>>((FSharpFunc<Unit, M0>)new VectorHelpers.dict<R>());
            return (FSharpFunc<object, R>)new VectorHelpers.createValueDispatcher<R>(callSite, dict);
        }

        internal static FSharpFunc<FSharpList<OptionalValue<T>>, OptionalValue<T>> VectorListTransformGetFunction<T>([In] VectorListTransform obj0)
        {
            VectorListTransform vectorListTransform = obj0;
            if (!(vectorListTransform is VectorListTransform.Binary))
                return ((VectorListTransform.Nary)vectorListTransform).item.GetFunction<T>();
            return (FSharpFunc<FSharpList<OptionalValue<T>>, OptionalValue<T>>)new VectorHelpers.VectorListTransformGetFunction<T>(((VectorListTransform.Binary)vectorListTransform).item.GetFunction<T>());
        }

        internal static IVector<object> boxVector(IVector vector)
        {
            VectorCallSite<IVector<object>> vectorCallSite = (VectorCallSite<IVector<object>>)new VectorHelpers.boxVector();
            return vector.Invoke<IVector<object>>(vectorCallSite);
        }

        internal static IVector unboxVector(IVector v)
        {
            IVector vector = v;
            VectorHelpers.IBoxedVector boxedVector = vector as VectorHelpers.IBoxedVector;
            if (boxedVector != null)
                return boxedVector.UnboxedVector;
            return vector;
        }

        internal static IVector transformColumn(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd, IVector vector)
        {
            VectorCallSite<IVector> vectorCallSite = (VectorCallSite<IVector>)new VectorHelpers.transformColumn(vectorBuilder, scheme, rowCmd);
            return vector.Invoke<IVector>(vectorCallSite);
        }


        internal static IVector<R> convertType<R>(ConversionKind conversionKind, IVector vector)
        {
            IVector vector1 = vector;
            VectorHelpers.IBoxedVector boxedVector = vector1 as VectorHelpers.IBoxedVector;
            IVector vector2 = boxedVector == null ? vector1 : boxedVector.UnboxedVector;
            return vector2 as IVector<R> ?? vector2.Invoke<IVector<R>>((VectorCallSite<IVector<R>>)new VectorHelpers.convertType<R>(conversionKind));
        }


        internal static Lazy<MethodInfo> convertTypeMethod
        {
            get
            {
                return VectorHelpers.convertTypeMethod;
            }
        }


        internal static IVector convertTypeDynamic<a>(Type typ, a conversionKind, IVector vector)
        {
            return (IVector)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<IVector>(VectorHelpers.convertTypeMethod.Value.MakeGenericMethod(typ).Invoke((object)conversionKind, new object[2] { (object)conversionKind, (object)vector }));
        }


        internal static OptionalValue<IVector<R>> tryConvertType<R>(ConversionKind conversionKind, IVector vector)
        {
            IVector vector1 = vector;
            VectorHelpers.IBoxedVector boxedVector = vector1 as VectorHelpers.IBoxedVector;
            IVector vector2 = boxedVector == null ? vector1 : boxedVector.UnboxedVector;
            IVector<R> vector3 = vector2 as IVector<R>;
            if (vector3 != null)
                return new OptionalValue<IVector<R>>(vector3);
            return vector2.Invoke<OptionalValue<IVector<R>>>((VectorCallSite<OptionalValue<IVector<R>>>)new VectorHelpers.tryConvertType<R>(conversionKind));
        }


        internal static FSharpOption<IVector<double>> AsFloatVector_(IVector v)
        {
            OptionalValue<IVector<double>> optionalValue = VectorHelpers.tryConvertType<double>(ConversionKind.Flexible, v);
            if (optionalValue.HasValue)
                return FSharpOption<IVector<double>>.Some(optionalValue.Value);
            return (FSharpOption<IVector<double>>)null;
        }


        internal static IVector<T> createRowReader<T>(IVector<IVector> data, IVectorBuilder builder, long rowAddress, FSharpFunc<long, long> colAddressAt)
        {
            return (IVector<T>)new VectorHelpers.RowReaderVector<T>(data, builder, rowAddress, colAddressAt);
        }


        internal static IVector<object> createObjRowReader(IVector<IVector> data, IVectorBuilder builder, long addr, FSharpFunc<long, long> colAddressAt)
        {
            return (IVector<object>)new VectorHelpers.RowReaderVector<object>(data, builder, addr, colAddressAt);
        }

        internal static TryValue<IVector> tryValues(IVector vect)
        {
            Type elementType = vect.ElementType;
            int num;
            if (elementType.IsGenericType)
            {
                Type genericTypeDefinition = elementType.GetGenericTypeDefinition();
                Type type1 = typeof(TryValue<object>);
                Type type2 = !type1.GetTypeInfo().IsGenericType ? type1 : type1.GetGenericTypeDefinition();
                num = LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)genericTypeDefinition, (M0)type2) ? 1 : 0;
            }
            else
                num = 0;
            if (num == 0)
                return TryValue<IVector>.NewSuccess(vect);
            return (TryValue<IVector>)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<TryValue<IVector>>(typeof(VectorHelpers.TryValuesHelper).GetMethod("TryValues", (BindingFlags)(32 | 8)).MakeGenericMethod(elementType.GetGenericArguments()[0]).Invoke((object)null, new object[1] { (object)vect }));
        }


        internal static R[,] toArray2D<R>(int rowCount, int colCount, IVector<IVector> data, Lazy<R> defaultValue)
        {
            R[,] res = Array2DModule.ZeroCreate<R>(rowCount, colCount);
            SeqModule.IterateIndexed<OptionalValue<IVector>>((FSharpFunc<int, FSharpFunc<M0, Unit>>)new VectorHelpers.toArray2D<R>(rowCount, defaultValue, res), (IEnumerable<M0>)FVectorextensionscore.IVector`1get_DataSequence<IVector>(data));
            return res;
        }


        internal static IVector createTypedVector(IVectorBuilder builder, Type vectorType, object[] data)
        {
            return (IVector)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<IVector>(typeof(VectorHelpers.mapFrameRowVector).GetMethod("Create", (BindingFlags)(32 | 8)).MakeGenericMethod(vectorType).Invoke((object)null, new object[2] { (object)builder, (object)data }));
        }

        internal static Type findCommonSupertype<a>(IEnumerable<a> types) where a : Type
        {
            Type type = (Type)SeqModule.Fold<a, Type>((FSharpFunc<M1, FSharpFunc<M0, M1>>)new VectorHelpers.ty<a>(), (M1)VectorHelpers.Inference.Top, types);
            if (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)type, (M0)VectorHelpers.Inference.Top))
                return VectorHelpers.Inference.Bottom;
            return type;
        }


        internal static IVector createInferredTypeVector(IVectorBuilder builder, object[] data)
        {
            Type commonSupertype = VectorHelpers.findCommonSupertype<Type>((IEnumerable<Type>)SeqModule.Map<object, Type>((FSharpFunc<M0, M1>)new VectorHelpers.vectorType(), (IEnumerable<M0>)data));
            return VectorHelpers.createTypedVector(builder, commonSupertype, data);
        }


        internal static IVector<TRow> mapFrameRowVector<TRow>(Func<IVector[], long, TRow> ctor, long length, FSharpFunc<long, long> addressAt, IVector[] data)
        {
            return (IVector<TRow>)new VectorHelpers.mapFrameRowVector<TRow>(ctor, length, addressAt, data);
        }


        internal static Lazy<ModuleBuilder> typedRowModule
        {
            get
            {
                return VectorHelpers.typedRowModule;
            }
        }


        internal static FSharpRef<int> typeCounter
        {
            get
            {
                return VectorHelpers.typeCounter;
            }
        }


        internal static Dictionary<Tuple<Type, FSharpList<string>>, Tuple<object, FSharpList<Tuple<string, Type>>>> createdTypedRowsCache
        {
            get
            {
                return VectorHelpers.createdTypedRowsCache;
            }
        }

        internal static Tuple<Func<IVector[], long, TRow>, FSharpList<Tuple<string, Type>>> createTypedRowCreator<TRow>(FSharpList<string> columnKeys)
        {
            Type type1 = typeof(TRow);
            Tuple<object, FSharpList<Tuple<string, Type>>> tuple1 = (Tuple<object, FSharpList<Tuple<string, Type>>>)null;
            Tuple<bool, Tuple<object, FSharpList<Tuple<string, Type>>>> tuple2 = new Tuple<bool, Tuple<object, FSharpList<Tuple<string, Type>>>>(VectorHelpers.createdTypedRowsCache.TryGetValue(new Tuple<Type, FSharpList<string>>(type1, columnKeys), out tuple1), tuple1);
            if (tuple2.Item1)
            {
                FSharpList<Tuple<string, Type>> fsharpList = tuple2.Item2.Item2;
                return new Tuple<Func<IVector[], long, TRow>, FSharpList<Tuple<string, Type>>>((Func<IVector[], long, TRow>)tuple2.Item2.Item1, fsharpList);
            }
            foreach (MethodInfo method in type1.GetMethods())
            {
                if ((method.IsSpecialName ? (!method.Name.StartsWith("get_") ? 1 : 0) : 1) != 0)
                    throw new InvalidOperationException("Only readonly properties are supported in the interface!");
            }
            Operators.Increment(VectorHelpers.typeCounter);
            TypeBuilder rowImpl = VectorHelpers.typedRowModule.Value.DefineType((string)FSharpFunc<string, int>.InvokeFast<string>((FSharpFunc<string, FSharpFunc<int, M0>>)new VectorHelpers.typeName((FSharpFunc<string, FSharpFunc<int, string>>)ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<string, FSharpFunc<int, string>>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<FSharpFunc<string, FSharpFunc<int, string>>, Unit, string, string, Tuple<string, int>>("Impl%s@%d"))), type1.Name, VectorHelpers.typeCounter.get_Value()), TypeAttributes.Public, typeof(object), new Type[1] { type1 });
            FSharpList<Tuple<bool, Type>> list = (FSharpList<Tuple<bool, Type>>)SeqModule.ToList<Tuple<bool, Type>>((IEnumerable<M0>)new VectorHelpers.columnTypes(type1, (MethodInfo)null, (IEnumerator<MethodInfo>)null, 0, (Tuple<bool, Type>)null));
            FSharpList<FieldBuilder> fsharpList1 = (FSharpList<FieldBuilder>)ListModule.MapIndexed<Tuple<bool, Type>, FieldBuilder>((FSharpFunc<int, FSharpFunc<M0, M1>>)new VectorHelpers.vecFields(rowImpl), (FSharpList<M0>)list);
            FieldBuilder fieldBuilder1 = rowImpl.DefineField("address", typeof(long), FieldAttributes.Private);
            ILGenerator ilGenerator1 = rowImpl.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, (Type[])ArrayModule.OfSeq<Type>((IEnumerable<M0>)FSharpList<Type>.Cons(VectorHelpers.Reflection.addrTyp, (FSharpList<Type>)ListModule.Map<Tuple<bool, Type>, Type>((FSharpFunc<M0, M1>)new VectorHelpers.ctor(), (FSharpList<M0>)list)))).GetILGenerator();
            ilGenerator1.Emit(OpCodes.Ldarg_0);
            ilGenerator1.Emit(OpCodes.Callvirt, VectorHelpers.Reflection.objCtor);
            ilGenerator1.Emit(OpCodes.Ldarg_0);
            ilGenerator1.Emit(OpCodes.Ldarg_1);
            ilGenerator1.Emit(OpCodes.Stfld, (FieldInfo)fieldBuilder1);
            ListModule.IterateIndexed<FieldBuilder>((FSharpFunc<int, FSharpFunc<M0, Unit>>)new VectorHelpers.createTypedRowCreator(ilGenerator1), (FSharpList<M0>)fsharpList1);
            ilGenerator1.Emit(OpCodes.Ret);
            IEnumerator<Tuple<MethodInfo, Tuple<Tuple<bool, Type>, FieldBuilder>>> enumerator = ((IEnumerable<Tuple<MethodInfo, Tuple<Tuple<bool, Type>, FieldBuilder>>>)SeqModule.Zip<MethodInfo, Tuple<Tuple<bool, Type>, FieldBuilder>>((IEnumerable<M0>)type1.GetMethods(), (IEnumerable<M1>)SeqModule.Zip<Tuple<bool, Type>, FieldBuilder>((IEnumerable<M0>)list, (IEnumerable<M1>)fsharpList1))).GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    Tuple<MethodInfo, Tuple<Tuple<bool, Type>, FieldBuilder>> current = enumerator.Current;
                    Type type2 = current.Item2.Item1.Item2;
                    MethodInfo methodInfoDeclaration = current.Item1;
                    bool flag = current.Item2.Item1.Item1;
                    FieldBuilder fieldBuilder2 = current.Item2.Item2;
                    MethodBuilder methodBuilder = rowImpl.DefineMethod(methodInfoDeclaration.Name, (MethodAttributes)(70 | 2048), methodInfoDeclaration.ReturnType, new Type[0]);
                    ILGenerator ilGenerator2 = methodBuilder.GetILGenerator();
                    ilGenerator2.Emit(OpCodes.Ldarg_0);
                    ilGenerator2.Emit(OpCodes.Ldfld, (FieldInfo)fieldBuilder2);
                    ilGenerator2.Emit(OpCodes.Ldarg_0);
                    ilGenerator2.Emit(OpCodes.Ldfld, (FieldInfo)fieldBuilder1);
                    ilGenerator2.Emit(OpCodes.Callvirt, fieldBuilder2.FieldType.GetMethod("GetValue"));
                    if (!flag)
                    {
                        Type localType = VectorHelpers.Reflection.optTyp.MakeGenericType(type2.GetGenericArguments()[0]);
                        LocalBuilder local = ilGenerator2.DeclareLocal(localType);
                        ilGenerator2.Emit(OpCodes.Stloc, local);
                        ilGenerator2.Emit(OpCodes.Ldloca_S, local);
                        ilGenerator2.Emit(OpCodes.Call, localType.GetProperty("Value").GetGetMethod());
                    }
                    ilGenerator2.Emit(OpCodes.Ret);
                    rowImpl.DefineMethodOverride((MethodInfo)methodBuilder, methodInfoDeclaration);
                }
            }
            finally
            {
                (enumerator as IDisposable)?.Dispose();
            }
            TypeInfo typeInfo = rowImpl.CreateTypeInfo();
            Type[] parameterTypes = new Type[2] { typeof(IVector[]), typeof(long) };
            DynamicMethod dynamicMethod = new DynamicMethod("Make" + type1.Name, type1, parameterTypes);
            ILGenerator ilGenerator3 = dynamicMethod.GetILGenerator();
            FSharpList<Tuple<LocalBuilder, Type>> fsharpList2 = (FSharpList<Tuple<LocalBuilder, Type>>)ListModule.MapIndexed<Tuple<bool, Type>, Tuple<LocalBuilder, Type>>((FSharpFunc<int, FSharpFunc<M0, M1>>)new VectorHelpers.locals(ilGenerator3), (FSharpList<M0>)list);
            ilGenerator3.Emit(OpCodes.Ldarg_1);
            FSharpList<Tuple<LocalBuilder, Type>> fsharpList3 = fsharpList2;
            for (FSharpList<Tuple<LocalBuilder, Type>> tailOrNull = fsharpList3.get_TailOrNull(); tailOrNull != null; tailOrNull = fsharpList3.get_TailOrNull())
            {
                Tuple<LocalBuilder, Type> headOrDefault = fsharpList3.get_HeadOrDefault();
                Type cls = headOrDefault.Item2;
                LocalBuilder local = headOrDefault.Item1;
                ilGenerator3.Emit(OpCodes.Ldloc, local);
                ilGenerator3.Emit(OpCodes.Castclass, cls);
                fsharpList3 = tailOrNull;
            }
            ConstructorInfo constructor = typeInfo.GetConstructors()[0];
            ilGenerator3.Emit(OpCodes.Newobj, constructor);
            ilGenerator3.Emit(OpCodes.Ret);
            Func<IVector[], long, TRow> func = (Func<IVector[], long, TRow>)dynamicMethod.CreateDelegate(typeof(Func<IVector[], long, TRow>));
            FSharpList<Tuple<string, Type>> fsharpList4 = (FSharpList<Tuple<string, Type>>)ListModule.OfSeq<Tuple<string, Type>>((IEnumerable<M0>)SeqModule.Map<Tuple<MethodInfo, Tuple<bool, Type>>, Tuple<string, Type>>((FSharpFunc<M0, M1>)new VectorHelpers.meta(), (IEnumerable<M0>)SeqModule.Zip<MethodInfo, Tuple<bool, Type>>((IEnumerable<M0>)type1.GetMethods(), (IEnumerable<M1>)list)));
            VectorHelpers.createdTypedRowsCache.Add(new Tuple<Type, FSharpList<string>>(type1, columnKeys), new Tuple<object, FSharpList<Tuple<string, Type>>>((object)func, fsharpList4));
            return new Tuple<Func<IVector[], long, TRow>, FSharpList<Tuple<string, Type>>>(func, fsharpList4);
        }


        internal static IVector<TRow> createTypedRowReader<TRow>(FSharpList<string> columnKeys, FSharpFunc<string, long> columnIndex, long size, FSharpFunc<long, long> addressAt, IVector<IVector> data)
        {
            Tuple<Func<IVector[], long, TRow>, FSharpList<Tuple<string, Type>>> typedRowCreator = VectorHelpers.createTypedRowCreator<TRow>(columnKeys);
            FSharpList<Tuple<string, Type>> meta = typedRowCreator.Item2;
            Func<IVector[], long, TRow> ctor = typedRowCreator.Item1;
            IVector[] array = (IVector[])SeqModule.ToArray<IVector>((IEnumerable<M0>)new VectorHelpers.subData(columnIndex, data, meta, (Type)null, (string)null, (Tuple<string, Type>)null, (IEnumerator<Tuple<string, Type>>)null, 0, (IVector)null));
            return VectorHelpers.mapFrameRowVector<TRow>(ctor, size, addressAt, array);
        }

        internal static FSharpFunc<VectorConstruction, VectorConstruction> substitute(int _arg1_0, VectorConstruction _arg1_1)
        {
            Tuple<int, VectorConstruction> subst = new Tuple<int, VectorConstruction>(_arg1_0, _arg1_1);
            int oldVar = subst.Item1;
            VectorConstruction newVect = subst.Item2;
            return (FSharpFunc<VectorConstruction, VectorConstruction>)new VectorHelpers.substitute(subst, oldVar, newVect);
        }


        internal static FSharpOption<Tuple<Lazy<long>, FSharpList<Tuple<VectorConstruction, long, IEnumerable<Tuple<long, long>>>>, IBinaryTransform>> CombinedRelocations(VectorConstruction _arg1)
        {
            VectorConstruction vectorConstruction = _arg1;
            if (vectorConstruction.get_Tag() == 6)
            {
                VectorConstruction.Combine combine = (VectorConstruction.Combine)vectorConstruction;
                if (combine.item3 is VectorListTransform.Binary)
                {
                    IBinaryTransform binaryTransform = ((VectorListTransform.Binary)combine.item3).item;
                    FSharpList<VectorConstruction> fsharpList1 = combine.item2;
                    Lazy<long> lazy = combine.item1;
                    if (!ListModule.ForAll<VectorConstruction>((FSharpFunc<M0, bool>)new VectorHelpers.CombinedRelocations_(), (FSharpList<M0>)fsharpList1))
                        return (FSharpOption<Tuple<Lazy<long>, FSharpList<Tuple<VectorConstruction, long, IEnumerable<Tuple<long, long>>>>, IBinaryTransform>>)null;
                    FSharpList<Tuple<VectorConstruction, long, IEnumerable<Tuple<long, long>>>> fsharpList2 = (FSharpList<Tuple<VectorConstruction, long, IEnumerable<Tuple<long, long>>>>)ListModule.Map<VectorConstruction, Tuple<VectorConstruction, long, IEnumerable<Tuple<long, long>>>>((FSharpFunc<M0, M1>)new VectorHelpers.parts(), (FSharpList<M0>)fsharpList1);
                    return FSharpOption<Tuple<Lazy<long>, FSharpList<Tuple<VectorConstruction, long, IEnumerable<Tuple<long, long>>>>, IBinaryTransform>>.Some(new Tuple<Lazy<long>, FSharpList<Tuple<VectorConstruction, long, IEnumerable<Tuple<long, long>>>>, IBinaryTransform>(lazy, fsharpList2, binaryTransform));
                }
            }
            return (FSharpOption<Tuple<Lazy<long>, FSharpList<Tuple<VectorConstruction, long, IEnumerable<Tuple<long, long>>>>, IBinaryTransform>>)null;
        }

        [Serializable]
        internal interface IBoxedVector : IVector<object>
        {
            IVector UnboxedVector { get; }
        }

        [Serializable]
        internal sealed class createBoxedVector<TValue> : FSharpFunc<TValue, object>
        {


            internal createBoxedVector()
            {
                base.ctor();
            }

            public virtual object Invoke(TValue x)
            {
                return (object)x;
            }
        }

        [Serializable]
        internal sealed class createBoxedVector<TValue> : FSharpFunc<TValue, object>
        {


            internal createBoxedVector()
            {
                base.ctor();
            }

            public virtual object Invoke(TValue x)
            {
                return (object)x;
            }
        }

        [Serializable]
        internal sealed class createBoxedVector<TValue> : FSharpFunc<TValue, object>
        {


            internal createBoxedVector()
            {
                base.ctor();
            }

            public virtual object Invoke(TValue x)
            {
                return (object)x;
            }
        }

        [Serializable]
        internal sealed class createBoxedVector<TValue> : FSharpFunc<OptionalValue<TValue>, OptionalValue<object>>
        {



            public FSharpFunc<TValue, object> f;



            internal createBoxedVector(FSharpFunc<TValue, object> f)
            {
                this.ctor();
                this.f = f;
            }

            public virtual OptionalValue<object> Invoke(OptionalValue<TValue> input)
            {
                if (input.HasValue)
                    return new OptionalValue<object>(this.f.Invoke(input.Value));
                return OptionalValue<object>.Missing;
            }
        }

        [Serializable]
        internal sealed class createBoxedVector<TValue> : FSharpFunc<TValue, object>
        {


            internal createBoxedVector()
            {
                base.ctor();
            }

            public virtual object Invoke(TValue x)
            {
                return (object)x;
            }
        }

        [Serializable]
        internal sealed class createBoxedVector<TValue> : FSharpFunc<OptionalValue<TValue>, OptionalValue<object>>
        {



            public FSharpFunc<TValue, object> f;



            internal createBoxedVector(FSharpFunc<TValue, object> f)
            {
                this.ctor();
                this.f = f;
            }

            public virtual OptionalValue<object> Invoke(OptionalValue<TValue> input)
            {
                if (input.HasValue)
                    return new OptionalValue<object>(this.f.Invoke(input.Value));
                return OptionalValue<object>.Missing;
            }
        }

        [Serializable]
        internal sealed class createBoxedVector<TValue> : FSharpFunc<TValue, object>
        {


            internal createBoxedVector()
            {
                base.ctor();
            }

            public virtual object Invoke(TValue x)
            {
                return (object)x;
            }
        }

        [Serializable]
        internal sealed class createBoxedVector<TValue, a> : OptimizedClosures.FSharpFunc<IVectorLocation, OptionalValue<TValue>, OptionalValue<a>>
        {
            public FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<object>, OptionalValue<a>>> f;



            internal createBoxedVector(FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<object>, OptionalValue<a>>> f)
            {
                this.ctor();
                this.f = f;
            }

            public virtual OptionalValue<a> Invoke(IVectorLocation loc, OptionalValue<TValue> v)
            {
                FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<object>, OptionalValue<a>>> f = this.f;
                IVectorLocation vectorLocation = loc;
                FSharpFunc<TValue, object> fsharpFunc = (FSharpFunc<TValue, object>)new VectorHelpers.createBoxedVector<TValue>();
                OptionalValue<TValue> optionalValue1 = v;
                OptionalValue<object> optionalValue2 = !optionalValue1.HasValue ? OptionalValue<object>.Missing : new OptionalValue<object>(fsharpFunc.Invoke(optionalValue1.Value));
                return (OptionalValue<a>)FSharpFunc<IVectorLocation, OptionalValue<object>>.InvokeFast<OptionalValue<a>>((FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<object>, M0>>)f, vectorLocation, optionalValue2);
            }
        }

        [Serializable]
        internal sealed class createBoxedVector<TValue> : FSharpFunc<TValue, object>
        {


            internal createBoxedVector()
            {
                base.ctor();
            }

            public virtual object Invoke(TValue x)
            {
                return (object)x;
            }
        }

        [Serializable]
        internal sealed class createBoxedVector0<TValue, a> : FSharpFunc<TValue, a>
        {



            public FSharpFunc<TValue, object> f;



            public FSharpFunc<object, a> g;



            internal createBoxedVector0(FSharpFunc<TValue, object> f, FSharpFunc<object, a> g)
            {
                this.ctor();
                this.f = f;
                this.g = g;
            }

            public virtual a Invoke(TValue x)
            {
                return this.g.Invoke(this.f.Invoke(x));
            }
        }

        [Serializable]
        internal sealed class createBoxedVector1<TValue> : FSharpFunc<object, TValue>
        {


            internal createBoxedVector1()
            {
                base.ctor();
            }

            public virtual TValue Invoke(object x)
            {
                return (TValue)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<TValue>(x);
            }
        }

        [Serializable]
        internal sealed class createBoxedVector2<TValue, a> : FSharpFunc<a, TValue>
        {



            public FSharpFunc<a, object> f;



            public FSharpFunc<object, TValue> g;



            internal createBoxedVector2(FSharpFunc<a, object> f, FSharpFunc<object, TValue> g)
            {
                this.ctor();
                this.f = f;
                this.g = g;
            }

            public virtual TValue Invoke(a x)
            {
                return this.g.Invoke(this.f.Invoke(x));
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class createBoxedVector<TValue> : VectorHelpers.IBoxedVector, IVector<object>, IVector
        {
            public IVector<TValue> vector;

            public createBoxedVector(IVector<TValue> vector)
            {
                this.vector = vector;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.createBoxedVector<TValue> createBoxedVector63 = this;
            }

            public override bool Equals(object another)
            {
                return this.vector.Equals(another);
            }

            public override int GetHashCode()
            {
                return this.vector.GetHashCode();
            }

            IVector VectorHelpers.IBoxedVector.get_UnboxedVector()
            {
                return (IVector)this.vector;
            }

            OptionalValue<object> IVector<object>.GetValue(long a)
            {
                return this.vector.GetObject(a);
            }

            OptionalValue<object> IVector<object>.GetValueAtLocation(IVectorLocation l)
            {
                OptionalValue<TValue> valueAtLocation = this.vector.GetValueAtLocation(l);
                FSharpFunc<TValue, object> fsharpFunc = (FSharpFunc<TValue, object>)new VectorHelpers.createBoxedVector<TValue>();
                OptionalValue<TValue> optionalValue = valueAtLocation;
                if (optionalValue.HasValue)
                    return new OptionalValue<object>(fsharpFunc.Invoke(optionalValue.Value));
                return OptionalValue<object>.Missing;
            }

            VectorData<object> IVector<object>.get_Data()
            {
                VectorData<TValue> data = this.vector.Data;
                VectorData<TValue> vectorData = data;
                if (!(vectorData is VectorData<TValue>.SparseList))
                {
                    if (vectorData is VectorData<TValue>.Sequence)
                        return VectorData<object>.NewSequence((IEnumerable<OptionalValue<object>>)SeqModule.Map<OptionalValue<TValue>, OptionalValue<object>>((FSharpFunc<M0, M1>)new VectorHelpers.createBoxedVector<TValue>((FSharpFunc<TValue, object>)new VectorHelpers.createBoxedVector<TValue>()), (IEnumerable<M0>)((VectorData<TValue>.Sequence)data).item));
                    ReadOnlyCollection<TValue> readOnlyCollection1 = ((VectorData<TValue>.DenseList)data).item;
                    FSharpFunc<TValue, object> fsharpFunc = (FSharpFunc<TValue, object>)new VectorHelpers.createBoxedVector<TValue>();
                    ReadOnlyCollection<TValue> readOnlyCollection2 = readOnlyCollection1;
                    object[] array = (object[])ArrayModule.ZeroCreate<object>(readOnlyCollection2.Count);
                    int index = 0;
                    int num = readOnlyCollection2.Count - 1;
                    if (num >= index)
                    {
                        do
                        {
                            array[index] = fsharpFunc.Invoke(readOnlyCollection2[index]);
                            ++index;
                        }
                        while (index != num + 1);
                    }
                    return VectorData<object>.NewDenseList(System.Array.AsReadOnly<object>(array));
                }
                ReadOnlyCollection<OptionalValue<TValue>> readOnlyCollection3 = ((VectorData<TValue>.SparseList)data).item;
                FSharpFunc<OptionalValue<TValue>, OptionalValue<object>> fsharpFunc1 = (FSharpFunc<OptionalValue<TValue>, OptionalValue<object>>)new VectorHelpers.createBoxedVector<TValue>((FSharpFunc<TValue, object>)new VectorHelpers.createBoxedVector<TValue>());
                ReadOnlyCollection<OptionalValue<TValue>> readOnlyCollection4 = readOnlyCollection3;
                OptionalValue<object>[] array1 = (OptionalValue<object>[])ArrayModule.ZeroCreate<OptionalValue<object>>(readOnlyCollection4.Count);
                int index1 = 0;
                int num1 = readOnlyCollection4.Count - 1;
                if (num1 >= index1)
                {
                    do
                    {
                        array1[index1] = fsharpFunc1.Invoke(readOnlyCollection4[index1]);
                        ++index1;
                    }
                    while (index1 != num1 + 1);
                }
                return VectorData<object>.NewSparseList(System.Array.AsReadOnly<OptionalValue<object>>(array1));
            }

            IVector<a> IVector<object>.Select<a>(FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<object>, OptionalValue<a>>> f)
            {
                return this.vector.Select<a>((FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<TValue>, OptionalValue<a>>>)new VectorHelpers.createBoxedVector<TValue, a>(f));
            }

            IVector<a> IVector<object>.Convert<a>(FSharpFunc<object, a> f, FSharpFunc<a, object> g)
            {
                return this.vector.Convert<a>((FSharpFunc<TValue, a>)new VectorHelpers.createBoxedVector0<TValue, a>((FSharpFunc<TValue, object>)new VectorHelpers.createBoxedVector<TValue>(), f), (FSharpFunc<a, TValue>)new VectorHelpers.createBoxedVector2<TValue, a>(g, (FSharpFunc<object, TValue>)new VectorHelpers.createBoxedVector1<TValue>()));
            }

            Addressing.IAddressingScheme IVector.get_AddressingScheme()
            {
                return this.vector.AddressingScheme;
            }

            long IVector.get_Length()
            {
                return this.vector.Length;
            }

            IEnumerable<OptionalValue<object>> IVector.get_ObjectSequence()
            {
                return this.vector.ObjectSequence;
            }

            bool IVector.get_SuppressPrinting()
            {
                return this.vector.SuppressPrinting;
            }

            Type IVector.get_ElementType()
            {
                return typeof(object);
            }

            OptionalValue<object> IVector.GetObject(long i)
            {
                return this.vector.GetObject(i);
            }

            a IVector.Invoke<a>(VectorCallSite<a> site)
            {
                return this.vector.Invoke<a>(site);
            }
        }


        [Serializable]
        internal interface IWrappedVector<T> : IVector<T>
        {
            IVector<T> UnwrapVector();
        }

        [Serializable]
        internal sealed class unwrapVector<TResult, TValue> : FSharpFunc<Unit, IVector<TResult>>
        {
            public FSharpFunc<TValue, TResult> f;
            public IVector<TValue> vector;



            internal unwrapVector(FSharpFunc<TValue, TResult> f, IVector<TValue> vector)
            {
                this.ctor();
                this.f = f;
                this.vector = vector;
            }

            public virtual IVector<TResult> Invoke(Unit unitVar)
            {
                return FVectorextensionscore.IVector`1Select<TValue, TResult>(this.vector, this.f);
            }
        }

        [Serializable]
        internal sealed class lazyMapVector<TValue, TResult> : FSharpFunc<OptionalValue<TValue>, OptionalValue<TResult>>
        {



            public FSharpFunc<TValue, TResult> f;



            internal lazyMapVector(FSharpFunc<TValue, TResult> f)
            {
                this.ctor();
                this.f = f;
            }

            public virtual OptionalValue<TResult> Invoke(OptionalValue<TValue> input)
            {
                if (input.HasValue)
                    return new OptionalValue<TResult>(this.f.Invoke(input.Value));
                return OptionalValue<TResult>.Missing;
            }
        }

        [Serializable]
        internal sealed class lazyMapVector<TValue, TResult> : FSharpFunc<OptionalValue<TValue>, OptionalValue<TResult>>
        {



            public FSharpFunc<TValue, TResult> f;



            internal lazyMapVector(FSharpFunc<TValue, TResult> f)
            {
                this.ctor();
                this.f = f;
            }

            public virtual OptionalValue<TResult> Invoke(OptionalValue<TValue> input)
            {
                if (input.HasValue)
                    return new OptionalValue<TResult>(this.f.Invoke(input.Value));
                return OptionalValue<TResult>.Missing;
            }
        }

        [Serializable]
        internal sealed class lazyMapVector<TValue, a, TResult> : OptimizedClosures.FSharpFunc<IVectorLocation, OptionalValue<TValue>, OptionalValue<a>>
        {
            public FSharpFunc<TValue, TResult> f;
            public FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<TResult>, OptionalValue<a>>> g;



            internal lazyMapVector(FSharpFunc<TValue, TResult> f, FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<TResult>, OptionalValue<a>>> g)
            {
                this.ctor();
                this.f = f;
                this.g = g;
            }

            public virtual OptionalValue<a> Invoke(IVectorLocation loc, OptionalValue<TValue> v)
            {
                FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<TResult>, OptionalValue<a>>> g = this.g;
                IVectorLocation vectorLocation = loc;
                FSharpFunc<TValue, TResult> f = this.f;
                OptionalValue<TValue> optionalValue1 = v;
                OptionalValue<TResult> optionalValue2 = !optionalValue1.HasValue ? OptionalValue<TResult>.Missing : new OptionalValue<TResult>(f.Invoke(optionalValue1.Value));
                return (OptionalValue<a>)FSharpFunc<IVectorLocation, OptionalValue<TResult>>.InvokeFast<OptionalValue<a>>((FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<TResult>, M0>>)g, vectorLocation, optionalValue2);
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class lazyMapVector<TValue, TResult> : IVector<TResult>, VectorHelpers.IWrappedVector<TResult>, IVector
        {
            public FSharpFunc<TValue, TResult> f;
            public IVector<TValue> vector;
            public Lazy<IVector<TResult>> unwrapVector;

            public lazyMapVector(FSharpFunc<TValue, TResult> f, IVector<TValue> vector, Lazy<IVector<TResult>> unwrapVector)
            {
                this.f = f;
                this.vector = vector;
                this.unwrapVector = unwrapVector;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.lazyMapVector<TValue, TResult> lazyMapVector108 = this;
            }

            public override bool Equals(object another)
            {
                return this.vector.Equals(another);
            }

            public override int GetHashCode()
            {
                return this.vector.GetHashCode();
            }

            OptionalValue<TResult> IVector<TResult>.GetValue(long a)
            {
                OptionalValue<TValue> optionalValue1 = this.vector.GetValue(a);
                FSharpFunc<TValue, TResult> f = this.f;
                OptionalValue<TValue> optionalValue2 = optionalValue1;
                if (optionalValue2.HasValue)
                    return new OptionalValue<TResult>(f.Invoke(optionalValue2.Value));
                return OptionalValue<TResult>.Missing;
            }

            OptionalValue<TResult> IVector<TResult>.GetValueAtLocation(IVectorLocation l)
            {
                OptionalValue<TValue> valueAtLocation = this.vector.GetValueAtLocation(l);
                FSharpFunc<TValue, TResult> f = this.f;
                OptionalValue<TValue> optionalValue = valueAtLocation;
                if (optionalValue.HasValue)
                    return new OptionalValue<TResult>(f.Invoke(optionalValue.Value));
                return OptionalValue<TResult>.Missing;
            }

            VectorData<TResult> IVector<TResult>.get_Data()
            {
                VectorData<TValue> data = this.vector.Data;
                VectorData<TValue> vectorData = data;
                if (!(vectorData is VectorData<TValue>.SparseList))
                {
                    if (vectorData is VectorData<TValue>.Sequence)
                        return VectorData<TResult>.NewSequence((IEnumerable<OptionalValue<TResult>>)SeqModule.Map<OptionalValue<TValue>, OptionalValue<TResult>>((FSharpFunc<M0, M1>)new VectorHelpers.lazyMapVector<TValue, TResult>(this.f), (IEnumerable<M0>)((VectorData<TValue>.Sequence)data).item));
                    ReadOnlyCollection<TValue> readOnlyCollection1 = ((VectorData<TValue>.DenseList)data).item;
                    FSharpFunc<TValue, TResult> f = this.f;
                    ReadOnlyCollection<TValue> readOnlyCollection2 = readOnlyCollection1;
                    TResult[] array = (TResult[])ArrayModule.ZeroCreate<TResult>(readOnlyCollection2.Count);
                    int index = 0;
                    int num = readOnlyCollection2.Count - 1;
                    if (num >= index)
                    {
                        do
                        {
                            array[index] = f.Invoke(readOnlyCollection2[index]);
                            ++index;
                        }
                        while (index != num + 1);
                    }
                    return VectorData<TResult>.NewDenseList(System.Array.AsReadOnly<TResult>(array));
                }
                ReadOnlyCollection<OptionalValue<TValue>> readOnlyCollection3 = ((VectorData<TValue>.SparseList)data).item;
                FSharpFunc<OptionalValue<TValue>, OptionalValue<TResult>> fsharpFunc = (FSharpFunc<OptionalValue<TValue>, OptionalValue<TResult>>)new VectorHelpers.lazyMapVector<TValue, TResult>(this.f);
                ReadOnlyCollection<OptionalValue<TValue>> readOnlyCollection4 = readOnlyCollection3;
                OptionalValue<TResult>[] array1 = (OptionalValue<TResult>[])ArrayModule.ZeroCreate<OptionalValue<TResult>>(readOnlyCollection4.Count);
                int index1 = 0;
                int num1 = readOnlyCollection4.Count - 1;
                if (num1 >= index1)
                {
                    do
                    {
                        array1[index1] = fsharpFunc.Invoke(readOnlyCollection4[index1]);
                        ++index1;
                    }
                    while (index1 != num1 + 1);
                }
                return VectorData<TResult>.NewSparseList(System.Array.AsReadOnly<OptionalValue<TResult>>(array1));
            }

            IVector<a> IVector<TResult>.Select<a>(FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<TResult>, OptionalValue<a>>> g)
            {
                return this.vector.Select<a>((FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<TValue>, OptionalValue<a>>>)new VectorHelpers.lazyMapVector<TValue, a, TResult>(this.f, g));
            }

            IVector<a> IVector<TResult>.Convert<a>(FSharpFunc<TResult, a> h, FSharpFunc<a, TResult> g)
            {
                throw new InvalidOperationException("lazyMapVector: Conversion is not supported");
            }

            IVector<TResult> VectorHelpers.IWrappedVector<TResult>.UnwrapVector()
            {
                return this.unwrapVector.Value;
            }

            Addressing.IAddressingScheme IVector.get_AddressingScheme()
            {
                return this.vector.AddressingScheme;
            }

            long IVector.get_Length()
            {
                return this.vector.Length;
            }

            IEnumerable<OptionalValue<object>> IVector.get_ObjectSequence()
            {
                return this.vector.ObjectSequence;
            }

            bool IVector.get_SuppressPrinting()
            {
                return this.vector.SuppressPrinting;
            }

            Type IVector.get_ElementType()
            {
                return typeof(TResult);
            }

            OptionalValue<object> IVector.GetObject(long i)
            {
                return this.vector.GetObject(i);
            }

            a IVector.Invoke<a>(VectorCallSite<a> site)
            {
                throw new InvalidOperationException("lazyMapVector: Invocation is not supported");
            }
        }


        [Serializable]
        internal interface ValueCallSite<R>
        {
            R Invoke<T>([In] T obj0);
        }

        [Serializable]
        internal sealed class dict<R> : FSharpFunc<Unit, Dictionary<IntPtr, Func<VectorHelpers.ValueCallSite<R>, object, R>>>
        {


            internal dict()
            {
                base.ctor();
            }

            public virtual Dictionary<IntPtr, Func<VectorHelpers.ValueCallSite<R>, object, R>> Invoke(Unit unitVar)
            {
                return new Dictionary<IntPtr, Func<VectorHelpers.ValueCallSite<R>, object, R>>();
            }
        }

        [Serializable]
        internal sealed class createValueDispatcher<R> : FSharpFunc<object, R>
        {
            public VectorHelpers.ValueCallSite<R> callSite;
            public Lazy<Dictionary<IntPtr, Func<VectorHelpers.ValueCallSite<R>, object, R>>> dict;



            internal createValueDispatcher(VectorHelpers.ValueCallSite<R> callSite, Lazy<Dictionary<IntPtr, Func<VectorHelpers.ValueCallSite<R>, object, R>>> dict)
            {
                this.ctor();
                this.callSite = callSite;
                this.dict = dict;
            }

            public virtual R Invoke(object value)
            {
                Type type = value.GetType();
                IntPtr key = type.TypeHandle.Value;
                if (key == VectorHelpers.doubleCode)
                    return this.callSite.Invoke<double>((double)value);
                if (key == VectorHelpers.intCode)
                    return this.callSite.Invoke<int>((int)value);
                if (key == VectorHelpers.stringCode)
                    return this.callSite.Invoke<string>((string)value);
                Func<VectorHelpers.ValueCallSite<R>, object, R> func1 = (Func<VectorHelpers.ValueCallSite<R>, object, R>)null;
                Tuple<bool, Func<VectorHelpers.ValueCallSite<R>, object, R>> tuple = new Tuple<bool, Func<VectorHelpers.ValueCallSite<R>, object, R>>(this.dict.Value.TryGetValue(key, out func1), func1);
                if (tuple.Item1)
                    return tuple.Item2(this.callSite, value);
                MethodInfo method = typeof(VectorHelpers.ValueCallSite<R>).GetMethod(nameof(Invoke)).MakeGenericMethod(type);
                ParameterExpression parameterExpression1 = Expression.Parameter(typeof(VectorHelpers.ValueCallSite<R>));
                ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object));
                Func<VectorHelpers.ValueCallSite<R>, object, R> func2 = Expression.Lambda<Func<VectorHelpers.ValueCallSite<R>, object, R>>((Expression)Expression.Call((Expression)parameterExpression1, method, new Expression[1] { (Expression)Expression.Convert((Expression)parameterExpression2, type) }), (IEnumerable<ParameterExpression>)FSharpList<ParameterExpression>.Cons(parameterExpression1, FSharpList<ParameterExpression>.Cons(parameterExpression2, FSharpList<ParameterExpression>.get_Empty()))).Compile();
                this.dict.Value[key] = func2;
                return func2(this.callSite, value);
            }
        }


        [Serializable]
        internal class BinaryTransform
        {
            internal static VectorListTransform Create<T>(FSharpFunc<OptionalValue<T>, FSharpFunc<OptionalValue<T>, OptionalValue<T>>> operation)
            {
                return VectorListTransform.NewBinary((IBinaryTransform)new VectorHelpers.Create<T>(operation));
            }

            internal static VectorListTransform CreateLifted<T>(FSharpFunc<T, FSharpFunc<T, T>> operation)
            {
                return VectorListTransform.NewBinary((IBinaryTransform)new VectorHelpers.CreateLifted<T>(operation));
            }

            internal static VectorListTransform LeftIfAvailable
            {
                get
                {
                    return VectorListTransform.NewBinary((IBinaryTransform)new VectorHelpers.get_LeftIfAvailable());
                }
            }

            internal static VectorListTransform RightIfAvailable
            {
                get
                {
                    return VectorListTransform.NewBinary((IBinaryTransform)new VectorHelpers.get_RightIfAvailable());
                }
            }

            internal static VectorListTransform AtMostOne
            {
                get
                {
                    return VectorListTransform.NewBinary((IBinaryTransform)new VectorHelpers.get_AtMostOne());
                }
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class Create<T> : IBinaryTransform
        {
            public FSharpFunc<OptionalValue<T>, FSharpFunc<OptionalValue<T>, OptionalValue<T>>> operation;

            public Create(FSharpFunc<OptionalValue<T>, FSharpFunc<OptionalValue<T>, OptionalValue<T>>> operation)
            {
                this.operation = operation;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.Create<T> create181 = this;
            }

            FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>> IBinaryTransform.GetFunction<R>()
            {
                return (FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>>)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>>>((object)this.operation);
            }

            bool IBinaryTransform.get_IsMissingUnit()
            {
                return false;
            }
        }

        [Serializable]
        internal sealed class CreateLifted<R, T> : OptimizedClosures.FSharpFunc<OptionalValue<R>, OptionalValue<R>, OptionalValue<R>>
        {
            public FSharpFunc<T, FSharpFunc<T, T>> operation;



            internal CreateLifted(FSharpFunc<T, FSharpFunc<T, T>> operation)
            {
                this.ctor();
                this.operation = operation;
            }

            public virtual OptionalValue<R> Invoke(OptionalValue<R> l, OptionalValue<R> r)
            {
                if ((!l.HasValue ? 0 : (r.HasValue ? 1 : 0)) != 0)
                    return new OptionalValue<R>((R)FSharpFunc<R, R>.InvokeFast<R>((FSharpFunc<R, FSharpFunc<R, M0>>)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<FSharpFunc<R, FSharpFunc<R, R>>>((object)this.operation), l.Value, r.Value));
                return OptionalValue<R>.Missing;
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class CreateLifted<T> : IBinaryTransform
        {
            public FSharpFunc<T, FSharpFunc<T, T>> operation;

            public CreateLifted(FSharpFunc<T, FSharpFunc<T, T>> operation)
            {
                this.operation = operation;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.CreateLifted<T> createLifted189 = this;
            }

            FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>> IBinaryTransform.GetFunction<R>()
            {
                return (FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>>)new VectorHelpers.CreateLifted<R, T>(this.operation);
            }

            bool IBinaryTransform.get_IsMissingUnit()
            {
                return false;
            }
        }

        [Serializable]
        internal sealed class get_LeftIfAvailable<R> : OptimizedClosures.FSharpFunc<OptionalValue<R>, OptionalValue<R>, OptionalValue<R>>
        {


            internal get_LeftIfAvailable()
            {
                base.ctor();
            }

            public virtual OptionalValue<R> Invoke(OptionalValue<R> l, OptionalValue<R> r)
            {
                if (l.HasValue)
                    return l;
                return r;
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class get_LeftIfAvailable : IBinaryTransform
        {
            public get_LeftIfAvailable()
            {
                VectorHelpers.get_LeftIfAvailable leftIfAvailable198 = this;
            }

            FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>> IBinaryTransform.GetFunction<R>()
            {
                return (FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>>)new VectorHelpers.get_LeftIfAvailable<R>();
            }

            bool IBinaryTransform.get_IsMissingUnit()
            {
                return true;
            }
        }

        [Serializable]
        internal sealed class get_RightIfAvailable<R> : OptimizedClosures.FSharpFunc<OptionalValue<R>, OptionalValue<R>, OptionalValue<R>>
        {


            internal get_RightIfAvailable()
            {
                base.ctor();
            }

            public virtual OptionalValue<R> Invoke(OptionalValue<R> l, OptionalValue<R> r)
            {
                if (r.HasValue)
                    return r;
                return l;
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class get_RightIfAvailable : IBinaryTransform
        {
            public get_RightIfAvailable()
            {
                VectorHelpers.get_RightIfAvailable rightIfAvailable206 = this;
            }

            FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>> IBinaryTransform.GetFunction<R>()
            {
                return (FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>>)new VectorHelpers.get_RightIfAvailable<R>();
            }

            bool IBinaryTransform.get_IsMissingUnit()
            {
                return true;
            }
        }

        [Serializable]
        internal sealed class get_AtMostOne<R> : OptimizedClosures.FSharpFunc<OptionalValue<R>, OptionalValue<R>, OptionalValue<R>>
        {


            internal get_AtMostOne()
            {
                base.ctor();
            }

            public virtual OptionalValue<R> Invoke(OptionalValue<R> l, OptionalValue<R> r)
            {
                if ((!l.HasValue ? 0 : (r.HasValue ? 1 : 0)) != 0)
                    throw new InvalidOperationException("Combining vectors failed - both vectors have a value.");
                if (l.HasValue)
                    return l;
                return r;
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class get_AtMostOne : IBinaryTransform
        {
            public get_AtMostOne()
            {
                VectorHelpers.get_AtMostOne getAtMostOne214 = this;
            }

            FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>> IBinaryTransform.GetFunction<R>()
            {
                return (FSharpFunc<OptionalValue<R>, FSharpFunc<OptionalValue<R>, OptionalValue<R>>>)new VectorHelpers.get_AtMostOne<R>();
            }

            bool IBinaryTransform.get_IsMissingUnit()
            {
                return true;
            }
        }


        [Serializable]
        internal class NaryTransform
        {
            internal static VectorListTransform Create<T>(FSharpFunc<FSharpList<OptionalValue<T>>, OptionalValue<T>> operation)
            {
                return VectorListTransform.NewNary((INaryTransform)new VectorHelpers.Create<T>(operation));
            }

            internal static VectorListTransform AtMostOne
            {
                get
                {
                    return VectorListTransform.NewNary((INaryTransform)new VectorHelpers.get_AtMostOne());
                }
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class Create<T> : INaryTransform
        {
            public FSharpFunc<FSharpList<OptionalValue<T>>, OptionalValue<T>> operation;

            public Create(FSharpFunc<FSharpList<OptionalValue<T>>, OptionalValue<T>> operation)
            {
                this.operation = operation;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.Create<T> create2241 = this;
            }

            FSharpFunc<FSharpList<OptionalValue<R>>, OptionalValue<R>> INaryTransform.GetFunction<R>()
            {
                return (FSharpFunc<FSharpList<OptionalValue<R>>, OptionalValue<R>>)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<FSharpFunc<FSharpList<OptionalValue<R>>, OptionalValue<R>>>((object)this.operation);
            }
        }

        [Serializable]
        internal sealed class get_AtMostOne<R> : OptimizedClosures.FSharpFunc<OptionalValue<R>, OptionalValue<R>, OptionalValue<R>>
        {


            internal get_AtMostOne()
            {
                base.ctor();
            }

            public virtual OptionalValue<R> Invoke(OptionalValue<R> s, OptionalValue<R> v)
            {
                if ((!s.HasValue ? 0 : (v.HasValue ? 1 : 0)) != 0)
                    throw new InvalidOperationException("Combining vectors failed - more than one vector has a value.");
                if (v.HasValue)
                    return v;
                return s;
            }
        }

        [Serializable]
        internal sealed class get_AtMostOne<R> : FSharpFunc<FSharpList<OptionalValue<R>>, OptionalValue<R>>
        {


            internal get_AtMostOne()
            {
                base.ctor();
            }

            public virtual OptionalValue<R> Invoke(FSharpList<OptionalValue<R>> l)
            {
                return (OptionalValue<R>)ListModule.Fold<OptionalValue<R>, OptionalValue<R>>((FSharpFunc<M1, FSharpFunc<M0, M1>>)new VectorHelpers.get_AtMostOne<R>(), (M1)OptionalValue<R>.Missing, (FSharpList<M0>)l);
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class get_AtMostOne : INaryTransform
        {
            public get_AtMostOne()
            {
                VectorHelpers.get_AtMostOne getAtMostOne2312 = this;
            }

            FSharpFunc<FSharpList<OptionalValue<R>>, OptionalValue<R>> INaryTransform.GetFunction<R>()
            {
                return (FSharpFunc<FSharpList<OptionalValue<R>>, OptionalValue<R>>)new VectorHelpers.get_AtMostOne<R>();
            }
        }

        [Serializable]
        internal sealed class VectorListTransformGetFunction<T> : FSharpFunc<FSharpList<OptionalValue<T>>, OptionalValue<T>>
        {



            public FSharpFunc<OptionalValue<T>, FSharpFunc<OptionalValue<T>, OptionalValue<T>>> reduction;



            internal VectorListTransformGetFunction(FSharpFunc<OptionalValue<T>, FSharpFunc<OptionalValue<T>, OptionalValue<T>>> reduction)
            {
                this.ctor();
                this.reduction = reduction;
            }

            public virtual OptionalValue<T> Invoke(FSharpList<OptionalValue<T>> list)
            {
                return (OptionalValue<T>)ListModule.Reduce<OptionalValue<T>>((FSharpFunc<M0, FSharpFunc<M0, M0>>)this.reduction, (FSharpList<M0>)list);
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class boxVector : VectorCallSite<IVector<object>>
        {
            public boxVector()
            {
                VectorHelpers.boxVector boxVector248 = this;
            }

            IVector<object> VectorCallSite<IVector<object>>.Invoke<T>(IVector<T> col)
            {
                return (IVector<object>)VectorHelpers.createBoxedVector<T>(col);
            }
        }


        [Serializable]
        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class transformColumn : VectorCallSite<IVector>
        {
            public Vectors.IVectorBuilder vectorBuilder;
            public Addressing.IAddressingScheme scheme;
            public VectorConstruction rowCmd;

            public transformColumn(IVectorBuilder vectorBuilder, Addressing.IAddressingScheme scheme, VectorConstruction rowCmd)
            {
                this.vectorBuilder = vectorBuilder;
                this.scheme = scheme;
                this.rowCmd = rowCmd;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.transformColumn transformColumn261 = this;
            }

            IVector VectorCallSite<IVector>.Invoke<T>(IVector<T> col)
            {
                return (IVector)this.vectorBuilder.Build<T>(this.scheme, this.rowCmd, new IVector<T>[1] { col });
            }
        }

        [Serializable]
        internal sealed class convertType<R, T> : FSharpFunc<T, R>
        {



            public ConversionKind conversionKind;



            internal convertType(ConversionKind conversionKind)
            {
                this.ctor();
                this.conversionKind = conversionKind;
            }

            public virtual R Invoke(T value)
            {
                return Deedle.Internal.Convert.convertType<R>(this.conversionKind, (object)value);
            }
        }

        [Serializable]
        internal sealed class convertType<R, T> : FSharpFunc<R, T>
        {



            public ConversionKind conversionKind;



            internal convertType(ConversionKind conversionKind)
            {
                this.ctor();
                this.conversionKind = conversionKind;
            }

            public virtual T Invoke(R value)
            {
                return Deedle.Internal.Convert.convertType<T>(this.conversionKind, (object)value);
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class convertType<R> : VectorCallSite<IVector<R>>
        {
            public ConversionKind conversionKind;

            public convertType(ConversionKind conversionKind)
            {
                this.conversionKind = conversionKind;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.convertType<R> convertType2722 = this;
            }

            IVector<R> VectorCallSite<IVector<R>>.Invoke<T>(IVector<T> col)
            {
                return col.Convert<R>((FSharpFunc<T, R>)new VectorHelpers.convertType<R, T>(this.conversionKind), (FSharpFunc<R, T>)new VectorHelpers.convertType<R, T>(this.conversionKind));
            }
        }

        [Serializable]
        internal sealed class convertTypeMethod : FSharpFunc<Unit, MethodInfo>
        {


            internal convertTypeMethod()
            {
                base.ctor();
            }

            public virtual MethodInfo Invoke(Unit unitVar0)
            {
                return typeof(OptionalValue<int>).Assembly.GetType("Deedle.VectorHelpers").GetMethod("convertType", (BindingFlags)(32 | 8));
            }
        }

        [Serializable]
        internal sealed class first<T> : FSharpFunc<OptionalValue<T>, FSharpOption<object>>
        {


            internal first()
            {
                base.ctor();
            }

            public virtual FSharpOption<object> Invoke(OptionalValue<T> v)
            {
                if ((!v.HasValue ? 0 : (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<object>((M0)(object)v.Value, (M0)(object)null) ? 1 : 0)) != 0)
                    return FSharpOption<object>.Some((object)v.Value);
                return (FSharpOption<object>)null;
            }
        }

        [Serializable]
        internal sealed class first<R> : FSharpFunc<object, bool>
        {



            public ConversionKind conversionKind;



            internal first(ConversionKind conversionKind)
            {
                this.ctor();
                this.conversionKind = conversionKind;
            }

            public virtual bool Invoke(object value)
            {
                return Deedle.Internal.Convert.canConvertType<R>(this.conversionKind, value);
            }
        }

        [Serializable]
        internal sealed class tryConvertType<R, T> : FSharpFunc<T, R>
        {
            public ConversionKind conversionKind;



            internal tryConvertType(ConversionKind conversionKind)
            {
                this.ctor();
                this.conversionKind = conversionKind;
            }

            public virtual R Invoke(T v)
            {
                return Deedle.Internal.Convert.convertType<R>(this.conversionKind, (object)v);
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class tryConvertType<R> : VectorCallSite<OptionalValue<IVector<R>>>
        {
            public ConversionKind conversionKind;

            public tryConvertType(ConversionKind conversionKind)
            {
                this.conversionKind = conversionKind;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.tryConvertType<R> tryConvertType293 = this;
            }

            OptionalValue<IVector<R>> VectorCallSite<OptionalValue<IVector<R>>>.Invoke<T>(IVector<T> col)
            {
                if (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<FSharpOption<bool>>((M0)OptionModule.Map<object, bool>((FSharpFunc<M0, M1>)new VectorHelpers.first<R>(this.conversionKind), (FSharpOption<M0>)Seq.headOrNone<object>((IEnumerable<object>)SeqModule.Choose<OptionalValue<T>, object>((FSharpFunc<M0, FSharpOption<M1>>)new VectorHelpers.first<T>(), (IEnumerable<M0>)FVectorextensionscore.IVector`1get_DataSequence<T>(col)))), (M0)FSharpOption<bool>.Some(false)))
                    return OptionalValue<IVector<R>>.Missing;
                try
                {
                    return new OptionalValue<IVector<R>>(FVectorextensionscore.IVector`1Select<T, R>(col, (FSharpFunc<T, R>)new VectorHelpers.tryConvertType<R, T>(this.conversionKind)));
                }
                catch (object ex)
                {
                    if ((Exception)ex is InvalidCastException || (Exception)ex is FormatException)
                        return OptionalValue<IVector<R>>.Missing;
                    throw;
                }
            }
        }


        [Serializable]
        internal class RowReaderVector<T> : IVector, IVector<T>
        {
            internal long rowAddress;
            internal IVector<IVector> data;
            internal FSharpFunc<long, long> colAddressAt;
            internal IVectorBuilder builder;

            public RowReaderVector(IVector<IVector> data, IVectorBuilder builder, long rowAddress, FSharpFunc<long, long> colAddressAt)
            {
                VectorHelpers.RowReaderVector<T> rowReaderVector = this;
                this.data = data;
                this.builder = builder;
                this.rowAddress = rowAddress;
                this.colAddressAt = colAddressAt;
            }

            public override bool Equals(object another)
            {
                object obj = another;
                if (obj == null)
                    return false;
                IVector<T> vector = obj as IVector<T>;
                if (vector != null)
                    return Seq.structuralEquals<OptionalValue<T>>(FVectorextensionscore.IVector`1get_DataSequence<T>((IVector<T>)this), FVectorextensionscore.IVector`1get_DataSequence<T>(vector));
                return false;
            }

            public override int GetHashCode()
            {
                return Seq.structuralHash<OptionalValue<T>>(FVectorextensionscore.IVector`1get_DataSequence<T>((IVector<T>)this));
            }

            internal OptionalValue<T>[] DataArray
            {
                get
                {
                    int length = (int)this.data.Length;
                    FSharpFunc<int, OptionalValue<T>> fsharpFunc = (FSharpFunc<int, OptionalValue<T>>)new VectorHelpers.get_DataArray<T>(this);
                    if (length < 0)
                        throw new ArgumentException(string.Format("{0}\n{1} = {2}", new object[3] { (object)LanguagePrimitives.ErrorStrings.get_InputMustBeNonNegativeString(), (object)"count", (object)length }), "count");
                    OptionalValue<T>[] optionalValueArray = new OptionalValue<T>[length];
                    for (int index = 0; index < optionalValueArray.Length; ++index)
                        optionalValueArray[index] = fsharpFunc.Invoke(index);
                    return optionalValueArray;
                }
            }

            OptionalValue<T> IVector<T>.GetValue(long columnAddress)
            {
                OptionalValue<IVector> optionalValue1 = this.data.GetValue(columnAddress);
                if (!optionalValue1.HasValue)
                    return OptionalValue<T>.Missing;
                OptionalValue<object> optionalValue2 = optionalValue1.Value.GetObject(this.rowAddress);
                FSharpFunc<object, T> fsharpFunc = (FSharpFunc<object, T>)new VectorHelpers.DeedleIVectorGetValue<T>(ConversionKind.Flexible);
                OptionalValue<object> optionalValue3 = optionalValue2;
                if (optionalValue3.HasValue)
                    return new OptionalValue<T>(fsharpFunc.Invoke(optionalValue3.Value));
                return OptionalValue<T>.Missing;
            }

            OptionalValue<T> IVector<T>.GetValueAtLocation(IVectorLocation loc)
            {
                return ((IVector<T>)this).GetValue(loc.Address);
            }


            VectorData<T> IVector<T>.get_Data()
            {
                return VectorData<T>.NewSparseList(System.Array.AsReadOnly<OptionalValue<T>>(this.DataArray));
            }

            IVector<a> IVector<T>.Select<a>(FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<T>, OptionalValue<a>>> f)
            {
                Type type1 = typeof(a);
                int num;
                if (type1.IsGenericType)
                {
                    Type genericTypeDefinition = type1.GetGenericTypeDefinition();
                    Type type2 = typeof(int?);
                    Type type3 = !type2.GetTypeInfo().IsGenericType ? type2 : type2.GetGenericTypeDefinition();
                    num = LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)genericTypeDefinition, (M0)type3) ? 1 : 0;
                }
                else
                    num = 0;
                bool flag = num != 0;
                FSharpFunc<OptionalValue<a>, OptionalValue<a>> flattenNA = (FSharpFunc<OptionalValue<a>, OptionalValue<a>>)new VectorHelpers.flattenNA<a>(!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)type1, (M0)typeof(double)) ? (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)type1, (M0)typeof(float)) ? ((!type1.IsValueType ? 0 : (!flag ? 1 : 0)) == 0 ? (FSharpFunc<a, bool>)new VectorHelpers.isNA9<a>() : (FSharpFunc<a, bool>)new VectorHelpers.isNA8<a>()) : (FSharpFunc<a, bool>)new VectorHelpers.isNA7<a>((FSharpFunc<a, bool>)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<FSharpFunc<a, bool>>((object)new VectorHelpers.isNA6()))) : (FSharpFunc<a, bool>)new VectorHelpers.isNA5<a>((FSharpFunc<a, bool>)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<FSharpFunc<a, bool>>((object)new VectorHelpers.isNA4())));
                return this.builder.CreateMissing<a>((OptionalValue<a>[])ArrayModule.MapIndexed<OptionalValue<T>, OptionalValue<a>>((FSharpFunc<int, FSharpFunc<M0, M1>>)new VectorHelpers.data<T, a>(this, f, flattenNA), (M0[])this.DataArray));
            }

            IVector<a> IVector<T>.Convert<a>(FSharpFunc<T, a> f, FSharpFunc<a, T> _arg1)
            {
                return FVectorextensionscore.IVector`1Select<T, a>((IVector<T>)this, f);
            }


            Addressing.IAddressingScheme IVector.get_AddressingScheme()
            {
                return this.data.AddressingScheme;
            }


            long IVector.get_Length()
            {
                return this.data.Length;
            }


            IEnumerable<OptionalValue<object>> IVector.get_ObjectSequence()
            {
                return (IEnumerable<OptionalValue<object>>)SeqModule.Map<OptionalValue<T>, OptionalValue<object>>((FSharpFunc<M0, M1>)new VectorHelpers.DeedleIVectorget_ObjectSequence<T>((FSharpFunc<T, object>)new VectorHelpers.DeedleIVectorget_ObjectSequence<T>()), (IEnumerable<M0>)this.DataArray);
            }


            bool IVector.get_SuppressPrinting()
            {
                return false;
            }


            Type IVector.get_ElementType()
            {
                return typeof(T);
            }

            OptionalValue<object> IVector.GetObject(long i)
            {
                FSharpFunc<T, object> fsharpFunc = (FSharpFunc<T, object>)new VectorHelpers.DeedleIVectorGetObject<T>();
                OptionalValue<T> optionalValue = ((IVector<T>)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<IVector<T>>((object)this)).GetValue(i);
                if (optionalValue.HasValue)
                    return new OptionalValue<object>(fsharpFunc.Invoke(optionalValue.Value));
                return OptionalValue<object>.Missing;
            }

            a IVector.Invoke<a>(VectorCallSite<a> site)
            {
                return site.Invoke<T>((IVector<T>)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<IVector<T>>((object)this));
            }
        }

        [Serializable]
        internal sealed class get_DataArray<T> : FSharpFunc<int, OptionalValue<T>>
        {
            public VectorHelpers.RowReaderVector<T> vector;



            internal get_DataArray(VectorHelpers.RowReaderVector<T> vector)
            {
                this.ctor();
                this.vector = vector;
            }

            public virtual OptionalValue<T> Invoke(int index)
            {
                return ((IVector<T>)this.vector).GetValue(this.vector.colAddressAt.Invoke((long)index));
            }
        }

        [Serializable]
        internal sealed class DeedleIVectorGetValue<T> : FSharpFunc<object, T>
        {



            public ConversionKind conversionKind;



            internal DeedleIVectorGetValue(ConversionKind conversionKind)
            {
                this.ctor();
                this.conversionKind = conversionKind;
            }

            public virtual T Invoke(object value)
            {
                return Deedle.Internal.Convert.convertType<T>(this.conversionKind, value);
            }
        }

        [Serializable]
        internal sealed class isNA4 : FSharpFunc<double, bool>
        {


            internal isNA4()
            {
                base.ctor();
            }

            public virtual bool Invoke(double arg00)
            {
                return double.IsNaN(arg00);
            }
        }

        [Serializable]
        internal sealed class isNA5<a> : FSharpFunc<a, bool>
        {



            public FSharpFunc<a, bool> clo1;



            internal isNA5(FSharpFunc<a, bool> clo1)
            {
                this.ctor();
                this.clo1 = clo1;
            }

            public virtual bool Invoke(a arg10)
            {
                return this.clo1.Invoke(arg10);
            }
        }

        [Serializable]
        internal sealed class isNA6 : FSharpFunc<float, bool>
        {


            internal isNA6()
            {
                base.ctor();
            }

            public virtual bool Invoke(float arg00)
            {
                return float.IsNaN(arg00);
            }
        }

        [Serializable]
        internal sealed class isNA7<a> : FSharpFunc<a, bool>
        {



            public FSharpFunc<a, bool> clo1;



            internal isNA7(FSharpFunc<a, bool> clo1)
            {
                this.ctor();
                this.clo1 = clo1;
            }

            public virtual bool Invoke(a arg10)
            {
                return this.clo1.Invoke(arg10);
            }
        }

        [Serializable]
        internal sealed class isNA8<a> : FSharpFunc<a, bool>
        {


            internal isNA8()
            {
                base.ctor();
            }

            public virtual bool Invoke(a _arg1)
            {
                return false;
            }
        }

        [Serializable]
        internal sealed class isNA9<a> : FSharpFunc<a, bool>
        {


            internal isNA9()
            {
                base.ctor();
            }

            public virtual bool Invoke(a v)
            {
                return object.Equals((object)null, (object)v);
            }
        }

        [Serializable]
        internal sealed class flattenNA<a> : FSharpFunc<OptionalValue<a>, OptionalValue<a>>
        {
            public FSharpFunc<a, bool> isNA;



            internal flattenNA(FSharpFunc<a, bool> isNA)
            {
                this.ctor();
                this.isNA = isNA;
            }

            public virtual OptionalValue<a> Invoke(OptionalValue<a> value)
            {
                if ((!value.HasValue ? 0 : (this.isNA.Invoke(value.Value) ? 1 : 0)) != 0)
                    return OptionalValue<a>.Missing;
                return value;
            }
        }

        [Serializable]
        internal sealed class data<T, a> : FSharpFunc<OptionalValue<T>, OptionalValue<a>>
        {



            public FSharpFunc<OptionalValue<T>, OptionalValue<a>> clo1;



            internal data(FSharpFunc<OptionalValue<T>, OptionalValue<a>> clo1)
            {
                this.ctor();
                this.clo1 = clo1;
            }

            public virtual OptionalValue<a> Invoke(OptionalValue<T> arg10)
            {
                return this.clo1.Invoke(arg10);
            }
        }

        [Serializable]
        internal sealed class data<T, a> : FSharpFunc<KnownLocation, FSharpFunc<OptionalValue<T>, OptionalValue<a>>>
        {



            public FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<T>, OptionalValue<a>>> clo0;



            internal data(FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<T>, OptionalValue<a>>> clo0)
            {
                this.ctor();
                this.clo0 = clo0;
            }

            public virtual FSharpFunc<OptionalValue<T>, OptionalValue<a>> Invoke(KnownLocation arg00)
            {
                return (FSharpFunc<OptionalValue<T>, OptionalValue<a>>)new VectorHelpers.data<T, a>(this.clo0.Invoke((IVectorLocation)arg00));
            }
        }

        [Serializable]
        internal sealed class data<T, a> : OptimizedClosures.FSharpFunc<int, OptionalValue<T>, OptionalValue<a>>
        {
            public VectorHelpers.RowReaderVector<T> vector;
            public FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<T>, OptionalValue<a>>> f;
            public FSharpFunc<OptionalValue<a>, OptionalValue<a>> flattenNA;



            internal data(VectorHelpers.RowReaderVector<T> vector, FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<T>, OptionalValue<a>>> f, FSharpFunc<OptionalValue<a>, OptionalValue<a>> flattenNA)
            {
                this.ctor();
                this.vector = vector;
                this.f = f;
                this.flattenNA = flattenNA;
            }

            public virtual OptionalValue<a> Invoke(int idx, OptionalValue<T> v)
            {
                return this.flattenNA.Invoke((OptionalValue<a>)FSharpFunc<KnownLocation, OptionalValue<T>>.InvokeFast<OptionalValue<a>>((FSharpFunc<KnownLocation, FSharpFunc<OptionalValue<T>, M0>>)new VectorHelpers.data<T, a>(this.f), new KnownLocation(this.vector.colAddressAt.Invoke((long)idx), (long)idx), v));
            }
        }


        [Serializable]
        internal class TryValuesHelper
        {
            internal static TryValue<IVector> TryValues<T>(IVector<TryValue<T>> vector)
            {
                FSharpList<Exception> fsharpList = (FSharpList<Exception>)ListModule.OfSeq<Exception>((IEnumerable<M0>)SeqModule.Choose<TryValue<T>, Exception>((FSharpFunc<M0, FSharpOption<M1>>)new VectorHelpers.exceptions<T>(), (IEnumerable<M0>)SeqModule.Choose<OptionalValue<TryValue<T>>, TryValue<T>>((FSharpFunc<M0, FSharpOption<M1>>)new VectorHelpers.exceptions<T>(), (IEnumerable<M0>)FVectorextensionscore.IVector`1get_DataSequence<TryValue<T>>(vector))));
                if (ListModule.IsEmpty<Exception>((FSharpList<M0>)fsharpList))
                    return TryValue<IVector>.NewSuccess((IVector)FVectorextensionscore.IVector`1Select<TryValue<T>, T>(vector, (FSharpFunc<TryValue<T>, T>)new VectorHelpers.TryValues<T>()));
                return TryValue<IVector>.NewError((Exception)new AggregateException((IEnumerable<Exception>)fsharpList));
            }
        }


        [Serializable]
        internal sealed class toArray2D<R> : OptimizedClosures.FSharpFunc<int, OptionalValue<R>, Unit>
        {
            public Lazy<R> defaultValue;
            public R[,] res;
            public int c;



            internal toArray2D(Lazy<R> defaultValue, R[,] res, int c)
            {
                this.ctor();
                this.defaultValue = defaultValue;
                this.res = res;
                this.c = c;
            }

            public virtual Unit Invoke(int r, OptionalValue<R> v)
            {
                this.res[r, this.c] = !v.HasValue ? this.defaultValue.Value : v.Value;
                return (Unit)null;
            }
        }

        [Serializable]
        internal sealed class toArray2D<R> : OptimizedClosures.FSharpFunc<int, OptionalValue<IVector>, Unit>
        {
            public int rowCount;
            public Lazy<R> defaultValue;
            public R[,] res;



            internal toArray2D(int rowCount, Lazy<R> defaultValue, R[,] res)
            {
                this.ctor();
                this.rowCount = rowCount;
                this.defaultValue = defaultValue;
                this.res = res;
            }

            public virtual void Invoke(int c, OptionalValue<IVector> vector)
            {
                if (vector.HasValue)
                {
                    SeqModule.IterateIndexed<OptionalValue<R>>((FSharpFunc<int, FSharpFunc<M0, Unit>>)new VectorHelpers.toArray2D<R>(this.defaultValue, this.res, c), (IEnumerable<M0>)FVectorextensionscore.IVector`1get_DataSequence<R>(VectorHelpers.convertType<R>(ConversionKind.Flexible, vector.Value)));
                    return;
                }
                int index = 0;
                int num = this.rowCount - 1;
                if (num >= index)
                {
                    do
                    {
                        this.res[index, c] = this.defaultValue.Value;
                        ++index;
                    }
                    while (index != num + 1);
                }
                return;
            }
        }


        [Serializable]
        internal class mapFrameRowVector
        {
            internal static IVector<T> Create<T>(IVectorBuilder builder, object[] data)
            {
                IVectorBuilder vectorBuilder1 = builder;
                FSharpFunc<object, T> fsharpFunc = (FSharpFunc<object, T>)new VectorHelpers.Create<T>(ConversionKind.Flexible);
                object[] objArray1 = data;
                if ((object)objArray1 == null)
                    throw new ArgumentNullException("array");
                T[] objArray2 = new T[objArray1.Length];
                IVectorBuilder vectorBuilder2 = vectorBuilder1;
                for (int index = 0; index < objArray2.Length; ++index)
                    objArray2[index] = fsharpFunc.Invoke(objArray1[index]);
                return vectorBuilder2.Create<T>(objArray2);
            }
        }

        [Serializable]
        internal sealed class Create<T> : FSharpFunc<object, T>
        {



            public ConversionKind conversionKind;



            internal Create(ConversionKind conversionKind)
            {
                this.ctor();
                this.conversionKind = conversionKind;
            }

            public virtual T Invoke(object value)
            {
                return Deedle.Internal.Convert.convertType<T>(this.conversionKind, value);
            }
        }

        [Serializable]
        internal sealed class ty<a> : OptimizedClosures.FSharpFunc<Type, a, Type> where a : Type
        {


            internal ty()
            {
                base.ctor();
            }

            public virtual Type Invoke(Type t1, a t2)
            {
                return VectorHelpers.Inference.commonSupertype(t1, (Type)t2);
            }
        }

        [Serializable]
        internal sealed class vectorType : FSharpFunc<object, Type>
        {


            internal vectorType()
            {
                base.ctor();
            }

            public virtual Type Invoke(object v)
            {
                if (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<object>((M0)v, (M0)null))
                    return VectorHelpers.Inference.Top;
                return v.GetType();
            }
        }

        [Serializable]
        internal sealed class mapFrameRowVector : OptimizedClosures.FSharpFunc<long, long, bool>
        {


            internal mapFrameRowVector()
            {
                base.ctor();
            }

            public virtual bool Invoke(long x, long y)
            {
                return x >= y;
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class mapFrameRowVector : IEnumerator, IDisposable, IEnumerator<long>
        {



            public long lo;



            public long hi;



            public long step;



            public FSharpFunc<long, FSharpFunc<long, bool>> geq;



            public FSharpRef<long> current;

            public mapFrameRowVector(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq, FSharpRef<long> current)
            {
                this.lo = lo;
                this.hi = hi;
                this.step = step;
                this.geq = geq;
                this.current = current;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.mapFrameRowVector frameRowVector5084 = this;
            }

            long IEnumerator<long>.get_Current()
            {
                return this.current.get_Value();
            }

            object IEnumerator.get_Current()
            {
                return (object)this.current.get_Value();
            }

            bool IEnumerator.MoveNext()
            {
                if (FSharpFunc<long, long>.InvokeFast<bool>((FSharpFunc<long, FSharpFunc<long, M0>>)this.geq, this.current.get_Value(), this.hi) != null)
                    return false;
                this.current.set_Value(this.current.get_Value() + this.step);
                return true;
            }

            void IEnumerator.Reset()
            {
                this.current.set_Value(this.lo - this.step);
            }

            void IDisposable.Dispose()
            {
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class mapFrameRowVector : IEnumerable, IEnumerable<long>
        {



            public long lo;



            public long hi;



            public long step;



            public FSharpFunc<long, FSharpFunc<long, bool>> geq;

            public mapFrameRowVector(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq)
            {
                this.lo = lo;
                this.hi = hi;
                this.step = step;
                this.geq = geq;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.mapFrameRowVector frameRowVector5083 = this;
            }

            IEnumerator<long> IEnumerable<long>.GetEnumerator()
            {
                return (IEnumerator<long>)new VectorHelpers.mapFrameRowVector(this.lo, this.hi, this.step, this.geq, (FSharpRef<long>)Operators.Ref<long>((M0)(this.lo - this.step)));
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)((IEnumerable<long>)this).GetEnumerator();
            }
        }

        [Serializable]
        internal sealed class mapFrameRowVector : OptimizedClosures.FSharpFunc<long, long, bool>
        {


            internal mapFrameRowVector()
            {
                base.ctor();
            }

            public virtual bool Invoke(long x, long y)
            {
                return x <= y;
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class mapFrameRowVector : IEnumerator, IDisposable, IEnumerator<long>
        {



            public long lo;



            public long hi;



            public long step;



            public FSharpFunc<long, FSharpFunc<long, bool>> geq;



            public FSharpRef<long> current;

            public mapFrameRowVector(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq, FSharpRef<long> current)
            {
                this.lo = lo;
                this.hi = hi;
                this.step = step;
                this.geq = geq;
                this.current = current;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.mapFrameRowVector frameRowVector5087 = this;
            }

            long IEnumerator<long>.get_Current()
            {
                return this.current.get_Value();
            }

            object IEnumerator.get_Current()
            {
                return (object)this.current.get_Value();
            }

            bool IEnumerator.MoveNext()
            {
                if (FSharpFunc<long, long>.InvokeFast<bool>((FSharpFunc<long, FSharpFunc<long, M0>>)this.geq, this.current.get_Value(), this.hi) != null)
                    return false;
                this.current.set_Value(this.current.get_Value() + this.step);
                return true;
            }

            void IEnumerator.Reset()
            {
                this.current.set_Value(this.lo - this.step);
            }

            void IDisposable.Dispose()
            {
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class mapFrameRowVector : IEnumerable, IEnumerable<long>
        {



            public long lo;



            public long hi;



            public long step;



            public FSharpFunc<long, FSharpFunc<long, bool>> geq;

            public mapFrameRowVector(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq)
            {
                this.lo = lo;
                this.hi = hi;
                this.step = step;
                this.geq = geq;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.mapFrameRowVector frameRowVector5086 = this;
            }

            IEnumerator<long> IEnumerable<long>.GetEnumerator()
            {
                return (IEnumerator<long>)new VectorHelpers.mapFrameRowVector(this.lo, this.hi, this.step, this.geq, (FSharpRef<long>)Operators.Ref<long>((M0)(this.lo - this.step)));
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)((IEnumerable<long>)this).GetEnumerator();
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class mapFrameRowVector<TRow> : GeneratedSequenceBase<OptionalValue<TRow>>
        {
            public long length;
            public FSharpFunc<long, long> addressAt;
            public IVector<TRow> x;
            public long i;



            public IEnumerator<long> @enum;



            public int pc;



            public OptionalValue<TRow> current;

            public mapFrameRowVector(long length, FSharpFunc<long, long> addressAt, IVector<TRow> x, long i, IEnumerator<long> @enum, int pc, OptionalValue<TRow> current)
            {
                this.length = length;
                this.addressAt = addressAt;
                this.x = x;
                this.i = i;
                this.@enum = @enum;
                this.pc = pc;
                this.current = current;
                this.ctor();
            }

            public virtual int GenerateNext(ref IEnumerable<OptionalValue<TRow>> next)
            {
                switch (this.pc)
                {
                    case 1:
                        label_5:
                        this.pc = 3;
                        LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<long>>((M0)this.@enum);
                        this.@enum = (IEnumerator<long>)null;
                        this.pc = 3;
                        goto case 3;
                    case 2:
                        this.i = 0L;
                        break;
                    case 3:
                        this.current = new OptionalValue<TRow>();
                        return 0;
                    default:
                        long lo = 0;
                        long hi = this.length - 1L;
                        this.@enum = (lo > hi ? (IEnumerable<long>)new VectorHelpers.mapFrameRowVector(lo, hi, 1L, (FSharpFunc<long, FSharpFunc<long, bool>>)new VectorHelpers.mapFrameRowVector()) : (IEnumerable<long>)new VectorHelpers.mapFrameRowVector(lo, hi, 1L, (FSharpFunc<long, FSharpFunc<long, bool>>)new VectorHelpers.mapFrameRowVector())).GetEnumerator();
                        this.pc = 1;
                        break;
                }
                if (this.@enum.MoveNext())
                {
                    this.i = this.@enum.Current;
                    this.pc = 2;
                    this.current = this.x.GetValue(this.addressAt.Invoke(this.i));
                    return 1;
                }
                goto label_5;
            }

            public virtual void Close()
            {
                Exception exception = (Exception)null;
                while (true)
                {
                    switch (this.pc)
                    {
                        case 3:
                            goto label_8;
                        default:
                            Unit unit;
                            try
                            {
                                switch (this.pc)
                                {
                                    case 0:
                                    case 3:
                                        this.pc = 3;
                                        this.current = new OptionalValue<TRow>();
                                        unit = (Unit)null;
                                        break;
                                    case 1:
                                        this.pc = 3;
                                        LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<long>>((M0)this.@enum);
                                        goto case 0;
                                    default:
                                        goto case 1;
                                }
                            }
                            catch (object ex)
                            {
                                exception = (Exception)ex;
                                unit = (Unit)null;
                            }
                            continue;
                    }
                }
                label_8:
                if (exception != null)
                    throw exception;
            }

            public virtual bool get_CheckClose()
            {
                switch (this.pc)
                {
                    case 0:
                    case 3:
                        return false;
                    case 1:
                        return true;
                    default:
                        return true;
                }
            }



            public virtual OptionalValue<TRow> get_LastGenerated()
            {
                return this.current;
            }



            public virtual IEnumerator<OptionalValue<TRow>> GetFreshEnumerator()
            {
                return (IEnumerator<OptionalValue<TRow>>)new VectorHelpers.mapFrameRowVector<TRow>(this.length, this.addressAt, this.x, 0L, (IEnumerator<long>)null, 0, new OptionalValue<TRow>());
            }
        }

        [Serializable]
        internal sealed class mapFrameRowVector : OptimizedClosures.FSharpFunc<Addressing.IAddressingScheme, Addressing.IAddressingScheme, Addressing.IAddressingScheme>
        {


            internal mapFrameRowVector()
            {
                base.ctor();
            }

            public virtual Addressing.IAddressingScheme Invoke(Addressing.IAddressingScheme a, Addressing.IAddressingScheme b)
            {
                if (!LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Addressing.IAddressingScheme>((M0)a, (M0)b))
                    throw Operators.Failure("mapFrameRowVector: Addressing scheme mismatch");
                return a;
            }
        }

        [Serializable]
        internal sealed class mapFrameRowVector : FSharpFunc<IVector, Addressing.IAddressingScheme>
        {


            internal mapFrameRowVector()
            {
                base.ctor();
            }

            public virtual Addressing.IAddressingScheme Invoke(IVector v)
            {
                return v.AddressingScheme;
            }
        }

        [Serializable]
        internal sealed class mapFrameRowVector1 : OptimizedClosures.FSharpFunc<long, long, bool>
        {


            internal mapFrameRowVector1()
            {
                base.ctor();
            }

            public virtual bool Invoke(long x, long y)
            {
                return x >= y;
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class mapFrameRowVector3 : IEnumerator, IDisposable, IEnumerator<long>
        {



            public long lo;



            public long hi;



            public long step;



            public FSharpFunc<long, FSharpFunc<long, bool>> geq;



            public FSharpRef<long> current;

            public mapFrameRowVector3(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq, FSharpRef<long> current)
            {
                this.lo = lo;
                this.hi = hi;
                this.step = step;
                this.geq = geq;
                this.current = current;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.mapFrameRowVector3 frameRowVector51713 = this;
            }

            long IEnumerator<long>.get_Current()
            {
                return this.current.get_Value();
            }

            object IEnumerator.get_Current()
            {
                return (object)this.current.get_Value();
            }

            bool IEnumerator.MoveNext()
            {
                if (FSharpFunc<long, long>.InvokeFast<bool>((FSharpFunc<long, FSharpFunc<long, M0>>)this.geq, this.current.get_Value(), this.hi) != null)
                    return false;
                this.current.set_Value(this.current.get_Value() + this.step);
                return true;
            }

            void IEnumerator.Reset()
            {
                this.current.set_Value(this.lo - this.step);
            }

            void IDisposable.Dispose()
            {
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class mapFrameRowVector2 : IEnumerable, IEnumerable<long>
        {



            public long lo;



            public long hi;



            public long step;



            public FSharpFunc<long, FSharpFunc<long, bool>> geq;

            public mapFrameRowVector2(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq)
            {
                this.lo = lo;
                this.hi = hi;
                this.step = step;
                this.geq = geq;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.mapFrameRowVector2 frameRowVector51712 = this;
            }

            IEnumerator<long> IEnumerable<long>.GetEnumerator()
            {
                return (IEnumerator<long>)new VectorHelpers.mapFrameRowVector3(this.lo, this.hi, this.step, this.geq, (FSharpRef<long>)Operators.Ref<long>((M0)(this.lo - this.step)));
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)((IEnumerable<long>)this).GetEnumerator();
            }
        }

        [Serializable]
        internal sealed class mapFrameRowVector4 : OptimizedClosures.FSharpFunc<long, long, bool>
        {


            internal mapFrameRowVector4()
            {
                base.ctor();
            }

            public virtual bool Invoke(long x, long y)
            {
                return x <= y;
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class mapFrameRowVector6 : IEnumerator, IDisposable, IEnumerator<long>
        {



            public long lo;



            public long hi;



            public long step;



            public FSharpFunc<long, FSharpFunc<long, bool>> geq;



            public FSharpRef<long> current;

            public mapFrameRowVector6(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq, FSharpRef<long> current)
            {
                this.lo = lo;
                this.hi = hi;
                this.step = step;
                this.geq = geq;
                this.current = current;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.mapFrameRowVector6 frameRowVector51716 = this;
            }

            long IEnumerator<long>.get_Current()
            {
                return this.current.get_Value();
            }

            object IEnumerator.get_Current()
            {
                return (object)this.current.get_Value();
            }

            bool IEnumerator.MoveNext()
            {
                if (FSharpFunc<long, long>.InvokeFast<bool>((FSharpFunc<long, FSharpFunc<long, M0>>)this.geq, this.current.get_Value(), this.hi) != null)
                    return false;
                this.current.set_Value(this.current.get_Value() + this.step);
                return true;
            }

            void IEnumerator.Reset()
            {
                this.current.set_Value(this.lo - this.step);
            }

            void IDisposable.Dispose()
            {
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class mapFrameRowVector5 : IEnumerable, IEnumerable<long>
        {



            public long lo;



            public long hi;



            public long step;



            public FSharpFunc<long, FSharpFunc<long, bool>> geq;

            public mapFrameRowVector5(long lo, long hi, long step, FSharpFunc<long, FSharpFunc<long, bool>> geq)
            {
                this.lo = lo;
                this.hi = hi;
                this.step = step;
                this.geq = geq;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.mapFrameRowVector5 frameRowVector51715 = this;
            }

            IEnumerator<long> IEnumerable<long>.GetEnumerator()
            {
                return (IEnumerator<long>)new VectorHelpers.mapFrameRowVector6(this.lo, this.hi, this.step, this.geq, (FSharpRef<long>)Operators.Ref<long>((M0)(this.lo - this.step)));
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return (IEnumerator)((IEnumerable<long>)this).GetEnumerator();
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class mapFrameRowVector0 : GeneratedSequenceBase<OptionalValue<object>>
        {
            public long length;
            public FSharpFunc<long, long> addressAt;
            public IVector x;
            public long i;



            public IEnumerator<long> @enum;



            public int pc;



            public OptionalValue<object> current;

            public mapFrameRowVector0(long length, FSharpFunc<long, long> addressAt, IVector x, long i, IEnumerator<long> @enum, int pc, OptionalValue<object> current)
            {
                this.length = length;
                this.addressAt = addressAt;
                this.x = x;
                this.i = i;
                this.@enum = @enum;
                this.pc = pc;
                this.current = current;
                this.ctor();
            }

            public virtual int GenerateNext(ref IEnumerable<OptionalValue<object>> next)
            {
                switch (this.pc)
                {
                    case 1:
                        label_5:
                        this.pc = 3;
                        LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<long>>((M0)this.@enum);
                        this.@enum = (IEnumerator<long>)null;
                        this.pc = 3;
                        goto case 3;
                    case 2:
                        this.i = 0L;
                        break;
                    case 3:
                        this.current = new OptionalValue<object>();
                        return 0;
                    default:
                        long lo = 0;
                        long hi = this.length - 1L;
                        this.@enum = (lo > hi ? (IEnumerable<long>)new VectorHelpers.mapFrameRowVector5(lo, hi, 1L, (FSharpFunc<long, FSharpFunc<long, bool>>)new VectorHelpers.mapFrameRowVector4()) : (IEnumerable<long>)new VectorHelpers.mapFrameRowVector2(lo, hi, 1L, (FSharpFunc<long, FSharpFunc<long, bool>>)new VectorHelpers.mapFrameRowVector1())).GetEnumerator();
                        this.pc = 1;
                        break;
                }
                if (this.@enum.MoveNext())
                {
                    this.i = this.@enum.Current;
                    this.pc = 2;
                    this.current = this.x.GetObject(this.addressAt.Invoke(this.i));
                    return 1;
                }
                goto label_5;
            }

            public virtual void Close()
            {
                Exception exception = (Exception)null;
                while (true)
                {
                    switch (this.pc)
                    {
                        case 3:
                            goto label_8;
                        default:
                            Unit unit;
                            try
                            {
                                switch (this.pc)
                                {
                                    case 0:
                                    case 3:
                                        this.pc = 3;
                                        this.current = new OptionalValue<object>();
                                        unit = (Unit)null;
                                        break;
                                    case 1:
                                        this.pc = 3;
                                        LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<long>>((M0)this.@enum);
                                        goto case 0;
                                    default:
                                        goto case 1;
                                }
                            }
                            catch (object ex)
                            {
                                exception = (Exception)ex;
                                unit = (Unit)null;
                            }
                            continue;
                    }
                }
                label_8:
                if (exception != null)
                    throw exception;
            }

            public virtual bool get_CheckClose()
            {
                switch (this.pc)
                {
                    case 0:
                    case 3:
                        return false;
                    case 1:
                        return true;
                    default:
                        return true;
                }
            }



            public virtual OptionalValue<object> get_LastGenerated()
            {
                return this.current;
            }



            public virtual IEnumerator<OptionalValue<object>> GetFreshEnumerator()
            {
                return (IEnumerator<OptionalValue<object>>)new VectorHelpers.mapFrameRowVector0(this.length, this.addressAt, this.x, 0L, (IEnumerator<long>)null, 0, new OptionalValue<object>());
            }
        }

        [Serializable]
        internal sealed class mapFrameRowVector7<TRow> : FSharpFunc<TRow, object>
        {


            internal mapFrameRowVector7()
            {
                base.ctor();
            }

            public virtual object Invoke(TRow x)
            {
                return (object)x;
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class mapFrameRowVector<TRow> : IVector, IVector<TRow>
        {
            public Func<IVector[], long, TRow> ctor;
            public long length;
            public FSharpFunc<long, long> addressAt;
            public IVector[] data;

            public mapFrameRowVector(Func<IVector[], long, TRow> ctor, long length, FSharpFunc<long, long> addressAt, IVector[] data)
            {
                this.ctor = ctor;
                this.length = length;
                this.addressAt = addressAt;
                this.data = data;
                // ISSUE: explicit constructor call
                base.ctor();
                VectorHelpers.mapFrameRowVector<TRow> frameRowVector504 = this;
            }

            OptionalValue<TRow> IVector<TRow>.GetValue(long a)
            {
                return new OptionalValue<TRow>(this.ctor(this.data, a));
            }

            OptionalValue<TRow> IVector<TRow>.GetValueAtLocation(IVectorLocation l)
            {
                return new OptionalValue<TRow>(this.ctor(this.data, l.Address));
            }

            VectorData<TRow> IVector<TRow>.get_Data()
            {
                return VectorData<TRow>.NewSequence((IEnumerable<OptionalValue<TRow>>)new VectorHelpers.mapFrameRowVector<TRow>(this.length, this.addressAt, (IVector<TRow>)this, 0L, (IEnumerator<long>)null, 0, new OptionalValue<TRow>()));
            }

            IVector<a> IVector<TRow>.Select<a>(FSharpFunc<IVectorLocation, FSharpFunc<OptionalValue<TRow>, OptionalValue<a>>> g)
            {
                throw Operators.Failure("mapFrameRowVector: Select not supported");
            }

            IVector<a> IVector<TRow>.Convert<a>(FSharpFunc<TRow, a> h, FSharpFunc<a, TRow> g)
            {
                throw Operators.Failure("mapFrameRowVector: Convert not supported");
            }

            Addressing.IAddressingScheme IVector.get_AddressingScheme()
            {
                return (Addressing.IAddressingScheme)SeqModule.Reduce<Addressing.IAddressingScheme>((FSharpFunc<M0, FSharpFunc<M0, M0>>)new VectorHelpers.mapFrameRowVector(), (IEnumerable<M0>)SeqModule.Map<IVector, Addressing.IAddressingScheme>((FSharpFunc<M0, M1>)new VectorHelpers.mapFrameRowVector(), (IEnumerable<M0>)this.data));
            }

            long IVector.get_Length()
            {
                return this.length;
            }

            IEnumerable<OptionalValue<object>> IVector.get_ObjectSequence()
            {
                return (IEnumerable<OptionalValue<object>>)new VectorHelpers.mapFrameRowVector0(this.length, this.addressAt, (IVector)this, 0L, (IEnumerator<long>)null, 0, new OptionalValue<object>());
            }

            bool IVector.get_SuppressPrinting()
            {
                return false;
            }

            Type IVector.get_ElementType()
            {
                return typeof(TRow);
            }

            OptionalValue<object> IVector.GetObject(long i)
            {
                OptionalValue<TRow> optionalValue1 = ((IVector<TRow>)LanguagePrimitives.IntrinsicFunctions.UnboxGeneric<IVector<TRow>>((object)this)).GetValue(i);
                FSharpFunc<TRow, object> fsharpFunc = (FSharpFunc<TRow, object>)new VectorHelpers.mapFrameRowVector7<TRow>();
                OptionalValue<TRow> optionalValue2 = optionalValue1;
                if (optionalValue2.HasValue)
                    return new OptionalValue<object>(fsharpFunc.Invoke(optionalValue2.Value));
                return OptionalValue<object>.Missing;
            }

            a IVector.Invoke<a>(VectorCallSite<a> site)
            {
                throw Operators.Failure("mapFrameRowVector: Invoke not supported");
            }
        }

        [Serializable]
        internal sealed class typedRowModule : FSharpFunc<Unit, ModuleBuilder>
        {


            internal typedRowModule()
            {
                base.ctor();
            }

            public virtual ModuleBuilder Invoke(Unit _arg1)
            {
                AssemblyName name = new AssemblyName("TypedRowAssembly");
                return AssemblyBuilder.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndCollect).DefineDynamicModule(name.Name);
            }
        }

        [Serializable]
        internal sealed class typeName : FSharpFunc<int, string>
        {



            public FSharpFunc<int, string> clo2;



            internal typeName(FSharpFunc<int, string> clo2)
            {
                this.ctor();
                this.clo2 = clo2;
            }

            public virtual string Invoke(int arg20)
            {
                return this.clo2.Invoke(arg20);
            }
        }

        [Serializable]
        internal sealed class typeName : FSharpFunc<string, FSharpFunc<int, string>>
        {



            public FSharpFunc<string, FSharpFunc<int, string>> clo1;



            internal typeName(FSharpFunc<string, FSharpFunc<int, string>> clo1)
            {
                this.ctor();
                this.clo1 = clo1;
            }

            public virtual FSharpFunc<int, string> Invoke(string arg10)
            {
                return (FSharpFunc<int, string>)new VectorHelpers.typeName(this.clo1.Invoke(arg10));
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class columnTypes : GeneratedSequenceBase<Tuple<bool, Type>>
        {
            public Type rowType;
            public MethodInfo m;



            public IEnumerator<MethodInfo> @enum;



            public int pc;



            public Tuple<bool, Type> current;

            public columnTypes(Type rowType, MethodInfo m, IEnumerator<MethodInfo> @enum, int pc, Tuple<bool, Type> current)
            {
                this.rowType = rowType;
                this.m = m;
                this.@enum = @enum;
                this.pc = pc;
                this.current = current;
                this.ctor();
            }

            public virtual int GenerateNext(ref IEnumerable<Tuple<bool, Type>> next)
            {
                switch (this.pc)
                {
                    case 1:
                        label_8:
                        this.pc = 3;
                        LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<MethodInfo>>((M0)this.@enum);
                        this.@enum = (IEnumerator<MethodInfo>)null;
                        this.pc = 3;
                        goto case 3;
                    case 2:
                        this.m = (MethodInfo)null;
                        break;
                    case 3:
                        this.current = (Tuple<bool, Type>)null;
                        return 0;
                    default:
                        this.@enum = ((IEnumerable<MethodInfo>)this.rowType.GetMethods()).GetEnumerator();
                        this.pc = 1;
                        break;
                }
                if (this.@enum.MoveNext())
                {
                    this.m = this.@enum.Current;
                    this.pc = 2;
                    Tuple<bool, Type> tuple;
                    if ((!this.m.ReturnType.IsGenericType ? 0 : (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)this.m.ReturnType.GetGenericTypeDefinition(), (M0)VectorHelpers.Reflection.optTyp) ? 1 : 0)) != 0)
                    {
                        int num = 1;
                        Type type1 = typeof(IVector<object>);
                        Type type2 = (!type1.GetTypeInfo().IsGenericType ? type1 : type1.GetGenericTypeDefinition()).MakeGenericType(this.m.ReturnType.GetGenericArguments()[0]);
                        tuple = new Tuple<bool, Type>(num != 0, type2);
                    }
                    else
                    {
                        int num = 0;
                        Type type1 = typeof(IVector<object>);
                        Type type2 = (!type1.GetTypeInfo().IsGenericType ? type1 : type1.GetGenericTypeDefinition()).MakeGenericType(this.m.ReturnType);
                        tuple = new Tuple<bool, Type>(num != 0, type2);
                    }
                    this.current = tuple;
                    return 1;
                }
                goto label_8;
            }

            public virtual void Close()
            {
                Exception exception = (Exception)null;
                while (true)
                {
                    switch (this.pc)
                    {
                        case 3:
                            goto label_8;
                        default:
                            Unit unit;
                            try
                            {
                                switch (this.pc)
                                {
                                    case 0:
                                    case 3:
                                        this.pc = 3;
                                        this.current = (Tuple<bool, Type>)null;
                                        unit = (Unit)null;
                                        break;
                                    case 1:
                                        this.pc = 3;
                                        LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<MethodInfo>>((M0)this.@enum);
                                        goto case 0;
                                    default:
                                        goto case 1;
                                }
                            }
                            catch (object ex)
                            {
                                exception = (Exception)ex;
                                unit = (Unit)null;
                            }
                            continue;
                    }
                }
                label_8:
                if (exception != null)
                    throw exception;
            }

            public virtual bool get_CheckClose()
            {
                switch (this.pc)
                {
                    case 0:
                    case 3:
                        return false;
                    case 1:
                        return true;
                    default:
                        return true;
                }
            }



            public virtual Tuple<bool, Type> get_LastGenerated()
            {
                return this.current;
            }



            public virtual IEnumerator<Tuple<bool, Type>> GetFreshEnumerator()
            {
                return (IEnumerator<Tuple<bool, Type>>)new VectorHelpers.columnTypes(this.rowType, (MethodInfo)null, (IEnumerator<MethodInfo>)null, 0, (Tuple<bool, Type>)null);
            }
        }

        [Serializable]
        internal sealed class vecFields : OptimizedClosures.FSharpFunc<int, Tuple<bool, Type>, FieldBuilder>
        {
            public TypeBuilder rowImpl;



            internal vecFields(TypeBuilder rowImpl)
            {
                this.ctor();
                this.rowImpl = rowImpl;
            }

            public virtual FieldBuilder Invoke(int i, Tuple<bool, Type> tupledArg)
            {
                bool flag = tupledArg.Item1;
                Type type = tupledArg.Item2;
                return this.rowImpl.DefineField(((FSharpFunc<int, string>)ExtraTopLevelOperators.PrintFormatToString<FSharpFunc<int, string>>((PrintfFormat<M0, Unit, string, string>)new PrintfFormat<FSharpFunc<int, string>, Unit, string, string, int>("vector_%d"))).Invoke(i), type, FieldAttributes.Private);
            }
        }

        [Serializable]
        internal sealed class ctor : FSharpFunc<Tuple<bool, Type>, Type>
        {


            internal ctor()
            {
                base.ctor();
            }

            public virtual Type Invoke(Tuple<bool, Type> tupledArg)
            {
                return tupledArg.Item2;
            }
        }

        [Serializable]
        internal sealed class createTypedRowCreator : OptimizedClosures.FSharpFunc<int, FieldBuilder, Unit>
        {
            public ILGenerator ilgen;



            internal createTypedRowCreator(ILGenerator ilgen)
            {
                this.ctor();
                this.ilgen = ilgen;
            }

            public virtual Unit Invoke(int i, FieldBuilder vecField)
            {
                this.ilgen.Emit(OpCodes.Ldarg_0);
                this.ilgen.Emit(OpCodes.Ldarg, 2 + i);
                this.ilgen.Emit(OpCodes.Stfld, (FieldInfo)vecField);
                return (Unit)null;
            }
        }

        [Serializable]
        internal sealed class locals : OptimizedClosures.FSharpFunc<int, Tuple<bool, Type>, Tuple<LocalBuilder, Type>>
        {
            public ILGenerator ilgen;



            internal locals(ILGenerator ilgen)
            {
                this.ctor();
                this.ilgen = ilgen;
            }

            public virtual Tuple<LocalBuilder, Type> Invoke(int i, Tuple<bool, Type> tupledArg)
            {
                bool flag = tupledArg.Item1;
                Type type = tupledArg.Item2;
                LocalBuilder local = this.ilgen.DeclareLocal(typeof(IVector));
                this.ilgen.Emit(OpCodes.Ldarg_0);
                this.ilgen.Emit(OpCodes.Ldc_I4, i);
                this.ilgen.Emit(OpCodes.Ldelem, typeof(IVector));
                this.ilgen.Emit(OpCodes.Stloc, local);
                return new Tuple<LocalBuilder, Type>(local, type);
            }
        }

        [Serializable]
        internal sealed class meta : FSharpFunc<Tuple<MethodInfo, Tuple<bool, Type>>, Tuple<string, Type>>
        {


            internal meta()
            {
                base.ctor();
            }

            public virtual Tuple<string, Type> Invoke(Tuple<MethodInfo, Tuple<bool, Type>> tupledArg)
            {
                return new Tuple<string, Type>(tupledArg.Item1.Name.Substring(4), tupledArg.Item2.Item2.GetGenericArguments()[0]);
            }
        }


        [Serializable]

        [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
        internal sealed class subData : GeneratedSequenceBase<IVector>
        {
            public FSharpFunc<string, long> columnIndex;
            public IVector<IVector> data;
            public FSharpList<Tuple<string, Type>> meta;
            public Type typ;
            public string name;
            public Tuple<string, Type> matchValue;
            public IEnumerator<Tuple<string, Type>> @enum;
            public int pc;
            public IVector current;

            public subData(FSharpFunc<string, long> columnIndex, IVector<IVector> data, FSharpList<Tuple<string, Type>> meta, Type typ, string name, Tuple<string, Type> matchValue, IEnumerator<Tuple<string, Type>> @enum, int pc, IVector current)
            {
                this.columnIndex = columnIndex;
                this.data = data;
                this.meta = meta;
                this.typ = typ;
                this.name = name;
                this.matchValue = matchValue;
                this.@enum = @enum;
                this.pc = pc;
                this.current = current;
                this.ctor();
            }

            public virtual int GenerateNext(ref IEnumerable<IVector> next)
            {
                switch (this.pc)
                {
                    case 1:
                        label_5:
                        this.pc = 3;
                        LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<string, Type>>>((M0)this.@enum);
                        this.@enum = (IEnumerator<Tuple<string, Type>>)null;
                        this.pc = 3;
                        goto case 3;
                    case 2:
                        this.name = (string)null;
                        this.typ = (Type)null;
                        this.matchValue = (Tuple<string, Type>)null;
                        break;
                    case 3:
                        this.current = (IVector)null;
                        return 0;
                    default:
                        this.@enum = ((IEnumerable<Tuple<string, Type>>)this.meta).GetEnumerator();
                        this.pc = 1;
                        break;
                }
                if (this.@enum.MoveNext())
                {
                    this.matchValue = this.@enum.Current;
                    this.typ = this.matchValue.Item2;
                    this.name = this.matchValue.Item1;
                    this.pc = 2;
                    OptionalValue<IVector> optionalValue = this.data.GetValue(this.columnIndex.Invoke(this.name));
                    this.current = !LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)optionalValue.Value.ElementType, (M0)this.typ) ? VectorHelpers.convertTypeDynamic<ConversionKind>(this.typ, ConversionKind.Flexible, optionalValue.Value) : optionalValue.Value;
                    return 1;
                }
                goto label_5;
            }

            public virtual void Close()
            {
                Exception exception = (Exception)null;
                while (true)
                {
                    switch (this.pc)
                    {
                        case 3:
                            goto label_8;
                        default:
                            Unit unit;
                            try
                            {
                                switch (this.pc)
                                {
                                    case 0:
                                    case 3:
                                        this.pc = 3;
                                        this.current = (IVector)null;
                                        unit = (Unit)null;
                                        break;
                                    case 1:
                                        this.pc = 3;
                                        LanguagePrimitives.IntrinsicFunctions.Dispose<IEnumerator<Tuple<string, Type>>>((M0)this.@enum);
                                        goto case 0;
                                    default:
                                        goto case 1;
                                }
                            }
                            catch (object ex)
                            {
                                exception = (Exception)ex;
                                unit = (Unit)null;
                            }
                            continue;
                    }
                }
                label_8:
                if (exception != null)
                    throw exception;
            }

            public virtual bool get_CheckClose()
            {
                switch (this.pc)
                {
                    case 0:
                    case 3:
                        return false;
                    case 1:
                        return true;
                    default:
                        return true;
                }
            }



            public virtual IVector get_LastGenerated()
            {
                return this.current;
            }



            public virtual IEnumerator<IVector> GetFreshEnumerator()
            {
                return (IEnumerator<IVector>)new VectorHelpers.subData(this.columnIndex, this.data, this.meta, (Type)null, (string)null, (Tuple<string, Type>)null, (IEnumerator<Tuple<string, Type>>)null, 0, (IVector)null);
            }
        }

        [Serializable]
        internal sealed class substitute : FSharpFunc<VectorConstruction, VectorConstruction>
        {
            public Tuple<int, VectorConstruction> subst;
            public int oldVar;
            public VectorConstruction newVect;



            internal substitute(Tuple<int, VectorConstruction> subst, int oldVar, VectorConstruction newVect)
            {
                this.ctor();
                this.subst = subst;
                this.oldVar = oldVar;
                this.newVect = newVect;
            }

            public virtual VectorConstruction Invoke(VectorConstruction _arg2)
            {
                VectorConstruction vectorConstruction1 = _arg2;
                if (vectorConstruction1.get_Tag() == 0)
                {
                    VectorConstruction.Return @return = (VectorConstruction.Return)vectorConstruction1;
                    if (@return.item == this.oldVar)
                    {
                        int num = @return.item;
                        return this.newVect;
                    }
                }
                switch (vectorConstruction1.get_Tag())
                {
                    case 1:
                        return VectorConstruction.NewEmpty(((VectorConstruction.Empty)vectorConstruction1).item);
                    case 2:
                        VectorConstruction.Relocate relocate = (VectorConstruction.Relocate)vectorConstruction1;
                        return VectorConstruction.NewRelocate(VectorHelpers.substitute(this.subst.Item1, this.subst.Item2).Invoke(relocate.item1), relocate.item2, relocate.item3);
                    case 3:
                        VectorConstruction.DropRange dropRange = (VectorConstruction.DropRange)vectorConstruction1;
                        return VectorConstruction.NewDropRange(VectorHelpers.substitute(this.subst.Item1, this.subst.Item2).Invoke(dropRange.item1), dropRange.item2);
                    case 4:
                        VectorConstruction.GetRange getRange = (VectorConstruction.GetRange)vectorConstruction1;
                        return VectorConstruction.NewGetRange(VectorHelpers.substitute(this.subst.Item1, this.subst.Item2).Invoke(getRange.item1), getRange.item2);
                    case 5:
                        VectorConstruction.Append append = (VectorConstruction.Append)vectorConstruction1;
                        VectorConstruction vectorConstruction2 = append.item2;
                        return VectorConstruction.NewAppend(VectorHelpers.substitute(this.subst.Item1, this.subst.Item2).Invoke(append.item1), VectorHelpers.substitute(this.subst.Item1, this.subst.Item2).Invoke(vectorConstruction2));
                    case 6:
                        VectorConstruction.Combine combine = (VectorConstruction.Combine)vectorConstruction1;
                        FSharpList<VectorConstruction> fsharpList = combine.item2;
                        Lazy<long> lazy = combine.item1;
                        VectorListTransform vectorListTransform = combine.item3;
                        return VectorConstruction.NewCombine(lazy, (FSharpList<VectorConstruction>)ListModule.Map<VectorConstruction, VectorConstruction>((FSharpFunc<M0, M1>)VectorHelpers.substitute(this.subst.Item1, this.subst.Item2), (FSharpList<M0>)fsharpList), vectorListTransform);
                    case 7:
                        VectorConstruction.FillMissing fillMissing = (VectorConstruction.FillMissing)vectorConstruction1;
                        return VectorConstruction.NewFillMissing(VectorHelpers.substitute(this.subst.Item1, this.subst.Item2).Invoke(fillMissing.item1), fillMissing.item2);
                    case 8:
                        VectorConstruction.CustomCommand customCommand = (VectorConstruction.CustomCommand)vectorConstruction1;
                        return VectorConstruction.NewCustomCommand((FSharpList<VectorConstruction>)ListModule.Map<VectorConstruction, VectorConstruction>((FSharpFunc<M0, M1>)VectorHelpers.substitute(this.subst.Item1, this.subst.Item2), (FSharpList<M0>)customCommand.item1), customCommand.item2);
                    case 9:
                        VectorConstruction.AsyncCustomCommand asyncCustomCommand = (VectorConstruction.AsyncCustomCommand)vectorConstruction1;
                        return VectorConstruction.NewAsyncCustomCommand((FSharpList<VectorConstruction>)ListModule.Map<VectorConstruction, VectorConstruction>((FSharpFunc<M0, M1>)VectorHelpers.substitute(this.subst.Item1, this.subst.Item2), (FSharpList<M0>)asyncCustomCommand.item1), asyncCustomCommand.item2);
                    default:
                        return VectorConstruction.NewReturn(((VectorConstruction.Return)vectorConstruction1).item);
                }
            }
        }

        [Serializable]
        internal sealed class CombinedRelocations_ : FSharpFunc<VectorConstruction, bool>
        {


            internal CombinedRelocations_()
            {
                base.ctor();
            }

            public virtual bool Invoke(VectorConstruction _arg2)
            {
                if (_arg2.get_Tag() != 2)
                    return false;
                return true;
            }
        }

        [Serializable]
        internal sealed class parts : FSharpFunc<VectorConstruction, Tuple<VectorConstruction, long, IEnumerable<Tuple<long, long>>>>
        {


            internal parts()
            {
                base.ctor();
            }

            public virtual Tuple<VectorConstruction, long, IEnumerable<Tuple<long, long>>> Invoke(VectorConstruction _arg3)
            {
                VectorConstruction vectorConstruction = _arg3;
                if (vectorConstruction.get_Tag() != 2)
                    throw Operators.Failure("logic error");
                VectorConstruction.Relocate relocate = (VectorConstruction.Relocate)vectorConstruction;
                IEnumerable<Tuple<long, long>> tuples = relocate.item3;
                long num = relocate.item2;
                return new Tuple<VectorConstruction, long, IEnumerable<Tuple<long, long>>>(relocate.item1, num, tuples);
            }
        }


        internal static class Reflection
        {

            internal static ConstructorInfo objCtor
            {
                get
                {
                    return VectorHelpers.objCtor;
                }
            }

            internal static Type addrTyp
            {
                get
                {
                    return typeof(long);
                }
            }


            internal static Type optTyp
            {
                get
                {
                    return VectorHelpers.optTyp;
                }
            }
        }


        internal static class Inference
        {

            internal static FSharpOption<Unit> isType<a>(FSharpList<a> types, a t)
            {
                if (ListModule.Exists<a>((FSharpFunc<M0, bool>)new VectorHelpers.Inference.isType<a>(t), types))
                    return FSharpOption<Unit>.Some((Unit)null);
                return (FSharpOption<Unit>)null;
            }


            internal static FSharpList<Type> intTypes
            {
                get
                {
                    return VectorHelpers.intTypes;
                }
            }


            internal static FSharpList<Type> int64Types
            {
                get
                {
                    return VectorHelpers.int64Types;
                }
            }


            internal static FSharpList<Type> floatTypes
            {
                get
                {
                    return VectorHelpers.floatTypes;
                }
            }


            internal static FSharpList<Type> stringTypes
            {
                get
                {
                    return VectorHelpers.stringTypes;
                }
            }


            internal static FSharpOption<Unit> Top_(Type ty)
            {
                if (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)ty, (M0)null))
                    return FSharpOption<Unit>.Some((Unit)null);
                return (FSharpOption<Unit>)null;
            }


            internal static FSharpOption<Unit> Bottom_(Type ty)
            {
                if (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)ty, (M0)typeof(object)))
                    return FSharpOption<Unit>.Some((Unit)null);
                return (FSharpOption<Unit>)null;
            }


            internal static FSharpOption<Unit> AsInt_(Type ty)
            {
                FSharpList<Type> intTypes = VectorHelpers.Inference.intTypes;
                if (ListModule.Exists<Type>((FSharpFunc<M0, bool>)new VectorHelpers.Inference.AsInt_(ty), (FSharpList<M0>)intTypes))
                    return FSharpOption<Unit>.Some((Unit)null);
                return (FSharpOption<Unit>)null;
            }


            internal static FSharpOption<Unit> AsInt64_(Type ty)
            {
                FSharpList<Type> int64Types = VectorHelpers.Inference.int64Types;
                if (ListModule.Exists<Type>((FSharpFunc<M0, bool>)new VectorHelpers.Inference.AsInt64_(ty), (FSharpList<M0>)int64Types))
                    return FSharpOption<Unit>.Some((Unit)null);
                return (FSharpOption<Unit>)null;
            }


            internal static FSharpOption<Unit> AsFloat_(Type ty)
            {
                FSharpList<Type> floatTypes = VectorHelpers.Inference.floatTypes;
                if (ListModule.Exists<Type>((FSharpFunc<M0, bool>)new VectorHelpers.Inference.AsFloat_(ty), (FSharpList<M0>)floatTypes))
                    return FSharpOption<Unit>.Some((Unit)null);
                return (FSharpOption<Unit>)null;
            }


            internal static FSharpOption<Unit> AsString_(Type ty)
            {
                FSharpList<Type> stringTypes = VectorHelpers.Inference.stringTypes;
                if (ListModule.Exists<Type>((FSharpFunc<M0, bool>)new VectorHelpers.Inference.AsString_(ty), (FSharpList<M0>)stringTypes))
                    return FSharpOption<Unit>.Some((Unit)null);
                return (FSharpOption<Unit>)null;
            }

            internal static Type Bottom
            {
                get
                {
                    return typeof(object);
                }
            }

            internal static Type Top
            {
                get
                {
                    return (Type)null;
                }
            }


            internal static Type commonSupertype(Type t1, Type t2)
            {
                Tuple<Type, Type> tuple = new Tuple<Type, Type>(t1, t2);
                Type type;
                if (VectorHelpers.Inference.Top_(tuple.Item1) == null)
                {
                    if (VectorHelpers.Inference.Top_(tuple.Item2) != null)
                    {
                        type = tuple.Item1;
                    }
                    else
                    {
                        if (LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)t1, (M0)t2))
                            return t1;
                        if (VectorHelpers.Inference.Bottom_(tuple.Item1) != null || VectorHelpers.Inference.Bottom_(tuple.Item2) != null)
                            return VectorHelpers.Inference.Bottom;
                        if (t1.IsAssignableFrom(t2))
                            return t1;
                        if (t2.IsAssignableFrom(t1))
                            return t2;
                        if (VectorHelpers.Inference.AsString_(tuple.Item1) != null && VectorHelpers.Inference.AsString_(tuple.Item2) != null)
                            return typeof(string);
                        if (VectorHelpers.Inference.AsInt_(tuple.Item1) != null && VectorHelpers.Inference.AsInt_(tuple.Item2) != null)
                            return typeof(int);
                        if (VectorHelpers.Inference.AsInt64_(tuple.Item1) != null && VectorHelpers.Inference.AsInt64_(tuple.Item2) != null)
                            return typeof(long);
                        if (VectorHelpers.Inference.AsFloat_(tuple.Item1) != null && VectorHelpers.Inference.AsFloat_(tuple.Item2) != null)
                            return typeof(double);
                        return VectorHelpers.Inference.Bottom;
                    }
                }
                else
                    type = tuple.Item2;
                return type;
            }

            [Serializable]
            internal sealed class isType<a> : FSharpFunc<a, bool>
            {



                public a x;



                internal isType(a x)
                {
                    this.ctor();
                    this.x = x;
                }

                public virtual bool Invoke(a y)
                {
                    return LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<a>((M0)this.x, (M0)y);
                }
            }

            [Serializable]
            internal sealed class AsInt_ : FSharpFunc<Type, bool>
            {



                public Type x;



                internal AsInt_(Type x)
                {
                    this.ctor();
                    this.x = x;
                }

                public virtual bool Invoke(Type y)
                {
                    return LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)this.x, (M0)y);
                }
            }

            [Serializable]
            internal sealed class AsInt64_ : FSharpFunc<Type, bool>
            {



                public Type x;



                internal AsInt64_(Type x)
                {
                    this.ctor();
                    this.x = x;
                }

                public virtual bool Invoke(Type y)
                {
                    return LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)this.x, (M0)y);
                }
            }

            [Serializable]
            internal sealed class AsFloat_ : FSharpFunc<Type, bool>
            {



                public Type x;



                internal AsFloat_(Type x)
                {
                    this.ctor();
                    this.x = x;
                }

                public virtual bool Invoke(Type y)
                {
                    return LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)this.x, (M0)y);
                }
            }

            [Serializable]
            internal sealed class AsString_ : FSharpFunc<Type, bool>
            {



                public Type x;



                internal AsString_(Type x)
                {
                    this.ctor();
                    this.x = x;
                }

                public virtual bool Invoke(Type y)
                {
                    return LanguagePrimitives.HashCompare.GenericEqualityIntrinsic<Type>((M0)this.x, (M0)y);
                }
            }
        }


        internal static class RangeRestriction
        {
            internal static RangeRestriction<long> ofSeq(long count, IEnumerable<long> indices)
            {
                return Deedle.RangeRestriction<long>.NewCustom((IRangeRestriction<long>)new VectorHelpers.RangeRestriction.ofSeq(count, indices));
            }

            [Serializable]

            [StructLayout(LayoutKind.Auto, CharSet = CharSet.Auto)]
            internal sealed class ofSeq : IEnumerable<long>, IEnumerable, IRangeRestriction<long>
            {
                public long count;
                public IEnumerable<long> indices;

                public ofSeq(long count, IEnumerable<long> indices)
                {
                    this.count = count;
                    this.indices = indices;
                    // ISSUE: explicit constructor call
                    VectorHelpers.RangeRestriction.ofSeq ofSeq40 = this;
                }

                long IRangeRestriction<long>.Count => this.count;

                IEnumerator<long> IEnumerable<long>.GetEnumerator()
                {
                    return this.indices.GetEnumerator();
                }

                IEnumerator IEnumerable.GetEnumerator()
                {
                    return (IEnumerator)this.indices.GetEnumerator();
                }
            }
        }


    }
}