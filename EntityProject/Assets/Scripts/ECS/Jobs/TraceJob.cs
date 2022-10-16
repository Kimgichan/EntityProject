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
public partial class TraceSystem : SystemBase
{
    [BurstCompile]
    protected override void OnUpdate()
    {
        if (GameManager.Instance == null || GameManager.Instance.GameController == null) return;

        var traceJobHandler = new TraceJob { deltaTime = UnityEngine.Time.deltaTime, dest = GameManager.Instance.GameController.player.transform.position }.Schedule();
    }
}

[BurstCompile]
#pragma warning disable CS0282
public partial struct TraceJob : IJobEntity
#pragma warning restore CS0282
{
    public float deltaTime;
    public Vector3 dest;

    [BurstCompile]
    void Execute(in Translation translation, in EnemyComponent enemyComponent,
        in StateComponent stateComponent, ref MoveComponent moveComponent)
    {
        if (stateComponent.Contains(Enums.EntityState.Push)) return; 

        Vector3 start = translation.Value;
        //Vector3 dest = enemyComponent.target.position;

        Vector3 force = dest - start;
        moveComponent.dir = (moveComponent.dir + force.normalized).normalized;
    }
}
