using System;
using System.Collections.Generic;
using System.Text;

namespace BW_Mobile
{
    public enum GameResult { NoResult, Win, Lose, Free, Initialized, Starting }
    public enum SummonMode { RandomClick, RandomValue, Clear }
    public enum ClickMode { Default, Change, NoChange }

    public class GameCore
    {
        public int GameSize { get; protected set; }
        public SummonMode SummonMode { get; protected set; }
        public ClickMode ClickMode {  get; protected set;}
        public int Seed { get; protected set; }
        public bool UsingSeed { get; protected set; }
        public GameResult Result { get; protected set; }
        public int Count { get; protected set; }
        public BWButton[,] Buttons { get; protected set; }

        public event EventHandler ButtonClicked;
        public event EventHandler GameEnd;
        
        public GameCore(int size)
        {
            if(size < 3)
            {
                throw new ArgumentException("The size is too small.", "size");
            }
            GameSize = size;
            //SummonMode = summonMode;
            //UsingSeed = false;
            Result = GameResult.Initialized;
            ClickMode = ClickMode.NoChange;
            Count = 0;
            Buttons = new BWButton[size, size];
            for (int ix = 0; ix < size; ix++)
            {
                for (int iy = 0; iy < size; iy++)
                {
                    Buttons[ix,iy] = new BWButton(ix, iy, Button_Clicked);
                }
            }
        }

        private Random random;
        private void Summon(SummonMode summonMode)
        {
            Clear();
            if (summonMode == SummonMode.Clear)
            {
                return;
            }
            foreach (BWButton button in Buttons)
            {
                if (random.Next(2) == 0) 
                {
                    if (summonMode == SummonMode.RandomClick)
                    {
                        Click(button.LocationX, button.LocationY);
                    }
                    if (summonMode == SummonMode.RandomValue)
                    {
                        button.ChangeColor();
                    }
                }
                
            }
        }

        public void Start(SummonMode summonMode)
        {
            random = new Random();
            UsingSeed = false;
            Result = GameResult.Starting;
            Summon(summonMode);
            Count = 0;
            Result = GameResult.NoResult;
            ClickMode = ClickMode.Default;
        }

        public void Start(SummonMode summonMode, int seed)
        {
            random = new Random(seed);
            UsingSeed = true;
            Seed = seed;
            Result = GameResult.Starting;
            Summon(summonMode);
            Count = 0;
            Result = GameResult.NoResult;
            ClickMode = ClickMode.Default;
        }

        public void Clear()
        {
            if (Result == GameResult.NoResult)
            {
                throw new Exception("There is no result. You can not clear now.");
            }
            foreach (BWButton button in Buttons)
            {
                button.Color = false;
            }
        }

        private void CheckWin()
        {
            if (Result == GameResult.NoResult)
            {
                bool standardColor = Buttons[0, 0].Color;
                bool result = true;
                foreach (BWButton button in Buttons)
                {
                    result = result && (standardColor == button.Color);
                }
                if (result)
                {
                    Result = GameResult.Win;
                    GameEnd.Invoke(this, new EventArgs());
                }
            }
        }

        public void Lose()
        {
            if (Result == GameResult.Initialized)
            {
                throw new Exception("Game has not started.");
            }
            if (Result == GameResult.Free)
            {
                throw new Exception("This is FreeMode.");
            }
            Result = GameResult.Lose;
            GameEnd.Invoke(this, new EventArgs());
        }

        public void FreeMode()
        {
            if (Result == GameResult.Initialized)
            {
                throw new Exception("Game has not started.");
            }
            Result = GameResult.Free;
        }

        public void ChangeClickMode(ClickMode clickMode)
        {
            if (Result == GameResult.Initialized)
            {
                throw new Exception("Game has not started.");
            }
            if (Result != GameResult.Free && clickMode == ClickMode.Change)
            {
                throw new ArgumentException("This ClickMode is not allowed without FreeMode.", "clickMode");
            }
            ClickMode = clickMode;
        }

        public void Click(int X, int Y)
        {
            if (Result == GameResult.Initialized)
            {
                throw new Exception("Game has not started.");
            }
            if (X < 0 || Y < 0 || X >= GameSize || Y >= GameSize)
            {
                throw new ArgumentException("This button is not exist.", "X or Y");
            }
            Buttons[X, Y].ChangeColor();
            if (X != 0)
            {
                Buttons[X - 1, Y].ChangeColor();
            }
            if (Y != 0)
            {
                Buttons[X, Y - 1].ChangeColor();
            }
            if (X != GameSize - 1)
            {
                Buttons[X + 1, Y].ChangeColor();
            }
            if (Y != GameSize - 1)
            {
                Buttons[X, Y + 1].ChangeColor();
            }
            if (Result == GameResult.NoResult)
            {
                Count++;
            }
            CheckWin();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            BWButton senderButton = (BWButton)sender;
            switch (ClickMode)
            {
                case ClickMode.Default:
                    Click(senderButton.LocationX, senderButton.LocationY);
                    break;
                case ClickMode.Change:
                    senderButton.ChangeColor();
                    break;
                default:
                    break;
            }
            //must be called
            ButtonClicked.Invoke(sender, e);
        }
    }
}
