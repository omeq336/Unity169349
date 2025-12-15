using System;
using UnityEngine;

public class HostileBarrel : MonoBehaviour
{
    // Movement
    [SerializeField]
    private float movementSpeed = 20f;
    private float originalMovementSpeed = 20f;

    private int movementType;

    [SerializeField]
    private float frequency = 2f;
    [SerializeField]
    private float amplitude = 1f;

    [SerializeField]
    private GameObject coinPrefab;

    void Start()
    {
        movementType = RollMovementType();
    }

    void Update()
    {
        float sinWave;
        if (movementType == 0)
        {
            sinWave = Mathf.Sin(Time.time * frequency) * amplitude;
        }
        else
        {
            sinWave = -Mathf.Sin(Time.time * frequency) * amplitude;
        }

        Vector3 movement = new Vector3(0, sinWave, -1f) * movementSpeed * Time.deltaTime;

        transform.Translate(movement);

        if (transform.position.z < -30f) Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerProjectile"))
        {
            // Debug.Log("Oberwalem!");
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private int RollMovementType()
    {
        int randomNum = UnityEngine.Random.Range(0, 2);

        return randomNum;
    }

    public void ReduceMovementSpeed()
    {
        movementSpeed = originalMovementSpeed / 2f;
        // Debug.Log("I'm slowing to " + movementSpeed);
    }
    public void RestoreMovementSpeed()
    {
        movementSpeed = originalMovementSpeed;
        // Debug.Log("I'm restoring to " + movementSpeed);
    }
}
