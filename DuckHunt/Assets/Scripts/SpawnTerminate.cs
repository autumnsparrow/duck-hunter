using UnityEngine;
using System.Collections;

public class SpawnTerminate : MonoBehaviour {

	public Transform[]  locations;

	private int loc=0;
	private float locationChangeRate = 5f;
	private float changeTime = 0f;

	void Start(){
		locationChangeRate = GameParams.instance ().flyRouteRate;
	}

	void Update(){

		if (Time.time > changeTime) {
			changeTime = Time.time + locationChangeRate;
			loc += 1;
			if (loc < locations.Length) {

				transform.position = locations [loc].position;
				Debug.Log ("Change Terminate Point:" + transform.position);
			} else {
				loc = 0;
				transform.position = locations [loc].position;
				Debug.Log ("Reset Terminate Point Location 0");
			}
		}
	}

	}



