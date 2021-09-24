using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerController playerControllerScript;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button startButton;
    public Button lvl_a;
    public Button lvl_b;
    public float smoothSpeed=0.01f;
    public Vector3 velocity =Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {   playerControllerScript=GameObject.Find("Player").GetComponent<PlayerController>();        
    }

    // Update is called once per frame
    void Update()
    {   if(playerControllerScript.gameOver)
        {   gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
        if(playerControllerScript.lvl1==true)
        {   playerControllerScript.lvl1=false;
            ChangeScene_1("Level1");
        }
        else if(playerControllerScript.lvl2==true)
        {   playerControllerScript.lvl2=false;
            ChangeScene_2("Level2");
        }
        else if(playerControllerScript.lvl3==true)
        {   playerControllerScript.lvl3=false;
            ChangeScene_3("Level3");
        }
    }
    public void RestartGame()
    {   playerControllerScript.gameOverFn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void startFn()
    {   startButton.gameObject.SetActive(false);
        playerControllerScript.startGame=true;
        lvl_a.gameObject.SetActive(false);
        lvl_b.gameObject.SetActive(false);
    }
    public void ChangeScene_1(string sceneName)
    {   //Debug.Log("changescene1");
        SceneManager.LoadScene("Level1");
        playerControllerScript.gravityHandler();
    }
    public void ChangeScene_2(string sceneName)
    {   //Debug.Log("changescene2");
        SceneManager.LoadScene("Level2");
        playerControllerScript.gravityHandler();
    }
    public void ChangeScene_3(string sceneName)
    {   //Debug.Log("changescene2");
        SceneManager.LoadScene("Level3");
        playerControllerScript.gravityHandler();
    }
}
