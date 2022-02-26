namespace Game.Player
{
    public interface IPlayerDataManager
    {
        PlayerData PlayerData { get; }
        void SendPlayerDataUpdate();   
    }
}