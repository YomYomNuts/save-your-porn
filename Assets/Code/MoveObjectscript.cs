using UnityEngine;
using System.Collections;

public class MoveObjectscript : MonoBehaviour {
	public float timeWaitingBeforeStart = 5f;
	public Vector3 vectorTranslation = new Vector3(0, -0.05f, 0);
	public float timeMoving = 22;
	public Const.LEVELS levelToLoadOnClick;
	
	private float timer;
	private bool start;

	// Use this for initialization
	void Start () {
		// Initialisation du bouton
		this.AddTheClickListener();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (!start && timer >= timeWaitingBeforeStart)
		{
			start = true;
			timer = 0;
		}
		if (start && timer <= timeMoving)
			this.transform.position += vectorTranslation;
	}
	
	// Permet d'ajouter le listener pour le bouton
	protected void AddTheClickListener() {
		//Ajout du listener
		this.GetComponent<ClickListenerScript>().OnClicked += new ClickListenerScript.ActionEventClick(
		() => {
			if(start && timer > timeMoving)
				Application.LoadLevel((int)levelToLoadOnClick);
		});
	}
}
