using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f; // Jump force value

    private Rigidbody rb;
    private Camera playerCamera;

    private float verticalRotation = 0f;

    public Node closestNode;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Handle player movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        rb.velocity = new Vector3(movementDirection.x * movementSpeed, rb.velocity.y, movementDirection.z * movementSpeed);

        // Handle player rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // Handle player jump
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jumping!");
        }

              FindClosestNode();
        // Optional: Connect to the closest node based on some condition, e.g., pressing a key
        if (Input.GetKeyDown(KeyCode.C)) // For example, press C to connect
        {
            ConnectToClosestNode();
        }
        
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }

    void FindClosestNode()
    {
        float closestDistance = Mathf.Infinity;
        Node[] allNodes = FindObjectsOfType<Node>();

        foreach (Node node in allNodes)
        {
            float distance = Vector3.Distance(transform.position, node.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestNode = node;
            }
        }
    }

    void ConnectToClosestNode()
    {
        if (closestNode != null)
        {
            // Assuming the player itself does not store connections but can trigger connections between nodes
            closestNode.ConnectNode(closestNode); // This line is a bit redundant without context. Typically, you'd have the player either become a node or directly manipulate nodes' connections.
        }
    }
}