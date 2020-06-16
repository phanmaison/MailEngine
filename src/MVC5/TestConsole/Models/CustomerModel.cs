using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole.Models
{
    [Serializable]
    public class CustomerModel
    {
        public string CustomerName { get; set; }

        public long TotalBudget { get; set; }

        public long OrderCount { get; set; }
    }
}
