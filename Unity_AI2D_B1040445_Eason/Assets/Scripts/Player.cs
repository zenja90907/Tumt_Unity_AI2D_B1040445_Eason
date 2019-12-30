using UnityEngine;
using UnityEngine.Events;
public class Player : MonoBehaviour
{
    public int speed = 50;
    public float jump = 5f;
    private bool isGround;
    private Rigidbody2D r2d;
    private Transform tra;
    public UnityEvent onEat;

    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        tra = GetComponent<Transform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) Turn();
        if(Input.GetKeyDown(KeyCode.A)) Turn(180);
    }

    private void FixedUpdate()
    {
        Walk();
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="鑽石")
        {
            Destroy(collision.gameObject);
            onEat.Invoke();
        }
    }

    /// <summary>
    /// 走路
    /// </summary>
    private void Walk()
    {
        r2d.AddForce(new Vector2(speed * Input.GetAxis("Horizontal"),0));
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&isGround==true)
        {
            isGround = false;
            r2d.AddForce(new Vector2(0, jump));
        }
    }

    /// <summary>
    /// 轉彎
    /// </summary>
    /// <param name="direction"></param>
    private void Turn(int direction=0)
    {
        tra.eulerAngles = new Vector3(0, direction,0);
    }
}
