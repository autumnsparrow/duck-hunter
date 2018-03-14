using UnityEngine;
using System.Collections;

public class SimpleBullet : MonoBehaviour {

	
	public float distance = 1000.0f;
	public float lifeTime = 3f;
	public float bulletSpeed = 500f;
	private float bornTime;
	private float flyDistance = 0;

	private bool disableRigidbody = false;
	

	private float flyingTime =0.0f;
	
	void Update ()
	{


		flyDistance -= bulletSpeed * Time.deltaTime;
		flyingTime -=Time.deltaTime;

		if (disableRigidbody) {
			transform.position += Camera.main.transform.forward * bulletSpeed * Time.deltaTime;
		}

		if (flyingTime<0|| flyDistance < 0) {
			Debug.Log("lifeTime:"+(Time.time-bornTime)+" flyDistacne:"+flyDistance+" flyintTime:"+flyingTime);
			Destroy (this.gameObject);
		}


		
	}

	public void  enable(RaycastHit hit){
		this.name="Bullet";
		this.bornTime = Time.time;
		this.flyDistance = distance;
		this.flyingTime = this.lifeTime;
		disableRigidbody = true;

	}

	public void enableRigidbody(RaycastHit hit){
		Vector3 heading = hit.point - transform.position;
		GetComponent<Rigidbody> ().velocity = bulletSpeed * Camera.main.transform.forward;
		this.name="Bullet";

		this.bornTime = Time.time+lifeTime;
		disableRigidbody = false;

	}

}
