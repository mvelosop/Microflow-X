using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domion.Base
{
    public class CommandResult<TResult>
    {
        private static List<ValidationResult> _noResults = new List<ValidationResult>();

        public CommandResult(TResult value)
        {
            Succeeded = true;
            Value = value;
            ValidationResults = _noResults;
        }

        public CommandResult(IEnumerable<ValidationResult> validationResults)
        {
            Succeeded = false;
            Value = default(TResult);
            ValidationResults = validationResults.ToList();
        }

        public CommandResult(Exception ex)
        {
            Succeeded = false;
            Value = default(TResult);
            ValidationResults = new List<ValidationResult>() { new ValidationResult(ex.Message) };
            Exception = ex;
        }

        public Exception Exception { get; }

        public bool Succeeded { get; }

        public List<ValidationResult> ValidationResults { get; }

        public TResult Value { get; }
    }
}