using UnityEngine;
using Aetherium.Core.Constants;

namespace Aetherium.CameraSystem
{
    public class ThirdPersonCameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 defaultOffset = new Vector3(0f, 3.0f, -6.0f);
        [SerializeField] private float followSpeed = 8.0f;
        [SerializeField] private float rotationSpeed = 10.0f;
        [SerializeField] private float lookAtHeight = 1.5f;

        private LockOnTargetController lockOnController;

        private void Awake()
        {
            lockOnController = GetComponent<LockOnTargetController>();
            if (target == null)
            {
                GameObject player = GameObject.FindGameObjectWithTag(TagConstants.PLAYER_TAG);
                if (player != null) target = player.transform;
            }
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        private void LateUpdate()
        {
            if (target == null) return;

            if (lockOnController != null && lockOnController.HasTarget)
            {
                HandleLockOnCamera();
            }
            else
            {
                HandleFreeFollowCamera();
            }
        }

        private void HandleFreeFollowCamera()
        {
            Vector3 desiredPosition = target.position + defaultOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

            Vector3 lookTarget = target.position + (Vector3.up * lookAtHeight);
            Quaternion targetRotation = Quaternion.LookRotation(lookTarget - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        private void HandleLockOnCamera()
        {
            Transform lockTarget = lockOnController.CurrentTarget;
            if (lockTarget == null) return;

            Vector3 midpoint = (target.position + lockTarget.position) * 0.5f;
            Vector3 direction = (target.position - lockTarget.position).normalized;
            Vector3 desiredPosition = target.position + (direction * 4.5f) + (Vector3.up * 2.5f);

            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(midpoint + (Vector3.up * lookAtHeight) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
