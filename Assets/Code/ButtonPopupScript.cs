using UnityEngine;
using System.Collections;

public class ButtonPopupScript : MonoBehaviour {
	//Attributs
	public Const.ACTION_TYPE actionButton = Const.ACTION_TYPE.CLOSE;
	public Vector3 maxShift = new Vector3(5, 5, 5);

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
			switch(this.actionButton)
			{
				case Const.ACTION_TYPE.CLOSE:
				{
	        		Destroy(this.transform.parent.gameObject);
					break;
				}
				case Const.ACTION_TYPE.POP_SAME:
				{
					Vector3 newPosition = new Vector3(
							this.transform.parent.position.x + Random.Range(-this.maxShift.x, this.maxShift.x),
							this.transform.parent.position.y + Random.Range(-this.maxShift.y, this.maxShift.y),
							this.transform.parent.position.z + Random.Range(0, this.maxShift.z));
					
					GameObject _popup = Resources.Load("Prefabs/Popup") as GameObject;
					GameObject instantiate = Instantiate(_popup, newPosition, this.transform.parent.rotation) as GameObject;
					instantiate.transform.parent = this.transform.parent.parent;
					break;
				}
			}
		});
	}
}
