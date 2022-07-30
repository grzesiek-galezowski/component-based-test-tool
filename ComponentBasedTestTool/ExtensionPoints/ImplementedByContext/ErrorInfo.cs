using System;
using System.Linq;

namespace ExtensionPoints.ImplementedByContext;

public class ErrorInfo : IEquatable<ErrorInfo>
{
  public static ErrorInfo With(string lastErrorFullText, string lastError)
  {
    return new ErrorInfo(lastErrorFullText, lastError);
  }


  private ErrorInfo(string lastErrorFullText, string lastError)
  {
    LastErrorFullText = lastErrorFullText;
    LastError = lastError;
  }

  public string LastErrorFullText { get; }

  public string LastError { get; }

  public bool Equals(ErrorInfo other)
  {
    if (ReferenceEquals(null, other)) return false;
    if (ReferenceEquals(this, other)) return true;
    return String.Equals(LastErrorFullText, other.LastErrorFullText) && String.Equals(LastError, other.LastError);
  }

  public override bool Equals(object obj)
  {
    if (ReferenceEquals(null, obj)) return false;
    if (ReferenceEquals(this, obj)) return true;
    if (obj.GetType() != this.GetType()) return false;
    return Equals((ErrorInfo) obj);
  }

  public override int GetHashCode()
  {
    unchecked
    {
      return ((LastErrorFullText != null ? LastErrorFullText.GetHashCode() : 0)*397) ^ (LastError != null ? LastError.GetHashCode() : 0);
    }
  }

  public static bool operator ==(ErrorInfo left, ErrorInfo right)
  {
    return Equals(left, right);
  }

  public static bool operator !=(ErrorInfo left, ErrorInfo right)
  {
    return !Equals(left, right);
  }

  public static ErrorInfo None()
  {
    return With(string.Empty, string.Empty);
  }

  private static string Format(Exception exception)
  {
    return exception.ToString().Split(
      new[] { Environment.NewLine },
      StringSplitOptions.RemoveEmptyEntries).First();
  }

  public static ErrorInfo From(Exception exception)
  {
    return With(exception.ToString(), Format(exception));
  }
}