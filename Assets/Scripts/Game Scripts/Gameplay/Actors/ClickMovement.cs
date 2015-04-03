using UnityEngine;
using System.Collections;

public class ClickMovement : AdvancedBehaviour {

    public float speed = 1.5f;
    private Vector3 target;
    private Vector3 startPosition;
    private float startTime;
    private float journeyLength;
    private bool isMoving;

    void Start() {
        target = transform.position;
    }

    void Update() {
        if(Input.GetMouseButtonDown(1)) {
            target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
            target.z = transform.position.z;
            startTime = Time.time;
            startPosition = transform.position;
            journeyLength = Vector3.Distance(transform.position, target); //Switch transform.position to startPosition for constant move speed
            isMoving = true;
        }
        
        if(isMoving) {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, target, fracJourney); //Switch transform.position to startPosition for constant move speed
        }
    }
}
