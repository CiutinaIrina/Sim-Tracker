using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SimsTracker.Models
{
    public class ValidAgeAndAspiration : ValidationAttribute
    {
        public ValidAgeAndAspiration(params string[] propertyNames)
        {
            this.PropertyNames = propertyNames;
        }
        public string[] PropertyNames { get; private set; }

        public string GetErrorMessage(string error) => error;

        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {
            var properties = this.PropertyNames.Select(validationContext.ObjectType.GetProperty);
            var sim = (Sim)validationContext.ObjectInstance;
            int Age = sim.age;
            string LifeStage = sim.lifeStage;
            string Aspiration = sim.aspiration;
            string Status = sim.status;
            string results = "Valid";

            Console.WriteLine(LifeStage);
            switch (LifeStage)
            {
                case "Baby":
                    if (Age > 3) results = $"{LifeStage}s can have up to 2 days before aging up!";
                    if (Aspiration != "Grow Up") results = $"{LifeStage}s can only have the Grow Up aspiration!";
                    if (Status == "Deceased") results = $"You can't create deceased {LifeStage}s!";
                    break;
                case "Toddler":
                    if (Age > 4) results = $"{LifeStage}s can have up to 3 days before aging up!";
                    if (Aspiration != "Grow Up") results = $"{LifeStage}s can only have the Grow Up aspiration!";
                    if (Status == "Deceased") results = $"You can't create deceased {LifeStage}s!";
                    break;
                case "Child":
                    if (Age > 8) results = $"Children can have up to 7 days before aging up!";
                    if (Aspiration != "Grow Up") results = $"Children can only have the Grow Up aspiration!";
                    if (Status == "Deceased") results = $"You can't create deceased Children!";
                    break;
                case "Teen":
                    if (Age > 15) results = $"{LifeStage}s can have up to 14 days before aging up!";
                    if (Aspiration == "Grow Up") results = $"{LifeStage}s cannot have the Grow Up aspiration!";
                    break;
                case "Young Adult":
                    if (Age > 25) results = $"{LifeStage}s can have up to 24 days before graduating!";
                    if (Aspiration == "Grow Up") results = $"{LifeStage}s cannot have the Grow Up aspiration!";
                    break;
                case "Adult":
                    if (Age > 29) results = $"{LifeStage}s can have up to 28 days before aging up!";
                    if (Aspiration == "Grow Up") results = $"{LifeStage}s cannot have the Grow Up aspiration!";
                    break;
                case "Elder":
                    if (Age > 31) results = $"{LifeStage}s can only live up to 30 days!";
                    if (Aspiration == "Grow Up") results = $"{LifeStage}s cannot have the Grow Up aspiration!";
                    break;

            }
            if (results != "Valid")
            {
                return new ValidationResult(GetErrorMessage(results));
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
