using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.ControllerLayer.JsonData
{
    public class BookingData : IValidatableObject
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = ValidateName(value, nameof(FirstName)); }
        }
        
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = ValidateName(value, nameof(LastName)); }
        }

        private string ValidateName(string name, string propertyName) =>
        string.IsNullOrEmpty(name) 
            ? throw new InvalidOperationException("could not set " + propertyName)
            : name;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new List<ValidationResult>();

            if(FirstName == null && LastName == null)
                results.Add(new ValidationResult("All given datapoints are null"));
            else if(FirstName is null || LastName is null)
                results.Add(new ValidationResult("One of the data entries are null"));
            
            return results;
        }
    }
}