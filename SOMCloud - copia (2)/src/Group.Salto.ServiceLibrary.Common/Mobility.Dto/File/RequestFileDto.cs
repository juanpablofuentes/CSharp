namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.File
{
    public class RequestFileDto
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
        public bool CheckedForDelete { get; set; }
    }
}
