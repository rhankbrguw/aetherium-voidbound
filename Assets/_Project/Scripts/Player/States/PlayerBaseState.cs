using Aetherium.Core.StateMachine;
using Aetherium.Player.Controller;

namespace Aetherium.Player.States
{
    public abstract class PlayerBaseState : BaseState
    {
        protected readonly PlayerController player;

        protected PlayerBaseState(StateMachine stateMachine, PlayerController player) 
            : base(stateMachine)
        {
            this.player = player;
        }
    }
}
