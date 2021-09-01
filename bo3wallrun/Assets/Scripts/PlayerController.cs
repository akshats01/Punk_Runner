using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{   public Rigidbody playerRb;
    private float jumpForce=43;
    private float wallJumpForce=30;
    private float horizontalInput;
    private float gravityModifier=8;
    private float wallGravity=20;
    public bool isOnGround=true;
    private float sideSpeed=20;
    public bool isOnLWall=false;
    public bool isOnRWall=false;
    public bool flag=false;
    public bool impulseFlag=false;
    private Animator playerAnim;
    public bool gameOver=false;
    public bool slideFlag=false;
    public bool startGame=false;
    // Start is called before the first frame update
    void Start()//to remove bug where jumping on edge of wall removes gravity, restrict the movement on the wall by taking the centre position of the wall in collision 
    {           //and calculating how much distance from the centre (to each sides) you want to limit for the player.
        Debug.Log("Start");
        gravityModifier=8;
        playerRb=GetComponent<Rigidbody>();
        playerAnim=GetComponent<Animator>();
        Physics.gravity*=gravityModifier;
        Debug.Log(Physics.gravity);
    }

    // Update is called once per frame
    void Update()
    {   //Debug.Log("Update");
        if(Input.GetKeyDown(KeyCode.Space)&&!slideFlag)
        {
        if(isOnGround){
            playerRb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
            isOnGround=false;
            playerAnim.SetTrigger("Jump_trig");
        }
        else if(isOnLWall&&!flag){
            transform.Rotate(0, 0,90);
            playerRb.useGravity = true;
            flag=true;
            impulseFlag=true;
            playerAnim.SetTrigger("Jump_trig");
            //playerRb.AddForce(Vector3.right*jumpForce,ForceMode.Impulse);
            //Physics.gravity*=wallGravityModifier;  
        }
        else if(isOnRWall&&!flag){
            transform.Rotate(0, 0,-90);
            playerRb.useGravity = true;  
            flag=true;    
            impulseFlag=true;      
            playerRb.AddForce(Vector3.down*wallGravity*Time.deltaTime);
            playerAnim.SetTrigger("Jump_trig");   
        }
        }
        else if(Input.GetKeyDown("s")&&(isOnGround||((isOnLWall||isOnRWall)&&!flag)))
        {   transform.Rotate(-90,0,0);
            StartCoroutine(slideCountdown());
            slideFlag=true;
        }
        horizontalInput=Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right*sideSpeed*horizontalInput*Time.deltaTime);
        if(flag)
        {
            if(isOnLWall)
                transform.Translate(Vector3.right*sideSpeed*Time.deltaTime);
            else if(isOnRWall)
                transform.Translate(Vector3.left*sideSpeed*Time.deltaTime);
        }
        if(impulseFlag){
            playerRb.AddForce(Vector3.up*wallJumpForce,ForceMode.Impulse);
            isOnGround=false;
            impulseFlag=false;
        }
        if(isOnLWall)
        {   if(!flag)
                playerRb.AddForce(Vector3.back*wallGravity);
        }
        if(isOnRWall)
        {   if(!flag)
                playerRb.AddForce(Vector3.forward*wallGravity);
        }
        if(playerRb.transform.position.y<-5||playerRb.transform.position.z<-40||playerRb.transform.position.z>40)
        {
            gameOverFn();
            Debug.Log("OutOfBounds");
        }
    }
    IEnumerator slideCountdown()
    {   playerAnim.SetTrigger("Jump_trig");
        yield return new WaitForSeconds(0.5f);
        transform.Rotate(90,0,0);
        slideFlag=false;
    }
    private void OnCollisionEnter(Collision collision)
    {   //Debug.Log("Collision");
        if(collision.gameObject.CompareTag("Ground") )
        {
            isOnGround=true;
            isOnLWall=false;
            isOnRWall=false;
            flag=false;

        }
        else if(collision.gameObject.CompareTag("LWall")&&!isOnGround&&!isOnLWall){
            playerRb.useGravity = false;
            //GetComponent<Rigidbody>().Constraints.FreezePosition.y = true;
            if(isOnLWall==false)
                transform.Rotate(0, 0,-90);
            if(isOnRWall==true)
            {
                 
                isOnRWall=false;
            }    
            isOnLWall=true;
            flag=false;
        }
        else if(collision.gameObject.CompareTag("RWall")&&!isOnGround&&!isOnRWall){
            playerRb.useGravity = false;
            //GetComponent<Rigidbody>().Constraints.FreezePosition.y = true;
            if(isOnRWall==false)
                transform.Rotate(0, 0,90);
            if(isOnLWall==true)
            {
                 
                isOnLWall=false;
            }
            isOnRWall=true;
            flag=false;
        }
        else if(collision.gameObject.CompareTag("obstacle"))
        {
            gameOverFn();
            Debug.Log("Obstacle");
            //playerAnim.SetBool("Death_b",true);
            //playerAnim.SetInteger("DeathType_int",1);
        }
        else if(collision.gameObject.CompareTag("Portal"))
        {   ChangeScene_2("Level2");

        }  
        else if(collision.gameObject.CompareTag("Portal2"))
        {   ChangeScene_3("Level3");

        }        
    }
    public void gameOverFn()
    {   //Debug.Log("gameOver");
        gameOver=true;
        Debug.Log("Game Over!");
        Physics.gravity/=gravityModifier;
        gravityModifier=1;
    }
    public void ChangeScene_1(string sceneName)
    {   //Debug.Log("changescene1");
        SceneManager.LoadScene("Level1");
        Physics.gravity/=gravityModifier;
    }
    public void ChangeScene_2(string sceneName)
    {   //Debug.Log("changescene2");
        SceneManager.LoadScene("Level2");
        Physics.gravity/=gravityModifier;
    }
    public void ChangeScene_3(string sceneName)
    {   //Debug.Log("changescene2");
        SceneManager.LoadScene("Level3");
        Physics.gravity/=gravityModifier;
    }
}