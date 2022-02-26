using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using System;
using UnityEngine;

namespace Game.Client
{
    public class GameClientManager : MonoBehaviour, IGameClientManager
    {
		public event Action<MessageReceivedEventArgs> OnMessageReceived;

		[SerializeField]
        private UnityClient client;

		public bool IsClientConnected => client.ConnectionState == ConnectionState.Connected;

		private void OnEnable()
		{
			client.MessageReceived += Client_MessageReceived;
		}

		private void OnDisable()
		{
			client.MessageReceived -= Client_MessageReceived;
		}

		private void Client_MessageReceived(object sender, MessageReceivedEventArgs messageEvent)
		{
			Debug.Log($"Received message of tag {messageEvent.GetMessage().Tag}");
			OnMessageReceived?.Invoke(messageEvent);
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