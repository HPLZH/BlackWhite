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
        const string TIME = "Time";

        static string[] MODE = { NORMAL, PERFECT, TIMED };
        public static void Clear()
        {
            for(int i = MIN_SIZE; i <= MAX_SIZE; i++)
            {
                foreach(string mode in MODE)
                {
                    Set<ulong>(0, STATISTICS, mode, i.ToString(), CLICK);
                    Set<ulong>(0, STATISTICS, mode, i.ToString(), GAME);
                    Set<ulong>(0, STATISTICS, mode, i.ToString(), WIN);
                }
                Set<ulong>(0, STATISTICS, TIMED, i.ToString(), TIME);
            }
        }

        public static class All
        {
            public static ulong Clicks() => Normal.Clicks() + Perfect.Clicks() + Timed.Clicks();
            public static ulong Games() => Normal.Games() + Perfect.Games() + Timed.Games();
            public static ulong Wins() => Normal.Wins() + Perfect.Wins() + Timed.Wins();

            public static ulong Clicks(int size) => Normal.Clicks(size) + Perfect.Clicks(size) + Timed.Clicks(size);
            public static ulong Games(int size) => Normal.Games(size) + Perfect.Games(size) + Timed.Games(size);
            public static ulong Wins(int size) => Normal.Wins(size) + Perfect.Wins(size) + Timed.Wins(size);
        }

        const string NORMAL = "Normal";
        public static class Normal
        {
            //存储
            public static ulong Clicks(int size) => Get<ulong>(STATISTICS, NORMAL, size.ToString(), CLICK);
            public static void AddClick(int size) => Set<ulong>(Clicks(size) + 1, STATISTICS, NORMAL, size.ToString(), CLICK);
            public static ulong Clicks()
            {
                ulong sum = 0;
                for (int i = MIN_SIZE; i <= MAX_SIZE; i++) sum += Clicks(i);
                return sum;
            }

            public static ulong Games(int size) => Get<ulong>(STATISTICS, NORMAL, size.ToString(), GAME);
            public static void AddGame(int size) => Set<ulong>(Games(size) + 1, STATISTICS, NORMAL, size.ToString(), GAME);
            public static ulong Games()
            {
                ulong sum = 0;
                for (int i = MIN_SIZE; i <= MAX_SIZE; i++) sum += Games(i);
                return sum;
            }

            public static ulong Wins(int size) => Get<ulong>(STATISTICS, NORMAL, size.ToString(), WIN);
            public static void AddWin(int size) => Set<ulong>(Wins(size) + 1, STATISTICS, NORMAL, size.ToString(), WIN);
            public static ulong Wins()
            {
                ulong sum = 0;
                for (int i = MIN_SIZE; i <= MAX_SIZE; i++) sum += Wins(i);
                return sum;
            }

            //计算
            public static decimal WinPercentage(int size) => (decimal)Wins(size) / (decimal)Games(size);
            public static decimal AverageClicks(int size) => (decimal)Clicks(size) / (decimal)Games(size);
        }

        const string PERFECT = "Perfect";
        public static class Perfect
        {
            //存储
            public static ulong Clicks(int size) => Get<ulong>(STATISTICS, PERFECT, size.ToString(), CLICK);
            public static void AddClick(int size) => Set<ulong>(Clicks(size) + 1, STATISTICS, PERFECT, size.ToString(), CLICK);
            public static ulong Clicks()
            {
                ulong sum = 0;
                for (int i = MIN_SIZE; i <= MAX_SIZE; i++) sum += Clicks(i);
                return sum;
            }

            public static ulong Games(int size) => Get<ulong>(STATISTICS, PERFECT, size.ToString(), GAME);
            public static void AddGame(int size) => Set<ulong>(Games(size) + 1, STATISTICS, PERFECT, size.ToString(), GAME);
            public static ulong Games()
            {
                ulong sum = 0;
                for (int i = MIN_SIZE; i <= MAX_SIZE; i++) sum += Games(i);
                return sum;
            }

            public static ulong Wins(int size) => Get<ulong>(STATISTICS, PERFECT, size.ToString(), WIN);
            public static void AddWin(int size) => Set<ulong>(Wins(size) + 1, STATISTICS, PERFECT, size.ToString(), WIN);
            public static ulong Wins()
            {
                ulong sum = 0;
                for (int i = MIN_SIZE; i <= MAX_SIZE; i++) sum += Wins(i);
                return sum;
            }

            //计算
            public static decimal WinPercentage(int size) => (decimal)Wins(size) / (decimal)Games(size);
            public static decimal AverageClicks(int size) => (decimal)Clicks(size) / (decimal)Games(size);
        }

        const string TIMED = "Timed";
        public static class Timed
        {
            //存储
            //Clicks不显示但是参与总数统计
            public static ulong Clicks(int size) => Get<ulong>(STATISTICS, TIMED, size.ToString(), CLICK);
            public static void AddClick(int size) => Set<ulong>(Clicks(size) + 1, STATISTICS, TIMED, size.ToString(), CLICK);
            public static ulong Clicks()
            {
                ulong sum = 0;
                for (int i = MIN_SIZE; i <= MAX_SIZE; i++) sum += Clicks(i);
                return sum;
            }

            //以秒为单位
            public static ulong Time(int size) => Get<ulong>(STATISTICS, TIMED, size.ToString(), TIME);
            public static void AddSecond(int size) => Set<ulong>(Time(size) + 1, STATISTICS, TIMED, size.ToString(), TIME);
            public static ulong Time()
            {
                ulong sum = 0;
                for (int i = MIN_SIZE; i <= MAX_SIZE; i++) sum += Time(i);
                return sum;
            }

            public static ulong Games(int size) => Get<ulong>(STATISTICS, TIMED, size.ToString(), GAME);
            public static void AddGame(int size) => Set<ulong>(Games(size) + 1, STATISTICS, TIMED, size.ToString(), GAME);
            public static ulong Games()
            {
                ulong sum = 0;
                for (int i = MIN_SIZE; i <= MAX_SIZE; i++) sum += Games(i);
                return sum;
            }

            public static ulong Wins(int size) => Get<ulong>(STATISTICS, TIMED, size.ToString(), WIN);
            public static void AddWin(int size) => Set<ulong>(Wins(size) + 1, STATISTICS, TIMED, size.ToString(), WIN);
            public static ulong Wins()
            {
                ulong sum = 0;
                for (int i = MIN_SIZE; i <= MAX_SIZE; i++) sum += Wins(i);
                return sum;
            }

            //计算
            public static decimal WinPercentage(int size) => (decimal)Wins(size) / (decimal)Games(size);
            public static double AverageTime(int size) => (double)Time(size) / (double)Games();

        }
    }
}
