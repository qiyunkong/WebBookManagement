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
    /// SalesManage 销售业务
    /// </summary>
    public class SalesManage
    {
        public SalesManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 实现对进货的验证
        /// </summary>
        /// <param name="id">进货名称</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool CheckCustomeById(int id)
        {
            bool result;
            //根据条件获取user对象
            Sales sales = SalesServices.GetSalesBySalesId(id);
            //判断进货是否存在
            if (sales != null)
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
        /// 实现对进货对象的添加
        /// </summary>
        /// <param name="Sales">进货对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static int AddSales(int customerid, int number, decimal saleprice, int goodsid, string paytype, string operateperson)
        {
            int result;
            User user = UserManage.GetCurrentUser();
            Goods goods = GoodsManage.GetGoodsById(goodsid);
            Sales Sales = new Sales();
            Sales.saleprice = saleprice;
            Sales.number = number;
            Sales.goodsid = goodsid;
            Sales.paytype = paytype;
            Sales.operateperson = operateperson;
            Sales.userid = user.id;
            Sales.salestime = DateTime.Now;
            Sales.customerid = customerid;
            //减少库存量
            goods.number = goods.number - number;

            if (SalesServices.AddSales(Sales) > 0)
            {
                GoodsServices.UpdateGoods(goods.id, goods);
                result = 1;
            }
            else
            {
                result = 0;
            }
            return result;
        }
        /// <summary>
        /// 实现根据进货id查找更新的进货对象
        /// </summary>
        /// <param name="id">进货id</param>
        /// <param name="dataSales">进货对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool UpdateSales(int id, int number, string paytype, string operateperson)
        {
            bool result;
            //根据条件获取Sales对象
            Sales Sales = SalesServices.GetSalesBySalesId(id);
            //查询商品
            Goods goods = GoodsServices.GetGoodsByGoodsId(Sales.goodsid);
            if (Sales == null)
            {
                result = false;
            }
            else
            {
                if (Sales.number > number)
                {
                    //加法
                    goods.number = goods.number + (goods.number - number);
                }
                else
                {
                    //减法
                    goods.number = goods.number - (number - goods.number);
                }
                //修改数据
                Sales.number = number;
                Sales.paytype = paytype;
                Sales.operateperson = operateperson;
                SalesServices.UpdateSales(id, Sales);
                if (SalesServices.UpdateSales(id, Sales))
                {
                    GoodsServices.UpdateGoods(goods.id, goods);
                }
                result = true;
            }
            return result;
        }
        /// <summary>
        /// 实现根据进货id查找删除进货对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回查询结果数据result</returns>
        public static bool DeleteSalesById(int id)
        {
            try
            {
                Sales Sales = SalesServices.GetSalesBySalesId(id);
                BookEntities1 db = new BookEntities1();
                //将要删除的进货对象状态修改为删除
                db.Entry(Sales).State = EntityState.Deleted;
                db.Sales.Remove(Sales);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 实现根据进货id获取进货对象
        /// </summary>
        /// <param name="id">进货id</param>
        /// <returns>返回查询结果数据表进货对象</returns>
        public static object GetSalesById(int id)
        {
            return SalesServices.GetSalesById(id);
        }

        /// <summary>
        /// 实现获取与进货表相关的供应商
        /// </summary>
        /// <returns></returns>
        public static object SelectCustomerIdList()
        {
            return SalesServices.GetSalesCustomerIdList();
        }
        /// <summary>
        /// 实现查询与进货表相关的供应商id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object SelectGoodsIdList(int id)
        {
            return SalesServices.GetSalesGoodsIdListByCustomerId(id);
        }
        /// <summary>
        ///  实现根据进货查找的分页
        /// </summary>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListPaging(int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = SalesServices.GetSalesCount();
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = SalesServices.GetSalesList(pageIndex, pageSize);
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
        ///  实现根据进货time查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByGoodsIdPaging(int id, int pageSize, int pageIndex, DateTime start, DateTime end)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = SalesServices.GetSalesCountByGoodsId(id);
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = SalesServices.GetSalesListByGoodsId(id, pageIndex, pageSize, start, end);
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