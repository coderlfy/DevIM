using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketCommunication.Cache
{
    public class CustomerCollector
    {
        private static List<Customer> _customers;

        public static List<Customer> _Customers
        {
            get { return _customers; }
            set { _customers = value; }
        }

        public static void Init()
        {
            _Customers = new List<Customer>();
        }

        private static Customer newCustomer = null;

        private static bool existContent(Customer oldCustomer)
        {
            Console.WriteLine("以下为检测是否存在该customer");

            return ((newCustomer._UId == oldCustomer._UId) &&
                (newCustomer.IPAddress == oldCustomer.IPAddress) &&
                (newCustomer.Port == oldCustomer.Port));
                
        }

        public static bool IsExist(Customer customer)
        {
            #region
            newCustomer = customer;
            if (_Customers.Count > 0)
                return (_Customers.Find(existContent) != null);
            else
                return false;
            #endregion
        }

        private static string _findUid = "";
        private static bool matchUID(Customer oldCustomer)
        {
            #region

            return (_findUid == oldCustomer._UId.ToString());
            #endregion
        }

        public static List<Customer> FindCustomers(string UId)
        {
            #region
            _findUid = UId;
            return _Customers.FindAll(matchUID);
            #endregion
        }

        public static void Add(Customer customer)
        {
            #region
            _Customers.Add(customer);
            #endregion
        }
        public static void Remove(Customer customer)
        {
            #region
            foreach (Customer temp in _customers)
            {
                if (temp._UId == customer._UId &&
                    temp.IPAddress == customer.IPAddress &&
                    temp.Port == customer.Port)
                {
                    _customers.Remove(temp);
                    break;
                }
            }
            #endregion
        }

        public static void ViewToConsole()
        {
            Console.Write(ToString());
        }

        public static string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (Customer temp in _customers)
                str.AppendLine(string.Format("UID:{0},IPAddress:{1},ClientPort:{2}",
                    temp._UId, temp.IPAddress, temp.Port));
            return str.ToString();
        }

        
    }
}
