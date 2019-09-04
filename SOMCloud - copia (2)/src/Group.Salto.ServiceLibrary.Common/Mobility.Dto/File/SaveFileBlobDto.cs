namespace Group.Salto.ServiceLibrary.Common.Mobility.Dto.File
{
    public class SaveFileBlobDto
    {
        public string Container { get; set; }
        public string Directory { get; set; }
        public string Name { get; set; }
        public byte[] FileBytes { get; set; }
    }
}
