using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public  class EntitySpawner : MonoBehaviour
{
    [SerializeField] private GameObject convertPrefab;
    private BlobAssetStore blobAssetStore;

    private void Start()
    {
        blobAssetStore = new BlobAssetStore();
    }

    private void OnDestroy()
    {
        blobAssetStore.Dispose();
    }

    public void CreateEntities(int count, UnityAction<Entity> settingFunc = null)
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
        var entityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(convertPrefab, settings);

        for(int i = 0; i<count; i++)
        {
            var instance = entityManager.Instantiate(entityPrefab);

            if (settingFunc != null)
                settingFunc(instance);
        }

        entityManager.DestroyEntity(entityPrefab);
    }
}
