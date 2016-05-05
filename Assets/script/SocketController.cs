using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Sockets;
using SocketIOClient;

public class SocketController : MonoBehaviour {

	public SocketIOClient.Client client;

	void Start() {
		string serverUrl = "http://127.0.0.1:8080";
		this.client = new Client(serverUrl);

		this.client.Opened += SocketOpened;
		this.client.Message += SocketMessage;

		this.client.Connect();
	}

	private void SocketOpened(object sender, EventArgs e) {
		Debug.Log("Connect !");
	}

	private void SocketMessage(object sender, MessageEventArgs e) {
		if ( e!= null && e.Message.Event == "deviceY") {
			DeviceOrientationClass deviceOrientationY = new DeviceOrientationClass();
			deviceOrientationY = JsonUtility.FromJson<DeviceOrientationClass> (e.Message.MessageText);

			float value = (float)deviceOrientationY.args[0];
			Debug.Log(value);
		}
		if ( e!= null && e.Message.Event == "deviceX") {
			DeviceOrientationClass deviceOrientationX = new DeviceOrientationClass();
			deviceOrientationX = JsonUtility.FromJson<DeviceOrientationClass> (e.Message.MessageText);

			float value = (float)deviceOrientationX.args[0];
			Debug.Log(value);
		}
	}

	public void sendCarSpeed(object data) {
		this.client.Emit("carSpeed", data);
	}
}
