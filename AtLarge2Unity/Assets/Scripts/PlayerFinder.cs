using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    public Transform player;
    public Transform NPC1;

    void Update() {
        float percentXUncovered = Mathf.Abs(player.position.x - NPC1.position.x)/0.454295f;
        float percentYUncovered = Mathf.Abs(player.position.y - NPC1.position.y)/0.559612f;
        float totalCovered = (1-percentXUncovered) * (1-percentYUncovered);
        Debug.Log($"x uncovered: {percentXUncovered} y uncovered: {percentYUncovered} total covered: {totalCovered}");
    }

}
