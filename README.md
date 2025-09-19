🌀 Non-Euclidean Tunnel Demo (Unity)










Prototype that fakes long/short tunnels using a portal trigger, yaw remap (+180°), and relative position. Works for straight, diagonal, and vertical tunnel illusions via rotation. Includes speed-based camera distortion and a hyperbolic geometry experiment.

⚠️ Demo, not a framework — minimal, self-contained, meant for learning.

✨ Features

🚪 Outside ↔ Inside teleports with overlap gate

🧭 Orientation continuity: yaw rotate + 180° flip

📐 3D variants: diagonal / vertical tunnels (rotate portal & receivers)

🎥 Camera FX: FOV + post-shader distortion scaled by speed

🧪 Hyperbolic lab: Poincaré-disc flavored experiments (Complex math, Möbius ops)

🧩 How it works (matches this repo)
PortalScript.cs 🔧 (C#)

Trigger gate: playerisOverlapping via OnTriggerEnter/Exit ("Player" tag)

Plane crossing:
dot = Vector3.Dot(transform.up, playerPos - transform.position) → teleport when dot < 0

Route selection:

InTunnelManager.instance.inTunnel == false → recieverInside

true → recieverOutside

Yaw remap (+180°):

float rotationdiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
rotationdiff += 180f;
playerPos.Rotate(Vector3.up, rotationdiff);


Relative offset:

Vector3 portalToPlayer = playerPos.position - transform.position;
Vector3 positionOffset = Quaternion.Euler(0f, rotationdiff, 0f) * portalToPlayer;
playerPos.position = receiver.position + positionOffset;


🛟 Safety clamp: if Distance(player, receiver) > 10f → snap to receiver

📏 Portal normal: uses transform.up (align your mesh/pivot accordingly)

✅ Diagonal/Vertical tunnels: rotate the portal and both receivers so their local axes match your tunnel direction. Same code path, no extras.

InTunnelManager.cs 🗺️

Singleton (instance) with bool inTunnel

Uses two colliders (colliderSmallTunnel, colliderBigTunnel) and bounds.Contains(player.position)

Toggles tunnel mesh: bigTunnel.SetActive(...)

CameraDistortion.cs 🎥

UpdateDistortion(speed) → lerps FOV base → maxFOV, sets shader _DistortionAmount

Built-in RP OnRenderImage post; for URP/HDRP use a Render Feature / Custom PP

Complex.cs + HyperbolicTileMapGenerator.cs 🧪

Complex ops (+, −, ×, ÷, Abs())

Möbius helpers, circle from 3 points, intersections

Generates editable polygon points; scaling/mapping experiments (WIP)

🧠 Tips & Gotchas

🎯 Align portal up to the plane normal (uses transform.up in dot test)

🧲 Keep the trigger thin & centered to avoid immediate re-entry

🧰 For vertical illusions, ensure receiver forward/up guide the desired post-warp facing

🫥 Hide snaps with a corner, light beat, VFX puff, or quick curve after the seam

🧱 Using Rigidbody? This demo doesn’t remap velocity; prefer CharacterController here

🐞 Known Limits

🔁 Yaw-only (no pitch/roll remap)

🕳️ No recursive views/mirrors (pure teleport + rotation)

⏱️ No debounce timer (relies on overlap + thin trigger)

🧮 Hyperbolic module is experimental (viz/prototype only)
