using UnityEngine;
using System.Collections;

public class GameParams : MonoBehaviour {

	private static GameParams  gameParams = null;
	public static GameParams instance(){

		return gameParams;
	}

	//public static bool loadMainMenuDefault = true;

	public static string loadMainMenu = "UIMain";

	public static bool gameFailed = false;

	public float minDuckSpeed = 30.0f;
	public float maxDuckSpeed = 100.0f;


	public int maxDucks = 100;

	public float flyRouteRate = 10f;



	void Awake(){
		gameParams = this;
	}


}
