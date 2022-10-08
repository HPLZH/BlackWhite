using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BlackWhite.App.Game.Base;
using BlackWhite.Core;

using Xamarin.Forms;

namespace BlackWhite.App.Game.Main
{
    internal class TimedGamePage : GamePage
    {
        private GameCore<GameButton> core;

        private int gameSize;
        private bool randomMode;
        private Random random1;

        private StackLayout infoStack = new StackLayout() { IsVisible = true };
        private StackLayout stateStack = new StackLayout() { HorizontalOptions = LayoutOptions.CenterAndExpand, Orientation = StackOrientation.Vertical };
        private StackLayout timeStack = new StackLayout() { HorizontalOptions = LayoutOptions.CenterAndExpand, Orientation = StackOrientation.Vertical };

        private Label stateLabel = new Label() { TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
        private Button newGameButton = new Button() { Text = Properties.GamePage.NewGame };
        private Label timeLabel = new Label() { Text = Properties.GamePage.Time, TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
        private Label timerLabel = new Label() { TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };

        private Thread timer;
        private uint timeCount = 0;
        private StopControler stopControler = new StopControler();

        public TimedGamePage(int size)
            : base()
        {
            randomMode = false;
            gameSize = size;
            InitializeInfoArea();
            NewGame();
        }

        public TimedGamePage(Random random)
            : base()
        {
            randomMode = true;
            random1 = random;
            InitializeInfoArea();
            NewGame();
        }

        private void InitializeInfoArea()
        {
            InfoContent = infoStack;
            infoStack.Children.Add(stateStack);
            infoStack.Children.Add(timeStack);
            stateStack.Children.Add(stateLabel);
            stateStack.Children.Add(newGameButton);
            timeStack.Children.Add(timeLabel);
            timeStack.Children.Add(timerLabel);
            newGameButton.Clicked += NewGameClicked;
        }

        private async void NewGameClicked(object sender, EventArgs e)
        {
            bool result = await DisplayAlert(Properties.GamePage.Msg_Title_NewGame,
                Properties.GamePage.Msg_Text_NewGame,
                Properties.GamePage.Msg_Button_NewGame,
                Properties.GamePage.Msg_Button_Cancel);
            if (result) NewGame();
        }

        private async void GameEndMsg()
        {
            bool result = await DisplayAlert(Properties.GamePage.Msg_Title_Finished,
                Properties.StringTools.MultiLine(
                    Properties.GamePage.Msg_Text_Finished,
                    string.Format(Properties.GamePage.Msg_Text_Time, timeCount.ToString())),
                Properties.GamePage.Msg_Button_NewGame,
                Properties.GamePage.Msg_Button_OK);
            if (result) NewGame();
        }

        private void NewGame()
        {
            if (core != null)
            {
                core.Close();
                core = null;
            }
            if (timer != null)
            {
                timer = null;
            }
            if (randomMode)
            {
                MainPage mainPage = (MainPage)((NavigationPage)Application.Current.MainPage).RootPage;
                int maxSize = SizeCalculator.GetMaxSize(SizeCalculator.GetTotalSize(mainPage.WindowSize.width, mainPage.WindowSize.height));
                if (maxSize > 9) maxSize = 9;
                gameSize = random1.Next(3, 1 + maxSize);
            }
            timerLabel.Text = "0";
            timeCount = 0;
            stopControler = new StopControler() { stop = false };
            stateLabel.Text = Properties.GamePage.State_Preparing;
            Initialize(gameSize);
            core = new GameCore<GameButton>(blocks, new Random());
            core.BlockClicked += BlockClicked;
            core.GameEnded += Ended;
            stateLabel.Text = Properties.GamePage.State_Unfinished;
            Statistics.Timed.AddGame(gameSize);
            timer = new Thread((stopCtrl) =>
            {
                StopControler stopControler1 = (StopControler)stopCtrl;
                while (true)
                {
                    Thread.Sleep(1000);
                    if (stopControler1.stop) break;
                    timeCount++;
                    Statistics.Timed.AddSecond(gameSize);
                    Dispatcher.BeginInvokeOnMainThread(() => { timerLabel.Text = timeCount.ToString(); });
                }
            });
            timer.Start(stopControler);
            Show();
        }

        private void BlockClicked(object sender, EventArgs e)
        {
            Statistics.Timed.AddClick(gameSize);
        }

        private void Ended(object sender, EventArgs e)
        {
            stopControler.stop = true;
            stateLabel.Text = Properties.GamePage.State_Finished;
            Statistics.Timed.AddWin(gameSize);
            GameEndMsg();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            infoStack.Orientation = width < height ? StackOrientation.Horizontal : StackOrientation.Vertical;
            base.OnSizeAllocated(width, height);
        }

        protected override void OnDisappearing()
        {
            stopControler.stop = true;
            stateLabel.Text = Properties.GamePage.State_Abort;
            Hide();
            base.OnDisappearing();
        }

        internal class StopControler
        {
            public bool stop;
        }
    }
}
