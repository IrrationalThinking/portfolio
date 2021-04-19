using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    //Initialise the gameobjects these will always be the same so I can afford to not make them dynamic
    [HideInInspector]
    public List<GameObject> unitList = new List<GameObject>();
    public int numSelected = 0;
    public GameObject unitA1;
    public GameObject unitA2;
    public GameObject unitA3;
    public GameObject unitA4;
    public GameObject unitB1;
    public GameObject unitB2;
    public GameObject unitB3;
    public GameObject unitB4;
    public GameObject unitButton;
    public GameObject confirmTeam;
    public GameObject gameEnd;
    public Button returnToMenu;
    [HideInInspector]
    public Unit[] units = new Unit[8];
    [HideInInspector]
    public GameObject[] team1 = new GameObject[4];
    [HideInInspector]
    public GameObject[] team2 = new GameObject[4];
    [HideInInspector]
    //public string[,] abilities = new string[16, 8];
    public List<string[]> abilities = new List<string[]>();
    [HideInInspector]
    public string[] unitNames = new string[8];
    public int points = 8000;
    [HideInInspector]
    public bool isTeam1 = true;
    public string currentFaction;
    //public int[] unitSpeeds = new int[8];
    /*[HideInInspector]
    public Unit unitStats;*/
    // Start is called before the first frame update
    void Awake()
    {
        //initialiseTeams();
        //readAbilities();
        
    }
    private void OnEnable() {
        SceneManager.sceneLoaded += OnGameStarted;
    }
    private void OnDisable() {
        SceneManager.sceneLoaded -= OnGameStarted;
    }
    public void OnGameStarted(Scene scene, LoadSceneMode mode) {
        if (scene.name == "GameScene") {
            gameEnd = GameObject.Find("GameEnd");
            returnToMenu = gameEnd.GetComponentInChildren<Button>();
            returnToMenu.onClick.AddListener(loadMenu);  
            gameEnd.SetActive(false);
            initialiseUnits();

        }
        
    }
    void Start() {
        DontDestroyOnLoad(gameObject);
    }
    
    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("Menu Panel").GetComponent<TurnController>().settingUp();
    }
    //puts each of the team members into the team array, it's a bit lengthy but this was the most reasonable way to do it
    void initialiseTeams() {
        //initialiseUnits();
        team1[0] = unitA1;
        unitA1.name = unitNames[0];
        team1[1] = unitA2;
        unitA2.name = unitNames[1];
        team1[2] = unitA3;
        unitA3.name = unitNames[2];
        team1[3] = unitA4;
        unitA4.name = unitNames[3];
        team2[0] = unitB1;
        unitB1.name = unitNames[4];
        team2[1] = unitB2;
        unitB2.name = unitNames[5];
        team2[2] = unitB3;
        unitB3.name = unitNames[6];
        team2[3] = unitB4;
        unitB4.name = unitNames[7];
        readAbilities();
    }
    /*void currentTurn() {

    }*/

    void checkUnits() {

        for(int i = 0; i < (team1.Length+team2.Length); i++) {
            if(i < team1.Length){
                units[i] = team1[i].GetComponentInChildren<Unit>();
                //unitSpeeds[i] = units[i].speed;
            } else {
                units[i] = team2[i-team2.Length].GetComponentInChildren<Unit>();
                //unitSpeeds[i] = units[i].speed;
            }
                //GameObject.Find("Menu Panel").GetComponent<TurnController>().settingUp();
                //Debug.Log(units[7].health + "hi");
        }
        //GameObject.Find("Menu Panel").GetComponent<TurnController>().settingUp();
        /*for(int f = 0; f < units.Length; f++) {
            Debug.Log(units[f] + " " + f);
        }*/
    }
    public void readAbilities() {
        //int counter = 0;
        string line;
        string[] words;
        string path = "Assets/Resources/Abilities.txt";
        StreamReader reader = new StreamReader(path);
        while ((line = reader.ReadLine()) != null) {
            words = line.Split(' ');
            abilities.Add(words);
            /*for(int i = 0; i < words.Length; i++) {
                //abilities[counter, i] = words[i];
                //abilities[counter][i] = words[i];
                //abilities.Add(words);

                Debug.Log(abilities[i][0]);
                /*if (i == 3) {
                    if(words[3].Contains("0")) {
                        break;
                    }
                }
            }
            counter++;*/
        }
        /*foreach(string[] item in abilities) {
            Debug.Log(item);
        }*/
        //Debugging purposes, currently works as intended
        /*for(int i = 0; i < abilities.GetLength(0); i++) {
            for(int f = 0; f < abilities.GetLength(1); f++) {
                Debug.Log("row is " + i + " column is " + f + " " + abilities[i, f]);
            }
        }*/
    }
    public void setFaction(Button button) {
        currentFaction = button.GetComponentInChildren<Text>().text.ToString();
    }
    public void populateButtons() {
        //List<GameObject> unitList = new List<GameObject>();
        string line;
        string[] words;
        unitButton = GameObject.Find("Unit");
        GameObject location = GameObject.Find("ButtonListContent");
        string path = "Assets/Resources/Units.txt";
        StreamReader reader = new StreamReader(path);
        while((line = reader.ReadLine()) != null) {
            words = line.Split(' ');
            if(words[1] == currentFaction){
                GameObject newUnit = Instantiate(unitButton, location.transform) as GameObject;
                newUnit.name = words[0];
                newUnit.GetComponentInChildren<Text>().text = words[0];
                newUnit.transform.GetChild(1).GetComponent<Text>().text = words[2];
                unitList.Add(newUnit);
            }
            //newUnit.transform.localScale = new Vector3(290, 200);
        }
        unitButton.gameObject.SetActive(false);

        //selectUnits(unitList);
    }
    public void selectUnits(Toggle buttonClicked) {
        //if(points > 0)
        Debug.Log(points);
        if (buttonClicked.GetComponent<Toggle>().isOn) {
            buttonClicked.GetComponent<Image>().color = new Color(1, 0, 0);
            Debug.Log(buttonClicked.gameObject.transform.GetChild(1).GetComponent<Text>().text);
            points -= int.Parse(buttonClicked.transform.GetChild(1).GetComponent<Text>().text.ToString());
            numSelected++;
        }
        if (!buttonClicked.GetComponent<Toggle>().isOn) {
            buttonClicked.GetComponent<Image>().color = new Color(1, 1, 1);
            points += int.Parse(buttonClicked.transform.GetChild(1).GetComponent<Text>().text.ToString());
            numSelected--;
        }
        Debug.Log(points);
        //Debug.Log(numSelected);
        foreach (GameObject unit in unitList) {
            if (numSelected >= 4) {
                if (!unit.GetComponent<Toggle>().isOn) {
                    unit.GetComponent<Toggle>().interactable = false;
                    confirmTeam.GetComponent<Button>().interactable = true;
                }
            } else {
                if(int.Parse(unit.transform.GetChild(1).GetComponent<Text>().text.ToString()) > points) {
                    unit.GetComponent<Toggle>().interactable = false;
                    if (unit.GetComponent<Toggle>().isOn) {
                        unit.GetComponent<Toggle>().interactable = true;
                    }
                } else {
                    unit.GetComponent<Toggle>().interactable = true;
                }
                confirmTeam.GetComponent<Button>().interactable = false;
            }
            
        }

    }
    public void initialiseUnits() {
        unitA1 = GameObject.Find("UnitA1");
        unitA2 = GameObject.Find("UnitA2");
        unitA3 = GameObject.Find("UnitA3");
        unitA4 = GameObject.Find("UnitA4");
        unitB1 = GameObject.Find("UnitB1");
        unitB2 = GameObject.Find("UnitB2");
        unitB3 = GameObject.Find("UnitB3");
        unitB4 = GameObject.Find("UnitB4");
        initialiseTeams();
    }
    public void resetNums() {
        GameObject removeall;
        numSelected = 0;
        points = 8000;
        isTeam1 = false;
        unitList.Clear();
        removeall = GameObject.Find("ButtonListContent");
        foreach(Transform child in removeall.transform) {
            if(child.name != "Unit"){
                GameObject.Destroy(child.gameObject);
            }
        }
        return;
    }
    public void setTeam() {
        int counter = 0;
        foreach (GameObject unit in unitList) {
            if(isTeam1){
                if (unit.GetComponent<Toggle>().isOn) {
                    counter++;
                    //check which team is going then do this
                    if (counter == 1) {
                        unitNames[0] = unit.name;
                    }
                    if (counter == 2) {
                        unitNames[1] = unit.name;
                    }
                    if (counter == 3) {
                        unitNames[2] = unit.name;
                    }
                    if (counter == 4) {
                        unitNames[3] = unit.name;
                    }
                }
            } else {
                if (unit.GetComponent<Toggle>().isOn) {
                    counter++;
                    //check which team is going then do this
                    if (counter == 1) {
                        unitNames[4] = unit.name;
                    }
                    if (counter == 2) {
                        unitNames[5] = unit.name;
                    }
                    if (counter == 3) {
                        unitNames[6] = unit.name;
                    }
                    if (counter == 4) {
                        unitNames[7] = unit.name;
                    }
                }
            }
        }
    }
    public void loadGame() {
        if (!isTeam1){
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        } else
            return;
    }
    public void loadMenu() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        reset();
    }
    public void reset() {
        isTeam1 = true;    
    }
}
