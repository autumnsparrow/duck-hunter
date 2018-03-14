using UnityEngine;
using System.Collections;

public class Duck : MonoBehaviour {



	public float lifeTime;


	private SpawnTerminate flyingRoute;

	private Vector3 flyingDirection;

	private static int sequenceId;

	private int seqId;
	private float bornTime;
	private Animator _animator;

	void Start(){
		_animator = GetComponent<Animator> ();

	}


	public void ReactToHit() {
		StartCoroutine(Die());
	}

	public void setFlyDirection(SpawnTerminate direction){
		flyingRoute = direction;
		seqId = sequenceId++;
		changeDirection ();

		bornTime = Time.time+lifeTime;


	}

	private void changeDirection(){
		if (flyingRoute!=null&&flyingDirection != flyingRoute.transform.position) {



			// find out the angle of the axis

			flyingDirection = flyingRoute.transform.position;
			
			Vector3 heading = flyingDirection - transform.position;

			// Get a copy of your forward vector

			//transform.Rotate(heading.normalized);
			//transform.rotation = transform.rotation*heading; //Quaternion.Slerp(transform.rotation,heading,1f);
			//GetComponent<Rigidbody> ().velocity=Vector2.zero;
			Rigidbody rigidbody= GetComponent<Rigidbody> ();//.velocity = heading.normalized * Random.Range (10.0f, 30.0f);
			rigidbody.velocity = heading.normalized * Random.Range (GameParams.instance().minDuckSpeed, GameParams.instance().maxDuckSpeed);

			//Vector3 forward = transform.forward;
			// Zero out the y component of your forward vector to only get the direction in the X,Z plane
			heading.y = 0;
			transform.eulerAngles = Quaternion.LookRotation(heading).eulerAngles;
			//float headingAngle = Quaternion.LookRotation(forward).eulerAngles.y;
			
			//transform.Rotate(Vector3.zero);
			//transform.Rotate(new Vector3(0,headingAngle,0));
			
			
		}
	}

	public static float Angle(Vector3 from, Vector3 to)
	{
		return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
	}

	void Update(){
		
		Vector3 direction = flyingDirection - transform.position;
		 
	//	Debug.Log ("Seq:" + seqId + " distance:" + direction.magnitude+" life:"+(bornTime-Time.time)  );
		         
		if (direction.magnitude < 10||direction.magnitude>200) {
			changeDirection();
			// change the direction of the Duck
			//setFlyDirection(flyingRoute.transform);
		}


		if ((Time.time > bornTime ) ||direction.magnitude>500){
			// lifetime die

			Debug.Log ("Seq:" + seqId +" Die");
			Destroy(this.gameObject);
		}

	}


	private IEnumerator Die() {


		Debug.Log ("Duck Die:"+seqId);
		_animator.SetBool ("IsDie", true);
		this.transform.Rotate(90, 0, 0);
		this.GetComponent<Rigidbody> ().useGravity = true;
		this.GetComponent<TrailRenderer> ().enabled = true;
		this.GetComponent<AudioSource> ().enabled = false;

		float velocity = GetComponent<Rigidbody> ().velocity.magnitude;
		LevelHeader.instance.addScore (Mathf.CeilToInt (velocity/10));

		yield return new WaitForSeconds(1.0f);
		Destroy(this.gameObject);
	}




	void OnTriggerEnter(Collider other) {
		if( other.gameObject.name.Equals("Bullet")){
			
			Debug.Log("Collison Bullet:"+seqId);

			StartCoroutine(Die());
		}
	}

}
