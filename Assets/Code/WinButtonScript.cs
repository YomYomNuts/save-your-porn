using UnityEngine;
using System.Collections;

public class WinButtonScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Initialisation du bouton
		this.AddTheClickListener();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	// Permet d'ajouter le listener pour le bouton
	protected void AddTheClickListener() {
		//Ajout du listener
		this.GetComponent<ClickListenerScript>().OnClicked += new ClickListenerScript.ActionEventClick(
		() => {
			Application.LoadLevel(PlayerPrefs.GetInt("choice"));
		});
	}
}
