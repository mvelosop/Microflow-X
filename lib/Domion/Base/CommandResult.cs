using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace Domion.Base
{
    public class CommandResult
    {
        private static readonly List<ValidationFailure> NoValidationFailures = new List<ValidationFailure>();

        public CommandResult()
        {
            Succeeded = false;
            ValidationFailures = NoValidationFailures;
        }

        public CommandResult(bool succeeded)
            : this()
        {
            Succeeded = succeeded;
        }

        public CommandResult(List<ValidationFailure> validationFailures)
            : this()
        {
            ValidationFailures = validationFailures;
        }

        public CommandResult(Exception ex)
            : this()
        {
            Exception = ex;
        }

        public Exception Exception { get; }

        public bool Succeeded { get; }

        public List<ValidationFailure> ValidationFailures { get; set; }

        public List<string> ValidationMessages => GetValidationMessages();

        private List<string> GetValidationMessages()
        {
            if (Succeeded) return Enumerable.Empty<string>().ToList();

            var messageList = new List<string>();

            if (ValidationFailures.Any())
            {
                messageList.AddRange(ValidationFailures.Select(vf => vf.ErrorMessage));
            }

            return messageList;
        }
    }

    public class CommandResult<TResult> : CommandResult
    {
        public CommandResult()
            :base(false)
        {
            
        }

        public CommandResult(TResult value)
            : base(true)
        {
            Value = value;
        }

        public CommandResult(List<ValidationFailure> validationFailures)
            : base(validationFailures)
        {
        }

        public CommandResult(Exception ex)
            : base(ex)
        {
        }

        public TResult Value { get; }
    }
}