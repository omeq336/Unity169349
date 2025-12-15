using UnityEngine;

public class SideSpinningItem : MonoBehaviour
{
    private Vector3 startingPosition;
    private bool isMovementStopped = false;
    private bool hasReachedHighestPoint = false;
    private float maximumYPosition;

    private float timeToDespawn = 3f;
    private float timeDeltaCounter = 0f;

    private bool shootRight;

    void Start()
    {
        startingPosition = transform.position;
        maximumYPosition = 7f;// startingPosition.y + 10f;
    }

    void Update()
    {
        if (!isMovementStopped)
        {
            if (shootRight) WeaponMoveRight();
            else WeaponMoveLeft();
        }
        else
        {
            timeDeltaCounter += Time.deltaTime;
            if (timeDeltaCounter >= timeToDespawn) Destroy(gameObject);
        }

    }

    private void StopMovement()
    {
        isMovementStopped = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void WeaponMoveRight()
    {
        transform.Rotate(new Vector3(0, 0, -720) * Time.deltaTime);
        float xAcceleration = Random.Range(0.05f, 0.15f);
        Vector3 currentPosition = transform.position;
        if (!hasReachedHighestPoint)
        {
            if (currentPosition.y >= maximumYPosition)
            {
                hasReachedHighestPoint = true;
            }
            transform.position = new Vector3(transform.position.x + xAcceleration, transform.position.y + 0.1f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x + xAcceleration, transform.position.y, transform.position.z);
        }

    }

    private void WeaponMoveLeft()
    {
        transform.Rotate(new Vector3(0, 0, 720) * Time.deltaTime);
        float xAcceleration = Random.Range(0.05f, 0.15f);
        Vector3 currentPosition = transform.position;
        if (!hasReachedHighestPoint)
        {
            if (currentPosition.y >= maximumYPosition)
            {
                hasReachedHighestPoint = true;
            }
            transform.position = new Vector3(transform.position.x - xAcceleration, transform.position.y + 0.1f, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - xAcceleration, transform.position.y, transform.position.z);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollapseFloor"))
        {
            StopMovement();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void SetShootDirection(bool shootRightDecision)
    {
        shootRight = shootRightDecision;
    }
}
