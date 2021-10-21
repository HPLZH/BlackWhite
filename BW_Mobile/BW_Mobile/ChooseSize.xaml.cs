﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BW_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseSize : ContentPage
    {
        private string gamemode;

        public ChooseSize(string mode)
        {
            InitializeComponent();
            gamemode = mode;
            switch (mode)
            {
                case "standard":
                    modeName.Text = "标准模式";
                    modeDescription.Text = "使用尽可能少的步数取得胜利";
                    
                    break;
                case "perfect":
                    modeName.Text = "最优解";
                    modeDescription.Text = "使用不超过方块总数的步数取得胜利";
                    
                    break;
            }
            
        }

        protected int orientation; // 0=>unexpected 1=>V -1=>H
        protected override void OnSizeAllocated(double width, double height)
        {
            int orient;
            if (width / height > 1)
            {
                orient = -1;
            }
            else if (width / height < 1)
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
                if (Device.Idiom != TargetIdiom.Desktop)
                {
                    switch (orientation)
                    {
                        case 1:
                        case 0:
                            tips.IsVisible = false;
                            break;
                        case -1:
                            tips.IsVisible = true;
                            break;
                    }
                }
            }
            base.OnSizeAllocated(width, height); //must be called
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //以后要改
            string size = ((Button)sender).Text.Substring(5, 1);
            if (size == "?")
            {
                Navigation.PushAsync(new GamePage(new Random().Next(3,10), gamemode));
            }
            else
            {
                Navigation.PushAsync(new GamePage(int.Parse(size), gamemode));
            }
            
        }
    }
}