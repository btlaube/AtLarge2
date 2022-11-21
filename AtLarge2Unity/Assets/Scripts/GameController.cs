using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GameController : MonoBehaviour {

    public Sprite[] sprites;
    public GameObject NPC;
    public Transform player;
    public GameEvent onWin;
    public GameEvent onLose;

    [SerializeField] private float waveSize = 10f;
    [SerializeField] private float xRange = 5f;
    [SerializeField] private float yRange = 5f;
    [SerializeField] private float buffer = 10f;
    [SerializeField] private float percentCoveredThreshold = 0.5f;
    private float timer;
    private int score = 0;
    private int highscore = 0;
    private TMP_Text scoreText;
    private TMP_Text highscoreText;
    private AudioManager audioManager;
    

    void Start() {
        audioManager = AudioManager.instance;

        scoreText = GameObject.Find("Score").GetComponent<Transform>().GetChild(2).GetComponent<TMP_Text>();
        highscoreText = GameObject.Find("Highscore").GetComponent<Transform>().GetChild(2).GetComponent<TMP_Text>();
        scoreText.text = score.ToString();
        highscoreText.text = highscore.ToString();

        float vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;    
        float horzExtent = vertExtent * Screen.width / Screen.height;

        xRange = horzExtent + buffer;
        yRange = vertExtent + buffer;
        StartRound();
    }

    public void StartRound() {
        SpawnEnemies();
        SetFurthestNPC();
    }

    public void EndRound() {
        float percentCovered = FindPlayer();
        if (percentCovered >= percentCoveredThreshold) {
            audioManager.Play("Point");
            score++;
            scoreText.text = score.ToString();
            if(score > highscore) {
                highscore = score;
                highscoreText.text = highscore.ToString();
            }
                onWin.TriggerEvent();
        }
        else {
            audioManager.Play("GameOver");
            score = 0;
            scoreText.text = score.ToString();
            onLose.TriggerEvent();
        }
    }

    public void SpawnEnemies() {
        foreach (Transform NPC in transform) {
            Destroy(NPC.gameObject);
        }

        for (int i = 0; i <= waveSize; i++) {
            int whichSprite = Random.Range(0, sprites.Length);

            float randX;
            float randY;
            bool extremeAxis = Random.value > 0.5;
            bool whichExtreme = Random.value > 0.5;
            if(extremeAxis) {
                if(whichExtreme) {
                    randX = xRange;
                }
                else {
                    randX = -xRange;
                }
                randY = Random.Range(-yRange, yRange);
            }
            else {
                if(whichExtreme) {
                    randY = yRange;
                }
                else {
                    randY = -yRange;
                }
                randX = Random.Range(-xRange, xRange);
            }

            Vector3 spawnPosition = new Vector3(randX, randY, transform.position.z);

            //Chooses z position
            bool zPosition = Random.value > 0.3;
            if (zPosition) {
                spawnPosition.z = -1f;
            }
            else {
                spawnPosition.z = 1f;
            }

            GameObject newEnemy = Instantiate(NPC, spawnPosition, Quaternion.identity, transform);
            newEnemy.GetComponent<SpriteRenderer>().sprite = sprites[whichSprite];
        }
    }

    private void SetFurthestNPC() {
        float maxDist = 0f;
        Transform furthestNPC = transform.GetChild(0);
        foreach (Transform NPC in transform) {
            float distance = Vector2.Distance(NPC.position, NPC.GetComponent<NPCBehavior>().target);
            if (distance > maxDist) {
                furthestNPC = NPC;
                maxDist = distance;
            }
        }

        foreach (Transform NPC in transform) {
            NPC.GetComponent<NPCBehavior>().furthestNPC = furthestNPC;
        }
    }

    private float FindPlayer() {
        float percentXUncovered = 1f;
        float percentYUncovered = 1f;
        float totalCovered = 0f;
        foreach (Transform NPC in transform) {
            if (NPC.position.z == -1f) {
                percentXUncovered = Mathf.Clamp(Mathf.Abs(player.position.x - NPC.position.x)/0.6244822f, 0, 1);
                percentYUncovered = Mathf.Clamp(Mathf.Abs(player.position.y - NPC.position.y)/0.7714447f, 0, 1);
                totalCovered += (1-percentXUncovered) * (1-percentYUncovered);
            }
        }
        return totalCovered;
    }

}
