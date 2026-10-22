using UnityEngine;
using UnityEngine.UI;
using Aetherium.CameraSystem;
using Aetherium.Core.Constants;

namespace Aetherium.UI
{
    public class LockOnMarkerController : MonoBehaviour
    {
        [SerializeField] private Image markerImage;
        [SerializeField] private LockOnTargetController lockOnController;
        [SerializeField] private Vector3 worldOffset = new Vector3(0f, 1.5f, 0f);
        [SerializeField] private float rotationSpeed = 90f;

        private UnityEngine.Camera mainCamera;

        private void Awake()
        {
            mainCamera = UnityEngine.Camera.main;
            if (lockOnController == null && mainCamera != null)
            {
                lockOnController = mainCamera.GetComponent<LockOnTargetController>();
            }
            if (markerImage == null)
            {
                markerImage = GetComponent<Image>();
            }
        }

        private void LateUpdate()
        {
            if (lockOnController == null || !lockOnController.HasTarget)
            {
                SetMarkerActive(false);
                return;
            }

            Transform target = lockOnController.CurrentTarget;
            if (target == null || mainCamera == null)
            {
                SetMarkerActive(false);
                return;
            }

            Vector3 targetWorldPos = target.position + worldOffset;
            Vector3 screenPos = mainCamera.WorldToScreenPoint(targetWorldPos);

            if (screenPos.z < 0f)
            {
                SetMarkerActive(false);
                return;
            }

            SetMarkerActive(true);
            transform.position = screenPos;
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }

        private void SetMarkerActive(bool active)
        {
            if (markerImage != null && markerImage.enabled != active)
            {
                markerImage.enabled = active;
            }
        }
    }
}
