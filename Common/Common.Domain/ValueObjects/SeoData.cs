
namespace Common.Domain.ValueObjects
{
    public class SeoData : ValueObject
    {
        private SeoData()
        {

        }

        public static SeoData CreateEmpty()
        {
            return new SeoData();
        }

        public SeoData(string? metaKeyWords, string metaDescription, string metaTitle, string canonical, string schema, bool indexPage)
        {
            MetaKeyWords = metaKeyWords;
            MetaDescription = metaDescription;
            MetaTitle = metaTitle;
            Canonical = canonical;
            Schema = schema;
            IndexPage = indexPage;
        }

        public string MetaTitle { get; private set; }

        public string MetaDescription { get; private set; }

        public string MetaKeyWords { get; private set; }

        public string Canonical { get; private set; }

        public string Schema { get; private set; }

        public bool IndexPage { get; private set; }
    }
}