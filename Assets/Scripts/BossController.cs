using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private GameObject ToxicArea;
    [SerializeField] private PlayerController PlayerControler;
    [SerializeField] private GameObject Player;
    [SerializeField] public float bossVelocity = 5;

    public float currentHealth;
    private float maxHealth = 200;
    
    State currentState;
    Dictionary<States, State> statesDict = new Dictionary<States, State>();


    public GameObject proyectilPrefab; // Prefab del proyectil
    public Transform proyectilTransforInstanciacion;    // Punto desde donde se dispara
    public float proyectilVelocity = 10f;
    public GameObject toxicCloudPrefab; // Prefab del proyectil
    public Transform toxicCloudTransforInstanciacion;    // Punto desde donde se dispara

    


    // ready
    void Start() 
    {
        // inicializar datos boss
        currentHealth = maxHealth;
        
        // inicializar estados:
        //      definir estado inicial
        currentState = new FollowState(this);
        currentState.Entry();
        //      crear lista de estados
        statesDict.Add(States.Follow, currentState);
        statesDict.Add(States.Rage, new RageState(this));
        statesDict.Add(States.Spit, new SpitState(this));
        statesDict.Add(States.Burp, new BurpState(this));
        statesDict.Add(States.Recovery, new RecoveryState(this));
        
        //      preparar sistema de eventos
    }

    // process
    void Update()
    {
        // llamar update del estado actual
        currentState.Update();
    }


    public GameObject getPlayer()
    {
        return Player;
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    public void ChangeStateKey(States newState)
    {
        if(statesDict.ContainsKey(newState))
        {
            ChangeState(statesDict[newState]);
        }
        else
        {
            Debug.LogWarning("State not in list.");
        }
    }
    
    void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Entry();
    }



    public void starSpit()
    {
        // Instanciar el proyectil en el punto de disparo con la rotación actual
        if (proyectilPrefab != null && proyectilTransforInstanciacion != null)
        {
            GameObject proyectil = Object.Instantiate(proyectilPrefab, proyectilTransforInstanciacion.position, proyectilTransforInstanciacion.rotation);

            // Añadir fuerza al proyectil si tiene un Rigidbody2D
            Rigidbody2D rb = proyectil.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Aplica una fuerza en la dirección deseada (right para 2D)
                Vector2 direccion = proyectilTransforInstanciacion.right * proyectilVelocity;
                rb.AddForce(direccion, ForceMode2D.Impulse); // Modo Impulse para una fuerza instantánea
            }
            else
            {
                Debug.LogError("El proyectil no tiene un Rigidbody2D.");
            }
        }
        else
        {
            Debug.LogError("ProyectilPrefab o PuntoDisparo no están asignados en el BossController.");
        }
    }

    public void starBurp()
    {
        // Instanciar el toxicCloud
        if (toxicCloudPrefab != null && toxicCloudTransforInstanciacion != null)
        {
            GameObject toxicCloud = Object.Instantiate(toxicCloudPrefab, toxicCloudTransforInstanciacion.position, toxicCloudTransforInstanciacion.rotation);

        }
        else
        {
            Debug.LogError("toxicCloudPrefab o toxicCloudTransforInstanciacion no están asignados en el BossController.");
        }
    }

}

public enum States
{
    Follow, Spit, Burp, Recovery, Rage,
}