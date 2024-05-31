using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAUPPOTO
{
    public class Salary
    {
        //размер почасовой оплаты труда
        private decimal hourlyPayValue;

        //свойство для поля hourlyPayValue
        public decimal HourlyPayValue 
        {
            get { return hourlyPayValue; }
            set
            {
                if (value > 0)
                    hourlyPayValue = value;
                else throw new ArgumentException("Размер почасовой оплаты труда должен быть больше 0.");
            }
        }

        //размер премии
        private decimal premiumValue;

        //свойство для поля premiumValue
        public decimal PremiumValue 
        {
            get { return premiumValue; }
            set
            {
                if (value > 0)
                    premiumValue = value;
                else throw new ArgumentException("Размер премии должен быть больше 0.");
            }
        }

        //количество рабочих часов в месяц
        private int workingHours;

        //свойство для поля workingHours
        public int WorkingHours 
        {
            get { return workingHours; }
            set
            {
                if (value > 0)
                    workingHours = value;
                else throw new ArgumentException("Количество рабочих часов должно быть больше 0.");
            }
        }

        //свойство размера зарплаты, которое только возвращает значение по формуле: размер почасовой оплаты труда * кол-во рабочих часов + размер премии.
        public decimal SalaryValue 
        {
            get { return hourlyPayValue * workingHours + premiumValue; }
        }

        public override string ToString()
        {
            return HourlyPayValue + " * " + WorkingHours + " + " + PremiumValue + " = " + SalaryValue;
        }
    }
}
