using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class MoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref MoveSpeedComponent moveSpeed) =>
        {
            translation.Value.y += 1f * UnityEngine.Time.deltaTime * moveSpeed.speed;
        });
    }
}
