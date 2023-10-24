using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microscore.application.models.dtos.accounts
{
    public class MovementRequest
    {
        public string AccountNumber { get; set; }
        public string TypeAccount { get; set; }
        public string BalanceTrx { get; set; }
        public bool State { get; set; }
        public string Movement { get; set; }
    }
}
