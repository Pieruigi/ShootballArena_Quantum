using Photon.Deterministic;
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
            // Get the character controller
            var cc = f.Unsafe.GetPointer<CharacterController3D>(entity);

            // Create a new cc config asset
            var config = AssetObject.Create<CharacterController3DConfig>();
            // Add the new asset to the dynamic database
            f.AddAsset(config);
            // Get the specs asset
            var specs = f.FindAsset(Specs);
            // Initialize values
            config.MaxSpeed = specs.MaxSpeed;
            config.BaseJumpImpulse = specs.JumpForce;
            config.Acceleration = specs.Acceleration;
            // Init the character controller with the new config 
            cc->Init((FrameThreadSafe)f, config);
            Debug.Log($"JumpForce:{f.FindAsset(cc->Config).BaseJumpImpulse}");
            // Set other values
            CurrentStamina = specs.MaxStamina;
        }
    }
}
