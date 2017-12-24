using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarService1.Models
{
    /***** База данных объектов **************************************************************************************/
    public class DBObjects
    {
        private static DBObjects instance;                              // Ссылка на текущий объект
        public int IdCar = 1;
        public List<RealObject> objects_list = new List<RealObject>();  // Список объектов

        //--- Конструктор класса (внутренний) -------------------------------------------------------------------------
        private DBObjects()
        {
            RealObject temp_obj = new RealObject("Системные параметры", "SYS");
            temp_obj.AddParameter(new RealObjectParameter("Макс.время ожидания ответа", "T", "RW", "500"));
            temp_obj.AddParameter(new RealObjectParameter("Кол-во попыток", "N", "RW", "10"));
            objects_list.Add(temp_obj);

            temp_obj = new RealObject("Таймер", "TIMER");
            temp_obj.AddParameter(new RealObjectParameter("Время с момента запуска", "T", "RW", "0"));
            temp_obj.AddParameter(new RealObjectParameter("Шаг (мс)", "STEP_MS", "RW", "100"));
            objects_list.Add(temp_obj);

            temp_obj = new RealObject("Аккумулятор 1", "ACCU1");
            temp_obj.AddParameter(new RealObjectParameter("Напряжение", "V", "R", "12.1"));
            temp_obj.AddParameter(new RealObjectParameter("Ток нагрузки", "A", "R", "3.14"));
            temp_obj.AddParameter(new RealObjectParameter("Остаток заряда", "CHARGE", "R", "67"));
            objects_list.Add(temp_obj);

            temp_obj = new RealObject("Аккумулятор 2", "ACCU2");
            temp_obj.AddParameter(new RealObjectParameter("Напряжение", "V", "R", "12.1"));
            temp_obj.AddParameter(new RealObjectParameter("Ток нагрузки", "A", "R", "3.14"));
            temp_obj.AddParameter(new RealObjectParameter("Остаток заряда", "CHARGE", "R", "67"));
            objects_list.Add(temp_obj);
        }

        //--- Конструктор класса (внешний) ----------------------------------------------------------------------------
        public static DBObjects getInstance()
        {
            if (instance == null)
                instance = new DBObjects();
            return instance;
        }
    }
}
