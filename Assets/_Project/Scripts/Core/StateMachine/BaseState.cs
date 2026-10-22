namespace Aetherium.Core.StateMachine
{
    public abstract class BaseState : IState
    {
        protected readonly StateMachine stateMachine;

        protected BaseState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void Enter() { }

        public virtual void Tick(float deltaTime) { }

        public virtual void FixedTick(float fixedDeltaTime) { }

        public virtual void Exit() { }
    }
}
