using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static readonly string Ace = "A";
        public static readonly string Two = "2";
        public static readonly string Three = "3";
        public static readonly string Four = "4";
        public static readonly string Five = "5";
        public static readonly string Six = "6";
        public static readonly string Seven = "7";
        public static readonly string Eight = "8";
        public static readonly string Nine = "9";
        public static readonly string Ten = "10";
        public static readonly string Jack = "J";
        public static readonly string Queen = "Q";
        public static readonly string King = "K";

        public static readonly List<string> SuitValues = new List<string>() {
            Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King
        };

        #endregion

        #region Extended Properties
        
        public static readonly string SuitProperty = "Suit";
        public static readonly string FaceProperty = "Face";

        #endregion

    }
}
