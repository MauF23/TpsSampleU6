using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public KeyCode pauseTimelineKey;


    void Start()
    {
        timelineDirector.Play();
	}

    void Update()
    {

    }
}
