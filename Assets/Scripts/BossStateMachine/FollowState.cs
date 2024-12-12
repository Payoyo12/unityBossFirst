using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : State
{
    public FollowState(BossController boss) : base(boss) { }

    private Transform playerTransform; // Referencia al jugador
    private float velocidadMovimiento;

    public override void Entry()
    {
        base.Entry();

        playerTransform = Boss.getPlayer().transform;
        velocidadMovimiento = Boss.bossVelocity;




        Boss.StartCoroutine(Spit());
        Debug.Log("Follow State Entered");
    }

    public override void Update()
    {
        base.Update();


        // Verificamos que el jugador no sea nulo
        if (playerTransform != null)
        {
            // Movemos al enemigo hacia el jugador
            Vector2 posicionActual = Boss.transform.position;
            Vector2 posicionJugador = playerTransform.position;

            // Calculamos la nueva posición
            Vector2 nuevaPosicion = Vector2.MoveTowards(posicionActual, posicionJugador, velocidadMovimiento * Time.deltaTime);

            // Asignamos la nueva posición
            Boss.transform.position = nuevaPosicion;
        }


        if(Boss.GetHealthPercentage() < .5f) Boss.ChangeStateKey(States.Rage);
    }

    IEnumerator Spit()
    {
        yield return new WaitForSeconds(1f);
        Boss.ChangeStateKey(States.Spit);
    }
}
