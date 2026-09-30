# PRODUCT REQUIREMENT DOCUMENT (PRD)
## Asset Specification & Technical Art Contract
**Project:** AETHERIUM: VOIDBOUND  
**Engine & Version:** Unity 6 (6000.6.3f1) C#  
**Target Genre:** 3D High-Precision Action Combat & Souls/Sekiro Boss Arena  
**Document Version:** 1.0.0 (Production-Ready)  

---

## 1. Executive Summary & Art Direction
* **Visual Theme:** Dark Fantasy / Astral Void (Dark Souls meets Sekiro). Moody, weathered ancient gothic colosseum floating in an astral void, bathed in a dying golden sun and illuminated by cyan void braziers.
* **Rigging Standard:** Unity Humanoid Rig (compatible with Mixamo, Blender Rigify, or Maya HumanIK).
* **Scale Unit:** 1 Unity Unit = 1 Meter (Standard Real-World Metric).

---

## 2. 3D Model Specifications

### 2.1 Player Character ("Soulbound Knight")
* **Format:** `.fbx` (Embedded Textures / PBR Standard Metallic-Roughness).
* **Polycount Target:** 15,000 – 30,000 Triangles.
* **Rigging:** Humanoid Skeleton with T-Pose / A-Pose default.
* **Dimensions:** Height: `1.80m`, Width: `0.50m`.
* **Socket Bones:**
  * `RightHand` (Weapon Hold Socket).
  * `Spine` / `UpperChest` (Sheath Socket).
* **Visual Style:** Weathered dark iron plate armor with torn cobalt/cyan cloth accents and an astral glowing crest.

### 2.2 Boss Character ("Voidbound Berserker")
* **Format:** `.fbx` (Humanoid Rig).
* **Polycount Target:** 25,000 – 45,000 Triangles.
* **Rigging:** Humanoid Skeleton.
* **Dimensions:** Height: `3.20m` (Imposing Scale), Width: `1.20m`.
* **Socket Bones:**
  * `RightHand` (Greatsword Main Socket).
  * `Head` (Posture/Groggy Deathblow UI Anchor).
* **Visual Style:** Corrupted demonic warlord adorned in shattered obsidian plate, crimson glowing cracks across the skin/armor, and void mist seeping from joints.

### 2.3 Weapons
| Asset Name | Type | Target Length | Attachment Point | Style |
|---|---|---|---|---|
| `Player_Katana_StraightSword.fbx` | One-Handed Sword | `1.10m` | Player `RightHand` | Sleek silver blade with ancient runes |
| `Boss_Void_Greatsword.fbx` | Ultra Greatsword | `2.40m` | Boss `RightHand` | Jagged obsidian slab infused with crimson void |

### 2.4 Environment Props (Modular Arena)
* `Arena_Floor_Disc.fbx`: Diameter `36.0m`, Height `0.5m`. Weathered obsidian tiles with circular runic engravings.
* `Gothic_Pillar.fbx`: Height `9.0m`, Base `2.5m x 2.5m`. Ancient weathered stone column with gothic arch engravings.
* `Astral_Brazier.fbx`: Height `1.2m`. Metal tripod brazier bowl holding eternal astral fire.

---

## 3. Mecanim Animation Clip Specifications

All animations must be formatted as **`FBX (Generic or Humanoid)`** at **`30 / 60 FPS`**, with **Root Motion Bake into Pose** (XZ for Locomotion, or handled by CharacterController).

### 3.1 Player Animation Set (12 Clips)

| Clip Name | Duration | Keyframe Events (Mecanim Relay) | Description |
|---|---|---|---|
| `Player_Combat_Idle` | Loop | — | Low stance, sword readied in two hands |
| `Player_Walk_BlendTree` | Loop (8-way) | `OnFootstep` (Frame 8, 22) | Directional walk (Forward, Back, Strafe) |
| `Player_Run_BlendTree` | Loop (8-way) | `OnFootstep` (Frame 6, 18) | Sprinting locomotion |
| `Player_Dodge_Roll` | 0.45s (14 frames) | `OnIFrameStart` (F2), `OnIFrameEnd` (F10) | Fast evasive combat roll forward/omni |
| `Player_LightAttack_1` | 0.40s (12 frames) | `OnHitboxActive` (F3), `OnHitboxInactive` (F8) | Diagonal downward slash (Right to Left) |
| `Player_LightAttack_2` | 0.45s (14 frames) | `OnHitboxActive` (F3), `OnHitboxInactive` (F9) | Horizontal sweeping slash (Left to Right) |
| `Player_LightAttack_3` | 0.55s (17 frames) | `OnHitboxActive` (F4), `OnHitboxInactive` (F11) | Upward thrust & cleave finisher |
| `Player_HeavyAttack_Charged` | 0.85s (26 frames) | `OnHitboxActive` (F12), `OnHitboxInactive` (F18) | Windup forward lunge thrust |
| `Player_Parry_Deflect` | 0.35s (11 frames) | `OnParryWindowOpen` (F1), `OnParryWindowClose` (F7) | Crisp Sekiro-style sword deflect block |
| `Player_HitStun` | 0.30s (9 frames) | — | Flinch impact reaction to torso |
| `Player_Deathblow_Execution` | 1.20s (36 frames) | `OnDealDamage` (F16), `OnCameraShake` (F16) | Plunging blade execution into staggered boss |
| `Player_Death` | 1.80s (54 frames) | — | Collapse to the floor |

