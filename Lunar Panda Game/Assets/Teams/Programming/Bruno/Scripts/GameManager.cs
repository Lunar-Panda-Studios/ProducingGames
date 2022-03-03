using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//SINGLETON
//Manager of Managers
//Scene Loaders
//Tracking Player:
//Current Level
//Others variables that could be useful

public enum GameState //Only Basic states
{
    MENU, GAME, PAUSE, QUIT
}

public class GameManager : MonoBehaviour
{
    //Need help with creating a Singleton
    public static GameManager Instance;

    public GameState gameStates;
    public int whichLevel = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void LoadCurrentScene(GameState state) //Scene Loader. Not doing anything as of right now as we don't have any scenes to load.
    {
        gameStates = state;
        switch (gameStates)
        {
            case GameState.MENU:
                break;
            case GameState.GAME:
                {
                    switch (whichLevel)
                    {
                        case 0:
                            {
                                SceneManager.LoadScene("Train");
                                break;
                            }
                        case 1:
                            {
                                SceneManager.LoadScene("Hospital");
                                break;
                            }                        
                        default:
                            {
                                SceneManager.LoadScene("Train");
                                break;
                            }
                    }
                    break;
                }
            case GameState.PAUSE:
                break;
            case GameState.QUIT:
                Application.Quit();
                break;
            default:
                break;
        }
    }



    public void currentLevel(int currLevel)
    {
        //Just returning the value of which game scene the player is in
        currLevel = whichLevel;
        return;
    }
}
