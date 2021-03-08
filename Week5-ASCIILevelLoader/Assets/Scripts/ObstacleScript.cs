using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
  // declare speed and a boolean for movement
  public float moveSpeed;
  bool moveRight = true;

  private Vector3 newPosition;

  // set coordinates for the left and right movement of the obstacle
  void Update()
  {

    if (transform.position.x > 4f)
    {
      moveRight = false;
    }

    if (transform.position.x < -4f)
    {
      moveRight = true;
    }
    
   
    if (moveRight)
      transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y); //move right if moving right
    else
      transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y); //move left left if not moving right
  }
  
  private void OnCollisionEnter2D(Collision2D other) 
  {
    if (other.gameObject.name.Contains("Player")) //if the player hits the obstacle 
    {
      GameManager.instance.GetComponent<AsciiLevelLoader>().ResetPlayer(); //call the ResetPlayer function in AsciiLevelLoader script
    }
  }
}
