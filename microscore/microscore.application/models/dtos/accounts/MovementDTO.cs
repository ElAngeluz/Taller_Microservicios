using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace microscore.application.models.dtos.accounts
{
    public class MovementDTO : MovementRequest
    {
        public Guid MovementId { get; set; }
    }
}
