using UnityEngine;

public class SpinningWeapon : MonoBehaviour
{
    private Vector3 startingPosition;
    private float maximumYPosition;
    private bool hasReachedHighestPoint = false;


    void Start()
    {
        startingPosition = transform.position;
        maximumYPosition = startingPosition.y + 10f;
    }

    void Update()
    {
  
        WeaponMove();

    }

    private void WeaponMove()
    {
        transform.Rotate(new Vector3(0, 0, -720) * Time.deltaTime);

        Vector3 currentPosition = transform.position;
        if (!hasReachedHighestPoint)
        {
            if (currentPosition.y >= maximumYPosition)
            {
                hasReachedHighestPoint = true;
            }
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z - 0.2f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z - 0.2f);
        }

    }


}