### 3.2 Boss Animation Set (9 Clips)

| Clip Name | Duration | Keyframe Events (Mecanim Relay) | Description |
|---|---|---|---|
| `Boss_Idle_Stance` | Loop | — | Heavy menacing breathing with broadsword resting |
| `Boss_Walk_Stalk` | Loop | `OnFootstep` (Frame 12, 28) | Slow, deliberate predatory advance |
| `Boss_Combo_TwinSlash` | 1.10s (33 frames) | `OnHitboxActive` (F10, F22) | Two sweeping wide horizontal slashes |
| `Boss_Overhead_Cleave` | 1.40s (42 frames) | `OnHitboxActive` (F24), `OnCameraShake` (F25) | Two-handed jump overhead ground slam |
| `Boss_Void_Shockwave` | 1.60s (48 frames) | `OnSpawnVFX` (F20), `OnCameraShake` (F20) | Roar unleashing radial shockwave |
| `Boss_Groggy_Kneel` | Loop (Stagger) | — | Posture broken; drops to one knee panting |
| `Boss_Phase2_Enrage` | 2.50s (75 frames) | `OnSpawnVFX` (F35), `OnRoar` (F35) | Phase transition roar with bursting void aura |
| `Boss_Deflected_Recoil` | 0.40s (12 frames) | — | Sword deflected back with heavy stagger |
| `Boss_Death_Collapse` | 3.00s (90 frames) | — | Slow disintegration / falling backward |

---

## 4. Audio & SFX Engineering Specifications

* **Format:** Uncompressed 24-bit / 48kHz Stereo `.WAV` or `.OGG`.
* **Dynamic Range:** Clean transients, minimal background noise floor ($< -60\text{ dB}$).

| Audio Token / Key | Loudness (LUFS) | Description / Sound Profile |
|---|---|---|
| `SFX_Parry_Success` | $-6.0\text{ LUFS}$ | Crisp, high-pitch metallic sword clash (*Sekiro "Clang"*) |
| `SFX_Block_Impact` | $-9.0\text{ LUFS}$ | Dull heavy metal-on-armor thud |
| `SFX_SwordHit_Flesh` | $-8.0\text{ LUFS}$ | Sharp slicing sound with wet tearing transient |
| `SFX_SwordSwing_Light` | $-14.0\text{ LUFS}$ | Fast whoosh through air |
| `SFX_SwordSwing_Heavy` | $-10.0\text{ LUFS}$ | Deep, bass-heavy whoosh |
| `SFX_Posture_Break` | $-5.0\text{ LUFS}$ | High-impact shattering glass / sonic resonance burst |
| `SFX_BossRoar_Phase2` | $-6.0\text{ LUFS}$ | Demonic, gutteral monstrous roar mixed with void wind |
| `SFX_Void_Explosion` | $-7.0\text{ LUFS}$ | Low-frequency sub-bass shockwave impact |
| `SFX_Dodge_Whoosh` | $-16.0\text{ LUFS}$ | Quick cloth / armor rustle with breeze |
| `SFX_Footstep_Stone` | $-18.0\text{ LUFS}$ | Armored boot stepping on hard solid stone |

---

## 5. Visual Effects & Particle Prefabs (VFX)

| VFX Prefab Name | System Type | Visual Characteristics |
|---|---|---|
| `VFX_Deflect_Sparks.prefab` | Burst Particle (15-25 particles) | Bright white-orange spark burst radiating from sword collision point |
| `VFX_Blood_Slash_Trail.prefab` | Mesh Trail / Splatter | Dark crimson & black void mist slash ribbon trail along the blade |
| `VFX_Void_Shockwave.prefab` | Expanding Ring Mesh + Particles | Radial cyan/purple shockwave ring expanding along the ground |
| `VFX_Deathblow_Sigil.prefab` | World Space Canvas / Unlit Quad | Sekiro-inspired glowing red Deathblow Kanji/Rune floating over boss head |
| `VFX_Astral_Flame.prefab` | Continuous Particle System | Ethereal cyan/turquoise fire emitting subtle embers and light |

---

## 6. Ready-to-Use GPT / AI Generation Prompts

Use these prompts directly when prompting Midjourney, ChatGPT, 3D generative tools, or searching the Unity Asset Store:

### Prompt 1: 3D Character Model (Soulbound Knight)
> *"Full-body 3D game-ready character model of an astral dark souls knight, worn blackened steel plate armor, glowing cyan ethereal runes engraved on gauntlets, tattered dark navy cape, symmetrical T-pose, realistic PBR textures, clean topology suitable for humanoid rigging, Unreal Engine / Unity 6 aesthetic, 8k resolution, stylized realism."*

### Prompt 2: 3D Boss Model (Voidbound Berserker)
> *"Full-body 3D boss character, 3.2 meters tall towering corrupted warrior, heavy cracked obsidian armor with glowing crimson magma/void veins, menacing horned helmet, asymmetrical spiked pauldrons, holding a massive brutal greatsword, T-pose, dark fantasy Soulsborne art style, high-poly PBR game asset."*

### Prompt 3: Audio SFX Generation (Sekiro Deflect Clang)
> *"High-frequency metallic blade clash, sharp katana deflection with crisp ringing transient, Sekiro parry sound effect, clean audio, zero reverb tail, high dynamic range, punchy combat sound design."*
