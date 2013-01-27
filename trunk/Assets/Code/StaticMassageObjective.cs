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
	public float	m_DistanceMax = 4;
	public float	m_SynchroFactor;
	public BlipManager	m_BlipManager;
	public float	m_BlipMaxDistance = 1.5f;
	public float	m_noBipMinScore = 5;
	
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
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_CurrentValue >= m_ValueToReach)
			return;
		
		updateFunction();
		UpdateTimer();
		
		m_CurrentValue -= m_LossFactor;
		if (m_CurrentValue < 0)
			m_CurrentValue = 0;
		
		if (m_CurrentValue < m_noBipMinScore && m_BlipManager)
			m_BlipManager.PlayBiiip();
			
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
			float distance = (m_Period - m_CurrentTimer) / m_Period;
			if (distance < 0)
				distance = 0;
			newScale *= distance;
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
			float syncDistance = Mathf.Abs(m_Period - m_CurrentTimer);
			float distance = (position - m_PositionToReach).sqrMagnitude;
			if (distance < m_BlipMaxDistance && m_BlipManager)
				m_BlipManager.PlayBip();			
			if (syncDistance > m_DistanceMax)
				m_CurrentSynchroFactor = 1;
			else
			{
				syncDistance *= 10; //en dixièmes de secondes;
				Debug.Log("sync Distance : " + syncDistance);
				m_CurrentSynchroFactor = 1 / (1 + syncDistance*syncDistance);
			}
			
		}
		m_Recording = true;
		m_CurrentNbKeys = nbKeys;
		m_CurrentPos = position;
		m_CurrentDispersion = dispersion;
		m_CurrentTimer = 0;
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
	
	private void EndRecord()
	{
		m_Recording = false;
		
		/* compute score for current massage and add to score */
		float synchroFactor = 1 - (2 * m_SynchroFactor * (1-m_CurrentSynchroFactor));
		float scoreToAdd = m_CurrentNbKeys * m_CurrentNbKeys * m_CurrentDispersion * synchroFactor - m_DistanceFactor *(m_PositionToReach - m_CurrentPos).sqrMagnitude;
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
