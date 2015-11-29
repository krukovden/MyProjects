using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Anketa
{
    public class Group:INotifyPropertyChanged
    {
        private string groupStudents;

        public string GroupStudent
        {
            get { return groupStudents; }
            set { groupStudents = value;
            RaisePropetyChanged("GroupStudent");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropetyChanged(string propetyName)
        {
            PropertyChangedEventHandler change = this.PropertyChanged;
            if (change != null)
                change.Invoke(this, new PropertyChangedEventArgs(propetyName));
        }
    }
}
