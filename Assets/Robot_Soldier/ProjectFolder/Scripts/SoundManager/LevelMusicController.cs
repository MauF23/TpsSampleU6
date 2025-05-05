using UnityEngine;

public class LevelMusicController : MonoBehaviour
{
	public SoundManager soundManagerGeneral;

	private void Start()
	{
		soundManagerGeneral.FadeSound(SoundEnums.BgMusic, 3, true);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			soundManagerGeneral.FadeSound(SoundEnums.BgMusic, 3, false);
		}
	}
}
