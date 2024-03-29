using UnityEngine;
using System.Collections;

public class ButtonPopupScript : MonoBehaviour {
	//Attributs
	public bool launchActionOnMouseEnter = false;
	public Const.POPUP_ACTION_TYPE actionButton = Const.POPUP_ACTION_TYPE.CLOSE;
	public Vector3 maxShift = new Vector3(15, 15, 15);
	public int numberOfPopupLaunch = 50;
	public float speedAppararition = 0.000001f;
	public float speedChangement = 0.01f;
	public int numberOfClickLaunchSecondAction = -1;
	public Const.POPUP_ACTION_TYPE secondActionButton = Const.POPUP_ACTION_TYPE.BLACK_SCREEN;
	public Const.LEVELS loadLevelAtTheEnd;
	public bool saveTheButtonClick;
	public Const.LEVELS levelToSave;
	public AudioSource	crtOffSource;
	
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
			if (g.layer == Const.LAYER_POPUP && (g.transform.position.z > this.transform.position.z || this.gameObject.layer == Const.LAYER_DESKTOP))
				return false;
		}
		return true;
	}
	
	// Action of the button
	public IEnumerator doActionButton()
	{
		Const.POPUP_ACTION_TYPE currentAction = this.actionButton;
		if (this.counterTransition == this.numberOfClickLaunchSecondAction)
			currentAction = this.secondActionButton;
		
		switch(currentAction)
		{
		case Const.POPUP_ACTION_TYPE.CLOSE:
			Destroy(this.transform.parent.gameObject);
			break;
		case Const.POPUP_ACTION_TYPE.POP_SAME:
			string namePrefab = this.transform.parent.gameObject.name.Replace("(Clone)", "");
			GameObject _popup = Resources.Load("Prefabs/Popups/" + namePrefab) as GameObject;
			popPopup(_popup);
			break;
		case Const.POPUP_ACTION_TYPE.POP_RANDOM:
			Object[] _popups = Resources.LoadAll("Prefabs/Popups", typeof(GameObject));
			
			for (int i = 0; i < numberOfPopupLaunch; i++)
			{
				int indice = Random.Range(0, _popups.Length - 1);
				popPopup((_popups[indice] as GameObject));
				yield return new WaitForSeconds(speedAppararition);
			}
			break;
		case Const.POPUP_ACTION_TYPE.BLACK_SCREEN:
			foreach(GameObject g in FindObjectsOfType(typeof(GameObject)))
			{
				if ((g.layer == Const.LAYER_DESKTOP || g.layer == Const.LAYER_POPUP) && g.renderer != null)
					g.renderer.enabled = false;
			}
			break;
		case Const.POPUP_ACTION_TYPE.CRT_OFF:
			Camera mainCamera = FindObjectOfType(typeof(Camera)) as Camera;
			Transform transformDesktop = this.transform.parent.parent;
			if (crtOffSource)
			{
				crtOffSource.Play();
			}
			while(transformDesktop.localScale.y > 0)
			{
				transformDesktop.localScale = new Vector3(transformDesktop.localScale.x, transformDesktop.localScale.y - speedChangement, transformDesktop.localScale.z);
				mainCamera.GetComponent<ScreenOverlay>().intensity += speedChangement * 6;
				yield return new WaitForSeconds(speedAppararition);
			}
			while(transformDesktop.localScale.x > 0)
			{
				transformDesktop.localScale = new Vector3(transformDesktop.localScale.x - speedChangement, transformDesktop.localScale.y, transformDesktop.localScale.z);
				mainCamera.GetComponent<ScreenOverlay>().intensity -= speedChangement * 10;
				yield return new WaitForSeconds(speedAppararition);
			}
			break;
		}
		
		if (saveTheButtonClick)
			PlayerPrefs.SetInt("choice", (int)levelToSave - 1);
		// On joue le son associé
		if(this.audio != null && this.audio.clip != null) {
			//AudioSource.PlayClipAtPoint(this.audio.clip, this.transform.position);
		}
		if (loadLevelAtTheEnd != Const.LEVELS.NOTHING && (this.numberOfClickLaunchSecondAction == -1 || this.counterTransition == this.numberOfClickLaunchSecondAction))
			Application.LoadLevel((int)loadLevelAtTheEnd - 1);
		
		this.counterTransition++;
	}
	
	// Pop a popup
	public void popPopup(GameObject objectToPop)
	{
		Transform transformDesktop = this.transform.parent.parent;
		Vector3 positionClosest = new Vector3(-10000, -10000, -10000);
		
		foreach(GameObject g in FindObjectsOfType(typeof(GameObject)))
		{
			if ((g.layer == Const.LAYER_POPUP || g.layer == Const.LAYER_DESKTOP) && g.transform.position.z > positionClosest.z)
				positionClosest = g.transform.position;
		}
		
		float posX = Random.Range(-this.maxShift.x, this.maxShift.x);
		float posY = Random.Range(-this.maxShift.y, this.maxShift.y);
		float posZ = positionClosest.z + Random.Range(0, this.maxShift.z);
		
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
