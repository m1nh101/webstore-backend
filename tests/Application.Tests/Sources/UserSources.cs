namespace Application.Tests.Sources;

public static class UserSources
{
  public static List<object> TrueCaseDataSources = new()
  {
    new object[] { "usertest1", "usertest1@test.com", "1234abc" },
    new object[] { "usertest2",  "usertest2@test.com", "Mi@13123" },
    new object[] { "usertest3",  "usertest3@test.com", "aldjfhjk12" },
    new object[] { "usertest4",  "usertest4@test.com", ";aodf123" },
    new object[] { "usertest5",  "usertest5@test.com", "P2938P12" },
    new object[] { "usertest6",  "usertest6@test.com", "PKjhksd123" },
    new object[] { "usertest7",  "usertest7@test.com", "12983amn" },
    new object[] { "usertest8",  "usertest8@test.com", "123098admn" },
    new object[] { "usertest9",  "usertest9@test.com", "1203981ad" },
  };

  public static List<object> FailCaseDataSource = new()
  {
    new object[] { "", "usertest11@test.com", "1234abc", new List<string> { "username" } },
    new object[] { "usertest1", "usertest11@test.com", "1234abc", new List<string> { "username" } },
    new object[] { "usertest11", "usertest1@test.com", "1234abc", new List<string> { "email" } },
    new object[] { "usertest1", "usertest1@test.com", "1234abc", new List<string> { "email", "username" } },
    new object[] { "usertest1", "usertest1@test.com", "abc", new List<string> { "username", "email", "password" } },
    new object[] { "usertest11", "usertest11@test.com", "abcdajsdf", new List<string> { "username", "email", "password" } },
    new object[] { "usertest11", "usertest11@test.com", "", new List<string> { "password" } },
  };
}