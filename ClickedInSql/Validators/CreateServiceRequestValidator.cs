using ClickedInSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Validators
{
    public class CreateServiceRequestValidator
    {
        public bool ValidateInterest(CreateServiceRequest request)
        {
            return string.IsNullOrEmpty(request.Name);
        }
    }
}
