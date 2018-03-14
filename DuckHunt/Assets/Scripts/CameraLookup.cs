using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraLookup : MonoBehaviour {

	

	public float minimumY = -60F;
	public float maximumY = 60F;
	float rotationY = 0F;
	public float sensitivityY = 15F;
	public Slider   verticalMeter;
	public VerticalHand verticalHand;

	void Awake(){
		verticalMeter.minValue = minimumY;
		verticalMeter.maxValue = maximumY;

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 movement = Input.acceleration;
		//rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
		rotationY += movement.x * sensitivityY;
		//Vector2 direction=simpleTouchPad.GetDirection();
		//rotationY += direction.y * sensitivityY;
		rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);

		verticalMeter.value = rotationY;

		verticalHand.value (minimumY, rotationY, maximumY); 
		
		transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		

	
	}
}
