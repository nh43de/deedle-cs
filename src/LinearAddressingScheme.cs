// Decompiled with JetBrains decompiler
// Type: Deedle.Addressing
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll

using System;

namespace Deedle
{
    public static partial class Addressing
    {
        [Serializable]
        public class LinearAddressingScheme : Addressing.IAddressingScheme
        {
            internal static Addressing.IAddressingScheme instance;
            internal static int init;

            internal LinearAddressingScheme()
            {
                Addressing.LinearAddressingScheme addressingScheme = this;
            }

            public static Addressing.IAddressingScheme Instance
            {
                get
                {
                    if (Addressing.LinearAddressingScheme.init < 1)
                        LanguagePrimitives.IntrinsicFunctions.FailStaticInit();
                    return Addressing.LinearAddressingScheme.instance;
                }
            }

            static LinearAddressingScheme()
            {
                Address.init = 0;
                int init = Address.init;
            }
        }
    }
}
