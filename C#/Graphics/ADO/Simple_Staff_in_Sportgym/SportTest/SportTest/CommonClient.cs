using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportTest
{
    class CommonClient
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Abonement { get; set; }
        public int? Card { get; set; }
        public DateTime? Birthday { get; set; }
        public string Sex { get; set; }
        public DateTime? Dateoff { get; set; }
        public int? Count { get; set; }
        public string Mail { get; set; }
        public string Tel { get; set; }
        public CommonClient() { }
        public CommonClient(int id, string name, string abon, int card, DateTime birth, string sex, DateTime dateoff, int count, string mail, string tel)
        {
            ID = id;
            Name = name;
            Abonement = abon;
            Card = card;
            Birthday = birth;
            Sex = sex;
            Dateoff = dateoff;
            Count = count;
            Mail = mail;
            Tel = tel;
        }
    }

    class CommonCoach
    {
        public int ID { get; set; }
        public string Name { get; set; }
       public int? Card { get; set; }
        public DateTime? Birthday { get; set; }
        public string Sex { get; set; }
        public decimal Money { get; set; }
        public string Mail { get; set; }
        public string Tel { get; set; }
        public CommonCoach() { }
        public CommonCoach(int id, string name, string abon, int card, DateTime birth, string sex, decimal money, string mail, string tel)
        {
            ID = id;
            Name = name;
           
            Card = card;
            Birthday = birth;
            Sex = sex;
            Money = money;
            Mail = mail;
            Tel = tel;
           
        }
    }

    class CoachVisit : CommonCoach
    {
        public DateTime Enter { get; set; }
        public DateTime Exit { get; set; }
        
        public CoachVisit(int id, string name, string abon, int card, DateTime birth, string sex, decimal money, string mail, string tel,DateTime enter, DateTime exit):base( id, name,abon, card,birth, sex, money, mail, tel)
            {
            Enter=enter;
            Exit = exit;
            }
        
    }
}
