using UnityEngine;
using UnityEngine.InputSystem;

public class PCContoller_v2 : MonoBehaviour
{
    public Rigidbody character;
    public float moveSpeed = 100f;
    //private float sprintMult = 2;
    private Vector3 moveVelocity = Vector3.zero;
    private Vector3 movement;
    //Input Actions
    private PlayerActionControls inputAction;
    //Move Input from user
    private Vector2 keyboardInput;
	private void Awake()
    {
        //ctx = context
        inputAction = new PlayerActionControls();
        //When move key/s pressed change character/player's velocity
        inputAction.Player.Move.performed +=
        ctx => moveVelocity =
        ctx.ReadValue<Vector2>();
    }
    private void OnEnable()
    {
        inputAction.Enable();
    }
    private void OnDisable()
    {
        inputAction.Disable();
    }
    private void MovePlayer(Vector3 moveVelocity)
    {
        movement.Set(moveVelocity.x, 0f, moveVelocity.y);
        movement = movement * moveSpeed * Time.deltaTime;
        //character.MovePosition();//transform.position + movement);
    }
    private void GetUserInput()
    {
        inputAction.Player.Move.performed +=
        ctx => moveVelocity =
        ctx.ReadValue<Vector2>();

        float x = moveVelocity.x;
        float y = moveVelocity.y;

        var userControlsInput = new Vector3(x, y, 0);

        if(moveVelocity.magnitude != 0f)
        {
            character.AddForce(userControlsInput * Time.deltaTime);
            //moveVelocity = Vector3.userControlsInput;//.Lerp(moveVelocity, userControlsInput, Time.deltaTime);
        }
        else
        {
            character.AddForce(0, 0, 0);
        }
        MovePlayer(moveVelocity);
        //Vector3 userMoveVelocity = moveVelocity;
    }
    void Update()
    {
        GetUserInput();
    }
}
