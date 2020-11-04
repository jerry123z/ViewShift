using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnim : MonoBehaviour
{
    private Animator anim;
    private GameObject player;
    private float playerSpd;
    private Vector3 playerPos;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        playerSpd = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMover playerScr = player.GetComponent<PlayerMover>();
        // Grounded
        if(playerScr._isGrounded) {
            anim.SetBool("IsGrounded", true);
        } else {
            anim.SetBool("IsGrounded", false);
        }

        // Moving
        playerSpd = playerScr._inputs.magnitude;
        //playerSpd = (new Vector3(player.transform.position.x - playerPos.x, 0, player.transform.position.z - playerPos.z)).magnitude/Time.deltaTime;
        anim.SetFloat("groundSpeed", playerSpd);
        playerPos = new Vector3(player.transform.position.x, 0, player.transform.position.z);
    }
}
