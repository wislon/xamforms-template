namespace XamForms.Shared.Extensions
{
  public static class StringExtensions
  {
    public static bool IsEmpty(this string s)
    {
      return string.IsNullOrWhiteSpace(s);
    }
    public static bool IsNotEmpty(this string s)
    {
      return !IsEmpty(s);
    }
  }
}