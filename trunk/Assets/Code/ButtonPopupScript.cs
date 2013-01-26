using UnityEngine;
using System.Collections;

public class ButtonPopupScript : MonoBehaviour {
	//Attributs
	public bool launchActionOnMouseEnter = false;
	public Const.ACTION_TYPE actionButton = Const.ACTION_TYPE.CLOSE;
	public Vector3 maxShift = new Vector3(15, 15, 15);
	public int numberOfPopupLaunch = 50;
	public int numberOfClickLaunchSecondAction = -1;
	public Const.ACTION_TYPE secondActionButton = Const.ACTION_TYPE.BLACK_SCREEN;
	private int counterTransition = 0;

	// Use this for initialization
	void Start () {
		// Initialisation du bouton
		this.AddTheClickListener();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	// Quand la souris passe sur le bouton, on exécute les actions adéquates
	void OnMouseEnter()
	{
		if (this.launchActionOnMouseEnter)
			doActionButton();
	}
	
	// Permet d'ajouter le listener pour le bouton
	protected void AddTheClickListener() {
		//Ajout du listener
		this.GetComponent<ClickListenerScript>().OnClicked += new ClickListenerScript.ActionEventClick(
		() => {
			if (!this.launchActionOnMouseEnter)
				doActionButton();
		});
	}
	
	// Action of the button
	public void doActionButton()
	{
		PopupScript thisPopup = this.transform.parent.gameObject.GetComponent<PopupScript>();
		if (thisPopup.counterChildPopup == 0)
		{
			Const.ACTION_TYPE currentAction = this.actionButton;
			if (this.counterTransition == this.numberOfClickLaunchSecondAction)
				currentAction = this.secondActionButton;
			
			switch(currentAction)
			{
				case Const.ACTION_TYPE.CLOSE:
				{
					if (thisPopup.parentPopup != null)
						thisPopup.parentPopup.counterChildPopup--;
        			Destroy(this.transform.parent.gameObject);
					break;
				}
				case Const.ACTION_TYPE.POP_SAME:
				{
					string namePrefab = this.transform.parent.gameObject.name.Replace("(Clone)", "");
					GameObject _popup = Resources.Load("Prefabs/Popups/" + namePrefab) as GameObject;
					popPopup(thisPopup, _popup);
					break;
				}
				case Const.ACTION_TYPE.POP_RANDOM:
				{
					Object[] _popups = Resources.LoadAll("Prefabs/Popups");
					
					for (int i = 0; i < numberOfPopupLaunch; i++)
					{
						popPopup(thisPopup, _popups[Random.Range(0, _popups.Length)]);
					}
					break;
				}
				case Const.ACTION_TYPE.BLACK_SCREEN:
				{
					foreach(GameObject g in FindObjectsOfType(typeof(GameObject)))
					{
						if (g.layer == Const.LAYER_DESKTOP && g.renderer != null)
							g.renderer.enabled = false;
					}
					break;
				}
			}
			this.counterTransition++;
		}
	}
	
	// Pop a popup
	public void popPopup(PopupScript thisPopup, Object objectToPop)
	{
		Vector3 newPosition = new Vector3(
				this.transform.parent.position.x + Random.Range(-this.maxShift.x, this.maxShift.x),
				this.transform.parent.position.y + Random.Range(-this.maxShift.y, this.maxShift.y),
				this.transform.parent.position.z + Random.Range(0, this.maxShift.z));
	
		GameObject instantiate = Instantiate(objectToPop, newPosition, this.transform.parent.rotation) as GameObject;
		instantiate.transform.parent = this.transform.parent.parent;
		instantiate.GetComponent<PopupScript>().parentPopup = thisPopup;
		thisPopup.counterChildPopup++;
	}
}
