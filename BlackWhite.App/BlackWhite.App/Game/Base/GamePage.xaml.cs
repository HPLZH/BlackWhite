using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using BlackWhite.Core;
using BlackWhite.App.Game.Base;

namespace BlackWhite.App.Game.Base
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GamePage : ContentPage
	{
		protected Blocks<GameButton> blocks;
		private int gameSize;
		private bool open = false;

		protected View InfoContent
        {
			get { return infoArea.Content; }
			set { infoArea.Content = value; }
        }

		public GamePage ()
		{
			InitializeComponent ();
		}

        protected override void OnSizeAllocated(double width, double height)
        {
			//if (width <= 0 || height <= 0) return;
			pageStack.Orientation = width < height ? StackOrientation.Vertical : StackOrientation.Horizontal;
			infoArea.Orientation = width < height ? ScrollOrientation.Horizontal : ScrollOrientation.Vertical;
            if (open)
            {
				foreach(ColumnDefinition column in blockGrid.ColumnDefinitions)
                {
					column.Width = SizeCalculator.GetBlockSizeM(SizeCalculator.GetTotalSize(width, height), gameSize);
                }
				foreach(RowDefinition row in blockGrid.RowDefinitions)
                {
					row.Height = SizeCalculator.GetBlockSizeM(SizeCalculator.GetTotalSize(width, height), gameSize);
                }
            }
            base.OnSizeAllocated(width, height);
        }

		protected void Initialize(int size)
        {
			Close();
			open = true;
			gameSize = size;
			blocks = new Blocks<GameButton>((uint)size, (uint)size);
			for (int i = 0; i < size; i++)
            {
				blockGrid.RowDefinitions.Add(new RowDefinition());
				blockGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
			foreach(GameButton button in blocks)
            {
				blockGrid.Children.Add(button, (int)button.X, (int)button.Y);
            }
        }

		protected void Show()
        {
			blockGrid.IsVisible = true;
			OnSizeAllocated(SizeCalculator.GetMainPage().WindowSize.width, SizeCalculator.GetMainPage().WindowSize.height);
			blockGrid.ForceLayout();
		}

		protected void Hide() => blockGrid.IsVisible = false;

		protected void Close()
        {
			Hide();
			open = false;
			blockGrid.Children.Clear();
			blockGrid.ColumnDefinitions.Clear();
			blockGrid.RowDefinitions.Clear();
        }
    }
}