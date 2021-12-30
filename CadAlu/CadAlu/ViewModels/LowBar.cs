using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CadAlu.ViewModels
{
    internal class LowBar : ViewCell
    {
        public LowBar()
        {
            var grid = new Grid();

            FlyoutItem flyoutItem = new FlyoutItem();
            flyoutItem.Title = "About";
            flyoutItem.Icon = "icon_about.png";

        }

    }
}
