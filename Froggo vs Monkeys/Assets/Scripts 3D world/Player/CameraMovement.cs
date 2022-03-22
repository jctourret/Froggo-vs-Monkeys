using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Follow")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 originalRotation;
    [Range(1, 10)]
    [SerializeField] private float smoothFactor;

    [Header("Zoom")]
    [Range(1, 100)]
    [SerializeField] private float zoomSpeed;
    [SerializeField] private Vector3 zoomPosOffset;
    [SerializeField] private Vector3 zoomRotOffset;

    // ----------------------

    private bool isZooming = false;

    // ----------------------

    private void FixedUpdate() 
    {
        Follow();
    }

    private void Follow()
    {
        if (!isZooming)
        {
            Vector3 targetPosition = target.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
    }

    public void Zoom(bool state)
    {
        isZooming = state;

        StartCoroutine(ZoomCamera(isZooming));
    }

    IEnumerator ZoomCamera(bool state)
    {
        float time = 0f;

        Transform initialTrans = this.transform;
        Vector3 targetPosition = target.position + zoomPosOffset;

        while (time < 1f)
        {
            if (state)
            {
                this.transform.position = Vector3.Lerp(initialTrans.position, targetPosition, time * zoomSpeed * Time.deltaTime);
                this.transform.rotation = Quaternion.Lerp(initialTrans.rotation, Quaternion.Euler(zoomRotOffset), time * zoomSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.rotation = Quaternion.Lerp(initialTrans.rotation, Quaternion.Euler(originalRotation), time * zoomSpeed * Time.deltaTime);
            }

            time += Time.deltaTime;

            yield return null;
        }
    }

}
