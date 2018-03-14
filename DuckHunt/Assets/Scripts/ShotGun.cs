using UnityEngine;
using System.Collections;

public class ShotGun : MonoBehaviour {

	public GameObject[] bullet;
	public int bulletType;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	
	private bool shouldFire;
	public TargetFocus targetFocus;

	

	public void Start(){
		//fireRate = GameParams.instance ().fireBulletRate;
		this.nextFire = 0;
		//SimpleBullet simpleBullet = bullet [bulletType].GetComponent<SimpleBullet>();
		//this.fireRate = simpleBullet.


	}
	
	public float fire(){
		if (!shouldFire) {
			shouldFire = true;
			nextFire = Time.time + fireRate;
			LevelHeader.instance.useBullet();
		}
		return nextFire;

	}
	
	public bool canFire(){
		return Time.time > nextFire;
	}

	private void isFocused(){
		Vector3 point = new Vector3 (Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0f);
		

		
		Ray ray = Camera.main.ScreenPointToRay (point);
		
		RaycastHit hit;
		
		bool canShot = Physics.Raycast (ray, out hit);


		targetFocus.IsFocused = canShot;

	
	}

	public void shoot(){

		shouldFire = false;

		Vector3 point = new Vector3 (Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0f);
		
		audio.Play ();
		// gernate mulitple ray.
		
		Ray ray = Camera.main.ScreenPointToRay (point);
		
		RaycastHit hit;
		
		bool canShot = Physics.Raycast (ray, out hit);
		
		////bullet = newBullet (hit);
		/// 
		bool useRigidbody = (bulletType==0);



		if (useRigidbody) {
			fireBullet (hit, useRigidbody);
			if (canShot) {
				Duck duck = hit.transform.gameObject.GetComponent<Duck> ();
				if (duck != null) {
					duck.ReactToHit ();
					// add the score 
					Vector3 distance=hit.transform.position-shotSpawn.position;
					LevelHeader.instance.addScore(Mathf.CeilToInt(distance.magnitude));
					//GameScore.addPoint(Mathf.CeilToInt(distance.magnitude));
				}
			}
		} else {

			fireBullet (hit, useRigidbody);
		}


		// classic shot gun.
		//  
		//
		//			if (canShot) {
		//
		//				Duck duck = hit.transform.gameObject.GetComponent<Duck> ();
		//			
		//				if (duck != null) {
		//
		//					duck.ReactToHit ();
		//				}
		//			}
		
	}
	
	void Update ()
	{


		isFocused ();
		if (shouldFire) {
			shoot();
		}
		float dtime = (nextFire - Time.time) / fireRate;


	}
	

	
	
	private void fireBullet(RaycastHit hit,bool rigidbody){
		GameObject obj = Instantiate (bullet[bulletType], shotSpawn.position, shotSpawn.rotation) as GameObject;

		SimpleBullet simpleBullet = obj.GetComponent<SimpleBullet> ();

		if(rigidbody)
			simpleBullet.enableRigidbody (hit);
		else
			simpleBullet.enable (hit);
		// or enable
		//	simpleBullet.enableRigidbody (hit);

	}
	

	

}
