using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace Cinema
{
    class Sofa
    {
        public bool ISReserve { get; set; }
        public bool ISBuy { get; set; }
        public int Cost { get; set; }
        public Button butt { get; set; }
        public bool ISChecked { get; set; }
        public string Owner { get; set; }
        public string Mobile { get; set; }
        public string Card { get; set; }
       
        public Size sizeSofa 
        {
            get { return new Size(butt.Width, butt.Height); }
            set 
            {
                butt.Width =value.Width;
                butt.Height = value.Height;
            } 
        }
        public VerticalAlignment Vertical 
        {
            get { return butt.VerticalAlignment; }
            set
            {
                butt.VerticalAlignment =value;
            } 
        }
        public HorizontalAlignment Horizontal 
        {
            get { return butt.HorizontalAlignment; }
            set 
            {
                butt.HorizontalAlignment = value;
            }
        }
        public Brush Color 
        {
            get { return Color; }
            set 
            {
                Color = value;
                butt.Background = Color;
            } 
        }


        public Sofa()
        {
            ISReserve = false;
            ISBuy = false;
            Cost = 0;
            butt = new Button();
            Vertical = VerticalAlignment.Top;
            Horizontal = HorizontalAlignment.Left;
            sizeSofa = new Size(25, 25);
            butt.Margin = new Thickness(5); 

        }
    }
}
