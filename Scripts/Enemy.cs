using UnityEngine;

public class Enemy : MonoBehaviour {

    Transform target;

    public float speed; //Default: 0.15
    public float distance; //Default: 2
    public float distanceToPPSpeed; //Default: 10
    public float viewConeAngleCosine; //Default: 0.8f
    public float radius; //Default: 3f;

    private int points_Velocidade = 0;
    private bool bPP_Velocidade = false;  //Variável que controla se o incremento da velocidade já foi realizado
    private float pontosParaIrMaisRapido = 20; // Variável que determina de quantos em quantos pontos deverá diminuir o tempo de nascimento de novos inimigos
    private float aumentoVelocidade = 1.5f; // Variável que controla quanto da velocidade será incrementado quando o personagem atingir determinada pontuação
    private float newX;
    private float newY;

    // Recupera a pontuação atual
    TextMesh textPoints;

    // Use this for initialization
    void Start () {
        // Recupera o GameObject com a Tag player e o transforma num transform.

		//target = GameObject.Find ("Personagem").GetComponent<Transform>(); //1ª forma de encontrar determinado GameObject
		target = GameObject.FindGameObjectWithTag("Player").transform;
	}

    // Update is called once per frame
    void Update() {

        // Recupera a pontuação atual
        textPoints = GameObject.Find("TextPoints").GetComponent<TextMesh>();
        points_Velocidade = int.Parse(textPoints.text);

        // Verifica se a pontuação atual é multipla de 10 e maior do que 0 e então diminui o tempo que eles levam pra nascer
        if (points_Velocidade % pontosParaIrMaisRapido == 0 && points_Velocidade > 0)
        {
            // Verifica se a variável tempoDeNascerNovoInimigo já foi decrementada
            if (!bPP_Velocidade)
            {
                speed = speed * aumentoVelocidade;
                points_Velocidade = 0;
                bPP_Velocidade = true;
                print("Aumento a velociade");
            }
        }
        else
        {
            // Indica que a variável tempoDeNascerNovoInimigo ainda não foi decrementada nesta nova sequências
            bPP_Velocidade = false;
        }

        // Recupera a posição de onde está o personagem
        float angulo = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        newX = radius * Mathf.Cos(angulo);
        newY = radius * Mathf.Sin(angulo);

        Vector3 playerDir = transform.TransformDirection(newX, newY, 0).normalized;

        // Recupera a direção para onde ir até o personagem
        Vector3 direcao = (target.position - transform.position).normalized;

        // Recupera o angulo entre a visão do inimigo e o personagem
        float dotValue = Vector3.Dot(direcao, playerDir);

        // Se o personagem estiver no ângulo de visão do inimigo, o inimigo avança
        if (dotValue > viewConeAngleCosine) //TODO
        {

            // Recupera a distância entre o personagem e o inimigo
            float distanceBetweenEnemyAndTarget = Vector3.Distance(target.position, transform.position);

            // Se a diferença entre o inimigo e o personagem for maior do que a distância definida, a velocidade do inimigo é dobrada
            if (distanceBetweenEnemyAndTarget > distanceToPPSpeed)
                transform.position += direcao * speed * 2;
            else
                transform.position += direcao * speed;
        }
    }
}