using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SAPERmodern
{
    public class CELL
    {
        
        private Panel panelpic;
        public Panel Panelpic
        {
            get { return panelpic; }
            set { panelpic = value; }
        }

        private int sizePanel;
        public int SizePanel
        {
            get { return sizePanel; }
            set 
            { 
                sizePanel = value;
                panelpic.Size = new System.Drawing.Size(sizePanel, sizePanel);
            }
        }

        private Point locationPanel;
        public Point LocationPanel
        {
            get { return panelpic.Location; }
            set { panelpic.Location = value; }
        }

        private Image backImage;

        public Image BackImage
        {
            get { return panelpic.BackgroundImage; }
            set { panelpic.BackgroundImage = value; }
        }


        private int cellValue;

        public int CellValue
        {
            get { return cellValue; }
            set { cellValue = value; }
        }
        
        private bool flagbomba;
        public bool FlagBomba
        {
            get { return flagbomba; }
            set { flagbomba = value; }
        }
       
        private bool bomba;
        public bool Bomba
        {
            get { return bomba; }
            set { bomba = value; }
        }
        private bool open;

        public bool IsOpen
        {
            get { return open; }
            set { open = value; }
        }
        

        public CELL()
        {
            panelpic = new Panel();
            CellValue = 0;
            panelpic.BackgroundImageLayout = ImageLayout.Zoom;
            panelpic.BackColor = Color.Black;
            
        }


    }
}
