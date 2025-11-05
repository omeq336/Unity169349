using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private bool isJumping = false;
    public float speed = 10;

    public TextMeshProUGUI countText;
    private int pickup_count = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pickup_count = 0;
        SetPickUpCountText();
        Debug.Log("Obecnie zebrane pickupy: ", countText);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            pickup_count += 1;
            Debug.Log("Obecnie zebrane pickupy: " + countText);
            SetPickUpCountText();
        }
    }

    void SetPickUpCountText()
    {
        countText.text = "Count: " + pickup_count.ToString();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue movementValue)
    {
        rb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
}