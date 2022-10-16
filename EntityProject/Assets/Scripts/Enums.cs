using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    [Flags]
    public enum EntityState
    {
        AllOff = 0,
        Push = 1 << 0,
        AllOn = int.MaxValue
    }
}
