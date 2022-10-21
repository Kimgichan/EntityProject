using System;
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
    public static int keyCount;

    public GameObject player;
    [SerializeField] private EntitySpawner entitySpawner;
    [SerializeField] private float spawnDistance;
    [SerializeField] private int spawnCount;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float collisionRadius;
    private EntityManager entityManager;

    //public NativeList<Translation> transArray;
    //public NativeList<EntityKeyComponent> keys;
    //public NativeList<CollisionComponent> collisions;

    public EntitySpawner EntitySpawner => entitySpawner;
    public EntityManager EntityManager => entityManager;
    

    public float MoveSpeed => moveSpeed;


    IEnumerator Start()
    {
        while(GameManager.Instance == null)
        {
            yield return null;
        }

        GameManager.Instance.GameController = this;
        //entities = new NativeList<Entity>(spawnCount, Allocator.Persistent);

        //transArray = new NativeList<Translation>(spawnCount, Allocator.Persistent);
        //keys = new NativeList<EntityKeyComponent>(spawnCount, Allocator.Persistent);
        //collisions = new NativeList<CollisionComponent>(spawnCount, Allocator.Persistent);
        StartRound();
    }

    private void OnDestroy()
    {
        //entities.Dispose();
        //transArray.Dispose();
        //keys.Dispose();
        //collisions.Dispose();
    }

    private void StartRound()
    {
        StartCoroutine(StartRoundCor());
    }

    private IEnumerator StartRoundCor()
    {
        var wait = new WaitForSeconds(spawnDelay);

        int count = spawnCount;
        //if (entities.Capacity < spawnCount) entities.Capacity = spawnCount;
        //entities.Clear();

        //if (transArray.Capacity < spawnCount) transArray.Capacity = spawnCount;
        //if (keys.Capacity < spawnCount) keys.Capacity = spawnCount;
        //if (collisions.Capacity < spawnCount) collisions.Capacity = spawnCount;

        while(count > 0)
        {
            yield return wait;

            EntitySpawner.CreateEntities(1, SettingEntity);
            count -= 1;
        }
    }

    private void SettingEntity(Entity entity)
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        entityManager.AddComponentData(entity, new EnemyComponent() { enemyGroup = 0 });

        var quaternion = Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(0f, 360f));
        var dir = quaternion * new Vector3(0f, 1f, 0f);
        dir *= spawnDistance;

        entityManager.AddComponentData(entity, new Translation { Value = new Unity.Mathematics.float3(dir) });
        entityManager.AddComponentData(entity, new MoveComponent { dir = Vector3.zero });
        entityManager.AddComponentData(entity, new StateComponent(Enums.EntityState.AllOff));
        entityManager.AddComponentData(entity, new EntityKeyComponent { key = keyCount++ });
        entityManager.AddComponentData(entity, new CollisionComponent { radius = collisionRadius });

        //entities.Add(entity);
    }
}
