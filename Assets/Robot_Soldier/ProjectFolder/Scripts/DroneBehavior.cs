using UnityEngine;
using DG.Tweening;
using System;

public class DroneBehavior : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWayPointIndex = 0;
    private Transform currentWayPoint;

    public float rotateTime, movementTime, jumpPower;
    int totalJumps = 1;

    //Declarar la referencia de la secuencia de tweens.
    Sequence sequenceMovement;

    [SerializeField]
    Ease easeType;

    [SerializeField]
    AnimationCurve easeCurve;
    Tween pathTween;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWayPoint = waypoints[currentWayPointIndex];
        Movement();
        //PathMovement();
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

        //Append suma tweens a la secuencia de forma secuancial.
        sequenceMovement.Append(transform.DOLookAt(currentWayPoint.position, rotateTime).SetEase(easeCurve));

        //Join suna tweens a la secuancia de forma simultánea al tween anterior
        sequenceMovement.Append(transform.DOJump(currentWayPoint.position, jumpPower, totalJumps, movementTime));


        sequenceMovement.Play();
        sequenceMovement.OnComplete(GetNextWaypoint);
    }

    public void PathMovement()
    {
        pathTween?.Kill(); // detener el tween si ya está iniciado.
        pathTween = transform.DOPath(WaypointsArray(), movementTime).SetLoops(-1, LoopType.Yoyo).SetLookAt(rotateTime); //-1 indica que el loop es infinito
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
