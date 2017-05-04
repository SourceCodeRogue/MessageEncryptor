using System;

namespace MessageEncryptor.Core.Utils
{
    public class DesignByContract
    {
        private static TException CreateExceptionWithParameterName<TException>(string parameterName)
            where TException : Exception, new()
        {
            var exceptionType = typeof(TException);
            TException exceptionToReturn = null;

            if (exceptionType.Equals(typeof(ArgumentNullException)))
                exceptionToReturn = new ArgumentNullException(parameterName) as TException;
            else if (exceptionType.Equals(typeof(ArgumentOutOfRangeException)))
                exceptionToReturn = new ArgumentOutOfRangeException(parameterName) as TException;
            else
                exceptionToReturn = new TException();
            
            return exceptionToReturn;
        }

        public static void ThrowIf<TException>(bool failCondition, string parameterName)
            where TException : Exception, new()
        {
            if (failCondition)
            {
                var exceptionToThrow = CreateExceptionWithParameterName<TException>(parameterName);
                throw exceptionToThrow;
            }
        }

        public static void ThrowIf<TException>(bool failCondition)
            where TException : Exception, new()
        {
            if (failCondition)
                throw new TException();
        }

        public static void ThrowIf<TException>(bool failCondition, Func<TException> exceptionCreationDelegate)
            where TException : Exception, new()
        {
            if (exceptionCreationDelegate == null)
                throw new ArgumentNullException(nameof(exceptionCreationDelegate));

            if (failCondition)
            {
                var exceptionToThrow = exceptionCreationDelegate();
                throw exceptionToThrow;
            }
        }
    }
}
