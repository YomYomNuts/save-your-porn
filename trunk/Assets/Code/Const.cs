using UnityEngine;
using System.Collections;

public class Const {
	
    #region LAYERS
	
    public const int LAYER_DESKTOP = 8;
    public const int LAYER_EFFECT = 9;
    public const int LAYER_POPUP = 10;
	
    #endregion
	
    #region ENUM
	
	public enum LEVELS
    {
		NOTHING,
        LEVEL_INTRO,
        LEVEL_LOSE,
        LEVEL_FIRSTREANIMATE,
		LEVEL_POPUP,
		LEVEL_BSOD,
		LEVEL_CHOICE,
		LEVEL_WIN,
		LEVEL_ENDPORN,
		LEVEL_ENDWORK,
		LEVEL_GENERIQUE
    };
    
	public enum END_ACTION_TYPE
    {
        LOAD_LEVEL,
        DO_COMPRESSIONS,
        DO_INSUFFLATIONS,
		DO_MASSAGE
    };
    
	public enum POPUP_ACTION_TYPE
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
	
	public enum ANIM_SMILEY
	{
		SMILEY_1,
		SMILEY_2,
		SMILEY_3,
		SMILEY_4,
		SMILEY_5,
		SMILEY_6
	};
	
	public enum ANIM_HEART
	{
		HEART_1,
		HEART_2
	};
	
	#endregion
}
