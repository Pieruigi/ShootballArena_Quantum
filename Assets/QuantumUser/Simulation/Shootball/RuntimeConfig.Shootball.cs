using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quantum
{
    public partial class RuntimeConfig
    {
        public AssetRef<Quantum.Shootball.GameMode> GameMode;

        partial void DumpUserData(ref String dump)
        {
        }
    }
}
