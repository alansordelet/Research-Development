🧩 How it works — PortalScript.cs 🔧

Goal: fake long/short tunnels by teleporting the player across a portal plane while keeping movement feeling continuous.

🎯 Inputs

playerPos → player root transform

recieverInside / recieverOutside → anchors (yes, spelled like in code)

InTunnelManager.instance.inTunnel → picks which receiver to use

Portal plane normal = transform.up

🚪 When it fires

We watch overlap (playerisOverlapping) via OnTriggerEnter/Exit on "Player".

Each LateUpdate we test the side of the plane:
dot = Vector3.Dot(transform.up, playerPos.position - transform.position)
👉 Teleport when dot < 0 (player crossed the portal plane).

🧭 Which side to send you to

inTunnel == false → use recieverInside

inTunnel == true → use recieverOutside

🔄 Keep facing “forward” (yaw remap)

We compute a yaw delta from entry → receiver and add a flip so the direction feels natural:

rotationdiff = -Quaternion.Angle(entry.rotation, receiver.rotation) + 180f

Apply: playerPos.Rotate(Vector3.up, rotationdiff)

📦 Keep your relative offset

We preserve your position relative to the portal, then re-express it at the receiver:

portalToPlayer = playerPos.position - entry.position

positionOffset = Quaternion.Euler(0, rotationdiff, 0) * portalToPlayer

playerPos.position = receiver.position + positionOffset

🛟 Safety clamp

If the remap drifts too far:
if (Distance(playerPos, receiver) > 10f) → snap to receiver.position

📐 Diagonal / vertical tunnels

Just rotate the portal and both receivers so their local axes match your tunnel direction. The same logic works—no extra code.

🧠 TL;DR

✅ Overlap ✔️ plane-cross (dot < 0) ✔️ pick receiver by inTunnel ✔️ yaw remap +180° ✔️ relative offset ✔️ clamp if > 10f.

❗ Uses transform.up as the portal plane normal—align your mesh/pivot accordingly.

Bonus: See CameraDistortion.cs 🎥 for speed-based FOV & _DistortionAmount post, and the hyperbolic 🧪 experiment files for Poincaré-disc explorations.
