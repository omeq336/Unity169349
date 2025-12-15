using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 30f;

    void Update()
    {
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        if (transform.position.z > 30f) Destroy(gameObject);

    }
}
