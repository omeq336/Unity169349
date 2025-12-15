using UnityEngine;

public class BarrelSpawner : MonoBehaviour
{
    private int leftBorderX = -35;
    private int rightBorderX = 35;

    [SerializeField]
    private GameObject barrel;
    private float spawnTimer = 0;
    private float spawnInterval = 1f;
    private float minimalSpawnInterval = 0.1f;

    private float spawnIntervalDecreaseValue = 0.05f;

    private float decreaseTimer = 0;
    private float momentToDecreaseInterval = 2f;


    private bool isSlowed = false;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        decreaseTimer += Time.deltaTime;

        if (decreaseTimer >= momentToDecreaseInterval && spawnInterval - spawnIntervalDecreaseValue > minimalSpawnInterval)
        {
            DecreaseSpawnInterval();
            Debug.Log("Current spawn spawnInterval: " + spawnInterval);
            decreaseTimer = 0;
        }
        
        if (spawnTimer >= spawnInterval)
        {
            GameObject spawnedBarrel = Instantiate(barrel, GetRandomPosition(), Quaternion.Euler(0f, 0f, 90f));
            spawnTimer = 0;
            if (isSlowed)
            {
                HostileBarrel barrelScript = spawnedBarrel.GetComponent<HostileBarrel>();
                barrelScript.ReduceMovementSpeed();
            }
            else
            {
                HostileBarrel barrelScript = spawnedBarrel.GetComponent<HostileBarrel>();
                barrelScript.RestoreMovementSpeed();
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        int randomX = Random.Range(leftBorderX, rightBorderX);

        Vector3 randomLocation = new Vector3(randomX, transform.position.y, transform.position.z);

        return randomLocation;
    }

    private void DecreaseSpawnInterval()
    {
        spawnInterval -= spawnIntervalDecreaseValue; 
    }

    public void SlowDownBarrels()
    {
        isSlowed = true;
    }

    public void CancelSlowDownBarrels()
    {
        isSlowed = false;
    }

    public void DisableBarrelSpawner()
    {
        gameObject.SetActive(false);
    }
}
