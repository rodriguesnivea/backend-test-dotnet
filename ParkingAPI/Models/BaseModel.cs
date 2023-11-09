using System.ComponentModel.DataAnnotations;
using System;

namespace ParkingAPI.Models
{
    public abstract class BaseModel //abstract - essa classe não pode ser instanciada, outra classe irá herda-lá e será instanciada.
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy HH: mm}")]
        private DateTime? _createAT;
        public DateTime? CreateAT
        {
            get { return _createAT; }
            set { _createAT = (value == null ? DateTime.UtcNow : value); }

        }
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy HH: mm}")]
        public DateTime? UpdateAt { get; set; }
    }
}
