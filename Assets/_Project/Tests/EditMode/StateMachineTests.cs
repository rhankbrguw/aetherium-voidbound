using NUnit.Framework;
using Aetherium.Core.StateMachine;

namespace Aetherium.Tests.EditMode
{
    public class StateMachineTests
    {
        private class TestState : IState
        {
            public bool EnterCalled { get; private set; }
            public bool ExitCalled { get; private set; }
            public int TickCount { get; private set; }
            public int FixedTickCount { get; private set; }

            public void Enter() => EnterCalled = true;
            public void Exit() => ExitCalled = true;
            public void Tick(float deltaTime) => TickCount++;
            public void FixedTick(float fixedDeltaTime) => FixedTickCount++;
        }

        [Test]
        public void StateMachine_TransitionsToNewState_CallsEnterAndExit()
        {
            StateMachine stateMachine = new StateMachine();
            TestState stateA = new TestState();
            TestState stateB = new TestState();

            stateMachine.ChangeState(stateA);
            Assert.IsTrue(stateA.EnterCalled);
            Assert.AreEqual(stateA, stateMachine.CurrentState);

            stateMachine.ChangeState(stateB);
            Assert.IsTrue(stateA.ExitCalled);
            Assert.IsTrue(stateB.EnterCalled);
            Assert.AreEqual(stateB, stateMachine.CurrentState);
        }

        [Test]
        public void StateMachine_TickAndFixedTick_PropagateToCurrentState()
        {
            StateMachine stateMachine = new StateMachine();
            TestState state = new TestState();
            stateMachine.ChangeState(state);

            stateMachine.Tick(0.016f);
            stateMachine.Tick(0.016f);
            stateMachine.FixedTick(0.02f);

            Assert.AreEqual(2, state.TickCount);
            Assert.AreEqual(1, state.FixedTickCount);
        }
    }
}
