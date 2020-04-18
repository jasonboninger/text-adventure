using System;
using System.Runtime.Serialization;

namespace BoningerWorks.TextAdventure.Engine.Exceptions
{
	public static class GenericException
	{
		public static GenericException<TData> Create<TData>(TData data, Exception innerException = null)
		where TData : class
		{
			// Return generic exception
			return new GenericException<TData>(data, innerException);
		}
	}
	[Serializable]
	public class GenericException<TData> : Exception
	where TData : class
	{
		public bool HasData { get; }
		public new TData Data { get; }

		public GenericException() { }
		public GenericException(string message) : base(message) { }
		public GenericException(string message, Exception innerException) : base(message, innerException) { }
		public GenericException(TData data, Exception innerException = null) 
		: base($"A generic exception of type ({nameof(TData)}) occurred.", innerException)
		{
			// Set has data
			HasData = data != null;
			// Set data
			Data = data;
		}

		protected GenericException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
