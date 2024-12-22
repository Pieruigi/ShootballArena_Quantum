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
    public unsafe class PlayerControllerSystem : SystemMainThreadFilter<PlayerControllerSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public CharacterController3D* CharacterController;
            public Transform3D* Transform;
            public CharacterStats* Stats;
            //public PlayerController* Controller;
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

            // Rotate 
            filter.Transform->Rotate(FPVector3.Up, input->AimDirection.X * f.DeltaTime * specs.RotationSpeed);

            // Compute movement direction
            var moveDirection = filter.Transform->Forward * input->Direction.Y + filter.Transform->Right * input->Direction.X;

            // Move
            cc->Move(f, filter.Entity, moveDirection.Normalized);
        }

        
    }
}
