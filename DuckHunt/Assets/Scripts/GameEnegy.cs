using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameEnegy : MonoBehaviour {
	
	private Button[] btns;
	
	// Use this for initialization
	void Start () {
		
		btns = GetComponentsInChildren<Button> ();
		
		btns [0].onClick.AddListener (()=>{PlayerInventory.inventory.exchangeEnegy (10);});
		btns [1].onClick.AddListener (()=>{PlayerInventory.inventory.exchangeEnegy (20);});
		btns [2].onClick.AddListener (()=>{PlayerInventory.inventory.exchangeEnegy (50);});
		
		
	}

	// Update is called once per frame
	void Update () {
	
	}
}
