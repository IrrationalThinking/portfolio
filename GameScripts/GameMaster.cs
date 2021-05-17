using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
/**
 * Author: Tom Kent-Peterson
 * GameMaster is a script which controls a majority of the games base systems, these systems include the teams, the units, the faction,
 * most of the functions of this are in the main menu but I carry the gameobject storing the script over because I use the script to keep track of everything game rules related.
 */
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

    //This allows the scene changing to occur
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
    //Start method sets the gamemaster gameobject to not be destroyed on scene change
    void Start() {
        DontDestroyOnLoad(gameObject);
    }
    
    /* initialiseTeams is a method which once each player has choosen their units this method puts those units into each team.
    */
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
    /* checkUnits is a method which goes through each unit slot and grabs the script information out of it (I don't think I'm using this but not 100% sure)*/
    /* void checkUnits() {
        for(int i = 0; i < (team1.Length+team2.Length); i++) {
            if(i < team1.Length){
                units[i] = team1[i].GetComponentInChildren<Unit>();
            } else {
                units[i] = team2[i-team2.Length].GetComponentInChildren<Unit>();
            }
        }
    }*/
    /* readAbilities is a method which reads a file which contains all of the abilities in the game, 
     * these abilities are added to a list along with their properties which is used for searching purposes*/
    public void readAbilities() {
        string line;
        string[] words;
        string path = "Assets/Resources/Abilities.txt";
        StreamReader reader = new StreamReader(path);
        while ((line = reader.ReadLine()) != null) {
            words = line.Split(' ');
            abilities.Add(words);
        }
        reader.Close();
    }
    /* setFaction is a short method which just sets the teams faction this is used so when the unit selection happens only units from that faction will appear*/
    public void setFaction(Button button) {
        currentFaction = button.GetComponentInChildren<Text>().text.ToString();
    }
    /* populateButtons is a method which reads the Units file and generates prefab buttons based on how many units matches the teams faction,
     * the method does this by checking the faction which is listed in the 2nd word in the file and checks it based on the setFaction method above.*/
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

        reader.Close();
    }
    /* selectUnits is a method which controls the button prefabs which are generated in the populateButtons method above,
     * the way it works is it checks and counts how many of the toggles have been clicked so the user cannot have more or less than 4 units in their team.
     * there is also a price cost for each unit which is either deducted or added to the points total when the user either activates or deactivates the toggle.
     * if the user lacks points or has selected 4 units the toggles which they cannot afford or haven't been selected are deactivated until the user deselects another toggle.*/
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
    /* initialiseUnits is a method which selects the unit slots in the new scene, then calls initialiseTeams so the selected units are put into the correct slots*/
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
    /* resetNums is the method which upon the first team finishing their unit selections resets everything that would've been effected by that team,
     * such as points spent, units selected, and the actual unit prefabs themselves*/
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
    /* setTeam is a method which puts those units into the currently selected team, 
     * first the method checks which team is choosing then checks what unit buttons are toggled on.
     * after which it sets the name of the unit gameobject which is used for identifying purposes later on.*/
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
    /* loadGame loads the game if team 2 is the active one if not it returns and lets team 2 pick */
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
    //reset just sets the menu to team1 in case the player wishes to play again.
    public void reset() {
        isTeam1 = true;    
    }
}
