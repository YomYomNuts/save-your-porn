using UnityEngine;
using System.Collections;

public class MicrophoneScript : MonoBehaviour {
	//Attributs
	private AudioSource audio;
	public bool loop = false;
	public int lengthSec = 10;
	public int frequency = 441;
	private int counter = 0;
	
	// Use this for initialization
	void Start () {
		audio = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
    	if (!audio.isPlaying)
		{
			/*if (counter == 0 || counter > 1)
				activeMicrophone();
			else if (counter == 1)
			{
		        float[] samples = new float[audio.clip.samples * audio.clip.channels];
	        	audio.clip.GetData(samples, 0);
				int i = 0;
		        while (i < samples.Length) {
		            Debug.Log(samples[i]);
		            ++i;
		        }
			}
			counter++;*/
			activeMicrophone();
		}
	}
	
	// Active the microphone
	public void activeMicrophone()
	{
    	audio.clip = Microphone.Start(Microphone.devices[0], loop, lengthSec, frequency);
	    audio.Play();
	}
	
	// Stop the record
	public void stopMicrophone()
	{ 
		audio.Stop();
		Debug.Log(audio.maxDistance);
		Debug.Log(audio.minDistance);
	}
}
