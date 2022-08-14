using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlackWhite.App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
    {
        public StatisticsPage()
        {
            InitializeComponent();
            n_c.Text = Statistics.Normal.Clicks().ToString();
            n_g.Text = Statistics.Normal.Games().ToString();
            n_w.Text = Statistics.Normal.Wins().ToString();
        }
    }
}