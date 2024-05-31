using System.Data.Entity;
using System.Linq;

namespace Workshop
{
    public static class LINQExtensions
    {
        /// <summary>
        /// Возвращает строковое значение количества всех заказов с указанным состоянием готовности.
        /// </summary>
        /// <param name="orders">Таблица заказов типа RepairOrder.</param>
        /// <param name="readiness">Состояние готовности.</param>
        /// <returns></returns>
        public static string OrdersCount(this DbSet<RepairOrder> orders, string readiness = "")
        {
            return orders
                .Select(a => a)
                .Where(a => readiness == "" ? true : a.Readiness == readiness)
                .Count()
                .ToString();
        }

        /// <summary>
        /// Возвращает строковое значение количества всех заказов с указанным состоянием готовности, за указанный год и квартал.
        /// </summary>
        /// <param name="orders">Таблица заказов типа RepairOrder.</param>
        /// <param name="year">Год.</param>
        /// <param name="quarter">Квартал.</param>
        /// <param name="readiness">Состояние готовности.</param>
        /// <param name="changeIndex">Индекс разницы кварталов.</param>
        /// <returns></returns>
        public static string QuarterOrdersCount(this DbSet<RepairOrder> orders, int year, int quarter, string readiness = "", int changeIndex = 0)
        {
            return orders
                .Where(a => readiness == "" ? true : a.Readiness == readiness)
                .Select(a => a.Date)
                .Where(y => y.Year == year)
                .Select(a => a.Month)
                .Where(m => quarter == 0 + changeIndex ? (1 <= m && m <= 3) :
                    quarter == 1 + changeIndex ? (4 <= m && m <= 6) :
                    quarter == 2 + changeIndex ? (7 <= m && m <= 9) :
                    (10 <= m && m <= 12))
                .Count()
                .ToString();
        }
    }
}
