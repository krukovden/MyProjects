using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Anketa
{
    public class Student :INotifyPropertyChanged
    {
        private string  name;

        public string  Name
        {
            get { return name; }
            set { name = value;
            this.RaisePropetyChanged("Name");
            }
        }
       
        private string surname;

        public string Surname
        {
            get { return surname; }
            set { surname = value;
            this.RaisePropetyChanged("Surname");
            }
        }

        private string lastname;

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value;
            this.RaisePropetyChanged("Lastname");
            }
        }

        private int age;

        public int Age
        {
            get { return age; }
            set { age = value;
            this.RaisePropetyChanged("Age");
            }
        }
        
        private string pol;

        public string Pol
        {
            get { return pol; }
            set { pol = value;
            this.RaisePropetyChanged("Pol");
            }
        }

        private Group group;


        public Group StudentGroup
        {
            get { return group; }
            set { group = value;
            this.RaisePropetyChanged("StudentGroup");
            }
        }

        public Student()
        {
            group = new Group();
        }
        public Student(string n, string l, string s,int a, string p, string g  ) 
        {
            Group tmp = new Group();
            tmp.GroupStudent = g;
            name = n;
            surname = s;
            lastname = l;
            age = a;
            pol = p;
            group = tmp;
        }
        


        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropetyChanged(string propertyName)
        {
            PropertyChangedEventHandler change = this.PropertyChanged;
            if (change != null)
                change.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
