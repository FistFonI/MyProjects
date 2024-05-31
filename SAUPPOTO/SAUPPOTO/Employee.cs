using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAUPPOTO
{
    public class Employee
    {
        //уникальный идентификатор сотрудника
        public Guid ID;

        //Фамилия Имя Отчетство
        public string FullName;

        //Должность
        public string Position;

        //Зарплата
        public Salary Salary;

        //конструктор объявления Employee
        public Employee()
        {
            Salary = new Salary();
        }

        public override string ToString()
        {
            return ID + "\n" + FullName + ", " + Position + "\n" + Salary;
        }
    }
}
