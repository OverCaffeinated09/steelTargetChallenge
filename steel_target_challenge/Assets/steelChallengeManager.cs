using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class steelChallengeManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;
    private int bestScore = 0;
    private int score;
    private int currentID;
    private float currTargetTime;

    public UnityEvent gameOver;

    public GameObject[] targetArray;

    public GameObject startButtonReference;

    public GameObject startEndlessButtonReference;

    private bool focusedOnTarget = false;

    private bool gameActive = false;

    private bool gamemodeIsEndless;
    
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";
        bestScoreText.text = "Best: 0";
        currentID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }

    public void startSteelChallenge() {
        gamemodeIsEndless = false;
        currentID = 0;
        score = 0;
        gameActive = true;
        startEndlessButtonReference.SetActive(false);
        for (int i = 0; i < targetArray.Length; i++) {
            targetArray[i].SetActive(true);
            targetArray[i].GetComponent<target>().reset();
        }
        targetArray[0].GetComponent<target>().isActive = true;
        focusedOnTarget = true;
        StartCoroutine(countTimeOnTarget());
    }

    public void startEndlessChallenge() {
        gamemodeIsEndless = true;
        currentID = 0;
        score = 0;
        gameActive = true;
        targetArray[0].SetActive(true);
        targetArray[0].GetComponent<target>().reset();
        targetArray[0].GetComponent<target>().isActive = true;
        focusedOnTarget = true;
        StartCoroutine(countTimeOnTarget());
    }

    public void processTargetHit(int targetID) {
        //If the correct target is hit, the score is incremented and
        //the next target in the sequence is highlighted as the one to hit
        if (gameActive) {
            if(!gamemodeIsEndless) {
                targetHitNormalChallenge(targetID);
            } else {
                targetHitEndlessChallenge(targetID);
            }
        }
        
    }

    public void targetHitNormalChallenge(int targetID) {
        if (targetID == currentID) {
            focusedOnTarget = false;
            //Debug.Log("SCORE_ADD");
            //Debug.Log("Time spent on last target: " + currTargetTime);
            targetArray[currentID].GetComponent<target>().isActive = false;
            int calculatedScore = (int) Mathf.Floor(1000 - (currTargetTime * 125));
            int scoreToAdd = calculatedScore > 0 ? calculatedScore : 0;
            score += scoreToAdd;
            //Debug.Log("Current score: " + score);
            if (currentID == targetArray.Length - 1) {
                if (score > bestScore) {
                    bestScore = score;
                    bestScoreText.text = "Best: " + bestScore;
                }
                startButtonReference.SetActive(true); 
                startEndlessButtonReference.SetActive(true);
                gameOver.Invoke();
                gameActive = false;
            } else {
                currentID++;
                targetArray[currentID].GetComponent<target>().isActive = true;
                currTargetTime = 0;
                StartCoroutine(countTimeOnTarget());
                focusedOnTarget = true;
            }
            
        } else {
            if (targetID >= 0) {
                //Hit another target out of order
                score -= 500;
            } else {
                //Missed all targets;
                score -= 150;
            }
        }
    }

    public void targetHitEndlessChallenge(int targetID) {
        if (targetID == 0) {
            focusedOnTarget = false;
            //Debug.Log("SCORE_ADD");
            //Debug.Log("Time spent on last target: " + currTargetTime);
            //targetArray[0].GetComponent<target>().isActive = false;
            int calculatedScore = (int) Mathf.Floor(1000 - (currTargetTime * 125));
            int scoreToAdd = calculatedScore > 0 ? calculatedScore : 0;
            score += scoreToAdd;
            //Debug.Log("Current score: " + score);
            //targetArray[0].GetComponent<target>().isActive = true;
            targetArray[0].GetComponent<target>().reset();
            targetArray[0].SetActive(true);
            currTargetTime = 0;
            StartCoroutine(countTimeOnTarget());
            focusedOnTarget = true;
        } else {
            //Note that only one target spawns
            //So score only decrements on misses
            score -= 150;
        }
        
    }

    IEnumerator countTimeOnTarget() {

        while(focusedOnTarget) {
            currTargetTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
