using UnityEngine;
using System.Collections;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	//public SimpleTouchPad simpleTouchPad;

	float rotationY = 0F;
	private Quaternion calibrationQuaternion;
	void FixedUpdate ()
	{
		Vector3 accelerationRaw = Input.acceleration;
		Vector3 acceleration = FixAcceleration (accelerationRaw);
		Vector3 movement = acceleration;// new Vector3 (acceleration.x, 0.0f, acceleration.y);

		if (axes == RotationAxes.MouseXAndY)
		{
			//float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			float rotationX = transform.localEulerAngles.y + movement.x * sensitivityX;

			//rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY += movement.y * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{

			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			//float rotationX = transform.localEulerAngles.y + movement.x * sensitivityX;
			//Vector2 direction=simpleTouchPad.GetDirection();

			//float rotationX = transform.localEulerAngles.y + direction.x * sensitivityX;

			rotationX  = ClampAngle(rotationX,minimumX,maximumX);
			transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, rotationX, 0);
			//transform.Rotate(0,Input.GetAxis("Mouse X") * sensitivityX,0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			//rotationY += movement.x * sensitivityY;
			//Vector2 direction=simpleTouchPad.GetDirection();
			//rotationY += direction.y * sensitivityY;
			//rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}

	static float ClampAngle(float angle, float min, float max)
	{
		if (min < 0 && max > 0 && (angle > max || angle < min))
		{
			angle -= 360;
			if (angle > max || angle < min)
			{
				if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
				else return max;
			}
		}
		else if(min > 0 && (angle > max || angle < min))
		{
			angle += 360;
			if (angle > max || angle < min)
			{
				if (Mathf.Abs(Mathf.DeltaAngle(angle, min)) < Mathf.Abs(Mathf.DeltaAngle(angle, max))) return min;
				else return max;
			}
		}
		
		if (angle < min) return min;
		else if (angle > max) return max;
		else return angle;
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;

		CalibrateAccelerometer ();
	}

	//Used to calibrate the Iput.acceleration input
	void CalibrateAccelerometer () {
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		//calibrationQuaternion =  Input.acceleration;//Quaternion.Inverse (accelerationSnapshot);
	}
	
	//Get the 'calibrated' value from the Input
	Vector3 FixAcceleration (Vector3 acceleration) {
		Vector3 fixedAcceleration = acceleration;//calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}


}