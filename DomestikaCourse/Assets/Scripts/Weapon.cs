using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shooter;

    public GameObject explosionEffect; //Efecto de explosion
    public LineRenderer lineRenderer; //Linea que cruce la pantalla imitando a una bala rapida
    private Transform _firePoint;

    float timer;
    int waitingTime = 2;

    void Awake()
    {
        _firePoint = transform.Find("FirePoint");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Invoke("Shoot", 1f);
        // Invoke("Shoot", 2f);
        // Invoke("Shoot", 3f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        if (bulletPrefab != null && _firePoint != null && shooter != null)
        {
            GameObject myBullet = Instantiate(bulletPrefab, _firePoint.position, Quaternion.identity) as GameObject;

            Bullet bulletComponent = myBullet.GetComponent<Bullet>();

            if (shooter.transform.localScale.x < 0f)
            {
                // Left
                bulletComponent.direction = Vector2.left; // new Vector2(-1f, 0f)
            }
            else
            {
                // Right
                bulletComponent.direction = Vector2.right; // new Vector2(1f, 0f)
            }
        }
    }

    public IEnumerator ShootWithRaycast()
    {
        // Si estan seteadas en el inspector...
        if (explosionEffect != null && lineRenderer != null)
        {
            //genera un "tiro" que atraviesa la pantalla y devuelve informacion dentro de raycasthit2d
            //los parametros son la posicion de disparo y la direccion
            RaycastHit2D hitInfo = Physics2D.Raycast(_firePoint.position, _firePoint.right);
            if (hitInfo)
            {
                //example code
                /*
                //por ejemplo si el rayo toca toca al jugador el hit info tiene como condicion
                // obtener informacion un collider tag player.. entonces podemos obtener informacion del player
                //incluso ejecutar sus metodos para que en este caso se quite 5 de vida, empujarlo, cualquier cosa
                if(hitInfo.collider.tag == "Player"){
                    Transform player = hitInfo.transform;
                    player.GetComponent<PlayerHealth>().ApplyDamage(5);
                }*/

                //Instancia explosion en el punto que toque se instancia en el punto donde fue interceptado
                Instantiate(explosionEffect, hitInfo.point, Quaternion.identity);

                //set line renderer
                lineRenderer.SetPosition(0, _firePoint.position); //seteada la posicion de origen en la punta del arma
                lineRenderer.SetPosition(1, hitInfo.point); //seteada posicion final el final es el punto donde toco con algo
            }
            else
            {
                //Si no se recibio ningun hitinfo, es porque el raycast no choco contra ningun collider
                lineRenderer.SetPosition(0, _firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point + Vector2.right * 100);
            }
            lineRenderer.enabled = true;
            yield return null;
            lineRenderer.enabled = false;
        }
    }
}
