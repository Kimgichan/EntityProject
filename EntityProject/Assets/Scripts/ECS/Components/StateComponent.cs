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

using Enums;

public struct StateComponent : IComponentData
{
    private EntityState entityState;

    public EntityState EntityState => entityState;

    public StateComponent(EntityState entityState)
    {
        this.entityState = entityState;
    }

    public void TurnOff(EntityState state)
    {
        entityState &= ~state;
    }
    public void TurnOn(EntityState state)
    {
        entityState |= state;
    }

    public bool Contains(EntityState state)
    {
        return (entityState & state) == state;
    }
}
