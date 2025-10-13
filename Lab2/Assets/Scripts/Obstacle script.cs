using UnityEngine;

public class Obstaclescript : MonoBehaviour
{
    public float speed = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 2.5)
        {
            transform.position = new Vector3(-2.8f, -2.3f, -9.2f);
        }
        SurpriseAttack();
    }

    void SurpriseAttack()
    {
        Vector3 moveDir = new Vector3(1f, 0f, 0f);
        float moveDistance = speed * Time.deltaTime;

        transform.position += moveDir * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * speed);

    }
}
