# 🌀 Non-Euclidean Tunnel Demo (Unity)

Prototype that fakes long/short tunnels using a portal trigger, yaw remap (+180°), and relative position.  
Works for straight, diagonal, and vertical tunnel illusions via rotation. Includes speed-based camera distortion, stencil shading tricks, a “magic box” visual effect, and a small hyperbolic geometry experiment.

⚠️ Demo, not a framework — minimal, self-contained, meant for learning.

---

## ✨ Features

- 🚪 Outside ↔ Inside teleports with overlap gate  
- 🧭 Orientation continuity: yaw rotate + 180° flip  
- 📐 3D variants: diagonal / vertical tunnels (rotate portal & receivers)  
- 🎥 Camera FX: FOV + post-shader distortion scaled by speed  
- 🎭 Stencil shading for controlled visibility and seamless transitions  
- 📦 “Magic Box” illusion — appearance changes depending on viewing angle  
- 🧪 Hyperbolic lab: Poincaré-disc flavored experiments (Complex math, Möbius ops)  

---

## 🔧 How it works

**PortalScript.cs**  
- Trigger gate: `playerIsOverlapping` via `OnTriggerEnter/Exit ("Player" tag)`  
- Plane crossing:  
  ```
  dot = Vector3.Dot(transform.up, playerPos - transform.position);
  if (dot < 0) { /* teleport */ }
    ```
Route selection:

  ```
InTunnelManager.instance.inTunnel == false → receiverInside
true → receiverOutside
Yaw remap (+180°):

  ```
  ```
float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
rotationDiff += 180f;
playerPos.Rotate(Vector3.up, rotationDiff);
Relative offset:

  ```
  ```
Vector3 portalToPlayer = playerPos.position - transform.position;
Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
playerPos.position = receiver.position + positionOffset;
  ```
🛟 Safety clamp: if (Distance(player, receiver) > 10f) → snap to receiver

📏 Portal normal: uses transform.up (align mesh/pivot accordingly)

InTunnelManager.cs

Singleton with bool inTunnel

Uses two colliders (colliderSmallTunnel, colliderBigTunnel)

Toggles tunnel mesh: bigTunnel.SetActive(...)

CameraDistortion.cs

UpdateDistortion(speed) → lerps FOV base → maxFOV, sets shader _DistortionAmount

Built-in RP OnRenderImage post; for URP/HDRP use a Render Feature

Complex.cs + HyperbolicTileMapGenerator.cs

Complex ops (+, −, ×, ÷, Abs())

Möbius helpers, circle from 3 points, intersections

Generates editable polygon points for scaling/mapping experiments (WIP)

🧠 Tips & Gotchas
🎯 Align portal up to the plane normal (uses transform.up in dot test)

🧲 Keep the trigger thin & centered to avoid immediate re-entry

🧰 For vertical illusions, ensure receiver forward/up guide the post-warp facing

🫥 Hide snaps with a corner, VFX puff, or camera curve after the seam

🧱 Using Rigidbody? This demo doesn’t remap velocity; prefer CharacterController

🐞 Known Limits
🔁 Yaw-only (no pitch/roll remap)

🕳️ No recursive views/mirrors (pure teleport + rotation)

⏱️ No debounce timer (relies on overlap + thin trigger)

🧮 Hyperbolic module = experimental, viz/prototype only
