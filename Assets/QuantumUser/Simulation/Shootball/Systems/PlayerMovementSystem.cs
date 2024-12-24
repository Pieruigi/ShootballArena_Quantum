namespace Quantum.Shootball
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;
    using Quantum;
    using System;
    using static UnityEngine.EventSystems.EventTrigger;
    using System.Diagnostics;
    using UnityEngine.UIElements;

    [Preserve]
    public unsafe class PlayerMovementSystem : SystemMainThreadFilter<PlayerMovementSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public CharacterController3D* CharacterController;
            public Transform3D* Transform;
            public CharacterStats* Stats;
            public PlayerAim* Aim;
        }

        public override void Update(Frame f, ref Filter filter)
        {
            Input* input = default;

            // Get the player link
            if(f.Unsafe.TryGetPointer<PlayerLink>(filter.Entity, out var link))
            {
                // Get the input
                input = f.GetPlayerInput(link->PlayerRef);

                // Update movement
                UpdateMovement(f, filter, input);
            }
        }

        private void UpdateMovement(Frame f, Filter filter, Input* input)
        {
            // Get character controller
            var cc = filter.CharacterController;

            // Move this to a characteer spec config file
            var specs = f.FindAsset(filter.Stats->Specs);

            // Jump
            if (input->Jump.WasPressed)
                cc->Jump(f);

            // Check if the player is sprinting
            if (input->Sprint.WasPressed)
                filter.Stats->SetSprinting(f, filter.Entity, true);
            else if (input->Sprint.WasReleased)
                filter.Stats->SetSprinting(f, filter.Entity, false);

            // Update aim component
            filter.Aim->Yaw += input->YawDelta;
            filter.Aim->Pitch += input->PitchDelta;

            // Rotate
            filter.Transform->Rotation = FPQuaternion.Euler(0, filter.Aim->Yaw, 0);

            // Compute movement direction
            var moveDirection = filter.Transform->TransformDirection(input->Direction.XOY);

            // Clamp direction magnitude eventually
            if(moveDirection.Magnitude > 1)
                moveDirection = moveDirection.Normalized;

            // Move
            cc->Move(f, filter.Entity, moveDirection);
                        
        }

        
    }
}
