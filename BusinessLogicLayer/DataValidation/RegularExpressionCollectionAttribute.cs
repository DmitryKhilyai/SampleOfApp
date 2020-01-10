using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer.DataValidation
{
    /// <summary>
    /// Regular expression validation attribute for collection items
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class RegularExpressionCollectionAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets the regular expression pattern to use
        /// </summary>
        public string Pattern { get; }

        /// <summary>
        ///     Gets or sets the timeout to use when matching the regular expression pattern (in milliseconds)
        ///     (-1 means never timeout).
        /// </summary>
        public int MatchTimeoutInMilliseconds
        {
            get => _matchTimeoutInMilliseconds;
            set
            {
                _matchTimeoutInMilliseconds = value;
                _matchTimeoutSet = true;
            }
        }

        private int _matchTimeoutInMilliseconds = -1;
        private bool _matchTimeoutSet;

        private Regex Regex { get; set; }

        /// <summary>
        /// Constructor that accepts the regular expression pattern
        /// </summary>
        /// <param name="pattern">The regular expression to use. It cannot be null.</param>
        public RegularExpressionCollectionAttribute(string pattern)
            : base(() => "Items in collection {0} must match the regular expression '{1}'.")
        {
            this.Pattern = pattern;
        }

        /// <summary>
        /// Override of <see cref="ValidationAttribute.IsValid(object)"/>
        /// </summary>
        /// <remarks>This override performs the specific regular expression matching of the given <paramref name="value"/></remarks>
        /// <param name="value">The value to test for validity.</param>
        /// <returns><c>true</c> if the given value matches the current regular expression pattern</returns>
        /// <exception cref="InvalidOperationException"> is thrown if the current attribute is ill-formed.</exception>
        /// <exception cref="ArgumentException"> is thrown if the <see cref="Pattern"/> is not a valid regular expression.</exception>
        public override bool IsValid(object value)
        {
            this.SetupRegex();

            var collection = value as IEnumerable<string>;

            // Automatically pass if value is null. RequiredAttribute should be used to assert a value is not null.
            if (collection == null)
            {
                return true;
            }

            // We are looking for an exact match, not just a search hit. This matches what
            // the RegularExpressionValidator control does

            return !(from item in collection 
                let m = this.Regex.Match(item) 
                where !(m.Success && m.Index == 0 && m.Length == item.Length) 
                select item).Any();
        }

        /// <summary>
        /// Override of <see cref="ValidationAttribute.FormatErrorMessage"/>
        /// </summary>
        /// <remarks>This override provide a formatted error message describing the pattern</remarks>
        /// <param name="name">The user-visible name to include in the formatted message.</param>
        /// <returns>The localized message to present to the user</returns>
        /// <exception cref="InvalidOperationException"> is thrown if the current attribute is ill-formed.</exception>
        /// <exception cref="ArgumentException"> is thrown if the <see cref="Pattern"/> is not a valid regular expression.</exception>
        public override string FormatErrorMessage(string name)
        {
            this.SetupRegex();

            return string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, this.Pattern);
        }

        /// <summary>
        /// Sets up the <see cref="Regex"/> property from the <see cref="Pattern"/> property.
        /// </summary>
        /// <exception cref="ArgumentException"> is thrown if the current <see cref="Pattern"/> cannot be parsed</exception>
        /// <exception cref="InvalidOperationException"> is thrown if the current attribute is ill-formed.</exception>
        /// <exception cref="ArgumentOutOfRangeException"> thrown if <see cref="MatchTimeoutInMilliseconds" /> is negative (except -1),
        /// zero or greater than approximately 24 days </exception>
        private void SetupRegex()
        {
            if (this.Regex == null)
            {
                if (string.IsNullOrEmpty(this.Pattern))
                {
                    throw new InvalidOperationException("The pattern must be set to a valid regular expression.");
                }

                Regex = !_matchTimeoutSet && MatchTimeoutInMilliseconds == -1
                    ? new Regex(Pattern)
                    : Regex = new Regex(Pattern, default(RegexOptions), TimeSpan.FromMilliseconds((double)MatchTimeoutInMilliseconds));
            }
        }
    }
}
