using Unity.Collections;
using Unity.Entities;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;



public partial class MoveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        #region
        //var deltaTime = UnityEngine.Time.deltaTime;
        //Entities.ForEach((ref Translation translation, ref MoveSpeedComponent moveSpeed) =>
        //{
        //    translation.Value.y += 1f * deltaTime * moveSpeed.speed;
        //}).Schedule();
        #endregion
        var moveJobHandler = new MoveJob { DeltaTime = UnityEngine.Time.deltaTime }.Schedule();
    }

}

[BurstCompile]
#pragma warning disable CS0282 // partial ����ü�� ���� ���𿡼� �ʵ� �� ������ ���ǵǾ� ���� �ʽ��ϴ�.
public partial struct MoveJob : IJobEntity
#pragma warning restore CS0282 // partial ����ü�� ���� ���𿡼� �ʵ� �� ������ ���ǵǾ� ���� �ʽ��ϴ�.
{
    public float DeltaTime;

    [BurstCompile]
    void Execute(ref Translation translation, ref MoveSpeedComponent moveSpeed)
    {
        translation.Value.y += 1f * DeltaTime * moveSpeed.speed;
    }
}