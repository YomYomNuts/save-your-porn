using UnityEngine;
using System.Collections;

public class GlitchRenderer : MonoBehaviour
{
	private ArrayList glitches;
	public int m_NbGlitches = 10;
	public float m_RandomPosRange;
	public float m_RandomSizeRange;
	public float m_RefreshPeriod;
	
	private Vector3	m_InitialScale;
	private Vector2 decalPos;
	private Vector2 factorPos;
	private float m_RefreshTimer;
	private int m_NbSkins = 5;
	
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
		m_RefreshTimer = 0;
		
		float height = Camera.mainCamera.orthographicSize;
		float width = height * 16 / 9;
		factorPos.x = width / InputAnalyzer.GetNbColumns();
		factorPos.y = height / InputAnalyzer.GetNbLines();
		
		decalPos.x = -width / 2;
		decalPos.y = -height / 2;
		
	}
	
	// Update is called once per frame
	void Update()
	{
		m_RefreshTimer += Time.deltaTime;
		if (m_RefreshTimer > m_RefreshPeriod)
		{
			m_RefreshTimer = 0;
			InputAnalyzer inputAn = InputAnalyzer.GetInstance();
			if (inputAn.GetNbKeysPressed() > 0)
			{
				int nbGlitchesToRender = Random.Range(0, m_NbGlitches);
				Vector2 inputPos = inputAn.GetInputPos();
				for (int i= 0; i < m_NbGlitches; ++i)
				{
					GameObject gao = glitches[i] as GameObject;
					if (i < nbGlitchesToRender)
					{
						gao.SetActive(true);
						Vector3 position = new Vector3(decalPos.x + inputPos.x * factorPos.x + Random.Range(-m_RandomPosRange, m_RandomPosRange), -(decalPos.y + inputPos.y * factorPos.y) + Random.Range(-m_RandomPosRange, m_RandomPosRange), 1f);
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
			else
			{
				foreach(GameObject gao in glitches)
				{
					gao.SetActive(false);
				}
			}
		}
	}
}
