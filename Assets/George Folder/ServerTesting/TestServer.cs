using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestServer : MonoBehaviour {

	NetworkServerSimple server;

	void Start() {

		server = new NetworkServerSimple ();
		server.listenPort = 25565;
		server.RegisterHandler(MsgType.Connect, OnConnected);
		server.RegisterHandler (145, MessageRead);
		var config = new ConnectionConfig ();
		config.Channels.Add (new ChannelQOS(QosType.Unreliable));
		config.ConnectTimeout = 100;
		server.Configure (config, 10);
		server.Listen (25565);
	}


	void Update() {
		server.Update ();
	}

	void OnConnected(NetworkMessage netMsg)
	{
		
		Debug.Log("Client connected");
	}

	void MessageRead(NetworkMessage netMsg) {
		TestClient.CoolMessage test = new TestClient.CoolMessage ("");
		test.Deserialize (netMsg.reader);
		Debug.Log("MessageReceived: " + test.cool);
	}

}
