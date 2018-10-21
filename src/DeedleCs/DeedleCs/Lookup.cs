// Decompiled with JetBrains decompiler
// Type: Deedle.Lookup
// Assembly: Deedle, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5BCB5510-44DC-8EDB-A745-03831055CB5B
// Assembly location: C:\code\Github\Deedle\bin\netstandard2.0\Deedle.dll


using System;

namespace Deedle
{
    [Flags]
    [Serializable]
    public enum Lookup
    {
        ///<summary> 
        /// Lookup a value associated with the exact specified key. 
        /// If the key is not available, then fail or return missing value. 
        ///</summary> 
        Exact = 1,
        ///<summary> 
        /// Lookup a value associated with the specified key or with the nearest
        /// greater key that has a value available. Fails (or returns missing value)
        /// only when the specified key is greater than all available keys.
        ///</summary> 


        ExactOrGreater = 3,

        ///<summary> 
        /// Lookup a value associated with the specified key or with the nearest
        /// smaller key that has a value available. Fails (or returns missing value)
        /// only when the specified key is smaller than all available keys.
        ///</summary> 
        ExactOrSmaller = 5,


        ///<summary> 
        /// Lookup a value associated with a key that is greater than the specified one.
        /// Fails (or returns missing value) when the specified key is greater or equal
        /// to the greatest available key.
        ///</summary> 
        Greater = 2,

        ///<summary> 
        /// Lookup a value associated with a key that is smaller than the specified one.
        /// Fails (or returns missing value) when the specified key is smaller or equal
        /// to the smallest available key.
        ///</summary> 

        Smaller = 4,
    }
}