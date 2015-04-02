﻿using System;
using UnityEngine;
using System.Collections;

    public class Player : Character {

        void Start () {
        }

        void Update() {

            float currentSpeed = GetCurrentSpeed();

            float translation = Input.GetAxis("Vertical") * currentSpeed;
            float rotation = Input.GetAxis("Horizontal") * currentSpeed;

            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            transform.Translate(0, translation, 0);
            transform.Translate(rotation, 0, 0);
        }

        /// <summary>
        /// If a player is running at both angles, don't speed the character
        /// up by double the speed. Instead, we allow them to go at 1.5 times the pace.
        /// 
        /// If only one is down, return using the regular speed calculation.
        /// </summary>
        /// <returns>float speed to move by</returns>
        float GetCurrentSpeed() {
            return Input.GetAxis("Vertical") != 0 && Input.GetAxis("Horizontal") != 0 ? speed / 1.5f : speed;
        }
    }
