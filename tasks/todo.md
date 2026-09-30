# Task List: Industry-Grade Combat Implementation

- [x] Task 1: Create Asset-Ready ScriptableObject Catalogs (`CharacterVisualSO`, `AudioCatalogSO`, `VFXCatalogSO`)
  - Acceptance: Clean data contracts for FBX models, audio clips, and VFX particles.
  - Verify: `make test && make verify-constraints` (12 tests passing)
  - Files: `Assets/_Project/Scripts/Core/Data/CharacterVisualSO.cs`, `Assets/_Project/Scripts/Core/Data/AudioCatalogSO.cs`, `Assets/_Project/Scripts/Core/Data/VFXCatalogSO.cs`

- [x] Task 2: Implement 8-Directional Camera-Relative Dodge Roll & Perfect Deflect
  - Acceptance: Dodge rotates and moves in exact input direction relative to camera; deflect window scales posture damage on sub-150ms timing.
  - Verify: `make test` passes with new dodge and deflect assertions.
  - Files: `Assets/_Project/Scripts/Player/States/PlayerDodgeState.cs`, `Assets/_Project/Scripts/Player/States/PlayerParryState.cs`

- [x] Task 3: Implement Lock-On UI Marker & Sekiro Deathblow Visuals
  - Acceptance: Visual lock-on marker tracks target transform; Groggy/Posture Break triggers Sekiro Deathblow sigil on boss HUD.
  - Verify: `make test && make verify-constraints`
  - Files: `Assets/_Project/Scripts/UI/LockOnMarkerController.cs`, `Assets/_Project/Scripts/UI/BossHealthBar.cs`, `Assets/_Project/Scripts/Editor/UIHierarchyBuilder.cs`

- [x] Task 4: Enhance Unit Test Harness & Full Verification
  - Acceptance: All unit tests pass in CLI via .NET 8 SDK; all files <= 150 lines, <= 30 lines/method, <= 3 nesting levels.
  - Verify: `make test && make verify-constraints`
  - Files: `Assets/_Project/Tests/EditMode/AssetCatalogTests.cs`
