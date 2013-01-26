using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public virtual void Init() {}
	
	public virtual float GetCompletionFactor()
	{
		return 0;
	}
}
