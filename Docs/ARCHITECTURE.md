# Technical Architecture & Coding Guidelines
# Project: AETHERIUM: VOIDBOUND

This document outlines the software engineering architecture, design patterns, and programming standards used in **AETHERIUM: VOIDBOUND**.

---

## 1. Architectural Philosophy

The codebase is built according to **Clean Architecture** and **SOLID Principles**, designed to be modular, highly testable, and memory-efficient:
1. **Separation of Concerns:** Logic (States), Presentation (View/UI/VFX), and Data (ScriptableObjects) are strictly decoupled.
2. **Event-Driven Decoupling:** Components communicate via strongly-typed C# Actions and an Event Bus rather than hard references.
3. **Data-Oriented ScriptableObjects:** Attack stats, frame windows, audio clips, and VFX prefabs are defined as assets, not hardcoded into scripts.
4. **Zero GC in Update Loop:** Avoid runtime allocations (`new`, boxing, LINQ, string concatenation in frame updates) to ensure a steady 60-120 FPS.

---

## 2. Core Subsystems

### 2.1 Hierarchical State Machine (HSM)
The Player and Enemies utilize a clean State Machine pattern:
* `IState`: Interface with `Enter()`, `Tick()`, `FixedTick()`, and `Exit()`.
* `StateMachine`: Base executor managing transitions and active state lifecycle.
* `PlayerBaseState`: Abstract class holding shared references (`PlayerController`, `PlayerInput`, `Animator`).
  * `PlayerLocomotionState` (Idle, Walk, Run)
  * `PlayerDodgeState` (8-Way roll with I-Frames)
  * `PlayerAttackState` (Combo chain processing & animation event hooks)
  * `PlayerParryState` (Parry timing window & deflection logic)
  * `PlayerStunState` / `PlayerDeadState`

### 2.2 Dynamic Frame-Data Attack System
Attacks are driven by `AttackDataSO` (ScriptableObjects):
```csharp
[CreateAssetMenu(fileName = "NewAttackData", menuName = "Aetherium/Combat/Attack Data")]
public class AttackDataSO : ScriptableObject
{
    public string attackName;
    public float damage;
    public float postureDamage;
    public float knockbackForce;
    public float hitStopDuration = 0.06f;
    public float cameraShakeIntensity = 0.3f;
    public GameObject hitVFXPrefab;
    public AudioClip hitSFX;
}
```

### 2.3 Damage & Hitbox Pipeline
1. During an attack animation, an `AnimationEvent` or `Timeline/Playables` marker triggers `HitboxController.EnableHitbox()`.
2. The weapon colliders / Raycast Sweeps record hit contacts using non-allocating physics (`Physics.OverlapSphereNonAlloc` or `BoxCastNonAlloc`).
3. Targets implementing `IDamageable` and `IPostureHittable` receive the payload:
```csharp
public interface IDamageable
{
    void TakeDamage(DamagePayload payload);
}

public interface IPostureHittable
{
    void TakePostureDamage(float amount);
}
```

### 2.4 Custom Behavior Tree Engine for Boss AI
Instead of monolithic `Update()` scripts, Boss decisions are evaluated through a clean Behavior Tree:
* **Composite Nodes:** `SelectorNode` (Fallback), `SequenceNode` (AND sequence).
* **Decorator Nodes:** `InverterNode`, `CooldownDecoratorNode`.
* **Action Nodes:** `BTTask_ChasePlayer`, `BTTask_ExecuteAttack`, `BTTask_Reposition`, `BTTask_CastAOE`.
* **Condition Nodes:** `BTCondition_IsPlayerInMeleeRange`, `BTCondition_IsBossLowHealth`.

### 2.5 Object Pooling Service
A generic `ObjectPool<T>` manages:
* Hit spark & slash ribbon VFX.
* Floating damage numbers & posture indicators.
* Projectiles and shockwave wave meshes.

---

## 3. Directory Layout Standards

All project assets reside under `Assets/_Project/`:
* `Scripts/Core/`: Base patterns (`StateMachine`, `EventBus`, `ObjectPool`).
* `Scripts/Player/`: Controller, State classes, Input adapters.
* `Scripts/Combat/`: Hitbox detection, damage calculations, combat interfaces.
* `Scripts/Enemy/`: Boss AI, Behavior Tree node implementations.
* `Scripts/Camera/`: Target lock-on and Cinemachine custom extensions.
* `Scripts/AudioVFX/`: Hit-stop time controllers, particle pool hooks.
* `ScriptableObjects/`: Asset definitions for weapons, combos, stats.
