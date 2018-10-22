// Decompiled with JetBrains decompiler
// Type: Deedle.Addressing
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


namespace Deedle
{
    public static partial class Addressing
    {
        /// <summary>
        /// An empty interface that is used as an marker for "addressing schemes". As discussed
        /// above, Deedle can use different addressing schemes. We need to make sure that the index
        /// and vector share the scheme - this is done by attaching `IAddressingScheme` to each
        /// index or vector and checking that they match. Implementations must support equality!
        /// </summary>
        public interface IAddressingScheme
        {

        }
    }
}
