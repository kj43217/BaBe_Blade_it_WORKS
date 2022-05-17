using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerBehaviour : NetworkBehaviour
{
    //Una vez comprobado que este es el movimiento necesario, eliminar el codigo que esta en comentarios no este 

    //Manages the velocity of the player per coordinate
    private Vector3 playerVelocity = Vector3.zero;
    //Value to easily change the player's speed
    public float speed = 10;

    void Update()
    {
        if (isLocalPlayer)
        { 
        Vector3 direction = new Vector3(0, 0, 0);

        //Player movement controller
        if (Input.GetKey("right"))
        {
            direction = new Vector3(1, 0, 0);
        }
        if (Input.GetKey("left"))
        {
            direction = new Vector3(-1, 0, 0);
        }
        if (Input.GetKey("up"))
        {
            direction = new Vector3(0, 0, 1);
        }
        if (Input.GetKey("down"))
        {
            direction = new Vector3(0, 0, -1);
        }
        if (Input.GetKey("up") && Input.GetKey("right"))
        {
            direction = new Vector3(1, 0, 1);
        }
        if (Input.GetKey("up") && Input.GetKey("left"))
        {
            direction = new Vector3(-1, 0, 1);
        }
        if (Input.GetKey("down") && Input.GetKey("right"))
        {
            direction = new Vector3(1, 0, -1);
        }
        if (Input.GetKey("down") && Input.GetKey("left"))
        {
            direction = new Vector3(-1, 0, -1);
        }
        SmoothMovement(direction);
    }
}

    //This function manages the player's movement
    private void SmoothMovement(Vector3 direction)
    {
        //This variable sets the speed to the desired direction
        Vector3 desiredVelocity = direction.normalized * speed;
        //This variable pushes the player to the desired direction
        Vector3 steeringForce = desiredVelocity - playerVelocity;
        playerVelocity += steeringForce * Time.deltaTime;
        transform.position += playerVelocity * Time.deltaTime;
    }

    // Una vez comprobado, este codigo puede ser eliminado 

    /*
    //Manages the velocity of the player per coordinate
    private Vector3 playerVelocity = Vector3.zero;
    //Value to easily change the player's speed
    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private Vector2 initialPositionRange = new Vector2(-3, 3);

    [SerializeField]
    private NetworkVariable<Vector3> networkPositionDirection = new NetworkVariable<Vector3>();

    [SerializeField]
    private NetworkVariable<PlayerState> networkPlayerState = new NetworkVariable<PlayerState>();

    private Vector3 oldInputPosition;
    private CharacterController controller;
    private Animator animator;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        if(IsOwner && IsClient)
        {
            transform.position = new Vector3(Random.Range(initialPositionRange.x, initialPositionRange.y), 0.5f, Random.Range(initialPositionRange.x, initialPositionRange.y));
        }
    }

    void Update()
    {
        if (IsOwner && IsClient)
        {
            ClientInput();

            ClientMove();
            ClientVisuals();

        }
    }

    private void ClientInput()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);

        float zInput = Input.GetAxis("Vertical");

        Vector3 inputPosition = direction * zInput;

        //Player movement controller
        if (Input.GetKey("right"))
        {
            direction = new Vector3(1, 0, 0);
        }
        if (Input.GetKey("left"))
        {
            direction = new Vector3(-1, 0, 0);
        }
        if (Input.GetKey("up"))
        {
            direction = new Vector3(0, 0, 1);
        }
        if (Input.GetKey("down"))
        {
            direction = new Vector3(0, 0, -1);
        }
        if (Input.GetKey("up") && Input.GetKey("right"))
        {
            direction = new Vector3(1, 0, 1);
        }
        if (Input.GetKey("up") && Input.GetKey("left"))
        {
            direction = new Vector3(-1, 0, 1);
        }
        if (Input.GetKey("down") && Input.GetKey("right"))
        {
            direction = new Vector3(1, 0, -1);
        }
        if (Input.GetKey("down") && Input.GetKey("left"))
        {
            direction = new Vector3(-1, 0, -1);
        }
        SmoothMovement(direction);

        if(oldInputPosition != inputPosition)
        {
            oldInputPosition = inputPosition;

            UpdateClientPositionServerRpc(inputPosition * speed);
        }

        if(zInput )
        {
            UpdatePlayerStateServerRpc(PlayerState.Idle)
        }
    }   //This function manages the player's movement
    
    private void ClientMove()
    {
        if(networkPositionDirection.Value != Vector3.zero)
        {
            controller.Move(networkPositionDirection.Value);
        }
    }

    private void ClientVisuals()
    {

    }

    [ServerRpc]
    public void UpdateClientPositionServerRpc(Vector3 newPositionDirection)
    {
        networkPositionDirection.Value = newPositionDirection;
    }

    [ServerRpc]
    public void UpdatePlayerStateServerRpc(PlayerState newState)
    {
        networkPlayerState.Value = newState;
    }

    private void SmoothMovement(Vector3 direction)
    {
        //This variable sets the speed to the desired direction
        Vector3 desiredVelocity = direction.normalized * speed;
        //This variable pushes the player to the desired direction
        Vector3 steeringForce = desiredVelocity - playerVelocity;
        playerVelocity += steeringForce * Time.deltaTime;
        transform.position += playerVelocity * Time.deltaTime;
    }
    */
}
