using UnityEngine;

namespace Aetherium.Core.Data
{
    [CreateAssetMenu(fileName = "CharacterVisual", menuName = "Aetherium/Data/Character Visual")]
    public class CharacterVisualSO : ScriptableObject
    {
        [Header("Model & Rig")]
        [SerializeField] private GameObject characterModelPrefab;
        [SerializeField] private RuntimeAnimatorController animatorController;
        [SerializeField] private Avatar characterAvatar;

        [Header("Weapon Sockets")]
        [SerializeField] private GameObject defaultWeaponPrefab;
        [SerializeField] private HumanBodyBones weaponSocketBone = HumanBodyBones.RightHand;
        [SerializeField] private Vector3 weaponPositionOffset = Vector3.zero;
        [SerializeField] private Vector3 weaponRotationOffset = Vector3.zero;

        [Header("Scale & Adjustments")]
        [SerializeField] private float uniformScale = 1.0f;

        public GameObject CharacterModelPrefab => characterModelPrefab;
        public RuntimeAnimatorController AnimatorController => animatorController;
        public Avatar CharacterAvatar => characterAvatar;
        public GameObject DefaultWeaponPrefab => defaultWeaponPrefab;
        public HumanBodyBones WeaponSocketBone => weaponSocketBone;
        public Vector3 WeaponPositionOffset => weaponPositionOffset;
        public Vector3 WeaponRotationOffset => weaponRotationOffset;
        public float UniformScale => uniformScale;
    }
}
