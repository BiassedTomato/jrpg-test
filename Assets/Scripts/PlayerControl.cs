using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    GameObject collidableObject;
    public float Speed=5;
    Vector2 Direction;
    public Rigidbody2D RBody;

    // Use this for initialization
    void OnEnable()
    {
        StartCoroutine("CheckInteraction");
        RBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag=="OverworldInteract")
        {
            collidableObject = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "OverworldInteractible")
        {
            collidableObject = null;
        }
    }

    IEnumerator CheckInteraction()
    {
        for (; ; )
        {
            if (Input.GetKeyDown(KeyCode.E)&&collidableObject!=null)
            {
                collidableObject.GetComponent<OverworldTerrain>().StartFight();
            }

            yield return 0f;

        }
    }

    private void FixedUpdate()
    {
        Direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * Speed;
        RBody.MovePosition((Vector2)transform.position+Direction);
    }

}
