namespace Quantum.Shootball
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;
    using Quantum;
    using UnityEditor.iOS;
    using UnityEngine;


    /// <summary>
    /// Deals with the aim system.
    /// Run it after the PlayerMovementSystem.
    /// </summary>
    [Preserve]
    public unsafe class PlayerAimSystem : SystemMainThreadFilter<PlayerAimSystem.Filter>, ISignalOnCharacterSpawned
    {
        

        public override void Update(Frame f, ref Filter filter)
        {
            // Get the PlayerLink
            var link = f.Get<PlayerLink>(filter.Aim->CharacterRef);

            // Get the player input
            var input = f.GetPlayerInput(link.PlayerRef);

            Debug.Log($"Pitch:{input->AimDirection}");
        }

        public struct Filter
        {
            public EntityRef Entity;
            public Transform3D* Transform;
            public PlayerAim* Aim;
        }

        public void OnCharacterSpawned(Frame f, EntityRef entity)
        {
            Debug.Log($"RunTimeConfig.PlayerAim:{f.RuntimeConfig.PlayerAimEntityPrototype}");

            // Create the entity
            var aim = f.Create(f.RuntimeConfig.PlayerAimEntityPrototype);

            // Get the PlayerAim
            var aimComp = f.Unsafe.GetPointer<PlayerAim>(aim);

            // Link the character
            aimComp->CharacterRef = entity;

        }
    }
}
