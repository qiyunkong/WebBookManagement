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
    /// ProviderManage 供应商业务
    /// </summary>
    public class ProviderManage
    {
        public ProviderManage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 实现对供应商的验证
        /// </summary>
        /// <param name="prename">供应商名称</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool CheckCustomeByProviderName(string prename)
        {
            bool result;
            //根据条件获取user对象
            Provider provider = ProviderServices.GetProviderByProviderName(prename);
            //判断供应商是否存在
            if (provider != null)
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
        /// 实现对供应商对象的添加
        /// </summary>
        /// <param name="provider">供应商对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static int AddProvider(Provider provider)
        {
            int result;
            if (CheckCustomeByProviderName(provider.providername))
            {
                if (ProviderServices.AddProvider(provider) > 0)
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
        /// 实现根据供应商id查找更新的供应商对象
        /// </summary>
        /// <param name="id">供应商id</param>
        /// <param name="dataProvider">供应商对象</param>
        /// <returns>返回查询结果数据result</returns>
        public static bool UpdateProvider(int id, Provider dataProvider)
        {
            bool result;
            //根据条件获取Provider对象
            Provider provider = ProviderServices.GetProviderByProviderName(dataProvider.providername);

            if (provider == null)
            {
                result = ProviderServices.UpdateProvider(id, dataProvider);
            }
            else
            {
                if (provider.id == id)
                {
                    result = ProviderServices.UpdateProvider(id, dataProvider);
                }
                else
                {
                    result = false;
                }

            }
            return result;
        }
        /// <summary>
        /// 实现根据供应商id查找删除供应商对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns>返回查询结果数据result</returns>
        public static bool DeleteProviderById(int id)
        {
            try
            {
                Provider Provider = ProviderServices.GetProviderByProviderId(id);
                BookEntities1 db = new BookEntities1();
                //将要删除的学生对象状态修改为删除
                db.Entry(Provider).State = EntityState.Deleted;
                db.Provider.Remove(Provider);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 实现根据供应商id获取供应商对象
        /// </summary>
        /// <param name="id">供应商id</param>
        /// <returns>返回查询结果数据表供应商对象</returns>
        public static Provider GetProviderById(int id)
        {
            return ProviderServices.GetProviderByProviderId(id);
        }
        /// <summary>
        ///  实现根据供应商prename查找的分页
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
            rcordCount = ProviderServices.GetProviderCountByProviderName(value);
            if (rcordCount > 0)
            {
                //供应商列表
                List<Provider> list;
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                list = ProviderServices.GetProviderListByProviderName(value, pageIndex, pageSize);
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
        ///  实现根据供应商Phone查找的分页
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
            rcordCount = ProviderServices.GetProviderCountByPhone(value);
            if (rcordCount > 0)
            {
                //供应商列表
                List<Provider> list;
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                list = ProviderServices.GetProviderListByPhone(value, pageIndex, pageSize);
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
        ///  实现根据供应商ConnectionPerson查找的分页
        /// </summary>
        /// <param name="value">查询的内容</param>
        /// <param name="rcordCount">总条数</param>
        /// <param name="pageSize">多少条一页</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="pageIndex">当前是第几页</param>
        /// <returns></returns>
        public static object GetListByProviderPersonPaging(string value, int pageSize, int pageIndex)
        {

            //总的条数 
            int rcordCount;
            //总的页数 
            int pageCount;
            //总条数 Contains方法是模糊查询 sql %内容% 
            rcordCount = ProviderServices.GetProviderCountByProviderPerson(value);
            if (rcordCount > 0)
            {
                //供应商列表
                List<Provider> list;
                //总的页数 （把总条数除于5就知道能分成几页）Ceiling是向上取整  比如： 10/4 = 3 
                pageCount = Convert.ToInt32(Math.Ceiling((double)rcordCount / pageSize));
                //不能分页到比1更小的页数
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                //不能分页到比总页还大的页数
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                //跳页查询
                list = ProviderServices.GetProviderListByProviderPerson(value, pageIndex, pageSize);
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