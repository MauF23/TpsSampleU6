using UnityEngine;
using DG.Tweening; //Importar DOTWEEN al script

public class DroneBehavior : MonoBehaviour
{
    public Transform[] waypoints;
    public int currentWayPointIndex = 0;
    public Transform currentWayPoint;

    public float rotateTime, movementTime;

    Sequence sequenceMovement;

    //Suavizado de la rotación.
    public Ease rotateEase;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWayPoint = waypoints[currentWayPointIndex];
        Movement();
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
        sequenceMovement = DOTween.Sequence(); //inicializar la secuencia.

        //Append añade tweens a la secuencia de forma secuencial, es decir se ejecuta cuando TERMINA el anterior.
        sequenceMovement.Append(transform.DOLookAt(currentWayPoint.position, rotateTime).SetEase(rotateEase));

        //Join añade tweens a la secuencia de forma paralela, es decir se ejecuta AL MISMO TIEMPO que el anterior.
        sequenceMovement.Join(transform.DOMove(currentWayPoint.position, movementTime));

        sequenceMovement.Play();

        //Obtener el siguiente waypoint al terminar la secuencia.
        sequenceMovement.OnComplete(GetNextWaypoint);
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
