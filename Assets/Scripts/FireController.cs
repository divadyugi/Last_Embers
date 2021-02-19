using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.LWRP;
using TMPro;

public class FireController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private ParticleSystem particleSystem;
    [SerializeField] private Slider slider;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    public Image fill;
    public Gradient gradient;

    [SerializeField]private Light2D fireLight;

    [SerializeField]private float detectRange;
    [SerializeField]private LayerMask whatIsPlayer;
    [SerializeField] private Player player;
    [SerializeField] private Inventory inventory;
     public bool walled;
    [SerializeField] private GameObject slabObject;
    void Start()
    {
        barErrorMessages(slider,fill,gradient);
        if(particleSystem==null){
            Debug.Log("Particle system not selected");
        }
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        StartCoroutine(weakenFire());
        fill.color = gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        textChanger();
    }

    public void barErrorMessages(Slider slider, Image fill, Gradient gradient){
        if(slider==null){
            Debug.Log("Slider not selected");
        }
        if(fill==null){
            Debug.Log("Fill not selected");
        }
        if(gradient == null){
            Debug.Log("Gradient not selected");
        }

    }

    IEnumerator fireDamage(){
        while(player.currentHealth>0){
            yield return new WaitForSeconds(5f);
            player.takeDamage(20);
            
            if(currentHealth>0){
                StartCoroutine(weakenFire());
                yield break;
            }
        }
    }
    IEnumerator weakenFire(){
        while(currentHealth>0){
        yield return new WaitForSeconds(5);
        if(walled){
        fireLight.intensity-=0.1632f;
        takeDamage(10);
        }else{
            fireLight.intensity-=0.3264f;
            takeDamage(15);
        }
        
        
        fill.color = gradient.Evaluate(slider.normalizedValue);
        }

        if(currentHealth<=0){
            StartCoroutine(fireDamage());
            yield break;
        }
    }

    private void reduceParticles(){
        var main = particleSystem.main;
        main.maxParticles = particleSystem.main.maxParticles-1;
    }

    private void increaseParticles(){
        var main = particleSystem.main;
        main.maxParticles = particleSystem.main.maxParticles+1;
    }
    public void gainHealth(int amount){
        if(currentHealth+amount<=maxHealth){
        fireLight.intensity+=0.3264f;    
        currentHealth += amount;
        increaseParticles();
        }else{
            fireLight.intensity=8.16f;
            currentHealth = maxHealth;
        }
        slider.value = currentHealth;
        
    }

    public void takeDamage(int amount){
        currentHealth-= amount;
        slider.value = currentHealth;
        reduceParticles();
    }

    void textChanger(){
        Collider2D playerInRange = Physics2D.OverlapCircle(transform.position,detectRange,whatIsPlayer);

        if(playerInRange!=null){
            transform.GetChild(2).gameObject.SetActive(true);
        }else{
            transform.GetChild(2).gameObject.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.E)){
            //Debug.Log("Correct key pressed");
        if(playerInRange!=null){
                for(int i =0; i<inventory.slots.Length; i++){
                   // Debug.Log("Slot "+player.selected+" is selected");
                    if(inventory.isOccupied[i]){
                        //Debug.Log("Slot "+i+" is occupied");
                        //Debug.Log(inventory.slots[i].transform.parent.name);
                        if(inventory.slots[i].transform.parent.name == player.selected){
                            //Debug.Log("Item "+i+"Selected");
                            Item item2 = inventory.slots[i].transform.GetChild(0).GetComponent<Item>();
                            //Debug.Log("Selected item's type is: "+item2.itemType);
                            //Debug.Log("Item count before: "+item2.count);
                            if(item2.itemType==ItemType.Fuel){
                            item2.count--;
                            Debug.Log(item2.fuelValue);
                            Debug.Log("Fire health: "+currentHealth);
                            gainHealth(item2.fuelValue);
                            item2.updateText();
                            if(item2.count<=0){
                                inventory.isOccupied[i]=false;
                                Destroy(inventory.slots[i].transform.GetChild(0).gameObject);
                            }
                            //Debug.Log("Item count after: "+item2.count);
                            }else if(item2.name=="Slab"){
                                if(!walled){
                                    walled = true;
                                    slabObject.SetActive(true);
                                    inventory.isOccupied[i]=false;
                                    Destroy(inventory.slots[i].transform.GetChild(0).gameObject);

                                }
                            }
                        }
                    }else{
                        StartCoroutine(changeText("Please select an fuel item"));
                    }
                }
        }

    }
    }

    IEnumerator changeText(string text){
        transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text=text;
        yield return new WaitForSeconds(3f);
        transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text= "Press 'E' to fuel the fire";
        yield break;
    }
}
