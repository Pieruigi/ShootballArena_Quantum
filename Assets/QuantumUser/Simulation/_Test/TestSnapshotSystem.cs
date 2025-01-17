using Photon.Deterministic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Quantum
{
    [Preserve]
    public unsafe class TestSnapshotSystem : SystemMainThreadFilter<TestSnapshotSystem.Filter>
    {
        public override void Update(Frame f, ref Filter filter)
        {
            

            float max = 5;
            if(filter.Ball->amount >= 5)
            {
                filter.Ball->amount = 0;
                filter.Ball->forward = !filter.Ball->forward;
            }

            // Move
            FP speed = 2;
            FPVector3 dir = filter.Ball->forward ? filter.Transform->Forward : filter.Transform->Back;
            FP amount = speed * f.DeltaTime;
            filter.Transform->Position += dir * amount;
            filter.Ball->amount += amount;
        }

        public struct Filter
        {
            public EntityRef Entity;
            public TestBall* Ball;
            public Transform3D* Transform;
        }
    }
}
