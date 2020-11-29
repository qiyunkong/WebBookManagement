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
    /// InportManage 进货业务
    /// </summary>
    public class InportManage
    {
        public InportManage()
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
            Inport inport = InportServices.GetInportByInportId(id);
            //判断进货是否存在
            if (inport != null)
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
        /// <param name="Inport">进货对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static int AddInport(int providerid, int number, decimal inportprice, int goodsid, string paytype, string operateperson)
        {
            int result;
            User user = UserManage.GetCurrentUser();
            Goods goods = GoodsManage.GetGoodsById(goodsid);
            Inport inport = new Inport();
            inport.inportprice = inportprice;
            inport.number = number;
            inport.goodsid = goodsid;
            inport.paytype = paytype;
            inport.operateperson = operateperson;
            inport.userid = user.id;
            inport.inporttime = DateTime.Now;
            inport.providerid = providerid;
            //增加库存量
            goods.number = goods.number + number;

            if (InportServices.AddInport(inport) > 0)
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
        /// <param name="dataInport">进货对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool UpdateInport(int id, int number, decimal inportprice, string paytype, string operateperson)
        {
            bool result;
            //根据条件获取Inport对象
            Inport inport = InportServices.GetInportByInportId(id);
            //查询商品
            Goods goods = GoodsServices.GetGoodsByGoodsId(inport.goodsid);
            if (inport == null)
            {
                result = false;
            }
            else
            {
                if (inport.number > number)
                {
                    //减法
                    goods.number = goods.number - (inport.number - number);
                }
                else
                {
                    //加法
                    goods.number = goods.number + (number - inport.number);
                }
                //修改数据
                inport.number = number;
                inport.inportprice = inportprice;
                inport.paytype = paytype;
                inport.operateperson = operateperson;
                InportServices.UpdateInport(id,inport);
                if (InportServices.UpdateInport(id, inport)) {
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
        public static bool DeleteInportById(int id)
        {
            try
            {
                Inport Inport = InportServices.GetInportByInportId(id);
                BookEntities1 db = new BookEntities1();
                //将要删除的进货对象状态修改为删除
                db.Entry(Inport).State = EntityState.Deleted;
                db.Inport.Remove(Inport);
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
        public static object GetInportById(int id)
        {
            return InportServices.GetInportById(id);
        }
        
        /// <summary>
        /// 实现获取与进货表相关的供应商
        /// </summary>
        /// <returns></returns>
        public static object SelectProviderIdList()
        {
            return InportServices.GetInportProviderIdList();
        }
        /// <summary>
        /// 实现查询与进货表相关的供应商id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object SelectGoodsIdList(int id)
        {
            return InportServices.GetInportGoodsIdListByProviderId(id);
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
            rcordCount = InportServices.GetInportCount();
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = InportServices.GetInportList(pageIndex, pageSize);
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
            rcordCount = InportServices.GetInportCountByGoodsId(id);
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = InportServices.GetInportListByGoodsId(id, pageIndex, pageSize,start,end);
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