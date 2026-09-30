using System;
using UnityEngine;
using Aetherium.Core.Events;

namespace Aetherium.Enemy.Boss
{
    public class BossPhaseManager : MonoBehaviour
    {
        [SerializeField] private float phase2HealthThreshold = 0.5f;

        private int currentPhase = 1;

        public int CurrentPhase => currentPhase;
        public event Action<int> OnPhaseChanged;

        public void CheckPhaseTransition(float currentHealth, float maxHealth)
        {
            if (currentPhase == 1 && (currentHealth / maxHealth) <= phase2HealthThreshold)
            {
                currentPhase = 2;
                OnPhaseChanged?.Invoke(currentPhase);
                EventBus.Raise(new BossHealthChangedEvent(currentHealth, maxHealth, currentPhase));
            }
        }

        public void ResetPhase()
        {
            currentPhase = 1;
        }
    }
}
