using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;



/**
 *  
 *  Reference :
 *   <a href="https://www.youtube.com/watch?v=J6FfcJpbPXE">Unity3d Persistence</a>
 * 
 * 
 * 
 * 
 * 
 * 
 * */


public class PlayerInventory : MonoBehaviour {

	[System.Serializable]
	public class PlayerData
	{
		public int coins;
		public int enegy;
		public int bullets;
	
	}

	// 
	public int coins;
	public int enegy;
	public int bullets;

	public static PlayerInventory inventory;
	string DataPath ;


	GameItemChanged _gameItemChanged;


	public void Awake(){

		if (inventory == null) {
			inventory = this;
			DataPath =  Application.persistentDataPath+"/playInfo.dat";
			if(!isExists()){
				// set a default coins for the player.
				coins = 1000;
				enegy = 100;
				bullets = 200;
				Save ();
			}else{
				Load ();
			}




			DontDestroyOnLoad (gameObject);
		} else if (inventory!=this) {
			Destroy(gameObject);
		}

	}

	public void Start(){

	
	}


	private void addValue(string category,int value){
		GameObject gameItemChanged = GameObject.FindGameObjectWithTag ("UIItemChanged");
		if(gameItemChanged!=null){
			_gameItemChanged = gameItemChanged.GetComponent<GameItemChanged>();

			_gameItemChanged.addValue (value, category);
		}
	}

	public void exchangeCoins(int money){
		Load ();


		int value = 0;
		switch (money) {
		
		case 1:
			value= 500;
			break;
		case 2:
			value=1500;
			break;
		case 3:
			value=5000;
			break;
		default:
			Debug.Log("Not an option");
			break;
		}
		addValue ("Coins", value);
		coins += value;
		Save ();
	}


	public void exchangeEnegy(int value){
		Load ();
		int item = 0;
		if (value > coins) {
			// notify a message.
			Debug.Log (" Don't have that much coins , please buy some coins");
		} else {

			if(value ==10){
				item =1;
			}else if (value==20){
				item=3;
			}else if (value==30){
				item=10;
			}

			enegy +=item;
			addValue ("Enegy", item);
			coins -= value;
		
		}

		Save ();
		
	}

	public void exchangeBullet(int value){
		Load ();
		if (value > coins) {
			Debug.Log ("Don't have that much coins, please buy some coins");
		} else {
			int item = 0;
			switch(value){
			case 1:
				item =2;
				break;
			case 2:
				item=5;
				break;
			case 5:
				item=20;
				break;
			default :
				break;

			}
			addValue ("Bullet",item);
			coins -=value;
			bullets+=item;

			
		}

		Save ();
	}




	public bool isExists(){
		return File.Exists (DataPath);
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(DataPath);
		
		PlayerData data = new PlayerData();
		data.coins = coins;
		data.enegy = enegy;
		data.bullets = bullets;
		
		bf.Serialize(file, data);
		file.Close();


	}


	public void Load()
	{
		if(File.Exists(DataPath))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(DataPath, FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();
			
			coins = data.coins;
			enegy = data.enegy;
			bullets = data.bullets;
			
		}
	}


	public void addCoins(int value){
		coins += value;
	}

	public bool useEnegry(){
		bool hasEnegy = false;
		if (enegy > 1) {
			enegy = enegy -1;
			hasEnegy = true;
		}

		return hasEnegy;
	}





}
