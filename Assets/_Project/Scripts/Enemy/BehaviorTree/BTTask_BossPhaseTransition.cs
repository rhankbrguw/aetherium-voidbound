using UnityEngine;
using Aetherium.Combat.Hitbox;

namespace Aetherium.Enemy.BehaviorTree
{
    public class BTTask_BossPhaseTransition : BTNode
    {
        private readonly Animator animator;
        private readonly Hurtbox hurtbox;
        private readonly int roarAnimationHash;
        private readonly float transitionDuration;

        private float timer;
        private bool hasTriggered;

        public BTTask_BossPhaseTransition(
            Animator animator, 
            Hurtbox hurtbox, 
            int roarAnimationHash, 
            float transitionDuration = 2.5f)
        {
            this.animator = animator;
            this.hurtbox = hurtbox;
            this.roarAnimationHash = roarAnimationHash;
            this.transitionDuration = transitionDuration;
        }

        public override NodeState Evaluate(float deltaTime)
        {
            if (!hasTriggered)
            {
                hasTriggered = true;
                timer = 0f;
                if (hurtbox != null) hurtbox.IsInvulnerable = true;
                if (animator != null) animator.SetTrigger(roarAnimationHash);
            }

            timer += deltaTime;
            if (timer >= transitionDuration)
            {
                if (hurtbox != null) hurtbox.IsInvulnerable = false;
                State = NodeState.Success;
                return State;
            }

            State = NodeState.Running;
            return State;
        }

        public override void Reset()
        {
            base.Reset();
            hasTriggered = false;
            timer = 0f;
            if (hurtbox != null) hurtbox.IsInvulnerable = false;
        }
    }
}
