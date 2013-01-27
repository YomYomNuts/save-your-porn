using UnityEngine;
using System.Collections;

public class EffectIntro : MonoBehaviour {
	//Attributs
	public GameObject window_porn;
	public GameObject window_work;
	public float timerAppearWindows = 2f;
	
	private float timer;
	private bool done;

	// Use this for initialization
	void Start () {
		window_porn.SetActive(false);
		window_work.SetActive(false);
		done = false;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= timerAppearWindows && !done)
		{
			done = true;
			window_porn.SetActive(true);
			window_work.SetActive(true);
			Camera.mainCamera.GetComponent<AudioSource>().Play();
		}
	}
}
