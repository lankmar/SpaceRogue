using System;

namespace Utilities.Extensions
{
    public static partial class CleanCodeExtensions
    {
        public static T Do<T>(this T target, Action<T> action)
        {
            action.Invoke(target);
            return target;
        }
        
        public static T Do<T>(this T target, Action<T> action, bool when)
        {
            if (when)
            {
                action.Invoke(target);
            }

            return target;
        }
    }
}