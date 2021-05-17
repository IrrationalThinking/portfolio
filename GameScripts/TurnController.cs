using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/**
 * Author: Tom Kent-Peterson
 * TurnController is a script which is used to control the rules of the games turn based system, 
 * this will be used whenever the player confirms an action but not the actual actions themselves.
 **/
public class TurnController : MonoBehaviour {
    [HideInInspector]
    public GameObject currentUnit;
    public GameObject turnArrowOff;
    public GameObject[] units = new GameObject[8];
    //public GameObject[] arrows = new GameObject[8];
    public Unit[] turnOrder = new Unit[8];
    public Text turnText, roundText;
    [HideInInspector]
    int unitStatusEffected = 0;
    //int turnNumber = 1;
    int turnStart = 0;
    int roundNumber = 0;
    //int unitGone = 0;
    int gameStarted = 0;
    public Button endTurn;
    public GameObject currentTurn;
    // Start is called before the first frame update
    void Start()
    {
        startGame();
        
    }
    /* this update method is specifically used to prevent an issue in the game where the scene is loaded before the unit scripts are done initialising, 
     * this is a bit of a bandaid solution and I hope to find a better one in the future.
     * also it controls a portion of the status effect system which prevents a unit from doing certain actions under certain effects
     */
    private void Update() {
        if(gameStarted == 0) {
            int counter = 0;
            for(int f = 0; f < turnOrder.Length; f++) {
                if(turnOrder[f].GetComponent<Unit>().speed != 0) {
                    counter++;
                }
            }
            if(counter == 8){
                settingUp();
                gameStarted = 1;
            }
        }
        GameObject temp;
        int i = 1;
        if (unitStatusEffected == 1){
            temp = GameObject.Find("Combat Panel").GetComponent<CombatController>().attacker;
            foreach (Text item in GameObject.Find("Combat Panel").GetComponentsInChildren<Text>()) {
                //Debug.Log("hi:)))))))))))");
                //Debug.Log(item.gameObject.name);
                if (item.gameObject.name == ("Unit" + i).ToString()) {
                    Debug.Log("hi:)))))))))))");
                    if (item.text != temp.GetComponent<Unit>().whoTaunted.name) {
                        //Debug.Log("hi:)))))))))))");
                        item.GetComponent<Button>().interactable = false;
                    }
                    if(item.gameObject.name == ("Unit" + i).ToString()) {
                        if (item.text == temp.GetComponent<Unit>().whoFeared.name) {
                            item.GetComponent<Button>().interactable = false;
                        }
                    }
                    i++;
                }
            }
        }
    }
    /* startGame is a method which puts the units into the turn order without sorting them once the game has been initialised. */
    public void startGame() {
        int f = 0;
        units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unit in units) {
            turnOrder[f] = unit.GetComponent<Unit>();
            f++;
        }
        return;
    }
    //settingUp initialises the turn scripts for the first time.
    public void settingUp() {
        checkTurn();
        endTurn.onClick.AddListener(checkTurn);
    }
    /* sortingInitiative is a method which sorts the turn order of the game using insertion sort but it sorts it in reverse,
     * so instead of doing smallest to largest it does largest to smallest.
     * it does also come with a safety catch which if a unit who has gone is next up this method will prevent that unit from going prior to others.*/
    public void sortingInitiative() {
        int n = countUnitsGone(), i, j, flag;
        Debug.Log("n is " + n);
        Unit placeHolder;
        for(i = 1; i < n; i++) {
            placeHolder = turnOrder[i];
            flag = 0;
                for (j = i - 1; j>=0 && flag != 1;) {
                if (!turnOrder[j].GetComponent<Unit>().hasGone) {
                    if (placeHolder.speed > turnOrder[j].speed) {
                    turnOrder[j + 1] = turnOrder[j];
                    j--;
                    turnOrder[j + 1] = placeHolder;
                    } else {
                        flag = 1;
                    }
                } else {
                    break;
                }
            }
        }
        for(int f = 0; f<turnOrder.Length; f++) {
            Debug.Log("index " + f + " is, unit is " + turnOrder[f].gameObject);
        }
    }
    /* moveQueue is a method which treats the turn order like a queue system
     * so rather than moving up the array for deciding who is next it instead shifts the array so whoever has gone will be last in the queue. 
     * the main reasoning for this is because the speeds of units can change during the game 
     * this system makes it a lot easier to manage as units change order often
     * and that lead to a lot of bugs in turn orders which took me far too long to fix so I went with this method instead.
     * The method checks which units are dead so dead units cannot have a turn either.*/
    public void moveQueue() {
        int deadUnits = 0;
        Unit temp;
        for (int i = 0; i < turnOrder.Length - 1; i++) {
            if (turnOrder[i].GetComponent<Unit>().isDead){
                deadUnits++;
            }
        }
        for (int i = 0; i < turnOrder.Length-1; i++) {
            if(!turnOrder[i+1].GetComponent<Unit>().isDead){
                if(i != turnOrder.Length-1 - deadUnits){
                    temp = turnOrder[i+1];
                    turnOrder[i+1] = turnOrder[i];
                    turnOrder[i] = temp;
                }

            }
        }
    }
    /*reorderDead is a method which moves dead units to the back of the queue, so they can't get a turn
     * I do this by using a temp variable which moves up the array by swapping with the entity infront of it,
     * unfortunately this causes a bug where the sorting is unstable, so if someone were to see the ordering
     * the dead units would swap quite frequently in turn order, but never actually have a turn.*/
    public void reorderDead() {
        Unit tempDead;
        Unit temp;
        for(int i = 0; i < turnOrder.Length; i++) {
            if (turnOrder[i].GetComponent<Unit>().isDead) {
                tempDead = turnOrder[i];
                turnOrder[i] = null;
                for(int f = i; f < turnOrder.Length-1; f++) {
                    temp = turnOrder[f + 1];
                    turnOrder[f + 1] = turnOrder[f];
                    turnOrder[f] = temp;
                }
                turnOrder[7] = tempDead;
            }
        }
    }
    /* countUnitsGone is a method which at the start of each turn checks the amount of units who have had a turn,
     * this allows the next turn to have the unit order reset by checking the amount of units alive vs the units who have gone
     * so for example if 6 units with 1 dead unit had gone when this method would know that next turn is the last turn until the round resets.*/
    public int countUnitsGone() {
        int count = 8;
        for(int i = 0; i < turnOrder.Length; i++) {
            if (turnOrder[i].GetComponent<Unit>().hasGone) {
                count--;
            }
            else if (turnOrder[i].GetComponent<Unit>().isDead) {
                count--;
            }
        }
        return count;
    }
    /* checkTurn is the method which controls the entirety of the games turn system, it allows the user to see who has their turn by using an arrow sprite
     * each turn that happens the method checks a number of methods to check if everything is as should be, first of all it checks how many units are alive,
     * then checks how many units have gone, this allows the game to know if a new round should be started, during each end turn the sortingInitiative method is used
     * along with the reorderDead method to make sure no units go when they shouldn't if the round hasn't reset then the moveQueue method is called moving the queue along by 1.
     * once the game knows whose turn it is the game restores 10% of the units mana, so mana reliant units aren't useless when they run out of mana, 
     * then it checks if the unit has any status effects which might effect its ability to do certain actions. After that it populates the magic buttons
     * once it knows which unit is going.*/
    public void checkTurn() {
        if (turnArrowOff != null) {
            turnArrowOff.transform.Find("TurnArrow").GetComponent<Image>().enabled = false;
            turnArrowOff = null;
        }
        int hasGoneCounter = 0;
        checkAlive();
        for(int i = 0; i < turnOrder.Length; i++) {
            if (turnOrder[i].GetComponent<Unit>().hasGone || turnOrder[i].GetComponent<Unit>().isDead) {
                hasGoneCounter++;
            }
        }
        if(hasGoneCounter == 8) {
            turnStart = 0;
        }
        //shifts the first unit to the end of the queue then rechecks the queue to see if anything has a turn when they shouldn't e.g someones speed is affected.
        if(turnStart == 1) {
            
            moveQueue();
            sortingInitiative();
            reorderDead();
        }
        else if(turnStart == 0) {
            Debug.Log("in turn start is 0 :)");
            for (int i = 0; i < turnOrder.Length; i++) {
                turnOrder[i].GetComponent<Unit>().hasGone = false;
            }
            //unitGone = 0;
            roundNumber++;
            turnStart = 1;
            sortingInitiative();
            reorderDead();
            GameObject.Find("RoundNumber").GetComponent<Text>().text = roundNumber.ToString();
        }
        //just in case something breaks and a dead unit attempts to go
        //sortingInitiative();
        if (turnOrder[0].GetComponent<Unit>().isDead)
            reorderDead();
        turnOrder[0].GetComponent<Unit>().hasGone = true;
        currentUnit = turnOrder[0].gameObject;
        currentUnit.GetComponent<Unit>().mana += currentUnit.GetComponent<Unit>().totalMana/10; //restores 10% of mana during a units turn
        if (currentUnit.GetComponent<Unit>().mana > currentUnit.GetComponent<Unit>().totalMana) {
            currentUnit.GetComponent<Unit>().mana = currentUnit.GetComponent<Unit>().totalMana;
        }
        currentUnit.GetComponent<Unit>().updateStats();
        //currentUnit.GetComponent<Unit>().hasGone = true;
        //sortingInitiative();
        if (currentUnit.transform.parent.name == "Team 1 Panel") {
            currentTurn.GetComponent<Text>().text = currentUnit.name + " of Team 1";
        }
        if (currentUnit.transform.parent.name == "Team 2 Panel") {
            currentTurn.GetComponent<Text>().text = currentUnit.name + " of Team 2";
        }

        currentUnit.transform.Find("TurnArrow").GetComponent<Image>().enabled = true;
        turnArrowOff = currentUnit;
        //turnText.text = turnNumber.ToString();
        //turnNumber++;

        GameObject.Find("Combat Panel").GetComponent<CombatController>().attacker = currentUnit;
        //currentUnit.GetComponent<Unit>().hasGone = true;
        //unitGone++;
        currentUnit.GetComponent<Unit>().statusEffects();
        checkStatusEffect(currentUnit);
        Button magic = GameObject.Find("Magic").GetComponent<Button>();
        magic.onClick.AddListener(abilitySorting);
    }

    /*checkStatusEffects is a method which checks the status of a unit, if they have any effects which will effect their ability to do something
     * this method will make sure those rules are being followed, for example a paralysed unit cannot do anything, so their ability to do actions will be lost.*/
    public void checkStatusEffect(GameObject unit) {
        if(unit.GetComponent<Unit>().isParalysed > 0) {
            GameObject.Find("Attack").GetComponent<Button>().interactable = false;
            GameObject.Find("Magic").GetComponent<Button>().interactable = false;
        }
        if(unit.GetComponent<Unit>().isSilenced > 0) {
            GameObject.Find("Magic").GetComponent<Button>().interactable = false;
        }
        //temp
        if(unit.GetComponent<Unit>().isStunned > 0) {
            GameObject.Find("Attack").GetComponent<Button>().interactable = false;
        }
        if (unit.GetComponent<Unit>().isTaunted > 0) {
            unitStatusEffected = 1;
        }else if(unit.GetComponent<Unit>().isScared > 0) {
            unitStatusEffected = 1;   
        } else {
            unitStatusEffected = 0;
        }     
    }
    /* abilitySorting is the method which is called during a units turn which will format the units abilities correctly.
     * so if the ability slot isn't empty the text will be changed to that variable name which is a string of the ability name.
     * if the ability is empty is still be made blank and none interactable.*/
    public void abilitySorting() {
        if(!string.IsNullOrEmpty(currentUnit.GetComponent<Unit>().ability1)){
            GameObject.Find("Ability1").GetComponent<Text>().text = currentUnit.GetComponent<Unit>().ability1;
            GameObject.Find("Ability1").GetComponent<Button>().interactable = true;
            //currentUnit.GetComponent<Unit>().ability1
        } else {
            GameObject.Find("Ability1").GetComponent<Text>().text = "";
            GameObject.Find("Ability1").GetComponent<Button>().interactable = false;
        }
        if (!string.IsNullOrEmpty(currentUnit.GetComponent<Unit>().ability2)) {
            GameObject.Find("Ability2").GetComponent<Text>().text = currentUnit.GetComponent<Unit>().ability2;
            GameObject.Find("Ability2").GetComponent<Button>().interactable = true;
            //currentUnit.GetComponent<Unit>().ability1
        } else {
            GameObject.Find("Ability2").GetComponent<Text>().text = "";
            GameObject.Find("Ability2").GetComponent<Button>().interactable = false;
        }
        if (!string.IsNullOrEmpty(currentUnit.GetComponent<Unit>().ability3)) {
            GameObject.Find("Ability3").GetComponent<Text>().text = currentUnit.GetComponent<Unit>().ability3;
            GameObject.Find("Ability3").GetComponent<Button>().interactable = true;
            //currentUnit.GetComponent<Unit>().ability1
        } else {
            GameObject.Find("Ability3").GetComponent<Text>().text = "";
            GameObject.Find("Ability3").GetComponent<Button>().interactable = false;
        }
        if (!string.IsNullOrEmpty(currentUnit.GetComponent<Unit>().ability4)) {
            GameObject.Find("Ability4").GetComponent<Text>().text = currentUnit.GetComponent<Unit>().ability4;
            GameObject.Find("Ability4").GetComponent<Button>().interactable = true;
            //currentUnit.GetComponent<Unit>().ability1
        } else {
            GameObject.Find("Ability4").GetComponent<Text>().text = "";
            GameObject.Find("Ability4").GetComponent<Button>().interactable = false;
        }
        if (!string.IsNullOrEmpty(currentUnit.GetComponent<Unit>().ability5)) {
            GameObject.Find("Ability5").GetComponent<Text>().text = currentUnit.GetComponent<Unit>().ability5;
            GameObject.Find("Ability5").GetComponent<Button>().interactable = true;
            //currentUnit.GetComponent<Unit>().ability1
        } else {
            GameObject.Find("Ability5").GetComponent<Text>().text = "";
            GameObject.Find("Ability5").GetComponent<Button>().interactable = false;
        }
        if (!string.IsNullOrEmpty(currentUnit.GetComponent<Unit>().ability6)) {
            GameObject.Find("Ability6").GetComponent<Text>().text = currentUnit.GetComponent<Unit>().ability6;
            GameObject.Find("Ability6").GetComponent<Button>().interactable = true;
            //currentUnit.GetComponent<Unit>().ability1
        } else {
            GameObject.Find("Ability6").GetComponent<Text>().text = "";
            GameObject.Find("Ability6").GetComponent<Button>().interactable = false;
        }
        if (!string.IsNullOrEmpty(currentUnit.GetComponent<Unit>().ability7)) {
            GameObject.Find("Ability7").GetComponent<Text>().text = currentUnit.GetComponent<Unit>().ability7;
            GameObject.Find("Ability7").GetComponent<Button>().interactable = true;
            //currentUnit.GetComponent<Unit>().ability1
        } else {
            GameObject.Find("Ability7").GetComponent<Text>().text = "";
            GameObject.Find("Ability7").GetComponent<Button>().interactable = false;
        }
    }
    //turnArrowsoff small method which turns all sprite arrows off
    public void turnArrowsoff() {
        foreach(GameObject unit in units) {
            unit.transform.Find("TargetArrow").GetComponent<Image>().enabled = false;
            unit.transform.Find("FriendlyArrow").GetComponent<Image>().enabled = false;
        }
    }
    /* checkAlive is a method which controls if someone has won, by checking if either team has 4 dead units
     * to save computation time it only does a majority of the method if more than 4 units are dead.*/
    public void checkAlive() {
        int count = 0;
        foreach(GameObject unit in units) {
            unit.GetComponent<Unit>().unitDead();
            if (unit.GetComponent<Unit>().isDead) {
                count++;
            }
        }
        if(count >= 4) {
            GameMaster checkifGameOver = GameObject.Find("GameMaster").GetComponent<GameMaster>();
            int team1Alive = 4;
            int team2Alive = 4;
            foreach(GameObject unit in checkifGameOver.team1) {
                if (unit.GetComponent<Unit>().isDead) {
                    team1Alive--;
                }
                if(team1Alive == 0) {
                    checkifGameOver.gameEnd.SetActive(true);
                    GameObject.Find("Winner").GetComponent<Text>().text = "Team 2 has won";
                }
            }
            foreach (GameObject unit in checkifGameOver.team2) {
                if (unit.GetComponent<Unit>().isDead) {
                    team2Alive--;
                }
                if (team2Alive == 0) {
                    checkifGameOver.gameEnd.SetActive(true);
                    GameObject.Find("Winner").GetComponent<Text>().text = "Team 1 has won";
                }
            }
        }
    }
}
