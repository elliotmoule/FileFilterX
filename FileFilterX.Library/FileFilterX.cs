using System.Text.RegularExpressions;

namespace FileFilterX.Library
{
    public static class FileFilterX
    {
        public static string FilterBuilder(bool includeAllFiles, params Filter[] filters)
        {
            if (filters == null) return string.Empty;

            var filterString = string.Empty;

            var first = true;
            foreach (var filter in filters)
                if (ValidFilterName(filter.FileName) && filter.Extension != null && filter.Extension.Length > 0)
                    AddFilter(ref filterString, ref first, filter);

            if (includeAllFiles)
                filterString += "|All files (*.*)|*.*";

            return filterString;
        }

        private static void AddFilter(ref string filterString, ref bool first, Filter filter)
        {
            filterString += $"{(first ? string.Empty : "|")}{filter.FileName} (";

            first = false;

            var e1 = string.Empty;
            var e2 = string.Empty;
            foreach (var extension in filter.Extension)
                if (ValidExtension(extension, out string ext))
                {
                    e1 += $"{ext};";
                    e2 += $"{ext};";
                }

            filterString += $"{e1.Substring(0, e1.Length - 1)})|{e2}";
            filterString = filterString.Substring(0, filterString.Length - 1);
        }

        public static bool ValidFilterName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;
            if (Regex.IsMatch(fileName, "/[a-zA-Z]+/g")) return false;
            return true;
        }

        public static bool ValidExtension(string extension, out string corrected)
        {
            corrected = string.Empty;
            if (string.IsNullOrWhiteSpace(extension)) return false;

            var ext = Regex.Replace(extension, @"[^0-9a-zA-Z:,]+", string.Empty);

            if (string.IsNullOrWhiteSpace(ext)) return false;

            corrected = $"*.{ext}";

            return true;
        }
    }
}
