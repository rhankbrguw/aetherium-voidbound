using UnityEngine;
using Aetherium.Core.StateMachine;
using Aetherium.Player.Data;
using Aetherium.Player.States;
using Aetherium.Combat.Hitbox;

namespace Aetherium.Player.Controller
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStatsSO stats;
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private HitboxController weaponHitbox;

        private CharacterController characterController;
        private StateMachine stateMachine;
        private float currentStamina;

        public PlayerStatsSO Stats => stats;
        public PlayerInputReader Input => inputReader;
        public Animator Animator => animator;
        public Transform CameraTransform => cameraTransform;
        public CharacterController CharacterController => characterController;
        public HitboxController WeaponHitbox => weaponHitbox;
        public float CurrentStamina => currentStamina;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            if (cameraTransform == null && UnityEngine.Camera.main != null)
            {
                cameraTransform = UnityEngine.Camera.main.transform;
            }
            if (stats == null)
            {
                stats = ScriptableObject.CreateInstance<PlayerStatsSO>();
            }
            if (inputReader == null)
            {
                inputReader = ScriptableObject.CreateInstance<PlayerInputReader>();
            }

            currentStamina = stats.MaxStamina;
            InitializeStateMachine();
        }

        private void InitializeStateMachine()
        {
            stateMachine = new StateMachine();
            PlayerLocomotionState locomotionState = new PlayerLocomotionState(stateMachine, this);
            stateMachine.ChangeState(locomotionState);
        }

        private void Update()
        {
            inputReader?.PollInput();
            stateMachine?.Tick(Time.deltaTime);
            RegenerateStamina(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            stateMachine?.FixedTick(Time.fixedDeltaTime);
        }

        public bool ConsumeStamina(float amount)
        {
            if (currentStamina < amount)
            {
                return false;
            }

            currentStamina -= amount;
            return true;
        }

        private void RegenerateStamina(float deltaTime)
        {
            if (stats == null || currentStamina >= stats.MaxStamina)
            {
                return;
            }

            currentStamina = Mathf.Min(currentStamina + stats.StaminaRegenRate * deltaTime, stats.MaxStamina);
        }

        public void ChangeState(IState newState)
        {
            stateMachine.ChangeState(newState);
        }
    }
}
