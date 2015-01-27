using UnityEngine;
using System.Collections;

public class DuckMovement : MonoBehaviour {

    public Path path;

    [SerializeField]
    [Range(0f, 10000f)]
    private float speed;
    
    [SerializeField]
    [Range(0f, 10000f)]
    private float maxRotation = 1f;

    [SerializeField]
    private float triggerDist = 0.1f;

    private Transform currentTraget;
    private int pathProgres = 0;
    private float pathLength;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position + new Vector3(0, 0.2f, 0), currentTraget.position + new Vector3(0, 0.2f, 0));
        Gizmos.DrawWireSphere(currentTraget.position, triggerDist);
    }

	void Start () {
        currentTraget = path.GetPoint(pathProgres);
        pathLength = path.GetPathLength();
        transform.rotation = Quaternion.Euler(0,GetRotationTarget(),0);
	}
	
	void Update () {
        transform.Rotate(0, maxSteer(GetRotationTarget()*Time.deltaTime*50f), 0);

        transform.Translate(Vector3.forward*speed*Time.deltaTime);

        if (Vector3.Distance(transform.position, currentTraget.position) < triggerDist)
        {
            SetNextPoint();
        }
	}

    float GetRotationTarget()
    {
        Vector3 _direction = (currentTraget.position - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);
        _lookRotation = Quaternion.Euler(0, _lookRotation.eulerAngles.y, 0);
        return fixAngle(_lookRotation.eulerAngles.y - fixAngle(transform.rotation.eulerAngles.y));
    }

    private float maxSteer(float a)
    {
        if (a < -maxRotation)
        {
            return -maxRotation;
        }
        if (a > maxRotation)
        {
            return maxRotation;
        }
        return a;
    }

    private float fixAngle(float a)
    {
        if (a > 180)
        {
            a -= 360;
        }
        return a;
    }

    void SetNextPoint()
    {
        pathProgres++;
        if (pathProgres == pathLength)
        {
            GameObject.Destroy(gameObject);
            return;
        }
        currentTraget = path.GetPoint(pathProgres);
    }
}
