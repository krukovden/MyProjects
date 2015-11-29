using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SportTest
{
    class workDataFunk
    {
        //public DataStepLinqDataContext x;
        public DataLINQDataContext x;
        static public Dictionary<int, DateTime> lsCoaches=new Dictionary<int, DateTime>();
        static public Dictionary<int, DateTime> lsClient = new Dictionary<int, DateTime>();

        public workDataFunk()
        {
            //x = new DataStepLinqDataContext();
            x = new DataLINQDataContext();
    

        }
        // Funk-show people in sportGum n--numbercard
        public void Enter(int n)
        {
            try
            {
                var Clients = from t in x.Users
                              join f in x.Client
                              on t.id equals f.id_user
                              where t.number_card == n
                              select new { id = t.id, name = t.name, abonement = f.id_abonement, card = t.number_card, birthday = t.birthday, Sex = t.gender, limit = f.limit_date, count = f.counts, time_Enter = DateTime.Now };

                if (Clients.Count() == 0)
                {
                    var Clients2 = from t in x.Users
                                   join f in x.Coach
                                   on t.id equals f.id_user
                                   where t.number_card == n
                                   select new { id = t.id, name = t.name, card = t.number_card, birthday = t.birthday, Sex = t.gender, time_Enter = DateTime.Now };
                    if (Clients2.Count() == 0)
                    {
                        MessageBox.Show("This number card noy exist!");
                    }
                    else
                    {
                        if (lsCoaches.ContainsKey(Clients2.First().id))
                        {
                            if (Search.action == "visit")
                            {
                                MessageBox.Show("Он уже в зале");
                                return;
                            }
                            x.Visiting.InsertOnSubmit(new Visiting { dateEnter = lsCoaches[Clients2.First().id], deteExit = DateTime.Now, id_user = Clients2.First().id });
                            x.SubmitChanges();
                            lsCoaches.Remove(Clients2.First().id);
                        }
                        else
                        {
                            if (Search.action == "exit")
                            {
                                MessageBox.Show("Его не было в спорт зале");
                            }
                            else
                            lsCoaches.Add(Clients2.First().id, Clients2.First().time_Enter);
                        }

                    }
                }
                else
                {
                    if (lsClient.ContainsKey(Clients.First().id))
                    {
                        if (Search.action == "visit")
                        {
                            MessageBox.Show("Он уже в зале");
                            return;
                        }
                        x.Visiting.InsertOnSubmit(new Visiting { dateEnter = lsClient[Clients.First().id], deteExit = DateTime.Now, id_user = Clients.First().id });
                        x.SubmitChanges();
                        lsClient.Remove(Clients.First().id);
                    }
                    else
                    {
                        if (Search.action == "exit")
                        {
                            MessageBox.Show("Его не было в спорт зале");
                        }
                        else
                            lsClient.Add(Clients.First().id, Clients.First().time_Enter);
                    }
                        

                }


            }
            catch (Exception ex)
            {
            }
        }
        public List<CommonClient> infoClients()
        {
            List<CommonClient> tt = new List<CommonClient>();
            var Clients = from t in x.Users
                          join f in x.Client
                          on t.id equals f.id_user
                          select new { id = t.id, name = t.name, abonement = f.id_abonement, card = t.number_card, birthday = t.birthday, Sex = t.gender, limit = f.limit_date, count = f.counts, Mail=t.email, Tel=t.tel };
            var Common = from t in Clients
                         join f in x.Abonement
                         on t.abonement equals f.id
                         select new CommonClient { ID = t.id, Name = t.name, Abonement = f.name, Card = t.card, Birthday = t.birthday, Sex = t.Sex, Dateoff = t.limit, Count = t.count, Mail = t.Mail, Tel = t.Tel };
            var result = new List<CommonClient>();
            foreach (var item in Common)
            {
                result.Add(item);
            }
            return result;

        }

        //Funk- add user
        public bool AddPeople(string FIO,DateTime DR,string pol, string mail, string tell)
        {
            try
            {
                var card = (from t in x.Users
                            select t.number_card).Max();
                var check = from t in x.Users
                            where t.name.CompareTo(FIO) == 0 && t.gender.CompareTo(pol) == 0 && t.email.CompareTo(mail) == 0 && t.tel.CompareTo(tell) == 0
                            select new { name = t.name, gen = t.gender, mail = t.email };
                if (check == null)
                {
                    MessageBox.Show("Этот человек уже есть в базе");
                    return false;
                }
                x.Users.InsertOnSubmit(new Users { name = FIO, birthday = DR, gender = pol, email = mail, tel = tell, number_card = card + 1 });
                x.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        // add abon
        public bool AddAbonInfo(string nameAbon, int count, DateTime limit_dates, int card, decimal prices )
        {
            try
            {
                var intAbon = from t in x.Abonement
                              where t.name.CompareTo(nameAbon) == 0
                              select t.id;
                var intId = from t in x.Users
                            where t.number_card == card
                            select t.id;

                x.Client.InsertOnSubmit(new Client { id_abonement = intAbon.First(), limit_date = limit_dates, id_user = intId.First(), counts = count });
                x.History_buy.InsertOnSubmit(new History_buy { price = prices, dates = DateTime.Now, id_abonement = intAbon.First(), id_user = intId.First() });
                x.SubmitChanges();
                return true;
            }
            catch
            {
                MessageBox.Show("Проблемы с абониментом");
                return false;
            }
        }

        //add info coach
        public bool AddInfoCoach(decimal money, int card)
        {
            try
            {
                var intId = from t in x.Users
                            join f in x.Coach
                            on t.id equals f.id_user
                            where t.number_card == card
                            select t;

                if (intId.Count() < 1)
                {
                    x.Coach.InsertOnSubmit(new Coach { lave = money, id_user = GetId(card) });
                    x.SubmitChanges();
                    return true;
                }
                
                var coachData = from t in x.Coach
                                join f in x.Users
                                on t.id_user equals f.id
                                where f.number_card == card
                                select t;

                foreach (Coach item in coachData)
                {
                    item.lave = money;
                }
                x.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }

        public List<string> infoAbon()
        {
            List<string> tt = new List<string>();
            var info = from t in x.Abonement
                       select t.name;
            
            foreach (var item in info)
            {
               tt.Add(item);
            }
            return tt;

        }

        public string GetName(int number_card)
        {
            var rez = from t in x.Users
                      where t.number_card == number_card
                      select t.name;
            if (rez.Count()<=0)
                return null;
            return rez.First();
        }
        public int GetCard(string name)
        {
            var rez = from t in x.Users
                      where t.name.CompareTo(name) == 0
                      select t.number_card;
            return (int) rez.First();
        }
        public int GetId(int number_card)
        {
            var rez = from t in x.Users
                      where t.number_card == number_card
                      select t.id;
            if (rez.Count()<=0)
                return 0;
            return  rez.First();
        }

        public List<CommonClient> ClientProplacheno()
        {
            
            var info = from t in infoClients()
                       where DateTime.Now.CompareTo( t.Dateoff)>=0
                       select new CommonClient { ID = t.ID, Name = t.Name, Abonement = t.Abonement, Card = t.Card, Birthday = t.Birthday, Sex = t.Sex, Dateoff = t.Dateoff, Count = t.Count, Mail=t.Mail, Tel=t.Tel };

            var result = new List<CommonClient>();
            foreach (var item in info)
            {
                result.Add(item);
            }
            return result;
        }

        public List<CommonCoach> infoCoach()
        {
            
            
            var Clients = from t in x.Users
                          join f in x.Coach
                          on t.id equals f.id_user
                          select new CommonCoach { ID = t.id, Name = t.name, Card = t.number_card, Birthday = t.birthday, Sex = t.gender, Money = (decimal)f.lave, Mail = t.email, Tel = t.tel };


            var result = new List<CommonCoach>();
            foreach (var item in Clients)
            {
                result.Add(item);
            }
            return result;

        }

        public CommonClient SearchDataClien { get; set; }
        public CommonCoach SearchDataCoach { get; set; }
    
        public bool GetInfo(int number_card)
        {
            SearchDataCoach=null;
            SearchDataClien = null;
            try{
            var co = from t in x.Users
                      join f in x.Coach
                      on t.id equals f.id_user 
                      where t.number_card == number_card
                      select t;

            var cli = from t in x.Users
                      join f in x.Client
                      on t.id equals f.id_user
                      where t.number_card == number_card
                      select t;

            if (co.Count() > 0)
            {
                var rez= from t in infoCoach()
                         where t.Card==number_card
                         select t;
                SearchDataCoach=rez.First();
            }
            else if (cli.Count() > 0)
            {
                var rez= from t in infoClients()
                         where t.Card==number_card
                         select t;
                SearchDataClien=rez.First();
            }
            else
                MessageBox.Show("Такого человека не зарегестрировано в спорт-зале");
              

            return true;

            
        }
            catch
            {
                return false;
            }
        }
        public bool GetInfo(string name)
        {
            SearchDataCoach = null;
            SearchDataClien = null;
            try
            {
                var co = from t in x.Users
                         join f in x.Coach
                         on t.id equals f.id_user
                         where t.name.CompareTo(name) == 0
                         select t;

                var cli = from t in x.Users
                          join f in x.Client
                          on t.id equals f.id_user
                          where t.name.CompareTo(name) == 0
                          select t;

                if (co.Count() > 0)
                {
                    var rez = from t in infoCoach()
                              where t.Name.CompareTo(name) == 0
                              select t;
                    SearchDataCoach = rez.First();
                }
                else if (cli.Count() > 0)
                {
                    var rez = from t in infoClients()
                              where t.Name.CompareTo(name) == 0
                              select t;
                    SearchDataClien = rez.First();
                }
                else
                    MessageBox.Show("Такого человека не зарегестрировано в спорт-зале");


                return true;


            }
            catch
            {
                return false;
            }
        }
        public bool IsExistCard(int card)
        {
            var re = from t in x.Users
                     where t.number_card == card
                     select t;
            if (re.Count() > 0)
                return true;
            else return false;
        }
        public bool IsExistName(string name)
        {
            var re = from t in x.Users
                     where t.name.CompareTo(name) == 0
                     select t;
            if (re.Count() > 0)
                return true;
            else return false;
        }
        
        //зарплата тренеров
        public void MoneyCoach()
        {
            var rez = from t in x.Visiting
                             select new { id = t.id, id_user = t.id_user, work = t.deteExit.Value.Subtract(t.dateEnter.Value) };
            var rez1=rez.GroupBy(dt=>dt.id_user).Select(g=>new{id=g.First().id,id_user=g.First().id_user, time=g.Sum(dt=>dt.work.TotalMinutes)});

        }
        //history visit coaches

        public bool delete(int card)
        {
            try
            {
                #region USER
                var deleteRow = from t in x.Users
                                where t.number_card == card
                                select t;
                if(deleteRow.Count()>0)
                    foreach (var item in deleteRow)
                        {
                            x.Users.DeleteOnSubmit(item);
                        }
                #endregion
                int idd = GetId(card);
                #region Client/Coach
                var coach = from t in x.Coach
                            where t.id_user == idd
                            select t;
               
                if (coach.Count() > 0)
                    foreach (var item in coach)
                    {
                        x.Coach.DeleteOnSubmit(item);
                    }
                else
                {
                    var client = from t in x.Client
                                 where t.id_user == idd
                                 select t;
                    if(client.Count()>0)
                        foreach (var item in client)
                        {
                            x.Client.DeleteOnSubmit(item);
                        }
                }
                #endregion

                #region Visiting
                var visit = from t in x.Visiting
                            where t.id_user == idd
                            select t;
                if (visit.Count() > 0)
                    foreach (var item in visit)
                    {
                        x.Visiting.DeleteOnSubmit(item);
                    }
                #endregion
                #region History
                var hist = from t in x.History_buy
                            where t.id_user == idd
                            select t;
                if (hist.Count() > 0)
                    foreach (var item in hist)
                    {
                        x.History_buy.DeleteOnSubmit(item);
                    }
#endregion


                x.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
