using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Clase base para crear los diferentes estados(ScriptableObject) del GameObject

//Esto añade en el menu de crear asset la opcion de crear un estado
[CreateAssetMenu (menuName = "AIController/State")]

public class State : ScriptableObject {

    // Update is called once per frame
    //Las acciones que se van a realizar cuando el GameObject se encuentre en este estado
    public Action[] actions;
    //Las transiciones que se pueden dar de este estado a otros
    public Transition[] transitions;
    bool decisionSuceeded;

public void UpdateState (StateController controller) {
        DoActions(controller);
        CheckTransitions(controller);
	}

//Itera sobre las acciones y las ejecuta
private void DoActions(StateController controller)
    {
        for (int i=0; i<actions.Length; ++i){
            actions[i].Act(controller);
        }
    }

    /*Itera sobre las transiciones y comprueba si se cumplen las condiciones para cambiar de estado.
    En caso de que se cumplan cambia de estado. Las transiciones tienen prioridad segun su posicion en el array*/
    private void CheckTransitions(StateController controller)
    {
        if (controller != null) {
        for (int i = 0; i < transitions.Length && !decisionSuceeded; ++i)
        {
            decisionSuceeded = transitions[i].decision.Decide(controller);
            if (decisionSuceeded)
            {
                controller.TransitionToState(transitions[i].trueState);
            }
            else
            {
                controller.TransitionToState(transitions[i].falseState);
            }
        }
        decisionSuceeded = false;
    }
    }


}
