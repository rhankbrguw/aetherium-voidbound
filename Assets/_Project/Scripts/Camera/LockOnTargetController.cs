using UnityEngine;
using Aetherium.Core.Constants;

namespace Aetherium.CameraSystem
{
    public class LockOnTargetController : MonoBehaviour
    {
        [SerializeField] private float lockOnRadius = 25f;
        [SerializeField] private LayerMask targetLayer;

        private readonly Collider[] hitBuffer = new Collider[GameConstants.NON_ALLOC_COLLIDER_BUFFER_SIZE];
        private Transform currentTarget;

        public Transform CurrentTarget => currentTarget;
        public bool HasTarget => currentTarget != null;

        public void ToggleLockOn()
        {
            if (currentTarget != null)
            {
                currentTarget = null;
                return;
            }

            FindClosestTarget();
        }

        private void FindClosestTarget()
        {
            int count = Physics.OverlapSphereNonAlloc(
                transform.position,
                lockOnRadius,
                hitBuffer,
                targetLayer,
                QueryTriggerInteraction.Collide
            );

            Transform closest = null;
            float minDistanceSqr = Mathf.Infinity;

            for (int i = 0; i < count; i++)
            {
                if (hitBuffer[i] == null) continue;

                float distSqr = (hitBuffer[i].transform.position - transform.position).sqrMagnitude;
                if (distSqr < minDistanceSqr)
                {
                    minDistanceSqr = distSqr;
                    closest = hitBuffer[i].transform;
                }
            }

            currentTarget = closest;
        }
    }
}
