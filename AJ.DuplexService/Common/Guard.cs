// Source: https://github.com/ajdotnet/AJ.Common
using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace AJ.DuplexService.Common
{
    /// <summary>
    /// Helper class to provide parameter checks.
    /// See https://ajdotnet.wordpress.com/2009/08/01/posting-guards-guard-classes-explained/ for background.
    /// </summary>
    public static class Guard
    {
        /// <summary>Checks if a given value is not null and throws a respective exception otherwise.</summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        [DebuggerStepThrough]
        static public void AssertNotNull([ValidatedNotNull]object arg, string paramName)
        {
            if (arg == null)
                ThrowArgumentNullException(paramName, null);
        }

        /// <summary>Checks if a given value is not null and throws a respective exception otherwise.</summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        [DebuggerStepThrough]
        static public void AssertNotNull([ValidatedNotNull]object arg, string paramName, string message)
        {
            if (arg == null)
                ThrowArgumentNullException(paramName, message);
        }

        /// <summary>Checks if a given value is not null and throws a respective exception otherwise.</summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An Object array containing zero or more objects to format.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        [DebuggerStepThrough]
        static public void AssertNotNull([ValidatedNotNull]object arg, string paramName, string format, params object[] args)
        {
            if (arg == null)
                ThrowArgumentNullException(paramName, SafeFormat(format, args));
        }

        /// <summary>Checks if a given value is not null and throws a respective exception otherwise.</summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertNotEmpty([ValidatedNotNull]string arg, string paramName)
        {
            AssertNotNull(arg, paramName);
            if (arg.Length == 0)
                ThrowArgumentOutOfRangeException(paramName, arg, null);
        }

        /// <summary>
        /// Checks if a given value is neither null nor empty and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertNotEmpty([ValidatedNotNull]string arg, string paramName, string message)
        {
            AssertNotNull(arg, paramName, message);
            if (arg.Length == 0)
                ThrowArgumentOutOfRangeException(paramName, arg, message);
        }

        /// <summary>
        /// Checks if a given value is neither null nor empty and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An Object array containing zero or more objects to format.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertNotEmpty([ValidatedNotNull]string arg, string paramName, string format, params object[] args)
        {
            AssertNotNull(arg, paramName, format, args);
            if (arg.Length == 0)
                ThrowArgumentOutOfRangeException(paramName, arg, SafeFormat(format, args));
        }

        /// <summary>
        /// Checks if a given value is neither null nor empty and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertNotEmpty([ValidatedNotNull]ICollection arg, string paramName)
        {
            AssertNotNull(arg, paramName);
            if (arg.Count == 0)
                ThrowArgumentOutOfRangeException(paramName, arg, null);
        }

        /// <summary>
        /// Checks if a given value is neither null nor empty and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertNotEmpty([ValidatedNotNull]ICollection arg, string paramName, string message)
        {
            AssertNotNull(arg, paramName, message);
            if (arg.Count == 0)
                ThrowArgumentOutOfRangeException(paramName, arg, message);
        }

        /// <summary>
        /// Checks if a given value is neither null nor empty and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An Object array containing zero or more objects to format. </param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertNotEmpty([ValidatedNotNull]ICollection arg, string paramName, string format, params object[] args)
        {
            AssertNotNull(arg, paramName, format, args);
            if (arg.Count == 0)
                ThrowArgumentOutOfRangeException(paramName, arg, SafeFormat(format, args));
        }

        /// <summary>
        /// Checks if a given value is neither null nor empty and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertNotEmpty([ValidatedNotNull]Guid arg, string paramName)
        {
            if (arg == Guid.Empty)
                ThrowArgumentOutOfRangeException(paramName, arg, null);
        }

        /// <summary>
        /// Checks if a given value is neither null nor empty and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertNotEmpty([ValidatedNotNull]Guid arg, string paramName, string message)
        {
            if (arg == Guid.Empty)
                ThrowArgumentOutOfRangeException(paramName, arg, message);
        }

        /// <summary>
        /// Checks if a given value is neither null nor empty and throws a respective exception otherwise.
        /// </summary>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An Object array containing zero or more objects to format.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertNotEmpty([ValidatedNotNull]Guid arg, string paramName, string format, params object[] args)
        {
            if (arg == Guid.Empty)
                ThrowArgumentOutOfRangeException(paramName, arg, SafeFormat(format, args));
        }

        /// <summary>Checks if a given condition is met and throws a respective exception otherwise.</summary>
        /// <param name="condition">The condition that has to be true, otherwise an exception is thrown.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An Object array containing zero or more objects to format.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertCondition(bool condition, string paramName, object arg,
        string format, params object[] args)
        {
            if (!condition)
                ThrowArgumentOutOfRangeException(paramName, arg, SafeFormat(format, args));
        }

        /// <summary>Checks if a given condition is met and throws a respective exception otherwise.</summary>
        /// <param name="condition">The condition that has to be true, otherwise an exception is thrown.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertCondition(bool condition, string paramName, object arg, string message)
        {
            if (!condition)
                ThrowArgumentOutOfRangeException(paramName, arg, message);

        }

        /// <summary>Checks if a given condition is met and throws a respective exception otherwise.</summary>
        /// <param name="condition">The condition that has to be true, otherwise an exception is thrown.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="arg">The value of the parameter that should be checked.</param>
        /// <exception cref="ArgumentNullException">Thrown when the value is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is empty.</exception>
        [DebuggerStepThrough]
        static public void AssertCondition(bool condition, string paramName, object arg)
        {
            if (!condition)
                // Condition not met!
                ThrowArgumentOutOfRangeException(paramName, arg, Properties.Resources.MsgConditionFailed);
        }

        /// <summary>
        /// <para>
        /// Helper method: Replaces <c>String.Format(string, object[] args)</c>.
        /// </para>
        /// <para>
        /// Replaces each format item in a specified String with the text equivalent of
        /// a corresponding Object instance in a specified array.
        /// </para>
        /// <para>
        /// Contrary to <c>String.Format(string, object[] args)</c>, this method does not throw an exception.
        /// Rather in case of an error it returns a descriptive string describing that error.
        /// This is done to avoid having a subsequent string format error obscure the original
        /// error that caused this call in the first place.
        /// </para>
        /// </summary>
        /// <param name="format">A composite format string.</param>
        /// <param name="args">An Object array containing zero or more objects to format.</param>
        /// <returns>
        /// A copy of format in which the format items have been replaced by the String
        /// equivalent of the corresponding instances of Object in args.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
            Justification = "No exceptions during error handling!")]
        static string SafeFormat(string format, params object[] args)
        {
            try
            {
                return string.Format(CultureInfo.CurrentCulture, format, args);
            }
            catch (Exception ex)
            {
                // Format failure:\n    Exception={0}\n    Format=\"{1}\"\n    args.Length={2}
                return string.Format(CultureInfo.CurrentCulture, Properties.Resources.MsgInvalidFormatString,
                    ex.Message, format, args.Length);
            }
        }

        /// <summary>report invalid switch value.</summary>
        /// <param name="variable">The variable.</param>
        /// <param name="value">The value.</param>
        /// <exception cref="InvalidOperationException"></exception>
        [DebuggerStepThrough]
        public static void InvalidSwitchValue(string variable, object value)
        {
            // Invalid switch value '{1}' for '{0}'.
            string msg = string.Format(CultureInfo.CurrentCulture, Properties.Resources.MsgInvalidSwitchValue,
                variable ?? Properties.Resources.ValueNoParam, value ?? Properties.Resources.ValueNull);
            throw new InvalidOperationException(msg);
        }

        private static void ThrowArgumentNullException(string paramName, string message)
        {
            if (message == null)
                // Argument ‘{0}’ should not be NULL!
                message = SafeFormat(Properties.Resources.MsgParamNull, paramName ?? Properties.Resources.ValueNoParam);
            throw new ArgumentNullException(paramName ?? Properties.Resources.ValueNoParam, message);
        }

        private static void ThrowArgumentOutOfRangeException(string paramName, object actualValue, string message)
        {
            if (message == null)
                // Argument ‘{0}’ should not be empty!
                message = SafeFormat(Properties.Resources.MsgParamEmpty, paramName ?? Properties.Resources.ValueNoParam);
            throw new ArgumentOutOfRangeException(paramName ?? Properties.Resources.ValueNoParam, actualValue, message);
        }

        /// <summary>
        /// Workaround to support FxCop; 
        /// see http://connect.microsoft.com/VisualStudio/feedback/details/567917/code-analysis-doesnt-resolve-copy-local-false-references
        /// </summary>
        [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
        internal sealed class ValidatedNotNullAttribute : Attribute
        {
        }
    }
}
