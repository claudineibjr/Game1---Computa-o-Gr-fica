using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float speed = 2f; //Default: 2f
    public float lifeTime = 5; //Default: 5

    private Vector3 posicaoDoMouse;
    private Vector3 direcaoParaSeMover;
    private float angle;

    // Use this for initialization
    void Start()
    {
        // Definindo a posição do mouse
        posicaoDoMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posicaoDoMouse.z = this.transform.position.z;
        direcaoParaSeMover = (posicaoDoMouse - transform.position).normalized;

        // Ângulo do projétil
        Vector3 anguloDoAlvo = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        anguloDoAlvo.z = 0;

        float angulo = Mathf.Atan2(
                                    anguloDoAlvo.y - transform.position.y,
                                    anguloDoAlvo.x - transform.position.x
                                    ) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(
                                                transform.rotation.eulerAngles.x,
                                                transform.rotation.eulerAngles.y,
                                                angulo);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (direcaoParaSeMover * speed);

        // Depois de determinado tempo de vida do projétil, ele desaparece
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(this.gameObject);

    }

	void OnTriggerEnter(Collider other)
	{
        // Se o projétil encontrar um inimigo, destrói o inimigo e se destrói
        if (other.tag == "Enemy")
        {
            // Recupera o texto dos pontos
            TextMesh textPoints;
            textPoints = GameObject.Find("TextPoints").GetComponent<TextMesh>();
            int points = int.Parse(textPoints.text);

            // Incrementa a pontuação e a exibe
            points++;
            textPoints.text = "" + points + "";

            // Destrói tanto o inimigo quanto o projétil
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
	}

}