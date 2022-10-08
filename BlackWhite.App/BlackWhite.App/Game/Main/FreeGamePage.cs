using BlackWhite.App.Game.Base;
using BlackWhite.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BlackWhite.App.Game.Main
{
    internal class FreeGamePage : GamePage
    {
        private FreeCore<GameButton> core;
        private Random random = new Random();

        private StackLayout toolStack = new StackLayout();
        private StackLayout generateStack = new StackLayout() { HorizontalOptions = LayoutOptions.CenterAndExpand, Orientation = StackOrientation.Vertical };
        private StackLayout clickStack = new StackLayout() { HorizontalOptions = LayoutOptions.CenterAndExpand, Orientation = StackOrientation.Vertical };

        private Label generateLabel = new Label() { Text = Properties.GamePage.Generate, TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
        private Label clickLabel = new Label() { Text = Properties.GamePage.Click, TextColor = Color.White, FontSize = 20, HorizontalTextAlignment = TextAlignment.Center };
        private Button generateButton = new Button() { Text = "..." };
        private Switch clickSwitch = new Switch() { HorizontalOptions = LayoutOptions.Center };

        public FreeGamePage(int size)
            : base()
        {
            Initialize(size);
            InitializeToolBar();
            core = new FreeCore<GameButton>(blocks);
            Show();
        }

        private void InitializeToolBar()
        {
            this.InfoContent = toolStack;
            toolStack.Children.Add(generateStack);
            toolStack.Children.Add(clickStack);
            generateStack.Children.Add(generateLabel);
            generateStack.Children.Add(generateButton);
            clickStack.Children.Add(clickLabel);
            clickStack.Children.Add(clickSwitch);
            generateButton.Clicked += GenerateButton_Clicked;
            clickSwitch.Toggled += ClickSwitch_Toggled;
        }

        private void ClickSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            if (clickSwitch.IsToggled)
            {
                clickLabel.Text = Properties.GamePage.Single;
                core.ClickMode = FreeCore<GameButton>.ClickModes.Change;
            }
            else
            {
                clickLabel.Text = Properties.GamePage.Click;
                core.ClickMode = FreeCore<GameButton>.ClickModes.Normal;
            }
        }

        private async void GenerateButton_Clicked(object sender, EventArgs e)
        {
            string result = await DisplayActionSheet(Properties.GamePage.Generate,
                Properties.GamePage.Msg_Button_Cancel,
                null, 
                Properties.GamePage.Generate_White,
                Properties.GamePage.Generate_Black,
                Properties.GamePage.Generate_GameRandomize,
                Properties.GamePage.Generate_FullRandomize,
                Properties.GamePage.Generate_MixRandomize,
                Properties.GamePage.Generate_Invert);
            if (result == Properties.GamePage.Generate_White) core.Clear(true);
            else if (result == Properties.GamePage.Generate_Black) core.Clear(false);
            else if (result == Properties.GamePage.Generate_GameRandomize) core.GameRandomize(random);
            else if (result == Properties.GamePage.Generate_FullRandomize) core.FullRandomize(random);
            else if (result == Properties.GamePage.Generate_MixRandomize) core.MixRandomize(random);
            else if (result == Properties.GamePage.Generate_Invert) core.Invert();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            toolStack.Orientation = width < height ? StackOrientation.Horizontal : StackOrientation.Vertical;
            base.OnSizeAllocated(width, height);
        }
    }
}
