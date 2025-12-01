using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = this.transform.parent.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider obj)
    {
        anim.SetBool("isGoingUp", true);
    }

    void OnTriggerExit(Collider obj)
    {
        anim.SetBool("isGoingUp", false);
    }
}
