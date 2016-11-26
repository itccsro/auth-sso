using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace System.Text.RegularExpressions
{
    public static class MatchExtensions
    {
        public static string GetCapturingGroupValue(this Match match, string capturingGroup)
        {
            var groupIndex = Int32.Parse(capturingGroup.TrimStart('$'));
            var group = match.Groups[groupIndex];
            return group != null ? group.Value : String.Empty;
        }
    }
}
