using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMenuBar : MonoBehaviour {

	
	Text coinsValue;
	Text enegyValue;
	Text bulletValue;



	// Use this for initialization
	void Start () {
		GameObject coinsObject = GameObject.FindWithTag("CoinsValue");
		GameObject enegyObject = GameObject.FindWithTag("EnegyValue");
		GameObject bulletObject = GameObject.FindWithTag("BulletValue");
		if(coinsObject!=null)
			coinsValue = coinsObject.GetComponent<Text>();
		if(enegyObject!=null)
			enegyValue = enegyObject.GetComponent<Text>();
		if(bulletObject!=null)
			bulletValue =bulletObject.GetComponent<Text>();
	
	}
	
	
	float textUpdateInterval = 0f;
	public void Update(){
		
		
		if (textUpdateInterval > 1.0f) {
			textUpdateInterval = 0;
			textValueUpdate();
		}
		textUpdateInterval += Time.deltaTime;
	}
	
	
	private void textValueUpdate(){
		if(coinsValue!=null)
			coinsValue.text = "" + PlayerInventory.inventory.coins;
		if(enegyValue!=null)
			enegyValue.text = "" +PlayerInventory.inventory. enegy;
		if(bulletValue!=null)
			bulletValue.text = "" +PlayerInventory.inventory.bullets;
		
	}

}
