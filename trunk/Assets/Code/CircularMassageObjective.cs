using UnityEngine;
using System.Collections;

/* objectif de massage circulaire : on doit suivre un point qui parcourt un cercle */
public class CircularMassageObjective : Objective
{
	/* user settable */
	public Vector2 	m_CenterPosition;
	public Vector2 	m_Radius;
	public float	m_ValueToReach;
	public bool		m_Clockwise;
	public float 	m_Period; // in seconds
	public float	m_ConstantLossFactor;
	public float	m_NbKeysFactor;
	private Vector2 decalPos;
	private Vector2 factorPos;

	private float	m_Timer;
	private	float	m_CurrentValue = 0;		
	private Vector2 m_CurrentPos = Vector2.zero;
	private static float SCORE_FACTOR = 10;

	// Use this for initialization
	void Start ()
	{
		Init();
	}
	
	public override void Init()
	{
		if (m_Period == 0)
		{
			Debug.LogError("Circular massage objective with 0 Period !");
			m_Period = 1;
		}
		else if (m_ConstantLossFactor < 0)
		{
			Debug.LogError("Circular massage objective with negative loss factor !");
			m_ConstantLossFactor = 0;
		}
		m_CurrentValue = 0;
		m_Timer = 0;
		
		m_CurrentPos.x = m_CenterPosition.x + m_Radius.x;
		m_CurrentPos.y = m_CenterPosition.y;
		
		float height = Camera.mainCamera.orthographicSize;
		float width = height * 16 / 9;
		factorPos.x = width / InputAnalyzer.GetNbColumns();
		factorPos.y = height / InputAnalyzer.GetNbLines();
		
		decalPos.x = -width / 2;
		decalPos.y = -height / 2;
	}
	
	void UpdateTargetMove()
	{
		m_Timer += Time.deltaTime;
		while (m_Timer > m_Period)
			m_Timer -= m_Period;
		
		float timer = m_Clockwise ? -m_Timer : m_Timer;
		timer *= 2 * Mathf.PI / m_Period;
		m_CurrentPos.x = m_CenterPosition.x + m_Radius.x * (Mathf.Cos(timer));
		m_CurrentPos.y = m_CenterPosition.y + m_Radius.y * (Mathf.Sin(timer));
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_CurrentValue >= m_ValueToReach)
			return;
		
		updateFunction();
		UpdateTargetMove();
		InputAnalyzer inputAn = InputAnalyzer.GetInstance();
		int nbKeys = inputAn.GetNbKeysPressed();
		Vector2 pos = inputAn.GetInputPos();
		float dispersion = inputAn.GetDispersion();
		if (nbKeys > 0)
		{
			float nbKeysFactor = 1 + m_NbKeysFactor * (nbKeys-1);
			float scoreToAdd = nbKeysFactor * dispersion / (1 + (m_CurrentPos - pos).sqrMagnitude) / SCORE_FACTOR;
			m_CurrentValue += scoreToAdd;
		}
		m_CurrentValue -= m_ConstantLossFactor;
		if (m_CurrentValue < 0)
			m_CurrentValue = 0;
		else if (m_CurrentValue >= m_ValueToReach)
		{
			Win();
		}
		/* DEBUG CODE */
		float newScale = (m_ValueToReach - m_CurrentValue) / m_ValueToReach;
		transform.localScale = new Vector3(newScale, newScale, 1);
		transform.position = new Vector3(decalPos.x + m_CurrentPos.x * factorPos.x, -(decalPos.y + m_CurrentPos.y * factorPos.y), 1f);
	}
	
	public override float GetCompletionFactor()
	{
		return m_CurrentValue / m_ValueToReach;
	}
	
	public Vector2 GetTargetPos()
	{
		return m_CurrentPos;
	}
}
