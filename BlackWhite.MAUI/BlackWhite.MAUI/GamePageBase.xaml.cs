using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BlackWhite.Core;

using static BlackWhite.MAUI.Properties.Resources;

namespace BlackWhite.MAUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public abstract partial class GamePageBase : ContentPage
    {
        /*
         这个类是由原有的代码改造而成的
         可能含有大量问题
         */

        private Dictionary<string, View> infoElements = new Dictionary<string, View>();

        private double currentWidth;
        private double currentHeight;

        protected Blocks<ButtonBlock> blocks;

        public GamePageBase()
        {
            InitializeComponent();

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

        protected abstract void RefreshInfoArea();

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
                        scrInfo.IsVisible = false;
                        infoArea.Orientation = StackOrientation.Vertical;
                        break;
                    case 1:
                        mainStack.Orientation = StackOrientation.Vertical;
                        scrInfo.IsVisible = true;
                        infoArea.Orientation = StackOrientation.Horizontal;
                        break;
                    case -1:
                        mainStack.Orientation = StackOrientation.Horizontal;
                        scrInfo.IsVisible = true;
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

        protected bool gameAreaLoaded = false;
        private double blockSize;
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
            return ((totalSize - gameArea.Padding.HorizontalThickness - gameArea.Margin.HorizontalThickness + gameArea.RowSpacing) / blocks.Xmax) - gameArea.RowSpacing;
        }



        
    }
}