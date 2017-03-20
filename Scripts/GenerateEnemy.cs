using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour {

    public float tempoDeNascerNovoInimigo; //Default: 2
    public float distanciaParaNascer; //Default: 10
    public GameObject inimigoANascer; //Default: Inimigo

    private int points_TempoParaNascer = 0;
    private float contaTempo;
    private bool bPP_TempoParaNascer = false; //Variável que controla se o tempo para nascer já foi incrementado
    private float diminuicaoTempoParaNascer = 0.2f; // Variável que controla de quanto em quanto tempo será decrementado o nascimento de novos personagens ao atingir determinada pontuação	
    private float pontosParaNascerMaisRapido = 10; // Variável que determina de quantos em quantos pontos deverá diminuir o tempo de nascimento de novos inimigos

	// Use this for initialization
	void Start () {
		contaTempo = tempoDeNascerNovoInimigo;
	}
	
	// Update is called once per frame
	void Update () {

        // Recupera a pontuação atual
        TextMesh textPoints;
        textPoints = GameObject.Find("TextPoints").GetComponent<TextMesh>();
        points_TempoParaNascer = int.Parse(textPoints.text);

        // Verifica se a pontuação atual é multipla de 10 e maior do que 0 e então diminui o tempo que eles levam pra nascer
        if (points_TempoParaNascer % pontosParaNascerMaisRapido == 0 && points_TempoParaNascer > 0)
        {
            // Verifica se a variável tempoDeNascerNovoInimigo já foi decrementada
            if (!bPP_TempoParaNascer)
            {
                tempoDeNascerNovoInimigo = tempoDeNascerNovoInimigo - diminuicaoTempoParaNascer;
                points_TempoParaNascer = 0;
                bPP_TempoParaNascer = true;
                print("Os caras vão nascer mais rápido");
            }
        }
        else
        {
            // Indica que a variável tempoDeNascerNovoInimigo ainda não foi decrementada nesta nova sequências
            bPP_TempoParaNascer = false;
        }

        // Decrementa a variável contaTempo segundo a segundo
        contaTempo -= Time.deltaTime;

        // Se o tempo for menor do que 0, é porquê já passou o tempo necessário
		if (contaTempo < 0) {
			contaTempo = tempoDeNascerNovoInimigo;

            float distanciaEntreOInimigoEOPersonagem = 0;
            Vector3 posicaoANascer = new Vector3(0, 0, 0);

            // Enquanto a distância entre o inimigo e o jogador for menor do que a definida, deve-se novamente gerar aleatoriamente o lugar para ele nascer
            while (distanciaEntreOInimigoEOPersonagem < distanciaParaNascer)
            {
                // Posição aleatória para nascer
                posicaoANascer = new Vector3(Random.Range(-distanciaParaNascer, distanciaParaNascer), Random.Range(-distanciaParaNascer, distanciaParaNascer), 0);
                distanciaEntreOInimigoEOPersonagem = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, posicaoANascer);
            }

            // Instancia o novo inimigo
			Instantiate (inimigoANascer, posicaoANascer, Quaternion.Euler(
                                                Random.Range(-distanciaParaNascer, distanciaParaNascer),
                                                Random.Range(-distanciaParaNascer, distanciaParaNascer),
                                                0));
		}
	}
}
