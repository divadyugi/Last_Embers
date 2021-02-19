using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] itemsToSpawn;
    public Vector2[] boundary;
    public float spawnTime;

    private bool isItMorning;
    [SerializeField] private GameObject itemContainer;
    [SerializeField] private int despawnTime;
    //go top left,
    //top right
    //bottom right
    //bottom left
    void Start()
    {
        StartCoroutine(spawnTimer());
        StartCoroutine(despawnTimer());
    }

    IEnumerator spawnTimer(){
        while(true){
            spawnItem();
            yield return new WaitForSeconds(spawnTime);
            
        }
    }

    IEnumerator despawnTimer(){
        while(true){
            deleteItem();
            yield return new WaitForSeconds(despawnTime);
        }
    }

    private void spawnItem(){
        //Tundra:
        //-175, 99.5
        //214, 99.5
        //214,-173
        //-175,-173
        int item = Random.Range(0,itemsToSpawn.Length);
        Vector2 leftTop = Vector2.Lerp(boundary[0],boundary[2],Random.Range(0,1f));
        Vector2 rightTop = Vector2.Lerp(boundary[1],boundary[3],Random.Range(0,1f));
        Vector2 spawnPoint = Vector2.Lerp(leftTop,rightTop,Random.Range(0,1f));
        GameObject spawnedItem = Instantiate(itemsToSpawn[item],spawnPoint,Quaternion.identity);
        spawnedItem.transform.parent = itemContainer.transform;


    }

    private void deleteItem(){
        Destroy(itemContainer.transform.GetChild(0).gameObject);
    }

    void OnDrawGizmosSelected()
    {
        
    }

}
