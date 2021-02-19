using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private float speed = 3;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    [SerializeField]private float pickupRange;
    [SerializeField]private LayerMask whatIsItem;
    private Inventory inventory;
    private Item selectedItem;

    private GameObject[] buttons;
    public ItemSelector[] itemSelector = new ItemSelector[6];
    public string selected;

    //private Animator anim;

    public Image healthFill;
    public Gradient healthGradient;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] public int currentHealth;
    public Image thirstFill;
    public Gradient thirstGradient;
    [SerializeField] private Slider thirstSlider;
    [SerializeField] private int maxThirst = 100;
    [SerializeField] private int currentThirst;

    

    
    public Image hungerFill;
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private int maxHunger = 100;
    [SerializeField] private int currentHunger;
    public GameObject crafting;
    private bool isCraftingActive = false;

    private bool isInWater;

    private bool facingRight = false;
    private float originalSpeed;
    private float waterSpeed;

    private DeathManager deathManager;
    private AudioSource audioSource;
    public bool win = false;

    private WinManager winManager;
    public FireController fireController;

    void Awake() {
        
        Time.timeScale =1f;    
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = true;
        Time.timeScale = 1f;
        deathManager = GameObject.Find("DeathManager").GetComponent<DeathManager>();
        winManager = GameObject.Find("WinManager").GetComponent<WinManager>();
        waterSpeed = speed/2;
        originalSpeed = speed;
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthFill.color = healthGradient.Evaluate(1f);

        currentThirst = maxThirst;
        thirstSlider.maxValue = maxThirst;
        thirstSlider.value = currentThirst;
        thirstFill.color = thirstGradient.Evaluate(1f);
        Coroutine thirstRoutine = StartCoroutine(thirst());

        currentHunger = maxHunger;
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;
        Coroutine hungerRoutine = StartCoroutine(hunger());


       // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        buttons = GameObject.FindGameObjectsWithTag("HotBar");
        for(int i =0; i<buttons.Length; i++){
            itemSelector[i]=(buttons[i].GetComponent<ItemSelector>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        //Debug.Log(anim.GetFloat("Horizontal"));
        //anim.SetFloat("Horizontal",moveDirection.x);
        pickupFunction();
        if(Input.GetKeyDown(KeyCode.Z)){
            dropFunction();
        }
        if(currentHealth<=0){
            audioSource.enabled=false;
            deathManager.startDeathScreen();
            //Destroy(this.gameObject);
            //this.gameObject.SetActive(false);
        }
        if(isInWater){
            this.transform.GetChild(0).gameObject.SetActive(true);
            speed= waterSpeed;
        }else{
            this.transform.GetChild(0).gameObject.SetActive(false);
            speed = originalSpeed;
        }
        if(Input.GetKeyDown(KeyCode.E)){
            //if in river, drink from river
            if(isInWater){
                gainThirst(20);
            }
            eat();
        }

        if(Input.GetKeyDown(KeyCode.C)){
            crafting.SetActive(isCraftingActive);
            isCraftingActive=!isCraftingActive;
        }
        if(win&&fireController.walled){
            //winScreen;
            audioSource.enabled=false;
            winManager.startWinScreen();
        }else if(win&&!fireController.walled){
            takeDamage(-1000);
        }

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position+moveDirection*speed*Time.deltaTime);
        if(facingRight && moveDirection.x<0){
            Flip();
        }else if(!facingRight && moveDirection.x>0){
            Flip();
        }
    }

    void Flip(){
        facingRight = !facingRight;
        transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
    }

    public void takeDamage(int amount){

        currentHealth -= amount;
        healthSlider.value = currentHealth;
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }

    public void heal(int amount){
        if(currentHealth+amount<=maxHealth){
        currentHealth += amount;
        }else{
            currentHealth = maxHealth;
        }
        healthSlider.value = currentHealth;
        healthFill.color = healthGradient.Evaluate(healthSlider.normalizedValue);
    }

    public void lowerThirst(int amount){
        currentThirst -= amount;
        thirstSlider.value = currentThirst;
        if(currentThirst<=0){
            thirstFill.color = thirstGradient.Evaluate(0);
        }
        thirstFill.color = thirstGradient.Evaluate(thirstSlider.normalizedValue);
    }

    public void gainThirst(int amount){
        if(currentThirst+amount<=maxThirst){
        currentThirst += amount;
        }else{
            currentThirst = maxThirst;
        }
        thirstSlider.value = currentThirst;
        thirstFill.color = thirstGradient.Evaluate(thirstSlider.normalizedValue);
    }

    IEnumerator thirstDamage(){
        while(currentHealth>0){
            takeDamage(15);
            yield return new WaitForSeconds(5f);
            if(currentThirst>0){
                StartCoroutine(thirst());
                yield break;
            }
        }

    }

    IEnumerator thirst(){
        //while player is alive and thirst is more than 0
        while(currentThirst>0){
            lowerThirst(10);
            yield return new WaitForSeconds(5f);
        }

        if(currentThirst<=0){
            StartCoroutine(thirstDamage());
            yield break;
        }


    }

    public void starve(int amount){
        currentHunger -= amount;
        hungerSlider.value = currentHunger;
    }

    public void fillHunger(int amount){
        if(currentHunger+amount<=maxHunger){
        currentHunger += amount;
        }else{
            currentHunger = maxHunger;
        }
        hungerSlider.value = currentHunger;
    }

    IEnumerator hungerDamage(){
         while(currentHealth>0 && currentHunger<=0){
            yield return new WaitForSeconds(5f);
            takeDamage(10);

        }
        if(currentHunger>0){
            StartCoroutine(hunger());
            yield break;
        }
    }

    IEnumerator hunger(){
        while(true){
            starve(10);
            yield return new WaitForSeconds(5f);
            if(currentHunger<=0){
            
            StartCoroutine(hungerDamage());
            yield break;
        }
        }


    }


   void pickupFunction(){
    Collider2D itemInRange = Physics2D.OverlapCircle(transform.position,pickupRange,whatIsItem);
    if(itemInRange!=null){
        if(Input.GetKeyDown(KeyCode.F)){
            Item item = itemInRange.GetComponent<Item>();
            if(item.name=="Log"){
                for(int i =0; i<inventory.slots.Length; i++){
                    if(inventory.isOccupied[i]){
                        if(inventory.slots[i].transform.parent.name == selected){
                        selectedItem=inventory.getItemAtPos(i);
                    if(selectedItem.name=="Axe"){
                        inventory.addItem(item,itemInRange.gameObject);
                    }else{
                        TextMeshPro textMesh = itemInRange.transform.GetChild(0).GetComponent<TextMeshPro>();
                        string originalText = textMesh.text;
                        StartCoroutine(changeText("You need an axe to gather this item",textMesh,originalText));
                        return;
                    }
                        }
                    }else{
                        TextMeshPro textMesh = itemInRange.transform.GetChild(0).GetComponent<TextMeshPro>();
                        string originalText = textMesh.text;
                        StartCoroutine(changeText("You need an axe to gather this item",textMesh,originalText));
                    }
                }
                return;
            }

            if(item.name=="Boulder"){
                for(int i =0; i<inventory.slots.Length; i++){
                    if(inventory.isOccupied[i]){
                        if(inventory.slots[i].transform.parent.name == selected){
                        selectedItem=inventory.getItemAtPos(i);
                    if(selectedItem.name=="PickAxe"){
                        inventory.addItem(item,itemInRange.gameObject);
                    }else{
                       // itemInRange.transform.GetChild(0).GetComponent<Text>
                        TextMeshPro textMesh = itemInRange.transform.GetChild(0).GetComponent<TextMeshPro>();
                        string originalText = textMesh.text;
                        StartCoroutine(changeText("You need a pickAxe to gather this item",textMesh,originalText));
                        return;
                    }
                        }
                    }else{
                        TextMeshPro textMesh = itemInRange.transform.GetChild(0).GetComponent<TextMeshPro>();
                        string originalText = textMesh.text;
                        StartCoroutine(changeText("You need an pickAxe to gather this item",textMesh,originalText));
                    }
                }
                return;
            }
            inventory.addItem(item,itemInRange.gameObject);
        }
    }
   }

    void dropFunction(){

        for(int i =0; i<inventory.slots.Length; i++){
            if(inventory.isOccupied[i]){
                Debug.Log(selected);
                if(inventory.slots[i].transform.parent.name == selected){
                    GameObject button = inventory.slots[i].transform.GetChild(0).gameObject;
                    Item item2 = inventory.slots[i].transform.GetChild(0).GetComponent<Item>();
                    GameObject itemToSpawn =(GameObject) Instantiate
                    (item2.itemObject,transform.position,Quaternion.identity);
                    itemToSpawn.GetComponent<Item>().count=item2.count;
                    Destroy(button);
                    inventory.isOccupied[i]= false;
                }
            }
        }
    }

    private void eat(){
        for(int i =0; i<inventory.slots.Length; i++){
            if(inventory.isOccupied[i]){
                if(inventory.slots[i].transform.parent.name == selected){
                    Item item2 = inventory.slots[i].transform.GetChild(0).GetComponent<Item>();
                    if(item2.itemType == ItemType.Food){
                        item2.count--;
                        item2.updateText();
                        fillHunger(item2.foodValue);
                        if(item2.count<=0){
                            inventory.isOccupied[i] = false;
                            Destroy(inventory.slots[i].transform.GetChild(0).gameObject);
                        }
                    }
                }
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position,pickupRange);
    }

    public Inventory GetInventory(){
        return inventory;
    }

    public bool GetisCraftingActive(){
        return isCraftingActive;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="River"){
            isInWater = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag=="River"){
            isInWater=false;
        }
    }

    IEnumerator changeText(string text, TextMeshPro textMesh, string originalText){
        textMesh.text = text;
        yield return new WaitForSeconds(3f);
        textMesh.text = "Press 'E' to pickup item";
        yield break;
    }

}
