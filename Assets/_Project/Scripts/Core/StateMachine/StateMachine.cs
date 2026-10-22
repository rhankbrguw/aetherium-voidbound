using System;

namespace Aetherium.Core.StateMachine
{
    public class StateMachine
    {
        public IState CurrentState { get; private set; }
        public event Action<IState> OnStateChanged;

        public void ChangeState(IState newState)
        {
            if (newState == null || newState == CurrentState)
            {
                return;
            }

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
            OnStateChanged?.Invoke(CurrentState);
        }

        public void Tick(float deltaTime)
        {
            CurrentState?.Tick(deltaTime);
        }

        public void FixedTick(float fixedDeltaTime)
        {
            CurrentState?.FixedTick(fixedDeltaTime);
        }
    }
}
