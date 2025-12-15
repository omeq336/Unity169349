using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject speedUpPickUp;
    [SerializeField] private GameObject tripleShotPickUp;
    [SerializeField] private GameObject barrelSlowDownPickUp;

    private List<GameObject> pickUpList = new List<GameObject>();

    private float timeDeltaTimer = 0f;
    private int timePassed = 0;

    void Start()
    {
        pickUpList.Add(speedUpPickUp);
        pickUpList.Add(tripleShotPickUp);
        pickUpList.Add(barrelSlowDownPickUp);
    }

    void Update()
    {

        timeDeltaTimer += Time.deltaTime;
        if (timeDeltaTimer >= 1f)
        {
            timePassed += 1;
            timeDeltaTimer = 0;
        }

        if (timePassed >= 10)
        {
            spawnRandomPickup();
            timePassed = 0;
        }

    }

    private void spawnRandomPickup()
    {
        int randomPickUpIndex = Random.Range(0,3);

        GameObject spawnedBarrel = Instantiate(pickUpList[randomPickUpIndex], GetRandomPosition(), Quaternion.Euler(90f, 0f, 0f));
    }

    private Vector3 GetRandomPosition()
    {
        int randomX = Random.Range(-31, 31);
        int randomZ = Random.Range(-16, 18);

        Vector3 randomLocation = new Vector3(randomX, 1.5f, randomZ);

        return randomLocation;
    }
}
