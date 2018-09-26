using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using ImageQueue.Models;

namespace ImageQueue.Helpers
{
    public static class ImageHelper
    {
        public static Stream TextToImage(Stream imageStream, double temperature)
        {
            BierRapportModel model = ImageHelper.setImageText(temperature);
            Bitmap bitMapImage = ImageHelper.drawOnImage(imageStream, model);
            MemoryStream stream = new MemoryStream();
            bitMapImage.Save(stream, ImageFormat.Png);
            stream.Position = 0;
            return stream;
        }

        public static BierRapportModel setImageText(double temperature)
        {
            double beerTemperature = 15;
            string displayTemperature = null;
            string temperatureCondition = null;
            string beerStatus = null;
            if (temperature < beerTemperature)
            {
                displayTemperature = string.Format("The temperature at this location is {0}°C", temperature);
                temperatureCondition = string.Format("The temperature is colder than {0}°C", beerTemperature);
                beerStatus = "The temperature is not good to drink a beer !";
            }
            else
            {
                displayTemperature = string.Format("The temperature at this location is {0}°C", temperature);
                if (temperature == beerTemperature)
                {
                    temperatureCondition = string.Format("The temperature is equal than {0}°C", beerTemperature);
                }
                else
                {
                    temperatureCondition = string.Format("The temperature is warmer  than {0}°C", beerTemperature);
                }
                beerStatus = "The temperature is great to drink a beer !";
            }
            return new BierRapportModel(displayTemperature, temperatureCondition, beerStatus);
        }

        public static Bitmap drawOnImage(Stream imageStream, BierRapportModel model)
        {
            Bitmap bitMapImage = new Bitmap(imageStream);
            Graphics graphicImage = Graphics.FromImage(bitMapImage);
            graphicImage.SmoothingMode = SmoothingMode.AntiAlias;
            graphicImage.DrawString(model.displayTemperature, new Font(
                "Arial",
                12,
                FontStyle.Bold),
                SystemBrushes.WindowText,
                new Point(50, 50)
            );
            graphicImage.DrawString(model.temperatureCondition, new Font(
                "Arial",
                12,
                FontStyle.Bold),
                SystemBrushes.WindowText,
                new Point(50, 70)
            );
            graphicImage.DrawString(model.beerStatus, new Font(
                "Arial",
                12,
                FontStyle.Bold),
                SystemBrushes.WindowText,
                new Point(50, 90)
            );
            return bitMapImage;
        }
    }
}
