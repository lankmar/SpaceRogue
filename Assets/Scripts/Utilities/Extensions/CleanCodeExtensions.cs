using System;

namespace Utilities.Extensions
{
    public static partial class CleanCodeExtensions
    {
        public static void Do<T>(this T target, Action<T> action)
        {
            action.Invoke(target);
        }
    }
}