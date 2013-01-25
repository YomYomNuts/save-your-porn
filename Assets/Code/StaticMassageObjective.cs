using UnityEngine;
using System.Collections;

/* objectif de massage statique : on doit appuyer plein de fois sur une zone donnée jusqu'à remplir une jauge */
public class StaticMassageObjective : MonoBehaviour
{
	/* user settable */
	public Vector2 m_PositionToReach;
	public float 	m_ValueToReach;
	public float	m_Frequency;
	
	private	float	m_CurrentValue;		
	private bool 	m_recording;
	private Vector2 m_CurrentPos;
	private int		m_CurrentNbKeys;
	private float	m_CurrentDispersion;

	// Use this for initialization
	void Start ()
	{
		Init();
		transform.position = new Vector3(m_PositionToReach.x, -m_PositionToReach.y); // DEBUG
	}
	
	void Init()
	{
		m_recording = false;
		m_CurrentValue = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		InputAnalyzer inputAn = InputAnalyzer.GetInstance();
		int nbKeys = inputAn.GetNbKeysPressed();
		Vector2 pos = inputAn.GetInputPos();
		float dispersion = inputAn.GetDispersion();
		if (nbKeys > 0)
		{
			if (!m_recording)
				StartRecord(pos, nbKeys, dispersion);
			else
				UpdateRecord(pos, nbKeys, dispersion);
		}
		else if (m_recording)
		{
			EndRecord();
		}
	}
	
	private void StartRecord(Vector2 position, int nbKeys, float dispersion)
	{
		m_recording = true;
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
		m_recording = false;
		
		/* compute score for current massage and add to score */
		float scoreToAdd = m_CurrentNbKeys * m_CurrentNbKeys * m_CurrentDispersion / (1 + (m_PositionToReach - m_CurrentPos).sqrMagnitude);
		m_CurrentValue += scoreToAdd;
		
		if (m_CurrentValue >= m_ValueToReach)
		{
			Win();
			Init();
		}
		else
		{
			/* DEBUG CODE */
			float newScale = (m_ValueToReach - m_CurrentValue) / m_ValueToReach;
			transform.localScale = new Vector3(newScale, newScale, 1);
			Debug.Log("Current value : " + m_CurrentValue);
			Debug.Log("Added : " + scoreToAdd);
			Debug.Log("Dispersion : " + m_CurrentDispersion);
		}
	}
}
