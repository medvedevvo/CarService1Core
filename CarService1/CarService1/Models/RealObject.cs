using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarService1.Models
{
    /***** Параметр объекта ******************************************************************************************/
    public class RealObjectParameter
    {
        public string name = "";                                        // Имя параметра
        public string key = "";                                         // Ключ параметра
        public string type = "";                                        // Тип параметра
        public string val = "";
       
        //--- Конструктор класса --------------------------------------------------------------------------------------
        public RealObjectParameter(string name, string key, string type)
        {
            this.name = name;
            this.key = key;
            this.type = type;
        }
        public RealObjectParameter(string name, string key, string type, string val)
        {
            this.name = name;
            this.key = key;
            this.type = type;
            this.val = val;
        }
    }

    /***** Реальный объект *******************************************************************************************/
    public class RealObject
    {
        public string name = "";                                        // Имя параметра
        public string key = "";                                         // Ключ параметра
        public List<RealObjectParameter> parameters;                    // Список параметров

        //--- Конструктор класса --------------------------------------------------------------------------------------
        public RealObject(string name, string key)
        {
            this.name = name;
            this.key = key;
            this.parameters = new List<RealObjectParameter>();
        }

        //--- Добавление параметра ------------------------------------------------------------------------------------
        public void AddParameter(string name, string key, string type)
        {
            parameters.Add(new RealObjectParameter(name, key, type));
        }
        public void AddParameter(string name, string key, string type, string var)
        {
            parameters.Add(new RealObjectParameter(name, key, type, var));
        }
        public void AddParameter(RealObjectParameter parameter)
        {
            parameters.Add(parameter);
        }

        
        public int IndexOf(string key)
        {
            if (parameters.Count >= 0)
            {
                for (int i = 0; i < parameters.Count; i++)
                    if (parameters[i].key == key)
                        return i;
            }

            return -1;
        }
    }
}
