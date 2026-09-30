# Spec: Industry-Grade Action Combat & Asset-Ready Pipeline (AETHERIUM: VOIDBOUND)

## Objective
Build a modular, zero-allocation, industry-standard 3D combat architecture inspired by Sekiro: Shadows Die Twice and Dark Souls.
The architecture provides clean plug-and-play ScriptableObject data slots for 3D FBX models, animations, audio clips, and VFX particles, enabling zero-code asset swap and maximum gameplay responsiveness.

## Tech Stack
- Engine: Unity 6 (6000.6.3f1) C#
- Render Pipeline: Universal Render Pipeline (URP) / Built-in fallback
- Input: Unity New Input System (polling & action callbacks)
- Architecture: Decoupled EventBus + StateMachine + BehaviorTree Engine

## Capability Map
| Module ID | Responsibility | Depends On |
|---|---|---|
| `asset-slots` | ScriptableObject & Prefab contracts for FBX models, weapons, audio, & particle FX | — |
| `combat-polish` | 8-Directional Dodge Roll, dynamic hit-stop, camera shake impulse, & parry sparks relay | `asset-slots` |
| `anim-bridge` | Mecanim Animator state machine integration & Animation Event relays | `asset-slots`, `combat-polish` |
| `ui-feedback` | Sekiro-style Red Deathblow Reticle, Posture Shatter HUD, & Boss Lock-On marker | `combat-polish` |

## Commands
- Build/Test: `make test`
- Lint/Constraints: `make verify-constraints`
- Standalone Linux Build: `make build-linux`
- Standalone Windows Build: `make build-windows`

## Boundaries
- **Always:**
  - Maximum 150 lines per file, maximum 30 lines per function, maximum 3 levels of nesting.
  - Zero allocation in Update/FixedUpdate loops (no `new`, no LINQ, no string concatenation).
  - All constants (tags, layers, keys, audio tokens, animation hashes) live in `Core/Constants/`.
- **Ask first:** Adding external third-party dependencies.
- **Never:**
  - Inline magic numbers.
  - Unhandled exceptions or empty catch blocks.
  - Direct coupling between UI and gameplay state machines.

## Success Criteria
1. NUnit test harness passes 100% via `make test`.
2. All files strictly adhere to `make verify-constraints`.
3. 8-Directional Dodge, Perfect Deflect, Posture Stagger, and Hit-Stop execute with zero runtime allocations and zero frame drops.
