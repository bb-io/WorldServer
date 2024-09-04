using System.Text;

namespace Apps.Worldserver.Utils;

public class MultipartFormBuilder
{
    static readonly string MultipartContentType = "multipart/form-data; boundary=";
    static readonly string FileHeaderTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: text/plain\r\n\r\n";
    static readonly string FormDataTemplate = "\r\n--{0}\r\nContent-Disposition: form-data; name=\"{1}\";\r\n\r\n{2}";

    public string ContentType { get; private set; }

    string Boundary { get; set; }

    Dictionary<string, (string, Stream)> FilesToSend { get; set; }
    Dictionary<string, string> FieldsToSend { get; set; }

    public MultipartFormBuilder()
    {
        Boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

        FilesToSend = new Dictionary<string, (string, Stream)>();
        FieldsToSend = new Dictionary<string, string>();

        ContentType = MultipartContentType + Boundary;
    }

    public void AddField(string key, string value)
    {
        FieldsToSend.Add(key, value);
    }

    public void AddFile(string key, string filename, Stream file)
    {
        FilesToSend.Add(key, (filename, file));
    }

    public MemoryStream GetStream()
    {
        var memStream = new MemoryStream();

        WriteFields(memStream);
        WriteStreams(memStream);
        WriteTrailer(memStream);

        memStream.Seek(0, SeekOrigin.Begin);

        return memStream;
    }

    void WriteFields(Stream stream)
    {
        if (FieldsToSend.Count == 0)
            return;

        foreach (var fieldEntry in FieldsToSend)
        {
            string content = string.Format(FormDataTemplate, Boundary, fieldEntry.Key, fieldEntry.Value);

            using (var fieldData = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                CopyTo(fieldData, stream, 81920);
            }
        }
    }

    void WriteStreams(Stream stream)
    {
        if (FilesToSend.Count == 0)
            return;

        foreach (var fileEntry in FilesToSend)
        {
            WriteBoundary(stream);

            string fileHeaderName = fileEntry.Key.Split(new char[] { '_' })[0];

            string header = string.Format(FileHeaderTemplate, fileHeaderName, fileEntry.Value.Item1);
            byte[] headerbytes = Encoding.UTF8.GetBytes(header);
            stream.Write(headerbytes, 0, headerbytes.Length);

            using (var fileData = new MemoryStream())
            {
                CopyTo(fileEntry.Value.Item2, stream, 81920);
            }
        }
    }

    void WriteBoundary(Stream stream)
    {
        byte[] boundarybytes = Encoding.UTF8.GetBytes("--" + Boundary + "\r\n");
        stream.Write(boundarybytes, 0, boundarybytes.Length);
    }

    void WriteTrailer(Stream stream)
    {
        byte[] trailer = Encoding.UTF8.GetBytes("\r\n--" + Boundary + "--\r\n");
        stream.Write(trailer, 0, trailer.Length);
    }

    void CopyTo(Stream source, Stream destination, int bufferSize)
    {
        byte[] array = new byte[bufferSize];
        int count;
        while ((count = source.Read(array, 0, array.Length)) != 0)
        {
            destination.Write(array, 0, count);
        }
    }
}

