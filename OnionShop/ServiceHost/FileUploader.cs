using _0_Framework.Application;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileUploader(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

       

        public string Upload(IFormFile file, string path)
        {
            if (file == null) return "";

            var directoryPath = $"{_hostEnvironment.WebRootPath}//ProductPictures//{path}";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var filePath = $"{directoryPath}//{file.FileName}";
            using var output =  File.Create(filePath);
            file.CopyTo(output);
            return $"{path}/{file.FileName}";
        }
    }
}
