namespace Quantum.Shootball
{
    using Photon.Deterministic;
    using Quantum;
    using UnityEngine.Scripting;


    /// <summary>
    /// Every time a new player is added to the simulation this class creates the corresponding character.
    /// </summary>
    [Preserve]
    public unsafe class PlayerSpawnSystem : SystemSignalsOnly, ISignalOnPlayerAdded
    {
        public void OnPlayerAdded(Frame f, PlayerRef player, bool firstTime)
        {
            // Get the runtime player data
            var data = f.GetPlayerData(player);
            
            // Get the prototype
            var entityPrototype = f.FindAsset<EntityPrototype>(data.PlayerAvatar);

            // Create the new player entity
            var entity = f.Create(entityPrototype);

            // Add the player link
            f.Add(entity, new PlayerLink() { PlayerRef = player });

            // Set position
            if (f.Unsafe.TryGetPointer<Transform3D>(entity, out var t))
            {
                t->Position = FPVector3.Up * 2;
                t->Teleport(f, t->Position);
            }

        }
    }
}
