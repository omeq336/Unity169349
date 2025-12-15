using UnityEngine;

public class DippingPlate : MonoBehaviour
{
    private float timeDeltaCounter = 0f;
    private float timeToRaise = 0.1f;

    private float minYPoint;
    private float maxYPoint;

    [SerializeField] private bool goUp = false;
    private float dippingSpeed = 1.5f;

    private void Start()
    {
        minYPoint = transform.position.y - 1.5f;
        maxYPoint = transform.position.y + 1.5f;
    }

    void Update()
    {
        float direction;

        if (goUp) direction = 1f;
        else direction = -1f;

        transform.position += Vector3.up * direction * dippingSpeed * Time.deltaTime;

        float y = transform.position.y;

        if (y >= maxYPoint) goUp = false;
        else if (y <= minYPoint) goUp = true;
    }

}
