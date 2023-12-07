using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingAPI.Entities
{
    public abstract class BaseEntity //abstract - essa classe não pode ser instanciada, outra classe irá herda-lá e será instanciada.
    {      
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy HH: mm}")]
        private DateTime? _createAT;

        [Column("start_date")]
        public DateTime? CreateAT
        {
            get { return _createAT; }
            set { _createAT = (value == null ? DateTime.UtcNow : value); }

        }
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy HH: mm}")]
        [Column("modify_date")]
        public DateTime? UpdateAt { get; set; }
    }
}
