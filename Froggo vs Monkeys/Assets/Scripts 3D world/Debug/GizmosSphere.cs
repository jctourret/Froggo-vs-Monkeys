using UnityEngine;

public class GizmosSphere : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private Color color = Color.blue;
    [SerializeField] private float radius = 1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;

        Gizmos.DrawWireSphere(objectToFollow.position, radius);
    }
}
