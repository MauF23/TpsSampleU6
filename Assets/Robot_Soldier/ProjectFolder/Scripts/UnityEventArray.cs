using UnityEngine;
using UnityEngine.Events;

public class UnityEventArray : MonoBehaviour
{
	public UnityEvent[] events;

	public void InvokeEvent(int eventId)
	{
		eventId = Mathf.Clamp(eventId, 0, events.Length);
		events[eventId]?.Invoke();
	}
}
