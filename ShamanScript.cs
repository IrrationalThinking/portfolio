using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*This script allows the shaman to run ahead of the player and stay a fixed distance away until the time limit has been reached,
if the player makes a mistake and stumbles the timer is reset. Upon reaching a set distance away from the player a reward is given.
The shaman will sprint ahead of the player as a very fast speed and then slow down when the set distance is reached.
*/
public class ShamanScript : MonoBehaviour
{
    private GameObject player;
    public GameObject shaman;
    public static float shamanSpeed;
    public static bool paused = false;
    public float elapsedTime = 0.0f;
    private float shamanZ;
    private float shamanX;
    private bool timerReached = false;
    private bool hasStarted = false;
    private Vector3 currentPosition;
    private Animator animator;
    public GameObject powerUpMagic;
    public GameObject powerUpPoints;
    public GameObject powerUpVacuum;
    public GameObject powerUpEthereal;
    // Start is called before the first frame update
	//Finds the required objects
    void Start()
    {
        player = GameObject.Find("Player");
        shamanZ = shaman.transform.position.z;
        animator = GameObject.Find("UnknownShaman").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //UnityEngine.Debug.Log(elapsedTime);
        if(GameGlobals.Instance.currentGameState.Equals("onPauseGame")) {
            paused = true;
        } else {
            if(GameGlobals.Instance.currentGameState.Equals("OnGameRunning") && paused == true) {
                Debug.Log("hi :)");
                paused = false;
                playAnimation("Run");
            }
        }

        if (player.transform.position.z > 1 && hasStarted == false) {
            hasStarted = true;
            playAnimation("Run");
        }
        if(hasStarted == true) {
            if(!paused){
                if(!Controller.resetTimer){
                    //Debug.Log("Timer is " + elapsedTime);
                    elapsedTime += Time.deltaTime;
                    float seconds = elapsedTime % 60;
                    UpdateMovement();
                } else {
                    elapsedTime = 0;
                    Controller.resetTimer = false;
                    //Debug.Log("I stumbled :)");
                }
            } else {
                //Debug.Log("Timer is " + elapsedTime);
                //StopMovement();
                playAnimation("Idle_Other");
            }
            //Vector3 playerPos = MovingObstacle.playerController.transform.position;
            //this.train.localPosition = new Vector3(0f, 0f, (currentZ - playerPos.z) * this.speed);
            //UpdateFunction();
        }
            

    }
	/*Controls the reward mechanic, the way the method works is when activated a number will be chosen between 1-4 these numbers corrspond to a powerup reward,
	 the magic numbers 5 and 6 exist for when the player already has a powerup, to avoid balancing issues and preventing some bugs from occuring, 
	 in this case they will recieve points instead.
	*/
    void gift() {
        int rand = UnityEngine.Random.Range(1, 4);
        if(PowerupController.powerUpActive){
            if(PowerupController.pointsActive){
                rand = 6;
            } else {
                rand = 5;
            }
        }
        //Debug.Log(rand);
        switch (rand) {

           case 1:
              Instantiate(powerUpEthereal, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);
                    //gift.doEtherealCollected();
                    //Debug.Log("hi1");
              break;
           case 2:
              Instantiate(powerUpPoints, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);
                    //Debug.Log("hi2");
                   // gift.doDoubleCollected();
              break;
           case 3:
              Instantiate(powerUpVacuum, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);
                    //Debug.Log("hi3");
                    // gift.doVacuumCollected();
              break;
           case 4:
              Instantiate(powerUpMagic, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z), Quaternion.identity);
                    //Debug.Log("hi4");
                    //gift.doMagicCollected();
              break;
           case 5:
                
              StartCoroutine(addCoinsOnGui(100, 0));
              break;
            case 6:

                StartCoroutine(addCoinsOnGui(200, 0));
                break;
            default:
              break;
        }
        elapsedTime = 0;
        timerReached = false;
        //Debug.Log("hi :)");
    }
    void FixedUpdate() {
        
    }
    private void UpdateMovement() {
        
        Vector3 playerPos = player.transform.position;
        Vector3 nextPos = Vector3.forward * shamanZ;
        Vector3 shaPos = shaman.transform.position;
        //Debug.Log("timer is " + elapsedTime);
        //Debug.Log("player Z is " + player.transform.position.z.ToString());
        //Debug.Log("shaman Z is " + shaman.transform.position.z.ToString());
        /*This is the same speed of the player the way it works is it works out where the person is and where the person should be with their current speed.
            this will be used when the shaman has ran ahead enough which I will say now is about 100 units
         */
        if (DistanceCheck(playerPos, shaPos, 100) && !timerReached){
            //Debug.Log("shaman speed is now the players speed");
            shamanZ += (shamanSpeed * Time.deltaTime);
            shaman.transform.position += Vector3.forward * (nextPos - shaman.transform.position).z;
        }else if(!DistanceCheck(playerPos, shaPos, 100) && !timerReached) {
            /*This is twice the players speed this will be until the shaman is far enough a head of the player*/
            //Debug.Log("shaman is speedy currently");
            shamanZ += ((shamanSpeed*3/2) * Time.deltaTime);
            shaman.transform.position += Vector3.forward * (nextPos - shaman.transform.position).z;
        }
        //half speed only happens when the player hasn't stumbled for a good period of time
        if(elapsedTime > 60 || timerReached == true){
            shamanZ += (shamanSpeed/2 * Time.deltaTime);
            shaman.transform.position += Vector3.forward * (nextPos - shaman.transform.position).z;
            timerReached = true;
            if(!DistanceCheck(playerPos, shaPos, 20)){
                gift();
            }
        }
    }

    Boolean DistanceCheck(Vector3 playerZ, Vector3 shamanZ, float addedDist) {
        if(shamanZ.z > playerZ.z + addedDist) {
            return true;
        } else {
            return false;
        }
    }
    public void Reset() {
        //Debug.Log("Reset has happened :)");
        paused = false;
        timerReached = false;
        hasStarted = false;
        elapsedTime = 0;
        Controller.resetTimer = false;
        shaman.transform.position = new Vector3(0, 0, 30);
    }
 
    private void playAnimation(string animName) {

        if (animator == null) return;

        animator.CrossFade(animName, 0.1f, 0, 0);

    }
	/*This controls the player recieving points you can set the delay of each coin being added and how many coins the player gets*/
    private IEnumerator addCoinsOnGui(int coinsTotal, float delay) {
        // Start delay
        yield return new WaitForSeconds(delay);

        float duration = 1.0f; // 1 sec.
        float time = 0;
        float counter = 0;

        while (true) {

            // Adding Coin
            float currentIncrement = ((((float)coinsTotal / duration) * time) / counter);
            if (currentIncrement > 0) {
                GameGlobals.Instance.achievements.currentCoins += Mathf.RoundToInt(currentIncrement);
            }

            // Clerking Sound
            float pitch = 1.0f + 0.3f / duration * time;
            GameGlobals.Instance.audioController.playSoundPitched("PowerupDefault", pitch);


            if (time >= duration) {
                //Debug.Log((((float)coinsTotal / duration) * time) / counter);
                break;
            }
            counter++;
            time += 0.05f;
            yield return new WaitForSeconds(0.05f);

        }

    }
}



