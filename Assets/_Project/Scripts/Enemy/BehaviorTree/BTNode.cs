namespace Aetherium.Enemy.BehaviorTree
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }

    public abstract class BTNode
    {
        public NodeState State { get; protected set; }

        public abstract NodeState Evaluate(float deltaTime);

        public virtual void Reset()
        {
            State = NodeState.Running;
        }
    }
}
