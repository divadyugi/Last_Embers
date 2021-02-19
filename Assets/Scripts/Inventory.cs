using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool[] isOccupied;
    public GameObject[] slots;

    public int selected = -1;


    public void addItem(Item item, GameObject itemInRange){

            for(int i =0; i<slots.Length; i++){
                if(isOccupied[i]){
                        if(slots[i].transform.childCount>0){

                        Item item2 = slots[i].transform.GetChild(0).GetComponent<Item>();
                        if(item2!=null){
                            if(item2.name==item.name){
                                item2.count+=item.count;
                                item2.updateText();
                                Destroy(itemInRange.gameObject);
                                return;
                                }
                            }
                    }
                }
            }
                for(int i =0; i<slots.Length; i++){
                if(!isOccupied[i]){
                if(item.count>0){
                    item.itemButton.GetComponent<Item>().count=item.count;
                    }
                    isOccupied[i]=true;
                    Instantiate(item.itemButton,slots[i].transform,false);
                    slots[i].transform.GetChild(0).GetComponent<Item>().updateText();
                    Destroy(itemInRange.gameObject);
                    return;
                }
                }

    }

    public Item getItemAtPos(int pos){
        return slots[pos].transform.GetChild(0).GetComponent<Item>();
    }


}
