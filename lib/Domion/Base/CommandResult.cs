using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domion.Base
{
    public class CommandResult
    {
        private static readonly List<ValidationResult> NoResults = new List<ValidationResult>();

        public CommandResult(bool succeeded)
        {
            Succeeded = succeeded;
            ValidationResults = NoResults;
        }

        public CommandResult(IEnumerable<ValidationResult> validationResults)
        {
            Succeeded = false;
            ValidationResults = validationResults.ToList();
        }

        public CommandResult(Exception ex)
        {
            Succeeded = false;
            ValidationResults = NoResults;
            Exception = ex;
        }

        public Exception Exception { get; }

        public bool Succeeded { get; }

        public List<ValidationResult> ValidationResults { get; }
    }

    public class CommandResult<TResult> : CommandResult
    {
        public CommandResult(TResult value)
            : base(true)
        {
            Value = value;
        }

        public CommandResult(IEnumerable<ValidationResult> validationResults)
            : base(validationResults)
        {
            Value = default(TResult);
        }

        public CommandResult(Exception ex)
            : base(ex)
        {
            Value = default(TResult);
        }

        public TResult Value { get; }
    }
}