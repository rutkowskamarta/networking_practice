using System;

namespace Game.Game
{
    public interface IGameManager
    {
        event Action OnGameStartedSuccess;
        event Action OnGameStartedFail;

        void SendStartGameRequest();
    }
}