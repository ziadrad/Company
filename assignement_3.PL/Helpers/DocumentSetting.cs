namespace assignement_3.PL.Helpers
{
    public class DocumentSetting
    {
       public static string UploadFile(IFormFile file , string FolderName) {
        
         var folderPath =    Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\files", FolderName);

            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            var filePath = Path.Combine(folderPath, fileName);
            var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }

        public static void DeletFile(string fileName, string FolderName)
        {

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", FolderName, fileName);

            if (File.Exists(filePath)) {
            File.Delete(filePath);
            }
        }

    }
}
