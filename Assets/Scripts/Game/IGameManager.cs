using System;
using System.Collections.Generic;

namespace Game.Game
{
    public interface IGameManager
    {
        event Action OnGameStartedSuccess;
        event Action OnGameStartedFail;
        event Action<string> OnGameCategoryAdded;
        event Action<string> OnGameCategoryRemoved;
        event Action<int> OnRoundsModified;
        event Action<int> OnPlayersReadyModified;
        event Action OnEveryoneReady;
        event Action<char> OnLetterGeneratedResponse;
        event Action OnTimeStoppedReceived;

        List<string> GameCategories { get; }
        public int Rounds { get; }
        public int PlayersParticipating { get; }

        void SendStartGameRequest();
        void SendAddGameCategoryRequest(string category);
        void SendRemoveGameCategoryRequest(string category);
        void SendRoundsModifiedRequest(int rounds);
        void SendPlayerReadyRequest();
        void SendPlayerUnreadyRequest();
        void SendLetterGenerationRequest();
        void SendStopTime();
    }
}