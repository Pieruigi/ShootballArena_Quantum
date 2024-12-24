using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quantum;
namespace Quantum.Shootball
{
    public unsafe class PlayerEntityView : QuantumEntityView
    {
        [SerializeField]
        Transform cameraTarget;

        QuantumGame game;
        PlayerInput localInput;
        bool localPlayer = false;

        public void OnInstantiated(QuantumGame game)
        {
            // Cache the quantum game
            this.game = game;

            // Get the last verified frame
            var frame = game.Frames.Verified;

            // Check if this is the local player
            if(frame.Unsafe.TryGetPointer<PlayerLink>(entityRef:EntityRef, out var link) && game.PlayerIsLocal(link->PlayerRef))
            {
                // Set the camera taget
                Camera.main.GetComponent<PlayerCamera>().Target = cameraTarget;

                // Get the local input
                localInput = FindFirstObjectByType<PlayerInput>();

                // Set local player trye
                localPlayer = true;
            }
            
        }

        protected override void ApplyTransform(ref UpdatePositionParameter param)
        {
            base.ApplyTransform(ref param);

            if (!localPlayer) return;

            // Get the last predicted frame (this frame is further than the verified frame)
            var frame = game.Frames.Predicted;
            // The the aim component
            var aim = frame.Get<PlayerAim>(EntityRef);

            // Adjust pitch and jaw by adding the cached values to the verified values
            transform.rotation = Quaternion.Euler(0, aim.Yaw.AsFloat + localInput.YawDelta, 0);
            cameraTarget.localRotation = Quaternion.Euler(aim.Pitch.AsFloat + localInput.PitchDelta, 0, 0);
        }


    }

}

