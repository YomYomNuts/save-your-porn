using UnityEngine;
using System.Collections;

/* objectif de massage statique : on doit appuyer plein de fois sur une zone donnée jusqu'à remplir une jauge */
public class SimpleStaticMassageObjective : Objective
{
	/* user settable */
	public Vector2 m_PositionToReach;
	public float 	m_ValueToReach;
	public float	m_Period; // in seconds
	public MaterialFader m_MatFader;
	public float	m_LossFactor;
	public float	m_DistanceFactor;
	public float	m_DistanceMax = 4;
	public float	m_SynchroFactor;
	public float	m_ChromaticPressedValue = -18;
	public float	m_ChromaticReleasedValue = 0;
	
	private	float	m_CurrentValue;
	private bool 	m_Recording;
	private Vector2 m_CurrentPos;
	private int		m_CurrentNbKeys;
	private float	m_CurrentDispersion;
	private float	m_CurrentTimer;
	private bool	m_TimerStarted;
	private float	m_CurrentSynchroFactor; /* 1 : total Sync, 0 : no Sync */
	

	// Use this for initialization
	void Start ()
	{
		Init();
		transform.position = new Vector3(m_PositionToReach.x, -m_PositionToReach.y); // DEBUG
	}
	
	public override void Init()
	{
		m_Recording = false;
		m_CurrentValue = 0;
		m_CurrentTimer = 0;
		m_TimerStarted = false;
		if (m_MatFader != null)
		{
			m_MatFader.m_FadeType = MaterialFader.E_FadeType.E_FADE_IN_OUT;
		}
	}
	
	void UpdateTimer()
	{
		if (m_TimerStarted)
		{
			m_CurrentTimer += Time.deltaTime;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_CurrentValue >= m_ValueToReach)
			return;
		
		updateFunction();
		
		InputAnalyzer inputAn = InputAnalyzer.GetInstance();
		int nbKeys = inputAn.GetNbKeysPressed();
		Vector2 pos = inputAn.GetInputPos();
		float dispersion = inputAn.GetDispersion();
		if (nbKeys > 0)
		{
			if (!m_Recording)
				StartRecord(pos, nbKeys, dispersion);
			else
				UpdateRecord(pos, nbKeys, dispersion);
		}
		else if (m_Recording)
		{
			EndRecord();
		}
	}
	
	private void StartRecord(Vector2 position, int nbKeys, float dispersion)
	{
		m_Recording = true;
		Vignetting vi = Camera.mainCamera.GetComponent<Vignetting>();
		if (vi)
			vi.chromaticAberration = m_ChromaticPressedValue;
	}
	
	private void UpdateRecord(Vector2 position, int nbKeys, float dispersion)
	{
	}
	
	private void EndRecord()
	{
		m_Recording = false;
		Vignetting vi = Camera.mainCamera.GetComponent<Vignetting>();
		if (vi)
			vi.chromaticAberration = m_ChromaticReleasedValue;
		++m_CurrentValue;
		if (m_CurrentValue >= m_ValueToReach)
		{
			m_CurrentValue = m_ValueToReach;
			Win();
		}
	}
	
	public override float GetCompletionFactor()
	{
		return m_CurrentValue / m_ValueToReach;
	}
}
