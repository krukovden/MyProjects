
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Anketa
{
    public class Languauge
    {

        private KindLanguage curentlang;

        public KindLanguage CurentLang
        {
            get { return curentlang; }

        }

        private string name;

        public string Name
        {
            get { return name; }


        }

        private string surname;

        public string Surname
        {
            get { return surname; }

        }

        private string lastname;

        public string Lastname
        {
            get { return lastname; }



        }

        private string age;

        public string Age
        {
            get { return age; }


        }

        private string pol;

        public string Pol
        {
            get { return pol; }


        }

        private string group;

        public string Group
        {
            get { return group; }


        }

        private string newstud;

        public string NewStud
        {
            get { return newstud; }


        }
        private string changestud;

        public string ChangeStud
        {
            get { return changestud; }


        }
        private string deletestud;

        public string DeleteStud
        {
            get { return deletestud; }


        }

        private string edit;

        public string Edit
        {
            get { return edit; }


        }

        private string lang;

        public string Lang
        {
            get { return lang; }


        }

        private string ok;

        public string Ok
        {
            get { return ok; }


        }

        private string menu;

        public string Menu
        {
            get { return menu; }


        }

        private string groupbox;

        public string GroupBox
        {
            get { return groupbox; }


        }

        private string openstud;

        public string OpenStud
        {
            get { return openstud; }


        }

        private string savestud;

        public string SaveStud
        {
            get { return savestud; }


        }
        private string rus;

        public string Rus
        {
            get { return rus; }


        }
        private string eng;

        public string Eng
        {
            get { return eng; }


        }

        private string cancel;

        public string Cancel
        {
            get { return cancel; }


        }

        private string man;

        public string Man
        {
            get { return man; }


        }

        private string woman;

        public string Woman
        {
            get { return woman; }


        }

        private string groupboxlanguage;
        public string GroupBoxLanguage
        {
            get { return groupboxlanguage; }
            set { groupboxlanguage = value; }

        }
        private string nameform;

        public string NameForm
        {
            get { return nameform; }

        }
        private string nameformlang;

        public string NameFormLang
        {
            get { return nameformlang; }

        }
        private string nameformedit;

        public string NameFormEdit
        {
            get { return nameformedit; }

        }
        private string nameformnew;

        public string NameFormNew
        {
            get { return nameformnew; }

        }




        public Languauge(KindLanguage kl)
        {
            switch (kl)
            {
                case KindLanguage.Russian:
                    {
                        name = "Имя ";
                        lastname = "Отчество";
                        surname = "Фамилия";
                        age = "Возраст";
                        pol = "Пол";
                        group = "Группа";

                        newstud = "Новый";
                        changestud = "Изменить";
                        deletestud = "Удалить";

                        openstud = "Открыть";
                        savestud = "Сохранить";
                        cancel = "Отмена";

                        edit = "Правка";
                        lang = "Язык";
                        menu = "Меню";

                        ok = "Хорошо";
                        groupbox = "Функции над студентами";

                        rus = "Руский ";
                        eng = "Английский ";

                        man = "Мужчина";
                        woman = "Женщина";

                        groupboxlanguage = "Выбор языка";
                        curentlang = KindLanguage.Russian;

                        nameform = "Журнал студентов";
                        nameformlang = "Выбор языка";
                        nameformedit = "Редактирования профиля";
                        nameformnew = "Заполнение данных о новом студенте";
                    }
                    break;
                case KindLanguage.English:
                    {
                        name = "Name";
                        lastname = "Lastname";
                        surname = "Surname";
                        age = "Age";
                        pol = "Sex";
                        group = "Group";

                        newstud = "New";
                        changestud = "Change";
                        deletestud = "Delete";

                        openstud = "Open";
                        savestud = "Save";
                        cancel = "Cancel";

                        edit = "Edit";
                        lang = "Language";
                        menu = "Menu";

                        ok = "Agree";
                        groupbox = "Funk under student";

                        rus = "Russian ";
                        eng = "English ";

                        man = "Man ";
                        woman = "Woman";

                        groupboxlanguage = "Choose language";
                        curentlang = KindLanguage.English;

                        nameform = "List of students";
                        nameformlang = "Language";
                        nameformedit = "Edit profile of student";
                        nameformnew = "Creat profile of student with data";
                    }
                    break;
            }
        }

    }
}
