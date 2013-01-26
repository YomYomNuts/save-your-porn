using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {
	//Attributs
	private AudioSource audio;
	
	// Use this for initialization
	void Start () {
		audio = this.GetComponent<AudioSource>();
	    audio.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
	    audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
