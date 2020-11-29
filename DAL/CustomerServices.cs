using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBookManagement.Models;
using System.Data;

namespace WebBookManagement.DAL
{
    /// <summary>
    /// CustomerServices 客户SQL服务
    /// </summary>
    public class CustomerServices
    {
        public CustomerServices()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        ///  对客户表进行添加 
        /// </summary>
        /// <param name="dataCustomer">新增加的客户对象</param>
        /// <returns></returns>
        public static int AddCustomer(Customer dataCustomer)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                //向数据库添加信息
                db.Entry(dataCustomer).State = EntityState.Added;
                db.Customer.Add(dataCustomer);
                return db.SaveChanges();
            };
        }
        /// <summary>
        ///  对客户表名称进行查询  
        /// </summary>
        /// <param name="customername">客户名称</param>
        /// <returns>返回查询结果数据表customer</returns>
        public static Customer GetCustomerByCustomerName(string customername)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据客户传来的customername 查询符合条件的user对象并返回
                Customer customer = db.Customer.FirstOrDefault(c => c.customername == customername);
                return customer;
            };

        }
        /// <summary>
        /// 对客户表id进行查询 
        /// </summary>
        /// <param name="id">客户id</param>
        /// <returns>返回查询结果数据表customer</returns>
        public static Customer GetCustomerByCustomerId(int id)
        {
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                //根据用户传来来的username 查询符合条件的user对象并返回
                Customer customer = db.Customer.Where<Customer>(c=> c.id == id).FirstOrDefault();
                return customer;
            };

        }
        /// <summary>
        ///  对客户表进行更新
        /// </summary>
        /// <param name="customer">查找的客户对象</param>
        /// <param name="datacustomer">更新的客户对象</param>
        /// <returns>返回查询结果数据表customer</returns>
        public static bool UpdateCustomer(int id,Customer datacustomer)
        {
            bool result;
            //数据库实例
            using (BookEntities1 db = new BookEntities1())
            {
                try
                {
                    //第一种写法
                    /*
                    
                    Customer customer = GetCustomerByCustomerId(id);
                    db.Entry(datacustomer).State = EntityState.Modified;
                    customer.id = datacustomer.id;
                    customer.customername = datacustomer.customername;
                    customer.address = datacustomer.address;
                    customer.telephone = datacustomer.telephone;
                    customer.connectionperson = datacustomer.connectionperson;
                    customer.phone = datacustomer.phone;
                    customer.bank = datacustomer.bank;
                    db.SaveChanges();
                    */
                    //第二种写法
               
                    db.Entry(datacustomer).State = EntityState.Modified;
                 
                    db.SaveChanges();


                    result = true;
                }
                catch {
                    result = false;
                }
            };
            return result;
        }

        public static object GetCustomerIdList()
        {
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Customer.Select(c => new
                {
                    c.id,
                    c.customername
                }).ToList();
            }
        }
        /// <summary>
        ///   对客户表的客户名称称进行模糊查询统计
        /// </summary>
        /// <param name="customername">客户名称</param>
        /// <returns>饭查询结果数据表的总数</returns>
        public static int GetCustomerCountByCustomerName(string customername)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Customer.Where<Customer>(c => c.customername.Contains(customername)).Count();
            };
        }
        /// <summary>
        /// 对客户表的客户名称进行模糊查询
        /// </summary>
        /// <param name="customername">客户的客户名称</param>
        /// <param name="pageIndex">查询的位置</param>
        /// <param name="pageSize">查询的多少</param>
        /// <returns>返回查询结果数据表List<Customer></returns>
        public static List<Customer> GetCustomerListByCustomerName(string customername,int pageIndex, int pageSize)
        {
            
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                List<Customer> list;
                list = db.Customer.Where<Customer>(u => u.customername.Contains(customername))
                   .OrderBy<Customer, int>(u => u.id)
                   .Skip<Customer>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Customer>(pageSize).ToList(); //截下取多少条
             
                return list;
            };
        }
        /// <summary>
        /// 对客户表的电话称进行模糊查询统计
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public static int GetCustomerCountByPhone(string phone)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Customer.Where<Customer>(c => c.phone.Contains(phone)).Count();
            };
        }
        /// <summary>
        /// 对客户表的电话称进行模糊查询
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<Customer> GetCustomerListByPhone(string phone, int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                List<Customer> list;
                list = db.Customer.Where<Customer>(u => u.phone.Contains(phone))
                   .OrderBy<Customer, int>(u => u.id)
                   .Skip<Customer>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Customer>(pageSize).ToList(); //截下取多少条

                return list;
            };
        }
        /// <summary>
        /// 对客户表的联系人称进行模糊查询统计
        /// </summary>
        /// <param name="connectionperson"></param>
        /// <returns></returns>
        public static int GetCustomerCountByConnectionPerson(string connectionperson)
        {
            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                return db.Customer.Where<Customer>(c => c.connectionperson.Contains(connectionperson)).Count();
            };
        }
        /// <summary>
        ///  对客户表的联系人称进行模糊查询
        /// </summary>
        /// <param name="connectionperson"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<Customer> GetCustomerListByConnectionPerson(string connectionperson, int pageIndex, int pageSize)
        {

            //创建数据库上下文看对象
            using (BookEntities1 db = new BookEntities1())
            {
                List<Customer> list;
                list = db.Customer.Where<Customer>(u => u.connectionperson.Contains(connectionperson))
                   .OrderBy<Customer, int>(u => u.id)
                   .Skip<Customer>((pageIndex - 1) * pageSize) //跳过多少条
                   .Take<Customer>(pageSize).ToList(); //截下取多少条

                return list;
            };
        }

    }
}