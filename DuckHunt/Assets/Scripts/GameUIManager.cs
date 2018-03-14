using UnityEngine;
using System.Collections;

public class GameUIManager : MonoBehaviour {

	public GameUIMenu CurrentMenu;

	public void Start(){

		GameUIMenu main =GameObject.FindGameObjectWithTag(GameParams.loadMainMenu).GetComponent< GameUIMenu>();

		CurrentMenu = main;
		ShowMenu (CurrentMenu);
	}

	public void ShowMenuByTag(string tag){
		GameUIMenu main =GameObject.FindGameObjectWithTag(tag).GetComponent< GameUIMenu>();
		ShowMenu (main);
	}

	public void ShowMenu(GameUIMenu menu){

		if (Time.timeScale == 0f) {
			Time.timeScale = 1f;
		}

		if (CurrentMenu != null) {
			CurrentMenu.IsOpen = false;
		}

		CurrentMenu = menu;
		CurrentMenu.IsOpen = true;

	}
}
