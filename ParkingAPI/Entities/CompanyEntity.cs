﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public virtual AddressEntity Address { get; set; }

        [Required()]
        [Column("address_id")]
        [ForeignKey("Address")]
        public Guid AddressId { get; set; }

        public CompanyEntity()
        {
        }

        public CompanyEntity(CompanyModel model)
        {
            Name = model.Name;
            CNPJ = model.CNPJ;
            NumberMotorcycies = model.NumberMotorcycies;
            NumberCars = model.NumberCars;
            Phone = model.Phone;
            Address = new AddressEntity(model.Address);
            AddressId = Address.Id;
            CreateAT = DateTime.Now;
            UpdateAt = DateTime.Now;                  
        }
    }
}
