using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Area"))
        {
            Vector3 playerPos = Player.Instance.transform.position;
            Vector3 myPos = transform.position;

            float diffX = Mathf.Abs(playerPos.x - myPos.x);
            float diffY = Mathf.Abs(playerPos.y - myPos.y);

            Vector3 playerDir = Player.Instance.inputVec;
            float dirX = playerDir.x > 0 ? 1 : -1;
            float dirY = playerDir.y > 0 ? 1 : -1;

            switch (transform.tag)
            {
                case "Ground":
                    if (diffX > diffY)
                        transform.Translate(Vector3.right * dirX * 52);
                    else if (diffX < diffY)
                        transform.Translate(Vector3.up * dirY * 52);
                    else
                        transform.Translate(Vector3.right * dirX * 52 + Vector3.up * dirY * 52);
                    break;

                case "Enemy":
                    if (col.enabled)
                    {
                        transform.Translate(playerDir * 30 + new Vector3(
                            Random.Range(-3f, 3f),
                            Random.Range(-3f, 3f),
                            0
                            ));
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
