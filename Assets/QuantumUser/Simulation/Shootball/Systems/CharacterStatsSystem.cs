namespace Quantum.Shootball
{
    using Photon.Deterministic;
    using UnityEngine.Scripting;
    using Quantum;
    using System.Diagnostics;

    [Preserve]
    public unsafe class CharacterStatsSystem : SystemMainThreadFilter<CharacterStatsSystem.Filter>, ISignalOnCharacterSpawned
    {
        
        public override void Update(Frame f, ref Filter filter)
        {
        }

        public struct Filter
        {
            public EntityRef Entity;
            public CharacterStats* Stats;
        }

        public void OnAdded(Frame f, EntityRef entity, CharacterStats* component)
        {
            UnityEngine.Debug.Log($"Added component {component->ToString()}");
            
        }

        public void OnCharacterSpawned(Frame f, EntityRef entity)
        {
            // Get the CharacterStats component
            if(f.Unsafe.TryGetPointer<CharacterStats>(entity, out var stats))
            {
                // Initialize character controller and other stats
                stats->Initialize(f, entity);
            }

            //UnityEngine.Debug.Log("Stats component:" + f.Unsafe.GetPointer<CharacterStats>(player)->ToString());
        }
    }
}
