namespace Game.PlayerData
{
    public interface IPlayerDataManager
    {
        PlayerData PlayerData { get; }
        void SendPlayerDataUpdate();   
    }
}