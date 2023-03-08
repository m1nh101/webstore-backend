using System.Net;

namespace Application.Tests.Sources;

public static class UserSources
{
  public static List<object> TrueCaseDataSources = new()
  {
    new object[] { "usertest1", "usertest1@test.com", "1234abc", "User Test 1" },
    new object[] { "usertest2",  "usertest2@test.com", "Mi@13123", "User Test 2" },
    new object[] { "usertest3",  "usertest3@test.com", "aldjfhjk12", "User Test 3" },
    new object[] { "usertest4",  "usertest4@test.com", ";aodf123", "User Test 4" },
    new object[] { "usertest5",  "usertest5@test.com", "P2938P12", "User Test 5" },
    new object[] { "usertest6",  "usertest6@test.com", "PKjhksd123", "User Test 6" },
    new object[] { "usertest7",  "usertest7@test.com", "12983amn", "User Test 7" },
    new object[] { "usertest8",  "usertest8@test.com", "123098admn", "User Test 8" },
    new object[] { "usertest9",  "usertest9@test.com", "1203981ad", "User Test 9" },
  };

  public static List<object> FailCaseDataSource = new()
  {
    new object[] { "", "usertest11@test.com", "1234abc", "User Test 1", new List<string> { "username" } },
    new object[] { "usertest1", "usertest11@test.com", "1234abc", "User Test 1", new List<string> { "username" } },
    new object[] { "usertest11", "usertest1@test.com", "1234abc", "User Test 1", new List<string> { "email" } },
    new object[] { "usertest1", "usertest1@test.com", "1234abc", "User Test 1", new List<string> { "email", "username" } },
    new object[] { "usertest1", "usertest1@test.com", "abc", "User Test 1", new List<string> { "password" } },
    new object[] { "usertest1", "usertest1@test.com", "abcdajsdf", "User Test 1", new List<string> { "password" } },
    new object[] { "usertest11", "usertest11@test.com", "", "User Test 1", new List<string> { "password" } },
  };

  public static List<object> Credentials = new()
  {
    new Object[] { "usertest1", "1234abc", true },
    new Object[] { "usertest1@test.com", "1234abc", true },
    new Object[] { "usertest23", "1234abc", false },
    new Object[] { "usertest1", "1234abcd", false },
    new Object[] { "usertest1", "", false },
    new Object[] { "user", "1234abc", false },
    new Object[] { "", "1234abc", false },
  };
}