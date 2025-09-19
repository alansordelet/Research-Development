# 🌀 Non-Euclidean Tunnel Demo (Unity)

Prototype that fakes long/short tunnels using a portal trigger, yaw remap (+180°), and relative position.  
Works for straight, diagonal, and vertical tunnel illusions via rotation. Includes speed-based camera distortion, stencil shading tricks, a “magic box” visual effect, and a small hyperbolic geometry experiment.

⚠️ Demo, not a framework — minimal, self-contained, meant for learning.

---

## ✨ Features

- 🚪 Outside ↔ Inside teleports with overlap gate  
- 🧭 Orientation continuity  
- 📐 3D variants: diagonal / vertical tunnels (rotate portal & receivers)  
- 🎥 Camera FX: FOV + post-shader distortion scaled by speed  
- 🎭 Stencil shading for controlled visibility and seamless transitions  
- 📦 “Magic Box” illusion — appearance changes depending on viewing angle  
- 🧪 Hyperbolic lab: Poincaré-disc flavored experiments (Complex math, Möbius ops)  

---

## 🔧 How it works

🎭 Magic Box (Stencil Illusion)

To create the “Magic Box” effect (different appearance depending on view side):
-Modified Unity’s Forward Rendering pipeline with a custom shader.
-Used the stencil buffer to mark geometry with reference values.
-Set different stencil tests (Comp Equal, Comp NotEqual) for front vs back facing.
-Depending on which side the player looks from, the box either shows or hides contents.

Shader snippet (simplified):
  ```
Stencil {
    Ref 1             // Reference value
    Comp Equal        // Only render if stencil == Ref
    Pass Replace      // Replace stencil with Ref on pass
}
  ```

This allowed the box to change visuals based on camera direction, faking an impossible object illusion.

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
  ```
  
Yaw remap (+180°):
```
float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
rotationDiff += 180f;
playerPos.Rotate(Vector3.up, rotationDiff);
  ```
 
Relative offset:
 ```
Vector3 portalToPlayer = playerPos.position - transform.position;
Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
playerPos.position = receiver.position + positionOffset;
  ```
📏 Portal normal: uses transform.up (align mesh/pivot accordingly)

🐞 Known Limits

🕳️ No recursive views/mirrors (pure teleport + rotation)

⏱️ No debounce timer (relies on overlap + thin trigger)

🧮 Hyperbolic module = experimental, viz/prototype only
