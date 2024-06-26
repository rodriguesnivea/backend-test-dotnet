﻿using ParkingAPI.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAPI.Entities
{
    public class AddressEntity : BaseEntity
    {

        [Required]
        [StringLength(100)]
        [Column("street")]
        public string Street { get; set; }
        [Required]
        [StringLength(100)]
        [Column("city")]
        public string City { get; set; }
        [Required]
        [StringLength(100)]
        [Column("state")]
        public string State { get; set; }
        [Required]
        [StringLength(100)]
        [Column("country")]
        public string Country { get; set; }

        [Required]
        [Column("number")]
        public int Number { get; set; }

        public AddressEntity()
        {
        }

        public AddressEntity(AddressModel model)
        {
            Id = model.Id;
            CreateAT = DateTime.Now;
            UpdateAt = DateTime.Now;
            Street = model.Street;
            City = model.City;
            State = model.State;
            Country = model.Country;
            Number = model.Number;
        }
    }

}
