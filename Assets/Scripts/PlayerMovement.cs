using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/****************************** Project Header ******************************\
Script Name:  PlayerMovement
Project:      DGT-Game Dungeon Runner
Author:       Khushwant Singh

Handles the movement of the Player.

\***************************************************************************/

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Players movement speed
    public Rigidbody2D rb; // Players rigidbody
    public Animator animator;
    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); // Detect any horizontal movement
        movement.y = Input.GetAxisRaw("Vertical"); // Detect any vertical movement
        movement = movement.normalized;

        animator.SetFloat("Horizontal", movement.x); // Change animator variables depending on horizontal movement
        animator.SetFloat("Vertical", movement.y); // Change animator variables depending on vertical movement
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); // Change players movement speed
    }
}
