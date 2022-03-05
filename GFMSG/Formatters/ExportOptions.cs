namespace GFMSG
{
    public struct ExportOptions
    {
        public StringOptions StringOptions { get; set; }

        public string Extension { get; set; }

        public bool IncludeId { get; set; }

        public bool Merged { get; set; }

        public string Path { get; set; }
    }
}
