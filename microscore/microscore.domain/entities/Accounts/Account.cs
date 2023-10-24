using microscore.domain.entities.abstractDomain;
using microscore.domain.entities.People;
using microscore.domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace microscore.domain.entities.Accounts
{
    public class Account : IEntity
    {
        public AccountType Type { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool State { get; set; }

        [StringLength(50)]
        public string Number { get; set; }

        [Column(TypeName = "decimal(10,5)")]
        public decimal Balance { get; set; }

        public Guid ClientId { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(ClientId))]
        public virtual Client? ClientNav { get; set; } = new Client();

        [JsonIgnore]
        [InverseProperty(nameof(Movement.AccountNav))]
        public ICollection<Movement>? MovementsNav { get; set; }

    }
}
