using System;

namespace ComponentBasedTestTool.ViewModels.Ports
{
  public class Runnability : IEquatable<Runnability>
  {
    public bool Equals(Runnability other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return CanRun == other.CanRun;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Runnability) obj);
    }

    public override int GetHashCode()
    {
      return CanRun.GetHashCode();
    }

    public static bool operator ==(Runnability left, Runnability right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Runnability left, Runnability right)
    {
      return !Equals(left, right);
    }

    public Runnability(bool canRun, bool canStop)
    {
      CanRun = canRun;
      CanStop = canStop;
    }

    public bool CanRun { get; }

    public bool CanStop { get; }

    public static Runnability Runnable()
    {
      return new Runnability(true, false);
    }

    public static Runnability Unavailable()
    {
      return new Runnability(false, false);
    }

    public static Runnability InProgress()
    {
      return new Runnability(false, true);
    }
  }
}