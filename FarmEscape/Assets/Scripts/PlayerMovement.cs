using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    private Animator anim;
    private bool grounded;

    private void Awake() {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }*/

    // Update is called once per frame
    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput* speed, body.velocity.y);

        //flip player when moving left or right
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(8,8,8);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-8, 8, 8);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        //Set animator parameters
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump"); 
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Ground")
        grounded = true;
    }
}
