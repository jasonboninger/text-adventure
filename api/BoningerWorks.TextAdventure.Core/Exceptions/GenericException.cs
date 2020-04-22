using BoningerWorks.TextAdventure.Core.Interfaces;
using System;
using System.Runtime.Serialization;

namespace BoningerWorks.TextAdventure.Core.Exceptions
{
	public static class GenericException
	{
		public static GenericException<TError> Create<TError>(TError error)
		where TError : class, IError
		{
			// Return generic exception
			return new GenericException<TError>(error);
		}
		public static GenericException<TError> Create<TError>(TError error, Exception innerException)
		where TError : class, IError
		{
			// Return generic exception
			return new GenericException<TError>(error, innerException);
		}
	}
	[Serializable]
	public class GenericException<TError> : Exception
	where TError : class, IError
	{
		public TError Error { get; }

		public GenericException() { }
		public GenericException(string message) : base(message) { }
		public GenericException(string message, Exception innerException) : base(message, innerException) { }
		public GenericException(TError error) : base(error?.Message)
		{
			// Set error
			Error = error;
		}
		public GenericException(TError error, Exception innerException) : base(error?.Message, innerException)
		{
			// Set error
			Error = error;
		}

		protected GenericException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
