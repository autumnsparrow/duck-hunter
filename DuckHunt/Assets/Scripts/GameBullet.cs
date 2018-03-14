using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameBullet : MonoBehaviour {


	private Button[] btns;
	
	// Use this for initialization
	void Start () {
		
		btns = GetComponentsInChildren<Button> ();
		
		btns [0].onClick.AddListener (()=>{PlayerInventory.inventory.exchangeBullet (1);});
		btns [1].onClick.AddListener (()=>{PlayerInventory.inventory.exchangeBullet (2);});
		btns [2].onClick.AddListener (()=>{PlayerInventory.inventory.exchangeBullet (5);});
		
		
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
