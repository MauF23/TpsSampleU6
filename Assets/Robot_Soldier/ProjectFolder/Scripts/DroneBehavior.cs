using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class DroneBehavior : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWayPointIndex = 0;
    private Transform currentWayPoint;

    public float rotateTime, movementTime;


    //Secuencia de movimiento de patrullaje
    Sequence sequenceMovement;
    public Ease tweenEase;
    public AnimationCurve tweenCurve;

    void Start()
    {
        currentWayPoint = waypoints[currentWayPointIndex];
        Movement();
        //MovementPath();
    }

    // Update is called once per frame
    void GetNextWaypoint()
    {
        currentWayPointIndex++;

        if (currentWayPointIndex > waypoints.Length - 1)
        {
            currentWayPointIndex = 0;
        }
        currentWayPoint = waypoints[currentWayPointIndex];
        Movement();
    }
    public void Movement()
    {
        sequenceMovement = DOTween.Sequence();

        //Append añade tweens a la secuencia de forma secuancial, es decir se reporducen hasta que termine de completarse el tween actual.
        sequenceMovement.Append(transform.DOLookAt(currentWayPoint.position, rotateTime).SetEase(tweenCurve));

        //Join añade tween a la secuencia de forma paralera al tween anterior, es decir se reproduce de manera simultánea a este pero
        //no al posterior de la lista.
        sequenceMovement.Join(transform.DOMove(currentWayPoint.position, movementTime));

        sequenceMovement.Play();
        sequenceMovement.OnComplete(GetNextWaypoint);

    }

    public void MovementPath()
    {
        transform.DOPath(WaypointsArray(), movementTime).SetLookAt(0.2f).SetLoops(-1, LoopType.Yoyo);
    }

    public void ResumeMovement()
    {
        sequenceMovement?.Play();

    }

    public void StopMovement()
    {
        sequenceMovement?.Pause();
    }

    private Vector3[] WaypointsArray()
    {
        Vector3[] points = new Vector3[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++)
        {
            points[i] = waypoints[i].position;
        }

        return points;

    }
}
