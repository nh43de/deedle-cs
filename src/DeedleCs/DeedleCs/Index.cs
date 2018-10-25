using Deedle.Indices;
using Deedle.Indices.Linear;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deedle
{
    public static class Index
    {
        public static IIndex<T> ofKeys<T>(IReadOnlyCollection<T> keys)
        {
            return LinearIndexBuilder.Instance.Create<T>(keys, null);
        }
    }
}
