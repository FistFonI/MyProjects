using System;

namespace Workshop
{
    /// <summary>
    /// Представляет собой заказ на ремонт.
    /// </summary>
    public class RepairOrder
    {
        /// <summary>
        /// Возвращает или задает ID.
        /// </summary>
        public int ID { get; set; }

        private string name;
        /// <summary>
        /// Возвращает или задает марку изделия.
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (value != null && value != "")
                    name = value;
                else
                    throw new ArgumentNullException("Марка изделия не должна быть пустой.");
            }
        }

        /// <summary>
        /// Возвращает или задает дату приёма.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Возвращает или задает состояние готовности.
        /// </summary>
        public string Readiness { get; set; }

        /// <summary>
        /// Возвращает или задает краткое описание проблемы.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Пустой конструктор инициализации RepairOrder.
        /// </summary>
        public RepairOrder() { }

        /// <summary>
        /// Конструктор инициализации RepairOrder, принимающий марку изделия, дату приёма, состояние готовности, описание проблемы.
        /// </summary>
        /// <param name="name">
        /// Марка изделия, полученной в заказе техники.
        /// </param>
        /// <param name="date">
        /// Дата приёма заказа.
        /// </param>
        /// <param name="readiness">
        /// Состояние готовности, сданной в ремонт техники.
        /// </param>
        /// <param name="description">
        /// Описание проблемы, сданной в ремонт техники.
        /// </param>
        public RepairOrder(string name, DateTime date, string readiness, string description) 
        {
            Name = name;
            Date = date;
            Readiness = readiness;
            Description = description;
        }

        public override string ToString()
        {
            return $"Марка изделия: {Name}, Дата приёма: {Date}, Состояние готовности: {Readiness}.";
        }
    }
}
