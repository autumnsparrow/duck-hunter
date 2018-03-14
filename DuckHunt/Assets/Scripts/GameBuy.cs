using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameBuy : MonoBehaviour {


	private Button[] btns;

	// Use this for initialization
	void Start () {

		btns = GetComponentsInChildren<Button> ();

		btns [0].onClick.AddListener ((()=>{PlayerInventory.inventory.exchangeCoins (1);}));
		btns [1].onClick.AddListener ((()=>{PlayerInventory.inventory.exchangeCoins (2);}));
		btns [2].onClick.AddListener ((()=>{PlayerInventory.inventory.exchangeCoins (3);}));

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
