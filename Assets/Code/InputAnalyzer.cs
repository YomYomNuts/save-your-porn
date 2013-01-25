using UnityEngine;
using System.Collections;

public class InputAnalyzer : MonoBehaviour
{
	private ArrayList keyboardMapFR;

	// Use this for initialization
	void Start()
	{
		keyboardMapFR = new ArrayList();
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha1, 0, 0));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha2, 1, 0));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha3, 2, 0));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha4, 3, 0));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha5, 4, 0));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha6, 5, 0));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha7, 6, 0));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha8, 7, 0));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha9, 8, 0));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Alpha0, 9, 0));
		
		keyboardMapFR.Add(new KeyboardKey(KeyCode.A, 0, 1));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Z, 1, 1));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.E, 2, 1));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.R, 3, 1));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.T, 4, 1));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Y, 5, 1));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.U, 6, 1));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.I, 7, 1));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.O, 8, 1));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.P, 9, 1));
		
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Q, 0, 2));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.S, 1, 2));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.D, 2, 2));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.F, 3, 2));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.G, 4, 2));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.H, 5, 2));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.J, 6, 2));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.K, 7, 2));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.L, 8, 2));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.M, 9, 2));
		
		keyboardMapFR.Add(new KeyboardKey(KeyCode.W, 			0, 3));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.X, 			1, 3));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.C, 			2, 3));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.V, 			3, 3));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.B, 			4, 3));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.N, 			5, 3));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Comma,		6, 3));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Semicolon, 	7, 3));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Colon, 		8, 3));
		keyboardMapFR.Add(new KeyboardKey(KeyCode.Exclaim, 	9, 3));
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 averagePos = Vector2.zero;
		int nbKeysPressed = 0;
		/* update inputs */
		foreach(KeyboardKey key in keyboardMapFR)
		{
			key.State = Input.GetKey(key.Code);
			if (key.State)
			{
				++nbKeysPressed;
				averagePos += key.Position;
			}
		}
		if (nbKeysPressed > 0)
			averagePos /= nbKeysPressed;
		
		Vector3 newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		newPos.x = averagePos.x;
		newPos.y = -averagePos.y;
		
		this.transform.position = newPos;
		
	}
}
