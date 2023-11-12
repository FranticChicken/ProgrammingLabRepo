using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//monobehaviour means it belongs to a game object
public class Player : MonoBehaviour
{
    //serializefield means unity can change a variable
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayers;
    [SerializeField, Range(1,20)] private float mouseSenseX;
    [SerializeField, Range(1,20)] private float mouseSenseY;
    [SerializeField, Range(-90, 0)] private float minViewAngle;
    [SerializeField, Range(0, 90)] private float maxViewAngle;
    [SerializeField] private Transform lookAtPoint;
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private float bulletForce;

    //UI
    [Header("Player UI")]
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI shotsFired;
    [SerializeField] private float maxHealth;
    int shotsFiredCounter;
    float _health;

    private float Health
    {
        get => _health;
        set
        {
            _health = value;
            healthBar.fillAmount = _health / maxHealth;
        }
    }



    private Vector2 currentRotation;

    private bool isGrounded;
    private Vector3 _moveDir;
    private Rigidbody rb;
    private float depth;
    private int score;
    [SerializeField] TextMeshProUGUI scoreText; 
   
    // Start is called before the first frame update
    void Start() 
    {
        InputManager.Init(this);

        //game controls are activated
        InputManager.GameMode();

        rb = GetComponent<Rigidbody>();

        depth = GetComponent<Collider>().bounds.size.y;

       
        scoreText.text = "Points: " + score.ToString();
        shotsFired.text = "Shots Fired: 0";

        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.rotation * (speed * Time.deltaTime * _moveDir);
        CheckGround();

        Health -= Time.deltaTime * 5; 
    }

    public void SetMovementDirection(Vector3 newDirection)
    {
        _moveDir = newDirection;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    } 

    public void SetLookDirection(Vector2 readValue)
    {
        //controls rotation angles
        currentRotation.x += readValue.x * Time.deltaTime * mouseSenseX;
        currentRotation.y += readValue.y * Time.deltaTime * mouseSenseY;

        
        //clamp rotation angle so you cant roll your head
        currentRotation.y = Mathf.Clamp(currentRotation.y, minViewAngle, maxViewAngle);

        //rotates left and right
        transform.rotation = Quaternion.AngleAxis(currentRotation.x, Vector3.up);

        //rotate up and down
        lookAtPoint.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.right);

          
    }

    public void Shoot()
    {
        //instantiate lets you create an object
        //spawn the object as a rigid body
        Rigidbody currentProjectile = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        //add instant force in the look at direction of the player
        currentProjectile.AddForce(lookAtPoint.forward * bulletForce, ForceMode.Impulse);

        ++shotsFiredCounter;
        shotsFired.text = "Shots Fired: " + shotsFiredCounter.ToString();

        //destroy destroys an object
        //destroy after 4 seconds 
        Destroy(currentProjectile.gameObject, 4);

        
    }
    

    public void ShootingProof()
    {
        Debug.Log("Pew! Pew! Pew!");
    }

    public void CrouchProof()
    {
        Debug.Log("Crouch");
    }

    private void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, depth, groundLayers);
        Debug.DrawRay(transform.position, Vector3.down * depth, Color.green, 0, false);
    }

    public void AddOneToScore()
    {
        score += 1; 
    }


    public void ReprintScore()
    {
        
        scoreText.text = "Points: " + score.ToString();
        Debug.Log("changed text");
        
    }

    

   
 }
