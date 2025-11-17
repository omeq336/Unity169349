using UnityEngine;

// baza https://github.com/SamuelAsherRivello/physics-for-unity


public class RaycastDemo : MonoBehaviour
{
    [SerializeField]
    private bool _isDebug = true;

    [SerializeField]
    private float _rayDistance = 3;

    [SerializeField]
    private float _rayDuration = 0.1f;

    [SerializeField]
    private float raycastHeightOffset = 1.5f;

    private Ray _ray;
    private Vector3 _rayPosition;
    private RaycastHit _raycastHit;

    public Transform door; // Drzwi, które maj¹ siê podnosiæ
    public float openHeight = 5f; // Wysokoœæ, na któr¹ drzwi siê podnios¹
    public float speed = 2f; // Prêdkoœæ otwierania/zamykania

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpening = false;


    void Start()
    {
        _ray = new Ray();
        _ray.direction = transform.forward;
        closedPosition = door.position;
        openPosition = closedPosition + Vector3.up * openHeight;
    }

    void FixedUpdate()
    {
        _ray.origin = transform.position + Vector3.up * raycastHeightOffset;

        _ray.direction = transform.forward;

        if (_isDebug == true)
        {
            Debug.DrawRay(_ray.origin, _ray.direction * _rayDistance, Color.red, _rayDuration);
        }

        Physics.Raycast(_ray, out _raycastHit, _rayDistance);

        if (_raycastHit.collider != null && _raycastHit.collider.CompareTag("Door"))
        {
            Debug.Log("Colliding with: " + _raycastHit.collider.gameObject.name);
            isOpening = true;
        }
        else
        {
            isOpening = false;
        }

        if (isOpening)
        {
            door.position = Vector3.Lerp(door.position, openPosition, speed * Time.deltaTime);
        }
        else
        {
            door.position = Vector3.Lerp(door.position, closedPosition, speed * Time.deltaTime);
        }
    }
}
