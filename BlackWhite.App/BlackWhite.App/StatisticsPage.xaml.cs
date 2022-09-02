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
        List<string> modes = new List<string>();
        List<string> sizes = new List<string>();

        Cell[] cells;

        public StatisticsPage()
        {
            InitializeComponent();

            modes.Add(Properties.StatisticsPage.All);
            modes.Add(Properties.StatisticsPage.Normal);
            modes.Add(Properties.StatisticsPage.Perfect);
            modes.Add(Properties.StatisticsPage.Timed);

            sizes.Add(Properties.StatisticsPage.All);
            sizes.Add("3x3");
            sizes.Add("4x4");
            sizes.Add("5x5");
            sizes.Add("6x6");
            sizes.Add("7x7");
            sizes.Add("8x8");
            sizes.Add("9x9");

            modePicker.ItemsSource = modes;
            sizePicker.ItemsSource = sizes;

            modePicker.SelectedIndex = 0;
            sizePicker.SelectedIndex = 0;

            modePicker.SelectedIndexChanged += (object sender, EventArgs e) => { CX(); };
            sizePicker.SelectedIndexChanged += (object sender, EventArgs e) => { CX(); };

            cells = new Cell[] { resultGames, resultWins, resultWinP, resultClicks, resultClickA, resultTime, resultTimeA };
            
            CX();
        }

        private void CX()
        {
            resultSection.Clear();
            foreach(var cell in cells)
            {
                resultSection.Add(cell);
            }
            switch (modePicker.SelectedIndex)
            {
                case 0:
                    resultSection.Remove(resultWinP);
                    resultSection.Remove(resultClickA);
                    resultSection.Remove(resultTime);
                    resultSection.Remove(resultTimeA);
                    break;
                case 1:
                case 2:
                    resultSection.Remove(resultTime);
                    resultSection.Remove(resultTimeA);
                    break;
                case 3:
                    resultSection.Remove(resultClicks);
                    resultSection.Remove(resultClickA);
                    break;
                default:
                    break;
            }
            if (sizePicker.SelectedIndex == 0)
            {
                resultSection.Remove(resultWinP);
                resultSection.Remove(resultClickA);
                resultSection.Remove(resultTimeA);
            }
            int size = sizePicker.SelectedIndex + 2;
            switch (modePicker.SelectedIndex)
            {
                case 0:
                    if (size == 2)
                    {
                        resultGames.Text = Statistics.All.Games().ToString();
                        resultWins.Text = Statistics.All.Wins().ToString();
                        resultClicks.Text = Statistics.All.Clicks().ToString();
                    }
                    else
                    {
                        resultGames.Text = Statistics.All.Games(size).ToString();
                        resultWins.Text = Statistics.All.Wins(size).ToString();
                        resultClicks.Text = Statistics.All.Clicks(size).ToString();
                    }
                    break;
                case 1:
                    if (size == 2)
                    {
                        resultGames.Text = Statistics.Normal.Games().ToString();
                        resultWins.Text = Statistics.Normal.Wins().ToString();
                        resultClicks.Text = Statistics.Normal.Clicks().ToString();
                    }
                    else
                    {
                        resultGames.Text = Statistics.Normal.Games(size).ToString();
                        resultWins.Text = Statistics.Normal.Wins(size).ToString();
                        resultClicks.Text = Statistics.Normal.Clicks(size).ToString();
                        resultWinP.Text = Statistics.Normal.Games(size) == 0 ? "N/A" : Statistics.Normal.WinPercentage(size).ToString("P");
                        resultClickA.Text = Statistics.Normal.Games(size) == 0 ? "N/A" : Statistics.Normal.AverageClicks(size).ToString("F");
                    }
                    break;
                case 2:
                    if (size == 2)
                    {
                        resultGames.Text = Statistics.Perfect.Games().ToString();
                        resultWins.Text = Statistics.Perfect.Wins().ToString();
                        resultClicks.Text = Statistics.Perfect.Clicks().ToString();
                    }
                    else
                    {
                        resultGames.Text = Statistics.Perfect.Games(size).ToString();
                        resultWins.Text = Statistics.Perfect.Wins(size).ToString();
                        resultClicks.Text = Statistics.Perfect.Clicks(size).ToString();
                        resultWinP.Text = Statistics.Perfect.Games(size) == 0 ? "N/A" : Statistics.Perfect.WinPercentage(size).ToString("P");
                        resultClickA.Text = Statistics.Perfect.Games(size) == 0 ? "N/A" : Statistics.Perfect.AverageClicks(size).ToString("F");
                    }
                    break;
                case 3:
                    if (size == 2)
                    {
                        resultGames.Text = Statistics.Timed.Games().ToString();
                        resultWins.Text = Statistics.Timed.Wins().ToString();
                        resultTime.Text = TimeSpan.FromSeconds(Statistics.Timed.Time()).ToString("c");
                    }
                    else
                    {
                        resultGames.Text = Statistics.Timed.Games(size).ToString();
                        resultWins.Text = Statistics.Timed.Wins(size).ToString();
                        resultTime.Text = TimeSpan.FromSeconds(Statistics.Timed.Time(size)).ToString("c");
                        resultWinP.Text = Statistics.Timed.Games(size) == 0 ? "N/A" : Statistics.Timed.WinPercentage(size).ToString("P");
                        resultTimeA.Text = Statistics.Timed.Games(size) == 0 ? "N/A" : TimeSpan.FromSeconds(Statistics.Timed.AverageTime(size)).ToString("c");
                    }
                    break;
                default:
                    break;
            }
            
        }
    }
}