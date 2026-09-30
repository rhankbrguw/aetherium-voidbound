using UnityEngine;

namespace Aetherium.Core.Constants
{
    public static class AnimationConstants
    {
        public static readonly int Speed = Animator.StringToHash("Speed");
        public static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        public static readonly int Dodge = Animator.StringToHash("Dodge");
        public static readonly int DodgeX = Animator.StringToHash("DodgeX");
        public static readonly int DodgeZ = Animator.StringToHash("DodgeZ");
        public static readonly int LightAttack = Animator.StringToHash("LightAttack");
        public static readonly int HeavyAttack = Animator.StringToHash("HeavyAttack");
        public static readonly int ComboIndex = Animator.StringToHash("ComboIndex");
        public static readonly int Parry = Animator.StringToHash("Parry");
        public static readonly int Block = Animator.StringToHash("Block");
        public static readonly int HitStun = Animator.StringToHash("HitStun");
        public static readonly int Groggy = Animator.StringToHash("Groggy");
        public static readonly int Death = Animator.StringToHash("Death");
    }
}
