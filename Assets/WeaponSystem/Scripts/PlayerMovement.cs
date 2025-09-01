using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private CharacterController controller;
    Vector3 velocity;

    // Start is called before the first frame update

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyBoardMovement();
    }

    private void KeyBoardMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

        controller.Move(move * movementSpeed * Time.deltaTime);

        velocity.y += -9.81f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
