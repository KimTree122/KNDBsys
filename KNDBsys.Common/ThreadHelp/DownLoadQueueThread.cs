using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common
{
    /// <summary>
    /// 下载线程对了.
    /// </summary>
    public class DownLoadQueueThread : QueueThreadBase<int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list">下载的列表ID</param>
        public DownLoadQueueThread(IEnumerable<int> list) : base(list)
        {

        }
        /// <summary>
        /// 每次多线程都到这里来,处理多线程
        /// </summary>
        /// <param name="pendingValue"列表ID></param>
        /// <returns></returns>
        protected override DoWorkResult DoWork(int pendingID)
        {
            try
            {

                //..........多线程处理....
                return DoWorkResult.ContinueThread;//没有异常让线程继续跑..

            }
            catch (Exception)
            {

                return DoWorkResult.AbortCurrentThread;//有异常,可以终止当前线程.当然.也可以继续,
                //return  DoWorkResult.AbortAllThread; //特殊情况下 ,有异常终止所有的线程...
            }

            //return base.DoWork(pendingValue);
        }
    }
}
