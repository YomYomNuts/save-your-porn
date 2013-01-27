using UnityEngine;
using System.Collections;

public class Const {
	
    #region LAYERS
	
    public const int LAYER_DESKTOP = 8;
	
    #endregion
	
    #region ENUM
	
	public enum LEVELS
    {
		NOTHING,
        LEVEL_LOSE,
        LEVEL_INTRO,
        LEVEL_FIRSTREANIMATE
    };
    
	public enum ACTION_TYPE
    {
        CLOSE,
        POP_SAME,
        POP_RANDOM,
		BLACK_SCREEN,
		CRT_OFF
    };
	
	public enum TYPE_ANIMATION
	{
		ANIMATION_NORMAL,
		ANIMATION_RANDOM,
		ANIMATION_MANUAL
	};
	
	#endregion
}
