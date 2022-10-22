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


public partial class DestroySystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref HPComponent hpcomp) =>
        {
            if(hpcomp.hp <= 0)
            {
                World.DefaultGameObjectInjectionWorld.EntityManager.DestroyEntity(entity);
            }
        });
    }
}


