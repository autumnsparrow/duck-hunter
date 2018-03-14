using UnityEngine;
using System.Collections;

public class SpwanDuck : MonoBehaviour {

	public GameObject duck;
	public SpawnTerminate flyingRoute;

	public float spwanRate  = 10f;

	private float nextSpwan = 0;

	private bool shouldStopSpwan = false;

	private float timeScale = 0.0f;

	// Use this for initialization
	void Start () {
		timeScale = Time.timeScale;
		Debug.Log ("TimeScale" + timeScale);
	}
	
	// Update is called once per frame
	void Update () {



	
		if (Time.time > nextSpwan&&(!shouldStopSpwan)) {
			nextSpwan = Time.time+spwanRate;

			spwanDuck();
		}
	}

	private IEnumerator restartSpawn(){
		yield return new WaitForSeconds (60);
		shouldStopSpwan = false;
		//Time.timeScale = timeScale;
	}

	private void spwanDuck(	){
		GameObject obj =Instantiate(duck, transform.position, transform.rotation) as GameObject;
		Duck d = obj.GetComponent<Duck> ();
		d.setFlyDirection (flyingRoute);


	}



}
