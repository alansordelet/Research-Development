🌀 Non-Euclidean Tunnels in Unity

Fake long ↔ short tunnels using clever rotation, relative transforms, and portal-like remapping—no custom render pipelines, no wizardry. Just neat math and scene tricks. 🎮✨

👀 What is this?

Create spaces that feel bigger (or smaller) than they are:

🚪 Seamless tunnel loops (walk forever in 10 meters)

🔁 Short-to-long swaps via rotation/offset

🧭 Perspective cheats that preserve player orientation

Use cases: puzzlers, liminal spaces, “impossible” labs, trippy dungeons.

🧩 How it works (high level)

Proxy Volumes: Entering a trigger re-positions the player relative to an exit anchor.

Rotation Remap: We rotate the player by the delta between entry/exit anchors so forward keeps feeling “forward”.

Distance Illusion: World chunks repeat or swap while maintaining continuous motion and sound.

🚀 Quick Start

Open in Unity 2022+.

Drop TunnelPortal.prefab (has EntryAnchor + ExitAnchor + Trigger).

Assign your Player (or CharacterController) to PortalRemapper.

Press Play → walk through the entry… and keep walking. 😈

📦 Folder Map
/Assets
  /NonEuclid
    /Prefabs
      TunnelPortal.prefab
    /Scripts
      PortalRemapper.cs
      RelativeWarp.cs
    /Demo
      Scene_Tunnel.unity

🛠️ Core Scripts

PortalRemapper.cs – repositions & reorients the player when crossing the trigger.

using UnityEngine;

public class PortalRemapper : MonoBehaviour
{
    [SerializeField] Transform entryAnchor;   // where you enter
    [SerializeField] Transform exitAnchor;    // where you appear
    [SerializeField] Transform target;        // player root

    void OnTriggerEnter(Collider other)
    {
        if (other.transform != target) return;

        // rotation delta (exit * inverse(entry))
        Quaternion deltaRot = exitAnchor.rotation * Quaternion.Inverse(entryAnchor.rotation);

        // position expressed relative to entry
        Vector3 local = Quaternion.Inverse(entryAnchor.rotation) * (target.position - entryAnchor.position);

        // remap to exit space
        Vector3 newWorldPos = exitAnchor.position + (deltaRot * local);

        // apply
        target.rotation = deltaRot * target.rotation;
        target.position  = newWorldPos;
    }
}


RelativeWarp.cs – optional “soft warp” to avoid a visible pop (lerps a few frames).

using UnityEngine;
using System.Collections;

public class RelativeWarp : MonoBehaviour
{
    public IEnumerator SmoothWarp(Transform t, Vector3 toPos, Quaternion toRot, float time = 0.06f)
    {
        Vector3  fromP = t.position;  Quaternion fromR = t.rotation;
        float a = 0f;
        while (a < 1f)
        {
            a += Time.unscaledDeltaTime / time;
            t.position = Vector3.Lerp(fromP, toPos, a);
            t.rotation = Quaternion.Slerp(fromR, toRot, a);
            yield return null;
        }
    }
}

🧠 Design Notes

Keep entry/exit anchors aligned to the tunnel direction for natural movement.

Hide swaps with lighting beats, SFX, or a tight curve.

Use multiple portals to chain long loops (“infinite” corridor).

Physics: remap root transform; child rigs/animators follow cleanly.

⚠️ Limits / Gotchas

Mirrors/true recursive views not included (this is transform trickery, not ray-traced portals).

Networked play needs server-authoritative warps.

NavMesh agents require off-mesh links at portals.
