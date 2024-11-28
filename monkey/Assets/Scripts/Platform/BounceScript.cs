using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    private AudioController audioController;
    private float jumpForce=600f;
    private float Scr;
    private float timer=0;
    private int cnt=100;
    void Start()
    {
        audioController = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioController>();
      
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(PlayerBehaviour.staScore!=0)
        Scr = PlayerBehaviour.staScore;
        if(Scr%cnt==0)
        {
            if (timer>15f)
            {
                cnt +=100;
                timer = 0;
                jumpForce += 100f;
            }  
        }    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !DestroyScript.isEquipped)
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (rb.velocity.y <= 0)
                {
                    if (audioController.loxoClip != null)
                    {
                        audioController.PlaySFX(audioController.loxoClip);
                    }
                    else
                    {
                        Debug.LogError("jumpClip chưa được gán trong AudioController!");
                    }
                    rb.AddForce(Vector2.up * jumpForce);
                }
            }
        }
    }
}
