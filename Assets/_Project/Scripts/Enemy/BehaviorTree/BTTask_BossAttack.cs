using UnityEngine;
using Aetherium.Combat.Data;
using Aetherium.Combat.Hitbox;

namespace Aetherium.Enemy.BehaviorTree
{
    public class BTTask_BossAttack : BTNode
    {
        private readonly Transform self;
        private readonly Transform target;
        private readonly Animator animator;
        private readonly HitboxController hitbox;
        private readonly AttackDataSO attackData;
        private readonly int animationTriggerHash;

        private float timer;
        private bool isHitboxActive;

        public BTTask_BossAttack(
            Transform self,
            Transform target,
            Animator animator,
            HitboxController hitbox,
            AttackDataSO attackData,
            int animationTriggerHash)
        {
            this.self = self;
            this.target = target;
            this.animator = animator;
            this.hitbox = hitbox;
            this.attackData = attackData;
            this.animationTriggerHash = animationTriggerHash;
        }

        public override NodeState Evaluate(float deltaTime)
        {
            if (attackData == null) return NodeState.Failure;

            if (State == NodeState.Running)
            {
                timer += deltaTime;
                RotateTowardsTargetDuringStartup(deltaTime);
                ManageHitboxWindow();

                if (timer >= attackData.TotalDuration)
                {
                    Reset();
                    State = NodeState.Success;
                    return State;
                }
                return NodeState.Running;
            }

            StartAttack();
            State = NodeState.Running;
            return State;
        }

        private void StartAttack()
        {
            timer = 0f;
            isHitboxActive = false;
            if (animator != null) animator.SetTrigger(animationTriggerHash);
        }

        private void RotateTowardsTargetDuringStartup(float deltaTime)
        {
            if (timer > attackData.StartupDuration || target == null) return;
            Vector3 direction = (target.position - self.position).normalized;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.001f)
            {
                self.rotation = Quaternion.Slerp(self.rotation, Quaternion.LookRotation(direction), 10f * deltaTime);
            }
        }

        private void ManageHitboxWindow()
        {
            if (timer >= attackData.StartupDuration && timer <= (attackData.StartupDuration + attackData.ActiveHitboxDuration))
            {
                if (!isHitboxActive)
                {
                    isHitboxActive = true;
                    hitbox?.EnableHitbox(attackData);
                }
            }
            else if (isHitboxActive)
            {
                isHitboxActive = false;
                hitbox?.DisableHitbox();
            }
        }

        public override void Reset()
        {
            base.Reset();
            timer = 0f;
            if (isHitboxActive)
            {
                isHitboxActive = false;
                hitbox?.DisableHitbox();
            }
        }
    }
}
