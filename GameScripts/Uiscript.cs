using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.EventSystems;

public class Uiscript : MonoBehaviour
{
    //public GameObject[] abilities = new GameObject[4];
    public Text[] teamUnits = new Text[4]; //ability1, ability2, ability3, ability4;
    public GameObject team1, team2, abilityInfoPan1, abilityInfoPan2;
    public Text[] abilities = new Text[4];
    [HideInInspector]
    public GameObject[] unitStats = new GameObject[10];
    public GameObject[] scores = new GameObject[2];
    //Transform objects;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        keepScore();
    }

    public void populateTargetButtons(GameObject team) {
        int i = 0;
        foreach (Transform unit in team.gameObject.transform) {
            teamUnits[i].text = unit.GetComponentInChildren<Text>().text;
            if (unit.GetComponentInChildren<Unit>().isDead) {
                teamUnits[i].GetComponentInChildren<Button>().interactable = false;
                teamUnits[i].GetComponent<EventTrigger>().enabled = false;
            } else {
                teamUnits[i].GetComponentInChildren<Button>().interactable = true;
                teamUnits[i].GetComponent<EventTrigger>().enabled = true;
            }
            i++;
        }
    }
    public void populateTargetingAll(GameObject team, bool isfriendlyTeam) {
        int i = 0;
        foreach (Transform unit in team.gameObject.transform) {
            teamUnits[i].text = unit.GetComponentInChildren<Text>().text;
            teamUnits[i].GetComponentInChildren<Button>().interactable = true;
            teamUnits[i].GetComponent<EventTrigger>().enabled = true;
            if (i > 0) {
                Debug.Log("hi I am in here for the UI script");
                teamUnits[i].text = "";
                teamUnits[i].GetComponentInChildren<Button>().interactable = false;
                teamUnits[i].GetComponent<EventTrigger>().enabled = false;
            }
            i++;
        }
        if(!isfriendlyTeam){
            teamUnits[0].text = "All Enemies";
        } else {
            teamUnits[0].text = "All Allies";
        }
    }

    public void attackingEnemies() {
        CombatController getCombatController = GameObject.Find("Combat Panel").GetComponent<CombatController>();
        bool getFriendly = getCombatController.isFriendly;
        bool allTarget = getCombatController.targetAll;
        //int i = 0;
        GameObject currentTeam = GameObject.Find("Menu Panel").GetComponent<TurnController>().currentUnit.transform.parent.gameObject;
        //Debug.Log(currentTeam);
        if(currentTeam.name == team1.name){
            if(!getFriendly){
                if(!allTarget){
                    populateTargetButtons(team2);
                } else {
                    populateTargetingAll(team2, getFriendly);
                }
                /*foreach (Transform unit in team2.gameObject.transform){
                    teamUnits[i].text = unit.GetComponentInChildren<Text>().text;
                    if (unit.GetComponentInChildren<Unit>().isDead) {
                        teamUnits[i].GetComponentInChildren<Button>().interactable = false;
                    } else {
                        teamUnits[i].GetComponentInChildren<Button>().interactable = true;
                    }
                    i++;
                }*/
            } else {
                if(!allTarget){
                    Debug.Log("is friendly for team 1");
                    populateTargetButtons(team1);
                } else {
                    populateTargetingAll(team1, getFriendly);
                }
                /*foreach (Transform unit in team1.gameObject.transform) {
                    teamUnits[i].text = unit.GetComponentInChildren<Text>().text;
                    if (unit.GetComponentInChildren<Unit>().isDead) {
                        teamUnits[i].GetComponentInChildren<Button>().interactable = false;
                    } else {
                        teamUnits[i].GetComponentInChildren<Button>().interactable = true;
                    }
                    i++;
                }*/
            }
        } else {
            if(!getFriendly){
                if(!allTarget){
                    populateTargetButtons(team1);
                } else{
                    populateTargetingAll(team1, getFriendly);
                }/*foreach (Transform unit in team1.gameObject.transform) {
                    teamUnits[i].text = unit.GetComponentInChildren<Text>().text;
                    if (unit.GetComponentInChildren<Unit>().isDead) {
                        teamUnits[i].GetComponentInChildren<Button>().interactable = false;
                    } else {
                        teamUnits[i].GetComponentInChildren<Button>().interactable = true;
                    }
                    i++;
                }*/
            } else {
                if(!allTarget){
                    Debug.Log("is Friendly for team 2");
                    populateTargetButtons(team2);
                } else {
                    populateTargetingAll(team2, getFriendly);
                }
                /*foreach (Transform unit in team2.gameObject.transform) {
                    teamUnits[i].text = unit.GetComponentInChildren<Text>().text;
                    if (unit.GetComponentInChildren<Unit>().isDead) {
                        teamUnits[i].GetComponentInChildren<Button>().interactable = false;
                    } else {
                        teamUnits[i].GetComponentInChildren<Button>().interactable = true;
                    }
                    i++;
                }*/
            }
        }
        /*ability1 = gameObject.transform.Find("Ability1").GetComponent<Text>();
        ability2 = gameObject.transform.Find("Ability2").GetComponent<Text>();
        ability3 = gameObject.transform.Find("Ability3").GetComponent<Text>();
        ability4 = gameObject.transform.Find("Ability4").GetComponent<Text>();*/
        //Transform firstChild = gameObject.transform.Find("Team 2 Panel").GetChild(0);
        /*ability1 = team2.GetComponentInChildren<Text>(); //transform.GetComponent<Text>();
        ability1.text = team2.name;
        ability2 = team2.GetComponentInChildren<Text>();
        ability2.text = team2.name;
        ability3 = team2.GetComponentInChildren<Text>();
        ability3.text = team2.name;
        ability4 = team2.GetComponentInChildren<Text>();
        ability4.text = team2.name;*/
    }
    public void infoPanel(Button button) {
        int i = 0;
        GameObject unit1 = GameObject.Find("UnitInfoTeam1");
        GameObject unit2 = GameObject.Find("UnitInfoTeam2");
        GameObject unitInfo;
        ///Debug.Log(button.transform.parent.name);
        if (button.transform.parent.name == "Team 1 Panel"){
            unitInfo = GameObject.Find("UnitInfoTeam1/Unit Stats");
            foreach(Transform child in unitInfo.transform) {
                unitStats[i] = child.gameObject;
                i++;
            }
            i = 0;
            unitStats[0].transform.Find("AttackValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().attack.ToString();
            unitStats[1].transform.Find("SpeedValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().speed.ToString();
            unitStats[2].transform.Find("MagicValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().magic.ToString();
            unitStats[3].transform.Find("DefenseValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().defense.ToString();
            unitStats[4].transform.Find("LuckValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().luck.ToString();
            unitStats[5].transform.Find("MrValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().magicResistance.ToString();
            if(!string.IsNullOrEmpty(button.GetComponent<Unit>().ability1)){
                unitStats[6].GetComponent<Text>().text = button.GetComponent<Unit>().ability1;
            } else {
                unitStats[6].GetComponent<Text>().text = "";
            }
            if (!string.IsNullOrEmpty(button.GetComponent<Unit>().ability2)) {
                unitStats[7].GetComponent<Text>().text = button.GetComponent<Unit>().ability2;
            } else {
                unitStats[7].GetComponent<Text>().text = "";
            }
            if (!string.IsNullOrEmpty(button.GetComponent<Unit>().ability3)) {
                unitStats[8].GetComponent<Text>().text = button.GetComponent<Unit>().ability3;
            } else {
                unitStats[8].GetComponent<Text>().text = "";
            }
            if (!string.IsNullOrEmpty(button.GetComponent<Unit>().ability4)) {
                unitStats[9].GetComponent<Text>().text = button.GetComponent<Unit>().ability4;
            } else {
                unitStats[9].GetComponent<Text>().text = "";
            }
            unit1.transform.Find("Unit Name").GetComponent<Text>().text = button.GetComponent<Unit>().uiName.text;
            /*unitStats[0].GetComponentInChildren<Text>().text = button.GetComponentInParent<Unit>().attack.ToString();
            unitStats[1].GetComponentInChildren<Text>().text = button.GetComponentInParent<Unit>().speed.ToString();
            unitStats[2].GetComponentInChildren<Text>().text = button.GetComponentInParent<Unit>().magic.ToString();
            unitStats[3].GetComponentInChildren<Text>().text = button.GetComponentInParent<Unit>().defense.ToString();
            unitStats[4].GetComponentInChildren<Text>().text = button.GetComponentInParent<Unit>().luck.ToString();
            unitStats[5].GetComponentInChildren<Text>().text = button.GetComponentInParent<Unit>().magicResistance.ToString();*/
        } else if (button.transform.parent.name == "Team 2 Panel") {
            unitInfo = GameObject.Find("UnitInfoTeam2/Unit Stats");
            foreach (Transform child in unitInfo.transform) {
                unitStats[i] = child.gameObject;
                i++;
            }
            i = 0;
            unitStats[0].transform.Find("AttackValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().attack.ToString();
            unitStats[1].transform.Find("SpeedValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().speed.ToString();
            unitStats[2].transform.Find("MagicValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().magic.ToString();
            unitStats[3].transform.Find("DefenseValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().defense.ToString();
            unitStats[4].transform.Find("LuckValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().luck.ToString();
            unitStats[5].transform.Find("MrValue").GetComponentInChildren<Text>().text = button.GetComponent<Unit>().magicResistance.ToString();
            if (!string.IsNullOrEmpty(button.GetComponent<Unit>().ability1)) {
                unitStats[6].GetComponent<Text>().text = button.GetComponent<Unit>().ability1;
            } else {
                unitStats[6].GetComponent<Text>().text = "";
            }
            if (!string.IsNullOrEmpty(button.GetComponent<Unit>().ability2)) {
                unitStats[7].GetComponent<Text>().text = button.GetComponent<Unit>().ability2;
            } else {
                unitStats[7].GetComponent<Text>().text = "";
            }
            if (!string.IsNullOrEmpty(button.GetComponent<Unit>().ability3)) {
                unitStats[8].GetComponent<Text>().text = button.GetComponent<Unit>().ability3;
            } else {
                unitStats[8].GetComponent<Text>().text = "";
            }
            if (!string.IsNullOrEmpty(button.GetComponent<Unit>().ability4)) {
                unitStats[9].GetComponent<Text>().text = button.GetComponent<Unit>().ability4;
            } else {
                unitStats[9].GetComponent<Text>().text = "";
            }
            unit2.transform.Find("Unit Name").GetComponent<Text>().text = button.GetComponent<Unit>().uiName.text;
        }
    }
    public void unitClicked(GameObject spriteParent) {
        Debug.Log(spriteParent);
        Button button = spriteParent.GetComponent<Button>();
        Debug.Log(button);
        infoPanel(button);
    }
    public void keepScore() {
        int team1score = 4;
        int team2score = 4;
        foreach (Transform unit in team1.gameObject.transform) {
            if (unit.GetComponentInChildren<Unit>().isDead) {
                team1score--;
            }
        }
        foreach (Transform unit in team2.gameObject.transform) {
            if (unit.GetComponentInChildren<Unit>().isDead) {
                team2score--;
            }
        }
        scores[0].GetComponent<Text>().text = team1score + "/4";
        scores[1].GetComponent<Text>().text = team2score + "/4";
    }
    public void abilityInfo(Text abilityName) {
        GameObject currentParent;
        GameObject unitTeam;
        Unit currentUnit;
        GameMaster unitAbilities = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        string unitName;
        string[] abilityEffects = new string[3];
        string[] abilityDescriptions = new string[] {"Slow will half the speed of the effected unit for the duration ", "Stun will prevent the unit from using the attack command for the duration ",
            "Paralyse will prevent the unit from taking actions for the duration ", "Maim will cause the enemy to bleed during their turn for the duration ", "Burning will burn the target during their turn for the duration ",
            "Silenced will prevent the unit from using the magic command for the duration", "Weaken will cause the enemy unit to lose half their attack power for the duration ",
            "Fear will prevent the unit from attacking the source of the fear for the duration ", "Taunt will force the target to attack the source of the taunt for the duration "};
        GameObject parent = abilityName.transform.parent.parent.gameObject;
        Debug.Log(abilityName.transform.parent.parent.gameObject);
        if(parent.name == "UnitInfoTeam1") {
            currentParent = abilityInfoPan1;
            unitTeam = team1;
        } else {
            currentParent = abilityInfoPan2;
            unitTeam = team2;
        }
        currentParent.transform.Find("AbilityName").GetComponent<Text>().text = abilityName.text;
        unitName = currentParent.transform.parent.transform.GetChild(0).GetComponent<Text>().text.ToString();
        currentUnit = unitTeam.transform.Find(unitName).GetComponent<Unit>();
        for(int i = 0; i < unitAbilities.abilities.Count; i++) {
            if(unitAbilities.abilities[i][0] == String.Concat(abilityName.text.ToString().Where(c => !Char.IsWhiteSpace(c)))) { //because the abilities don't have whilespace I remove it for the check
                if(unitAbilities.abilities[i][3] != "n"){
                    abilityEffects[0] = unitAbilities.abilities[i][3];
                    if(unitAbilities.abilities[i][4] != "n") {
                        abilityEffects[1] = unitAbilities.abilities[i][4];
                        if(unitAbilities.abilities[i][5] != "n") {
                            abilityEffects[2] = unitAbilities.abilities[i][5];
                        }
                    }
                }
                break;
            }
        }
        currentParent.transform.GetChild(1).GetComponent<Text>().text = abilityName.text.ToString() + " is a magical attack which deals " + currentUnit.magic + " damage to the targeted unit. This is reduced by the targets magic defense stat";
        if (!string.IsNullOrEmpty(abilityEffects[0])) {
            currentParent.transform.GetChild(3).GetComponent<Text>().text = debuffDescription(abilityEffects[0], abilityDescriptions);
            if (!string.IsNullOrEmpty(abilityEffects[1])) {
                currentParent.transform.GetChild(4).GetComponent<Text>().text = debuffDescription(abilityEffects[1], abilityDescriptions);
                if (!string.IsNullOrEmpty(abilityEffects[2])) {
                    currentParent.transform.GetChild(5).GetComponent<Text>().text = debuffDescription(abilityEffects[2], abilityDescriptions);
                } else {
                    currentParent.transform.GetChild(5).GetComponent<Text>().text = "";
                }
            } else {
                currentParent.transform.GetChild(4).GetComponent<Text>().text = "";
                currentParent.transform.GetChild(5).GetComponent<Text>().text = "";
            }
        } else {
            currentParent.transform.GetChild(3).GetComponent<Text>().text = "";
            currentParent.transform.GetChild(4).GetComponent<Text>().text = "";
            currentParent.transform.GetChild(5).GetComponent<Text>().text = "";
        }
        /*
         make the description of the ability
         make the description of the status effects
         
         */
        
        //Debug.Log(currentUnit);
    }
    public string debuffDescription(string debuffName, string[] descriptions) {
        string description = "";
        if(debuffName == "slowed") {
            description = descriptions[0];
        }else if (debuffName == "stunned") {
            description = descriptions[1];
        }else if (debuffName == "paralysed") {
            description = descriptions[2];
        }else if (debuffName == "maimed") {
            description = descriptions[3];
        }else if (debuffName == "burning") {
            description = descriptions[4];
        }else if (debuffName == "silenced") {
            description = descriptions[5];
        }else if (debuffName == "weakened") {
            description = descriptions[6];
        }else if (debuffName == "feared") {
            description = descriptions[7];
        }else if (debuffName == "taunted") {
            description = descriptions[8];
        }
        return description;
    }
}
    
