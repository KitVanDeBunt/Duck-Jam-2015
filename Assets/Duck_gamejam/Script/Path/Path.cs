using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField]
    private Transform[] pathPoints;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < (pathPoints.Length - 1); i++)
        {
            Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
        }
        for (int i = 0; i < (pathPoints.Length); i++)
        {
            Gizmos.DrawWireCube(pathPoints[i].position, Vector3.one);
        }
    }

    public Transform GetPoint(int index)
    {
        return pathPoints[index];
    }

    public float GetPathLength()
    {
        return pathPoints.Length;
    }
}
