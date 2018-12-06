using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Esta clase se asocia al objeto*/

public class StateController : MonoBehaviour {

    //Estado actual
    public State currentState;
    //Puntos de patrulla del objeto
    //public GameObject [] patrolPoints;
   
    public State remainState;
    public Enemy enemyStats;

    //Actions
    public float timerActionAttack;
    //Numero para acceder a una posicion del array. Iguala al numero de veces que el GameObject a llegado a su destino
    public int patrolCount = 0;

    //Decision
    //Puntos de patrulla mas lejanos entre si
    public GameObject furthestLeftPoint;
    public GameObject furthestRightPoint;

    //Puntos de patrulla mas lejanos entre si
    public GameObject furthestLeftPointKnocked;
    public GameObject furthestRightPointKnocked;


    void Start () {
        enemyStats = gameObject.GetComponent<Enemy>();
	}
	
	
	void Update () {
        currentState.UpdateState(this);
	}

    //Cambia de estado
    public void TransitionToState(State nextState) {
        if(nextState != remainState)
            currentState = nextState;
    }

}
