using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * 
 * 
 *  Functions (A menu widget):
 * 
 * 1. limit the max value of slider
 * 2. change the value of slider
 * 3. change the value of text.
 * 
 * 
 * */
public class UpdateSlider : MonoBehaviour {

	private Slider _slider;
	private Text _text;
	private bool needUpdate;
	public  bool ShouldUpdate{
		get { return needUpdate;}
		set { needUpdate = value;}
	}


	public float MaxValue{

		get { return _slider.maxValue;}
		set { _slider.maxValue = value; }

	}

	public float Value{
		get {return _slider.value;}
		set {_slider.value = value;}
	}

	public string Text{
		get { return _text.text;}
		set { if(_text!=null) _text.text = value;}

	}

	// Use this for initialization
	void Awake () {

		_slider = GetComponentInChildren<Slider> ();

		_text = _slider.GetComponentInChildren<Text> ();

		ShouldUpdate = true;
	
	}
	
	// Update is called once per frame
	void Update () {

		refresh ();

	}

	void refresh(){
		if(ShouldUpdate)
			Text = _slider.value + "";

	}
}
