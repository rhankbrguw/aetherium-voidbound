using NUnit.Framework;
using Aetherium.Enemy.BehaviorTree;

namespace Aetherium.Tests.EditMode
{
    public class BehaviorTreeTests
    {
        private class MockNode : BTNode
        {
            private readonly NodeState stateToReturn;
            public int ExecutionCount { get; private set; }

            public MockNode(NodeState state) => stateToReturn = state;

            public override NodeState Evaluate(float deltaTime)
            {
                ExecutionCount++;
                State = stateToReturn;
                return stateToReturn;
            }
        }

        [Test]
        public void BTSelector_ReturnsSuccessOnFirstSuccessChild()
        {
            MockNode failureNode = new MockNode(NodeState.Failure);
            MockNode successNode = new MockNode(NodeState.Success);
            MockNode unreachedNode = new MockNode(NodeState.Success);

            BTSelector selector = new BTSelector(new BTNode[] { failureNode, successNode, unreachedNode });
            NodeState result = selector.Evaluate(0.016f);

            Assert.AreEqual(NodeState.Success, result);
            Assert.AreEqual(1, failureNode.ExecutionCount);
            Assert.AreEqual(1, successNode.ExecutionCount);
            Assert.AreEqual(0, unreachedNode.ExecutionCount);
        }

        [Test]
        public void BTSequence_FailsImmediatelyOnFirstFailure()
        {
            MockNode successNode = new MockNode(NodeState.Success);
            MockNode failureNode = new MockNode(NodeState.Failure);
            MockNode unreachedNode = new MockNode(NodeState.Success);

            BTSequence sequence = new BTSequence(new BTNode[] { successNode, failureNode, unreachedNode });
            NodeState result = sequence.Evaluate(0.016f);

            Assert.AreEqual(NodeState.Failure, result);
            Assert.AreEqual(1, successNode.ExecutionCount);
            Assert.AreEqual(1, failureNode.ExecutionCount);
            Assert.AreEqual(0, unreachedNode.ExecutionCount);
        }
    }
}
