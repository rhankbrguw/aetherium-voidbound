using UnityEngine;
using Aetherium.Combat.Data;
using Aetherium.Combat.Hitbox;
using Aetherium.Core.Constants;
using Aetherium.Core.Events;
using Aetherium.Enemy.BehaviorTree;

namespace Aetherium.Enemy.Boss
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 1000f;
        [SerializeField] private float maxPosture = 100f;
        [SerializeField] private Hurtbox hurtbox;
        [SerializeField] private BossPhaseManager phaseManager;

        private float currentHealth;
        private float currentPosture;
        private bool isGroggy;
        private float groggyTimer;
        private BTNode rootNode;

        public float CurrentHealth => currentHealth;
        public float CurrentPosture => currentPosture;
        public bool IsGroggy => isGroggy;

        private void Awake()
        {
            currentHealth = maxHealth;
            currentPosture = 0f;
            SubscribeHurtbox();
            BuildBehaviorTree();
        }

        private void SubscribeHurtbox()
        {
            if (hurtbox == null) return;
            hurtbox.OnDamaged += HandleDamage;
            hurtbox.OnPostureDamaged += HandlePostureDamage;
        }

        private void BuildBehaviorTree()
        {
            rootNode = new BTSelector(
                new BTTask(ExecuteGroggyState),
                new BTTask(ExecuteBossCombatLogic)
            );
        }

        private void Update()
        {
            rootNode?.Evaluate(Time.deltaTime);
            RecoverPosture(Time.deltaTime);
        }

        private void HandleDamage(DamagePayload payload)
        {
            currentHealth = Mathf.Max(0f, currentHealth - payload.DamageAmount);
            phaseManager?.CheckPhaseTransition(currentHealth, maxHealth);
            EventBus.Raise(new BossHealthChangedEvent(
                currentHealth, 
                maxHealth, 
                phaseManager != null ? phaseManager.CurrentPhase : 1
            ));
        }

        private void HandlePostureDamage(float amount)
        {
            if (isGroggy) return;

            currentPosture = Mathf.Min(maxPosture, currentPosture + amount);
            if (currentPosture >= maxPosture)
            {
                TriggerGroggyState();
            }

            EventBus.Raise(new BossPostureChangedEvent(currentPosture, maxPosture, isGroggy));
        }

        private void TriggerGroggyState()
        {
            isGroggy = true;
            groggyTimer = GameConstants.GROGGY_STATE_DURATION;
        }

        private NodeState ExecuteGroggyState(float deltaTime)
        {
            if (!isGroggy) return NodeState.Failure;

            groggyTimer -= deltaTime;
            if (groggyTimer <= 0f)
            {
                isGroggy = false;
                currentPosture = 0f;
                EventBus.Raise(new BossPostureChangedEvent(currentPosture, maxPosture, false));
                return NodeState.Success;
            }
            return NodeState.Running;
        }

        private NodeState ExecuteBossCombatLogic(float deltaTime)
        {
            return NodeState.Running;
        }

        private void RecoverPosture(float deltaTime)
        {
            if (isGroggy || currentPosture <= 0f) return;
            currentPosture = Mathf.Max(0f, currentPosture - GameConstants.POSTURE_RECOVERY_RATE * deltaTime);
            EventBus.Raise(new BossPostureChangedEvent(currentPosture, maxPosture, false));
        }
    }
}
