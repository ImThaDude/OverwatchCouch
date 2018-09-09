using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestClient : MonoBehaviour {

	NetworkClient client;
	[SerializeField]
	bool connectServer = false;
	[SerializeField]
	bool sendMessage = false;

	void Update() {

		if (connectServer) {
			client = new NetworkClient ();
			client.RegisterHandler (MsgType.Connect, OnConnected);
			var config = new ConnectionConfig ();
			config.Channels.Add (new ChannelQOS(QosType.Unreliable));
			config.ConnectTimeout = 100;
			client.Configure (config, 10);
			client.Connect ("localhost", 25565);
			connectServer = false;
		}

		if (sendMessage) {
			client.Send (145, new CoolMessage("Stuff"));
			sendMessage = false;
		}

	}

	void OnConnected(NetworkMessage msg) {
		Debug.Log ("Client Connected");
	}

	public class CoolMessage : MessageBase {
		public string cool;

		public CoolMessage(string cool) {
			this.cool = cool;
		}

		public override void Serialize (NetworkWriter writer)
		{
			writer.Write (cool);
		}

		public override void Deserialize (NetworkReader reader)
		{
			cool = reader.ReadString ();
		}

	}



}
