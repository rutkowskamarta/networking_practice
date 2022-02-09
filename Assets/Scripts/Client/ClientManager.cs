using DarkRift;
using DarkRift.Client.Unity;
using UnityEngine;

namespace Game.Client
{
	interface IClientManager
	{
		void SendRequest(string data);
	}

    public class ClientManager : MonoBehaviour, IClientManager
    {
        [SerializeField]
        private UnityClient client;

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
			throw new System.NotImplementedException();
		}

		public void SendRequest(string data)
		{
			using (DarkRiftWriter writer = DarkRiftWriter.Create())
			{
				writer.Write(data);

				using (Message message = Message.Create(0, writer))
					client.SendMessage(message, SendMode.Unreliable);
			}
		}

	}
}