using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Doctor_L_O_B_A_N_O_V
{
    class Kind
    {
        string name;
        List<object> simpt=new List<object>(); 
        public Kind(string ill, List<object> simptom)
        {
            name = ill;
            foreach (var item in simptom)
            {
                simpt.Add(item);
            }
        }
        public override string ToString()
        {
            return name;
        }
    }
    class Zapolnenie    
    {
       //public List<Kind> medicina=new List<Kind>();
       public Dictionary<string, List<object>> medicina = new Dictionary<string, List<object>>();
      public Zapolnenie ()
      {
          medicina.Add("бронхит", new List<object>() { BAD.Nose.body_temp, BAD.Nose.body_kashel, BAD.Nose.body_bol_v_grydi, BAD.Nose.head_bol, BAD.Nose.body_hrip_legkie });
          medicina.Add("гайморит", new List<object>() { BAD.Nose.nose_nasmork, BAD.Nose.head_bol, BAD.Nose.nose_zatrudnenie, BAD.Nose.nose_oboianie, BAD.Nose.eye_slezotechenie, BAD.Nose.body_temp });
          medicina.Add("вывих", new List<object>() { BAD.Nose.hand_bol_v_systave, BAD.Nose.hand_opychla, BAD.Nose.hand_krovoizlianie });
          medicina.Add("артрит", new List<object>() { BAD.Nose.hand_otek, BAD.Nose.hand_bol, BAD.Nose.hand_krovoizlianie, BAD.Nose.hand_tygoPodviznost, BAD.Nose.body_temp });
          medicina.Add("грип", new List<object>() { BAD.Nose.body_temp,BAD.Nose.body_slabost,BAD.Nose.body_potlivost,BAD.Nose.body_bol_v_mushci,BAD.Nose.head_bol,BAD.Nose.eye_bol,BAD.Nose.eye_slezotechenie});
          medicina.Add("грыжа", new List<object>() { BAD.Nose.body_otdishka, BAD.Nose.body_vtiazgenie_mezgreb_promezgytkov,BAD.Nose.body_takihardia});
           medicina.Add("коньюктивит", new List<object>() { BAD.Nose.eye_otek,BAD.Nose.eye_pokrasnenie,BAD.Nose.eye_gnoi_vudil,BAD.Nose.eye_vospalenie});

      }


      
    }
}
