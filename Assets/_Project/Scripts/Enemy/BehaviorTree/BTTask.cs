using System;

namespace Aetherium.Enemy.BehaviorTree
{
    public class BTTask : BTNode
    {
        private readonly Func<float, NodeState> action;

        public BTTask(Func<float, NodeState> action)
        {
            this.action = action;
        }

        public override NodeState Evaluate(float deltaTime)
        {
            if (action == null)
            {
                State = NodeState.Failure;
                return State;
            }

            State = action.Invoke(deltaTime);
            return State;
        }
    }
}
