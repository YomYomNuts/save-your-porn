using UnityEngine;
using System.Collections;

public class ButtonPopupScript : MonoBehaviour {
	//Attributs
	public Transform transformDesktop;
	public bool launchActionOnMouseEnter = false;
	public Const.ACTION_TYPE actionButton = Const.ACTION_TYPE.CLOSE;
	public Vector3 maxShift = new Vector3(15, 15, 15);
	public int numberOfPopupLaunch = 50;
	public float speedAppararitionPopup = 0.000001f;
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
		if (this.launchActionOnMouseEnter && isClosest())
			StartCoroutine("doActionButton");
	}
	
	// Permet d'ajouter le listener pour le bouton
	protected void AddTheClickListener() {
		//Ajout du listener
		this.GetComponent<ClickListenerScript>().OnClicked += new ClickListenerScript.ActionEventClick(
		() => {
			if (!this.launchActionOnMouseEnter && isClosest())
				StartCoroutine("doActionButton");
		});
	}
	
	// Savoir si c'est le plus proche de la caméra
	public bool isClosest()
	{
		foreach(GameObject g in FindObjectsOfType(typeof(GameObject)))
		{
			if (g.layer == Const.LAYER_DESKTOP && g.renderer != null && g.transform.position.z > this.transform.position.z)
				return false;
		}
		return true;
	}
	
	// Action of the button
	public IEnumerator doActionButton()
	{
		Const.ACTION_TYPE currentAction = this.actionButton;
		if (this.counterTransition == this.numberOfClickLaunchSecondAction)
			currentAction = this.secondActionButton;
		
		switch(currentAction)
		{
			case Const.ACTION_TYPE.CLOSE:
			{
    			Destroy(this.transform.parent.gameObject);
				break;
			}
			case Const.ACTION_TYPE.POP_SAME:
			{
				string namePrefab = this.transform.parent.gameObject.name.Replace("(Clone)", "");
				GameObject _popup = Resources.Load("Prefabs/Popups/" + namePrefab) as GameObject;
				popPopup(_popup);
				break;
			}
			case Const.ACTION_TYPE.POP_RANDOM:
			{
				Object[] _popups = Resources.LoadAll("Prefabs/Popups");
				
				for (int i = 0; i < numberOfPopupLaunch; i++)
				{
					popPopup((_popups[Random.Range(0, _popups.Length - 1)] as GameObject));
					yield return new WaitForSeconds(speedAppararitionPopup);
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
	
	// Pop a popup
	public void popPopup(GameObject objectToPop)
	{
		float posX = this.transform.parent.position.x + Random.Range(-this.maxShift.x, this.maxShift.x);
		float posY = this.transform.parent.position.y + Random.Range(-this.maxShift.y, this.maxShift.y);
		float posZ = this.transform.parent.position.z + Random.Range(0, this.maxShift.z);
		
		/*if (posX + objectToPop.transform.localScale.x / 2 >= transformDesktop.position.x + transformDesktop.localScale.x / 2)
			posX = transformDesktop.position.x + transformDesktop.localScale.x;
		else if (posX - objectToPop.transform.localScale.x / 2 <= transformDesktop.position.x - transformDesktop.localScale.x / 2)
			posX = transformDesktop.position.x + transformDesktop.localScale.x;*/
		
		if (posY + objectToPop.transform.localScale.y / 2 >= transformDesktop.position.y + transformDesktop.localScale.y / 2)
			posY = transformDesktop.position.y - transformDesktop.localScale.y / 2 - objectToPop.transform.localScale.y / 2;
		else if (posY - objectToPop.transform.localScale.y / 2 <= transformDesktop.position.y - transformDesktop.localScale.y / 2)
			posY = transformDesktop.position.y + transformDesktop.localScale.y / 2 + objectToPop.transform.localScale.y / 2;
		
		Vector3 newPosition = new Vector3(posX, posY, posZ);
		GameObject instantiate = Instantiate(objectToPop, newPosition, this.transform.parent.rotation) as GameObject;
		instantiate.transform.parent = this.transform.parent.parent;
	}
}
