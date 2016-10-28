using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    private Animator anim;
    private Rigidbody2D rb2d;
    public float Velocidade;
    [HideInInspector]
    public bool ViradoDireita = true;
    private float ShootingRate = 0.1f;
    private float ShootCooldown = 0f;
    public Transform SpawnBullet;
    public GameObject Bullet;

    void Start() {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void FixedUpdate()
    {
        if (ShootCooldown > 0)
            ShootCooldown -= Time.deltaTime;


        float translationY = 0;
        float translationX = Input.GetAxis("Horizontal") * Velocidade;
        transform.Translate(translationX, translationY, 0);
        if (translationX != 0)
        {
            anim.SetTrigger("Corre");
        }
        else
        {
            anim.SetTrigger("Parado");
        }
        if (translationX > 0 && !ViradoDireita) 
        {
            Flip(); 
        }
        else if (translationX < 0 && ViradoDireita)
            Flip();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Atirar");
            Fire();
            ShootCooldown = ShootingRate;


        }
            
    }
    void Flip()
    {
        ViradoDireita = !ViradoDireita; 
        Vector3 theScale = transform.localScale; 
        theScale.x *= -1; 
        transform.localScale = theScale;
    }
   void Fire()
    {
        if (ShootCooldown <= 0f)
        {
            if(Bullet != null)
            { var cloneBullet = Instantiate(Bullet, SpawnBullet.position, Quaternion.identity) as GameObject;
                cloneBullet.transform.localScale = this.transform.localScale;

            }
        }
    }
}
