namespace HollowCards
{
    public interface IDeck<T>
    {
        bool HasCards { get; }
        Card<T> Deal();
        void Shuffle();
        void NewGame();
        ICardsConfiguration<T> Config { get; }
    }
}