using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/**
 * 
 * 
 * 
 *  <a href="https://www.youtube.com/watch?v=QxRAIjXdfFU"/>
 * 
  **/

public class GameUIMenu : MonoBehaviour {

	private Animator _animator;
	private CanvasGroup _canvasGroup;
	void Awake(){

		Time.timeScale = 1.0f;
		_animator = GetComponent<Animator> ();

		_canvasGroup = GetComponent<CanvasGroup> ();

		RectTransform rect = GetComponent<RectTransform> ();

		rect.offsetMax = rect.offsetMin = Vector2.zero;

	}


	public bool IsOpen{

		get { return _animator.GetBool("IsOpen");}
		set { _animator.SetBool ("IsOpen", value);}
	}


	void Update(){

		//Debug.Log ("Time delta:" + Time.deltaTime);

		if (!_animator.GetCurrentAnimatorStateInfo (0).IsName ("Open")) {
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
		} else {
			_canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
		}
	}



	public void startGame(){
		if (PlayerInventory.inventory.useEnegry ())
			Application.LoadLevel ("Ocean");
		else {
			GameUIManager uiManager =GameObject.FindGameObjectWithTag("UIMenuManager").GetComponent<GameUIManager>();
			uiManager.ShowMenuByTag("UIEnegy");
		}
	}

	public void exitGame(){
		Application.Quit ();
	}


}
