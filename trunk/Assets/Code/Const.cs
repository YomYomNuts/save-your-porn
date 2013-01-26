using UnityEngine;
using System.Collections;

public class Const {
	
    #region LAYERS
	
    public const int LAYER_DESKTOP = 8;
	
    #endregion
	
	#region LEVELS
	
	public const int LEVEL_LOSE = 0;
	
	#endregion
	
    #region ENUM
    
	public enum ACTION_TYPE
    {
        CLOSE,
        POP_SAME,
        POP_RANDOM,
		BLACK_SCREEN
    };
	
	public enum TYPE_ANIMATION
	{
		ANIMATION_NORMAL,
		ANIMATION_RANDOM,
		ANIMATION_MANUAL
	};
	
	#endregion
}
