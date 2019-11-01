//Copyright © alienquake@hotmail.com
using System;

namespace EETLauncherMVVM {
    public static class EETLauncherStringExtensions {
        // Why this is not build-in method of the .NET?
        public static bool ContainsIgnoreCase(string source, string toCheck) {
            return source?.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
