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

[BurstCompile]
public partial class EntityMoveSystem : SystemBase
{
    [BurstCompile]
    protected override void OnUpdate()
    {
        if (GameManager.Instance == null || GameManager.Instance.GameController == null) return;

        var moveJobHandler = new EntityMoveJob { speed = GameManager.Instance.GameController.MoveSpeed,
        deltaTime = UnityEngine.Time.deltaTime}.Schedule();
    }
}

[BurstCompile]
#pragma warning disable CS0282
public partial struct EntityMoveJob : IJobEntity
#pragma warning disable CS0282
{
    public float deltaTime;
    public float speed;

    [BurstCompile]
    void Execute(ref Translation translation, in StateComponent stateComponent, in MoveComponent moveComponent)
    {
        if (stateComponent.Contains(Enums.EntityState.Push)) return;

        translation.Value += (float3)(moveComponent.dir * speed * deltaTime);
    }
}