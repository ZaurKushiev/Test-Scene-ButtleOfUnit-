public interface IBattleAdapter
{
    void SendActionToServer(UnitAction action);
    void ReceiveActionFromServer(UnitAction action);
}
