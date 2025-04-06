using UnityEngine;
using DG.Tweening;

public class DroneBehavior : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWayPointIndex;
	private Transform currentWayPoint;

    public float rotateTime, movementTime;

    Sequence sequenceMovement;
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

        if (currentWayPointIndex > waypoints.Length-1)
        {
            currentWayPointIndex = 0;
		}
        currentWayPoint = waypoints[currentWayPointIndex];
        Movement();

	}
	public void Movement()
    {
        sequenceMovement = DOTween.Sequence();
        sequenceMovement.Append(transform.DOLookAt(currentWayPoint.position, rotateTime));
        sequenceMovement.Append(transform.DOMove(currentWayPoint.position, movementTime));

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
