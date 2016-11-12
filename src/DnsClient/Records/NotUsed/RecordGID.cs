namespace DnsClient.Records
{
    public class RecordGID : RDataRecord
    {
        public RecordGID(ResourceRecord resource, RecordReader recordReader)
            : base(resource, recordReader)
        {
        }

        public override string ToString()
        {
            return string.Format("not-used");
        }
    }
}