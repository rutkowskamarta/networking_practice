using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Client
{
    public class GameClientManager : MonoBehaviour, IGameClientManager
    {
		public event Action<ConnectionState> OnConnectionStateChanged;
		public event Action<MessageReceivedEventArgs> OnMessageReceived;
		public event Action OnFirstConnectionEstablished;

		[SerializeField]
        private UnityClient client;
		[SerializeField]
		private float connectRetryFrequency = 1f;

		public bool IsClientConnected => client.ConnectionState == ConnectionState.Connected;

		private ConnectionState connectionState;
		private bool wasAlreadyEstablished;

		private void Update()
		{
			HandleConnectionState();
		}

		private void OnEnable()
		{
			client.MessageReceived += Client_MessageReceived;
		}

		private void OnDisable()
		{
			client.MessageReceived -= Client_MessageReceived;
		}

		private void HandleConnectionState()
		{
			if (connectionState != client.ConnectionState)
			{
				connectionState = client.ConnectionState;
				OnConnectionStateChanged?.Invoke(connectionState);

				if (connectionState == ConnectionState.Disconnected)
				{
					SetupConnection();
				}
			}
		}

		private void Client_MessageReceived(object sender, MessageReceivedEventArgs messageEvent)
		{
			Debug.Log($"Received message of tag {messageEvent.GetMessage().Tag}");
			OnMessageReceived?.Invoke(messageEvent);
		}

		public void SetupConnection()
		{
			StartCoroutine(ConnectToServerCoroutine());
		}

		private IEnumerator ConnectToServerCoroutine()
		{
			connectionState = client.ConnectionState;
			while (client.ConnectionState != ConnectionState.Connected)
			{
				try
				{
					client.Connect(client.Host, client.Port, true);
				}
				catch(Exception e)
				{
					Debug.Log("Could not connect to the server");
				}
				yield return new WaitForSeconds(connectRetryFrequency);
			}
			if (!wasAlreadyEstablished)
			{
				OnFirstConnectionEstablished?.Invoke();
				wasAlreadyEstablished = true;
			}
		}

		public void SendRequest(ushort tag, IDarkRiftSerializable data = null)
		{
			Debug.Log($"Sending message of tag {tag}");

			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				if (data != null)
				{
					writer.Write<IDarkRiftSerializable>(data);
				}

				using (Message message = Message.Create(tag, writer))
					client.SendMessage(message, SendMode.Unreliable);
			}
		}
	}
}