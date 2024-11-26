using System.Collections;
using UnityEngine;

public class ScreenWarpScript : MonoBehaviour
{
    private bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Player") && !isTeleporting)
        {
            isTeleporting = true;

            if (gameObject.CompareTag("rightWall"))
            {
                obj.transform.position = new Vector2(-6f, obj.transform.position.y);
            }
            else if (gameObject.CompareTag("leftWall"))
            {
                obj.transform.position = new Vector2(6f, obj.transform.position.y);
            }
            StartCoroutine(TeleportCooldown());
        }
    }

    private IEnumerator TeleportCooldown()
    {

        yield return new WaitForSeconds(0.5f);
        isTeleporting = false;
    }
}