using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{

    private Animator anim;
    void Start()
    {
        anim = this.transform.parent.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider obj)
    {
        anim.SetBool("isOpen", true);
    }

    void OnTriggerExit(Collider obj)
    {
        anim.SetBool("isOpen", false);
    }
}