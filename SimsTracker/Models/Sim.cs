using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SimsTracker.Models



{
    public class Sim
    {
        [Required]
        public int id { get; set; }
        [Required, StringLength(30, MinimumLength = 1), Display(Name = "First Name"), RegularExpression(@"[A-Z].*", ErrorMessage = "First character needs to be uppercase.")]
        public string firstName { get; set; }
        [Required, StringLength(30, MinimumLength = 1), Display(Name = "Last Name"), RegularExpression(@"[A-Z].*", ErrorMessage = "First character needs to be uppercase.")]
        public string lastName { get; set; }
        [Required, Display(Name = "Days")]
        [Range(1, 50)]
        public int age { get; set; }
        [Required, Display(Name = "Life Stage")]
        public string lifeStage { get; set; }
        [Required, Display(Name = "Gender")]
        public string gender { get; set; }
        [Required, Display(Name = "Aspiration")]
        public string aspiration { get; set; }
        [StringLength(200, MinimumLength = 1), Display(Name = "Useful Notes")]
        [ValidAgeAndAspiration("age", "lifeStage", "aspiration", "status", ErrorMessage = "Something went wrong?")]
        public string notes { get; set; }
        [Required, Display(Name = "Home Neighborhood"), RegularExpression(@"[A-Z].*", ErrorMessage = "First character needs to be uppercase.")]
        public string hoodName { get; set; }
        [Required, Display(Name = "Status")]
        
        public string status { get; set; }  


    }
}
