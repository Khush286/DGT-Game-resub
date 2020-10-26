﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****************************** Project Header ******************************\
Script Name:  ProjectileDown
Project:      DGT-Game Dungeon Runner
Author:       Khushwant Singh

Intended for a projectile moving downwards.

\***************************************************************************/

public class ProjectileDown : MonoBehaviour
{
    void Update()
    {
        Invoke("DestroyProjectile", 2.0f); // Move projectile
        transform.Translate(Vector3.down * Time.deltaTime * 5);
    }
    void DestroyProjectile() // If called be outside scripts, destroy the projectile
    {
        Destroy(gameObject);
    }
}
