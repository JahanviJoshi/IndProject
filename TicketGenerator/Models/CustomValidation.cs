using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TicketGenerator.Models
{
    public class NameRegrex: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Regex re = new Regex("^[A-Z][a-zA-Z]*$");
            if (re.IsMatch(Convert.ToString(value)))
            {
                return true;
            }
            return false;
        }

        public class PhoneRegrex : ValidationAttribute
        {

            public override bool IsValid(object value)
            {
                Regex re = new Regex(@"\d{10}$");
                if (re.IsMatch(Convert.ToString(value)))
                {
                    return true;
                }
                return false;
            }

        }

        public class EmailRegrex : ValidationAttribute
        {

            public override bool IsValid(object value)
            {
                Regex re = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (re.IsMatch(Convert.ToString(value)))
                {
                    return true;
                }
                return false;
            }

        }

        public class PasswordRegrex : ValidationAttribute
        {

            public override bool IsValid(object value)
            {
                Regex re = new Regex("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]){6,20}$");
                if (re.IsMatch(Convert.ToString(value)))
                {
                    return true;
                }
                return false;
            }

        }
    }
    
}
