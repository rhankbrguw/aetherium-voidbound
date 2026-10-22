# AGENTS.md

> Read this file before every task. The global `engineering-standards` skill applies
> to all code. This file provides project-specific context that overrides or extends it.

---

## Project

```
name    : AETHERIUM: VOIDBOUND
genre   : 3D High-Precision Action Combat & Boss Arena
stack   : Unity (C#) + Universal Render Pipeline (URP) + Cinemachine 3
input   : Unity New Input System
physics : 3D Rigidbody / Non-Allocating Spatial Queries (Physics.*NonAlloc)
ai      : Custom C# Garbage-Free Behavior Tree Engine
```

## Active Stack Rules

```
stacks: [unity, csharp]
```

## Folder Structure

```
Assets/_Project/
├── Scripts/
│   ├── Core/
│   │   ├── Constants/       # GameConstants.cs, TagConstants.cs, AnimationConstants.cs, AudioConstants.cs
│   │   ├── StateMachine/    # IState.cs, StateMachine.cs, BaseState.cs
│   │   ├── Events/          # EventBus.cs, GameEvents.cs
│   │   ├── Pools/           # GenericObjectPool.cs, PoolableObject.cs
│   │   └── ServiceLocator/  # ServiceLocator.cs, IGameService.cs
│   ├── Player/
│   │   ├── Controller/      # PlayerController.cs, PlayerInputReader.cs
│   │   ├── States/          # LocomotionState.cs, AttackState.cs, DodgeState.cs, ParryState.cs
│   │   └── Data/            # PlayerStatsSO.cs
│   ├── Combat/
│   │   ├── Hitbox/          # HitboxController.cs, Hurtbox.cs
│   │   ├── Data/            # AttackDataSO.cs, ComboDataSO.cs, DamagePayload.cs
│   │   └── Interfaces/      # IDamageable.cs, IPostureHittable.cs, IParryable.cs
│   ├── Enemy/
│   │   ├── BehaviorTree/    # BTNode.cs, BTSequence.cs, BTSelector.cs, BTTask.cs
│   │   └── Boss/            # BossController.cs, BossPhaseManager.cs, BossStates/
│   ├── Camera/              # LockOnTargetController.cs, CameraShakeManager.cs
│   ├── UI/                  # PlayerHUDController.cs, BossHealthBar.cs, DamagePopup.cs
│   └── AudioVFX/            # HitStopManager.cs, VFXManager.cs, SoundManager.cs
├── ScriptableObjects/       # Concrete ScriptableObject Assets (Attacks, Stats, Configs)
├── Prefabs/                 # Player, Boss, VFX, Projectile Prefabs
├── Animations/              # Animators, Override Controllers, Blend Trees
└── Materials/               # Shaders, Textures, Skyboxes
```

## Agent Constraints

Must:

- Propose architecture/approach before touching more than one file.
- Strictly adhere to `engineering-standards`:
  - Maximum **150 lines** per file.
  - Maximum **30 lines** per function.
  - Maximum **3 levels** of nesting (use early guard clauses).
  - No filler comments; self-documenting code with meaningful names.
- Zero Hardcode Policy: All tag names, layer masks, string keys, animation parameters, and magic numbers must live in `Core/Constants/`.
- Zero Allocation in Update Loop: Never instantiate or allocate memory (no `new`, no LINQ, no string concat) during `Update()` or `FixedUpdate()`. Use object pooling and `Physics.*NonAlloc`.
- Maintain strict decoupled architecture: Use `EventBus` and C# `Action` for cross-system communication. No direct tight coupling between Player and UI/Audio.

Must not:

- Leave any TODO, placeholder, or unfinished stub in final code.
- Write inline magic numbers (e.g. `damage = 25f`, `speed = 7f` must come from ScriptableObjects or Constants).
- Direct call between View/UI components and internal combat logic.
- Exceed 150 lines per file or 30 lines per function.
