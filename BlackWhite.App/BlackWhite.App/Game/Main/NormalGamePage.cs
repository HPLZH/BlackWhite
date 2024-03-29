﻿using System;
using System.Collections.Generic;
using System.Text;
using BlackWhite.App.Game.Base;
using BlackWhite.Core;

using Xamarin.Forms;

namespace BlackWhite.App.Game.Main
{
    internal class NormalGamePage : GamePage
    {
        private GameCore<GameButton> core;

        private int gameSize;
        private bool randomMode;
        private Random random1;

        private StackLayout infoStack = new StackLayout() { IsVisible = true };
        private StackLayout stateStack = new StackLayout() { HorizontalOptions = LayoutOptions.CenterAndExpand, Orientation = StackOrientation.Vertical };
        private StackLayout countStack = new StackLayout() { HorizontalOptions = LayoutOptions.CenterAndExpand, Orientation = StackOrientation.Vertical };

        private Label stateLabel = new Label() { TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
        private Button newGameButton = new Button() { Text = Properties.GamePage.NewGame };
        private Label countLabel = new Label() { Text = Properties.GamePage.Count, TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
        private Label counter = new Label() { TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };

        public NormalGamePage(int size)
            : base()
        {
            randomMode = false;
            gameSize = size;
            InitializeInfoArea();
            NewGame();
        }

        public NormalGamePage(Random random)
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
            stateStack.Children.Add(stateLabel);
            stateStack.Children.Add(newGameButton);
            countStack.Children.Add(countLabel);
            countStack.Children.Add(counter);
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
                    string.Format(Properties.GamePage.Msg_Text_Count, core.Count.ToString())),
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
            core = new GameCore<GameButton>(blocks, new Random());
            core.BlockClicked += BlockClicked;
            core.GameEnded += Ended;
            stateLabel.Text = Properties.GamePage.State_Unfinished;
            Statistics.Normal.AddGame(gameSize);
            Show();
        }

        private void BlockClicked(object sender, EventArgs e)
        {
            counter.Text = core.Count.ToString();
            Statistics.Normal.AddClick(gameSize);
        }

        private void Ended(object sender, EventArgs e)
        {
            stateLabel.Text = Properties.GamePage.State_Finished;
            Statistics.Normal.AddWin(gameSize);
            GameEndMsg();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            infoStack.Orientation = width < height ? StackOrientation.Horizontal : StackOrientation.Vertical;
            base.OnSizeAllocated(width, height);
        }
    }
}
