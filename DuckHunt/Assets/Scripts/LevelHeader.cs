using UnityEngine;
using System.Collections;

public class LevelHeader : MonoBehaviour {

	UpdateSlider _bag;

	UpdateSlider _timer;

	UpdateSlider _score;

	private  float time  = 0f;
	

	private  float origin = 0f;


	private int score;
	private int bullet;


	public void useBullet(){
		if (PlayerInventory.inventory.bullets > 1) {
			bullet ++;
		} else {
			loadUI("UIBullet");
		}
	}

	public void addScore(int value){
		score += value;
		_score.Text = "" + score;
		gameItemChanged.addValue (value, "score");
	}

	public static LevelHeader instance;

	void Awake(){

		if (instance == null) {
			instance = this;
			//DontDestroyOnLoad (this);
		} else if(instance !=this){
			Destroy(this);
		}

	}

	private GameItemChanged gameItemChanged;
	// Use this for initialization
	void Start () {

		UpdateSlider[]  _sliders = GetComponentsInChildren<UpdateSlider> ();
		if (_sliders != null) {
			_bag = _sliders[0];

			_timer = _sliders[1];

			_score = _sliders[2];

			_bag.MaxValue = PlayerInventory.inventory.bullets;
			_timer.MaxValue = 100;
			_score.MaxValue= 100;

			_score.ShouldUpdate=false;



		}

		gameItemChanged = GameObject.FindGameObjectWithTag ("UIItemChanged").GetComponent<GameItemChanged> ();

		origin = Time.time;
	}


	private void fresh(){
		time = Time.time - origin;
		if (_timer != null) {

			_timer.Value = Mathf.CeilToInt (_timer.MaxValue - time);
		}
		if (_bag != null) {
			_bag.Value = Mathf.CeilToInt (_bag.MaxValue - bullet);
		}
		if (_score != null) {
			//_score.Value = score;
			Debug.Log(" "+score);
			_score.Text =""+ score;
		}
	}

	private float interval = 0.0f;

	private bool validTime =true;
	private bool markOfCallOnce = false;

	private bool failedMark = false;
	
	// Update is called once per frame
	void Update () {


		validTime = (Time.time - origin < _timer.MaxValue);
		if (validTime) {
			interval += Time.deltaTime;
			if (interval > 1.0f) {
				interval = 0.0f;
				fresh ();
			}
		} else {
			// call only once load the failed ui
			//failedMark = true;
			int minScore = Mathf.CeilToInt(_score.MaxValue*0.8f);
			if(score> minScore){

				SaveUserData ();
				float seed = Random.Range(0,1);
				Application.LoadLevel (seed>0.5f?"Lake":"Ocean");

				//loadUI("UIMain");
			}else{
				loadUI("UIFailed");
			}
		}


	}


	private void SaveUserData(){
		PlayerInventory.inventory.bullets -= bullet;
		PlayerInventory.inventory.coins += score;
		PlayerInventory.inventory.Save ();
	}

	private void loadUI(string uiName){

		if (!markOfCallOnce) {

			markOfCallOnce = true;
			GameParams.loadMainMenu = uiName;

			// record the bullets using and score
			SaveUserData ();

			Application.LoadLevel ("GameUI");
		}

	}





}
