using microscore.application.interfaces.abstractapp;
using microscore.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace microscore.domain.entities.Accounts
{
    public class Movement : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public MovementType MovementType { get; set; }

        [Column(TypeName = "decimal(10,5)")]
        public decimal Value { get; set; }

        [Column(TypeName = "decimal(10,5)")]
        public decimal ValueBalance { get; set; }

        public Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual Account? AccountNav { get; set; }
        public bool State { get; set; }
    }
}
