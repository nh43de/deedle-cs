// Decompiled with JetBrains decompiler
// Type: <StartupCode$Deedle>.$Deedle.VectorHelpers
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using Deedle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace StartupCodeDeedle.Deedle
{
    internal static class VectorHelpers
    {

        internal static readonly IntPtr doubleCode;

        internal static readonly IntPtr intCode;

        internal static readonly IntPtr stringCode;

        internal static readonly Lazy<MethodInfo> convertTypeMethod;

        internal static readonly IEnumerable<Type> intTypes;

        internal static readonly IEnumerable<Type> int64Types;

        internal static readonly IEnumerable<Type> floatTypes;

        internal static readonly IEnumerable<Type> stringTypes;

        internal static readonly Lazy<ModuleBuilder> typedRowModule;

        internal static readonly ConstructorInfo objCtor;

        internal static readonly Type optTyp;

        internal static readonly FSharpRef<int> typeCounter;

        internal static readonly Dictionary<Tuple<Type, FSharpList<string>>, Tuple<object, FSharpList<Tuple<string, Type>>>> createdTypedRowsCache;

        internal static int init;

        static VectorHelpers()
        {
            /// Type code of the type for efficient type equality test
            RuntimeTypeHandle typeHandle1 = typeof(double).TypeHandle;
            IntPtr num1 = StartupCodeDeedle.Deedle.VectorHelpers.doubleCode = typeHandle1.Value;

            RuntimeTypeHandle typeHandle2 = typeof(int).TypeHandle;
            IntPtr num2 = StartupCodeDeedle.Deedle.VectorHelpers.intCode = typeHandle2.Value;

            RuntimeTypeHandle typeHandle3 = typeof(string).TypeHandle;
            IntPtr num3 = StartupCodeDeedle.Deedle.VectorHelpers.stringCode = typeHandle3.Value;

            //TODO:
            Lazy<MethodInfo> lazy1 = StartupCodeDeedle.Deedle.VectorHelpers.convertTypeMethod = 
                Lazy<_>.Create((FSharpFunc<Unit, M0>)new Deedle.VectorHelpers.convertTypeMethod());

            intTypes = new[] {typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(int)};

            int64Types = intTypes.Union(new[]
            {
                typeof(uint),
                typeof(long)
            }).ToArray();

            floatTypes = int64Types.Union(new[]
            {
                typeof(Decimal),
                typeof(ulong),
                typeof(float),
                typeof(double)
            }).ToArray();

            stringTypes = new[]
            {
                typeof(char),
                typeof(string)
            };

            Type bottom = Deedle.VectorHelpers.Inference.Bottom;

            Type top = Deedle.VectorHelpers.Inference.Top;

            Lazy<ModuleBuilder> lazy2 = StartupCodeDeedle.Deedle.VectorHelpers.typedRowModule = (Lazy<ModuleBuilder>)LazyExtensions.Create<ModuleBuilder>((FSharpFunc<Unit, M0>)new Deedle.VectorHelpers.typedRowModule());

            ConstructorInfo constructorInfo = StartupCodeDeedle.Deedle.VectorHelpers.objCtor = typeof(object).GetConstructor(new Type[0]);

            Type addrTyp = Deedle.VectorHelpers.Reflection.addrTyp;

            Type type1 = typeof(OptionalValue<object>);

            Type type2 = StartupCodeDeedle.Deedle.VectorHelpers.optTyp = !type1.GetTypeInfo().IsGenericType ? type1 : type1.GetGenericTypeDefinition();

            FSharpRef<int> fsharpRef = StartupCodeDeedle.Deedle.VectorHelpers.typeCounter = (FSharpRef<int>)Operators.Ref<int>((M0)0);

            Dictionary<Tuple<Type, FSharpList<string>>, Tuple<object, FSharpList<Tuple<string, Type>>>> dictionary = StartupCodeDeedle.Deedle.VectorHelpers.createdTypedRowsCache = new Dictionary<Tuple<Type, FSharpList<string>>, Tuple<object, FSharpList<Tuple<string, Type>>>>();

        }
    }
}
