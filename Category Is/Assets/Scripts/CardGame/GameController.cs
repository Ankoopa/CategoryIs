using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;


public class GameController : MonoBehaviourPunCallbacks
{
    private PunTurnManager turnManager;

    void Start()
    {
        // this.turnManager = this.gameObject.AddComponent<PunTurnManager>();
        // //this.turnManager.TurnManagerListener = this;
        // this.turnManager.TurnDuration = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
