using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
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
    private void Update() {
        //fixes and issue with the game starting before all the units are ready, this looks awful and I will learn a better way to do it I promise
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
                    i++;
                }
            }
        }
    }
    public void startGame() {
        int f = 0;
        units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unit in units) {
            turnOrder[f] = unit.GetComponent<Unit>();
            f++;
        }

        return;
    }
    public void settingUp() {
        checkTurn();
        //sortingInitiative();
        endTurn.onClick.AddListener(checkTurn);
    }
    public void sortingInitiative() {
        int n = countUnitsGone(), i, j, flag;
        Debug.Log("n is " + n);
        Unit placeHolder;
        for(i = 1; i < n; i++) {

            placeHolder = turnOrder[i];
            //val = turnOrder[i].speed;
            flag = 0;

                for (j = i - 1; j>=0 && flag != 1;) {
                if (!turnOrder[j].GetComponent<Unit>().hasGone) {
                    if (placeHolder.speed > turnOrder[j].speed) {
                    turnOrder[j + 1] = turnOrder[j];
                    //turnOrder[j + 1].speed = turnOrder[i].speed;
                    j--;
                    turnOrder[j + 1] = placeHolder;
                    //turnOrder[j + 1].speed = val;

                    } else {
                        flag = 1;
                    }
                } else {
                    break;
                }
                
            }
            
           //Debug.Log("current array order is " + i + " " + turnOrder[i]);
           //Debug.Log("current speed of each unit is " + turnOrder[i].speed);
        }

        //System.Array.Reverse(turnOrder);
        for(int f = 0; f<turnOrder.Length; f++) {
            Debug.Log("index " + f + " is, unit is " + turnOrder[f].gameObject);
        }
        //reorderDead();
    }
    public void moveQueue() {
        int deadUnits = 0;
        Unit temp;
        for (int i = 0; i < turnOrder.Length - 1; i++) {
            if (turnOrder[i].GetComponent<Unit>().isDead){
                //reorderDead();
                deadUnits++;
            }
        }
            //Unit tobeMoved;
        //turnOrder[0].GetComponent<Unit>().hasGone = true;
        //tobeMoved = turnOrder[0];
        //turnOrder[0] = null;
        for (int i = 0; i < turnOrder.Length-1; i++) {
            /*if (i == turnOrder.Length) {
                turnOrder[i] = tobeMoved;
            }*/
            if(!turnOrder[i+1].GetComponent<Unit>().isDead){
            //temp = turnOrder[i];
            //temp = turnOrder[i];
            //turnOrder[i] = null;
            //for (int f = i; f < turnOrder.Length-1; f++) {
                //temp = turnOrder[f-1];
                if(i != turnOrder.Length-1 - deadUnits){
                    temp = turnOrder[i+1];
                    turnOrder[i+1] = turnOrder[i];
                    turnOrder[i] = temp;
                }

            } //else {
                //reorderDead();
            //}
        }
        /*for(int i = 0; i < turnOrder.Length; i++) {
            Debug.Log("current turn order is " + turnOrder[i]);
        }*/
    }
    public void reorderDead() {
        Unit tempDead;
        Unit temp;
        //int counter = 0;
        for(int i = 0; i < turnOrder.Length; i++) {
            if (turnOrder[i].GetComponent<Unit>().isDead) {
                //Debug.Log("hi I is " + i);
                tempDead = turnOrder[i];
                //counter++;
                turnOrder[i] = null;
                for(int f = i; f < turnOrder.Length-1; f++) {
                    //Debug.Log("hi f is " + f);
                    temp = turnOrder[f + 1];
                    turnOrder[f + 1] = turnOrder[f];
                    turnOrder[f] = temp;
                }
                turnOrder[7] = tempDead;
            }
        }
    }
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
    //slight bug where if one unit has already gone and the unit after it has to go it doesn't get reordered
    public void duringTurnHasGone(Unit hasGone) {
        Unit temp;
        for(int i = 0; i < turnOrder.Length; i++) {
            if(turnOrder[i] == hasGone) {
                temp = turnOrder[i];
                turnOrder[i] = null;
                for(int f = i; f < turnOrder.Length - 1; f++) {
                    temp = turnOrder[f + 1];
                    turnOrder[f + 1] = turnOrder[f];
                    turnOrder[f] = temp;
                }
                turnOrder[7] = hasGone;
            
            }
        }
    }
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
        currentUnit.GetComponent<Unit>().mana += currentUnit.GetComponent<Unit>().mana/10; //restores 10% of mana during a units turn
        if (currentUnit.GetComponent<Unit>().mana > currentUnit.GetComponent<Unit>().totalMana) {
            currentUnit.GetComponent<Unit>().mana = currentUnit.GetComponent<Unit>().totalMana;
        }
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
        } else {
            unitStatusEffected = 0;
        }     
    }
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
    public void turnArrowsoff() {
        foreach(GameObject unit in units) {
            unit.transform.Find("TargetArrow").GetComponent<Image>().enabled = false;
            unit.transform.Find("FriendlyArrow").GetComponent<Image>().enabled = false;
        }
    }
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
    // Update is called once per frame
   /* void EndTurn()
    {
        /*if (turnNumber >= 7) {
            Debug.Log("in if");
            turnNumber = 0;
            turnStart = 0;
        }


        checkTurn();
    }*/
}
