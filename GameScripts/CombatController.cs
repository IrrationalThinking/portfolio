using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class CombatController : MonoBehaviour
{
    public GameObject attacker;
    [HideInInspector]
    public GameObject defender;
    [HideInInspector]
    public GameObject[] teamEffected = new GameObject[4];
    public Text currentButton, currentAbility;
    [HideInInspector]
    public string search, abilityUsing;
    [HideInInspector]
    public GameMaster abilityData;
    public GameObject abilityCC1;
    public TurnEvent events;
    public string turnEvent;
    [HideInInspector]
    public int roll;
    public int damageStore;
    public bool wasAbility = false;
    public bool isFriendly = false;
    public bool targetAll = false;
    //public Button initTeamsTurn;
    // Start is called before the first frame update
    void Start()
    {
        abilityData = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void selectAbility(Button abilityClicked) {
        Uiscript callAttack = GameObject.Find("Arena").GetComponent<Uiscript>();
        GameObject currentTeam = GameObject.Find("Menu Panel").GetComponent<TurnController>().currentUnit.transform.parent.gameObject;
        GameObject parentOfButton = abilityClicked.transform.gameObject;
        currentAbility = parentOfButton.transform.GetComponent<Text>();
        abilityUsing = currentAbility.text;

        for (int i = 0; i < abilityData.abilities.Count; i++) {
            if (abilityData.abilities[i][0] == String.Concat(abilityUsing.ToString().Where(c => !Char.IsWhiteSpace(c)))) {
                Debug.Log("in this :))))))))))))");
                if (abilityData.abilities[i][2] == "F") {
                    Debug.Log("in this :)");
                    isFriendly = true;
                }else if(abilityData.abilities[i][2] == "FA") {
                    targetAll = true;
                    isFriendly = true;
                }else if(abilityData.abilities[i][2] == "EA") {
                    isFriendly = false;
                    targetAll = true;
                }
                break;
            }
        }
        callAttack.attackingEnemies();
    }
    //This prevents the user from storing an ability and then using a basic attack to use the ability without actually using it.
    public void deselectAbility() {
        abilityUsing = "";
        isFriendly = false;
        targetAll = false;
    }
    public void selectDefender(Button buttonClicked) {
        

        GameObject currentTeam = GameObject.Find("Menu Panel").GetComponent<TurnController>().currentUnit.transform.parent.gameObject;
        GameObject parentOfButton = buttonClicked.transform.gameObject;
        currentButton = parentOfButton.transform.GetComponent<Text>();
        Debug.Log(currentButton);
        search = currentButton.text;
        Debug.Log(search);
        //here I would search for the gameobject based on which team has a turn but because I don't have turns fully made yet I can just search for the 1 unit who is the tester for now
        Debug.Log(currentTeam.name);
        if (currentTeam.name == "Team 2 Panel") {
            if(!isFriendly){
                if(!targetAll){
                    defender = GameObject.Find("Team 1 Panel/" + search);
                    defender.transform.Find("TargetArrow").GetComponent<Image>().enabled = true;
                } else {
                    search = GameObject.Find("Team 1 Panel").transform.GetChild(0).name;
                    defender = GameObject.Find("Team 1 Panel/" + search);
                    foreach (GameObject arrow in abilityData.team1) {
                        arrow.transform.Find("FriendlyArrow").GetComponent<Image>().enabled = true;
                    }
                }
            } else {
                if(!targetAll){
                    defender = GameObject.Find("Team 2 Panel/" + search);
                    defender.transform.Find("FriendlyArrow").GetComponent<Image>().enabled = true;

                } else {
                    search = GameObject.Find("Team 2 Panel").transform.GetChild(0).name;
                    defender = GameObject.Find("Team 2 Panel/" + search);
                    foreach (GameObject arrow in abilityData.team2) {
                        arrow.transform.Find("FriendlyArrow").GetComponent<Image>().enabled = true;
                    }
                }
            }
        } else {
            if(!isFriendly){
                if(!targetAll){
                    defender = GameObject.Find("Team 2 Panel/" + search);
                    defender.transform.Find("TargetArrow").GetComponent<Image>().enabled = true;
                } else {
                    search = GameObject.Find("Team 2 Panel").transform.GetChild(0).name;
                    defender = GameObject.Find("Team 2 Panel/" + search);
                    foreach(GameObject arrow in abilityData.team2) {
                        arrow.transform.Find("TargetArrow").GetComponent<Image>().enabled = true;
                    }
                }
            } else {
                if(!targetAll){
                    defender = GameObject.Find("Team 1 Panel/" + search);
                    defender.transform.Find("FriendlyArrow").GetComponent<Image>().enabled = true;
                } else {
                    search = GameObject.Find("Team 1 Panel").transform.GetChild(0).name;
                    defender = GameObject.Find("Team 1 Panel/" + search);
                    foreach (GameObject arrow in abilityData.team1) {
                        arrow.transform.Find("FriendlyArrow").GetComponent<Image>().enabled = true;
                    }
                }
            }
        }
        //defender = GameObject.Find(currentTeam.name + "/" + search);
        Debug.Log(defender);
        
    }
    public void action() {
        roll = UnityEngine.Random.Range(1, 100);
        if (string.IsNullOrEmpty(abilityUsing))
            attack();
        else
            //Debug.Log("in else :)");
            ability();
        events.setText(turnEvent); //temp
    }
    //searches for a specific string and then does the ability based on the rules associated with the string, 
    //this will be changed later down the line as the game gets larger, 
    //as reading it like this is awful and I will probably use a list of lists or something similar later on
    public void ability() {
        //String.Concat(abilityName.text.ToString().Where(c => !Char.IsWhiteSpace(c)))
        string[] rowUsed = new string[9];
        for(int i = 0; i < abilityData.abilities.Count; i++) {
            if(String.Concat(abilityUsing.ToString().Where(c => !Char.IsWhiteSpace(c))) == abilityData.abilities[i][0]) {
                int cost = int.Parse(abilityData.abilities[i][1]);
                abilityCost(cost);
                for(int f = 0; f < abilityData.abilities[i].Length; f++) { //check to see if it works
                    rowUsed[f] = abilityData.abilities[i][f];
                }
                if(!targetAll){
                    if(rowUsed[8] == "M"){
                        abilityDamage(true, rowUsed);
                    } else {
                        abilityDamage(false, rowUsed);
                    }
                } else {
                    abilityDamageEntireTeam(rowUsed);
                }
            }
        }
    }
    public void abilityCost(int cost) {
        int newMana;
        newMana = attacker.GetComponent<Unit>().mana;
        newMana = newMana - cost;
        attacker.GetComponent<Unit>().mana = newMana;
        attacker.GetComponent<Unit>().updateStats();
    }
    public void checkCost() {
        int currentMana = attacker.GetComponent<Unit>().mana;
        //Button currentItem;
        int counter = 0;
        int maxCount = 0;
        string[] abilityCC = new string[4];
        //string abilityCC1, abilityCC2, abilityCC3, abilityCC4;
        if (!string.IsNullOrEmpty(attacker.GetComponent<Unit>().ability1)) {
            abilityCC[0] = attacker.GetComponent<Unit>().ability1;
            maxCount++;
            if (!string.IsNullOrEmpty(attacker.GetComponent<Unit>().ability2)) {
                abilityCC[1] = attacker.GetComponent<Unit>().ability2;
                maxCount++;
                if (!string.IsNullOrEmpty(attacker.GetComponent<Unit>().ability3)) {
                    abilityCC[2] = attacker.GetComponent<Unit>().ability3;
                    maxCount++;
                    if (!string.IsNullOrEmpty(attacker.GetComponent<Unit>().ability4)) {
                        abilityCC[3] = attacker.GetComponent<Unit>().ability4;
                        maxCount++;
                    }
                }
            }
        }
        while(counter != maxCount) {
            
            for (int i = 0; i < abilityData.abilities.Count; i++) {
                if (abilityCC[counter] == abilityData.abilities[i][0]) {
                    if(currentMana < int.Parse(abilityData.abilities[i][1])) {
                        //currentItem = GameObject.Find("Ability" + counter+1).GetComponent<Button>();
                        //currentItem.interactable = false;
                        if(counter == 0){
                            Debug.Log(abilityData.abilities[i][1]);
                            //GameObject.Find("Ability1").SetActive(true);
                            //abilityCC1.GetComponent<Button>().interactable = false;
                            GameObject.Find("Ability1").GetComponent<Button>().enabled = false;
                            //GameObject.Find("Ability1").GetComponent<Button>().interactable = false;
                            //GameObject.Find("Ability1").GetComponent<Button>().enabled = true;
                            Debug.Log(GameObject.Find("Ability1").GetComponent<Button>().IsInteractable());
                        } else if (counter == 1) {
                            //Debug.Log("Hi");
                            GameObject.Find("Ability2").GetComponent<Button>().enabled = false;
                        } else if (counter == 2) {
                            //Debug.Log("Hi");
                            GameObject.Find("Ability3").GetComponent<Button>().enabled = false;
                        }
                        if (counter == 3) {
                            //Debug.Log("Hi");
                            GameObject.Find("Ability4").GetComponent<Button>().enabled = false;
                        }
                    }
                }
            }
            counter++;
        }
        /*for (int i = 0; i < abilityData.abilities.GetLength(0); i++) {
            if (abilityCC1 == abilityData.abilities[i, 0]) {
                int cost = int.Parse(abilityData.abilities[i, 1]);
                abilityCost(cost);
            }

        }*/
    }
    //states the effects for the abilities and applies them to the unit affected e.g stun will cause an enemy to lose their turn
    public void abilityEffect(string[] ability) {
        if(ability[3] == "slowed" || ability[4] == "slowed" || ability[5] == "slowed") {
            doSlow(int.Parse(ability[6]));
        }
        if (ability[3] == "maimed" || ability[4] == "maimed" || ability[5] == "maimed") {
            doMaim(int.Parse(ability[6]));
        }
        if (ability[3] == "paralysed" || ability[4] == "paralysed" || ability[5] == "paralysed") {
            doParalyse(int.Parse(ability[6]));
        }
        if (ability[3] == "stunned" || ability[4] == "stunned" || ability[5] == "stunned") {
            doStun(int.Parse(ability[6]));
        }
        if (ability[3] == "feared" || ability[4] == "feared" || ability[5] == "feared") {
            doFear(int.Parse(ability[6]));
        }
        if (ability[3] == "burning" || ability[4] == "burning" || ability[5] == "burning") {
            doBurning(int.Parse(ability[6]));
        }
        if (ability[3] == "weakened" || ability[4] == "weakened" || ability[5] == "weakened") {
            doWeaken(int.Parse(ability[6]));
        }
        if (ability[3] == "silenced" || ability[4] == "silenced" || ability[5] == "silenced") {
            doWeaken(int.Parse(ability[6]));
        }
        if (ability[3] == "taunted" || ability[4] == "taunted" || ability[5] == "taunted") {
            doTaunted(int.Parse(ability[6]));
        }
        if(ability[3] == "dead" || ability[4] == "dead" || ability[5] == "dead") {
            doDie();
        }
        if (ability[3] == "manaRenewed" || ability[4] == "manaRenewed" || ability[5] == "manaRenewed") {
            magicGain(int.Parse(ability[6]));
        }
        if (ability[3] == "cleansed" || ability[4] == "cleansed" || ability[5] == "cleansed") {
            clearDebuffs();
        }
        if (ability[3] == "hasted" || ability[4] == "hasted" || ability[5] == "hasted") {
            doHaste(int.Parse(ability[6]));
        }
        if (ability[3] == "oxstrength" || ability[4] == "oxstrength" || ability[5] == "oxstrength") {
            doStrength(int.Parse(ability[6]));
        }
        if (ability[3] == "shielded" || ability[4] == "shielded" || ability[5] == "shielded") {
            doArmourIncrease(int.Parse(ability[6]));
        }
        if (ability[3] == "damageResist" || ability[4] == "damageResist" || ability[5] == "damageResist") {
            doDamageResistance(int.Parse(ability[6]));
        }
        if (ability[3] == "lifeStolen" || ability[4] == "lifeStolen" || ability[5] == "lifeStolen") {
            doLifeSteal();
        }
        if (ability[3] == "soulStolen" || ability[4] == "soulStolen" || ability[5] == "soulStolen") {
            doManaDrain();
        }
    }
    public void doSlow(int dur) {
        if(!targetAll){
            defender.GetComponent<Unit>().speed = defender.GetComponent<Unit>().speed / 2;
            defender.GetComponent<Unit>().isSlowed = dur;
        } else {
            for(int i = 0; i < teamEffected.Length; i++) {
                if (teamEffected[i].GetComponent<Unit>().wasHitByAOE){
                    teamEffected[i].GetComponent<Unit>().speed = teamEffected[i].GetComponent<Unit>().speed / 2;
                    teamEffected[i].GetComponent<Unit>().isSlowed = dur;
                }
            }
        }
    }
    public void doMaim(int dur) {
        if(!targetAll){
            defender.GetComponent<Unit>().isMaimed = dur;
        } else {
            for (int i = 0; i < teamEffected.Length; i++) {
                if (teamEffected[i].GetComponent<Unit>().wasHitByAOE) {
                    teamEffected[i].GetComponent<Unit>().isMaimed = dur;
                }
            }
        }
    }
    public void doParalyse(int dur) {
        if(!targetAll){
            defender.GetComponent<Unit>().isParalysed = dur;
        } else {
            for (int i = 0; i < teamEffected.Length; i++) {
                if (teamEffected[i].GetComponent<Unit>().wasHitByAOE) {
                    teamEffected[i].GetComponent<Unit>().isParalysed = dur;
                }
            }
        }
    }
    public void doStun(int dur) {
        if (!targetAll) {
            defender.GetComponent<Unit>().isStunned = dur;
        } else {
            for (int i = 0; i < teamEffected.Length; i++) {
                if (teamEffected[i].GetComponent<Unit>().wasHitByAOE) {
                    teamEffected[i].GetComponent<Unit>().isStunned = dur;
                }
            }
        }
    }
    public void doFear(int dur) {
        if(!targetAll){
            defender.GetComponent<Unit>().isScared = dur;
            defender.GetComponent<Unit>().whoFeared = attacker;
        } else {
            for (int i = 0; i < teamEffected.Length; i++) {
                if (teamEffected[i].GetComponent<Unit>().wasHitByAOE) {
                    teamEffected[i].GetComponent<Unit>().isScared = dur;
                teamEffected[i].GetComponent<Unit>().whoFeared = attacker;
                }
            }
        }
    }
    public void doBurning(int dur) {
        if (!targetAll) {
            defender.GetComponent<Unit>().isBurning = dur;
        } else {
            for (int i = 0; i < teamEffected.Length; i++) {
                if (teamEffected[i].GetComponent<Unit>().wasHitByAOE) {
                    teamEffected[i].GetComponent<Unit>().isBurning = dur;
                }
            }
        }
    }
    public void doWeaken(int dur) {
        if (!targetAll) {
            defender.GetComponent<Unit>().attack = defender.GetComponent<Unit>().attack / 2;
            defender.GetComponent<Unit>().isWeak = dur;
        } else {
            for (int i = 0; i < teamEffected.Length; i++) {
                if (teamEffected[i].GetComponent<Unit>().wasHitByAOE) {
                    teamEffected[i].GetComponent<Unit>().attack = teamEffected[i].GetComponent<Unit>().attack / 2;
                teamEffected[i].GetComponent<Unit>().isWeak = dur;
                }
            }
        }
    }
    public void doSilenced(int dur) {
        if (!targetAll) {
            defender.GetComponent<Unit>().isSilenced = dur;
        } else {
            for (int i = 0; i < teamEffected.Length; i++) {
                if (teamEffected[i].GetComponent<Unit>().wasHitByAOE) {
                    teamEffected[i].GetComponent<Unit>().isSilenced = dur;
                }
            }
        }
    }
    public void doTaunted(int dur) {
        if (!targetAll) {
            defender.GetComponent<Unit>().isTaunted = dur;
            defender.GetComponent<Unit>().whoTaunted = attacker;
        } else {
            for (int i = 0; i < teamEffected.Length; i++) {
                if (teamEffected[i].GetComponent<Unit>().wasHitByAOE) {
                    teamEffected[i].GetComponent<Unit>().isTaunted = dur;
                    teamEffected[i].GetComponent<Unit>().whoTaunted = attacker;
                }
            }
        }
    }
    public void doDie() {
        defender.GetComponent<Unit>().health = 0;
    }
    public void magicGain(int power) {
        int manaGained = 0;
        if(power == 1){
            manaGained = UnityEngine.Random.Range(5, 10);
        }else if(power == 2) {
            manaGained = UnityEngine.Random.Range(10, 30);
        } else {
            manaGained = UnityEngine.Random.Range(25, 50);
        }
        if(defender.GetComponent<Unit>().mana + manaGained >= defender.GetComponent<Unit>().totalMana){
            defender.GetComponent<Unit>().mana = defender.GetComponent<Unit>().totalMana;
        } else {
            defender.GetComponent<Unit>().mana += manaGained;
        }
    }
    public void doHaste(int dur) {
        if (!targetAll) {
            defender.GetComponent<Unit>().speed = defender.GetComponent<Unit>().speed * 2;
            defender.GetComponent<Unit>().isHasted = dur;
        } else {
            for (int i = 0; i < teamEffected.Length; i++) {
                if (teamEffected[i].GetComponent<Unit>().wasHitByAOE) {
                    teamEffected[i].GetComponent<Unit>().speed = teamEffected[i].GetComponent<Unit>().speed * 2;
                    teamEffected[i].GetComponent<Unit>().isSlowed = dur;
                }
            }
        }
    }
    public void doStrength(int dur) {
        if (!targetAll) {
            defender.GetComponent<Unit>().attack = defender.GetComponent<Unit>().attack * 2;
            defender.GetComponent<Unit>().isStrengthened = dur;
        } else {
            for (int i = 0; i < teamEffected.Length; i++) {
                if (teamEffected[i].GetComponent<Unit>().wasHitByAOE) {
                    teamEffected[i].GetComponent<Unit>().attack = teamEffected[i].GetComponent<Unit>().attack * 2;
                    teamEffected[i].GetComponent<Unit>().isStrengthened = dur;
                }
            }
        }
    }
    public void doArmourIncrease(int dur) {
        defender.GetComponent<Unit>().isShielded = dur;
        defender.GetComponent<Unit>().defense = defender.GetComponent<Unit>().defense * 2;
    }
    public void doDamageResistance(int dur) {
        int magnitude = UnityEngine.Random.Range(2, 10);
        defender.GetComponent<Unit>().damageResistance = magnitude;
        defender.GetComponent<Unit>().isDamageResistant = dur;
    }
    public void clearDebuffs() {
        defender.GetComponent<Unit>().clearStatusEffects();
    }
    public void doMagicImmune(int dur) {
        defender.GetComponent<Unit>().isMagicImmune = dur;
    }
    public void doLifeSteal() {
        attacker.GetComponent<Unit>().mana += damageStore;
    }
    public void doManaDrain() {
        attacker.GetComponent<Unit>().health += damageStore;
    }
    //checks if the ability is magic or physical if the attack is physical just do an attack instead but lose mana
    public void abilityDamage(bool isMagic, string[] abilityName) {
        bool isCrit = false;
        if (isMagic) {
            string listOfStatusEffects = "";
            string team = attacker.transform.parent.name;
            string teamAttacking;
            //Debug.Log("I made it in :)");
            int mDef = defender.GetComponent<Unit>().magicResistance;
            int magicAC = 45 + mDef;
            int magic = attacker.GetComponent<Unit>().magic;
            int health = defender.GetComponent<Unit>().health;
            int totalAtk = magic + roll;
            int damage = 0;
            if (attacker.transform.parent.name == "Team 1 Panel") {
                teamAttacking = "Team 1";
                //teamDefending = "Team 2";
            } else {
                teamAttacking = "Team 2";
                //teamDefending = "Team 1";
            }
            if (isFriendly)
                roll = 10000; //makes it so the heal/buff autohits
            Debug.Log("roll is " + roll);
            if (roll+magic >= magicAC && defender.GetComponent<Unit>().isMagicImmune == 0) {
                if(roll > 100 - attacker.GetComponent<Unit>().luck) {
                    isCrit = true;
                }
                Debug.Log("roll and magic was equal to " + roll+magic + " vs the enemies magicAC which was " + magicAC);
                for(int i = 0; i < abilityData.abilities.Count; i++) {
                    if(abilityData.abilities[i][0] == abilityName[0]) {
                        string damageText = abilityData.abilities[i][7];
                        if (damageText != "0") {
                            string[] letters = damageText.Split('d');
                            damage = calculateMagicDamage(letters);
                            Debug.Log("overall damage was " + damage);
                            if (isCrit) {
                                damage += damage;
                            }
                            if (!isFriendly){
                                    health = health - damage;
                            } else {
                                health = health + damage;
                                if(health > defender.GetComponent<Unit>().totalHealth) {
                                        health = defender.GetComponent<Unit>().totalHealth;
                                }
                            }
                            damageStore = damage;
                            defender.GetComponent<Unit>().health = health;
                            defender.GetComponent<Unit>().updateStats();
                            Debug.Log(defender.GetComponent<Unit>().health);
                        }
                        if (abilityName[3] != "n") {
                            abilityEffect(abilityName);
                            if (abilityName[4] != "n") {
                                if (abilityName[5] != "n") {
                                    listOfStatusEffects = abilityName[3] + " " + abilityName[4] + " " + abilityName[5];
                                } else {
                                    listOfStatusEffects = abilityName[3] + " " + abilityName[4];
                                }
                            } else {
                                listOfStatusEffects = abilityName[3];
                            }
                        }
                        break;
                    }
                }
                if(damage == 0){
                    if(isFriendly){
                        eventLogger(damage, totalAtk, magicAC, teamAttacking, isCrit, "M", true, true, false, false, abilityName, listOfStatusEffects);
                    } else {
                        eventLogger(damage, totalAtk, magicAC, teamAttacking, isCrit, "M", true, false, false, false, abilityName, listOfStatusEffects);
                    }
                }
            } else {
                if(defender.GetComponent<Unit>().isMagicImmune == 0){
                    eventLogger(damage, totalAtk, magicAC, teamAttacking, isCrit, "M", false, false, false, false, abilityName, listOfStatusEffects);
                }
                else{
                    eventLogger(damage, totalAtk, magicAC, teamAttacking, isCrit, "M", false, false, false, true, abilityName, listOfStatusEffects);
                }
            }

        } else {
            wasAbility = true;

            attack(abilityName);
        }
        isFriendly = false;
    }

    public void abilityDamageEntireTeam(string[] abilityName) {
        string listOfStatusEffects = "";
        string teamAttacking = "";
        string teamDefending = "";
        GameObject parentOfDefender = defender.transform.parent.gameObject;
        int magic = attacker.GetComponent<Unit>().magic;
        if (isFriendly)
            roll = 10000;
        if(parentOfDefender.name == "Team 1 Panel") {
            teamEffected = abilityData.team1;
            teamAttacking = "Team 2";
            teamDefending = "Team 1";
        }else if(parentOfDefender.name == "Team 2 Panel") {
            teamEffected = abilityData.team2;
            teamAttacking = "Team 1";
            teamDefending = "Team 2";
        }
        int[] teamMDef = new int[4];
        int[] teamHealth = new int[4];
        int[] teamMagicAC = new int[4];
        int overallDamage = 0;
        int numberEffected = 0;
        populateAoEStats(teamMDef, "mDef");
        populateAoEStats(teamHealth, "health");
        populateAoEStats(teamMagicAC, "magicAC");
        for(int i = 0; i<teamMDef.Length; i++) {
            if(roll+magic > teamMagicAC[i] && teamEffected[i].GetComponent<Unit>().isMagicImmune == 0 && teamEffected[i].GetComponent<Unit>().isDead == false) {
                numberEffected++;
                teamEffected[i].GetComponent<Unit>().wasHitByAOE = true;
                for (int f = 0; f < abilityData.abilities.Count; f++) {
                    if (abilityData.abilities[f][0] == abilityName[0]) {
                        string damageText = abilityData.abilities[f][7];
                        if(damageText != "0") {
                            string[] letters = damageText.Split('d');
                            int damage = calculateMagicDamage(letters);
                            overallDamage += damage;
                            if (!isFriendly) {
                                teamHealth[i] = teamHealth[i] - damage;
                            } else {
                                teamHealth[i] = teamHealth[i] + damage;
                                if (teamHealth[i] > teamEffected[i].GetComponent<Unit>().totalHealth) {
                                    teamHealth[i] = teamEffected[i].GetComponent<Unit>().totalHealth;
                                }
                            }
                            teamEffected[i].GetComponent<Unit>().health = teamHealth[i];
                            teamEffected[i].GetComponent<Unit>().updateStats();
                        }
                        if (abilityName[3] != "n") {
                            abilityEffect(abilityName);
                            if (abilityName[4] != "n") {
                                if (abilityName[5] != "n") {
                                    listOfStatusEffects = abilityName[3] + " " + abilityName[4] + " " + abilityName[5];
                                } else {
                                    listOfStatusEffects = abilityName[3] + " " + abilityName[4];
                                }
                            } else {
                                listOfStatusEffects = abilityName[3];
                            }
                        }
                       break;
                    }
                }
            }
        }
        for(int i = 0; i < teamEffected.Length; i++) {
            teamEffected[i].GetComponent<Unit>().wasHitByAOE = false;
        }
        if(numberEffected > 0){
            if (!isFriendly) {
                eventLogger(0, 0, 0, teamAttacking, false, "M", true, false, true, false, abilityName, listOfStatusEffects, numberEffected, overallDamage, teamDefending);
            } else {
                eventLogger(0, 0, 0, teamAttacking, false, "M", true, true, true, false, abilityName, listOfStatusEffects, numberEffected, overallDamage, teamDefending);
            }
        } else {
            eventLogger(0, 0, 0, teamAttacking, false, "M", false, false, true, false, abilityName, listOfStatusEffects, numberEffected, overallDamage, teamDefending);
        }
        isFriendly = false;
        targetAll = false;
    }

    public void populateAoEStats(int[] array, string datatype) {
        for(int i = 0; i<array.Length; i++) {
            if(datatype == "health") {
                array[i] = teamEffected[i].GetComponent<Unit>().health;
            }else if (datatype == "magicAC") {
                array[i] = teamEffected[i].GetComponent<Unit>().magicResistance + 45;
            }else if (datatype == "mDef") {
                array[i] = teamEffected[i].GetComponent<Unit>().magicResistance;
            }
        }
    }
    public int calculateMagicDamage(string[] nums) {
        int damage = 0;
        for(int i = 0; i < int.Parse(nums[0]); i++) {
            damage += UnityEngine.Random.Range(1, int.Parse(nums[1]));
        }
        return damage;
    }
    public void attack(string[] abilityName = null) {
        bool isCrit = false;
        string team = attacker.transform.parent.name;
        string teamAttacking;
        string listOfStatusEffects = "";
        int def = defender.GetComponent<Unit>().defense;
        int armourClass = 40 + def;
        int atk = attacker.GetComponent<Unit>().attack;
        int health = defender.GetComponent<Unit>().health;
        int damage = 0;
        int totalAtk =  atk + roll;
        Debug.Log("randomised roll is " + roll);
        if (attacker.transform.parent.name == "Team 1 Panel") {
            teamAttacking = "Team 1";
            //teamDefending = "Team 2";
        } else {
            teamAttacking = "Team 2";
            //teamDefending = "Team 1";
        }
        if (roll + atk >= armourClass) {
            if(roll > 100 - attacker.GetComponent<Unit>().luck) {
                isCrit = true;
            }
            if (wasAbility) {
                Debug.Log("In attack ability :)");
                for (int i = 0; i < abilityData.abilities.Count; i++) {
                    if (abilityData.abilities[i][0] == abilityName[0]) {
                        string damageText = abilityData.abilities[i][7];
                        if (damageText != "0") {
                            string[] letters = damageText.Split('d');
                            damage = calculateMagicDamage(letters);
                        }
                        if (abilityName[3] != "n") {
                            abilityEffect(abilityName);
                            if(abilityName[4] != "n") {
                                if(abilityName[5] != "n") {
                                    listOfStatusEffects = abilityName[3] + " " + abilityName[4] + " " + abilityName[5];
                                } else {
                                    listOfStatusEffects = abilityName[3] + " " + abilityName[4];
                                }
                            } else {
                                listOfStatusEffects = abilityName[3];
                            }
                        }
                        break;
                    }
                }
            } else {
                damage = UnityEngine.Random.Range(1, 10);
            }
            Debug.Log("current Randomised damage is " + damage);
            if (atk / 4 > 1 && abilityName == null) {
                damage = damage + atk / 4;
            }
            //Debug.Log("attack bonus is " + atk/4);
            if (isCrit) {
                damage += damage;
            }
            Debug.Log("overall damage was " + damage);
            if (defender.GetComponent<Unit>().isDamageResistant == 0) {
                health = health - damage;
            } else {
                if (damage - defender.GetComponent<Unit>().damageResistance >= 0){ //if this isn't true then damage will be 0 so I ignore changing the health
                    health = health - damage + defender.GetComponent<Unit>().damageResistance;
                }
            }

            damageStore = damage;
            defender.GetComponent<Unit>().health = health;
            defender.GetComponent<Unit>().updateStats();
            Debug.Log(defender.GetComponent<Unit>().health);
            if(wasAbility){
                if(abilityName[3] != "n"){
                    eventLogger(damage, totalAtk, armourClass, teamAttacking, isCrit, "P", true, false, false, false, abilityName, listOfStatusEffects);
                }
            } else {
                eventLogger(damage, totalAtk, armourClass, teamAttacking, isCrit, "P", true, false, false, false);
            }
        } else {
            eventLogger(damage, totalAtk, armourClass, teamAttacking, false, "P", false, false, false, false);
        }
        wasAbility = false;
        
        //string teamDefending;   
    }
    public void eventLogger(int damage, int totalAtk, int AC, string teamAttacking, bool wasCrit, string attackType, bool hit, bool friendly, 
                bool isAreaOfEffect, bool isMagicImmune, string[] abilityName = null, string statusEffect = "", int numberEffected = 0, int overAllDamage = 0, string teamDefending = "") {
        if(!isAreaOfEffect) {
            if (attackType == "M") {
                if (hit){
                    if (friendly) {
                        if (damage != 0){
                            if(!wasCrit){
                                if (statusEffect == "") {
                                    Debug.Log("Line 752");
                                    turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has healed them for " + damage;
                                } else {
                                    Debug.Log("Line 755");
                                    if (abilityName[6] == "0")
                                        turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has healed them for " + damage + " and has affected them with " + statusEffect;
                                    else if (abilityName[6] == "1")
                                        turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has healed them for " + damage + " and has affected them with " + statusEffect + " for " + abilityName[6] + " round";
                                    else
                                        turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has healed them for " + damage + " and has affected them with " + statusEffect + " for " + abilityName[6] + " rounds";

                                }
                            } else {
                                if(statusEffect == ""){
                                    Debug.Log("Line 760");
                                    turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has critically healed them for " + damage;
                                } else {
                                    Debug.Log("Line 763");
                                    if (abilityName[6] == "0")
                                        turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has critically healed them for " + damage + " and has affected them with " + statusEffect;
                                    else if (abilityName[6] == "1")
                                        turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has critically healed them for " + damage + " and has affected them with " + statusEffect + " for " + abilityName[6] + " round";
                                    else
                                        turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has critically healed them for " + damage + " and has affected them with " + statusEffect + " for " + abilityName[6] + " rounds";
                                }
                            }
                        } else {
                            Debug.Log("Line 768");
                            if (abilityName[6] == "0")
                                turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has affected them with " + statusEffect;
                            else if (abilityName[6] == "1")
                                turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has affected them with " + statusEffect + " for " + abilityName[6] + " round";
                            else
                                turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has affected them with " + statusEffect + " for " + abilityName[6] + " rounds";
                        }
                    } else{
                        if (damage == 0) {
                            if (statusEffect != ""){
                                Debug.Log("Line 774");
                                if(abilityName[6] == "0")
                                    turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has hit with a roll of "
                                        + totalAtk + " vs a Magic Resistance check of " + AC + ". " + defender.name + " is now " + statusEffect;
                                else if(abilityName[6] == "1") 
                                    turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has hit with a roll of "
                                        + totalAtk + " vs a Magic Resistance check of " + AC + ". " + defender.name + " is now " + statusEffect + " for " + abilityName[6] + " round";
                                 else
                                    turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has hit with a roll of "
                                        + totalAtk + " vs a Magic Resistance check of " + AC + ". " + defender.name + " is now " + statusEffect + " for " + abilityName[6] + " rounds";
                            } else {
                                Debug.Log("Line 778");
                                turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has hit with a roll of "
                                    + totalAtk + " vs a Magic Resistance check of " + AC;
                            }
                        } else {
                            if(statusEffect == ""){
                                if(!wasCrit){
                                    Debug.Log("Line 785");
                                    turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has hit with a roll of "
                                            + totalAtk + " vs a Magic Resistance check of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name;
                                } else {
                                    Debug.Log("Line 789");
                                    int neededForCrit = 100 - attacker.GetComponent<Unit>().luck;
                                    turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has critically hit with a roll of "
                                        + totalAtk + " (needed for crit " + neededForCrit + " ) vs a Magic Resistance check of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name;
                                }
                            } else {
                                if(!wasCrit){
                                    Debug.Log("Line 796");
                                    turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has hit with a roll of "
                                            + totalAtk + " vs a Magic Resistance check of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name
                                            + " and " + defender.name + " is now " + statusEffect;
                                } else {
                                    Debug.Log("Line 801");
                                    int neededForCrit = 100 - attacker.GetComponent<Unit>().luck;
                                    if (abilityName[6] == "0")
                                        turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has critically hit with a roll of "
                                             + totalAtk + " (needed for crit " + neededForCrit + " ) vs a Magic Resistance check of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name
                                             + " and " + defender.name + " is now " + statusEffect;
                                    
                                    else if(abilityName[6] != "1")
                                        turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has critically hit with a roll of "
                                             + totalAtk + " (needed for crit " + neededForCrit + " ) vs a Magic Resistance check of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name
                                             + " and " + defender.name + " is now " + statusEffect + " for " + abilityName[6] + " round";
                                    else
                                        turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and has critically hit with a roll of "
                                              + totalAtk + " (needed for crit " + neededForCrit + " ) vs a Magic Resistance check of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name
                                              + " and " + defender.name + " is now " + statusEffect + " for " + abilityName[6] + " rounds";
                                }
                            }
                        }
                    }
                } else {
                    if (!isMagicImmune) {
                        Debug.Log("Line 812");
                        turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and " + defender.name
                            + " has resisted the effect with a roll of " + totalAtk + " vs a Magic Resistance check of " + AC;
                    } else {
                        Debug.Log("Line 816");
                        turnEvent = attacker.name + " of " + teamAttacking + " has cast " + abilityName[0] + " on " + defender.name + " and " + defender.name
                            + " is magic immune so it has no effect.";
                    }
                }
            } else {
                if(abilityName == null){
                    if (hit) {
                        if (defender.GetComponent<Unit>().damageResistance != 0) {
                            if(damage - defender.GetComponent<Unit>().damageResistance >= 0){
                                Debug.Log("Line 826");
                                turnEvent = attacker.name + " of " + teamAttacking + " has attacked " + defender.name + " and has hit with a roll of " + totalAtk + " vs an AC of " + AC
                                 + ", " + attacker.name + " would deal " + damage + " damage to " + defender.name + " but " + defender.name
                                 + " has ignored the blow with damage resistance";
                            }else{
                                Debug.Log("Line 831");
                                turnEvent = attacker.name + " of " + teamAttacking + " has attacked " + defender.name + " and has hit with a roll of " + totalAtk + " vs an AC of " + AC
                                  + ", " + attacker.name + " deals " + damage + " damage to " + defender.name + " but " + defender.name
                                  + " has reduced the damage by " + defender.GetComponent<Unit>().damageResistance;
                            }
                        } else {
                            if(!wasCrit){
                                Debug.Log("Line 838");
                                turnEvent = attacker.name + " of " + teamAttacking + " has attacked " + defender.name + " and has hit with a roll of " + totalAtk + " vs an AC of " + AC
                                   + ", " + attacker.name + " deals " + damage + " damage to " + defender.name;
                            } else {
                                Debug.Log("Line 842");
                                int neededForCrit = 100 - attacker.GetComponent<Unit>().luck;
                                turnEvent = attacker.name + " of " + teamAttacking + " has attacked " + defender.name + " and has critically hit with a roll of " + totalAtk + " (needed for crit " + neededForCrit + " ) vs an AC of " + AC
                                    + ", " + attacker.name + " deals " + damage + " damage to " + defender.name;
                            }
                        }
                    } else {
                        Debug.Log("Line 849");
                        turnEvent = attacker.name + " of " + teamAttacking + " has attacked " + defender.name + " and has missed with a roll of " + totalAtk + " vs an AC of " + AC;
                    }
                } else {
                    if (damage == 0) {
                        if (statusEffect != "") {
                            Debug.Log("Line 855");
                            if(abilityName[6] == "0")
                                turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " against " + defender.name + " and has hit with a roll of "
                                    + totalAtk + " vs an Armour Class of " + AC + ". " + defender.name + " is now " + statusEffect;
                            else if(abilityName[6] == "1")
                                turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " against " + defender.name + " and has hit with a roll of "
                                    + totalAtk + " vs an Armour Class of " + AC + ". " + defender.name + " is now " + statusEffect + " for " + abilityName[6] + " round";
                            else
                                turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " against " + defender.name + " and has hit with a roll of "
                                    + totalAtk + " vs an Armour Class of " + AC + ". " + defender.name + " is now " + statusEffect + " for " + abilityName[6] + " rounds";
                        } else {
                            Debug.Log("Line 859");
                            turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " on " + defender.name + " and has hit with a roll of "
                               + totalAtk + " vs an Armour Class of " + AC;
                        }
                    } else {
                        if (statusEffect != "") {
                            if (!wasCrit) {
                                Debug.Log("Line 866");
                                if(abilityName[6] =="0")
                                    turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " on " + defender.name + " and has hit with a roll of "
                                    + totalAtk + " vs an Armour Class of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name
                                    + " and " + defender.name + " is now " + statusEffect;
                                else if(abilityName[6] == "1")
                                    turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " on " + defender.name + " and has hit with a roll of "
                                        + totalAtk + " vs an Armour Class of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name
                                        + " and " + defender.name + " is now " + statusEffect + " for " + abilityName[6] + " round";
                                else
                                    turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " on " + defender.name + " and has hit with a roll of "
                                        + totalAtk + " vs an Armour Class of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name
                                        + " and " + defender.name + " is now " + statusEffect + " for " + abilityName[6] + " rounds";
                            } else {
                                Debug.Log("Line 871");
                                int neededForCrit = 100 - attacker.GetComponent<Unit>().luck;

                                if (abilityName[6] == "0")
                                    turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " on " + defender.name + " and has hit a critical hit with a roll of "
                                        + totalAtk + " (needed for crit " + neededForCrit + " ) vs an Armour Class of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name
                                        + " and " + defender.name + " is now " + statusEffect;
                                else if(abilityName[6] == "1")
                                    turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " on " + defender.name + " and has hit a critical hit with a roll of "
                                        + totalAtk + " (needed for crit " + neededForCrit + " ) vs an Armour Class of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name
                                        + " and " + defender.name + " is now " + statusEffect + " for " + abilityName[6] + " round";
                                else
                                    turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " on " + defender.name + " and has hit a critical hit with a roll of "
                                        + totalAtk + " (needed for crit " + neededForCrit + " ) vs an Armour Class of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name
                                        + " and " + defender.name + " is now " + statusEffect + " for " + abilityName[6] + " rounds";
                            }
                        } else {
                            if(!wasCrit){
                                Debug.Log("Line 877");
                                turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " on " + defender.name + " and has hit with a roll of "
                                   + totalAtk + " vs an Armour Class of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name;
                            } else {
                                Debug.Log("Line 882");
                                int neededForCrit = 100 - attacker.GetComponent<Unit>().luck;
                                turnEvent = attacker.name + " of " + teamAttacking + " has attacked with " + abilityName[0] + " on " + defender.name + " and has hit a critical hit with a roll of "
                                    + totalAtk + " (needed for crit " + neededForCrit + " ) vs an Armour Class of " + AC + ", " + attacker.name + " deals " + damage + " damage to " + defender.name;
                            }
                        }
                    }
                }
            }
        } else {
            if(numberEffected > 0){
                if(friendly){
                    if(damage != 0){
                        if(statusEffect == "") {
                            Debug.Log("Line 897");
                            turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                                  + "all of " + teamDefending + " has been healed for a total of " + damage + " split between the living members";
                        } else {
                            Debug.Log("Line 901");
                            if (abilityName[6] == "0")
                                turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                                    + "all of " + teamDefending + " are healed " + damage + " split between the living members and effected by " + statusEffect;
                            else if (abilityName[6] == "1")
                                turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                                    + "all of " + teamDefending + " are healed " + damage + " split between the living members and effected by " + statusEffect + " for "
                                     + abilityName[6] + " round";
                            else
                                turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                                    + "all of " + teamDefending + " are healed " + damage + " split between the living members and effected by " + statusEffect + " for "
                                     + abilityName[6] + " rounds";
                        }
                    } else {
                        Debug.Log("Line 906");
                        if (abilityName[6] == "0")
                            turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                                + "all of " + teamDefending + " are effected by " + statusEffect;
                        else if (abilityName[6] == "1")
                            turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                                + "all of " + teamDefending + " are effected by " + statusEffect + " for " + abilityName[6] + " round";
                        else
                            turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                                + "all of " + teamDefending + " are effected by " + statusEffect + " for " + abilityName[6] + " rounds";
                    }
                } else {
                    if (statusEffect == "") {
                        Debug.Log("Line 912");
                        turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                                + " " + numberEffected + "/4 were effected for an overall damage total of " + damage;
                    } else {
                        Debug.Log("Line 916");
                        if (abilityName[6] == "0")
                            turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                                + " " + numberEffected + "/4 were effected for an overall damage total of " + damage + " and damaged members are effected by " +
                                statusEffect;
                        else if (abilityName[6] == "1")
                            turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                                + " " + numberEffected + "/4 were effected for an overall damage total of " + damage + " and damaged members are effected by " +
                                statusEffect + " for " + abilityName[6] + " round";
                        else
                            turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                                + " " + numberEffected + "/4 were effected for an overall damage total of " + damage + " and damaged members are effected by " +
                                statusEffect + " for " + abilityName[6] + " rounds";
                    }

                }
            } else {
                Debug.Log("Line 923");
                turnEvent = attacker.name + " of " + teamAttacking + " has cast the area of effect spell " + abilityName[0] + " on " + teamDefending
                    + " but everyone resisted it";
            }
        }
    }
}
