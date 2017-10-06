using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

    public enum Player {One, Two};
    public Player playerNumber;

    public float movementSpeed;
    public float jumpForce = 40;

    public LayerMask groundLayer;

    float horizontalTop;
    float verticalTop;

	float horizontalBot;
	float verticalBot;

    Rigidbody2D rb;

    bool grounded;

    public AudioSource Jump;

    private Animator anim;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        anim.SetBool("Grounded", grounded);

        horizontalTop = Input.GetAxis("HorizontalTop");
       verticalTop = Input.GetAxis("VerticalTop");

		horizontalBot = Input.GetAxis("HorizontalBot");
		verticalBot = Input.GetAxis("VerticalBot");

        RaycastHit2D leftHit = Physics2D.Raycast(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), Vector2.down * rb.gravityScale, 2, groundLayer); //Creates a raycast aimed downwards and saves it "hit" info into a raycastHit2d variable. It ignores objects that are not in the ground layer.
        Debug.DrawRay(new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z), new Vector2(0, -1f * rb.gravityScale));

        RaycastHit2D rightHit = Physics2D.Raycast(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), Vector2.down * rb.gravityScale, 2, groundLayer); //Creates a raycast aimed downwards and saves it "hit" info into a raycastHit2d variable. It ignores objects that are not in the ground layer.
        Debug.DrawRay(new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z), new Vector2(0, -1f * rb.gravityScale));
        if (leftHit.collider != null || rightHit.collider != null) //Checks if the raycast collides with anything on the ground layer.
        {
           // Debug.Log("hit");
            grounded = true; //sets the grounded bool to true, so that the script knows if the object has touched ground.
        }
        if (leftHit.collider == false || rightHit.collider == false)
        {
            //Debug.Log("not");
            grounded = false;
        }

		if (horizontalTop < 0 && playerNumber == Player.One || horizontalBot < 0 && playerNumber == Player.Two)
        {
            rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
        }

		if (horizontalTop > 0 && playerNumber == Player.One || horizontalBot > 0 && playerNumber == Player.Two)
        {
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }

		if(verticalTop > 0 && playerNumber == Player.One && grounded || verticalBot > 0 && playerNumber == Player.Two && grounded)
        {
			Jump.Play();
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce * rb.gravityScale));
        }

        anim.SetFloat("Walking", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        if (GetComponent<Rigidbody2D>().velocity.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }

	void OnCollisionEnter2D(Collision2D other)
	{

		if (other.transform.tag == "MovingPlatform")
		{
			transform.parent = other.transform;
		} 
	}
	void OnCollisionExit2D(Collision2D other)
	{
		if (other.transform.tag == "MovingPlatform")
		{
			transform.parent = null;
		}
	}

}
