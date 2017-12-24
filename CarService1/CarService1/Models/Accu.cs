using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService1.Models
{
    public class AccuListWithTime
    {
        public int idCar = 1;
        public int time = 0;
        public List<Accu> accu;

        public AccuListWithTime(int time, List<Accu> accu)
        {
            this.time = time;
            this.accu = accu;
        }

        public AccuListWithTime(int time, Accu accu)
        {
            this.time = time;
            if(accu != null)
            {
                this.accu = new List<Accu>();
                this.accu.Add(accu);
            }
                
        }
    }


    public class Accu
    {
        public int id = 0;
        public string name = "";
        public double voltage = 0.0;
        public double current = 0.0;
        public int charge = 0;

        public Accu()
        {

        }
        public Accu(int id, string name, double voltage, double current, int charge)
        {
            this.name = name;
            this.id = id;
            this.voltage = voltage;
            this.current = current;
            this.charge = charge;
        }
    }

    public class AccuAdapter
    {
        private DBObjects dBObjects = DBObjects.getInstance();

        private int id = 0;
        private string name = "";
        private double voltage = 0.0;
        private double current = 0.0;
        private int charge = 0;
        private int realObjectID;

        public AccuAdapter(int realObjectID, int id)
        {
            this.realObjectID = realObjectID;
            this.id = id;
            getName();
        }

        public AccuAdapter(int realObjectID, int id, string name, double voltage, double current, int charge)
        {
            this.id = id;
            this.name = name;
            this.realObjectID = realObjectID;
            this.voltage = voltage;
            this.current = current;
            this.charge = charge;
            getName();
        }

        public double getVoltage()
        {
            string s = dBObjects.objects_list[realObjectID].parameters[0].val.Replace('.', ',');
            voltage = Convert.ToDouble(s);
            return voltage;
        }
        public double getCurrent()
        {
            string s = dBObjects.objects_list[realObjectID].parameters[1].val.Replace('.', ',');
            current = Convert.ToDouble(s);
            return current;
        }
        public int getCharge()
        {
            string s = dBObjects.objects_list[realObjectID].parameters[2].val.Replace('.', ',');
            charge = Convert.ToInt32(s);
            return charge;
        }
        public string getName()
        {
            name = dBObjects.objects_list[realObjectID].name;
            return name;
        }

        public Accu get()
        {
            getVoltage();
            getCurrent();
            getCharge();
            getName();

            return new Accu(id, name, voltage, current, charge);
        }
    }

    public class Accus
    {
        private static Accus instance;                                  // Ссылка на текущий объект
        private DBObjects dBObjects = DBObjects.getInstance();
        private List<AccuAdapter> list = new List<AccuAdapter>();

        //--- Конструктор класса (внутренний) -------------------------------------------------------------------------
        private Accus()
        {
            for(int i = 0; i < dBObjects.objects_list.Count; i++)
            {
                if (dBObjects.objects_list[i].key.IndexOf("ACCU") >= 0)
                    list.Add(new AccuAdapter(i, list.Count));
            }
        }

        //--- Конструктор класса (внешний) ----------------------------------------------------------------------------
        public static Accus getInstance()
        {
            if (instance == null)
                instance = new Accus();
            return instance;
        }

        public List<Accu> get()
        {
            List<Accu> accu_list = new List<Accu>();
            foreach (AccuAdapter aa in list)
                accu_list.Add(aa.get());

            return accu_list;
        }

        public Accu get(int id)
        {
            if ((id >= list.Count) || (id < 0))
                return null;
            return list[id].get();
        }
    }
}
