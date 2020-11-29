using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebBookManagement.Models;
using WebBookManagement.DAL;

namespace WebBookManagement.BLL
{
    /// <summary>
    /// CustomerManage 客户业务
    /// </summary>
    public class CustomerManage
    {
        public CustomerManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 实现对客户的验证
        /// </summary>
        /// <param name="customername">客户名称</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool CheckCustomeByCustomerName(string customername)
        {
            bool result;
            //根据条件获取user对象
            Customer customer = CustomerServices.GetCustomerByCustomerName(customername);
            //判断客户是否存在
            if (customer != null)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 实现对客户对象的添加
        /// </summary>
        /// <param name="customer">客户对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static int AddCustomer(Customer customer)
        {
            int result;
            if (CheckCustomeByCustomerName(customer.customername))
            {
                if (CustomerServices.AddCustomer(customer) > 0)
                {
                    result = 1;
                }
                else
                {
                    result = 0;
                }

            }
            else {
                result = -1;
            }
            return result;
        }
        /// <summary>
        /// 实现根据客户id查找更新的客户对象
        /// </summary>
        /// <param name="id">客户id</param>
        /// <param name="datacustomer">客户对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool UpdateCustomer(int id, Customer datacustomer)
        {
            bool result;
            //根据条件获取customer对象
            Customer customer = CustomerServices.GetCustomerByCustomerName(datacustomer.customername);

            if (customer == null)
            {
                 result =  CustomerServices.UpdateCustomer(id, datacustomer);
            }
            else
            {
                if (customer.id == id)
                {
                    result = CustomerServices.UpdateCustomer(id, datacustomer);
                }
                else
                {
                    result = false;
                }
                   
            }
            return result;
        }
        /// <summary>
        /// 实现根据客户id查找删除客户对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回查询结果数据result</returns>
        public static bool DeleteCustomerById(int id)
        {
            try
            {
                Customer customer = CustomerServices.GetCustomerByCustomerId(id);
                BookEntities1 db = new BookEntities1();
                //将要删除的学生对象状态修改为删除
                db.Entry(customer).State = EntityState.Deleted;
                db.Customer.Remove(customer);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            

        }

        public static object SelectCustomerIdList()
        {
            return CustomerServices.GetCustomerIdList();
        }
        /// <summary>
        ///  实现根据客户CustomerName查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="rcordCount">总条数</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByCustomerNamePaging(string value,int pageSize,int pageIndex)
        {
           
            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = CustomerServices.GetCustomerCountByCustomerName(value);
            if (rcordCount > 0)
            {
                //客户列表
                List<Customer> list;
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                list = CustomerServices.GetCustomerListByCustomerName(value, pageIndex, pageSize);
                return new { list, pageIndex, pageCount };

            }
            else
            {
                List<int> list = new List<int>();
                pageCount = 0;
                return new { list,pageIndex, pageCount };
            }
           
            
        }
        /// <summary>
        ///  实现根据客户Phone查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="rcordCount">总条数</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByPhonePaging(string value, int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = CustomerServices.GetCustomerCountByPhone(value);
            if (rcordCount > 0)
            {
                //客户列表
                List<Customer> list;
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                list = CustomerServices.GetCustomerListByPhone(value, pageIndex, pageSize);
                return new { list, pageIndex, pageCount };

            }
            else
            {
                List<int> list = new List<int>();
                pageCount = 0;
                return new { list, pageIndex, pageCount };
            }


        }
        /// <summary>
        ///  实现根据客户ConnectionPerson查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="rcordCount">总条数</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByConnectionPersonPaging(string value, int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = CustomerServices.GetCustomerCountByConnectionPerson(value);
            if (rcordCount > 0)
            {
                //客户列表
                List<Customer> list;
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                list = CustomerServices.GetCustomerListByConnectionPerson(value, pageIndex, pageSize);
                return new { list, pageIndex, pageCount };

            }
            else
            {
                List<int> list = new List<int>();
                pageCount = 0;
                return new { list, pageIndex, pageCount };
            }


        }
    }
}