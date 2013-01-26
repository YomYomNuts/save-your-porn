using UnityEngine;
using System.Collections;

public class RandomAnimatedScript : MonoBehaviour
{
	//vars for the whole sheet
	public int colCount = 4;
	public int rowCount = 4;
	
	//vars for animation
	private int rowNumber = 0;
	private int colNumber = 0;
	private int totalCells;
	//Maybe this should be a private var
	private Vector2 offset;
	private Vector2 size;
	
	// Use this for initialization
	void Start () {
		this.totalCells = this.colCount * this.rowCount;
		
		// Size of every cell
		size = new Vector2(1.0f / colCount, 1.0f / rowCount);
	}
	
	//Update
	void Update ()
	{
		SetSpriteAnimation(colCount, rowCount, rowNumber, colNumber, totalCells);
	}
	
	//SetSpriteAnimation
	void SetSpriteAnimation(int colCount ,int rowCount ,int rowNumber ,int colNumber,int totalCells)
	{
		// Calculate index
		int index = Random.Range(0, totalCells);
		
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
}