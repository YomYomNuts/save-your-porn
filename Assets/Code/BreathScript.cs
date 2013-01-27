using UnityEngine;
using System.Collections;

public class BreathScript : Objective {
	//Attributs
	public int numberOfInsufflation = 2;
	public float timeOfInsufflationInSeconds = 1f;
	private float time;
	private int counterInsufflation;
	private bool pause;

	// Use this for initialization
	void Start () {
		time = 0;
		counterInsufflation = 0;
		pause = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (counterInsufflation >= numberOfInsufflation)
			return;
		
		updateFunction();
		if (this.gameObject.GetComponent<MicHandle>().doInsufflation() && counterInsufflation < numberOfInsufflation)
		{
			time += Time.deltaTime;
			//Debug.Log("Time " + time + " Insufflation " + counterInsufflation);
			if (time >= timeOfInsufflationInSeconds && pause)
			{
				//Debug.Log("------------------------------Add Insufflation-------------------------------------");
				counterInsufflation++;
				time = 0;
				pause = false;
			}
		}
		else if (!this.gameObject.GetComponent<MicHandle>().doInsufflation())
		{
			pause = true;
			time = 0;
		}
		if (counterInsufflation >= numberOfInsufflation)
		{
			Debug.Log("YATTTTAAAAAAAA");
			Win();
		}
	}
}
