﻿using ParkingAPI.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAPI.Models
{
    public class CompanyModel : BaseModel
    {
        [Required()]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required()]
        [StringLength(14)]
        public string CNPJ { get; set; }

        [Required()]
        public int NumberMotorcycies { get; set; }

        [Required()]
        public int NumberCars { get; set; }

        [Required()]
        [RegularExpression(@"^\(?\d{2}\)?[-.\s]?\d{4,5}[-.\s]?\d{4}$")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required()]
        public AddressModel Address { get; set; }
    }
}
