using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public Player player;
    public CraftingRecipe craftingRecipe;

    private Inventory inventory;
    private Item mat1;
    private int mat1Pos;
    private Item mat2;
    private int mat2Pos;
    void Start(){
        
    }

    public void craftItem(){
        //look for mat1
        inventory = player.GetInventory();
        for(int i =0; i<inventory.slots.Length; i++){
            if(inventory.isOccupied[i]){
                if(inventory.getItemAtPos(i).name==craftingRecipe.material1.name){
                    mat1 = inventory.getItemAtPos(i);
                    mat1Pos = i;
                    break;
                }
            }
        }
        //look for mat2
                for(int i =0; i<inventory.slots.Length; i++){
            if(inventory.isOccupied[i]){
                if(inventory.getItemAtPos(i).name==craftingRecipe.material2.name){
                    mat2 = inventory.getItemAtPos(i);
                    mat2Pos = i;
                    break;
                }
            }
        }

        //then if either mat1 or mat2's count is less than the count for them in crafting recipe break
        if(mat1==null){
            Debug.Log("You don't ave any "+craftingRecipe.material1.name);
            return;
        }
        if(mat1.count<craftingRecipe.material1Count){
            Debug.Log("You need more "+craftingRecipe.material1.name);
            return;
        }
        if(mat2==null){
            Debug.Log("You don't ave any "+craftingRecipe.material2.name);
            return;
        }
        if(mat2.count<craftingRecipe.material2Count){
            Debug.Log("You need more: "+craftingRecipe.material2.name);
            return;
        }



        mat1.count -= craftingRecipe.material1Count;
        mat2.count -= craftingRecipe.material2Count;
        if(mat1.count<=0){
            Debug.Log(mat1.count);
            inventory.isOccupied[mat1Pos] = false;
        
            Destroy(inventory.slots[mat1Pos].transform.GetChild(0).gameObject);
        }else{
            mat2.updateText();
        }
        if(mat2.count<=0){
            inventory.isOccupied[mat2Pos] = false;
            Destroy(inventory.slots[mat2Pos].transform.GetChild(0).gameObject);
        }else{
            Debug.Log(mat2.count);
            mat2.updateText();
        }
        if(mat1!=null){
            mat1.updateText();
        }
        if(mat2!=null){
            mat2.updateText();
        }
        player.crafting.SetActive(false);

        //otherwise instantiate item in front of player, and delete the mats used for this
                Instantiate(craftingRecipe.resultingItem,player.transform.position,Quaternion.identity);

    }
}
