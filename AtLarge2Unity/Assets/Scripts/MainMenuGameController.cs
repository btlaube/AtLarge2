using UnityEngine;
using TMPro;

public class MainMenuGameController : MonoBehaviour
{

    public Sprite[] sprites;
    public GameObject NPC;

    [SerializeField] private float waveSize = 10f;
    [SerializeField] private float xRange = 5f;
    [SerializeField] private float yRange = 5f;
    [SerializeField] private float waveRate = 5f;
    [SerializeField] private float buffer = 10f;
    private float timer;

    void Start() {

        timer = Time.deltaTime;

        float vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;    
        float horzExtent = vertExtent * Screen.width / Screen.height;

        xRange = horzExtent + buffer;
        yRange = vertExtent + buffer;
        StartRound();
    }

    void Update() {
        timer += Time.deltaTime;
        
        if (timer >= waveRate) {
            timer = Time.deltaTime;
            StartRound();
        }
    }

    public void StartRound() {
        SpawnEnemies();
        SetFurthestNPC();
    }

    public void SpawnEnemies() {
        for (int i = 0; i <= waveSize; i++) {
            //Pick which enemy to spawn with varying probability
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
            bool zPosition = Random.value > 0.5;
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
}
