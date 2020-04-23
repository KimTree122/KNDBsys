using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KNDBsys.Common.Mathcls
{
    /// <summary>
    /// 四舍五入计算类
    /// </summary>
    public class Round
    {
        /// <summary>
        /// 标准切割，结果保留两位小数
        /// 不计算四舍五入
        /// </summary>
        public static decimal Standard(string money)
        {
            return decimal.Parse((Math.Truncate(decimal.Parse(money) * 100) / 100.00M).ToString("0.00"));
        }

        /// <summary>
        /// 标准四舍五入，结果保留两位小数
        /// </summary>
        public static decimal RoundForStandard(string money)
        {
            return Standard(Math.Round(decimal.Parse(money), 2, MidpointRounding.AwayFromZero).ToString());
        }

        /// <summary>
        /// 针对用户的四舍五入，结果保留两位小数
        /// 要用户交钱的四舍五入，目的就是要用户多交钱
        /// </summary>
        public static decimal RoundForUser(string money)
        {
            if ((decimal.Parse(money) * 100) > (Math.Truncate(decimal.Parse(money) * 100))) //看下小数点第三位是否有数
            {
                //有的时候，直接进1
                return Standard((decimal.Parse(money) + 0.01M).ToString());
            }
            else
            {
                return Standard(money);
            }
        }

        /// <summary>
        /// 针对商家的四舍五入，结果保留两位小数
        /// 要商家交钱的四舍五入，目的是要商家少交钱
        /// </summary>
        public static decimal RoundForMerchant(string money)
        {
            return Standard(money);
        }

        /// <summary>
        /// 固定点的转换，可将小数后面多余的零去掉
        /// 这个不固定保留多少位小数
        /// </summary>
        public static decimal Fixed(string money)
        {
            return decimal.Parse(string.Format("{0:G}", money));
        }
    }
}
