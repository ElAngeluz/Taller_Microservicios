using microscore.domain.entities.abstractDomain;
using microscore.domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace microscore.domain.entities.People
{
    [Table("person")]
    public class Person : IEntity
    {
        [StringLength(100)]
        public string Name { get; set; }

        public GenderType Gender { get; set; }

        public uint YearsOld { get; set; }

        [Key]
        public Guid Id { get; set; }

        [StringLength(15)]
        public string Identification { get; set; }

        [StringLength(100)]
        [JsonPropertyName("Direccion")]
        public string Address { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }

        [JsonIgnore]
        public Client? ClientNav { get; set; }
        public bool State { get; set; }
    }
}
