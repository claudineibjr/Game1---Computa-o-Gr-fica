using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public float speed;
    public GameObject Projectile;
    public int minZoom = 5;
    public int maxZoom = 20;

    private Vector3 direction;
    private TextMesh textPoints;
    private int points;
    private TextMesh A_Seta;


  	// Use this for initialization
	void Start () {
        textPoints = GameObject.Find("TextPoints").GetComponent<TextMesh>();
        textPoints.text = "" + 0 + "";
    }
	
	// Update is called once per frame
	void Update () {
        // Movimento do usuário
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, speed, 0);
        if (Input.GetKey(KeyCode.A))
            transform.position += new Vector3(-speed, 0, 0);
        if (Input.GetKey(KeyCode.S))
            transform.position += new Vector3(0, -speed, 0);
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(speed, 0, 0);

        // Disparo
        if (Input.GetMouseButton(0))
        {
            Instantiate(Projectile, transform.position, Quaternion.Euler(0, 0, 0));
        }

        // Zoom da camera
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // Rolagem para cima
        {
            if (Camera.main.orthographicSize > minZoom)
                Camera.main.orthographicSize--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // Rolagem para baixo
        {
            if (Camera.main.orthographicSize < maxZoom)
                Camera.main.orthographicSize++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Se o inimigo enconstar no personagem, o personagem morre e a aplicação é encerrada
        if (other.tag == "Enemy") {
            Destroy(this.gameObject);
            Application.Quit();
        }
    }

}