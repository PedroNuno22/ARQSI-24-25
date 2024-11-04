using System;

namespace Domain.ValueObjects
{
    public class TimeSlot : IEquatable<TimeSlot>
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        // Constructor
        public TimeSlot(DateTime startTime, DateTime endTime)
        {
            if (endTime <= startTime)
                throw new ArgumentException("End time must be after start time.");

            StartTime = startTime;
            EndTime = endTime;
        }

        // Duration in minutes
        public int DurationInMinutes => (int)(EndTime - StartTime).TotalMinutes;

        // Check if this TimeSlot overlaps with another TimeSlot
        public bool OverlapsWith(TimeSlot other)
        {
            return StartTime < other.EndTime && EndTime > other.StartTime;
        }

        // Equality methods to treat TimeSlot as a Value Object
        public bool Equals(TimeSlot other)
        {
            if (other is null) return false;
            return StartTime == other.StartTime && EndTime == other.EndTime;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as TimeSlot);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartTime, EndTime);
        }

        // String representation for debugging
        public override string ToString()
        {
            return $"{StartTime:yyyy-MM-dd HH:mm} - {EndTime:yyyy-MM-dd HH:mm}";
        }
    }
}