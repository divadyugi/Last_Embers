using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSelector : MonoBehaviour
{
    public Inventory inventory;
    public Player player;
    public KeyCode keyCode;
    public void getSelected(){
        inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>();

       /* for(int i =0; i<inventory.slots.Length;i++){
            Debug.Log(transform.name);
            if(inventory.slots[i].transform.parent.name==transform.name){
                inventory.selected = i;
            }
        }*/
        player.selected=transform.name;
    }

    public void Update(){
        if(Input.GetKeyDown(keyCode)){
            EventSystem.current.SetSelectedGameObject(this.gameObject);
            Debug.Log(transform.name);
            inventory = GameObject.Find("InventoryManager").GetComponent<Inventory>();
            player.selected=transform.name;
        }
    }
}
