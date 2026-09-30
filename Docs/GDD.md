# Game Design Document (GDD)
# Project: AETHERIUM: VOIDBOUND

## 1. High Concept
* **Title:** AETHERIUM: VOIDBOUND
* **Genre:** 3D Third-Person Character Action / Boss Duel (Souls-lite x Character Action)
* **Target Audience:** Fans of high-skill melee combat games (*Sekiro*, *Devil May Cry*, *Elden Ring*, *Hades*).
* **Setting:** The Shattered Astral Sanctum — a dark celestial arena surrounded by floating obsidian monoliths, glowing cosmic rifts, and ambient celestial dust.

---

## 2. Narrative Premise
You are **The Riftwalker**, an astral champion bound to the boundary between realities. To sever the encroaching void corrupting the astral realms, you must confront and defeat **Aethelgard, The Void Sentinel**—the fallen celestial guardian corrupted by dark void matter.

---

## 3. Core Combat Pillars

### 3.1 Movement & Traversal
* **Smooth 360° Movement:** Responsive analog/WASD directional motion relative to camera orientation.
* **Sprint & Stamina:** Press/Hold Sprint consuming stamina over time.
* **8-Way Directional Dodge Roll:**
  * **Duration:** 0.45s (approx. 27 frames at 60 FPS).
  * **Invulnerability Window (I-Frames):** Frames 2 to 14.
  * **Recovery / Endlag:** Frames 15 to 27 (vulnerable to punishment).

### 3.2 Combat Mechanics
* **3-Hit Light Attack Combo:**
  * Strike 1: Fast horizontal slash (Quick startup, low damage).
  * Strike 2: Diagonal upward slash (Medium damage).
  * Strike 3: Heavy thrust / finisher (High damage, knockback, long recovery).
* **Charged Heavy Attack:**
  * Hold attack button to charge up a devastating void-cleaving slash with extended range.
* **Parry & Block System:**
  * **Perfect Parry Window:** 0.15s (Frames 1–9 upon button press). Deflecting an attack completely negates damage, induces hit-stop on the attacker, and inflicts heavy **Posture Damage**.
  * **Regular Block:** Holding guard reduces physical damage by 70%, but drains player Stamina.
* **Posture / Stagger Meter:**
  * Both Player and Boss have a Posture bar.
  * Constant aggression and successful parries fill the enemy's Posture bar. When filled, the target enters a **Groggy/Staggered State**, opening an opportunity for a **Visceral Critical Strike**.

---

## 4. Boss Design: Aethelgard, The Void Sentinel

### Phase 1: The Celestial Duelist (100% - 50% HP)
* **Style:** Measured, elegant greatsword strikes.
* **Attack Moveset:**
  1. *Twin Crescent Slash:* 2-hit horizontal swipe. (Parryable).
  2. *Overhead Cleave:* Delayed heavy overhead smash. (High damage, dodge required).
  3. *Lunge Thrust:* Fast gap-closing thrust if player retreats too far.
  4. *Defensive Reposition:* Backstep into guard stance.

### Phase 2: The Void Berserker (50% - 0% HP)
* **Trigger:** Aethelgard unleashes a cosmic roar, infusing weapons with dark void fire.
* **New Moveset:**
  1. *Void Shockwave (AOE):* Plunges greatsword into ground, spawning a radiating shockwave that must be jumped or rolled through.
  2. *Astral Dash Combo:* Rapid 3-hit teleport dash slash across the arena.
  3. *Void Nova:* Channels an explosive burst if player stays too close for too long.

---

## 5. Visual & Sound Feedback ("Juice")
* **Hit-Stop (Microfreeze):** 0.06s time-scale drop (0.05x) on successful melee impacts.
* **Camera Shake:** Cinemachine Impulse tuned to attack weight (light combo = 0.2 intensity, heavy strike = 0.8 intensity).
* **Slash Particle Arcs:** Distinct glowing cyan/void purple ribbon trails following the blade edge.
* **Spark & Blood Impacts:** Directional particle bursts oriented toward the collision normal.
