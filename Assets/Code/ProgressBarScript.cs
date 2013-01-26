using UnityEngine;
using System.Collections;

public class ProgressBarScript : MonoBehaviour {
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	public Transform barEmpty;
	public Transform barFull;
	public GameObject picture;
	public GameObject textFile;
	public bool showprogresstest = true;
	public float speed = 0.01f;
	public float percent = 0f;
	public float timerFinish = 2f;
	
	private Object[] textures;
	private int counterTextures;
	private bool finish;
	private float timer;
	
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
		
		textures = Resources.LoadAll("Data/file_deleted");
		counterTextures = 0;
		picture.renderer.materials[0].mainTexture = textures[counterTextures] as Texture2D;
		textFile.GetComponent<TextMesh>().text = (textures[counterTextures] as Texture2D).name;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(showprogresstest)
			percent += speed;
		if(percent > 1)
		{
			counterTextures++;
			if (textures.Length > counterTextures)
			{
				percent = 0;
				picture.renderer.materials[0].mainTexture = textures[counterTextures] as Texture2D;
				textFile.GetComponent<TextMesh>().text = (textures[counterTextures] as Texture2D).name;
			}
			else
			{
				finish = true;
				picture.renderer.materials[0].mainTexture = null;
				textFile.GetComponent<TextMesh>().text = "Done.";
				timer += Time.deltaTime;
				if (timer >= timerFinish)
					Application.LoadLevel(Const.LEVEL_LOSE);
			}
		}
		if (!finish)
		{
			if(percent < 0)
				percent = 0;
			
			barFull.transform.localScale = new Vector3(percent, barFull.transform.localScale.y, barFull.transform.localScale.z);
			barFull.transform.localPosition = new Vector3((0.3f * percent) - 0.3f, barFull.transform.localPosition.y, barFull.transform.localPosition.z);
		}
	}
}
