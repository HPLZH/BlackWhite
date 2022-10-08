using System;
using System.Collections.Generic;
using System.Text;
using BlackWhite.App.Game.Base;
using BlackWhite.Core;

using Xamarin.Forms;

namespace BlackWhite.App.Game.Main
{
    internal class PerfectGamePage : GamePage
    {
        private PerfectCore<GameButton> core;

        private int gameSize;
        private bool randomMode;
        private Random random1;

        private StackLayout infoStack = new StackLayout() { IsVisible = true };
        private StackLayout stateStack = new StackLayout() { HorizontalOptions = LayoutOptions.CenterAndExpand, Orientation = StackOrientation.Vertical };
        private StackLayout countStack = new StackLayout() { HorizontalOptions = LayoutOptions.CenterAndExpand, Orientation = StackOrientation.Vertical };
        private StackLayout remainStack = new StackLayout() { HorizontalOptions = LayoutOptions.CenterAndExpand, Orientation = StackOrientation.Vertical };

        private Label stateLabel = new Label() { TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
        private Button newGameButton = new Button() { Text = Properties.GamePage.NewGame };
        private Label countLabel = new Label() { Text = Properties.GamePage.Count, TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
        private Label counter = new Label() { TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
        private Label remainLabel = new Label() { Text = Properties.GamePage.Remaining, TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
        private Label remainCounter = new Label() { TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };

        public PerfectGamePage(int size)
            : base()
        {
            randomMode = false;
            gameSize = size;
            InitializeInfoArea();
            NewGame();
        }

        public PerfectGamePage(Random random)
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
            infoStack.Children.Add(countStack);
            infoStack.Children.Add(remainStack);
            stateStack.Children.Add(stateLabel);
            stateStack.Children.Add(newGameButton);
            countStack.Children.Add(countLabel);
            countStack.Children.Add(counter);
            remainStack.Children.Add(remainLabel);
            remainStack.Children.Add(remainCounter);
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
                    string.Format(Properties.GamePage.Msg_Text_Count, core.Count.ToString()),
                    string.Format(Properties.GamePage.Msg_Text_Remaining, core.Remaining.ToString())),
                Properties.GamePage.Msg_Button_NewGame,
                Properties.GamePage.Msg_Button_OK);
            if (result) NewGame();
        }

        private async void GameStopMsg(object sender, EventArgs e)
        {
            stateLabel.Text = Properties.GamePage.State_Fail;
            bool result = await DisplayAlert(Properties.GamePage.Msg_Title_Fail,
                Properties.GamePage.Msg_Text_Fail,
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
            if (randomMode)
            {
                MainPage mainPage = (MainPage)((NavigationPage)Application.Current.MainPage).RootPage;
                int maxSize = SizeCalculator.GetMaxSize(SizeCalculator.GetTotalSize(mainPage.WindowSize.width, mainPage.WindowSize.height));
                if (maxSize > 9) maxSize = 9;
                gameSize = random1.Next(3, 1 + maxSize);
            }
            counter.Text = "0";
            stateLabel.Text = Properties.GamePage.State_Preparing;
            Initialize(gameSize);
            core = new PerfectCore<GameButton>(blocks, new Random());
            core.BlockClicked += BlockClicked;
            core.GameEnded += Ended;
            core.GameStopped += GameStopMsg;
            stateLabel.Text = Properties.GamePage.State_Unfinished;
            remainCounter.Text = core.Remaining.ToString();
            Statistics.Perfect.AddGame(gameSize);
            Show();
        }

        private void BlockClicked(object sender, EventArgs e)
        {
            counter.Text = core.Count.ToString();
            remainCounter.Text = core.Remaining.ToString();
            Statistics.Perfect.AddClick(gameSize);
        }

        private void Ended(object sender, EventArgs e)
        {
            stateLabel.Text = Properties.GamePage.State_Finished;
            Statistics.Perfect.AddWin(gameSize);
            GameEndMsg();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            infoStack.Orientation = width < height ? StackOrientation.Horizontal : StackOrientation.Vertical;
            base.OnSizeAllocated(width, height);
        }
    }
}
