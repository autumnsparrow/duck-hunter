using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {
	
	public float smoothing;
	
	private Vector2 origin;
	private Vector2 direction;
	private Vector2 smoothDirection;
	private bool touched;
	private int pointerID;

	private Image backgroudImage ;
	private Image joyStickImage;
	private Vector3 inputVector;

	void Start(){
		backgroudImage = 	GetComponent<Image>();
		joyStickImage = transform.GetChild (0).GetComponent<Image> ();
	}
	
	void Awake () {
		direction = Vector2.zero;
		touched = false;
	}
	
	public void OnPointerDown (PointerEventData data) {
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			origin = data.position;
		}



	}
	
	public void OnDrag (PointerEventData data) {


		if (data.pointerId == pointerID) {
			Vector2 currentPosition = data.position;
			Vector2 directionRaw = currentPosition - origin;
			direction = directionRaw.normalized;


			// touchpad 
			Vector2 pos;
			if(RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroudImage.rectTransform,
			                                                           data.position,
			                                                           data.pressEventCamera,
			                                                           out pos)){
				Debug.Log("Transform:"+pos);
				float w = backgroudImage.rectTransform.rect.width;
				float h = backgroudImage.rectTransform.rect.height;

				
				
				inputVector = new Vector3(direction.x,0,direction.y);
				
				inputVector = (inputVector.magnitude>1f)?inputVector.normalized:inputVector;
				//Move JoyStcik Image
				
				Debug.Log(inputVector);
				joyStickImage.rectTransform.anchoredPosition = new Vector3(inputVector.x*(w/3),0,
				                                                           inputVector.y*(h/3));
			}
			
		}


	}
	
	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointerID) {
			direction = Vector3.zero;
			touched = false;
			inputVector = Vector3.zero;
			joyStickImage.rectTransform.anchoredPosition = Vector3.zero;
		}

	}
	
	public Vector2 GetDirection () {
		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
		return smoothDirection;
	}
}