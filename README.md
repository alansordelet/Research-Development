# 🌀 Non-Euclidean Illusions Demo (Unity)

A Unity playground of impossible spaces — portal tunnels, magic box illusions, stencil shading tricks, speed-based camera warps, and hyperbolic geometry experiments.

⚠️ Demo, not a framework — minimal, self-contained, meant for learning.

---

## ✨ Features

- 🚪 **Non-Euclidean tunnels:** outside size doesn’t match inside length  
   - Looks long from outside → feels short once inside  
   - Looks short from outside → feels unexpectedly long once inside  
- 🧭 **Orientation continuity:** smooth yaw remap (+180°) for seamless entry/exit  
- 📐 **3D variants:** supports straight, diagonal/vertical tunnel illusions by rotating portals & receivers  
- 🎥 **Camera FX:** FOV + post-shader distortion scaled by speed  
- 🎭 **Stencil shading:** controlled visibility for seamless transitions  
- 📦 **“Magic Box” illusion:** object changes appearance depending on view direction  
- 🧪 **Hyperbolic lab:** Poincaré-disc inspired experiments (complex math + Möbius transforms)  

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

### `PortalScript.cs`

- **Trigger gate:** activates when the player enters/exits the portal trigger
  
 ```
playerIsOverlapping = true/false via OnTriggerEnter/Exit("Player");
 ```
- **Plane crossing check:** determines when the player fully passes through the portal plane
 ```
float dot = Vector3.Dot(transform.up, player.position - transform.position);
if (dot < 0) Teleport();
 ```
- **Non-Euclidean tunnel logic:**
Chooses whether the player is entering the long or short version of the tunnel.
 ```
if (!InTunnelManager.instance.inTunnel) 
    receiver = receiverInside;   // tunnel feels shorter than outside
else 
    receiver = receiverOutside;  // tunnel feels longer than outside
 ```
- **Yaw remap (+180°):** ensures orientation stays continuous after teleport
 ```
float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
rotationDiff += 180f;
player.Rotate(Vector3.up, rotationDiff);
 ```
- **Relative position offset:** keeps player position consistent relative to the portal
 ```
Vector3 portalToPlayer = player.position - transform.position;
Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
player.position = receiver.position + positionOffset;
 ```

📏 Portal normal: uses transform.up to decide crossing direction (align mesh pivot properly)

🐞 Known Limits

🕳️ No recursive views/mirrors (pure teleport + rotation)

⏱️ No debounce timer (relies on overlap + thin trigger)

🧮 Hyperbolic module = experimental, viz/prototype only
