using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameItemChanged : MonoBehaviour {

	Animator _animator;
	Text _text;

	void Awake(){
		_animator = GetComponent<Animator> ();
		_text = GetComponentInChildren<Text> ();

	}


	public void addValue(int value,string category){

		_text.text = category + "  +  " + value;
		_animator.SetTrigger ("ItemValueChanged");

	}
}
