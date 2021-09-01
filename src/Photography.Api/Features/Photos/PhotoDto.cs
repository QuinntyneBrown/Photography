using System;

namespace Photography.Api.Features
{
    public class PhotoDto
    {
        public Guid PhotoId { get; set; }
        public string Name { get; set; }
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
        public string Src { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
    }
}
