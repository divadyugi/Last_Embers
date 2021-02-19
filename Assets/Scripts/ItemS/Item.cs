using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public enum ItemType{
        Weapon,
        Food,
        Fuel,
        Material
}

public class Item :MonoBehaviour
{

    public Text text;
    public string name;
    public int count;
    public ItemType itemType;

    public GameObject itemButton;

    public GameObject itemObject;

    public int fuelValue;

    public int foodValue;
    public void updateText(){
        text.text = count.ToString();
    }

    public void SetValues(int a){
        count = a;
    }


}
