using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour
{
	public GameObject movieGao;
	
	public void Start()
	{
		if (movieGao)
		{
			MovieTexture movTex = movieGao.renderer.material.mainTexture as MovieTexture;
			if (!movTex.isPlaying)
			{
				movTex.loop = false;
				movTex.Play();
			}
		}
	}
	
	public void Update()
	{
		if (movieGao)
		{
			MovieTexture movTex = movieGao.renderer.material.mainTexture as MovieTexture;
			if (movTex.isPlaying)
				Application.Quit();
		}
	}
}

