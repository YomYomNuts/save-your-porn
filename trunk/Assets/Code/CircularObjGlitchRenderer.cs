using UnityEngine;
using System.Collections;

public class CircularObjGlitchRenderer : MonoBehaviour
{
	private ArrayList glitches;
	public int m_NbGlitches = 10;
	public float m_RandomPosRange;
	public float m_RandomSizeRange;
	public float m_RefreshPeriod;
	public float m_ShowDuration;
	public float m_HideDuration;
	public AudioClip[] m_Clips;
	public AudioSource m_Source;
	public CircularMassageObjective	m_Objective;
	
	private Vector3	m_InitialScale;
	private Vector2 decalPos;
	private Vector2 factorPos;
	private float m_Timer;
	private float m_RefreshTimer;
	private Vector2 m_CurrentPos;
	private int m_NbSkins = 5;
	private bool m_IsShowing;
	
	// Use this for initialization
	void Start()
	{
		glitches = new ArrayList(m_NbGlitches);
		for (int i = 0; i < m_NbGlitches; ++i)
		{
			GameObject gao = Instantiate(Resources.Load("Prefabs/glitch plane")) as GameObject;
			gao.SetActive(false);
			glitches.Add(gao);
			m_InitialScale = new Vector3(gao.transform.localScale.x, gao.transform.localScale.y, gao.transform.localScale.z);
		}
		m_Timer = 0;
		m_RefreshTimer = 0;
		m_IsShowing = false;
		
		float height = Camera.mainCamera.orthographicSize;
		float width = height * 16 / 9;
		factorPos.x = width / InputAnalyzer.GetNbColumns();
		factorPos.y = height / InputAnalyzer.GetNbLines();
		
		decalPos.x = -width / 2;
		decalPos.y = -height / 2;
	}
	
	private void HideGlitches()
	{
		for (int i= 0; i < m_NbGlitches; ++i)
		{
			GameObject gao = glitches[i] as GameObject;
			gao.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		if (!m_Objective.gameObject.activeInHierarchy || !m_Objective.enabled)
		{
			HideGlitches();
			return;
		}
		m_Timer += Time.deltaTime;
		if (m_Timer >= 0 && m_Timer < m_HideDuration)
		{
			if (m_IsShowing)
			{
				m_IsShowing = false;
				foreach(GameObject gao in glitches)
				{
					gao.SetActive(false);
				}
				m_RefreshTimer = 0;
			}
		}
		else if (m_Timer < m_HideDuration + m_ShowDuration)
		{
			if (!m_IsShowing)
			{
				m_IsShowing = true;
				m_RefreshTimer = 0;
				m_CurrentPos = m_Objective.GetTargetPos();
				
			}
			else
			{
				m_RefreshTimer += Time.deltaTime;
				if (m_RefreshTimer > m_RefreshPeriod)
				{
					m_RefreshTimer = 0;
					int nbGlitchesToRender = Random.Range(0, m_NbSkins);
					for (int i= 0; i < m_NbGlitches; ++i)
					{
						GameObject gao = glitches[i] as GameObject;
						if (i < nbGlitchesToRender)
						{
							gao.SetActive(true);
							Vector3 position = new Vector3(decalPos.x + m_CurrentPos.x * factorPos.x + Random.Range(-m_RandomPosRange, m_RandomPosRange), -(decalPos.y + m_CurrentPos.y * factorPos.y) + Random.Range(-m_RandomPosRange, m_RandomPosRange), 1f);
							gao.transform.position = position;
							Vector3 size = new Vector3(m_InitialScale.x + Random.Range(-m_RandomSizeRange, m_RandomSizeRange), m_InitialScale.y + Random.Range(-m_RandomSizeRange, m_RandomSizeRange), m_InitialScale.z);
							gao.transform.localScale = size;
							
							string glitchTexName = "Textures/Glitches " + Random.Range(1, m_NbSkins);
							gao.renderer.material.mainTexture = (Resources.Load(glitchTexName) as Texture);
						}
						else
						{
							gao.SetActive(false);
						}
					}
				}
			}
		}
		else
		{
			m_Timer = 0;
		}
	}
}
