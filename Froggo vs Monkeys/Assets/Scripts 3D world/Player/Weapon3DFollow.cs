using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon3DFollow : MonoBehaviour
{
    [Header("Weapon")]
    [SerializeField] private Transform weaponObj;
    
    [Header("Follow")]
    [SerializeField] private Vector3 frontOffset;
    [SerializeField] private Vector3 backOffset;
    [Range(1, 10)]
    [SerializeField] private float smoothFactor = 1f;

    // -------------------------------

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        weaponObj.rotation = this.transform.rotation;

        Follow();
    }

    private void Follow()
    {
        Vector3 targetPosition;

        if (animator.GetFloat("VelocityZ") > 0)
        {
            targetPosition = transform.position + backOffset;
        }
        else
        {
            targetPosition = transform.position + frontOffset;
        }

        Vector3 smoothPosition = Vector3.Lerp(weaponObj.transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        weaponObj.transform.position = smoothPosition;
    }

}
