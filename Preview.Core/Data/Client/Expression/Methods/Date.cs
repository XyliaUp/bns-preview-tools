using Xylia.Preview.Data.Models;
using Xylia.Preview.Common.Extension;

namespace Xylia.Preview.Data.Client;
internal partial class BsonExpressionMethods
{
    #region Year/Month/Day/Hour/Minute/Second

    /// <summary>
    /// Get year from date
    /// </summary>
    public static AttributeValue YEAR(AttributeValue value)
    {
        if (value.IsDateTime) return value.AsDateTime.Year;

        return null;
    }

    /// <summary>
    /// Get month from date
    /// </summary>
    public static AttributeValue MONTH(AttributeValue value)
    {
        if (value.IsDateTime) return value.AsDateTime.Month;

        return null;
    }

    /// <summary>
    /// Get day from date
    /// </summary>
    public static AttributeValue DAY(AttributeValue value)
    {
        if (value.IsDateTime) return value.AsDateTime.Day;

        return null;

    }

    /// <summary>
    /// Get hour from date
    /// </summary>
    public static AttributeValue HOUR(AttributeValue value)
    {
        if (value.IsDateTime) return value.AsDateTime.Hour;

        return null;
    }

    /// <summary>
    /// Get minute from date
    /// </summary>
    public static AttributeValue MINUTE(AttributeValue value)
    {
        if (value.IsDateTime) return value.AsDateTime.Minute;

        return null;

    }

    /// <summary>
    /// Get seconds from date
    /// </summary>
    public static AttributeValue SECOND(AttributeValue value)
    {
        if (value.IsDateTime) return value.AsDateTime.Second;

        return null;
    }

    #endregion

    #region Date Functions

    /// <summary>
    /// Add an interval to date. Use dateInterval: "y" (or "year"), "M" (or "month"), "d" (or "day"), "h" (or "hour"), "m" (or "minute"), "s" or ("second")
    /// </summary>
    public static AttributeValue DATEADD(AttributeValue dateInterval, AttributeValue number, AttributeValue value)
    {
        if (dateInterval.IsString && number.IsNumeric && value.IsDateTime)
        {
            var datePart = dateInterval.AsString;
            var numb = number.AsInt32;
            var date = value.AsDateTime;

            datePart = datePart == "M" ? "month" : datePart.ToLower();

            if (datePart == "y" || datePart == "year") return date.AddYears(numb);
            else if (datePart == "month") return date.AddMonths(numb);
            else if (datePart == "d" || datePart == "day") return date.AddDays(numb);
            else if (datePart == "h" || datePart == "hour") return date.AddHours(numb);
            else if (datePart == "m" || datePart == "minute") return date.AddMinutes(numb);
            else if (datePart == "s" || datePart == "second") return date.AddSeconds(numb);
        }

        return null;
    }

    /// <summary>
    /// Returns an interval about 2 dates. Use dateInterval: "y|year", "M|month", "d|day", "h|hour", "m|minute", "s|second"
    /// </summary>
    public static AttributeValue DATEDIFF(AttributeValue dateInterval, AttributeValue starts, AttributeValue ends)
    {
        if (dateInterval.IsString && starts.IsDateTime && ends.IsDateTime)
        { 
            var datePart = dateInterval.AsString;
            var start = starts.AsDateTime;
            var end = ends.AsDateTime;

            datePart = datePart == "M" ? "month" : datePart.ToLower();

            if (datePart == "y" || datePart == "year") return start.YearDifference(end);
            else if (datePart == "month") return start.MonthDifference(end);
            else if (datePart == "d" || datePart == "day") return Convert.ToInt32(Math.Truncate(end.Subtract(start).TotalDays));
            else if (datePart == "h" || datePart == "hour") return Convert.ToInt32(Math.Truncate(end.Subtract(start).TotalHours));
            else if (datePart == "m" || datePart == "minute") return Convert.ToInt32(Math.Truncate(end.Subtract(start).TotalMinutes));
            else if (datePart == "s" || datePart == "second") return Convert.ToInt32(Math.Truncate(end.Subtract(start).TotalSeconds));
        }

        return null;
    }

    /// <summary>
    /// Convert UTC date into LOCAL date
    /// </summary>
    public static AttributeValue TO_LOCAL(AttributeValue date)
    {
        if (date.IsDateTime)
        {
            return date.AsDateTime.ToLocalTime();
        }

        return null;
    }

    /// <summary>
    /// Convert LOCAL date into UTC date
    /// </summary>
    public static AttributeValue TO_UTC(AttributeValue date)
    {
        if (date.IsDateTime)
        {
            return date.AsDateTime.ToUniversalTime();
        }

        return null;
    }

    #endregion
}
