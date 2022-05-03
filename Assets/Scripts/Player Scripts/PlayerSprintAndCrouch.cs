using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{

    private PlayerMovement playerMovement;

    public float sprint_speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;
    public float prone_Speed = 1f;

    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height = 0.8f;
    private float prone_Height = 0.4f;

    private bool is_Crouching;
    private bool is_Prone;

    private PlayerFootsteps player_Footsteps;

    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.5f;
    private float prone_Volume = 0.1f;
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;

    private float walk_Step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.25f;
    private float crouch_Step_Distance = 0.5f;
    private float prone_Step_Distance = 1f;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        look_Root = transform.GetChild(0);

        player_Footsteps = GetComponentInChildren<PlayerFootsteps>();

    }

    void Start(){
        player_Footsteps.volume_Min = walk_Volume_Min;
        player_Footsteps.volume_Max = walk_Volume_Max;
        player_Footsteps.step_Distance = walk_Step_Distance;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
        Prone();
    }

    void Sprint(){
        if (Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching && !is_Prone){
            playerMovement.speed = sprint_speed;

            player_Footsteps.step_Distance = sprint_Step_Distance;
            player_Footsteps.volume_Min = sprint_Volume;
            player_Footsteps.volume_Max = sprint_Volume;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching && !is_Prone){
            playerMovement.speed = move_Speed;

            player_Footsteps.step_Distance = walk_Step_Distance;
            player_Footsteps.volume_Min = walk_Volume_Min;
            player_Footsteps.volume_Max = walk_Volume_Max;
        }
    }

    void Crouch(){
        if (Input.GetKeyDown(KeyCode.C)){
            if (is_Crouching){
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
                playerMovement.speed = move_Speed;

                
                player_Footsteps.step_Distance = walk_Step_Distance;
                player_Footsteps.volume_Min = walk_Volume_Min;
                player_Footsteps.volume_Max = walk_Volume_Max;

                is_Crouching = false;
            } else{
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                playerMovement.speed = crouch_Speed;

                player_Footsteps.step_Distance = crouch_Step_Distance;
                player_Footsteps.volume_Min = crouch_Volume;
                player_Footsteps.volume_Max = crouch_Volume;

                is_Crouching = true;
            }
        }
    }

    void Prone(){
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            if (is_Prone){
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
                playerMovement.speed = move_Speed;

                player_Footsteps.step_Distance = walk_Step_Distance;
                player_Footsteps.volume_Min = walk_Volume_Min;
                player_Footsteps.volume_Max = walk_Volume_Max;

                is_Prone = false;
            } else{
                look_Root.localPosition = new Vector3(0f, prone_Height, 0f);
                playerMovement.speed = prone_Speed;

                player_Footsteps.step_Distance = prone_Step_Distance;
                player_Footsteps.volume_Min = prone_Volume;
                player_Footsteps.volume_Max = prone_Volume;

                is_Prone = true;
            }
        }
    }
}
