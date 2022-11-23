using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalLine : MonoBehaviour
{
    public float speed = 5f;
    public Vector2 target = new Vector2(1f, 1f);

    [SerializeField] private float xRange = 5f;
    [SerializeField] private float yRange = 5f;
    private bool atTarget = false;
    private Animator animator;
    private float vertExtent;
    private float horzExtent;

    void Awake() {
        animator = GetComponent<Animator>();  
    }

    void Start() {

        vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;    
        horzExtent = vertExtent * Screen.width / Screen.height;

        xRange = horzExtent;

        target = new Vector3(horzExtent, transform.position.y, transform.position.z);
    }

    void Update() {
        if (!atTarget) {
            animator.SetBool("Walking", true);
            // Move our position a step closer to the target.
            float step =  speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.x, target.y, transform.position.z), step);
        }
        
        // Check if the position of the cube and sphere are approximately equal.
        if (Vector2.Distance(transform.position, target) < 0.001f) {
            // Swap the position of the cylinder.
            //target *= -1.0f;
            //atTarget = true;
            //animator.SetBool("Walking", false);
            target = new Vector3(-target.x, transform.position.y, transform.position.z);
        }
    }

}
