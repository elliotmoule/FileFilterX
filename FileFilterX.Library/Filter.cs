namespace FileFilterX.Library
{
    public class Filter
    {
        public Filter(string fileName, params string[] extensions)
        {
            FileName = fileName;
            Extension = extensions;
        }

        public Filter(CommonFilters commonFilter)
        {
            switch (commonFilter)
            {
                case CommonFilters.JPG:
                    FileName = "JPEG";
                    Extension = new string[] { "jpg", "jpeg" };
                    break;
                case CommonFilters.PNG:
                    FileName = "PNG";
                    Extension = new string[] { "png" };
                    break;
                case CommonFilters.TXT:
                    FileName = "TXT";
                    Extension = new string[] { "txt" };
                    break;
                case CommonFilters.DOC:
                    FileName = "Doc";
                    Extension = new string[] { "docx", "doc" };
                    break;
            }
        }

        public string[] Extension { get; private set; }
        public string FileName { get; private set; } = string.Empty;
    }
}
