using UnityEngine;

namespace Utilities.Mathematics
{
    public static class MathExtensions
    {
        public static float MaxVector3CoordinateOnPlane(this Vector3 vector) =>
            vector.x > vector.y ? vector.x : vector.y;
    }
}