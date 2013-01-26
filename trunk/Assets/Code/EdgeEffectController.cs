using UnityEngine;
using System.Collections;

public class EdgeEffectController : MonoBehaviour
{
	
	public Objective m_Objective;
	public EdgeDetectEffect m_Target;
	
	private static float MAX_THRESHOLD = 1.5f;
	private static float MIN_THRESHOLD = 0;
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		float compFact = m_Objective.GetCompletionFactor();
		float threshold = MIN_THRESHOLD + compFact * compFact * compFact * compFact * MAX_THRESHOLD;
		threshold += Random.Range(-threshold/5f,threshold/5f);
		m_Target.threshold = threshold;
		if (compFact >= 1 && m_Target.enabled)
			m_Target.enabled = false;
	}
}
