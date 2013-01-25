using UnityEngine;
using System.Collections;

public class InputAnalyzer
{
	private ArrayList m_keyboardMapFR;
	private Vector2 m_averageInputPos;
	private int m_nbKeysPressed;
	
	private static InputAnalyzer m_instance;
	
	public static InputAnalyzer GetInstance()
	{
		if (m_instance == null)
			m_instance = new InputAnalyzer();
		return m_instance;
	}
	
	private InputAnalyzer()
	{
	}
	
	public Vector2 GetInputPos()
	{
		return m_averageInputPos;
	}
	
	public int GetNbKeysPressed()
	{
		return m_nbKeysPressed;
	}

	// Use this for initialization
	public void Start()
	{
		m_keyboardMapFR = new ArrayList();
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha1, 0, 0));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha2, 1, 0));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha3, 2, 0));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha4, 3, 0));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha5, 4, 0));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha6, 5, 0));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha7, 6, 0));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha8, 7, 0));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha9, 8, 0));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha0, 9, 0));
		
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.A, 0, 1));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Z, 1, 1));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.E, 2, 1));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.R, 3, 1));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.T, 4, 1));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Y, 5, 1));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.U, 6, 1));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.I, 7, 1));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.O, 8, 1));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.P, 9, 1));
		
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Q, 0, 2));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.S, 1, 2));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.D, 2, 2));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.F, 3, 2));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.G, 4, 2));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.H, 5, 2));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.J, 6, 2));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.K, 7, 2));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.L, 8, 2));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.M, 9, 2));
		
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.W, 			0, 3));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.X, 			1, 3));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.C, 			2, 3));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.V, 			3, 3));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.B, 			4, 3));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.N, 			5, 3));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Comma,		6, 3));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Period,	 	7, 3));
		m_keyboardMapFR.Add(new KeyboardKey(KeyCode.Slash, 		8, 3));
	
	}
	
	// Update is called once per frame
	public void Update ()
	{
		m_averageInputPos = Vector2.zero;
		m_nbKeysPressed = 0;
		/* update inputs */
		foreach(KeyboardKey key in m_keyboardMapFR)
		{
			key.State = Input.GetKey(key.Code);
			if (key.State)
			{
				++m_nbKeysPressed;
				m_averageInputPos += key.Position;
			}
		}
		if (m_nbKeysPressed > 0)
			m_averageInputPos /= m_nbKeysPressed;
		else
			m_averageInputPos = new Vector2(-10000, -10000);
	}
}
