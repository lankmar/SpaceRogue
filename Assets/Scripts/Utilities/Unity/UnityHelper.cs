using UnityEngine;

namespace Utilities.Unity
{
    public static class UnityHelper
    {
        public static bool IsAnyObjectAtPosition(Vector3 position, float radius)
        {
            var collider = Physics2D.OverlapCircle(position, radius);
            return collider is not null;
        }
    }
}