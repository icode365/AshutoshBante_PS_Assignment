# Gravity Shift Prototype

A third-person gravity-based gameplay prototype built in Unity.

The player navigates a constrained level by collecting targets while dynamically changing gravity direction to traverse walls, ceilings, and different surfaces.

---

# Core Gameplay

## Objective

Collect all target boxes before the timer expires.

The player can:

- Move using third-person controls
- Jump
- Change gravity direction using arrow keys
- Apply gravity shifts dynamically
- Traverse walls and ceilings
- Predict out-of-bounds danger zones

The game ends when:

- All collectibles are gathered (Win)
- Timer reaches zero (Lose)
- Player exits gameplay bounds (Lose)

---

# Controls

| Action                   | Input      |
| ------------------------ | ---------- |
| Move                     | WASD       |
| Camera Look              | Mouse      |
| Jump                     | Space      |
| Select Gravity Direction | Arrow Keys |
| Apply Gravity            | Enter      |

---

# Playthrough

[![Watch the video](Docs/Preview.gif)](https://youtu.be/nnRC-L-T9hc)

---

# Gameplay Features

## Gravity Shift System

The player can rotate gravity in 90° increments.

Gravity transitions are:

- Relative to current gravity orientation
- Constrained to world cardinal axes
- Applied manually using Enter

Supported traversal:

- Floor
- Walls
- Ceiling

---

## Third-Person Character Controller

Custom controller implementation using Unity CharacterController.

Features:

- Camera-relative movement
- Gravity-relative movement projection
- Jumping
- Smooth rotation
- Gravity alignment
- Ground detection using SphereCast

---

## Dynamic Ground Detection

Custom grounding system replaces CharacterController.isGrounded.

Uses:

- SphereCast
- Gravity-relative checks
- Surface normal detection

Supports:

- Sideways gravity
- Ceiling traversal
- Arbitrary orientation movement

---

## Collectible Queue System

Collectibles are activated sequentially.

Behavior:

1. Current target becomes active
2. Player collects target
3. Next target becomes active
4. UI updates
5. Repeat until completion

---

## Target Direction System

The HUD displays target direction feedback.

Features:

- Arrow appears when target is off-screen
- Box indicator appears when target is visible
- Dynamic screen-space tracking

---

## Out-of-Bounds Prediction

The system predicts dangerous proximity to gameplay boundaries.

Features:

- Detects closest arena boundary
- Displays warning decal
- Decal scales based on proximity
- Boundary trigger respawns player

---

## Gameplay UI

Displays:

- Collected item count
- Total collectibles
- Win state
- Lose state
- Lose reason

UI feedback includes:

- Collection text animation
- Result panels
- Dynamic updates

---

# Architecture

## Main Systems

### GameManager

Handles:

- Gameplay flow
- Countdown lifecycle
- Player spawning
- Win/Lose state management

---

### GlobalEventHandler

Centralized gameplay event system.

Events:

- PlayerSpawned
- CountdownStarted
- CountdownEnded
- GravityChanged
- BoxCollected
- PlayerOutOfBounds

---

### PlayerInputHandler

Handles:

- Input Action Map
- Movement input
- Jump input
- Gravity selection
- Camera look input

Uses Unity Input System.

---

### GravityController

Handles:

- Current gravity direction
- Pending gravity direction
- Gravity application
- Gravity preview visualization

---

### PlayerCharacterController

Handles:

- Character movement
- Gravity velocity
- Jumping
- Visual rotation
- Gravity alignment
- Final movement application

Important: Only ONE CharacterController.Move() call is executed per frame.

---

### GroundDetector

Handles:

- SphereCast grounding
- Ground normal detection
- Surface hit information

---

### BoxCollectionGameplayHandler

Handles:

- Collectible queue
- Current target activation
- Collection progression
- Gameplay completion

---

### TargetDirectionSolver

Determines:

- Whether target is visible
- Screen position of target
- Off-screen direction

---

### TargetDirectionVisualizer

Handles HUD visualization.

Displays:

- Direction arrow
- On-screen target indicator

---

### GameplayUIController

Handles:

- Gameplay UI updates
- Collection count
- Win/Lose panels
- UI animations

---

# Scene Hierarchy Example

```plaintext
GameManager

Canvas
    GameplayUIController
    CollectionText
    ResultPanel

PlayerSpawnPoint

Player
    CharacterController
    PlayerInputHandler
    GravityController
    GroundDetector
    PlayerCharacterController
    PlayerAnimationHandler

    CameraRig
        ThirdPersonCameraController
        Main Camera

    Visuals
        Mesh
        Animator
        GravityPreviewHologram
```

---

# Technical Notes

## Why Custom Grounding Was Used

Unity CharacterController.isGrounded does not behave reliably with arbitrary gravity.

A custom SphereCast-based grounding system was implemented to support:

- Wall traversal
- Ceiling traversal
- Dynamic gravity orientation

---

# Known Limitations

- Camera remains world-up stabilized
- CharacterController is not truly arbitrary-gravity compatible

---

# Technologies Used

- Unity
- C#
- Unity Input System
- CharacterController
- TextMeshPro

---