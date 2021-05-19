using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security;

namespace ExapleForPoint.Modelling
{
    /// <summary>
    /// Класс, агрегирующий данные для подключения к БД
    /// </summary>
    public class DbParams
    {
        private string dbName;
        private string dataSource;
        private string port;
        private string userID;
        private String password;

        public string DbName { 
            get { return dbName; }
            set { dbName = value; }
        }
        public string DataSource
        {
            get { return dataSource; }
            set { dataSource = value; }
        }
        public string Port
        {
            get { return port; }
            set { port = value; }
        }
        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public String Password
        {
            get { return password; }
            set { password = value; }
        }

    }

    internal class DataBaseWorker
    {
        PointExampleContext exampleContext;

        /// <summary>
        /// Метод создания БД и ORM для дальнейшего применения
        /// </summary>
        public PointExampleContext ConfigDb(DbParams context)
        {
            try { exampleContext = new PointExampleContext(context);
                return exampleContext;
            }
            catch (Exception ex)
            { throw ex; }
        }


        /// <summary>
        /// Метод генерации строки подключения
        /// </summary>
        /// <param name="dbParams">Параметры подключения к БД</param>
        /// <returns></returns>
        internal static string EFConnectionString (DbParams dbParams)
        {
            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = dbParams.DataSource + "," + dbParams.Port;
            cs.InitialCatalog = dbParams.DbName;
            cs.UserID = dbParams.UserID;
            cs.Password = dbParams.Password;

            cs.MultipleActiveResultSets = true;

            return cs.ToString();
        }
    }

    /// <summary>
    /// Конструктор, определяющий основные элементы класса
    /// </summary>
    internal class DataBaseFiller
    {
        private List<string> MaleFirstName, MaleMiddleName, MaleLastName,
            FemaleFirstName, FemaleMiddleName, FemaleLastName, Sex;


        public DataBaseFiller()
        {
            MaleFirstName = new List<string> {
            "Иван", "Петр", "Семён", "Евгений", "Алексей",
            "Владимир","Олег", "Сергей","Тимур","Азбек"};
            MaleMiddleName = new List<string> {
            "Иванович", "Петрович", "Семёнович", "Евгеньевич", "Алексеевич",
            "Владимирович","Олегович", "Сергеевич","Тимурович","Александрович"};
            MaleLastName = new List<string> {
            "Иванов", "Петров", "Семёнов", "Игорян", "Алексеев",
            "Владимиров","Михельсон", "Сергеев","Квашнин","Александров"};

            FemaleFirstName = new List<string> {
            "Татьяна","Мария","Александра","Надежда","Анна",
            "Ольга","Маруся", "Алиса","Софья","Ирэн"};
            FemaleMiddleName = new List<string> {
            "Ивановна", "Петровна", "Семёновна", "Евгеньевна", "Алексеевна",
            "Владимировна","Олеговна", "Сергеевна","Тимуровна","Александровна"};
            FemaleLastName = new List<string> {
            "Иванова", "Варнава", "Петрова", "Семенова", "Захарченко",
            "Куценко","Михельсон", "Авен","Горбачева","Гайдай"};

            Sex = new List<string> { "Мужской", "Женский" };
        }


        /// <summary>
        /// Метод получения рандомного значения по рандомному индексу в листе
        /// </summary>
        /// <param name="list">Лист, из которого осуществляется выборка</param>
        /// <returns></returns>
        private string GetRandomListValue (List<string> list)
        {
            Random random = new Random();
            int index = random.Next(list.Count);

            return list[index];
        }

        /// <summary>
        /// Метод получения рандомной даты от обозначенной до текущего момента
        /// </summary>
        /// <param name="dt">Дата начала вычисления ранддомной даты</param>
        /// <returns></returns>
        internal DateTime GetRandomDt(DateTime dt)
        {
            Random random = new Random();
            DateTime newDt = new DateTime();
            int dateRange = (DateTime.Today - dt).Days;

            return newDt = dt.AddDays(random.Next(dateRange));

        }

        /// <summary>
        /// Метод заполнения листа Клиентов
        /// </summary>
        /// <param name="customersCount">Количество клиентов</param>
        /// <returns></returns>
        internal List<Customers> SetCustomers (int customersCount)
        {
            Random rnd = new Random();
            List<Customers> custList = new List<Customers>(customersCount);
            DateTime birthDate = new DateTime(1950, 1, 1);
            DateTime regDate = new DateTime(2015, 1, 1);

            int i = 0;
            foreach (var cust in custList)
            {
                cust.ID = i;
                int j = rnd.Next(1);
                if (Convert.ToBoolean(j))
                {
                    cust.LastName = GetRandomListValue(MaleLastName);
                    cust.FirstName = GetRandomListValue(MaleFirstName);
                    cust.MiddleName = GetRandomListValue(MaleMiddleName);
                    cust.Sex = Sex[j];
                }
                else
                {
                    cust.LastName = GetRandomListValue(FemaleLastName);
                    cust.FirstName = GetRandomListValue(FemaleFirstName);
                    cust.MiddleName = GetRandomListValue(FemaleMiddleName);
                    cust.Sex = Sex[j];
                }
                cust.BirthDate = GetRandomDt(birthDate);
                cust.RegistrationDate = GetRandomDt(regDate);
                i++;
            }
            return custList;
        }

        /// <summary>
        /// Метод заполнения листа заказов
        /// </summary>
        /// <param name="customersCount">Количество клиентов</param>
        /// <param name="ordersCount">Количество заказов</param>
        /// <returns></returns>
        internal List<Orders> SetOders (int customersCount, int ordersCount)
        {
            List<Orders> ordList = new List<Orders>(ordersCount);
            Random rnd = new Random();
            DateTime orderDate = new DateTime(2016, 1, 1);

            int i = 0;
            foreach(var ord in ordList)
            {
                ord.ID = i;
                ord.CustomerID = rnd.Next(customersCount-1);
                ord.OrderDate = GetRandomDt(orderDate);
                ord.Price = rnd.Next(150, 25000);
                i++;
            }

            return ordList;
        }


    }
}
