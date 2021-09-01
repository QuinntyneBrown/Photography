using System;

namespace Photography.Api.Models
{
    public class Photo
    {
        public Guid PhotoId { get; private set; }
        public string Name { get; private set; }
        public byte[] Bytes { get; private set; }
        public string ContentType { get; private set; }
        public float Height { get; private set; }
        public float Width { get; private set; }

        public Photo(string name)
        {
            Name = name;
        }

        private Photo()
        {

        }

        public void Update(byte[] bytes, string contentType, float height, float width)
        {
            Bytes = bytes;
            ContentType = contentType;
            Height = height;
            Width = width;
        }
    }
}
