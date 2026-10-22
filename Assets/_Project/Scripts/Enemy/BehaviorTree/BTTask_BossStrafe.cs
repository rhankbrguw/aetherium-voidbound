using UnityEngine;

namespace Aetherium.Enemy.BehaviorTree
{
    public class BTTask_BossStrafe : BTNode
    {
        private readonly Transform self;
        private readonly Transform target;
        private readonly CharacterController characterController;
        private readonly float strafeSpeed;
        private readonly float maxDuration;

        private float timer;
        private int strafeDirection;

        public BTTask_BossStrafe(
            Transform self, 
            Transform target, 
            CharacterController characterController, 
            float strafeSpeed = 3.5f, 
            float maxDuration = 2.0f)
        {
            this.self = self;
            this.target = target;
            this.characterController = characterController;
            this.strafeSpeed = strafeSpeed;
            this.maxDuration = maxDuration;
        }

        public override NodeState Evaluate(float deltaTime)
        {
            if (self == null || target == null) return NodeState.Failure;

            if (State == NodeState.Running)
            {
                timer += deltaTime;
                PerformStrafe(deltaTime);

                if (timer >= maxDuration)
                {
                    Reset();
                    State = NodeState.Success;
                    return State;
                }
                return NodeState.Running;
            }

            timer = 0f;
            strafeDirection = Random.value > 0.5f ? 1 : -1;
            State = NodeState.Running;
            return State;
        }

        private void PerformStrafe(float deltaTime)
        {
            Vector3 toTarget = (target.position - self.position).normalized;
            toTarget.y = 0f;
            self.rotation = Quaternion.LookRotation(toTarget);

            Vector3 tangent = Vector3.Cross(Vector3.up, toTarget) * strafeDirection;
            Vector3 movement = (tangent * strafeSpeed) + (Vector3.up * -2f);
            characterController?.Move(movement * deltaTime);
        }

        public override void Reset()
        {
            base.Reset();
            timer = 0f;
        }
    }
}
