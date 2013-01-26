using UnityEngine;
using System.Collections;

/// <summary>
/// Script de récupération de click
/// </summary>
public class ClickListenerScript : MonoBehaviour {
	#region Attributes
    // Définition du type d'évènement
    public delegate void ActionEventClick();
	
	/// <summary>
	/// Liste des méthodes à appeler au click
	/// </summary>
	public event ActionEventClick OnClicked;
	#endregion
	
	#region Unity Methods
	// On utilise OnMouseUp car MouseDown ou GetButton risque d'activer le bouton plusieurs,
	// ce qui est gênant pour les boutons suivant et précédent de la vue Info
	void OnMouseUp() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(this.collider.bounds.IntersectRay(ray)) {
			Click();
		}
	}
	#endregion
	
	#region Methods
	// Méthode appelé lors du click
	public void Click() {
		if(OnClicked != null) {
			OnClicked();
		}
	}
	#endregion
	
}
