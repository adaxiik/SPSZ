using System;
using System.ComponentModel.DataAnnotations;

namespace SPSZui.UIModel
{
    public class GradeModel
    {

        [Required (ErrorMessage = "Hodnota je povinný údaj")]
        public int Value { get; set; }
        [Required (ErrorMessage = "Váha je povinný údaj")]
        public int Weight { get; set; }
        [MaxLength(200, ErrorMessage = "Maximální délka popisu je 200 znaků")]
        public string Description { get; set; }

        [Required (ErrorMessage = "Předmět je povinný údaj")]
        public int SubjectID { get; set; }
    }
}
