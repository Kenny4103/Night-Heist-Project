using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// This is the script for Player movement in the Hubworld
// It's separate from the PlayerManager script
public class ThiefMovement : MonoBehaviour
{
    /*** References ***/
    private Rigidbody2D _rb;
    private Animator _animator;
    private InputManager _input;

    public Text messageText; // Drag your UI Text here in the Inspector
    private float messageDuration = 2f; // Duration to show the message
    private Vector3 originalMessagePosition; // To store original position of the text

    private GameObject currentCollectable = null; // Store the current collectable object
    private int totalCollectables; // Total collectables in the level
    private int collectedJewels = 0; // Number of collected jewels

    /*** Class Variables ***/
    [SerializeField] private float _moveSpeed = 5f;
    private Vector2 _movement;

    // String Constants
    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";
    private const string _lasthorizontal = "LastHorizontal";
    private const string _lastvertical = "LastVertical";

    /*** Methods ***/
    private void Start()
    {
        // Count all objects tagged as "Collectable" in the scene at the start of the level
        totalCollectables = GameObject.FindGameObjectsWithTag("Collectable").Length;

        // Store the original position of the message text
        originalMessagePosition = messageText.rectTransform.position;

        // Show the starting message when the level starts
        ShowMessage("WASD TO MOVE. COLLECT ALL JEWELS AND DON'T GET CAUGHT!!!");

        // Hide the message text at the start (after showing the initial message)
    }


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputManager>();
    }

    private void Update()
    {
        // Sets the thief's velocity according to movement value
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        _rb.velocity = _movement * _moveSpeed;

        if (Input.GetKeyDown(KeyCode.Return) && currentCollectable != null)
        {
            Destroy(currentCollectable); // Destroy the collectable object
            collectedJewels++; // Increment the count of collected jewels
            currentCollectable = null; // Reset the stored collectable
            ShowMessage("Collected Jewels: " + collectedJewels + "/" + totalCollectables);
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        // Updates the Thief's Animator parameters according to left/right & up/down movement
        _animator.SetFloat(_horizontal, _movement.x);
        _animator.SetFloat(_vertical, _movement.y);

        if (_movement != Vector2.zero)
        {
            _animator.SetFloat(_lasthorizontal, _movement.x);
            _animator.SetFloat(_lastvertical, _movement.y);
        }
    }

    // Function to show message on screen
    private void ShowMessage(string message)
    {
        // Start a coroutine to display the message
        StartCoroutine(DisplayMessage(message));
    }
    private IEnumerator DisplayMessage(string message)
    {
        // Move the messageText down slightly to avoid overlap
        messageText.rectTransform.position = originalMessagePosition + new Vector3(0, -30 * collectedJewels, 0);

        // Display the message
        messageText.text = message;

        // Wait for the specified duration
        yield return new WaitForSeconds(messageDuration);

        // Hide the message
        messageText.text = "";

        // Reset position for the next message
        messageText.rectTransform.position = originalMessagePosition;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (Transform child in collision.transform)
        {
            if (child.CompareTag("Collectable"))
            {
                currentCollectable = child.gameObject; // Store the collectable object
                ShowMessage("Found Jewel! Press Enter");
                break; // Exit the loop after finding the collectable
            }
            else if (child.CompareTag("Exit"))
            {
                if (collectedJewels >= totalCollectables)
                {
                    ShowMessage("Finished Level!");
                    // Uncomment this to load next level
                    if (SceneManager.GetActiveScene().name == "Level 1")
                    {
                        SceneManager.LoadScene("Level 2");
                    }
                    else if (SceneManager.GetActiveScene().name == "Level 2")
                    {
                        SceneManager.LoadScene("Level 3");
                    }
                    else if (SceneManager.GetActiveScene().name == "Level 3")
                    {
                        SceneManager.LoadScene("Win Screen");
                    }    
                }
                else
                {
                    ShowMessage("You're still missing Jewels!");
                }
            }
        }
    }
}