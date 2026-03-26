using System.Collections.Generic;
using Meta.XR.MRUtilityKit;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public int numberOfOrbsToSpawn = 5;
    public GameObject orbPrefab;
    public float height;

    public bool orbsSpawned = false;
    public List<GameObject> orbsAlive;
    public MRUKAnchor.SceneLabels sceneLabels;

    public int maxNumberOfTry = 100;
    private int currentTry = 0;

    public static OrbSpawner instance;

    void Start()
    {
        MRUK.Instance.RegisterSceneLoadedCallback(SpawnOrbs);
    }

    void Awake()
    {
        instance = this;
    }

    void SpawnOrbs()
    {
        for (int i = 0; i < numberOfOrbsToSpawn; i++)
        {
            Vector3 randomPosition = Vector3.zero;

            MRUKRoom room = MRUK.Instance.GetCurrentRoom();

            while (currentTry < maxNumberOfTry)
            {
                bool hasFoundPosition = room.GenerateRandomPositionOnSurface(MRUK.SurfaceType.FACING_UP, 1,
                 new LabelFilter(sceneLabels), out randomPosition, out Vector3 n);

                if (hasFoundPosition)
                    break;

                currentTry++;
            }

            currentTry = 0;
            randomPosition.y = height;
            GameObject orb = Instantiate(orbPrefab, randomPosition, Quaternion.identity);
            orbsAlive.Add(orb);
        }

        orbsSpawned = true;
    }

    public void DestroyOrb(GameObject orb)
    {
        orbsAlive.Remove(orb);
        Destroy(orb);
    }

}
