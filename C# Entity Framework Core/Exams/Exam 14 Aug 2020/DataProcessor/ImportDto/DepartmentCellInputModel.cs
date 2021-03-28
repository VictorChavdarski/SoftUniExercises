namespace SoftJail.DataProcessor.ImportDto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DepartmentCellInputModel
    {
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }

        public IEnumerable<CellInputModel> Cells { get; set; }
    }

    public class CellInputModel
    {
        [Range(1,1000)]
        public int CellNumber { get; set; }

        public bool HasWindow{ get; set; }
    }
}
