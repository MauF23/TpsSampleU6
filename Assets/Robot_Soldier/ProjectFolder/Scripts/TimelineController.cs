using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public KeyCode pauseTimelineKey, skipTimelineKey;
    public bool timelinePaused = false;
    private bool skipped = false;


    void Start()
    {
        timelineDirector.Play();
	}

    void Update()
    {
        if (Input.GetKeyDown(pauseTimelineKey))
        {
            timelinePaused = !timelinePaused;

            if (timelinePaused == true)
            {
                Time.timeScale = 0f;
            }
            else
            {
				Time.timeScale = 1f;
			}
		}

		if (Input.GetKeyDown(skipTimelineKey) && skipped == false)
        {
            timelineDirector.time = timelineDirector.duration;
            skipped = true;
        }

	}
}
