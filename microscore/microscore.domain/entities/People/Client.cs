using microscore.domain.entities.abstractDomain;
using microscore.domain.entities.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace microscore.domain.entities.People
{
    [Table("cliente")]
    public class Client : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ClientId { get; set; }

        [StringLength(50)]
        public string Password { get; set; }

        public bool State { get; set; }

        public Guid PersonId { get; set; }

        [ForeignKey(nameof(PersonId))]
        public virtual Person? PersonNav { get; set; } = new Person();

        [JsonIgnore]
        [InverseProperty(nameof(Account.ClientNav))]
        public ICollection<Account>? AccountsNav { get; set; }

    }
}
