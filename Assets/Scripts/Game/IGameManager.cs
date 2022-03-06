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

        List<string> GameCategories { get; }

        void SendStartGameRequest();
        void SendAddGameCategoryRequest(string category);
        void SendRemoveGameCategoryRequest(string category);
    }
}