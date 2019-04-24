using ClickedInSql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClickedInSql.Validators
{
    public class CreateUserRequestValidator
    {
        public bool Validate(CreateUserRequest requestToValidate)
        {
            return string.IsNullOrEmpty(requestToValidate.Name);
        }
    }
}
