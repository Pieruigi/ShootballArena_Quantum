namespace Quantum.Shootball
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;
    using Quantum;
    using System;
    using static UnityEngine.EventSystems.EventTrigger;

    [Preserve]
    public unsafe class PlayerControllerSystem : SystemMainThreadFilter<PlayerControllerSystem.Filter>
    {
        public struct Filter
        {
            public EntityRef Entity;
            public CharacterController3D* CharacterController;
            public Transform3D* Transform;
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

            FP speed = 5;
            FP acceleration = 8;

            if (input->Jump.WasPressed)
                cc->Jump(f);

            cc->Move(f, filter.Entity, input->Direction.XOY);
        }

        
    }
}
