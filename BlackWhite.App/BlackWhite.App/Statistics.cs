using System;
using System.Collections.Generic;
using System.Text;
using static BlackWhite.App.PropertiesIO;

namespace BlackWhite.App
{
    internal static class Statistics
    {
        const string STATISTICS = "Statistics";

        const int MAX_SIZE = 9;
        const int MIN_SIZE = 3;

        const string CLICK = "Click";
        const string GAME = "Game";
        const string WIN = "Win";

        public static void Clear()
        {

        }

        const string ALL = "All";
        public static class All
        {

        }

        const string NORMAL = "Normal";
        public static class Normal
        {
            public static ulong Clicks(int size) => Get<ulong>(STATISTICS, NORMAL, size.ToString(), CLICK);
            public static void AddClick(int size) => Set<ulong>(Clicks(size) + 1, STATISTICS, NORMAL, size.ToString(), CLICK);

            public static ulong Games(int size) => Get<ulong>(STATISTICS, NORMAL, size.ToString(), GAME);
            public static void AddGame(int size) => Set<ulong>(Games(size) + 1, STATISTICS, NORMAL, size.ToString(), GAME);

            public static ulong Wins(int size) => Get<ulong>(STATISTICS, NORMAL, size.ToString(), WIN);
            public static void AddWin(int size) => Set<ulong>(Wins(size) + 1, STATISTICS, NORMAL, size.ToString(), WIN);
        }

        const string PERFECT = "Perfect";
        public static class Perfect
        {

        }

        const string TIMED = "Timed";
        public static class Timed
        {

        }
    }
}
