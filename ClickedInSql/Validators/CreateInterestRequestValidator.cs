using ClickedInSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Validators
{
    public class CreateInterestRequestValidator
    {
        public bool ValidateInterest(CreateInterestRequest request)
        {
            return string.IsNullOrEmpty(request.Name);
        }
    }
}
