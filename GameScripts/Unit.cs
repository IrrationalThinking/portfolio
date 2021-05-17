using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;

/**
  * Author: Tom Kent-Peterson
  * Unit is a script which controls all of the information about each individual unit in my game
  * while very verbose each unit does have a lot of different status in an be in.
  * The script itself will only contain information about an individual unit and is a reference for other scripts to edit.
  */
public class Unit : MonoBehaviour
{
    public int health, mana, attack, defense, 
        magic, magicResistance, speed, luck, cost;
    public int totalHealth, totalMana, totalAttack, totalDefense,
        totalMagic, totalMagicResistance, totalSpeed, totalLuck;
    public string unit, faction, ability1, ability2, ability3, ability4, ability5, ability6, ability7;
    public Text uiName, uiHealth, uiMagic;
    public TextAsset unitStats;
    public int isSilenced, isStunned, isMaimed, isParalysed, isSlowed, isBurning, isScared, isWeak, isTaunted;
    public int isMagicImmune, isHasted, isStrengthened, damageResistance, isDamageResistant, isShielded;
    public bool isDead = false;
    public bool hasGone = false;
    public bool wasHitByAOE = false;
    public GameObject whoFeared, whoTaunted;
    // Start is called before the first frame update
    void Awake() {
        //unit = this.gameObject.name.ToString();

        //readFile();
    }
    //sets the name of the unit to the gameobject name
    void Start()
    {
        unit = this.gameObject.name.ToString();

        readFile();
    }

    // Update is called once per frame
    void Update()
    {

    }
    /* readFile is a method which reads the Units.txt file and inserts the information into the appropriate variables.
     * it firstly checks the name of the unit which is set in the Start method, then if that unit is found it adds the information
     * on that line to each variable seperated by a space.*/
    public void readFile() {
        int counter = 0;
        string line;
        string[] words;
        string path = "Assets/Resources/Units.txt";
        StreamReader reader = new StreamReader(path);
        while ((line = reader.ReadLine()) != null) {
            words = line.Split(' ');
            if(words[0] == unit) {
                faction = words[1];
                cost = int.Parse(words[2]);
                health = int.Parse(words[3]);
                mana = int.Parse(words[4]);
                attack = int.Parse(words[5]);
                defense = int.Parse(words[6]);
                magic = int.Parse(words[7]);
                magicResistance = int.Parse(words[8]);
                speed = int.Parse(words[9]);
                luck = int.Parse(words[10]);
                for(int i = 11; i < words.Length; i++) {
                    if(string.IsNullOrEmpty(ability1)){
                        ability1 = words[i];
                        ability1 = abilityTextfix(ability1);
                    }else if (string.IsNullOrEmpty(ability2)) {
                        ability2 = words[i];
                        ability2 = abilityTextfix(ability2);
                    } else if (string.IsNullOrEmpty(ability3)) {
                        ability3 = words[i];
                        ability3 = abilityTextfix(ability3);
                    } else if (string.IsNullOrEmpty(ability4)) {
                        ability4 = words[i];
                        ability4 = abilityTextfix(ability4);
                    } else if (string.IsNullOrEmpty(ability5)) {
                        ability5 = words[i];
                        ability5 = abilityTextfix(ability5);
                    } else if (string.IsNullOrEmpty(ability6)) {
                        ability6 = words[i];
                        ability6 = abilityTextfix(ability6);
                    } else if (string.IsNullOrEmpty(ability7)) {
                        ability7 = words[i];
                        ability7 = abilityTextfix(ability7);
                    }
                }
                totalHealth = health;
                totalMana = mana;
                totalAttack = attack;
                totalDefense = defense;
                totalMagic = magic;
                totalMagicResistance = magicResistance;
                totalSpeed = speed;
                totalLuck = luck;
                uiName = gameObject.transform.Find("Unit Name").GetComponent<Text>();
                uiName.text = unit;
                uiHealth = gameObject.transform.Find("Unit Health Value").GetComponent<Text>();
                uiHealth.text = (health + "/" + totalHealth).ToString();
                uiMagic = gameObject.transform.Find("Unit Magic Value").GetComponent<Text>();
                uiMagic.text = (mana + "/" + totalMana).ToString();
                break;
            } else {
                counter++;
            }
            
            
        }
        reader.Close();
    }
    /*abilityTextfix this method checks if there is a capital letter in the middle of a word because if there is it means that it should be two words
     * so this method adds a space inbetween them making the ability 2 words this causes bugs later so I use a similar check to see if they should be 1 word instead.*/
    public string abilityTextfix(string word) {
        string newString;
        newString = string.Concat(word.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        word = newString;
        return word;
    }
    /* updateStats just updates the current health/mana which is often called in the CombatContoller script*/
    public void updateStats() {
        uiHealth.text = (health + "/" + totalHealth).ToString();
        uiMagic.text = (mana + "/" + totalMana).ToString();
    }
    /* clearStatusEffects is a method which clears the unit of all debuffs often used in certain cure skills */
    public void clearStatusEffects() {
        isSilenced = 0;
        isMaimed = 0;
        isStunned = 0;
        isParalysed = 0;
        isBurning = 0;
        isScared = 0;
        isWeak = 0;
        isTaunted = 0;
        isSlowed = 0;
        speed = totalSpeed;
        attack = totalAttack;
        magic = totalMagic;
        magicResistance = totalMagicResistance;
        luck = totalLuck;
        whoFeared = null;
        whoTaunted = null;

    }
    /* statusEffects is a method which keeps track of the status effects a unit is under as well as reducing them by 1
     * this is called at the end of a defenders turn and usually has their effect trigger here too.
     */
    public void statusEffects() {
        if(isSilenced > 0) 
            isSilenced--;
        if (isStunned > 0)
            isStunned--;
        if (isMaimed > 0){
            isMaimed--;
            health = Convert.ToInt32(health * 0.9);
            if(health == 0) {
                health = 1;
            }
        }
        if (isParalysed > 0)
            isParalysed--;
        if (isSlowed > 0){
            isSlowed--;
            if(isSlowed == 0) {
                speed = totalSpeed;
            }
        }
        if (isBurning > 0){
            isBurning--;
            health = health - 5;
        }
        if (isScared > 0){
            isScared--;
            if(isScared == 0){
                whoFeared = null;
            }
        }
        if (isWeak > 0)
            isWeak--;
        if(isTaunted > 0) {
            isTaunted--;
            if(isTaunted == 0) {
                whoTaunted = null;
            }
        }
        if(isMagicImmune > 0) {
            isMagicImmune--;
        }
        if(isHasted > 0) {
            isHasted--;
            if(isHasted == 0) {
                speed = totalSpeed;
            }
        }
        if (isStrengthened > 0) {
            isStrengthened--;
            if(isStrengthened == 0) {
                attack = totalAttack;
            }
        }
        if(isDamageResistant > 0) {
            isDamageResistant--;
            if(isDamageResistant == 0) {
                damageResistance = 0;
            }
        }
        if(isShielded > 0) {
            isShielded--;
            if(isShielded == 0) {
                defense = totalDefense;
            }
        }
    }
    public void unitDead() {
        if (health <= 0) {
            isDead = true;
        }
    }
}