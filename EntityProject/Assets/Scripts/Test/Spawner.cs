using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;


public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int createNumber;

    // Start is called before the first frame update
    void Start()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        #region
        //var entityArchetype = entityManager.CreateArchetype(
        //    typeof(LevelComponent),
        //    typeof(Translation),
        //    typeof(RenderMesh),
        //    typeof(LocalToWorld),
        //    typeof(MoveSpeedComponent),
        //    typeof(RenderBounds),
        //    typeof(Rotation),
        //    typeof(Scale)
        //);

        //var blobAssetStore = new BlobAssetStore();
        //GameObjectConversionSettings setting = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);



        //var entity = entityManager.CreateEntity(entityArchetype);
        //entityManager.SetComponentData(entity, new LevelComponent { level = 10f });

        //var entityArray = new NativeArray<Entity>(5, Allocator.Temp);
        //entityManager.CreateEntity(entityArchetype, entityArray);

        //for(int i = 0, icount = entityArray.Length; i<icount; i++)
        //{
        //    var entity = entityArray[i];
        //    entityManager.SetComponentData(entity, new LevelComponent { level = Random.Range(10, 20) });
        //    entityManager.SetComponentData(entity, new MoveSpeedComponent { speed = Random.Range(-1f, 1f) });
        //    entityManager.SetComponentData(entity, new Translation { Value = new Unity.Mathematics.float3(Random.Range(-8f, 8f), Random.Range(-5f, 5f), Random.Range(-5f, 5f)) });
        //    //entityManager.SetComponentData(entity, new Scale { Value = 1f });

        //    entityManager.SetSharedComponentData(entity, new RenderMesh
        //    {
        //        material = mat,
        //        mesh = mesh
        //    });
        //    entityManager.SetComponentData(entity, new Rotation { Value = Unity.Mathematics.quaternion.identity });
        //    entityManager.SetComponentData(entity, new Scale { Value = 1f });
        //}

        //entityArray.Dispose();
        #endregion

        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(prefab, settings);

        for(int i = 0; i<createNumber; i++)
        {
            var instance = entityManager.Instantiate(entityPrefab);

            entityManager.SetComponentData(instance, new Translation { Value = new Unity.Mathematics.float3(Random.Range(-20f, 20f), Random.Range(-5f, 5f), 0f) });
            entityManager.AddComponentData(instance, new Scale { Value = 0.25f });
            entityManager.AddComponentData(instance, new MoveSpeedComponent { speed = Random.Range(-1f, 1f) });
        }
    }
}
