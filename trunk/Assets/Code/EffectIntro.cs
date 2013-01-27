using UnityEngine;
using System.Collections;

public class EffectIntro : MonoBehaviour {
	//Attributs
	public GameObject window_porn;
	public GameObject window_work;
	public float timerAppearWindows = 2f;
	
	private float timer;

	// Use this for initialization
	void Start () {
		window_porn.SetActive(false);
		window_work.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= timerAppearWindows)
		{
			window_porn.SetActive(true);
			window_work.SetActive(true);
		}
	}
}
