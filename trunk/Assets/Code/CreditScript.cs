using UnityEngine;
using System.Collections;

public class CreditScript : MonoBehaviour
{
	public GameObject movieGao;
	private float m_Timer = 0;
	
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
			m_Timer += Time.deltaTime;
			MovieTexture movTex = movieGao.renderer.material.mainTexture as MovieTexture;
			if (!movTex.isPlaying && m_Timer > 20)
				Application.Quit();
		}
	}
}

