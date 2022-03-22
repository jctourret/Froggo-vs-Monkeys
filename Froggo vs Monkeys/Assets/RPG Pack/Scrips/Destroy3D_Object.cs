using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy3D_Object : MonoBehaviour
{

    [SerializeField] private string collisionTag = "Tongue";

    // --------------------------

    private Animator animator;

    // --------------------------

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == collisionTag)
        {
            animator.SetTrigger("Destroy");

            GetComponent<Collider>().enabled = false;
        }
    }

}
