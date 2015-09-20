using UnityEngine; 
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtenPressed : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
	public Image btn;
	public Sprite imgUp;
	public Sprite imgDown;
	public Color colorUp;
	public Color colorDown;

	public void OnPointerDown (PointerEventData eventData) {
		SetStatePressed ();
	}
	public void OnPointerUp (PointerEventData eventData) {
		SetStateNormal ();
	}
	public void OnPointerExit (PointerEventData eventData) {
		SetStateNormal ();
	}

	private void SetStateNormal()
	{
		btn.sprite = imgUp;
		btn.color = colorUp;
	}
	private void SetStatePressed()
	{
		btn.sprite = imgDown;
		btn.color = colorDown;
	}
}