using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemText : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField]private float detectRange;
    [SerializeField]private LayerMask whatIsPlayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     textChanger();   
    }

    void textChanger(){
        Collider2D playerInRange = Physics2D.OverlapCircle(transform.position,detectRange,whatIsPlayer);

        if(playerInRange!=null){
            transform.GetChild(0).gameObject.SetActive(true);
        }else{
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
