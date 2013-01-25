using UnityEngine;
using System.Collections;

public class KeyboardKey
{
	public Vector2 Position
	{
		get;
		set;
	}
	
	public bool State //true : pressed, false : released 
	{
		get;
		set;
	}
	
	public KeyCode Code
	{
		get;
		set;
	}
	
	public KeyboardKey()
	{
		State = false;
		Position = new Vector2(0,0);
	}
	
	public KeyboardKey(KeyCode code, float x, float y)
	{
		Code = code;
		State = false;
		Position = new Vector2(x,y);
	}
}
