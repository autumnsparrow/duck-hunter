using UnityEngine;
using System.Collections;

public class VerticalHand : MonoBehaviour {



	RectTransform _transform;


	void Start(){
		_transform = GetComponent<RectTransform> ();
		_transform.localScale = Vector3.zero;
	}

	void Update(){


	}


	public void value(float min,float current,float max){

		if (current > 0) {
			float v = current/max;
			Debug.Log("scale value:"+v);
			_transform.localScale = new Vector3 (current / max, 1, 1);
		} 
		if(current<0){
			float v = current/min;
			Debug.Log("scale value:"+v);
			_transform.localScale = new Vector3(-current/min,1,1);
		}
	}


}
