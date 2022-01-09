using UnityEngine;

namespace Helpers
{
    public static class TransformHelper
    {
        public static void LookAt2D(this Transform transform, Vector2 target)
        {
            Vector2 current   = transform.position;
            var     direction = target - current;
            var     angle     = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}