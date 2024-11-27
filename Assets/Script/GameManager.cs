using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;


// using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public static StageManager sm_instance;

    [SerializeField]
    private GameObject[] characters;

    [SerializeField]
    private Text scoreBoard ;

    [SerializeField]
    private int score;

    private GameObject GameOverUI;
    private GameObject StoreUI;
    private GameObject DropZone;
    private List<Sprite> inventory ;
    private List<Sprite> objective;

    private int stage;

    private int _charIndex;
    public int CharIndex{
        get{return _charIndex;}
        set { _charIndex = value;}
    }
    private bool isCharacterDead;

    public bool CharStatus{
        get{return isCharacterDead;}
        set { isCharacterDead = value;}
    }

    private void Awake(){
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable(){
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode){
        if(scene.name == "Gameplay"){
            // scoreBoard = GameObject.Find("scoreBoard").GetComponent<Text>();
            Instantiate(characters[CharIndex]);
            CharStatus = false;
            score = 0;
            resumeGame();
            innitUI();
            inventory = new List<Sprite>();
            Debug.Log("instantiate status: "+GameManager.instance.CharStatus);
        }
    }

    public void incrScore(){
        score++;
        scoreBoard.text = score.ToString();
    }

    public int getScore(){
        return score;
    }

    public void pauseGame(){
        Time.timeScale = 0f;
    }

    public void resumeGame(){
        Time.timeScale = 1f;
    }

    public void GameOver(){
        pauseGame();
        GameOverUI.SetActive(true);
    }

    public void showStoreUI(Sprite item_sprite){
        StoreUI.SetActive(true);
        GameObject item = GameObject.Find("ItemSprite").gameObject;
        SpriteRenderer item_sr = item.GetComponent<SpriteRenderer>();
        item_sr.sprite = item_sprite;
        Debug.Log("item sprite have been changed with:"+ item_sprite.name);
    }

    public void hideStoreUi(){
        StoreUI.SetActive(false);
    }

    public void addItemToInventory(Sprite item){
        if (!inventory.Contains(item)){
            inventory.Add(item);
            addToInventoryUI(item);
        }
    }

    public void addToInventoryUI(Sprite item){
        GameObject inventory = GameObject.Find("Outline");
        for (int i = 0; i < inventory.transform.childCount; i++)
        {
            GameObject child = inventory.transform.GetChild(i).gameObject;
            SpriteRenderer sr = child.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                if (sr.sprite != null)
                {
                    Debug.Log(child.name + " has a sprite: " + sr.sprite.name);
                }else{
                    sr.sprite = item;
                    return;
                }
            }
        }
    }

    public void showDropZoneUI(){

        DropZone.SetActive(true);
    }

    public void hideDropZoneUI(){

        DropZone.SetActive(false);

    }

   
    public void checkTarget(){
        if (objective == inventory){
            GameOver();
        }
    }

    private void innitUI(){
        GameOverUI = GameObject.Find("Game Over");
        GameOverUI.SetActive(false);
        StoreUI = GameObject.Find("Store");
        StoreUI.SetActive(false);
        DropZone = GameObject.Find("DropZone");
        hideDropZoneUI();
    }

    
}
