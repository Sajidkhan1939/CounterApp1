// Models/EmguFaceDetectionService.cs
using CounterApp1.Models;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.IO;


public class EmguFaceDetectionService : IFaceDetectionService
{
    public bool ContainsFace(Stream imageStream)
    {
        using (var imageBytes = new MemoryStream())
        {
            imageStream.CopyTo(imageBytes);
            byte[] imageData = imageBytes.ToArray();

            using (var image = new Mat())
            {
                CvInvoke.Imdecode(imageData, ImreadModes.Color, image);

                if (!image.IsEmpty)
                {
                    using (var faceCascade = new CascadeClassifier("haarcascade\\haarcascade_frontalface_default.xml"))
                    {
                        var grayImage = new UMat();
                        CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);
                        var faces = faceCascade.DetectMultiScale(grayImage);

                        return faces.Length > 0;
                    }
                }
            }
        }


        return false;
    }
}
