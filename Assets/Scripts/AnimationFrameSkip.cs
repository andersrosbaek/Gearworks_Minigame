using UnityEngine;
using System.Collections;

public class AnimationFrameSkip : MonoBehaviour {

	public float startPositionInCommaPercentage = 0;
	private Animator animator = null;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		animator.ForceStateNormalizedTime(startPositionInCommaPercentage);
	}
}
