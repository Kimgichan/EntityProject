using Unity.Collections;
using Unity.Entities;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public struct MoveComponent : IComponentData
{
    public Vector3 dir;
}
