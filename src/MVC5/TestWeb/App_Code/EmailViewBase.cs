using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TestWeb
{
    public class EmailViewBase : WebViewPage
    {
        public override void Execute()
        {

        }
    }

    public class EmailViewBase<T> : WebViewPage<T>
    {
        public override void Execute()
        {
        }

        protected const string ColorText = "#0000ff";


        /// <summary>
        /// Calculates the next salary.
        /// </summary>
        /// <param name="currentSalary">The current salary.</param>
        /// <returns></returns>
        protected long CalculateNextSalary(long currentSalary)
        {
            return (long)(currentSalary * 1.2);
        }

        /// <summary>
        /// Formats the date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        protected string FormatDate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
    }
}
