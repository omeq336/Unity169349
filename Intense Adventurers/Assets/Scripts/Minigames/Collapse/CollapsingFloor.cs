using UnityEngine;

public class CollapsingFloor : MonoBehaviour
{
    [SerializeField]
    private GameObject floorObject;
    private Vector3 originalFloorPosition;

    private float fallingSpeed = 0.001f;
    private bool isFalling = false;

    [SerializeField] private CollapsePlayer collapsePlayer;


    void Start()
    {
        originalFloorPosition = floorObject.transform.position;
        Debug.Log(originalFloorPosition);
    }

    void Update()
    {
        if (!collapsePlayer.HasGameFinished) CollapseFloor();
    }

    private void CollapseFloor()
    {
        Vector3 currentFloorPosition = floorObject.transform.position;

        if (isFalling || currentFloorPosition.y < originalFloorPosition.y * 1.2f)
        {
            Vector3 positionSubtractionValue = new Vector3(0, fallingSpeed, 0);

            if (currentFloorPosition.y < originalFloorPosition.y * 1.2f)
            {
                positionSubtractionValue = new Vector3(0, fallingSpeed * 50, 0);
            }
            if (currentFloorPosition.y < originalFloorPosition.y * 2f)
            {
                Destroy(gameObject);
            }
            floorObject.transform.position = currentFloorPosition - positionSubtractionValue;
        }
        else
        {
            if (currentFloorPosition.y < originalFloorPosition.y && currentFloorPosition.y > originalFloorPosition.y * 1.2f)
            {
                Vector3 positionSubtractionValue = new Vector3(0, 0.001f, 0);
                floorObject.transform.position = currentFloorPosition + positionSubtractionValue;
            }
        }
        Debug.Log(currentFloorPosition);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFalling = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFalling = false;
        }
    }

}
