using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{   private PlayerController playerControllerScript;
    private float verticalInput;
    // Start is called before the first frame update
    private float speed=60;
    void Start()
    {
        playerControllerScript=GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {   /*if(gameObject.CompareTag("obstacle"))
        {
            transform.Translate(Vector3.right*Time.deltaTime*speed);
        }
        else
        {
            transform.Translate(Vector3.forward*Time.deltaTime*speed);
        }*/
        if(playerControllerScript.startGame)
        {
            //verticalInput=Input.GetAxis("Vertical");
        if(!playerControllerScript.gameOver&&gameObject.CompareTag("Player"))
        {   if(!playerControllerScript.slideFlag)
                transform.Translate(Vector3.forward*Time.deltaTime*speed);
            else
                transform.Translate(Vector3.down*Time.deltaTime*speed);
        }
        }
    }
}