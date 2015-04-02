using System;
using UnityEngine;
using System.Collections;

    public class Player : Character {

        void Start () {
        }

        void Update() {
            float translation = Input.GetAxis("Vertical") * speed;
            float rotation = Input.GetAxis("Horizontal") * speed;

            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            transform.Translate(0, 0, translation);
            transform.Translate(rotation, 0, 0);
        }
    }
