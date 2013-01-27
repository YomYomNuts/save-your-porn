using UnityEngine;
using System.Collections;

public class AnimatedScript : MonoBehaviour
{
	//vars for the whole sheet
	public int colCount =  4;
	public int rowCount =  4;
	public int speedAnimation = 10;
	public bool deactivateOnEnd = false;
	
	//vars for animation
	private int rowNumber = 0;
	private int colNumber = 0;
	private int totalCells;
	//Maybe this should be a private var
	private Vector2 offset;
	private Vector2 size;
	private float timer;
	public Const.TYPE_ANIMATION typeAnimation = Const.TYPE_ANIMATION.ANIMATION_NORMAL;
	private int animId = 0;
	
	// Use this for initialization
	void Start () {
		this.totalCells = this.colCount * this.rowCount;
		
		Reset();
		
		// Size of every cell
		size = new Vector2(1.0f / colCount, 1.0f / rowCount);
	}
	
	public void Reset()
	{
		timer = 0;
	}
	
	//Update
	void Update ()
	{
		SetSpriteAnimation();
	}
	
	//Update Manual
	public void updateManual()
	{
		animId++;
	}
	
	//SetSpriteAnimation
	void SetSpriteAnimation()
	{
		int index = 0;
		if (typeAnimation == Const.TYPE_ANIMATION.ANIMATION_NORMAL)
		{
			// Calculate index
			timer += Time.deltaTime * speedAnimation;
			index  = (int)(timer);
			// Repeat when exhausting all cells
			if (deactivateOnEnd && index >= totalCells)
				gameObject.SetActive(false);
			else
				index = index % totalCells;
		}
		else if (typeAnimation == Const.TYPE_ANIMATION.ANIMATION_RANDOM)
		{
			// Calculate index
			index = Random.Range(0, totalCells);
		}
		else if (typeAnimation == Const.TYPE_ANIMATION.ANIMATION_MANUAL)
		{
			// Calculate index
			index = animId;
		}
		
		// split into horizontal and vertical index
		var uIndex = index % colCount;
		var vIndex = index / colCount;
		
		// build offset
		// v coordinate is the bottom of the image in opengl so we need to invert.
		float offsetX = (uIndex + colNumber) * size.x;
		float offsetY = (1.0f - size.y) - (vIndex + rowNumber) * size.y;
		Vector2 offset = new Vector2(offsetX, offsetY);
		
		renderer.material.SetTextureOffset("_MainTex", offset);
		renderer.material.SetTextureScale("_MainTex", size);
	}
	
	public static void updateAnimation()
	{
		foreach(GameObject g in FindObjectsOfType(typeof(GameObject)))
		{
			if (g.layer == Const.LAYER_EFFECT && g.GetComponent<AnimatedScript>())
				g.GetComponent<AnimatedScript>().updateManual();
		}
	}
}