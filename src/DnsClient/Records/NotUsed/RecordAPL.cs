namespace DnsClient.Records
{
	public class RecordAPL : RDataRecord
	{
        public RecordAPL(ResourceRecord resource, RecordReader recordReader)
            : base(resource, recordReader)
        {
        }

        public override string ToString()
        {
            return string.Format("not-used");
        }
    }
}