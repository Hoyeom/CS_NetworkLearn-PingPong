using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MyNetworkManager : NetworkManager
{
    private GameObject ball;
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        if (numPlayers != 2) return;
        ball = Instantiate(spawnPrefabs.Find(
            prefab => prefab.name == "Ball"));
        NetworkServer.Spawn(ball);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if(ball !=null)
            NetworkServer.Destroy(ball);
        
        base.OnServerDisconnect(conn);
    }
}
