using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimsTracker.Models
{
    public class Hood
    {
        public int id { get; set; }
        [Required, StringLength(30, MinimumLength = 1), Display(Name = "Hood Name"), RegularExpression(@"[A-Z].*", ErrorMessage = "First character needs to be uppercase.")]
        public string name { get; set; }
        [Display(Name = "Population")]
        public int population { get; set; }
        public List<Sim> Sims { get; set; }
        public Hood()
        {

        }
    }
}
