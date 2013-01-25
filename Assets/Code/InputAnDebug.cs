using UnityEngine;
using System.Collections;

public class InputAnDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		Vector2 averagePos = InputAnalyzer.GetInstance().GetInputPos();
		
		/* DEBUG CODE */
		Vector3 newPos = Vector3.zero;
		newPos.x = averagePos.x;
		newPos.y = -averagePos.y;
		
		this.transform.position = newPos;
	}
}
