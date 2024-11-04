using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public class MedicalRecordNumber : IEquatable<MedicalRecordNumber>
    {
        // The underlying value of the medical record number
        public string Value { get; }

        // Regular expression to validate the format YYYYMMnnnnnn
        private static readonly Regex ValidFormat = new Regex(@"^\d{4}(0[1-9]|1[0-2])\d{6}$", RegexOptions.Compiled);

        // Constructor that enforces validity of the value
        public MedicalRecordNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Medical record number cannot be null or empty.", nameof(value));

            if (!ValidFormat.IsMatch(value))
                throw new ArgumentException("Invalid medical record number format. Expected format is YYYYMMnnnnnn.", nameof(value));

            Value = value;
        }

        // Factory method to generate a new medical record number based on current date and sequential number
        public static MedicalRecordNumber GenerateNew(int sequentialNumber)
        {
            if (sequentialNumber < 0 || sequentialNumber > 999999)
                throw new ArgumentOutOfRangeException(nameof(sequentialNumber), "Sequential number must be between 0 and 999999.");

            // Get the current year and month
            string year = DateTime.Now.Year.ToString();
            string month = DateTime.Now.Month.ToString("D2"); // Format month as 2 digits

            // Format the sequential number to 6 digits (e.g., 000001, 000002, etc.)
            string sequentialPart = sequentialNumber.ToString("D6");

            // Concatenate to form YYYYMMnnnnnn
            string recordNumber = $"{year}{month}{sequentialPart}";

            return new MedicalRecordNumber(recordNumber);
        }

        // Override Equals method for value equality comparison
        public override bool Equals(object obj)
        {
            return Equals(obj as MedicalRecordNumber);
        }

        // Implement IEquatable interface for type-safe equality
        public bool Equals(MedicalRecordNumber other)
        {
            return other != null && Value == other.Value;
        }

        // Override GetHashCode to ensure value-based hash code
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        // ToString override for easy debugging and display
        public override string ToString()
        {
            return Value;
        }
    }
}