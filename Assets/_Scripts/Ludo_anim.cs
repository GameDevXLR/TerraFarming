using UnityEngine;

public class Ludo_anim : MonoBehaviour
{
    private Animator anim;
    public float Speed;
    public float TurnSpeed;
    public int forceConst = 20;
    public bool isgrounded;

    private bool canJump;
    public Rigidbody selfRigidbody;

    private int jumpHash = Animator.StringToHash("Jump");

    // Use this for initialization
    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        anim = GetComponent<Animator>();
        selfRigidbody = GetComponent<Rigidbody>();
    }

    public void JumpUI()
    {
        if (isgrounded == true && (!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump1")))
        {
            canJump = true;
        }
    }

    private void FixedUpdate()
    {
        if (canJump)
        {
            canJump = false;
            selfRigidbody.AddForce(0, forceConst, 0, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        float move = Input.GetAxis("Vertical");
        float rotation = Input.GetAxis("Horizontal");
        anim.SetFloat("TurnSpeed", rotation);
        anim.SetFloat("Speed", move);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump1"))
        {
            isgrounded = false;
        }
        else
        {
            isgrounded = true;
        }

        //AVANCER
        if (move > 0 || Input.GetKey(KeyCode.UpArrow))//(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, ((anim.speed * 10) * Time.deltaTime));
        }

        //RECULER
        if (move < -0) //(Input.GetKey(KeyCode.DownArrow))
        {
            canJump = false;
            transform.Translate(0, 0, -((anim.speed * 5) * Time.deltaTime));
        }

        //TOURNER A GAUCHE
        if (rotation < -0.5) //Input.GetKey(KeyCode.LeftArrow)
        {
            transform.Rotate(0, -(anim.speed * Time.deltaTime) * 70, 0);
        }

        //TOURNER A DROITE
        if ((rotation > 0.5)) //Input.GetKey(KeyCode.RightArrow)
        {
            transform.Rotate(0, (anim.speed * Time.deltaTime) * 70, 0);
        }

        //SAUTER
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 0"))
        {
            if (isgrounded == true && (!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump1")))
            {
                canJump = true;
                anim.SetTrigger(jumpHash);
            }
        }
    }
}