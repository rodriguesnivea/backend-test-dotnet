using Microsoft.AspNetCore.Components;
using ParkingAPI.Mappers;
using ParkingAPI.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAPI.Entities
{
    public class CompanyEntity : BaseEntity
    {
        [Required()]
        [MaxLength(100)]
        [MinLength(3)]
        [Column("name")]
        public string Name { get; set; }

        [Required()]
        [StringLength(14)]
        [RegularExpression(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}-\d{2}$")]
        [Column("cnpj")]
        public string CNPJ { get; set; }

        [Required()]
        [Column("number_motorcycies")]
        public int NumberMotorcycies { get; set; }

        [Required()]
        [Column("number_cars")]
        public int NumberCars { get; set; }

        [Required()]
        [RegularExpression(@"^\(?\d{2}\)?[-.\s]?\d{4,5}[-.\s]?\d{4}$")]
        [DataType(DataType.PhoneNumber)]
        [Column("phone")]
        public string Phone { get; set; }

        [Required()]
        [Column("address")]
        public AddressEntity Address { get; set; }

        [Required()]
        [Column("address_Id")]
        public Guid AddressEntityId { get; set; }

        public static CompanyEntity ToEntity(CompanyModel model)
        {
            var entity = CompanyMap.ModelToEntity(model);
            entity.Id = Guid.NewGuid();
            var adressId = Guid.NewGuid();
            entity.AddressEntityId = adressId;
            entity.Address.Id = adressId;
            entity.CreateAT = DateTime.Now;
            entity.UpdateAt = DateTime.Now;
            entity.Address.CreateAT = DateTime.Now;
            entity.Address.UpdateAt = DateTime.Now;
            return entity;
        }
    }
}
