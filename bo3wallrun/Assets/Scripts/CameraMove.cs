using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{   private PlayerController playerControllerScript;
    private Rigidbody cameraRb;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button startButton;
    public Button lvl_a;
    public Button lvl_b;
    public float smoothSpeed=0.01f;
    public Vector3 velocity =Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {   cameraRb=GetComponent<Rigidbody>();
        playerControllerScript=GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    
    void LateUpdate()
    {
        
        if(!playerControllerScript.gameOver)
        {   
            Vector3 pos=new Vector3(17.63f+playerControllerScript.playerRb.transform.position.x,5.0f+playerControllerScript.playerRb.transform.position.y,playerControllerScript.playerRb.transform.position.z);
            Vector3 smoothPos=Vector3.Lerp(pos,cameraRb.transform.position,smoothSpeed);
            cameraRb.transform.position=smoothPos;
        }
        
    } 
}
