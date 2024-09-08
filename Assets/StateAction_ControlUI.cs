using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAction_ControlUI : StateMachineBehaviour
{
    [SerializeField]
    private string _panelID;

    [SerializeField]
    private bool _enabled;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Core.UI.UIPanelManager.instance.EnablePanel(_panelID, _enabled);
    }
}
