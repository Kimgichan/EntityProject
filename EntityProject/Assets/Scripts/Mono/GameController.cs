using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Burst;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public class GameController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private EntitySpawner entitySpawner;
    [SerializeField] private float spawnDistance;
    [SerializeField] private int spawnCount;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float moveSpeed;

    public EntitySpawner EntitySpawner => entitySpawner;
    public float MoveSpeed => moveSpeed;


    IEnumerator Start()
    {
        while(GameManager.Instance == null)
        {
            yield return null;
        }

        GameManager.Instance.GameController = this;

        StartRound();
    }

    private void StartRound()
    {
        StartCoroutine(StartRoundCor());
    }

    private IEnumerator StartRoundCor()
    {
        var wait = new WaitForSeconds(spawnDelay);

        int count = spawnCount;
        while(count > 0)
        {
            yield return wait;

            EntitySpawner.CreateEntities(1, SettingEntity);
            count -= 1;
        }
    }

    private void SettingEntity(Entity entity)
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.AddComponentData(entity, new EnemyComponent() { enemyGroup = 0 });

        var quaternion = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));
        var dir = quaternion * new Vector3(0f, 1f, 0f);
        dir *= spawnDistance;

        entityManager.AddComponentData(entity, new Translation { Value = new Unity.Mathematics.float3(dir) });
        entityManager.AddComponentData(entity, new MoveComponent { dir = Vector3.zero });
        entityManager.AddComponentData(entity, new StateComponent(Enums.EntityState.AllOff));
    }
}
