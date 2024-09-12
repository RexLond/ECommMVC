namespace ECommMVC.UI.Areas.Admin.Models
{
    public class FileSystem
    {
        // File Save
        public static async Task<string> SaveFileAsync(IFormFile file, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string originalFileName = Path.GetFileNameWithoutExtension(file.FileName);
            string fileExtension = Path.GetExtension(file.FileName);
            string fileName = originalFileName + fileExtension;
            string filePath = Path.Combine(directoryPath, fileName);

            int count = 1;
            while (System.IO.File.Exists(filePath))
            {
                string tempFileName = $"{originalFileName}_{count}{fileExtension}";
                filePath = Path.Combine(directoryPath, tempFileName);
                count++;
            }

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Path.GetFileName(filePath);
        }
    }
}
