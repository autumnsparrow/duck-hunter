using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	
	private bool touched;
	private int pointerID;
	private bool canFire; 

	private Image fire;
	private Color origColor;

	void Start(){

		fire = GetComponent<Image> ();
		origColor = fire.color;
	}
	
	void Awake () {
		touched = false;
		canFire = false;
	}
	
	public void OnPointerDown (PointerEventData data) {
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			canFire = true;

		}
	}
	
	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointerID) {
			canFire = false;
			touched = false;
			//fire.color = Color.blue;
		}
	}
	
	public bool CanFire () {
		return canFire;
	}

	public void enableColor(bool v){
		if (v) {
			fire.color = origColor;
		} else {
			fire.color = Color.gray;
		}
	}



}