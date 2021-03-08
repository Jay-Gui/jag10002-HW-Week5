using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AsciiLevelLoader : MonoBehaviour
{
    // Remember: ascii are codes for letters; takes the form of characters on the keyboard 
    
    
    public float xOffset; //move along the x in inspector
    public float yOffset; //move along the y in inspector 
    
    public GameObject player;
    public GameObject wall;
    public GameObject obstacleA, obstacleB;
    public GameObject prize;

    //variable for the current player
    public GameObject currentPlayer;

    //variable for the start position of the player
    private Vector2 startPos; 

    //name of the level file
    public string fileName;

    //current level variable
    public int currentLevel = 0;

    //property that wraps up currentLevel
    //when the level changes, we load that specific level 
    public int CurrentLevel
    {
        get { return currentLevel; }
        set
        {
            currentLevel = value;
            LoadLevel();
        }
    }

    //empty game object that holds the level
    public GameObject level; 
    
    void Start()
    {
        LoadLevel(); //call the LoadLevel function in start
    }

    // function that creates a level based on the ASCII Text file 
    void LoadLevel()
    {
        Destroy(level); //destroy the current level
        level = new GameObject("Level"); //create a new level gameObject
        
        //build up a new level path based on the current level file
        string current_file_path = //build the path to the level file
            Application.dataPath +
            "/Levels/" +
            fileName.Replace("Num", currentLevel + "");
           

        //instead of reading a chunk of strings, we read each individual element of an array
        string[] fileLines = File.ReadAllLines(current_file_path);

        //loops through each line
        for (int y = 0;
            y < fileLines.Length;
            y++) // chose y because reading down lines is like reading down the y axis of a page
        {
            //go to each line in a row
            string lineText = fileLines[y];

            //convert lines into an array of characters
            char[] characters = lineText.ToCharArray(); //create characters array

            //loop through each character 
            for (int x = 0; x < characters.Length; x++) //length of the characters in the file
            {
                //grabs the current character
                char c = characters[x];

                GameObject newObj; //creates a holder for all those characters

                //we use a switch function to switch between each character on the fly and instantiate game objects
                //(we use "c" for "character" in this case
                switch (c)
                {
                    case 'p':
                        newObj = Instantiate(player);
                        if (currentPlayer == null) //if we don't have a current player
                            currentPlayer = newObj; //then make this the current player
                        startPos = new Vector2(x + xOffset, -y + yOffset); //save this position to use for resetting the player
                        break;
                    case 'w':
                        newObj = Instantiate(wall);
                        break;
                    case '*':
                        newObj = Instantiate(obstacleA);
                        break;
                    case '#':
                        newObj = Instantiate(obstacleB);
                        break; 
                    case '&':
                        newObj = Instantiate(prize);
                        break;
                    default:
                        newObj = null;
                        break;
                }

                //did we create a new object?
                if (newObj != null)
                {
                    if (!newObj.name.Contains("Player")) //if new object is not the player
                    {
                        newObj.transform.parent = level.transform; //parent it to the level
                    }
                    //if so, add the offsets that we insert into inspector to the x and y vectors to the position of the new object
                    //-y makes it so that what we put in the txt correlates with how things appear on the game screen
                    newObj.transform.position = new Vector3(x + xOffset, -y + yOffset, 0);
                }
            }
        }
    }

    //this resets the player position 
    public void ResetPlayer()
    {
        //player returns to the starting position
        currentPlayer.transform.position = startPos;
    }
}
    