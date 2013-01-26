using UnityEngine;
using System.Collections;

public class MicHandle : MonoBehaviour {
	// Name of the device
	private static string device;
	// Source audio
	private AudioSource audio;
	// Active the loop
	public bool loop = false;
	// Size of the record
	public int lengthSec = 10;
	// Tolerance of noise
	public float toleranceNoise = 0.7f;
	// Sample Count.
	private int sampleCount = 1024;
	// RMS value for 0 dB.
	private float refValue = 0.1f;
	// Minimum amplitude to extract pitch (recieve anything)
	private float threshold = 0.02f;
	// Volume in RMS
	private float rmsValue;
	// Volume in DB
	private float dbValue;
	// Pitch - Hz (is this frequency?)
	private float pitchValue;
	// Samples
	private float[] samples;
	// Spectrum
	private float[] spectrum;
	
	public void Start ()
	{
		samples = new float[sampleCount];
		spectrum = new float[sampleCount];
		
		// This starts the mic, for lengthSec seconds, recording at frequency hz. I am unsure of how to avoid this hack.
		if (device == null)
			device = Microphone.devices[0];
		audio = this.GetComponent<AudioSource>();
		audio.clip = Microphone.Start(MicHandle.device, loop, lengthSec, AudioSettings.outputSampleRate);
		while (!(Microphone.GetPosition(device) > 0))
		{
		}
		audio.Play();
	}
	
	public void Update ()
	{
		if (audio.isPlaying)
		{
			// Le big cheese doing its thing.
			AnalyzeSound();
			
			//Debug.Log("RMS: " + rmsValue.ToString("F2") + " (" + dbValue.ToString("F1") + " dB)\n" + "Pitch: " + pitchValue.ToString("F0") + " Hz");
		}
	}
	
	private void AnalyzeSound()
	{
		// Get all of our samples from the mic.
		audio.GetOutputData(samples, 0);
		
		// Sums squared samples
		float sum = 0;
		for (int i = 0; i < sampleCount; i++)
		sum += Mathf.Pow(samples[i], 2);
		
		// RMS is the square root of the average value of the samples.
		rmsValue = Mathf.Sqrt(sum/sampleCount);
		// dB
		dbValue = 20*Mathf.Log10(rmsValue/refValue);
		
		// Clamp it to -160dB min
		if (dbValue < -160)
			dbValue = -160;
		
		// Gets the sound spectrum.
		audio.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
		float maxV = 0;
		int maxN = 0;
		
		// Find the highest sample.
		for (int i = 0; i < sampleCount; i++)
		{
			if (spectrum[i] > maxV && spectrum[i] > threshold)
			{
				maxV = spectrum[i];
				// maxN is the index of max
				maxN = i;
			}
		}
		
		// Pass the index to a float variable
		float freqN = maxN;
		
		// Interpolate index using neighbours
		if (maxN > 0 && maxN < sampleCount - 1) {
			float dL = spectrum[maxN-1] / spectrum[maxN];
			float dR = spectrum[maxN+1] / spectrum[maxN];
			freqN += 0.5f * (dR * dR - dL * dL);
		}
		
		// Convert index to frequency
		pitchValue = freqN * 24000 / sampleCount;
	}
	
	public bool doInsufflation()
	{
		return rmsValue >= toleranceNoise;
	}
}