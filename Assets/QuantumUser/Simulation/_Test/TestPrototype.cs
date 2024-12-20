using Photon.Deterministic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quantum.Prototypes
{
    public unsafe partial class TestPrototype
    {
        //public FP ValueA = 20;
        //public FP ValueB = 30;

        partial void MaterializeUser(Frame f, ref Test result, in PrototypeMaterializationContext context)
        {
            result.Prototype = context.ComponentPrototypeRef;
        }


    }
}
