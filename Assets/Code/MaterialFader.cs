using UnityEngine;
using System.Collections;

public class MaterialFader : MonoBehaviour {
	
	public Material m_Material;
	public float	m_Timer;
	public E_FadeType	m_FadeType;
	public float	m_minFadeValue;
	public float	m_maxFadeValue;
	
	private bool m_Activated;
	private float m_CurrentTime;
	
	public enum E_FadeType
	{
		E_FADE_IN,
		E_FADE_OUT,
		E_FADE_IN_OUT
	}

	// Use this for initialization
	void Start () {
		m_Activated = false;
		m_CurrentTime = 0;
		if (m_Material != null)
		{
			m_Material.color = new Color(m_minFadeValue, m_minFadeValue, m_minFadeValue);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if (m_Activated)
		{
			m_CurrentTime += Time.deltaTime;
			if (m_CurrentTime > m_Timer)
				End ();
			else
			{
				switch (m_FadeType)
				{
					case E_FadeType.E_FADE_IN:
						UpdateFadeIn();
						break;
					case E_FadeType.E_FADE_IN_OUT:
						UpdateFadeInOut();
						break;
					case E_FadeType.E_FADE_OUT:
						UpdateFadeOut();
						break;
					default:
						Debug.LogError("Wrong Fade Type !");
						break;
				}
			}
		}
	}
	
	public bool IsActivated()
	{
		return m_Activated;
	}
	
	public void Activate()
	{
		if (!m_Activated)
		{
			m_Activated = true;
			m_CurrentTime = 0;
			float colorValue = 0;
			Color color;
			switch (m_FadeType)
			{
				case E_FadeType.E_FADE_IN:
				case E_FadeType.E_FADE_IN_OUT:
					colorValue = m_minFadeValue;
					break;
				case E_FadeType.E_FADE_OUT:
					colorValue = m_maxFadeValue;
					break;
				default:
					Debug.LogError("Wrong Fade Type !");
					break;
			}
			color = new Color(colorValue, colorValue, colorValue);
			m_Material.color = color;
		}
	}
	
	private void End()
	{
		float colorValue = 0;
		Color color;
		m_Activated = false;
		
		switch (m_FadeType)
		{
			case E_FadeType.E_FADE_IN:
			case E_FadeType.E_FADE_IN_OUT:
				colorValue = m_minFadeValue;
				break;
			case E_FadeType.E_FADE_OUT:
				colorValue = m_maxFadeValue;
				break;
			default:
				Debug.LogError("Wrong Fade Type !");
				break;
		}
		
		Debug.Log("color : " + colorValue);
		color = new Color(colorValue, colorValue, colorValue);
		m_Material.color = color;
	}
	
	private void UpdateFadeIn()
	{
		float colorValue = m_minFadeValue + (m_maxFadeValue - m_minFadeValue)* m_CurrentTime / m_Timer;
		Color color = new Color(colorValue, colorValue, colorValue);
		m_Material.color = color;
	}
	
	private void UpdateFadeOut()
	{
		float colorValue = m_minFadeValue + (m_maxFadeValue - m_minFadeValue)*(1 - (m_CurrentTime / m_Timer));
		Color color = new Color(colorValue, colorValue, colorValue);
		m_Material.color = color;
	}
	
	private void UpdateFadeInOut()
	{
		float colorValue;
		if (m_CurrentTime > m_Timer / 2)
			colorValue = m_maxFadeValue + (m_minFadeValue - m_maxFadeValue)*(2 * (m_CurrentTime - m_Timer/2) / m_Timer);
		else
			colorValue = m_minFadeValue + (m_maxFadeValue - m_minFadeValue)*(2 * m_CurrentTime / m_Timer);
		Color color = new Color(colorValue, colorValue, colorValue);
		m_Material.color = color;
	}
}
