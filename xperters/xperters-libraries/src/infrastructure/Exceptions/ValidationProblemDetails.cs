using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace xperters.infrastructure.Exceptions
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public ICollection<ValidationError> ValidationErrors { get; set; }
    }
    
}