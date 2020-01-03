using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    [Header("移動速度"), Range(0, 100)]
    public float speed = 1.5f;
    [Header("傷害"), Range(0, 100)]
    public float damage = 20;

    public Transform CheckPoint;

    private Rigidbody2D r2d;

    private void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 繪製圖示
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(CheckPoint.position, -CheckPoint.up * 2);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name=="玩家")
        {
            Track(collision.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "玩家")
        {
            collision.gameObject.GetComponent<Player>().Damage(damage);
        }
    }

    /// <summary>
    /// 隨機移動
    /// </summary>
    private void Move()
    {
        r2d.AddForce(-transform.right * speed);

        RaycastHit2D hit = Physics2D.Raycast(CheckPoint.position, -CheckPoint.up, 1.5f, 1 << 8);

        if(hit==false)
        {
            transform.eulerAngles += new Vector3(0, 180, 0);
        }
    }

    /// <summary>
    /// 追蹤玩家
    /// </summary>
    /// <param name="target">玩家座標</param>
    private void Track(Vector3 target)
    {
        if(target.x<transform.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}

