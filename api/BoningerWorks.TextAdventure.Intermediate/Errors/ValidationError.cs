using BoningerWorks.TextAdventure.Core.Exceptions;
using BoningerWorks.TextAdventure.Core.Interfaces;
using System;

namespace BoningerWorks.TextAdventure.Intermediate.Errors
{
	public class ValidationError : IError
	{
		public static implicit operator GenericException<ValidationError>(ValidationError? validationError)
		{
			// Return generic exception
			return GenericException.Create(validationError);
		}

		public string Message { get; }

		public ValidationError(string message)
		{
			// Set message
			Message = message;
		}

		public GenericException<ValidationError> ToGenericException(GenericException<ValidationError>? innerException = null)
		{
			// Return generic exception
			return ToGenericException((Exception?)innerException);
		}
		public GenericException<ValidationError> ToGenericException(Exception? innerException = null)
		{
			// Check if inner exception is validation error generic exception
			if (innerException is GenericException<ValidationError> innerGenericException)
			{
				// Create message
				var message = Message;
				// Check if error exists and message exists
				if (innerGenericException.Error != null && !string.IsNullOrWhiteSpace(innerGenericException.Message))
				{
					// Add message
					message += " " + innerGenericException.Error.Message;
				}
				// Return generic exception
				return GenericException.Create(new ValidationError(message), innerGenericException);
			}
			// Return generic exception
			return GenericException.Create(this, innerException);
		}
	}
}
