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

public partial class CollisionSystem : SystemBase
{
    public static NativeList<EntityKeyComponent> keys;
    public static NativeList<Translation> transArray;
    public static NativeList<CollisionComponent> collisions;
    public static System.Random random;


    protected override void OnCreate()
    {
        keys = new NativeList<EntityKeyComponent>(0, Allocator.Persistent);
        transArray = new NativeList<Translation>(0, Allocator.Persistent);
        collisions = new NativeList<CollisionComponent>(0, Allocator.Persistent);
        random = new System.Random();
    }
    protected override void OnDestroy()
    {
        keys.Dispose();
        transArray.Dispose();
        collisions.Dispose();
    }

    protected override void OnStopRunning()
    {
        keys.Clear();
        transArray.Clear();
        collisions.Clear();
    }

    protected override void OnUpdate()
    {
        EntityQuery query = GetEntityQuery(typeof(Translation), typeof(EntityKeyComponent), typeof(CollisionComponent));

        NativeList<EntityKeyComponent>.ParallelWriter keysWriter = keys.AsParallelWriter();
        NativeList<Translation>.ParallelWriter transArrayWriter = transArray.AsParallelWriter();
        NativeList<CollisionComponent>.ParallelWriter collisionsWriter = collisions.AsParallelWriter();

        keys.Clear();
        transArray.Clear();
        collisions.Clear();

        var count = query.CalculateEntityCount();
        if (keys.Capacity < count)
        {
            keys.Capacity = count;
        }
        if (transArray.Capacity < count)
        {
            transArray.Capacity = count;
        }
        if (collisions.Capacity < count)
        {
            collisions.Capacity = count;
        }


        //keys = query.ToComponentDataArrayAsync<EntityKeyComponent>(Allocator.TempJob, out JobHandle keyHandle);
        //transArray = query.ToComponentDataArrayAsync<Translation>(Allocator.TempJob, out JobHandle transHandle);
        //collisions = query.ToComponentDataArrayAsync<CollisionComponent>(Allocator.TempJob, out JobHandle collisionHandle);

        //var collisionHandle = new CollisionJob { keyComps = keys, translations = transArray }.Schedule(dep);

        Entities.ForEach((ref Translation position, ref EntityKeyComponent keyComp, ref CollisionComponent collisionComp) =>
        {
            keys.Add(keyComp);
            transArray.Add(position);
            collisions.Add(collisionComp);
        }).Schedule();


        Entities.ForEach((ref Translation position, ref StateComponent stateComp, ref EntityKeyComponent entityKeyComp, ref MoveComponent moveComponent, ref CollisionComponent collisionComp) =>
        {
            //if (GameManager.Instance == null || GameManager.Instance.GameController == null) return;

            //var GC = GameManager.Instance.GameController;

            Vector3 rotVec = Vector3.zero;
            for (int i = 0, icount = keys.Length; i<icount; i++)
            {
                var keyComp = keys[i];
                if (entityKeyComp.key == keyComp.key) continue;

                Vector3 targetPos = position.Value;
                Vector3 crashPos = transArray[i].Value;

                Vector3 force = targetPos - crashPos;
                float dist = force.magnitude;

                if (dist < collisionComp.radius + collisions[i].radius)
                {
                    //if(random.Next(0, 100)%2 == 0)
                    //{
                    //    rotVec = (Quaternion.AngleAxis(90f, -Vector3.forward) * Quaternion.FromToRotation(Vector3.forward, moveComponent.dir)) * Vector3.forward;
                    //}
                    //else
                    //{
                    //    rotVec = (Quaternion.AngleAxis(-90f, -Vector3.forward) * Quaternion.FromToRotation(Vector3.forward, moveComponent.dir)) * Vector3.forward;
                    //}

                    rotVec += force;
                }
            }

            if (rotVec == Vector3.zero) return;

            rotVec.Normalize();
            var forVec = moveComponent.dir + rotVec * (float)random.Next(10, 81) * 0.01f;
            moveComponent.dir = forVec.normalized;
        }).Schedule();

        //keys.Dispose(dep);
        //transArray.Dispose(dep);
        //collisions.Dispose(dep);
    }
}


[BurstCompile]
#pragma warning disable CS0282
public partial struct CollisionJob : IJobEntity
#pragma warning restore CS0282
{
    public NativeArray<EntityKeyComponent> keyComps;
    public NativeArray<Translation> translations;

    [BurstCompile]
    void Execute(in Translation translation, in StateComponent stateComp,
        in EntityKeyComponent entityKeyComp, ref MoveComponent moveComp)
    {
        for(int i = 0, icount = keyComps.Length; i<icount; i++)
        {
            var keyComp = keyComps[i];
            if (entityKeyComp.key == keyComp.key) continue;
        }
    }
}
