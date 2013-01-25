using UnityEngine;
using System.Collections;

public class Engine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		InputAnalyzer.GetInstance().Start();
	}
	
	// Update is called once per frame
	void Update ()
	{
		InputAnalyzer.GetInstance().Update();
	}
}
