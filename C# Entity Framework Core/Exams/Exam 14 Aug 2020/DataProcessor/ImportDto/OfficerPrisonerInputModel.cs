using SoftJail.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ImportDto
{
    [XmlType("Officer")]
    public class OfficerPrisonerInputModel
    {
        [XmlElement("Name")]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        [XmlElement("Money")]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        public decimal Money { get; set; }

        [XmlElement("Position")]
        [EnumDataType(typeof(Position))]
        public string Position { get; set; }

        [XmlElement("Weapon")]
        [EnumDataType(typeof(Weapon))]
        public string Weapon { get; set; }

        [XmlElement("DepartmentId")]
        public int DepartmentId { get; set; }
        
        [XmlArray("Prisoners")]
        public PrisonerInputModel[] Prisoners { get; set; }
    }

    [XmlType("Prisoner")]
    public class PrisonerInputModel
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

    }
}
