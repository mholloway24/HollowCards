namespace HollowCards
{
    public interface IDeck
    {
        bool HasCards { get; }
        Card Deal();
        void Shuffle();
        void NewGame();
    }
}