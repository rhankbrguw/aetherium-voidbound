using UnityEngine;

namespace Aetherium.Enemy.BehaviorTree
{
    public class BTCondition_Distance : BTNode
    {
        private readonly Transform self;
        private readonly Transform target;
        private readonly float minDistance;
        private readonly float maxDistance;

        public BTCondition_Distance(Transform self, Transform target, float minDistance, float maxDistance)
        {
            this.self = self;
            this.target = target;
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
        }

        public override NodeState Evaluate(float deltaTime)
        {
            if (self == null || target == null)
            {
                State = NodeState.Failure;
                return State;
            }

            float distanceSqr = (target.position - self.position).sqrMagnitude;
            bool isInRange = distanceSqr >= (minDistance * minDistance) && distanceSqr <= (maxDistance * maxDistance);

            State = isInRange ? NodeState.Success : NodeState.Failure;
            return State;
        }
    }
}
