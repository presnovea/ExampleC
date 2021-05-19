using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExapleForPoint.Modelling
{
    /// <summary>
    /// ORM представление клиентов
    /// </summary>
    public class Customers
    {
        public decimal ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }

        //public virtual ICollection<Orders> Ord { get; set; }
    }
    /// <summary>
    /// ORM представление заказов
    /// </summary>
    public class Orders
    {
        public decimal ID { get; set; }
        public decimal CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }

        // public virtual Customers Customers { get; set; }
    }

    /// <summary>
    /// EF Контекст БД
    /// </summary>
    public class PointExampleContext : DbContext
    {
        public PointExampleContext(DbParams dbParams) : base(DataBaseWorker.EFConnectionString(dbParams))
        { }
        //"name=PointDbString"

        public DbSet<Customers> customers { get; set; }
        public DbSet<Orders> orders { get; set; }

        /// <summary>
        /// Метод, осуществляющий выборку заказов по ID клиента
        /// </summary>
        /// <param name="customerID">ID клиента</param>
        /// <returns></returns>
        public List<Orders> GetOrdersByCustomer (decimal customerID)
        {
            List<Orders> orderByCust = new List<Orders>();

            var queryRes =
                from num in orders
                where num.CustomerID == customerID
                select num;
            foreach (var num in queryRes)
            {
                orderByCust.Add(num);
            }

            return orderByCust;
        }

    }

   
}
