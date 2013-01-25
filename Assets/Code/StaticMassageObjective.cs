using UnityEngine;
using System.Collections;

/* objectif de massage statique : on doit appuyer plein de fois sur une zone donnée jusqu'à remplir une jauge */
public class StaticMassageObjective : MonoBehaviour
{
	public Vector2 m_PositionToReach;
	
	/* counting achievement percentage */
	public float 	m_ValueToReach;
	private	float	m_CurrentValue;		
	
	private bool 	m_recording;
	private Vector2 m_CurrentPos;
	private int		m_CurrentNbKeys;

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
		if (nbKeys > 0)
		{
			if (!m_recording)
				StartRecord(pos, nbKeys);
			else
				UpdateRecord(pos, nbKeys);
		}
		else if (m_recording)
		{
			EndRecord();
		}
	}
	
	private void StartRecord(Vector2 position, int nbKeys)
	{
		m_recording = true;
		m_CurrentNbKeys = nbKeys;
		m_CurrentPos = position;
	}
	
	private void UpdateRecord(Vector2 position, int nbKeys)
	{
		if (nbKeys >= m_CurrentNbKeys)
		{
			m_CurrentNbKeys = nbKeys;
			m_CurrentPos = position;
		}
	}
	
	private void Win()
	{
		Debug.Log("Win !");
	}
	
	private void EndRecord()
	{
		m_recording = false;
		m_CurrentValue += m_CurrentNbKeys * m_CurrentNbKeys / (1 + (m_PositionToReach - m_CurrentPos).sqrMagnitude);
		
		
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
		}
	}
}
