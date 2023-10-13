namespace  MiniDefinition.Shared
{
    public class LookupDto<TKey>
    {
        public TKey Id { get; set; }

        public string DisplayName { get; set; }

        public string Code { get; set; }
    }
}