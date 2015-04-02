using System;
using UnityEngine;
using System.Collections;

    public class Player : Character {

        void Start () {
            StartCoroutine(Move());
        }

        IEnumerator Move() {
            if(Input.GetAxis("Vertical") > 0) {
                Debug.Log("Moving");
            }
            yield return null;
        }
    }
