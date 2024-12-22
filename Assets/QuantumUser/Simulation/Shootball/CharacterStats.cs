using Quantum.Shootball;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quantum
{
    public partial struct CharacterStats : IComponent
    {
        public unsafe void Initialize(Frame f, EntityRef entity)
        {
            // Load the character specs
            var specs = f.FindAsset<CharacterSpecs>(Specs);

            // Get the character controller
            if(f.Unsafe.TryGetPointer<CharacterController3D>(entity, out var cc))
            {
                // Retrieve the character controller config; modifying it at runtime ensures that changes do not affect other controllers
                var ccConfig = f.FindAsset<CharacterController3DConfig>(cc->Config);

                // Change values
                ccConfig.Acceleration = specs.Acceleration;
                ccConfig.BaseJumpImpulse = specs.JumpForce;
                ccConfig.MaxSpeed = specs.MaxSpeed;

                // Change value on stats
                CurrentStamina = specs.MaxStamina;
                SprintMultiplier = specs.SprintMultiplier;
            }

        }
    }
}
