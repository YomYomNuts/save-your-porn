using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {
	// Attributs
	public float limitTimer = -1;
	private float timer;
	public Const.END_ACTION_TYPE actionWin;
	public Const.LEVELS levelLoadIfWin;
	public GameObject objectTakeActionIfWin;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		updateFunction();
	}
	
	public void updateFunction()
	{
		timer += Time.deltaTime;
		if (timer > this.limitTimer && this.limitTimer != - 1)
			Application.LoadLevel((int)Const.LEVELS.LEVEL_LOSE - 1);
	}
	
	public virtual void Init() {}
	
	public virtual float GetCompletionFactor()
	{
		return 0;
	}
	
	protected void Win()
	{
		AnimatedScript.updateAnimation();
		this.enabled = false;
		switch(actionWin)
		{
		case Const.END_ACTION_TYPE.LOAD_LEVEL:
			Application.LoadLevel((int)levelLoadIfWin - 1);
			break;
		case Const.END_ACTION_TYPE.DO_COMPRESSIONS:
			objectTakeActionIfWin.GetComponent<SimpleStaticMassageObjective>().enabled = true;
			break;
		case Const.END_ACTION_TYPE.DO_INSUFFLATIONS:
			objectTakeActionIfWin.GetComponent<MicHandle>().enabled = true;
			objectTakeActionIfWin.GetComponent<BreathScript>().enabled = true;
			break;
		case Const.END_ACTION_TYPE.DO_MASSAGE:
			objectTakeActionIfWin.GetComponent<CircularMassageObjective>().enabled = true;
			break;
		}
	}
}
