using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/**
 * 
 * TargetFoucs Usage:
 * 
 * 1. when the crosshair raycast the moving target, scale the size and color of it.
 * 2. when the player press the pause button, handle the resume,main menu and restart functions.
 * 
 * 
 * 
 * 
 * */
public class TargetFocus : MonoBehaviour {

	private Animator _animator;
	private GameUIMenu gamePaused;


	private CanvasGroup _canvasGroup;
	public bool IsFocused{

		get { return _animator.GetBool ("IsFocused");}
		set { _animator.SetBool ("IsFocused", value);}

	}

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator> ();
		gamePaused = GameObject.FindGameObjectWithTag ("Pause").GetComponent<GameUIMenu>();

	}
	private bool _gameFailed;
	// Update is called once per frame
	void Update () {


	
	}

	
	IEnumerator setGamePause(){
		
		yield return  new WaitForSeconds(1);
		
		Time.timeScale=0f;
		
	}
	

	public void pauseGame(){
	
		gamePaused.IsOpen = true;

		StartCoroutine(setGamePause());
	}


	public void resumeGame(){

		Time.timeScale = 1.0f;
		gamePaused.IsOpen = false;
	

	}


	public void restartGame(){
		if(gamePaused.IsOpen)
			gamePaused.IsOpen = false;
		if (PlayerInventory.inventory.useEnegry ()) {
			Application.LoadLevel ("Ocean");	
		} else {
			GameParams.loadMainMenu="UIEnegy";
			Application.LoadLevel("GameUI");
		}

	}

	public void mainMenu(){
		if(gamePaused.IsOpen)
			gamePaused.IsOpen = false;
		Application.LoadLevel ("GameUI");
	}


}
