# Plan: Industry-Grade Action Combat & Asset-Ready Pipeline

## Architecture Overview
This plan implements the 4 core capability modules defined in `SPEC.md`:
1. `asset-slots`: ScriptableObjects contracts (`CharacterVisualSO`, `AudioCatalogSO`, `VFXCatalogSO`).
2. `combat-polish`: 8-Directional camera-relative dodge math, perfect deflect frame window (< 0.15s), camera impulse shake, and non-alloc hit-stop.
3. `anim-bridge`: Enhanced animation event relays for Mecanim state machines.
4. `ui-feedback`: Lock-on target marker, deathblow Kanji / Posture break indicator in HUD.

## Implementation Phases
- **Phase 1 (asset-slots):** Create modular data container SOs for character visual meshes, weapons, SFX, and VFX.
- **Phase 2 (combat-polish):** Implement omnidirectional dodge roll calculation, perfect deflect timing window, and hit-stop manager.
- **Phase 3 (anim-bridge & ui-feedback):** Implement lock-on target UI indicator and tie deathblow sigil to enemy groggy state.
- **Phase 4 (verification):** Write comprehensive unit tests and verify engineering constraints.
