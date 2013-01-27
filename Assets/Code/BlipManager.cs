using UnityEngine;
using System.Collections;

public class BlipManager : MonoBehaviour {
	
	public AudioSource m_Source;
	public AudioClip m_Biiip;
	public AudioClip m_Bip;
	public AnimatedScript m_BlipAnim;

	// Use this for initialization
	void Start ()
	{
		m_Source.clip = m_Biiip;
		m_Source.loop = true;
		m_Source.Play();
		m_BlipAnim.deactivateOnEnd = true;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void PlayBiiip()
	{
		if (m_Source.isPlaying)
			return;

		m_Source.clip = m_Biiip;
		m_Source.loop = true;
		m_Source.Play();
	}
	
	public void PlayBip()
	{
		if (m_Source.isPlaying)
		{
				m_Source.Stop();
		}
		m_Source.clip = m_Bip;
		m_Source.loop = false;
		m_Source.Play();
		
		m_BlipAnim.Reset();
		m_BlipAnim.transform.gameObject.SetActive(true);
	}
}
