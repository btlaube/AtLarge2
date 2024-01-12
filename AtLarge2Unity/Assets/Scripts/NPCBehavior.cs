using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour {

    public Transform furthestNPC;
    public float speed = 5f;
    public Vector2 target;

    [SerializeField] private float xRange = 5f;
    [SerializeField] private float yRange = 5f;
    private bool atTarget = false;
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();  
    }

    void Start() {

        float vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;    
        float horzExtent = vertExtent * Screen.width / Screen.height;

        xRange = horzExtent;
        yRange = vertExtent;

        float randX;
        float randY;
        randX = Random.Range(-xRange, xRange);
        randY = Random.Range(-yRange, yRange);
        target = new Vector3(randX, randY, transform.position.z);
    }

    void Update() {
        if (!atTarget) {
            animator.SetBool("Walking", true);
            // Move our position a step closer to the target.
            speed = (Vector2.Distance(transform.position, target))/(Vector2.Distance(furthestNPC.position, furthestNPC.GetComponent<NPCBehavior>().target)) * 5f;
            float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, target.y, transform.position.z), step);
        }
        

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector2.Distance(transform.position, target) < 1f) {
            // Swap the position of the cylinder.
            //target *= -1.0f;
            atTarget = true;
            animator.SetBool("Walking", false);
        }
    }

}
