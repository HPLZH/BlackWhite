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
    public partial class ChooseSize : ContentPage
    {
        private Settings settings;

        private string gamemode;

        public ChooseSize(string mode)
        {
            InitializeComponent();
            settings = new Settings();
            gamemode = mode;
            switch (mode)
            {
                case "standard":
                    modeName.Text = Properties.Resources.Mode_Standard;
                    modeDescription.Text = Properties.Resources.Mode_Standard_Info;
                    
                    break;
                case "perfect":
                    modeName.Text = Properties.Resources.Mode_Perfect;
                    modeDescription.Text = Properties.Resources.Mode_Perfect_Info;
                    
                    break;
            }
            for(int i = 10;i <= 30; i++)
            {
                testSelect.Items.Add(i.ToString());
            }
            test.IsVisible = settings.TestMode;
            testSelect.IsVisible = settings.TestMode;
            testWarn.IsVisible = settings.TestMode;
            testSelect.SelectedIndex = 0;
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
            string size = ((Button)sender).AutomationId;
            if (size == "?")
            {
                Navigation.PushAsync(new GamePage(new Random(), gamemode));
            }
            else if(size == "_")
            {
                Navigation.PushAsync(new GamePage(int.Parse(testSelect.SelectedItem.ToString()), gamemode));
            }
            else
            {
                Navigation.PushAsync(new GamePage(int.Parse(size), gamemode));
            }
            
        }
    }
}