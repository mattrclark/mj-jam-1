using UnityEngine;

namespace Entities.Player
{
    public class CameraController : MonoBehaviour
    {
        public Transform followMe;

        public int maxX;
        public int minX;
        public int maxY;
        public int minY;

        private void Update()
        {
            bool IsWithinBounds(float real, int max, int min) => real < max && real > min;

            var newPosition    = transform.position;
            var followPosition = followMe.position;

            if (IsWithinBounds(followPosition.x, maxX, minX))
                newPosition.x = followPosition.x;

            if (IsWithinBounds(followPosition.y, maxY, minY))
                newPosition.y = followPosition.y;

            transform.position = newPosition;
        }
    }
}