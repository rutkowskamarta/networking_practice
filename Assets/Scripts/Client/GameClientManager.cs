using DarkRift;
using DarkRift.Client.Unity;
using UnityEngine;

namespace Game.Client
{
    public class GameClientManager : MonoBehaviour, IGameClientManager
    {
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

		private void Client_MessageReceived(object sender, DarkRift.Client.MessageReceivedEventArgs e)
		{
		}

		public void SendRequest(ushort tag, IDarkRiftSerializable data)
		{
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write<IDarkRiftSerializable>(data);

				using (Message message = Message.Create(tag, writer))
					client.SendMessage(message, SendMode.Unreliable);
			}
		}
	}
}