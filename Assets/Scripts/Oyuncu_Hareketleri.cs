using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oyuncu_Hareketleri : MonoBehaviour
{
    public  CharacterController kontrolcu;
    private Vector3 vektor;
    public float ileriHiz;
    private int lanes = 1;
    public float laneDistance = 3;
    public float jumpforce;
    public float gravity = -20;
    public float maxSpeed;

    public Animator animator;

    void Start()
    {
        kontrolcu = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
       
        if (!Player_Manager.isGameStarted)
        {
            return;
        }
        if (ileriHiz < maxSpeed)
            ileriHiz += 0.3f * Time.deltaTime;
        animator.SetBool("isGameStarted", true);
        animator.SetBool("isYerde", kontrolcu.isGrounded);
        vektor.z = ileriHiz;
        vektor.y += gravity * Time.deltaTime;
        if(kontrolcu.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                jump();
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            lanes++;
            if(lanes==3)
            { lanes = 2; }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            lanes--;
            if (lanes == -1)
            { lanes = 0; }
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if(lanes==0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        if (lanes == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        //Vector3.Lerp(transform.position,targetPosition,80*Time.deltaTime);
        transform.position = targetPosition;
        kontrolcu.center = kontrolcu.center;
    }
    private void FixedUpdate()
    {
        if (!Player_Manager.isGameStarted)
        {
            return;
        }
        kontrolcu.Move(vektor * Time.fixedDeltaTime);

    }
    private void jump()
    {
        vektor.y = jumpforce;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "obstacle")
        {
            Player_Manager.gameOver = true;
        }
        if(hit.transform.tag=="duman")
        {
           
            Debug.Log("dumana carptý");
        }
    }
}
