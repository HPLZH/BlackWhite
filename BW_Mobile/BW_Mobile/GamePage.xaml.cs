using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BW_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        public const string STANDARD = "standard";
        public const string PERFECT = "perfect";
        
        private GameCore core;

        private bool isRandom = false;
        private Random random;

        private string gamemode;
        //private bool randSize = false;
        private int initSeed;
        private bool useSeed;
        //private int core.GameSize;
        private bool gameAreaLoaded = false;
        private Dictionary<string, View> infoElements = new Dictionary<string, View>();

        //private int gameState = 0; // 0 playing; 1 win; -1 lose
        //private int counter = 0;

        private double blockSize;
        //private BWButton[,] gameButtons;

        private double currentWidth;
        private double currentHeight;

        public GamePage(int size, string gameMode)
        {
            InitializeComponent();
            core = new GameCore(size);
            core.ButtonClicked += Button_Click;
            core.GameEnd += Game_End;
            gamemode = gameMode;
            useSeed = false;
            StartGame();
            LoadInfoArea();
            LoadGameArea();
        }

        public GamePage(int size, string gameMode, int seed)
        {
            InitializeComponent();
            core = new GameCore(size);
            core.ButtonClicked += Button_Click;
            core.GameEnd += Game_End;
            gamemode = gameMode;
            useSeed = true;
            initSeed = seed;
            StartGame();
            //useSeed = false;
            initSeed = new Random().Next();
            LoadInfoArea();
            LoadGameArea();
        }

        public GamePage(Random randomSize, string gameMode)
        {
            InitializeComponent();
            this.random = randomSize;
            isRandom = true;

            /*
            core = new GameCore(this.random.Next(3,9));
            core.ButtonClicked += Button_Click;
            core.GameEnd += Game_End;
            */

            gamemode = gameMode;
            useSeed = false;
            StartGame();
            
            LoadInfoArea();
            //LoadGameArea();
            
        }

        private void StartGame()
        {
            if (isRandom)
            {
                core = new GameCore(this.random.Next(3, 10));
                core.ButtonClicked += Button_Click;
                core.GameEnd += Game_End;
                gameAreaLoaded = false;
            }

            switch (gamemode)
            {
                default:
                    if (useSeed)
                    {
                        core.Start(SummonMode.RandomClick, initSeed);
                    }
                    else
                    {
                        core.Start(SummonMode.RandomClick);
                    }
                    break;
            }

            if (isRandom)
            {
                //LoadInfoArea();
                LoadGameArea();
            }
        }

        private void LoadGameArea()
        {
            gameArea.Children.Clear();
            gameArea.ColumnDefinitions.Clear();
            gameArea.RowDefinitions.Clear();
            //gameArea = new Grid();
            for (int i = 0; i < core.GameSize; i++)
            {
                gameArea.ColumnDefinitions.Add(new ColumnDefinition());
                gameArea.RowDefinitions.Add(new RowDefinition());
            }
            //gameButtons = new BWButton[core.GameSize, core.GameSize];
            for (int ix = 0; ix < core.GameSize; ix++)
            {
                for (int iy = 0; iy < core.GameSize; iy++)
                {
                    //gameButtons[ix,iy] = new BWButton(ix, iy, GameButton_Clicked);
                    gameArea.Children.Add(core.Buttons[ix,iy], ix, iy);
                }
            }
            gameAreaLoaded = true;
            TryChangeBlockSize();
        }

        private void LoadInfoArea()
        {
            switch (gamemode)
            {
                case STANDARD:
                    //state
                    infoElements.Add("stateStack", new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand });
                    infoElements.Add("state", new Label { TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center });
                    infoElements.Add("newGame", new Button { TextColor = ((Device.RuntimePlatform == Device.UWP) ? Color.White : Color.Default), Text = "开始新游戏" });
                    ((Button)infoElements["newGame"]).Clicked += NewGame;
                    infoArea.Children.Add(infoElements["stateStack"]);
                    ((StackLayout)infoElements["stateStack"]).Children.Add(infoElements["state"]);
                    ((StackLayout)infoElements["stateStack"]).Children.Add(infoElements["newGame"]);
                    //count
                    infoElements.Add("countStack", new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand });
                    infoElements.Add("countLabel", new Label { TextColor = Color.White, FontSize = 20, Text = "已用步数", HorizontalTextAlignment = TextAlignment.Center });
                    infoElements.Add("standardCount", new Label { TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center });
                    infoArea.Children.Add(infoElements["countStack"]);
                    ((StackLayout)infoElements["countStack"]).Children.Add(infoElements["countLabel"]);
                    ((StackLayout)infoElements["countStack"]).Children.Add(infoElements["standardCount"]);
                    break;
                case PERFECT:
                    //state
                    infoElements.Add("stateStack", new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand });
                    infoElements.Add("state", new Label { TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center });
                    infoElements.Add("newGame", new Button { TextColor = ((Device.RuntimePlatform == Device.UWP) ? Color.White : Color.Default), Text = "开始新游戏" });
                    ((Button)infoElements["newGame"]).Clicked += NewGame;
                    infoArea.Children.Add(infoElements["stateStack"]);
                    ((StackLayout)infoElements["stateStack"]).Children.Add(infoElements["state"]);
                    ((StackLayout)infoElements["stateStack"]).Children.Add(infoElements["newGame"]);
                    //count
                    infoElements.Add("countStack", new StackLayout { HorizontalOptions = LayoutOptions.CenterAndExpand });
                    infoElements.Add("countLabel", new Label { TextColor = Color.White, FontSize = 20, Text = "剩余步数", HorizontalTextAlignment = TextAlignment.Center });
                    infoElements.Add("perfectCount", new Label { TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center });
                    infoArea.Children.Add(infoElements["countStack"]);
                    ((StackLayout)infoElements["countStack"]).Children.Add(infoElements["countLabel"]);
                    ((StackLayout)infoElements["countStack"]).Children.Add(infoElements["perfectCount"]);
                    break;
                default:
                    throw new ArgumentException("This gamemode is not supported.", "gameMode");
                    //break;
            }
            if (useSeed)
            {
                //要改
                infoElements.Add("seed", new Label { Text = $"种子: {core.Seed}" });
                infoArea.Children.Add(infoElements["seed"]);
            }
            //RefreshInfoArea();
        }

        private void ChangeBlockSize(double size)
        {
            foreach (RowDefinition row in gameArea.RowDefinitions)
            {
                row.Height = size;
            }
            foreach (ColumnDefinition column in gameArea.ColumnDefinitions)
            {
                column.Width = size;
            }
        }

        private void RefreshInfoArea()
        {
            switch (gamemode)
            {
                case "standard":
                    if (orientation != 0)
                    {
                        Title = "";
                        ((Label)infoElements["state"]).Text = (core.Result == GameResult.Win) ? "你胜利了" : "未完成";
                        ((Label)infoElements["standardCount"]).Text = $"{core.Count}";
                    }
                    else
                    {
                        Title = $"{((core.Result == GameResult.Win) ? "胜利! " : "")}已用步数: {core.Count}";
                    }
                    break;
                case "perfect":
                    if (orientation != 0)
                    {
                        Title = "";
                        ((Label)infoElements["state"]).Text = (core.Result == GameResult.Win) ? "你胜利了" : ((core.Result == GameResult.Lose) ? "你失败了" : "未完成");
                        ((Label)infoElements["perfectCount"]).Text = $"{core.GameSize * core.GameSize - core.Count}";
                    }
                    else
                    {
                        Title = $"{((core.Result == GameResult.Win) ? "胜利! " : ((core.Result == GameResult.Lose) ? "失败! " : ""))}剩余步数: {core.GameSize * core.GameSize - core.Count}";
                    }
                    break;
                default:
                    break;
            }
        }

        protected int orientation = 2; // 0=>unexpected 1=>V -1=>H
        protected override void OnSizeAllocated(double width, double height)
        {
            int orient;
            if (width - height > 150)
            {
                orient = -1;
            }
            else if (height - width > 150)
            {
                orient = 1;
            }
            else
            {
                orient = 0;
            }
            if(orient != orientation)
            {
                orientation = orient;
                switch (orient)
                {
                    case 0:
                        mainStack.Orientation = StackOrientation.Horizontal;
                        info.IsVisible = false;
                        infoArea.Orientation = StackOrientation.Vertical;
                        break;
                    case 1:
                        mainStack.Orientation = StackOrientation.Vertical;
                        info.IsVisible = true;
                        infoArea.Orientation = StackOrientation.Horizontal;
                        break;
                    case -1:
                        mainStack.Orientation = StackOrientation.Horizontal;
                        info.IsVisible = true;
                        infoArea.Orientation = StackOrientation.Vertical;
                        break;
                }
                //sizeWarn.IsVisible = !info.IsVisible;
            }
            double infoLength;
            switch (orient)
            {
                case 1:
                    infoLength = (height - width) / 2 - info.Padding.VerticalThickness;
                    info.HeightRequest = (infoLength > 102) ? infoLength : 102;
                    break;
                case -1:
                    infoLength = (width - height) / 2 - info.Padding.HorizontalThickness;
                    info.WidthRequest = (infoLength > 102) ? infoLength : 102;
                    break;
            }
            currentHeight = height;
            currentWidth = width;
            TryChangeBlockSize();
            RefreshInfoArea();

            base.OnSizeAllocated(width, height); //must be called
        }

        private void TryChangeBlockSize()
        {
            if (gameAreaLoaded)
            {
                blockSize = (currentWidth > currentHeight) ? GetBlockSize(currentHeight) : GetBlockSize(currentWidth);
                if (blockSize < 30)
                {
                    gameLayout.IsVisible = false;
                    sizeWarn.IsVisible = true;
                }
                else
                {
                    ChangeBlockSize(blockSize);
                    gameLayout.IsVisible = true;
                    sizeWarn.IsVisible = false;
                }
            }
        }

        private double GetBlockSize(double totalSize)
        {
            return ((totalSize - gameArea.Padding.HorizontalThickness - gameArea.Margin.HorizontalThickness + gameArea.RowSpacing) / core.GameSize) - gameArea.RowSpacing;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            switch (gamemode){
                case STANDARD:
                    break;
                case PERFECT:
                    if(core.Result == GameResult.NoResult && core.GameSize * core.GameSize - core.Count <= 0)
                    {
                        core.Lose();
                    }
                    break;
                default:
                    break;
            }
            RefreshInfoArea();
        }

        private async void Game_End(object sender, EventArgs e)
        {
            RefreshInfoArea();
            if (core.Result == GameResult.Win)
            {
                bool response = await DisplayAlert("游戏结束", $"你胜利了!{Environment.NewLine}总共用了{core.Count}步{Environment.NewLine}还剩{core.GameSize * core.GameSize - core.Count}步", "再来一局", "确定");
                if (response)
                {
                    StartGame();
                    RefreshInfoArea();
                }
            }
            else if(core.Result == GameResult.Lose)
            {
                await DisplayAlert("游戏结束", $"你失败了{Environment.NewLine}你还可以尝试继续完成游戏", "确定");
            }
        }
        
        private async void NewGame(object sender, EventArgs e)
        {
            bool response = await DisplayAlert("开始新游戏", "你确定要开始新游戏吗?", "是", "否");
            if (response)
            {
                StartGame();
                RefreshInfoArea();
            }
        }
    }
}