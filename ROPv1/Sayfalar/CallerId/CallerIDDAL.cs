using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROPv1.Sayfalar.CallerId
{
    public class CallerIDDAL
    {
        /// <summary>
        /// Gets the customer information from the database by phone number.
        /// </summary>
        /// <param name="phone">Phone is the unique key for each customer.</param>
        /// <returns></returns>
        public Customer selectCustomer(string phone)
        {
            try
            {
                Customer customer = new Customer();
                SqlCommand cmd = SQLBaglantisi.getCommand("Select Phone, Name, LastName, Address, Region, CustomerNote from Customers Where Phone=@phone");
                cmd.Parameters.AddWithValue("@phone", phone);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                customer.Phone = dr.GetString(0);
                customer.Name = dr.GetString(1);
                customer.LastName = dr.GetString(2);
                customer.Address = dr.GetString(3);
                customer.Region = dr.GetString(4);
                customer.CustomerNote = dr.GetString(5);
                return customer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool insertNewCustomer(Customer customer)
        {
            try
            {
                SqlCommand cmd = SQLBaglantisi.getCommand("Insert into Customers Values(@phone,@name,@lastName,@address,@region,@customerNote)");
                cmd.Parameters.AddWithValue("@phone", customer.Phone);
                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@lastName", customer.LastName);
                cmd.Parameters.AddWithValue("@address", customer.Address);
                cmd.Parameters.AddWithValue("@region", customer.Region);
                cmd.Parameters.AddWithValue("@customerNote", customer.CustomerNote);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Connection.Dispose();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
