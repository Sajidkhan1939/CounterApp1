namespace CounterApp1.Models
{
    using System.IO;

    public interface IFaceDetectionService
    {
        bool ContainsFace(Stream imageStream);
    }
}
