using System.Collections.Generic;

namespace Aetherium.Enemy.BehaviorTree
{
    public class BTSequence : BTNode
    {
        private readonly List<BTNode> children = new List<BTNode>();

        public BTSequence(params BTNode[] nodes)
        {
            children.AddRange(nodes);
        }

        public override NodeState Evaluate(float deltaTime)
        {
            for (int i = 0; i < children.Count; i++)
            {
                NodeState childState = children[i].Evaluate(deltaTime);
                if (childState == NodeState.Running)
                {
                    State = NodeState.Running;
                    return State;
                }
                if (childState == NodeState.Failure)
                {
                    State = NodeState.Failure;
                    return State;
                }
            }

            State = NodeState.Success;
            return State;
        }

        public override void Reset()
        {
            base.Reset();
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Reset();
            }
        }
    }
}
