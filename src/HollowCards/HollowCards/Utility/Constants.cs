using System.Collections.Generic;

namespace HollowCards.Utility
{
    public class Constants
    {
        #region Suits

        public static readonly string Clubs = "Clubs";
        public static readonly string Spades = "Spades";
        public static readonly string Hearts = "Hearts";
        public static readonly string Diamonds = "Diamonds";

        public static readonly List<string> Suits = new List<string>() { Clubs, Spades, Hearts, Diamonds };

        #endregion

        #region Suit Values

        public const string Ace = "A";
        public const string Two = "2";
        public const string Three = "3";
        public const string Four = "4";
        public const string Five = "5";
        public const string Six = "6";
        public const string Seven = "7";
        public const string Eight = "8";
        public const string Nine = "9";
        public const string Ten = "10";
        public const string Jack = "J";
        public const string Queen = "Q";
        public const string King = "K";
        public const string BigJoker = "JB";
        public const string LittleJoker = "JL";

        public static readonly List<string> SuitValues = new List<string>() {
            Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King
        };

        public static readonly List<string> SuitValuesWithJoker = new List<string>() {
            Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, LittleJoker, BigJoker
        };

        public static readonly List<string> JokerValues = new List<string>() {
            LittleJoker, BigJoker
        };

        #endregion

        #region Extended Properties

        public static readonly string SuitProperty = "Suit";
        public static readonly string FaceProperty = "Face";

        #endregion

    }
}
