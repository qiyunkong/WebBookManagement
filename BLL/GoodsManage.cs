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
    /// GoodsManage 商品业务
    /// </summary>
    public class GoodsManage
    {
        public GoodsManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 实现对商品的验证
        /// </summary>
        /// <param name="prename">商品名称</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool CheckCustomeByGoodsName(string goodsname)
        {
            bool result;
            //根据条件获取user对象
            Goods goods = GoodsServices.GetGoodsByGoodsName(goodsname);
            //判断商品是否存在
            if (goods != null)
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
        /// 实现对商品对象的添加
        /// </summary>
        /// <param name="Goods">商品对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static int AddGoods(Goods goods)
        {
            int result;
            if (CheckCustomeByGoodsName(goods.goodsname))
            {
                if (GoodsServices.AddGoods(goods) > 0)
                {
                    result = 1;
                }
                else
                {
                    result = 0;
                }

            }
            else
            {
                result = -1;
            }
            return result;
        }
        /// <summary>
        /// 实现根据商品id查找更新的商品对象
        /// </summary>
        /// <param name="id">商品id</param>
        /// <param name="dataGoods">商品对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool UpdateGoods(int id, Goods dataGoods)
        {
            bool result;
            //根据条件获取Goods对象
            Goods goods = GoodsServices.GetGoodsByGoodsName(dataGoods.goodsname);

            if (goods == null)
            {
                result = GoodsServices.UpdateGoods(id, dataGoods);
            }
            else
            {
                if (goods.id == id)
                {
                    result = GoodsServices.UpdateGoods(id, dataGoods);
                }
                else
                {
                    result = false;
                }

            }
            return result;
        }
        /// <summary>
        /// 实现根据商品id查找删除商品对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回查询结果数据result</returns>
        public static bool DeleteGoodsById(int id)
        {
            try
            {
                Goods goods = GoodsServices.GetGoodsByGoodsId(id);
                BookEntities1 db = new BookEntities1();
                //将要删除的学生对象状态修改为删除
                db.Entry(goods).State = EntityState.Deleted;
                db.Goods.Remove(goods);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 实现根据商品id获取商品对象
        /// </summary>
        /// <param name="id">商品id</param>
        /// <returns>返回查询结果数据表商品对象</returns>
        public static Goods GetGoodsById(int id)
        {
            return GoodsServices.GetGoodsByGoodsId(id);
        }
        /// <summary>
        /// 实现获取与商品表相关的供应商id
        /// </summary>
        /// <returns></returns>
        public static object SelectProviderIdList()
        {
            return GoodsServices.GetGoodsProviderIdList();
        }
        public static object GetGoodsList()
        {
            return GoodsServices.GetGoodsList();
        }
        /// <summary>
        /// 实现查询与商品表相关的供应商id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static object SelectGoodsIdList(int id)
        {
            return GoodsServices.GetGoodsGoodsIdListByProviderId(id);
        }
        /// <summary>
        ///  实现根据商品prename查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="rcordCount">总条数</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByGoodsNamePaging(string value, int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = GoodsServices.GetGoodsCountByGoodsName(value);
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = GoodsServices.GetGoodsListByGoodsName(value, pageIndex, pageSize);
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
        ///  实现根据商品Phone查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="rcordCount">总条数</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByProviderNamePaging(string value, int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = GoodsServices.GetGoodsCountByProviderName(value);
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = GoodsServices.GetGoodsListByProviderName(value, pageIndex, pageSize);
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
        ///  实现根据商品ConnectionPerson查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="rcordCount">总条数</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByPreNamePaging(string value, int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = GoodsServices.GetGoodsCountByPreName(value);
            if (rcordCount > 0)
            {
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                var list = GoodsServices.GetGoodsListByPreName(value, pageIndex, pageSize);
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