using Novacode;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamForm
{
    public enum RezultTest { All, Good, Bad}
    enum TypeWindow { Users, Admins, UserInfo }
    public enum TablesExam
    {
        User, Question, Answer, Rezult, Ticket, Topic, level, Admin, Question_Answer, Ticket_Question
    }

    public static class EnumExtension
    {
        public static string ToStringCorrect(this TablesExam table)
        {
            switch (table)
            {
                case TablesExam.Question_Answer:
                    return "Question-Answer";
                case TablesExam.Ticket_Question:
                    return "Ticket-Question";
                default:
                    return table.ToString();
            }
        }

        public static bool IsGetValue(this IEnumerable<AnswerInfo> source, AnswerInfo value)
        {
            foreach (AnswerInfo item in source)
              if (value.ID == item.ID)
                    return true;
            return false;
        }
    }

    public class Data
    {
        public int ID { get; set; }
        public string name { get; set; }

        public Data(int id, string text)
        { ID = id; name = text; }
    }

    class RezultInfo
    {
        public int ID;
        public int TicketID;
        public string Question;
        public string NameLevel;
        public string NameTopic;
        public bool IsTrue;
        public string Answer;
        public int IdAnswer;
        

    }

    class Protocol
    {     
        public Ticket ticket { get; set; }
        public int answers { get; set; }
        public int errors { get; set; }
    }

    struct UserInfo
    {
        public int ID{ get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
    }

    class Ticket
    {
        public int ID { get; set; }
        public DateTime date { get; set; }
        public UserInfo user { get; set; }
        public string IsOffline { get; set; }
        public string IsPassed { get; set; }
        public string level { get; set; }

    }
    
    public class AnswerInfo 
    {
        public int ID { get; set; }
        public string Text { get; set; }
        public bool IsTrue { get; set; }        
    }

    public class QuestionInfo : IEquatable<QuestionInfo>
    {
        public int ID;
        public string Question;
        public string Info;
        public string Level;
        public string Topic;

        public bool Equals(QuestionInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ID == other.ID;
        }
        public override bool Equals(object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals((QuestionInfo)other);
        }
        public override int GetHashCode()
        {
            return ID;
        }
    }

    struct FieldValue
    {
        public string Field;
        public string Value;

        public FieldValue(string field, string value)
        {
            Field = field;
            Value = value;
        }
    }

    public class AdminInfo
    {
        public string Login { get; set; }
        public string Password { get; set;}
        public string Owner { get; set; }
        public DateTime Date { get; set; }

    }

    class DBWokrSql : IDisposable
    {
        SqlConnection connection;

        string NameDataBase;

        public DBWokrSql(string con)
        {
            connection = new SqlConnection(con);
            SqlConnectionStringBuilder b = new SqlConnectionStringBuilder(con);
            NameDataBase =!String.IsNullOrWhiteSpace(b.AttachDBFilename)?Path.GetFileNameWithoutExtension(b.AttachDBFilename) : b.InitialCatalog;
            connection.Open();
        }

        #region Get

        object GetField(string fieldNameGet, TablesExam table, params FieldValue[] fieldvalue)
        {
            string command = string.Format("SELECT {0} FROM [{1}] WHERE ", fieldNameGet, table);

            bool first = true;
            foreach (var item in fieldvalue)
            {
                if (first) first = false;
                else
                    command += " AND ";

                command += string.Format("{0}='{1}' ", item.Field, item.Value);
            }

            SqlCommand cmd = new SqlCommand(command, connection);

            return cmd.ExecuteScalar();

        }

        int GetID(TablesExam table, params FieldValue[] fieldvalue)
        {
            int rez = -1;
            var ob = GetField("id", table, fieldvalue);
            if (ob != null) int.TryParse(ob.ToString(), out rez);
            return rez;
        }

        IDictionary<int, string> GetAllOneFields(TablesExam table)
        {
            Dictionary<int, string> output = new Dictionary<int, string>();
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM [{0}]", table), connection);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    output.Add(reader.GetInt32(0), reader.GetString(1));

            return output;
        }

        public IEnumerable<AnswerInfo> GetAnswers(int idQuestion, bool onlyTrue = false)
        {
            List<AnswerInfo> answer = new List<AnswerInfo>();

            string command = string.Format("SELECT * FROM [View_Answer] WHERE question_id='{0}' AND IsNotUse='0'", idQuestion);

            if (onlyTrue)
            {
                 command += " AND istrue='1'"; //check field istrue
            }

            SqlCommand cmd = new SqlCommand(command, connection);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    answer.Add(new AnswerInfo() { Text = reader.GetString(0), IsTrue = reader.GetBoolean(1), ID = reader.GetInt32(2) });

            return answer;


        }

        public IDictionary<int, string> GetLevels()
        {
            return GetAllOneFields(TablesExam.level);
        }

        public int GetIDLevel(string name)
        {
            return GetID(TablesExam.level, new FieldValue("name", name));
        }

        public IEnumerable<QuestionInfo> GetIDQuestionLike(int id)
        {
            string command = "SELECT * FROM [QuestionView]  WHERE id LIKE N'%" + id.ToString() + "%' AND IsNotUse='0'";

            SqlCommand cmd = new SqlCommand(command, connection);

            List<QuestionInfo> questions = new List<QuestionInfo>();

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    questions.Add(new QuestionInfo() { ID = reader.GetInt32(0), Level = reader.GetString(1), Question = reader.GetString(2), Info = reader.IsDBNull(reader.GetOrdinal("info")) ? "" : reader.GetString(3), Topic = reader.GetString(4) });
            return questions;
        }

        public UserInfo GetUserLike(int id)
        {
            string command = "SELECT * FROM [User]  WHERE id LIKE N'%" + id.ToString() + "%'";

            SqlCommand cmd = new SqlCommand(command, connection);

            UserInfo rez = new UserInfo();

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    rez.ID = reader.GetInt32(0);
                    rez.Fname = PrepareString(reader.GetString(1));
                    rez.Lname = PrepareString(reader.GetString(2));
                    break;
                }

            return rez;
        }

        public IEnumerable<Users> GetStatisticUsersLike(string Lname)
        {   
            string command = "SELECT Lname, Fname, COUNT(*) FROM [View_Ticket] where Lname LIKE '%"+Lname+"%' Group BY Fname, Lname"; //Total attempts //[Exam].[dbo].

            return GetStatisticUsers(command);
        }

        public IEnumerable<int> GetIDQuestions(int topicId = 0, int levelId = 0)
        {
            string command = "SELECT id FROM [Question]";

            if (topicId != 0 || levelId != 0)
            {
                command += " WHERE ";

                if (topicId != 0)
                    command += " topic_id=" + topicId;
                if (levelId != 0)
                {
                    if (topicId != 0)
                        command += " AND ";
                    command += " level_id=" + levelId;
                }
                command += " AND IsNotUse = '0'";
            }
            List<int> question = new List<int>();

            SqlCommand cmd = new SqlCommand(command, connection);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    question.Add(reader.GetInt32(0));

            return question;
        }

        public IEnumerable<int> GetIdQuestion(int idTicket)
        {
            string command = "SELECT question_id FROM [Ticket-Question] where ticket_id='"+idTicket+"'";

            List<int> question = new List<int>();

            SqlCommand cmd = new SqlCommand(command, connection);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    question.Add(reader.GetInt32(0));

            return question;
        }

        public QuestionInfo GetQuestion(int id_question)
        {
            string command = "SELECT * FROM [QuestionView]  WHERE id=" + id_question;

            SqlCommand cmd = new SqlCommand(command, connection);

            QuestionInfo question = null;
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    question = new QuestionInfo() { ID = id_question, Level = reader.GetString(1), Question = reader.GetString(2),Info=reader.IsDBNull(reader.GetOrdinal("info")) ? "": reader.GetString(3), Topic = reader.GetString(4) };


            return question;
        }

        public Ticket GetTicket(int id)
        {
            string command = "SELECT * FROM [View_Ticket]  WHERE id=" + id;//[Exam].[dbo].

            SqlCommand cmd = new SqlCommand(command, connection);

            Ticket ticket = null;
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    ticket = new Ticket()
                    {
                        ID = reader.GetInt32(0),
                        date = reader.GetDateTime(1),
                        user = new UserInfo() { ID = reader.GetInt32(2), Fname = reader.GetString(3), Lname = reader.GetString(4) },
                        IsOffline = reader.IsDBNull(reader.GetOrdinal("IsPrint")) ? "так" : reader.GetBoolean(5) ? "ні": "так",
                        IsPassed = reader.IsDBNull(reader.GetOrdinal("IsPassed")) ? "--" : reader.GetBoolean(6) ? "так" : "ні"

                    };
                }

            command = "SELECT TOP 1  [question_id] FROM[Ticket - Question] where ticket_id = '" + ticket.ID + "'";//[Exam].[dbo].

            SqlCommand cmd_ = new SqlCommand(command, connection);

            int question = Int32.Parse((string)cmd_.ExecuteScalar());

            ticket.level = GetQuestion(question).Level;


            return ticket;
        }

        public IEnumerable<Ticket> GetTickets(int id_user = 0)
        {
            string command = "";

            if(id_user==0)
                command = "SELECT * FROM [View_Ticket]";
            else
                command = "SELECT * FROM [View_Ticket] WHERE user_id=" + id_user;

            SqlCommand cmd = new SqlCommand(command, connection);

            List<Ticket> ticket = new List<Ticket>();
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    ticket.Add(new Ticket()
                    {
                        ID = reader.GetInt32(0),
                        date = reader.GetDateTime(1),
                        user = new UserInfo() { ID = reader.GetInt32(2), Fname = reader.GetString(3), Lname = reader.GetString(4) },
                        IsOffline = reader.IsDBNull(reader.GetOrdinal("IsPrint")) ? "так" : reader.GetBoolean(5) ? "ні" : "так",
                        IsPassed = reader.IsDBNull(reader.GetOrdinal("IsPassed")) ? "--" : reader.GetBoolean(6) ? "так" : "ні"                        
                    });

            for (int i = 0; i < ticket.Count; i++)
            {
                command = "SELECT TOP 1  [question_id] FROM[Ticket-Question] where ticket_id = '" + ticket[i].ID + "'";//[Exam].[dbo].

                SqlCommand cmd_ = new SqlCommand(command, connection);

                int question = (int)cmd_.ExecuteScalar();

               ticket[i].level = GetQuestion(question).Level;
            }

            return ticket;
        }

        public IEnumerable<Protocol> GetProtocol(DateTime from_, DateTime to_, RezultTest typeRezult=RezultTest.All)
        {
            List<Protocol> prot = new List<Protocol>();
            string command = "";
            switch (typeRezult)
            {
                case RezultTest.All:
                    command = string.Format("SELECT * FROM [View_Ticket] WHERE time>='{0}' AND time<'{1}'", from_.ToString("yyyy-MM-dd"), to_.ToString("yyyy-MM-dd"));
                    break;
                case RezultTest.Good:
                    command = string.Format("SELECT * FROM [View_Ticket] WHERE time>='{0}' AND time<'{1}' AND IsPassed='1'", from_.ToString("yyyy-MM-dd"), to_.ToString("yyyy-MM-dd"));
                    break;
                case RezultTest.Bad:
                    command = string.Format("SELECT * FROM [View_Ticket] WHERE time>='{0}' AND time<'{1}' AND IsPassed='0'", from_.ToString("yyyy-MM-dd"), to_.ToString("yyyy-MM-dd"));
                    break;
            }            

            SqlCommand cmd = new SqlCommand(command, connection);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    prot.Add(new Protocol()
                    {                        
                        ticket = new Ticket()
                        {
                            ID = reader.GetInt32(0),
                            level = reader.GetString(7),
                            date = reader.GetDateTime(1),
                            IsOffline = reader.IsDBNull(reader.GetOrdinal("IsPrint")) ? "так" : reader.GetBoolean(5) ? "ні" : "так",
                            IsPassed = reader.IsDBNull(reader.GetOrdinal("IsPassed")) ? "--" : reader.GetBoolean(6) ? "так" : "ні",
                            user = new UserInfo()
                            {
                                ID = reader.GetInt32(2),
                                Lname = reader.GetString(3),
                                Fname = reader.GetString(4)
                            },
                        }
                    });

            for (int i = 0; i < prot.Count(); i++)
            {
                command = string.Format("SELECT Count(*) FROM [View_Rezult] where ticket_id='{0}'", prot[i].ticket.ID);// [Exam].[dbo].
                cmd = new SqlCommand(command, connection);
                prot[i].answers = (int)cmd.ExecuteScalar();

                command = string.Format("SELECT Count(*) FROM [View_Rezult] where ticket_id='{0}' AND istrue='false' ", prot[i].ticket.ID);// [Exam].[dbo].
                cmd = new SqlCommand(command, connection);
                prot[i].errors = (int)cmd.ExecuteScalar();
            }

            return prot;

        }

        public IEnumerable<Users> GetStatisticUsers(string commandFirst=null)
        {
            List<Users> users = new List<Users>();

            string command;

            if (commandFirst == null)
                command = "SELECT Lname, Fname, COUNT(*) FROM [View_Ticket]  Group BY Fname, Lname"; //Total attempts [Exam].[dbo].
            else
                command = commandFirst;

            SqlCommand cmd = new SqlCommand(command, connection);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    users.Add(new Users() { Lname = reader.GetString(0),
                                            Fname = reader.GetString(1),
                                            Count = reader.GetInt32(2) });


            for (int i = 0; i < users.Count(); i++)
            {
                //offline 
                command = string.Format("SELECT Count(*) FROM [View_Ticket] where Lname='{0}' AND Fname='{1}' AND  IsPrint='1'", users[i].Lname, users[i].Fname);// [Exam].[dbo].
                cmd = new SqlCommand(command, connection);
                users[i].Offline=(int) cmd.ExecuteScalar();

                //online
                command = string.Format("SELECT Count(*) FROM [View_Ticket] where Lname='{0}' AND Fname='{1}' AND  IsPrint='0'", users[i].Lname, users[i].Fname);// [Exam].[dbo].
                cmd = new SqlCommand(command, connection);
                users[i].Online = (int)cmd.ExecuteScalar();

                //passed online
                command = string.Format("SELECT Count(*) FROM [View_Ticket] where Lname='{0}' AND Fname='{1}' AND  IsPassed is not NULL AND IsPassed!='0' ", users[i].Lname, users[i].Fname);// [Exam].[dbo].
                cmd = new SqlCommand(command, connection);
                users[i].Passed = (int)cmd.ExecuteScalar();
            }
                                           
            return users;

        }

        public IEnumerable<AnswerInfo> GetRezultsAnswer(int TicketID, int Question_id)
        {
            if (GetField("user_id", TablesExam.Ticket, new FieldValue("id", TicketID.ToString())) == null) throw new Exception("Tiket is not exist");

            List<AnswerInfo> rez = new List<AnswerInfo>();

            string command = string.Format("SELECT [answer], [istrue], [answer_id] FROM [View_Rezult]  WHERE ticket_id='{0}' AND question_id='{1}' ", TicketID, Question_id);

            SqlCommand cmd = new SqlCommand(command, connection);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    rez.Add(new AnswerInfo()
                    {
                        Text = reader.GetString(0),
                        IsTrue = reader.GetBoolean(1),
                        ID = reader.GetInt32(2)

                    } );
                }

            return rez;

        }

        public IEnumerable<RezultInfo> GetRezults(int TicketID, int Question_id)
        {
            if (GetField("user_id", TablesExam.Ticket, new FieldValue("id", TicketID.ToString())) == null) throw new Exception("Tiket is not exist");

            List<RezultInfo> rez = new List<RezultInfo>();

            string command = string.Format("SELECT * FROM [View_Rezult]  WHERE ticket_id='{0}' AND question_id='{}' ", TicketID, Question_id);

            SqlCommand cmd = new SqlCommand(command, connection);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    rez.Add(new RezultInfo()
                    {
                        ID = reader.GetInt32(0),
                        TicketID = reader.GetInt32(1),
                        Question = reader.GetString(2),
                        NameLevel = reader.GetString(3),
                        NameTopic = reader.GetString(4),
                        IsTrue = reader.GetBoolean(5),
                        Answer = reader.GetString(6),
                        IdAnswer=reader.GetInt32(7)
                    });

            return rez;

        }

        public IDictionary<int, string> GetTopics()
        {
            return GetAllOneFields(TablesExam.Topic);
        }

        public int GetIdUser(string Fname, string Lname)
        {
            return GetID(TablesExam.User, new FieldValue("Fname", PrepareString(Fname).ToUpper()), new FieldValue("Lname", PrepareString(Lname).ToUpper()));
        }

        public UserInfo GetUser(int id)
        {
            string command = string.Format("SELECT Fname, Lname FROM [{0}] WHERE id='{1}'", TablesExam.User,id);
         
            SqlCommand cmd = new SqlCommand(command, connection);

            UserInfo rez = new UserInfo();

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    rez.ID = id;
                    rez.Fname = PrepareString(reader.GetString(0));
                    rez.Lname = PrepareString(reader.GetString(1));

                }

            return rez;
        }

        public IEnumerable<UserInfo> GetUsers()
        {
            List<UserInfo> output = new List<UserInfo>();
            SqlCommand cmd = new SqlCommand(string.Format("SELECT * FROM [{0}]", TablesExam.User), connection);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    output.Add(new UserInfo() { ID = int.Parse(reader.GetString(0)), Fname = reader.GetString(1), Lname = reader.GetString(2) });

            return output;
        }

        public int GetIDAdmin(string login)
        {
            return GetID(TablesExam.Admin, new FieldValue("login", PrepareString(login)));
        }

        public int GetIDAdmin(string login, string password)
        {
            return GetID(TablesExam.Admin, new FieldValue("login", PrepareString(login)),new FieldValue("password", PrepareString(password)));
        }

        public IEnumerable<AdminInfo> GetAdmins()
        {
            List<AdminInfo> admins = new List<AdminInfo>();

            string command = string.Format("SELECT * FROM Admin");
           
            SqlCommand cmd = new SqlCommand(command, connection);

            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                    admins.Add(new AdminInfo() { Login = reader.GetString(1), Password = reader.GetString(2), Owner = reader.GetString(3), Date=reader.GetDateTime(4) });

            return admins;

        }       

        public Dictionary<QuestionInfo, IEnumerable<AnswerInfo>> GetTicketRezults(int TicketID)
        {
            Dictionary<QuestionInfo, IEnumerable<AnswerInfo>> rezult = new Dictionary<QuestionInfo, IEnumerable<AnswerInfo>>();

            var questionsID = GetIdQuestion(TicketID);

            foreach (var item in questionsID)
            {
                var user_answers = GetRezultsAnswer(TicketID, item);

                var questionInfo = GetQuestion(item);

                if (user_answers.Count() == 0)
                    rezult.Add(questionInfo, new List<AnswerInfo>());
                else
                    rezult.Add(questionInfo, user_answers.ToList());
            }
                return rezult;
        }

        #endregion

        #region ADD

        int InsertName(TablesExam table, string nameField, string nameValue)
        {

            if (nameField != null)
            {
                var id = GetID(table, new FieldValue(nameField, nameValue));

                if (id > 0) return id;
            }

            return InsertValue(table, false, nameValue);

        }

        int InsertValue(TablesExam table, bool WithoutReturnID, params string[] value)
        {
            string command = string.Format("INSERT INTO [{0}] VALUES (", table.ToStringCorrect()); //==TablesExam.Question_Answer? "Question-Answer":table==TablesExam.Ticket_Question? table.ToString()) );//[Exam].[dbo].[

            foreach (var item in value)
            {
                if (WithoutReturnID)
                    command += "'" + item + "',";
                else
                {
                    if (item == null)
                    { command += "null,"; continue; }                    
                    command += "'" + PrepareString(item) + "',";
                }
            }
            command = command.TrimEnd(',');

            if (WithoutReturnID)
                command += "); ";
            else
                command += string.Format("); SELECT id FROM [{0}] WHERE id =@@IDENTITY; ", table);

            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = command;
            cmd.Transaction = connection.BeginTransaction();
           
            try
            {
                if (WithoutReturnID)
                {
                    cmd.ExecuteNonQuery();
                    cmd.Transaction.Commit();
                    return 0;
                }
                else
                {
                    int rez = (int)cmd.ExecuteScalar();
                    cmd.Transaction.Commit();
                    return rez;
                }
            }
            catch (Exception ex)
            {
                try { cmd.Transaction.Rollback(); throw new Exception(ex.Message); } catch (Exception) { throw new Exception("Rollback InsertValue didnt work: " + ex.Message); }
            }
        }
        
        public int AddQuestion(QuestionInfo question)
        {
            int level_ = AddLevel(question.Level);
            int topic_ = AddTopic(question.Topic);
            int question_ = AddQuestion(topic_, question.Question, level_, question.Info);

            return question_;

        }

        public int AddQuestion(int idTopic, string question, int idLevel, string info)
        {
            if (GetField("name", TablesExam.Topic, new FieldValue("id", idTopic.ToString())) == null) throw new Exception("Topic is not exist"); ;
            if (GetField("name", TablesExam.level, new FieldValue("id", idLevel.ToString())) == null) throw new Exception("Level is not exist"); ;

            return InsertValue(TablesExam.Question, false, idTopic.ToString(), question.Replace('\'', ' '), idLevel.ToString(), String.IsNullOrWhiteSpace(info)? null : info.Replace('\'', ' '), "false");
        }

        public int AddUser(int idNumber, string Fname, string Lname)
        {
            var UserInfo= GetUser(idNumber);

            if (UserInfo.ID > 0) return UserInfo.ID;           

            string fname = PrepareString(Fname).ToUpper();
            string lname = PrepareString(Lname).ToUpper();            

            InsertValue(TablesExam.User, true, idNumber.ToString(), fname.Replace('\'', ' '), lname.Replace('\'', ' '));

            return idNumber;

        }

        public int AddLevel(string name)
        {
            return InsertName(TablesExam.level, "name", name.ToLower());
        }

        public int AddTopic(string name)
        {
            return InsertName(TablesExam.Topic, "name", name.ToUpper());
        }

        public int AddAnswer(string name)
        {
            return InsertValue(TablesExam.Answer, false, name.Replace('\'', ' '), "false");          
        }

        public int AddTicket(int idUser, bool IsPrint)
        {
            var name = GetField("Lname", TablesExam.User, new FieldValue("id", idUser.ToString()));
            if (name == null)
                throw new Exception("User is not exist");
            return InsertValue(TablesExam.Ticket, false, idUser.ToString(), DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"), IsPrint ? "1" : "0","0");
        }

        public void AddQuestionAnswer(int idQuestion, int idAnswer, bool IsTrue)
        {
            var topicID = GetField("text", TablesExam.Question, new FieldValue("id", idQuestion.ToString()));
            if (topicID == null) throw new Exception("Topic is not exist");
            var levelID = GetField("answer", TablesExam.Answer, new FieldValue("id", idAnswer.ToString()));
            if (levelID == null) throw new Exception("Level is not exist");

            InsertValue(TablesExam.Question_Answer, true ,idQuestion.ToString(), idAnswer.ToString(), IsTrue ? "1" : "0");
        }

        public void AddTicketQuestion(int TicketID, int QuestionID)
        {
            if (GetField("user_id", TablesExam.Ticket, new FieldValue("id", TicketID.ToString())) == null)
                throw new Exception("Ticket is not exist");
            if (GetField("info", TablesExam.Question, new FieldValue("id", QuestionID.ToString())) == null)
                throw new Exception("Question is not exist");
            InsertValue(TablesExam.Ticket_Question, true, TicketID.ToString(), QuestionID.ToString());
        }

        public int AddRezult(int TicketID, int AnswerID, int QuestionID)
        {
            if (GetField("user_id", TablesExam.Ticket, new FieldValue("id", TicketID.ToString())) == null)
                throw new Exception("Ticket is not exist");
            if (GetField("info", TablesExam.Question, new FieldValue("id", QuestionID.ToString())) == null)
                throw new Exception("Question is not exist");
            if (GetField("answer", TablesExam.Answer, new FieldValue("id", AnswerID.ToString())) == null)
                throw new Exception("Answer is not exist");
            return InsertValue(TablesExam.Rezult, false ,TicketID.ToString(), AnswerID.ToString(), QuestionID.ToString());
        }

        public int AddTicket(IDictionary<QuestionInfo, IEnumerable<AnswerInfo>> ticket, int userID, bool IsPrint)
        {
            int idTicket = AddTicket(userID, IsPrint);

            foreach (var item in ticket)
                AddTicketQuestion(idTicket, item.Key.ID);

            return idTicket;
        }

        public int AddNewQuestion(QuestionInfo Question, IEnumerable<AnswerInfo> answer)
        {
            int level_ = AddLevel(Question.Level);
            int topic_ = AddTopic(Question.Topic);
            int question_ = AddQuestion(topic_, Question.Question, level_, Question.Info);

            foreach (var item in answer)
            {
                if (!string.IsNullOrWhiteSpace(item.Text))
                {
                    AddQuestionAnswer(question_, AddAnswer(item.Text), item.IsTrue);
                }
            }
            return question_;

        }

        public int AddAdmin(string login, string password, string father)
        {
            if (GetIDAdmin(login) > 0)
                return -1;//("Користувач з таким ім'ям існує")

            return InsertValue(TablesExam.Admin, false, login.Replace('\'', ' '), password, father, DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss"));
        }

        #endregion

        #region Delete

        public void DeleteValue(TablesExam table, params FieldValue[] fieldValue)
        {
            int id = GetID(table, fieldValue);

            string command = string.Format("DELETE FROM [{0}] WHERE ", table.ToStringCorrect());

            bool first = true;
            foreach (var item in fieldValue)
            {
                if (first) first = false;
                else
                    command += " AND ";

                command += string.Format("{0}='{1}' ", item.Field, item.Value);
            }

            SqlCommand cmd = new SqlCommand(command, connection);
                      
            cmd.ExecuteNonQuery();

        }

        public void DeleteAdmin(string name)
        {
            DeleteValue(TablesExam.Admin, new FieldValue("login", name));            
        }
        #endregion

        #region Update

        public void UpdateValue(TablesExam table, int id, params FieldValue[] fieldValue)
        {
            string command = string.Format("UPDATE {0} SET ", table.ToStringCorrect());

            bool first = true;
            foreach (var item in fieldValue)
            {
                if (first) first = false;
                else
                    command += ", ";

                command += string.Format("{0}='{1}' ", item.Field, item.Value);
            }

            command += "WHERE id="+id;

            SqlCommand cmd = new SqlCommand(command, connection);

            cmd.ExecuteNonQuery();

        }

        public void UpdateTicketRezult(int id_ticket,bool IsPassed)
        {
            UpdateValue(TablesExam.Ticket, id_ticket, new FieldValue("IsPassed", IsPassed ? "1" : "0"));
        }


        public void DisableAnswer(int id)
        {
            UpdateValue(TablesExam.Answer, id, new FieldValue("IsNotUse", "true"));
        }

        public void DisableQuestion(int id)
        {
            UpdateValue(TablesExam.Question, id, new FieldValue("IsNotUse", "true"));
        }

        public void UpdateAnswer(int idQuestion,AnswerInfo answer)
        {                  
                int idAnswer = AddAnswer(answer.Text.Replace('\'', ' '));

                AddQuestionAnswer(idQuestion, idAnswer, answer.IsTrue);

                DisableAnswer(answer.ID);
        }

        public int UpdateQuestion(QuestionInfo info)
        {
            UpdateValue(TablesExam.Answer, info.ID, new FieldValue("IsNotUse", "true"));

            return AddQuestion(info);
        }

         void UpdateAdmin(int id, params FieldValue[] fieldValue)
        {
            UpdateValue(TablesExam.Admin, id, fieldValue);            
        }

        public void UpdateAdmin(int id, string login, string password=null)
        {
            List<FieldValue> fields = new List<FieldValue>();

            if (login != null)
                fields.Add(new FieldValue("login", login));
            if (password != null)
                fields.Add(new FieldValue("password", password));

            if (fields.Count > 0)
                UpdateAdmin(id, fields.ToArray());
        }    

        #endregion

        public IDictionary<QuestionInfo, IEnumerable<AnswerInfo>> CreateTicket(int LevelID)
        {
            try {
                Random rand = new Random();

                Dictionary<QuestionInfo, IEnumerable<AnswerInfo>> ticket = new Dictionary<QuestionInfo, IEnumerable<AnswerInfo>>();

                var topics = GetTopics();

                List<int> keys = topics.Keys.Take(3).ToList();

                for (int i = keys.Count; i < 10; i++)
                    keys.Add(keys[rand.Next(keys.Count() - 1)]);


                foreach (var item in keys)
                {
                    List<int> questions = (List<int>)GetIDQuestions(item, LevelID);
                    if (questions.Count == 0)
                        do
                        {
                            questions = (List<int>)GetIDQuestions(keys[rand.Next(keys.Count() - 1)], LevelID);

                        } while (questions.Count < 2);
                    while (true)
                    {
                        var id_question = questions[rand.Next(questions.Count() - 1)];

                        var question = GetQuestion(id_question);
                        if (ticket.ContainsKey(question)) continue;
                        var answer = GetAnswers(id_question);

                        ticket.Add(question, answer);
                        break;
                    }
                }
                return ticket;
            }
            catch(Exception ex)
            {
                throw new Exception("CreateTicket " + ex.Message);
            }
        }

        public string PrepareString(string str)
        {
            char[] separ = new char[] { ' ', '\\' };
            return str.TrimStart(separ).TrimEnd(separ);
        }

        public object DoCommand(string cmd)
        {
            return new SqlCommand(cmd, connection).ExecuteScalar();
        }

        public bool WriteTicketToWord(IDictionary<QuestionInfo, IEnumerable<AnswerInfo>> ticket, UserInfo user, int ticketID, string namefile)
        {
            try
            {
                var doc = DocX.Create(namefile);

                doc.MarginTop = 20;
                doc.MarginLeft = 20;
                doc.MarginRight = 20;
                doc.MarginBottom = 20;
                
                
                var header = new Formatting();
                header.FontFamily = new System.Drawing.FontFamily("Times New Roman");
                header.Size = 16;
                header.Bold = true;

                var titleName = new Formatting();
                titleName.FontFamily = new System.Drawing.FontFamily("Times New Roman");
                titleName.Size = 11;

                var titleQuestion = new Formatting();
                titleQuestion.FontFamily = new System.Drawing.FontFamily("Times New Roman");
                titleQuestion.Size = 9;
                titleQuestion.Bold = true;

                var titleInfo = new Formatting();
                titleInfo.FontFamily = new System.Drawing.FontFamily("Times New Roman");
                titleInfo.Size = 9D;
                titleInfo.Italic = true;

                var contenttext = new Formatting();
                contenttext.FontFamily = new System.Drawing.FontFamily("Times New Roman");
                contenttext.Size = 9D;

                doc.InsertParagraph("Тест для підвищення / пониження класності водіям та розрядів машиністам "+ ticket.First().Key.Level, false, header).Alignment=Alignment.center;
                doc.InsertParagraph(string.Format("Іспит складає {0} {1}\tТаб. № {2}", PrepareString(user.Fname), PrepareString(user.Lname),new string('_',20)), false, titleName).Alignment = Alignment.left;
                doc.InsertParagraph(DateTime.Now.ToString(), false, titleName).Alignment = Alignment.left;
                doc.InsertParagraph("Білет № " + ticketID, false, titleName).Alignment = Alignment.center;
                               
                foreach (var question in ticket)
                {
                    doc.InsertParagraph(question.Key.Topic, false, titleInfo);
                    doc.InsertParagraph(question.Key.Question, false, titleQuestion);
                    doc.InsertParagraph(question.Key.Info, false, titleInfo);
                    foreach (var answer in question.Value)
                        doc.InsertParagraph(answer.Text, false, contenttext);
                    
                }
                doc.InsertParagraph("");                

                    Paragraph p = doc.InsertParagraph("Підпис працівника який проходить тестування ___________________________________", false, contenttext);
                    p.Alignment = Alignment.left;

                    doc.InsertParagraph("");
                    doc.InsertParagraph(@"Підпис членів комісії  ________________________________________________________
                                       ________________________________________________________
                                       ________________________________________________________", false, contenttext).Alignment = Alignment.left;              
                doc.Save();
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("WriteTicketToWord " + ex.Message);
            }
        }

        public static bool WriteProtocol( IEnumerable<Protocol> people, string nameFile, DateTime from, DateTime to)
        {
            try
            {
                var doc = DocX.Create(nameFile);

                doc.PageLayout.Orientation = Novacode.Orientation.Landscape;
                doc.MarginTop = 20;
                doc.MarginLeft = 20;
                doc.MarginRight = 20;
                doc.MarginBottom = 20;

                var header = new Formatting();
                header.FontFamily = new System.Drawing.FontFamily("Times New Roman");
                header.Size = 16;
                header.Bold = true;

                var titleName = new Formatting();
                titleName.FontFamily = new System.Drawing.FontFamily("Times New Roman");
                titleName.Size = 11;     

                var contenttext = new Formatting();
                contenttext.FontFamily = new System.Drawing.FontFamily("Times New Roman");
                contenttext.Size = 9D;

                doc.InsertParagraph("Протокол тестування ", false, header).Alignment = Alignment.center;
                doc.InsertParagraph(string.Format("Період проведення з {0} до {1}", from.ToString("dd-MM-yyyy"), to.ToString("dd-MM-yyyy")), false, titleName).Alignment = Alignment.left;

                Table table = doc.AddTable(people.Count() + 1, 10);

                table.Rows[0].Cells[0].Paragraphs.First().Append(" № ");
                table.Rows[0].Cells[1].Paragraphs.First().Append("Прізвище");
                table.Rows[0].Cells[2].Paragraphs.First().Append("Таб. ном.");
                table.Rows[0].Cells[3].Paragraphs.First().Append("Білет");
                table.Rows[0].Cells[4].Paragraphs.First().Append("Дата");
                table.Rows[0].Cells[5].Paragraphs.First().Append("На комп'ютері");
                table.Rows[0].Cells[6].Paragraphs.First().Append("Клас       ");
                table.Rows[0].Cells[7].Paragraphs.First().Append("Відповіді");
                table.Rows[0].Cells[8].Paragraphs.First().Append("Помилки");
                table.Rows[0].Cells[9].Paragraphs.First().Append("Результат");

                List<Protocol> people_ = people.ToList();

                for (int z = 0; z < people_.Count(); z++)
                {
                    table.Rows[z + 1].Cells[0].Paragraphs.First().Append((z + 1).ToString());
                    table.Rows[z + 1].Cells[1].Paragraphs.First().Append(people_[z].ticket.user.Lname);
                    table.Rows[z + 1].Cells[2].Paragraphs.First().Append(people_[z].ticket.user.ID.ToString());
                    table.Rows[z + 1].Cells[3].Paragraphs.First().Append(people_[z].ticket.ID.ToString());
                    table.Rows[z + 1].Cells[4].Paragraphs.First().Append(people_[z].ticket.date.ToString("dd-MM-yyyy"));
                    table.Rows[z + 1].Cells[5].Paragraphs.First().Append(people_[z].ticket.IsOffline.Equals("ні")?"так": "ні");
                    table.Rows[z + 1].Cells[6].Paragraphs.First().Append(people_[z].ticket.level);
                    table.Rows[z + 1].Cells[7].Paragraphs.First().Append(people_[z].answers.ToString());
                    table.Rows[z + 1].Cells[8].Paragraphs.First().Append(people_[z].errors.ToString());
                    table.Rows[z + 1].Cells[9].Paragraphs.First().Append(people_[z].ticket.IsPassed);

                    if (people_[z].ticket.IsPassed.Equals("ні"))
                        for (int i = 0; i < 10; i++)
                            table.Rows[z + 1].Cells[i].FillColor = System.Drawing.Color.OrangeRed;

                }

                doc.InsertTable(table);
                doc.InsertParagraph("");
                doc.InsertParagraph("");
                doc.InsertParagraph(@"Підпис членів комісії  ________________________________________________________
                                       ________________________________________________________
                                       ________________________________________________________", false, contenttext).Alignment = Alignment.left;
                doc.Save();

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("WriteTicketToWord " + ex.Message);
            }

        }
        #region Dispose
        public void Dispose()
        {
            Dispose(true); GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {

            if (disposing)
            {
                connection.Close();
            }
        }

        ~DBWokrSql() { Dispose(false); }
        #endregion
    }
}
