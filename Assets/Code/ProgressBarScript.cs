using UnityEngine;
using System.Collections;

public class ProgressBarScript : MonoBehaviour {
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	public Transform barEmpty;
	public Transform barFull;
	public bool showprogresstest = true;
	public float speed = 0.01f;
	private float percent = 0f;
	private bool finish = false;
	
	// Use this for initialization
	void Start ()
	{
		if(barEmpty != null)
		{
			Material[] themats = barEmpty.renderer.materials;
			themats[0].mainTexture = progressBarEmpty;
		}
		
		if(barFull != null)
		{
			Material[] themats2 = barFull.renderer.materials;
			themats2[0].mainTexture = progressBarFull;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(showprogresstest)
			percent += speed;
		if(percent > 1)
			finish = true;
		if (!finish)
		{
			if(percent < 0)
				percent = 0;
			
			barFull.transform.localScale = new Vector3(percent, barFull.transform.localScale.y, barFull.transform.localScale.z);
			barFull.transform.localPosition = new Vector3((0.3f * percent) - 0.3f, barFull.transform.localPosition.y, barFull.transform.localPosition.z);
		}
	}
}
