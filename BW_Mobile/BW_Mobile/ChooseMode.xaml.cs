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
    public partial class ChooseMode : ContentPage
    {
        public ChooseMode()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.UWP)
            {
                next.TextColor=Color.White;
            }
        }

        protected int orientation = 2; // 0=>unexpected 1=>V -1=>H
        protected override void OnSizeAllocated(double width, double height)
        {
            int orient;
            if (width / height > 1)
            {
                orient = -1;
            }
            else if (width / height <= 1)
            {
                orient = 1;
            }
            else
            {
                orient = 0;
            }
            
            if (orient != orientation)
            {
                orientation = orient;
                if(orientation == 1)
                {
                    mainStack.Orientation = StackOrientation.Vertical;
                    info.HorizontalOptions = LayoutOptions.FillAndExpand;
                    info.VerticalOptions = LayoutOptions.EndAndExpand;
                    info.HeightRequest = 152;
                }
                /*
                else
                {
                    mainStack.Orientation = StackOrientation.Horizontal;
                    info.HorizontalOptions = LayoutOptions.EndAndExpand;
                    info.VerticalOptions = LayoutOptions.FillAndExpand;
                }
                */
            }
            if (orientation == -1)
            {
                double infWidth = width * (1 - 0.618) - info.Padding.HorizontalThickness;
                if (infWidth >= 102)
                {
                    mainStack.Orientation = StackOrientation.Horizontal;
                    info.HorizontalOptions = LayoutOptions.EndAndExpand;
                    info.VerticalOptions = LayoutOptions.FillAndExpand;
                    info.WidthRequest = infWidth;
                }
                else
                {
                    mainStack.Orientation = StackOrientation.Vertical;
                    info.HorizontalOptions = LayoutOptions.FillAndExpand;
                    info.VerticalOptions = LayoutOptions.EndAndExpand;
                    //info.HeightRequest = 102;
                }
            }
            base.OnSizeAllocated(width, height); //must be called
        }

      
        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            switch(RadioButtonGroup.GetSelectedValue(select))
            {
                case "standard":
                    modeName.Text = "标准模式";
                    modeDescription.Text = "使用尽可能少的步数取得胜利";
                    next.IsEnabled = true;
                    break;
                case "perfect":
                    modeName.Text = "最优解";
                    modeDescription.Text = "使用不超过方块总数的步数取得胜利";
                    next.IsEnabled = false;
                    break;
            }
            if (orientation == 1)
            {
                info.HeightRequest = 152;
            }
        }

        private void next_Clicked(object sender, EventArgs e)
        {
            string mode = (string)RadioButtonGroup.GetSelectedValue(select);
            switch (RadioButtonGroup.GetSelectedValue(select))
            {
                case "standard":
                case "perfect":
                    Navigation.PushAsync(new ChooseSize(mode));
                    break;
            }
        }
    }
}