using UnityEngine;
using System.Collections;

/* objectif de massage statique : on doit appuyer plein de fois sur une zone donnée jusqu'à remplir une jauge */
public class StaticMassageObjective : Objective
{
	/* user settable */
	public Vector2 m_PositionToReach;
	public float 	m_ValueToReach;
	public float	m_Period; // in seconds
	public MaterialFader m_MatFader;
	public float	m_LossFactor;
	public float	m_DistanceFactor;
	
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
		if (m_Period == 0)
		{
			Debug.LogError("Static massage objective with 0 Period !");
			m_Period = 1;
		}
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
			while (m_CurrentTimer > m_Period)
			{
				m_CurrentTimer -= m_Period;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_CurrentValue >= m_ValueToReach)
			return;
		
		UpdateTimer();
		
		m_CurrentValue -= m_LossFactor;
		if (m_CurrentValue < 0)
			m_CurrentValue =0;
			
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
		
		/* DEBUG CODE */
		float newScale = (m_ValueToReach - m_CurrentValue) / m_ValueToReach;
		if (m_TimerStarted)
		{
			float distance = m_Period - m_CurrentTimer < m_CurrentTimer ? m_Period - m_CurrentTimer : m_CurrentTimer;
			float syncScaleFact = (m_Period) / (distance + m_Period/2);
			newScale *= syncScaleFact;
		}
		transform.localScale = new Vector3(newScale, newScale, 1);
	}
	
	private void StartRecord(Vector2 position, int nbKeys, float dispersion)
	{
		if (!m_TimerStarted)
		{
			m_TimerStarted = true;
			m_CurrentSynchroFactor = 1;
		}
		else
		{
			float distance = m_Period - m_CurrentTimer < m_CurrentTimer ? m_Period - m_CurrentTimer : m_CurrentTimer;
			distance *= 10; //en dixièmes de secondes;
			Debug.Log("Distance : " + distance);
			m_CurrentSynchroFactor = 1 / (1 + distance*distance);
		}
		m_Recording = true;
		m_CurrentNbKeys = nbKeys;
		m_CurrentPos = position;
		m_CurrentDispersion = dispersion;
	}
	
	private void UpdateRecord(Vector2 position, int nbKeys, float dispersion)
	{
		if (nbKeys >= m_CurrentNbKeys)
		{
			m_CurrentNbKeys = nbKeys;
			m_CurrentPos = position;
			m_CurrentDispersion = dispersion;
		}
	}
	
	private void Win()
	{
		Debug.Log("Win !");
	}
	
	private void EndRecord()
	{
		m_Recording = false;
		
		/* compute score for current massage and add to score */
		float scoreToAdd = m_CurrentNbKeys * m_CurrentNbKeys * m_CurrentDispersion * (2* (m_CurrentSynchroFactor - 0.5f)) - m_DistanceFactor *(m_PositionToReach - m_CurrentPos).sqrMagnitude;
		m_CurrentValue += scoreToAdd;
		if (m_CurrentValue < 0)
			m_CurrentValue = 0;
		if (m_MatFader != null && !m_MatFader.IsActivated())
		{
			m_MatFader.m_maxFadeValue = GetCompletionFactor();
			if (m_MatFader.m_maxFadeValue > 1)
			{
				m_MatFader.m_maxFadeValue = 1;
				m_MatFader.m_FadeType = MaterialFader.E_FadeType.E_FADE_IN;
			}
			m_MatFader.Activate();
		}
		
		if (m_CurrentValue >= m_ValueToReach)
		{
			m_CurrentValue = m_ValueToReach;
			Win();
		}
		else
		{
			Debug.Log("Current value : " + m_CurrentValue);
			Debug.Log("Added : " + scoreToAdd);
			Debug.Log("Dispersion : " + m_CurrentDispersion);
			Debug.Log("Synchro : " + m_CurrentSynchroFactor);
		}
	}
	
	public override float GetCompletionFactor()
	{
		return m_CurrentValue / m_ValueToReach;
	}
}
